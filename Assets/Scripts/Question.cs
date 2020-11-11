using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Question : MonoBehaviour
{
    public int answerSize = 4;
    public List<string> stringList = new List<string>();
    public List<string> finalStringList = new List<string>();
    
    public List<QuestionList> questionList = new List<QuestionList>();
    public List<QuestionList> tenQuestions = new List<QuestionList>();
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
    public List<int> UsedQuestions = new List<int>();
    
    public void Initialize()
    {
        Debug.Log("Initialized Question");
        AnswerAText = AnswerAButton.GetComponentInChildren<TMP_Text>();
        AnswerBText = AnswerBButton.GetComponentInChildren<TMP_Text>();
        AnswerCText = AnswerCButton.GetComponentInChildren<TMP_Text>();
        AnswerDText = AnswerDButton.GetComponentInChildren<TMP_Text>();
    }
    public void Parse(string text)
    {
        stringList.Clear();
        questionList.Clear();
        string tempString = "";
        QuestionList list = new QuestionList();
        
        foreach(char c in text )
        {
            if(c==';')
            {
                tempString ="";
            }
            else if(c== '{')
            {
                if(list != null)
                {
                    if(!UsedQuestions.Contains( int.Parse(tempString)))
                    {
                        list.id = int.Parse(tempString);
                        questionList.Add(list);
                    }
                    else
                    {
                        Debug.Log("Question has been used");
                    }                                                     
                }
                //Debug.Log(int.Parse(tempString));
                tempString = "";
            }
            else if(c== '_')
            {
                stringList.Add(tempString);
                tempString="";
            }
            else if(c=='/')
            {
                stringList.Add(tempString);
                tempString="";
                list = new QuestionList();
                foreach(string s in stringList)
                {
                    list.optionsList.Add(s);
                }
                stringList.Clear();
            }
            else
            {
                tempString += c;
            }
        }
        
        RandomQuestion();
    }
    public void RandomQuestion()
    {   
        tenQuestions.Clear();
        for(int j = 0;j<10;j++){
            int randomInt = 0;
            finalStringList.Clear();
            stringList.Clear();
            if(questionList.Count ==0)
            {
                Debug.Log("No unused questions found, Clearing used QuestionsList");
                UsedQuestions.Clear();
                GetComponent<DatabaseManager>().FetchQuestion();
                return;
                
            }
            randomInt =UnityEngine.Random.Range(0,questionList.Count);
            
            UsedQuestions.Add(questionList[randomInt].id);
            foreach(string s in questionList[randomInt].optionsList)
            {
                if(s != "")
                {
                   
                    stringList.Add(s);
                }
            
            }
            finalStringList.Add(stringList[0]);
            finalStringList.Add(stringList[1]);
            for(int i = 0;i<3;i++)
            {
                int randomNumber = UnityEngine.Random.Range(2,stringList.Count);
                finalStringList.Add(stringList[randomNumber]);
                stringList.Remove(stringList[randomNumber]);
                
            
            }
            
            List<string> templist = new List<string>();
            foreach(string s in finalStringList)
            {
                templist.Add(s);
            }
            tenQuestions.Add(new QuestionList(templist,questionList[randomInt].id));
            questionList.Remove(questionList[randomInt]);
        }
        SetText();
    }
    public void SetText()
    {
        if(tenQuestions[0]== null)
        {
            Debug.Log("No Questions Left");
            UsedQuestions.Clear();
            GetComponent<DatabaseManager>().FetchQuestion();
            return;
            
        }
        QuestionText.text = tenQuestions[0].optionsList[0];
        AnswerAText.text =  tenQuestions[0].optionsList[1];
        AnswerBText.text =  tenQuestions[0].optionsList[2];
        AnswerCText.text =  tenQuestions[0].optionsList[3];
        AnswerDText.text =  tenQuestions[0].optionsList[4];
        tenQuestions.RemoveAt(0);
        
        
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
}
[System.Serializable]
public class QuestionList
{
    public QuestionList(){}
    public QuestionList(List<string> list, int number)
    {
        optionsList = list;
        id = number;
    }
    public List<string> optionsList = new List<string>();
    public int id;
}
