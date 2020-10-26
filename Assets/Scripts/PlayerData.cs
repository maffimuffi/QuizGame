using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{

    public int level;
    public int correctAnswers;
    public int wrongAnswers;
    public int score;
    public int highscore;
    public int gameState;

    public float timeLeft;

    public bool answering;
    public bool playerAnswer;

    public PlayerData(GameManager manager)
    {
        level = manager.level;
        correctAnswers = manager.correctAnswers;
        score = manager.score;
        highscore = manager.highscore;
        gameState = manager.gameState;

        timeLeft = manager.timeLeft;

        answering = manager.answering;
        playerAnswer = manager.playerAnswer;
    }

}
