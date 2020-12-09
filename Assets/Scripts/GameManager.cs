using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GameManager : MonoBehaviour
{
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
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(this);
        }
        gameState = 0;
        databaseManager = GetComponent<DatabaseManager>();
        question = GetComponent<Question>();
        colorThemeManager = GetComponent<ColorThemeManager>();
        //colorThemeManager.SetTheme();
        mainMenu.settingsScreen.SetActive(false);
        mainMenu.gameScreen.SetActive(false);
        settingsMenu.musicOn = true;
        settingsMenu.soundOn = true;
      
    }

    // Update is called once per frame
    void Update()
    {
        CheckGameState();
        // Päivittää pelin ylälaidassa olevia piste-, taso-, ja kysymystekstejä eli pisteiden määrää, tasonumeroa ja sen hetkisen kysymyksen numneroa.
        if (gameState != 0)
        {
            gameInfoText.text = "Pisteet: " + score + "        " + "Taso: " + level + "         " + "Kysymys: " + questionNumber + "/10";
        }
    }
    // Metodi millä uusi peli käynnistetään päävalikosta. Asettaa kaikki seurattavat arvot nollille eli aloittaa pelin täysin puhtaalta pöydältä.
    public void StartNewGame()
    {
        mainMenu.mainmenuScreen.SetActive(false);
        mainMenu.gameScreen.SetActive(true);
        infoScreen.SetActive(true);
        gameEndScreen.SetActive(true);
        roundEndScreen.SetActive(true);
        //colorThemeManager.FillColorLists();
        //colorThemeManager.SetTheme();
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
        SavePlayer();
        databaseManager.FetchQuestion();
        question.Initialize();
    }
    // Metodi millä aloitetaan uusi kierros kun 10 kysymystä on käyty ja halutaan jatkaa eteenpäin.
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
        //colorThemeManager.SetTheme();
    }
    // Metodi millä siirrytään seuraavaan kysymykseen ellei 10 kysymystä ole jo käyty, koska sillon peli siirtyy seuraavaan tilaan, koska taso on päättynyt.
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
            //databaseManager.FetchQuestion();
            
        }
    }
    // Metodi mikä kutsutaan sen jälkeen, kun kierros päättynyt ja halutaan seuraavalle tasolle. Tällä tavalla saadaan siis tason päättymisruutu pidetty näkyvillä ja sitten tätä kutsuttaessa vasta mennään eteenpäin
    public void ContinueToNextRound()
    {
        continuedToNextRound = true;
        roundEndScreen.SetActive(false);
    }
    // Metodi millä päästään kysymysten ja kierroksien välissä takaisin päävalikkoon, jos ei haluta jatkaa pelaamista. Lopettaessa metodi tallentaa etenemisen pelissä.
    public void ExitToMenu()
    {
        infoScreen.SetActive(false);
        roundEndScreen.SetActive(false);
        mainMenu.gameScreen.SetActive(false);
        mainMenu.mainmenuScreen.SetActive(true);
        SavePlayer();
        gameState = 0;
    }
    // Metodia kutsutaan, kun pelin kaikki 10 tasoa on läpäisty ja pelistä poistutaan takaisin päävalikkoon.
    public void EndGame()
    {
        gameEndScreen.SetActive(false);
        mainMenu.gameScreen.SetActive(false);
        mainMenu.mainmenuScreen.SetActive(true);
        gameState = 0;
    }
    // Metodi, mikä on kiinni oikean vastauksen napissa. Saadaan tieto, että kysymys on mennyt oikein, ja annetaan pelaajalle pisteitä.
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

        if(settingsMenu.soundOn)
        {
            AudioManager.Instance.PlaySound("CorrectSFX");
        }
    }
    // Metodi, mikä on kiinni väärien vastauksien napeissa. Saadaan tieto, että kysymys on mennyt väärin.
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

    // Metodi, millä pelin eteneminen tallennetaan.
    public void SavePlayer()
    {
        SaveSystem.SaveGame(this);
    }
    // Metodi, millä ladataan aikaisempi eteneminen pelissä ja jatketaan sitä.
    public void LoadPlayer()
    {
        infoScreen.SetActive(false);
        gameEndScreen.SetActive(false);
        roundEndScreen.SetActive(false);
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

    // Tarkistaa, missä tilassa peli on
    void CheckGameState()
    {
        // gameState 1 on tilanne, missä pelaaja on vastaamassa kysymykseen ja aika kuluu. Sitten kun kysymykseen vastataan jotakin, peli siirtyy tilaan 2.
        if (gameState == 1)
        {
            if (answering)
            {
                // Tämä on vain testaukseen tarkoitettu helpotus millä saa aina oikean vastauksen välilyöntiä painamalla, kannattaa poistaa ennen pelin julkaisua.
                if(Input.GetKeyDown(KeyCode.Space))
                {
                    CorrectAnswer();
                }
                timeLeft -= Time.deltaTime;
                timerText.text = timeLeft.ToString("0");
            }
            // Tarkistetaan, jos pelaaja on vastannut niin siirrytään tilaan 2.
            if (!answering)
            {
                gameState = 2;
            }
            // Tarkistaa, jos aika menee 0, niin pelaajalle annetaan väärä vastaus
            if (timeLeft <= 0 && answering)
            {
                timeLeft = 0f;
                playerAnswer = false;
                gameState = 2;
            }
        }
        // Tila missä ollaan sillon kun pelaaja on vastannut kysymykseen eikä olla siirrytty seuraavaan kysymykseen.
        if (gameState == 2)
        {
            // Jos vastaus on oikea, ruudulle tulee teksti, että meni oikein. Tässä voisi olla se mahdollisuus saada kysymykseen tarkentava lisätieto teksti näkyviin tuon "Oikein" tilalle.
            if (playerAnswer == true)
            {
                infoScreen.SetActive(true);
                questionInfoText.text = "Oikein!";

            }
            // Jos vastaus on väärin, ruudulle tulee siitä teksti, että väärin meni.
            else if (playerAnswer == false)
            {
                infoScreen.SetActive(true);
                questionInfoText.text = "Väärin!";
            }
        }
        // Tila 3 on tilanne, minne peli siirtyy jos kaikkiin tason 10 kysymykseen on vastattu. Tarkistetaan paljonko pelaaja on vastannut oikein, ja edetään sen mukaan.
        if (gameState == 3)
        {
            roundEndScreen.SetActive(true);
            // Tänne mennään, jos pelaaja pääsee seuraavalle tasolle. Tarkistaa myös oliko kierros jo viimeinen, ja jos oli niin peli siirtyy tilaan 4.
            if (correctAnswers >= 8)
            {
                if(level < 10)
                {
                    roundEndText.text = "Pääset Uudelle Tasolle!";
                    if (continuedToNextRound)
                    {
                        score -= 1 * repeatRound * level;
                        level++;

                        if (level > highestRound)
                        {
                            highestRound = level;
                        }
                        
                        repeatRound = 0;
                        NewRound();
                    }
                }
                else
                {
                    score -= 1 * repeatRound * level;
                    level = 10;
                    gameState = 4;
                }
            }
            // Tänne mennään, jos pelaaja pysyy samalla tasolla eikä saanut tarpeeksi oikein, että nousisi tai liian vähän oikein, että tippuisi tasolta.
            else if (correctAnswers > 4 && correctAnswers < 8)
            {
                roundEndText.text = "Pysyt Samalla Tasolla!";
                if (continuedToNextRound)
                {
                    repeatRound++;
                    score -= roundScore;
                    NewRound();
                }
            }
            // Tänne mennään, jos pelaaja on vastannut liian vähän oikein, että pelaaja tiputetaan alemmalle tasolle, jos sen hetkinen taso ei ole 1.
            else if (correctAnswers <= 4)
            {
                if (level > 1)
                {
                    roundEndText.text = "Tiput Alemmalle Tasolle!";
                    if (continuedToNextRound)
                    {
                        score -= roundScore;
                        level--;
                        repeatRound = 0;
                        NewRound();
                    }
                }
                else if(level == 1)
                {
                    roundEndText.text = "Yritä Uudelleen!";
                    if (continuedToNextRound)
                    {
                        NewRound();
                    }
                }              
            }
        }
        // Tila minne mennään pelin päätyttyä. Avataan lopetus ruutu, missä näytetään paljonko on yhteensä saanut kysymyksiä oikein ja väärin ja myös yhteispisteet
        if (gameState == 4)
        {
            roundEndScreen.SetActive(false);
            gameEndScreen.SetActive(true);

            gameEndText.text = "Peli On Päättynyt!\n\n" + "Oikeat Vastaukset: " + combinedCorrectAnswers + "\n\n" + "Väärät Vastaukset: " + wrongAnswers + "\n\n" + "Pisteet: " + score;

            if (score > highscore)
            {
                highscore = score;
                // Update highscore in db
            }
        }
    }
}