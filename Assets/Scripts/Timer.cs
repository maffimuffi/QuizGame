using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float width;
    public float maxWidth;
    public float timer = 60f;
    public RectTransform timerFill;
    public TMPro.TMP_Text timerText;
    // Start is called before the first frame update
    void Start()
    {
        
        maxWidth = GetComponent<RectTransform>().rect.width;
        width = maxWidth;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(timer <=0)
        {
            Debug.Log("Aika Loppui");
            ResetTimer();
        }
        else
        {
            SetWidth();
        }
    }
    public void SetWidth()
    {
        timer-= Time.deltaTime;
        timerFill.sizeDelta = new Vector2(timer*(maxWidth/60),timerFill.rect.height);
        timerText.text = timer.ToString("0.00");
    }
    public void ResetTimer()
    {
        timerFill.sizeDelta = new Vector2(maxWidth,timerFill.rect.height);
        timer = 60f;
    }
}
