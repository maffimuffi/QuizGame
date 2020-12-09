using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GameManager : MonoBehaviour
{

    /*
     * GameManager(gm) huolehtii kaikesta pelin perustoiminnasta, kun peliä pelataan. Gm tarkistaa kokoajan, missä tilassa(gameState) peli on, että mitä sen kuuluu tehdä.
     */
    public static GameManager instance;
    // Integers
    [HideInInspector] public int level;
    [HideInInspector] public int repeatRound = 0;
    [HideInInspector] public int correctAnswers;
    [HideInInspector] public int combinedCorrectAnswers;
    [HideInInspector] public int wrongAnswers;
    [HideInInspector] public int questionNumber;
    [HideInInspector] public int score;
    [HideInInspector] public int highscore;
    [HideInInspector] public int gameState;
    [HideInInspector] public int roundScore;
    [HideInInspector] public int highestRound;

    // Floats
    [HideInInspector] public float timeLeft = 60f;

    // Booleans
    [HideInInspector] public bool answering = false;
    [HideInInspector] public bool playerAnswer = false;
    [HideInInspector] public bool continuedToNextRound = false;
    public bool gameEnded = false;

    // GameObjects
    public GameObject infoScreen;
    public GameObject roundEndScreen;
    public GameObject gameEndScreen;
    public GameObject timerObject;


    // Texts
    public TMP_Text gameInfoText;
    public TMP_Text timerText;
    public TMP_Text questionInfoText;
    public TMP_Text roundEndText;
    public TMP_Text gameEndText;

    //Scripts
    public MainMenu mainMenu;
    public SettingsMenu settingsMenu;
    public DatabaseManager databaseManager;
    public Question question;
    public ColorThemeManager colorThemeManager;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }
        gameState = 0;
        databaseManager = GetComponent<DatabaseManager>();
        question = GetComponent<Question>();
        colorThemeManager = GetComponent<ColorThemeManager>();
        colorThemeManager.SetTheme();
        mainMenu.settingsScreen.SetActive(false);
        mainMenu.gameScreen.SetActive(false);
        settingsMenu.musicOn = true;
        settingsMenu.soundOn = true;

    }

    // Update is called once per frame
    void Update()
    {
        CheckGameState();
        if (gameState != 0 && colorThemeManager.canvas.name == "CanvasMobile")
        {
            gameInfoText.text = "P: " + score + "    " + "TP: " + roundScore + "/" + (level * 10) + "    " + "T: " + level + "    " + "K: " + questionNumber + "/10";
        }
        else if (gameState != 0 && colorThemeManager.canvas.name == "CanvasWeb")
        {
            gameInfoText.text = "Pisteet: " + score + "    " + "Tasopisteet: " + roundScore + "/" + (level * 10) + "    " + "Taso: " + level + "     " + "Kysymys: " + questionNumber + "/10";
        }
    }
    // Pelin aloitukseen tarkoitettu metodi, mikä sammuttaa väärät ikkunat ja avaa oikeat, ja nollaa kaikki arvot, kun päävalikon "Uusi peli"-nappi painetaan.
    public void StartNewGame()
    {
        mainMenu.mainmenuScreen.SetActive(false);
        mainMenu.gameScreen.SetActive(true);
        //infoScreen.SetActive(true);
        //gameEndScreen.SetActive(true);
        //roundEndScreen.SetActive(true);
        colorThemeManager.FillColorLists();
        colorThemeManager.SetTheme();
        infoScreen.SetActive(false);
        gameEndScreen.SetActive(false);
        roundEndScreen.SetActive(false);
        timerObject.SetActive(true);
        level = 1;
        correctAnswers = 0;
        combinedCorrectAnswers = 0;
        wrongAnswers = 0;
        questionNumber = 1;
        timeLeft = 60f;
        score = 0;
        roundScore = 0;
        highestRound = 1;
        answering = true;
        continuedToNextRound = false;
        gameState = 1;
        databaseManager.FetchQuestion();
        question.Initialize();
        SavePlayer();
    }

    // Metodi, mikä aloittaa uuden kierroksen/tason, kun "Seuraava taso"-nappia painetaan. Nollaa tarvittavat arvot ja vaihtaa pelin tilan.
    public void NewRound()
    {
        roundEndScreen.SetActive(false);
        timerObject.SetActive(true);
        correctAnswers = 0;
        questionNumber = 1;
        roundScore = 0;
        timeLeft = 60f;
        answering = true;
        gameState = 1;
        continuedToNextRound = false;
        databaseManager.difficulty = level.ToString();
        databaseManager.FetchQuestion();
        colorThemeManager.SetTheme();
    }

    // Metodi, mikä siirtää pelissä pelaajan seuraavaan kysymykseen, jos 10 kysymystä ei ole vielä käytä, muuten peli siirtyy tilaan, missä kierros on päättynyt.
    public void NextQuestion()
    {
        questionNumber++;
        if (questionNumber > 10)
        {
            questionNumber = 10;
            infoScreen.SetActive(false);
            gameState = 3;
        }
        else
        {
            infoScreen.SetActive(false);
            timerObject.SetActive(true);
            answering = true;
            timeLeft = 60f;
            gameState = 1;
            question.SetText();
        }
    }

    // Metodi, mitä kutsutaan, kun halutaan siirtyä seuraavalle tasolle(vaikka ei ylemmäs pääsisi). Pääperiaate on pitää roundEndScreen näkyvillä, ja tällä sen vasta saa sammutettua.
    public void ContinueToNextRound()
    {
        continuedToNextRound = true;
        roundEndScreen.SetActive(false);
    }

    // Metodi "Takasin valikkoon"-napille. Poistuu pelistä päävalikkoon, sulkee pelin ja tallentaa edistymisen.
    public void ExitToMenu()
    {
        infoScreen.SetActive(false);
        roundEndScreen.SetActive(false);
        mainMenu.gameScreen.SetActive(false);
        mainMenu.mainmenuScreen.SetActive(true);
        SavePlayer();
        gameState = 0;
    }

    // Pelin lopetukseen liittyvä metodi vimeisessä ikkunassa, mistä tällä siirrytään päävalikkoon.
    public void EndGame()
    {
        gameEndScreen.SetActive(false);
        mainMenu.gameScreen.SetActive(false);
        mainMenu.mainmenuScreen.SetActive(true);
        gameState = 0;
        SavePlayer();
    }

    // Metodi liitettynä oikean vastauksen nappiin. Tällä saadaan merkattua, että vastaus meni oikein, ja annetaan pelaajalle pisteet, jos he eivät ole päässeet kyseistä tasoa läpi.
    public void CorrectAnswer()
    {
        answering = false;
        correctAnswers++;
        combinedCorrectAnswers++;
        timerObject.SetActive(false);

        if (level == highestRound)
        {
            score += 1 * level;
        }

        roundScore += 1 * level;
        playerAnswer = true;

        if (settingsMenu.soundOn)
        {
            AudioManager.Instance.PlaySound("CorrectSFX");
        }
    }

    // Metodi liitettynä väärien vastausten nappeihin. Saadaan merkattua, että vastaus meni väärin.
    public void WrongAnswer()
    {
        answering = false;
        playerAnswer = false;
        wrongAnswers++;
        timerObject.SetActive(false);

        if (settingsMenu.soundOn)
        {
            AudioManager.Instance.PlaySound("IncorrectSFX");
        }
    }

    // Metodi pelin tallentamiseen.
    public void SavePlayer()
    {
        SaveSystem.SaveGame(this);
    }

    // Metodi pelin lataamiseen.
    public void LoadPlayer()
    {
        infoScreen.SetActive(false);
        gameEndScreen.SetActive(false);
        roundEndScreen.SetActive(false);
        question.Initialize();
        PlayerData data = SaveSystem.LoadGame();

        level = data.level;
        repeatRound = data.repeatRound;
        correctAnswers = data.correctAnswers;
        wrongAnswers = data.wrongAnswers;
        combinedCorrectAnswers = data.combinedCorrectAnswers;
        questionNumber = data.questionNumber;
        score = data.score;
        highscore = data.highscore;
        gameState = data.gameState;
        roundScore = data.roundScore;
        highestRound = data.highestRound;
        timeLeft = data.timeLeft;
        answering = data.answering;
        playerAnswer = data.playerAnswer;
        continuedToNextRound = data.continuedToNextRound;
        question.tenQuestions = data.questionList;
    }

    // Metodi, mikä huolehtii koko pelistä muuten. Tarkistaa, missä pelitilassa ollaan ja toimii sen mukaisesti.
    void CheckGameState()
    {
        // Kysymys-tila
        if (gameState == 1)
        {
            // Pelaaja on vastaamassa kysymykseen.
            if (answering)
            {
                // Alla oleva kannattaa poistaa peliä julkaistaessa! Space painamalla saa suoraan oikean vastauksen.
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    CorrectAnswer();
                }
                timeLeft -= Time.deltaTime;
                timerText.text = timeLeft.ToString("0");
            }
            // Tarkistetaan, jos pelaaja on vastannut kysymykseen niin siirrytään eteenpäin.
            if (!answering)
            {
                gameState = 2;
            }
            // Tarkistetaan, jos aika loppuu niin annetaan siitä pelaajalle väärä vastaus.
            if (timeLeft <= 0 && answering)
            {
                timeLeft = 0f;
                playerAnswer = false;
                wrongAnswers++;
                timerObject.SetActive(false);
                if (settingsMenu.soundOn)
                {
                    AudioManager.Instance.PlaySound("IncorrectSFX");
                }
                gameState = 2;
            }
        }
        // Tila minne siirrytään, kun kysymykseen on vastattu.
        if (gameState == 2)
        {
            // Pelaajan vastaus on oikein.
            if (playerAnswer == true)
            {
                infoScreen.SetActive(true);
                questionInfoText.text = "Oikein!";

            }
            // Pelaajan vastaus on väärin.
            else if (playerAnswer == false)
            {
                infoScreen.SetActive(true);
                questionInfoText.text = "Väärin!";
            }
        }
        // Tila minne siirrytään, kun tason kaikki 10 kysymystä on käyty läpi.
        if (gameState == 3)
        {
            roundEndScreen.SetActive(true);

            // Pelaaja on vastannut 8 tai enemmän oikein.
            if (correctAnswers >= 8)
            {
                // Tarkistetaan, että onko pelaajan suorittama taso alle 10 eli ei vielä viimeinen taso, muuten peli lopetetaan. Muuten siirrytään seuraavalle tasolle.
                if (level < 10)
                {
                    roundEndText.text = "Onnittelut, nouset seuraavalle tasolle!";
                    if (continuedToNextRound)
                    {
                        score -= 1 * repeatRound * level;
                        level++;
                        // Katsotaan, että jos seuraava taso on korkein mihin pelaaja on päässyt niin muutetaan highestRound siihen, että pisteen lasku toimii halutusti.
                        if (level > highestRound)
                        {
                            highestRound = level;
                        }

                        repeatRound = 0;
                        NewRound();
                    }
                }
                // Tänne siirrytään, jos pelaajan läpäisemä taso oli taso 10 eli pelin pitää päättyä eli siirrytään tilaan 4.
                else
                {
                    score -= 1 * repeatRound * level;
                    level = 10;
                    gameState = 4;
                }
            }
            // Pelaaja pysyy samalla tasolla.
            else if (correctAnswers > 4 && correctAnswers < 8)
            {
                roundEndText.text = "Sait alle 8/10 oikein. Pysyt samalla tasolla.";
                if (continuedToNextRound)
                {
                    repeatRound++;
                    score -= roundScore;
                    NewRound();
                }
            }
            // Pelaaja tippuu alemmalle tasolle, ja tekstiä muuten sen mukaan, että oliko pelaaja korkeammalla tasolla kuin 1 vai ei.
            else if (correctAnswers <= 4)
            {
                if (level > 1)
                {
                    roundEndText.text = "Sorry, liian monta väärää vastausta. Putoat alemmalle tasolle.";
                    if (continuedToNextRound)
                    {
                        score -= roundScore;
                        level--;
                        repeatRound = 0;
                        NewRound();
                    }
                }
                else if (level == 1)
                {
                    roundEndText.text = "Sorry, liian monta väärää vastausta. Pysyt tasolla 1.";
                    if (continuedToNextRound)
                    {
                        NewRound();
                    }
                }
            }
        }
        // Pelin lopetuksen tila. Avataan oikea ikkuna ja näytetään pelaajalle hänen "tilastonsa".
        if (gameState == 4)
        {
            roundEndScreen.SetActive(false);
            gameEndScreen.SetActive(true);

            gameEndText.text = "Onnittelut, pääsit loppuun asti!\n\n" + "Oikeat Vastaukset: " + combinedCorrectAnswers + "\n\n" + "Väärät Vastaukset: " + wrongAnswers + "\n\n" + "Pisteet: " + score + "/550";
            // Täältä voisi lähettää parhaan tuloksen tietokantaan, jos se on parempi tulos. Ei toiminnassa!!
            /*
            if (score > highscore)
            {
                highscore = score;
            }
            */
        }
    }
}