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
        if (gameState != 0)
        {
            gameInfoText.text = "Pisteet: " + score + "        " + "Taso: " + level + "         " + "Kysymys: " + questionNumber + "/10";
        }
    }

    public void StartNewGame()
    {
        mainMenu.mainmenuScreen.SetActive(false);
        mainMenu.gameScreen.SetActive(true);
        infoScreen.SetActive(true);
        gameEndScreen.SetActive(true);
        roundEndScreen.SetActive(true);
        colorThemeManager.FillColorLists();
        colorThemeManager.SetTheme();
        infoScreen.SetActive(false);
        gameEndScreen.SetActive(false);
        roundEndScreen.SetActive(false);
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

    


    public void NewRound()
    {
        roundEndScreen.SetActive(false);
        correctAnswers = 0;
        questionNumber = 1;
        roundScore = 0;
        timeLeft = 60f;
        answering = true;
        gameState = 1;
        continuedToNextRound = false;
        databaseManager.FetchQuestion();
        colorThemeManager.SetTheme();
    }

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
            answering = true;
            timeLeft = 60f;
            gameState = 1;
            question.SetText();
            //databaseManager.FetchQuestion();
            
        }
    }

    public void ContinueToNextRound()
    {
        
        continuedToNextRound = true;
        roundEndScreen.SetActive(false);
    }

    public void ExitToMenu()
    {
        infoScreen.SetActive(false);
        roundEndScreen.SetActive(false);
        mainMenu.gameScreen.SetActive(false);
        mainMenu.mainmenuScreen.SetActive(true);
        SavePlayer();
        gameState = 0;
    }

    public void EndGame()
    {
        gameEndScreen.SetActive(false);
        mainMenu.gameScreen.SetActive(false);
        mainMenu.mainmenuScreen.SetActive(true);
        gameState = 0;
    }

    public void CorrectAnswer()
    {
        answering = false;
        correctAnswers++;
        combinedCorrectAnswers++;

        if(level == highestRound)
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

    public void WrongAnswer()
    {
        answering = false;
        playerAnswer = false;
        wrongAnswers++;

        if (settingsMenu.soundOn)
        {
            AudioManager.Instance.PlaySound("IncorrectSFX");
        }
    }

    // Methods for saving and loading the game
    public void SavePlayer()
    {
        SaveSystem.SaveGame(this);
    }

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

    // Checks what state the game is currently in
    void CheckGameState()
    {
        if (gameState == 1)
        {
            // Player is answering a question
            if (answering)
            {
                if(Input.GetKeyDown(KeyCode.Space))
                {
                    CorrectAnswer();
                }
                timeLeft -= Time.deltaTime;
                timerText.text = timeLeft.ToString("0");
            }
            // Must check if the player has answered
            if (!answering)
            {
                gameState = 2;
            }
            // If timer goes down to 0, player failed
            if (timeLeft <= 0 && answering)
            {
                timeLeft = 0f;
                playerAnswer = false;
                gameState = 2;
            }
        }
        if (gameState == 2)
        {
            
            // Player answered a question
            if (playerAnswer == true)
            {
                infoScreen.SetActive(true);
                questionInfoText.text = "Oikein!";

            }
            else if (playerAnswer == false)
            {
                infoScreen.SetActive(true);
                questionInfoText.text = "Väärin!";
            }
        }
        if (gameState == 3)
        {
            // Level has ended
            
            roundEndScreen.SetActive(true);
            // Player gets promoted to the next level!
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
            // Player stays in the current level
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
            // Player gets demoted to the lower level
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

        if (gameState == 4)
        {
            
            // Game has ended, open game ending screen
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