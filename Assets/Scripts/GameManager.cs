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
        if (gameState != 0 && colorThemeManager.canvas.name == "CanvasMobile")
        {
            gameInfoText.text = "P: " + score + "    " + "TP: " + roundScore + "/" + (level * 10) + "    " + "T: " + level + "    " + "K: " + questionNumber + "/10";
        }
        else if(gameState != 0 && colorThemeManager.canvas.name == "CanvasWeb")
        {
            gameInfoText.text = "Pisteet: " + score + "    " + "Tasopisteet: " + roundScore + "/" + (level * 10) + "    " + "Taso: " + level + "     " + "Kysymys: " + questionNumber + "/10";
        }
    }
<<<<<<< Updated upstream
    // Metodi millä uusi peli käynnistetään päävalikosta. Asettaa kaikki seurattavat arvot nollille eli aloittaa pelin täysin puhtaalta pöydältä.
=======
    // Pelin aloitukseen tarkoitettu metodi, mikä sammuttaa väärät ikkunat ja avaa oikeat, ja nollaa kaikki arvot, kun päävalikon "Uusi peli"-nappi painetaan.
>>>>>>> Stashed changes
    public void StartNewGame()
    {
        mainMenu.mainmenuScreen.SetActive(false);
        mainMenu.gameScreen.SetActive(true);
<<<<<<< Updated upstream
        infoScreen.SetActive(true);
        gameEndScreen.SetActive(true);
        roundEndScreen.SetActive(true);
        //colorThemeManager.FillColorLists();
        //colorThemeManager.SetTheme();
=======
        //infoScreen.SetActive(true);
        //gameEndScreen.SetActive(true);
        //roundEndScreen.SetActive(true);
        colorThemeManager.FillColorLists();
        colorThemeManager.SetTheme();
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
    // Metodi millä aloitetaan uusi kierros kun 10 kysymystä on käyty ja halutaan jatkaa eteenpäin.
=======

    // Metodi, mikä aloittaa uuden kierroksen/tason, kun "Seuraava taso"-nappia painetaan. Nollaa tarvittavat arvot ja vaihtaa pelin tilan.
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
    // Metodi millä siirrytään seuraavaan kysymykseen ellei 10 kysymystä ole jo käyty, koska sillon peli siirtyy seuraavaan tilaan, koska taso on päättynyt.
=======

    // Metodi, mikä siirtää pelissä pelaajan seuraavaan kysymykseen, jos 10 kysymystä ei ole vielä käytä, muuten peli siirtyy tilaan, missä kierros on päättynyt.
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
    // Metodi mikä kutsutaan sen jälkeen, kun kierros päättynyt ja halutaan seuraavalle tasolle. Tällä tavalla saadaan siis tason päättymisruutu pidetty näkyvillä ja sitten tätä kutsuttaessa vasta mennään eteenpäin
    public void ContinueToNextRound()
    {
        continuedToNextRound = true;
        roundEndScreen.SetActive(false);
    }
    // Metodi millä päästään kysymysten ja kierroksien välissä takaisin päävalikkoon, jos ei haluta jatkaa pelaamista. Lopettaessa metodi tallentaa etenemisen pelissä.
=======

    // Metodi, mitä kutsutaan, kun halutaan siirtyä seuraavalle tasolle(vaikka ei ylemmäs pääsisi). Pääperiaate on pitää roundEndScreen näkyvillä, ja tällä sen vasta saa sammutettua.
    public void ContinueToNextRound()
    {      
        continuedToNextRound = true;
        roundEndScreen.SetActive(false);
    }

    // Metodi "Takasin valikkoon"-napille. Poistuu pelistä päävalikkoon, sulkee pelin ja tallentaa edistymisen.
>>>>>>> Stashed changes
    public void ExitToMenu()
    {
        infoScreen.SetActive(false);
        roundEndScreen.SetActive(false);
        mainMenu.gameScreen.SetActive(false);
        mainMenu.mainmenuScreen.SetActive(true);
        SavePlayer();
        gameState = 0;
    }
<<<<<<< Updated upstream
    // Metodia kutsutaan, kun pelin kaikki 10 tasoa on läpäisty ja pelistä poistutaan takaisin päävalikkoon.
=======

    // Pelin lopetukseen liittyvä metodi vimeisessä ikkunassa, mistä tällä siirrytään päävalikkoon.
>>>>>>> Stashed changes
    public void EndGame()
    {
        gameEndScreen.SetActive(false);
        mainMenu.gameScreen.SetActive(false);
        mainMenu.mainmenuScreen.SetActive(true);
        gameState = 0;
        SavePlayer();
    }
<<<<<<< Updated upstream
    // Metodi, mikä on kiinni oikean vastauksen napissa. Saadaan tieto, että kysymys on mennyt oikein, ja annetaan pelaajalle pisteitä.
=======

    // Metodi liitettynä oikean vastauksen nappiin. Tällä saadaan merkattua, että vastaus meni oikein, ja annetaan pelaajalle pisteet, jos he eivät ole päässeet kyseistä tasoa läpi.
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
    // Metodi, mikä on kiinni väärien vastauksien napeissa. Saadaan tieto, että kysymys on mennyt väärin.
=======

    // Metodi liitettynä väärien vastausten nappeihin. Saadaan merkattua, että vastaus meni väärin.
>>>>>>> Stashed changes
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

<<<<<<< Updated upstream
    // Metodi, millä pelin eteneminen tallennetaan.
=======
    // Metodi pelin tallentamiseen.
>>>>>>> Stashed changes
    public void SavePlayer()
    {
        SaveSystem.SaveGame(this);
    }
<<<<<<< Updated upstream
    // Metodi, millä ladataan aikaisempi eteneminen pelissä ja jatketaan sitä.
=======

    // Metodi pelin lataamiseen.
>>>>>>> Stashed changes
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

<<<<<<< Updated upstream
    // Tarkistaa, missä tilassa peli on
    void CheckGameState()
    {
        // gameState 1 on tilanne, missä pelaaja on vastaamassa kysymykseen ja aika kuluu. Sitten kun kysymykseen vastataan jotakin, peli siirtyy tilaan 2.
        if (gameState == 1)
        {
            if (answering)
            {
                // Tämä on vain testaukseen tarkoitettu helpotus millä saa aina oikean vastauksen välilyöntiä painamalla, kannattaa poistaa ennen pelin julkaisua.
=======
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
>>>>>>> Stashed changes
                if(Input.GetKeyDown(KeyCode.Space))
                {
                    CorrectAnswer();
                }
                timeLeft -= Time.deltaTime;
                timerText.text = timeLeft.ToString("0");
            }
<<<<<<< Updated upstream
            // Tarkistetaan, jos pelaaja on vastannut niin siirrytään tilaan 2.
=======
            // Tarkistetaan, jos pelaaja on vastannut kysymykseen niin siirrytään eteenpäin.
>>>>>>> Stashed changes
            if (!answering)
            {
                gameState = 2;
            }
<<<<<<< Updated upstream
            // Tarkistaa, jos aika menee 0, niin pelaajalle annetaan väärä vastaus
=======
            // Tarkistetaan, jos aika loppuu niin annetaan siitä pelaajalle väärä vastaus.
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
        // Tila missä ollaan sillon kun pelaaja on vastannut kysymykseen eikä olla siirrytty seuraavaan kysymykseen.
        if (gameState == 2)
        {
            // Jos vastaus on oikea, ruudulle tulee teksti, että meni oikein. Tässä voisi olla se mahdollisuus saada kysymykseen tarkentava lisätieto teksti näkyviin tuon "Oikein" tilalle.
=======
        // Tila minne siirrytään, kun kysymykseen on vastattu.
        if (gameState == 2)
        {
            // Pelaajan vastaus on oikein.
>>>>>>> Stashed changes
            if (playerAnswer == true)
            {
                infoScreen.SetActive(true);
                questionInfoText.text = "Oikein!";

            }
<<<<<<< Updated upstream
            // Jos vastaus on väärin, ruudulle tulee siitä teksti, että väärin meni.
=======
            // Pelaajan vastaus on väärin.
>>>>>>> Stashed changes
            else if (playerAnswer == false)
            {
                infoScreen.SetActive(true);
                questionInfoText.text = "Väärin!";
            }
        }
<<<<<<< Updated upstream
        // Tila 3 on tilanne, minne peli siirtyy jos kaikkiin tason 10 kysymykseen on vastattu. Tarkistetaan paljonko pelaaja on vastannut oikein, ja edetään sen mukaan.
        if (gameState == 3)
        {
            roundEndScreen.SetActive(true);
            // Tänne mennään, jos pelaaja pääsee seuraavalle tasolle. Tarkistaa myös oliko kierros jo viimeinen, ja jos oli niin peli siirtyy tilaan 4.
=======
        // Tila minne siirrytään, kun tason kaikki 10 kysymystä on käyty läpi.
        if (gameState == 3)
        {
            roundEndScreen.SetActive(true);

            // Pelaaja on vastannut 8 tai enemmän oikein.
>>>>>>> Stashed changes
            if (correctAnswers >= 8)
            {
                // Tarkistetaan, että onko pelaajan suorittama taso alle 10 eli ei vielä viimeinen taso, muuten peli lopetetaan. Muuten siirrytään seuraavalle tasolle.
                if(level < 10)
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
<<<<<<< Updated upstream
            // Tänne mennään, jos pelaaja pysyy samalla tasolla eikä saanut tarpeeksi oikein, että nousisi tai liian vähän oikein, että tippuisi tasolta.
=======
            // Pelaaja pysyy samalla tasolla.
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
            // Tänne mennään, jos pelaaja on vastannut liian vähän oikein, että pelaaja tiputetaan alemmalle tasolle, jos sen hetkinen taso ei ole 1.
=======
            // Pelaaja tippuu alemmalle tasolle, ja tekstiä muuten sen mukaan, että oliko pelaaja korkeammalla tasolla kuin 1 vai ei.
>>>>>>> Stashed changes
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
                else if(level == 1)
                {
                    roundEndText.text = "Sorry, liian monta väärää vastausta. Pysyt tasolla 1.";
                    if (continuedToNextRound)
                    {
                        NewRound();
                    }
                }              
            }
        }
<<<<<<< Updated upstream
        // Tila minne mennään pelin päätyttyä. Avataan lopetus ruutu, missä näytetään paljonko on yhteensä saanut kysymyksiä oikein ja väärin ja myös yhteispisteet
        if (gameState == 4)
        {
=======
        // Pelin lopetuksen tila. Avataan oikea ikkuna ja näytetään pelaajalle hänen "tilastonsa".
        if (gameState == 4)
        {           
>>>>>>> Stashed changes
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