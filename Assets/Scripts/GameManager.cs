using System.Collections;
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
public class GameManager : MonoBehaviour
{

    public int level = 1;
    public int correctAnswers = 0;
    public int wrongAnswers = 0;
    public int questionsLeft = 10;
    public int score = 0;
    public int highscore;
    public int gameState = 0;

    public float timeLeft = 60f;

    public bool roundEnded = false;
    public bool answering = false;
    public bool playerAnswer = false;

    // Function is called at the start of a game
    void StartGame()
    {
        level = 1;
        correctAnswers = 0;
        wrongAnswers = 0;
        questionsLeft = 10;
        timeLeft = 60f;
        score = 0;
        answering = true;
        gameState = 2;
    }

    void NewRound()
    {
        correctAnswers = 0;
        wrongAnswers = 0;
        questionsLeft = 10;
        timeLeft = 60f;
        answering = true;
    }

    void NextQuestion()
    {
        timeLeft = 60f;
        answering = true;
        gameState = 2;
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
            // Game starts and "opens the game screen"
            StartGame();
        }
        if (gameState == 2)
        {
            // Player is answering a question
            if (answering)
            {
                timeLeft -= Time.deltaTime;
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
            answering = false; // Possibly not needed
            if (playerAnswer == true)
            {
                correctAnswers++;
                questionsLeft--;
                if (questionsLeft < 1)
                {
                    gameState = 4;
                }

                // Score is added based on the level in question
                score += level;
            }
            else if (playerAnswer == false)
            {
                wrongAnswers++;
                questionsLeft--;
                if (questionsLeft < 1)
                {
                    gameState = 4;
                }
            }
        }
        if (gameState == 4)
        {
            // Level has ended

            // Player gets promoted to the next level!
            if (correctAnswers >= 8)
            {
                level++;
                NewRound();
            }
            // Player stays in the current level
            if (correctAnswers >= 5 && correctAnswers <= 7)
            {
                NewRound();
            }
            // Player gets demoted to the lower level
            if (correctAnswers >= 0 && correctAnswers <= 4)
            {
                level--;
                NewRound();
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

    // Update is called once per frame
    void Update()
    {
        CheckGameState();
    }
}


