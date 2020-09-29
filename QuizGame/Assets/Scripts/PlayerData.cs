using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{

    public int level;
    public int correctAnswers;
    public int wrongAnswers;
    public int questionsLeft;
    public int score;
    public int highscore;
    public int gameState;

    public float timeLeft;

    public bool roundEnded;
    public bool answering;
    public bool playerAnswer;

    public PlayerData(GameManager manager)
    {
        level = manager.level;
        correctAnswers = manager.correctAnswers;
        wrongAnswers = manager.wrongAnswers;
        questionsLeft = manager.questionsLeft;
        score = manager.score;
        highscore = manager.highscore;
        gameState = manager.gameState;

        timeLeft = manager.timeLeft;

        roundEnded = manager.roundEnded;
        answering = manager.answering;
        playerAnswer = manager.playerAnswer;
    }

}
