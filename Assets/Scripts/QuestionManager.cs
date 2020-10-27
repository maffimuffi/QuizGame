using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    [SerializeField] private TMPro.TMP_Text questionText;
    [SerializeField] private TMPro.TMP_Text correctText;
    [SerializeField] private TMPro.TMP_Text wrongText1;
    [SerializeField] private TMPro.TMP_Text wrongText2;
    [SerializeField] private TMPro.TMP_Text wrongText3;

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

        correctText = correctButton.GetComponentInChildren<TMPro.TMP_Text>();
        wrongText1 = wrongButton1.GetComponentInChildren<TMPro.TMP_Text>();
        wrongText2 = wrongButton2.GetComponentInChildren<TMPro.TMP_Text>();
        wrongText3 = wrongButton3.GetComponentInChildren<TMPro.TMP_Text>();
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
        string tempString = "";

        foreach(char c in text)
        {
            if(c == '_')
            {
                stringList.Add(tempString);
                tempString = "";
            }
            else if(c == '/')
            {
                stringList.Add(tempString);
                tempString = "";
            }
            else
            {
                tempString += c;
            }
        }
        SetTexts();
    }

    public void SetTexts()
    {
        questionText.text = stringList[0];
        correctText.text = stringList[1];
        wrongText1.text = stringList[2];
        wrongText2.text = stringList[3];
        wrongText3.text = stringList[4];
    }
}
