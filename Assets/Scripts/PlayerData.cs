using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{

    public int level;
    public int repeatRound;
    public int correctAnswers;
    public int wrongAnswers;
    public int combinedCorrectAnswers;
    public int questionNumber;
    public int score;
    public int highscore;
    public int gameState;
    public int roundScore;
    public int highestRound;

    public float timeLeft;

    public bool answering;
    public bool playerAnswer;
    public bool continuedToNextRound;
    
    public List<QuestionList> questionList = new List<QuestionList>();

    public PlayerData(GameManager manager)
    {
        level = manager.level;
        repeatRound = manager.repeatRound;
        correctAnswers = manager.correctAnswers;
        wrongAnswers = manager.wrongAnswers;
        combinedCorrectAnswers = manager.combinedCorrectAnswers;
        questionNumber = manager.questionNumber;
        score = manager.score;
        highscore = manager.highscore;
        gameState = manager.gameState;
        roundScore = manager.roundScore;
        highestRound = manager.highestRound;

        timeLeft = manager.timeLeft;

        answering = manager.answering;
        playerAnswer = manager.playerAnswer;
        continuedToNextRound = manager.continuedToNextRound;
        foreach(QuestionList q in manager.question.tenQuestions)
        {
            questionList.Add(q);
        }
    }

}
