using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorThemeManager : MonoBehaviour
{
    /*
    // GameObjects to change
    [SerializeField] private GameObject backGround;
    [SerializeField] private GameObject correctButton;
    [SerializeField] private GameObject wrongButon1;
    [SerializeField] private GameObject wrongButton2;
    [SerializeField] private GameObject wrongButton3;

    [SerializeField] private GameObject questionBox;
    [SerializeField] private GameObject answerBox;
    */
    // Containers for all objects with categories
    [SerializeField] private GameObject[] baseColorObjects;
    [SerializeField] private GameObject[] secondaryColorObjects;
    [SerializeField] private GameObject[] textColorObjects;

    // musicButton.image.color = Color.red;
    void Awake()
    {
        Theme1();
    }

    void Update()
    {
        
    }

    void Theme1()
    {
        foreach(GameObject obj1 in baseColorObjects)
        {
            obj1.GetComponent<Image>().color = new Color(0, 33, 43);
        }
        foreach (GameObject obj2 in secondaryColorObjects)
        {
            obj2.GetComponent<Image>().color = new Color(0, 57, 75);
        }
        foreach (GameObject obj in baseColorObjects)
        {
            obj.GetComponent<Image>().color = new Color(255, 0, 0);
        }
    }
}
