using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Question : MonoBehaviour
{
    public int answerSize = 4;
    public List<string> stringList = new List<string>();
    public List<List<string>> questionList = new List<List<string>>();
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
    

    // Start is called before the first frame update
    void Start()
    {
        
        AnswerAText = AnswerAButton.GetComponentInChildren<TMP_Text>();
        AnswerBText = AnswerBButton.GetComponentInChildren<TMP_Text>();
        AnswerCText = AnswerCButton.GetComponentInChildren<TMP_Text>();
        AnswerDText = AnswerDButton.GetComponentInChildren<TMP_Text>();
        for(int i = 1;i<answerSize;i++)
        {
            AnswerButtons[i].onClick.AddListener(() => WrongAnswer());
        }
        AnswerAButton.onClick.AddListener (()=> CorrectAnswer());
    }
    public void WrongAnswer()
    {
        Debug.Log("Väärin meni");
    }
    public void CorrectAnswer()
    {
        Debug.Log("Oikein");
    }
    public void Parse(string text)
    {
        string tempString = "";
        
        foreach(char c in text )
        {
            
            if(c== '_')
            {
                stringList.Add(tempString);
                tempString="";
            }
            else if(c=='/')
            {
                stringList.Add(tempString);
                tempString="";
            }
            else
            {
                tempString += c;
            }
        }
        
        SetText();
    }
    public void SetText()
    {
        QuestionText.text = stringList[0];
        AnswerAText.text = stringList[1];
        AnswerBText.text = stringList[2];
        AnswerCText.text = stringList[3];
        AnswerDText.text = stringList[4];
        ShuffleButtons();
    }
    public void ShuffleButtons()
    {
        
        int randomNumber;
        List<int> takenNumber = new List<int>();
        takenNumber.Clear();
        for(int i = 0; i<answerSize;i++)
        {

            
            do
            {
                randomNumber = UnityEngine.Random.Range(0,answerSize);
                 
            }while(takenNumber.Contains(randomNumber));
            takenNumber.Add(randomNumber);
            AnswerButtons[i].transform.SetSiblingIndex(randomNumber);
            
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ShuffleButtons();
        }
    }
    // remove listeners from buttons
    public void ClearButtonListeners()
    {
        AnswerAButton.onClick.RemoveAllListeners();
        AnswerBButton.onClick.RemoveAllListeners();
        AnswerCButton.onClick.RemoveAllListeners();
        AnswerDButton.onClick.RemoveAllListeners();
    }
    
    public void NewQuestion()
    {
       

    }
    public void RandomizeOrder()
    {

    }
}
