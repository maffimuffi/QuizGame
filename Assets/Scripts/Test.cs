using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{

    public GameManager manager;

    public GameObject answer1;
    public GameObject answer2;
    public GameObject infoScreen;

    public Text scoreText;
    public Text questionNumberText;
    public Text levelText;
    public TMPro.TMP_Text timerText;
    public Text infoText;

    private bool answered;
    private bool answering;

    private float timer;

    private void Start()
    {
        answered = false;
        answering = true;
        timer = 60f;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Pisteet: " + manager.score;
        questionNumberText.text = "Kysymys: " + manager.questionNumber + "/10";
        levelText.text = "Taso: " + manager.level;
        timerText.text = timer.ToString("0");

        if(answering)
        {
            timer -= Time.deltaTime;
            if(timer <= 0 && answering)
            {
                timer = 0f;
                WrongAnswer();
                infoText.text = "Ei nyt menny ihan putkeen vai?";
            }
        }

    }

    public void RightAnswer()
    {
        manager.correctAnswers++;
        manager.score += 100;
        infoScreen.SetActive(true);
        infoText.text = "Oikein!";
        answering = false;
    }

    public void WrongAnswer()
    {
        answering = false;
        infoScreen.SetActive(true);
        infoText.text = "Väärin!";
    }

    public void NextQ()
    {
        manager.questionNumber++;
        if (manager.questionNumber > 10)
        {
            if (manager.correctAnswers >= 8)
            {
                infoText.text = "Pääset Uudelle Tasolle!";
                manager.level++;
            }
            if(manager.correctAnswers < 8 && manager.correctAnswers > 4)
            {
                infoText.text = "Pysyt Samalla Tasolla!";
            }
            if (manager.correctAnswers <= 4 && manager.level > 1)
            {
                infoText.text = "Tiput Alemmalle Tasolle!";
                manager.level--;
            }
            manager.questionNumber = 1;
            manager.correctAnswers = 0;
        }
        answering = true;
        timer = 60f;
        infoScreen.SetActive(false);
    }
}
