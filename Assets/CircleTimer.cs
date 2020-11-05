using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleTimer : MonoBehaviour
{
    public Gradient gradient;
    public Image timerImage;
    // Start is called before the first frame update
    void Start()
    {
        timerImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
        timerImage.fillAmount = GameManager.instance.timeLeft /60;
        timerImage.color = gradient.Evaluate(1-timerImage.fillAmount);
    }
}
