using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Question : MonoBehaviour
{
    public int answerSize = 4;
    public TMP_Text QuestionText;
    public TMP_Text AnswerAText;
    public TMP_Text AnswerBText;
    public TMP_Text AnswerCText;
    public TMP_Text AnswerDText;
    public Button AnswerAButton;
    public Button AnswerBButton;
    public Button AnswerCButton;
    public Button AnswerDButton;
    public List <Button> AnswerButtons = new List<Button>();
    public int correctAnswer;

    // Start is called before the first frame update
    void Start()
    {
        AnswerAText = AnswerAButton.GetComponentInChildren<TMP_Text>();
        AnswerBText = AnswerBButton.GetComponentInChildren<TMP_Text>();
        AnswerCText = AnswerCButton.GetComponentInChildren<TMP_Text>();
        AnswerDText = AnswerDButton.GetComponentInChildren<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // remove listeners from buttons
    public void ClearButtonListeners()
    {
        AnswerAButton.onClick.RemoveAllListeners();
        AnswerBButton.onClick.RemoveAllListeners();
        AnswerCButton.onClick.RemoveAllListeners();
        AnswerDButton.onClick.RemoveAllListeners();
    }
    
    public void NewQuestion(string questText,int rightAnswer)
    {
        QuestionText.text = questText;
        correctAnswer = rightAnswer;

    }
    public void RandomizeOrder()
    {

    }
}
