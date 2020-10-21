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
    }

    private void Update()
    {
        //Debug.Log("Position 1: " + answerPosition1);
        //Debug.Log("Position 2: " + answerPosition2);
        //Debug.Log("Position 3: " + answerPosition3);
        //Debug.Log("Position 4: " + answerPosition4);
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
}
