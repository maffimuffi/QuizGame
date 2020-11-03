using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestionManager : MonoBehaviour
{
    
    // Buttons
    public GameObject correctButton;
    public GameObject wrongButton1;
    public GameObject wrongButton2;
    public GameObject wrongButton3;
    public GameObject[] buttons;

    // Answer buttons positions
    private Vector3 answerPosition1;
    private Vector3 answerPosition2;
    private Vector3 answerPosition3;
    private Vector3 answerPosition4;
    public List<Vector3> positions;

    // Texts
    public List<string> stringList = new List<string>();
    public List<string> finalStringList = new List<string>();
    public List<int> usedQuestions = new List<int>();
    public List<QuestionLists> questionList = new List<QuestionLists>();

    [SerializeField] private TMP_Text questionText;
    [SerializeField] private TMP_Text correctText;
    [SerializeField] private TMP_Text wrongText1;
    [SerializeField] private TMP_Text wrongText2;
    [SerializeField] private TMP_Text wrongText3;

    // Start is called before the first frame update
    void Awake()
    {
        answerPosition1 = correctButton.transform.localPosition;
        answerPosition2 = wrongButton1.transform.localPosition;
        answerPosition3 = wrongButton2.transform.localPosition;
        answerPosition4 = wrongButton3.transform.localPosition;

        buttons[0] = correctButton;
        buttons[1] = wrongButton1;
        buttons[2] = wrongButton2;
        buttons[3] = wrongButton3;

        correctText = correctButton.GetComponentInChildren<TMP_Text>();
        wrongText1 = wrongButton1.GetComponentInChildren<TMP_Text>();
        wrongText2 = wrongButton2.GetComponentInChildren<TMP_Text>();
        wrongText3 = wrongButton3.GetComponentInChildren<TMP_Text>();
    }

    public void ChangeAnswerPositions()
    {
        positions[0] = answerPosition1;
        positions[1] = answerPosition2;
        positions[2] = answerPosition3;
        positions[3] = answerPosition4;

        for (int i = buttons.Length - 1; i >= 0; i--)
        {
            int index = Random.Range(0, positions.Count);
            buttons[i].transform.localPosition = positions[index];
            positions.RemoveAt(index);
        }

        positions.Add(answerPosition1);
        positions.Add(answerPosition2);
        positions.Add(answerPosition3);
        positions.Add(answerPosition4);
    }

    public void ParseText(string text)
    {
        stringList.Clear();
        questionList.Clear();
        string tempString = "";
        QuestionLists list = new QuestionLists();

        foreach (char c in text)
        {
            if (c == ';')
            {
                tempString = "";
            }
            else if (c == '{')
            {
                if (list != null)
                {
                    if (!usedQuestions.Contains(int.Parse(tempString)))
                    {
                        list.id = int.Parse(tempString);
                        questionList.Add(list);
                    }
                    else
                    {
                        Debug.Log("Question has been used!");
                    }
                }
                tempString = "";
            }
            else if (c == '_')
            {
                stringList.Add(tempString);
                tempString = "";
            }
            else if (c == '/')
            {
                stringList.Add(tempString);
                tempString = "";
                list = new QuestionLists();
                foreach (string s in stringList)
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
        int randomInt = 0;
        finalStringList.Clear();
        stringList.Clear();
        if (questionList.Count == 0)
        {
            Debug.Log("No unused questions found, Clearing used QuestionsList");
            usedQuestions.Clear();
            return;
        }
        randomInt = UnityEngine.Random.Range(0, questionList.Count);

        usedQuestions.Add(questionList[randomInt].id);
        foreach (string s in questionList[randomInt].optionsList)
        {
            if (s != "")
            {
                stringList.Add(s);
            }
        }
        finalStringList.Add(stringList[0]);
        finalStringList.Add(stringList[1]);
        for (int i = 0; i < 3; i++)
        {
            int randomNumber = UnityEngine.Random.Range(2, stringList.Count);
            finalStringList.Add(stringList[randomNumber]);
            stringList.Remove(stringList[randomNumber]);
        }

        SetTexts();
    }

    public void SetTexts()
    {
        questionText.text = finalStringList[0];
        correctText.text = finalStringList[1];
        wrongText1.text = finalStringList[2];
        wrongText2.text = finalStringList[3];
        wrongText3.text = finalStringList[4];
    }
}
[System.Serializable]
public class QuestionLists
{
    public List<string> optionsList = new List<string>();
    public int id;

}
