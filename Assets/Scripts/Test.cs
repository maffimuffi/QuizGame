using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{

    public GameManager manager;
    public MainMenu mainMenu;

    public GameObject answer1;
    public GameObject answer2;
    public GameObject infoScreen;

    public Text scoreText;
    public TMPro.TMP_Text timerText;
    public TMPro.TMP_Text infoText;

    private bool answering;

    private float timer;

    private void Start()
    {
        answering = true;
        timer = 60f;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Pisteet: " + manager.score + "        " + "Taso: " + manager.level + "         " + "Kysymys: " + manager.questionNumber + "/10";
        //questionNumberText.text = "Kysymys: " + manager.questionNumber + "/10";
        //levelText.text = "Taso: " + manager.level;
        timerText.text = timer.ToString("0");

        if(answering)
        {
            timer -= Time.deltaTime;
            if(timer <= 0 && answering)
            {
                timer = 0f;
                WrongAnswer();
                infoText.text = "Aika loppui!";
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

    public void ExitToMenu()
    {
        infoScreen.SetActive(false);
        mainMenu.gameScreen.SetActive(false);
        mainMenu.mainmenuScreen.SetActive(true);
    }
}
