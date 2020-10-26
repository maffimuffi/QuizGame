using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DatabaseManager : MonoBehaviour
{
    public Question currentQuestion;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetQuestion());
        
    }
    IEnumerator GetQuestion()
    {
        using(UnityWebRequest www = UnityWebRequest.Get("http://users.metropolia.fi/~niklaslm/Tietokilpa/GetQuestion.php"))
        {
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                currentQuestion.Parse(www.downloadHandler.text);

                byte[] results = www.downloadHandler.data;
            }


        }
     
    }
    
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
