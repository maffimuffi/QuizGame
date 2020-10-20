﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * GameManager keeps track on what state game is in.
 * Is the game on or in the main menu
 * Did the game just start
 * Is the player answering a question
 * Has the player answered and what is the result
 * Has a level or round ended
 * Has the game ended
 */
public class CopyTest : MonoBehaviour
{
    // Integers
    [HideInInspector]
    public int level;
    //[HideInInspector]
    public int correctAnswers;
    [HideInInspector]
    public int questionNumber;
    [HideInInspector]
    public int score;
    [HideInInspector]
    public int highscore;
    public int gameState;

    // Floatssdf
    public float timeLeft = 60f;

    // Booleans
    public bool answering = false;
    public bool playerAnswer = false;
    public bool continuedToNextRound = false;

    // Buttons
    public GameObject correctButton;
    public GameObject wrongButton1;
    public GameObject wrongButton2;
    public GameObject wrongButton3;

    // GameObjects
    public GameObject infoScreen;
    public GameObject roundEndScreen;

    // Texts
    public TMPro.TMP_Text gameInfoText;
    public TMPro.TMP_Text timerText;
    public TMPro.TMP_Text questionInfoText;
    public TMPro.TMP_Text roundEndText;

    //Scripts
    public MainMenu mainMenu;

    // Answer buttons positions
    public Vector3 answerPosition1;
    public Vector3 answerPosition2;
    public Vector3 answerPosition3;
    public Vector3 answerPosition4;

    private void Awake()
    {
        answerPosition1 = correctButton.transform.position;
        answerPosition2 = wrongButton1.transform.position;
        answerPosition3 = wrongButton2.transform.position;
        answerPosition4 = wrongButton3.transform.position;

        gameState = 0;
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

    public void ContinueGame()
    {
        LoadPlayer();
    }

    public void StartNewGame()
    {
        level = 1;
        correctAnswers = 0;
        questionNumber = 1;
        timeLeft = 60f;
        score = 0;
        answering = true;
        continuedToNextRound = false;
        gameState = 2;
        SavePlayer();
    }

    public void NewRound()
    {
        roundEndScreen.SetActive(false);
        questionNumber = 1;
        timeLeft = 60f;
        answering = true;
        gameState = 2;
    }

    public void NextQuestion()
    {
        questionNumber++;
        if (questionNumber > 10)
        {
            questionNumber = 10;
            infoScreen.SetActive(false);
            gameState = 4;
        }
        else
        {
            answering = true;
            timeLeft = 60f;
            infoScreen.SetActive(false);
            gameState = 2;
        }
    }

    public void ExitToMenu()
    {
        infoScreen.SetActive(false);
        roundEndScreen.SetActive(false);
        mainMenu.gameScreen.SetActive(false);
        mainMenu.mainmenuScreen.SetActive(true);
    }

    public void ContinueToNextRound()
    {
        continuedToNextRound = true;
        roundEndScreen.SetActive(false);
    }

    public void CorrectAnswer()
    {
        answering = false;
        score += 100;
        playerAnswer = true;
    }

    public void WrongAnswer()
    {
        answering = false;
        playerAnswer = false;
    }

    // Methods for saving and loading the game
    public void SavePlayer()
    {
        //SaveSystem.SaveGame(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadGame();

        level = data.level;
        correctAnswers = data.correctAnswers;
        score = data.score;
        highscore = data.highscore;
        gameState = data.gameState;
        timeLeft = data.timeLeft;
        answering = data.answering;
        playerAnswer = data.playerAnswer;
    }

    // Checks what state the game is currently in
    void CheckGameState()
    {
        if (gameState == 0)
        {
            // Game is not on and the game is in the main menu
        }
        if (gameState == 1)
        {
            StartNewGame();
        }
        if (gameState == 2)
        {
            // Player is answering a question
            if (answering)
            {
                timeLeft -= Time.deltaTime;
                timerText.text = timeLeft.ToString("0");
            }
            // Must check if the player has answered
            if (!answering)
            {
                gameState = 3;
            }
            // If timer goes down to 0, player failed
            if (timeLeft <= 0 && answering)
            {
                timeLeft = 0f;
                playerAnswer = false;
                gameState = 3;
            }
        }
        if (gameState == 3)
        {
            // Player answered a question
            if (playerAnswer == true)
            {
                correctAnswers++;
                infoScreen.SetActive(true);
                questionInfoText.text = "Oikein!";
            }
            else if (playerAnswer == false)
            {
                infoScreen.SetActive(true);
                questionInfoText.text = "Väärin!";
            }
        }
        if (gameState == 4)
        {
            // Level has ended
            continuedToNextRound = false;
            roundEndScreen.SetActive(true);
            // Player gets promoted to the next level!
            if (correctAnswers >= 8)
            {
                if (!continuedToNextRound)
                {
                    roundEndText.text = "Pääset Uudelle Tasolle!";
                    if (continuedToNextRound)
                    {
                        level++;
                        NewRound();
                    }
                }
            }
            // Player stays in the current level
            if (correctAnswers >= 5 && correctAnswers <= 7)
            {
                if (!continuedToNextRound)
                {
                    roundEndText.text = "Pysyt Samalla Tasolla!";
                    if (continuedToNextRound)
                    {
                        NewRound();
                    }
                }
            }
            // Player gets demoted to the lower level
            if (correctAnswers >= 0 && correctAnswers <= 4)
            {
                if (!continuedToNextRound)
                {
                    if (level > 1)
                    {
                        roundEndText.text = "Tiput Alemmalle Tasolle!";
                        if (continuedToNextRound)
                        {
                            level--;
                            NewRound();
                        }
                    }
                    else
                    {
                        roundEndText.text = "Yritä Uudelleen!";
                        if (continuedToNextRound)
                        {
                            NewRound();
                        }
                    }
                }
            }
        }

        if (gameState == 5)
        {
            // Game has ended, open game ending screen
            // Open that shiit
            if (score > highscore)
            {
                highscore = score;
                // Update highscore in db
            }
            // Finally after exiting the ending screen somehow change gameState to 0
        }
    }
}


