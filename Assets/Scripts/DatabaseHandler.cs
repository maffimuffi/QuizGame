using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DatabaseHandler : MonoBehaviour
{

    [SerializeField] private QuestionManager qManager;

    private void Awake()
    {
        qManager = gameObject.GetComponent<QuestionManager>();
        StartCoroutine(GetQuestionFromDB());
    }

    IEnumerator GetQuestionFromDB()
    {
        using(UnityWebRequest www = UnityWebRequest.Get("http://users.metropolia.fi/~niklaslm/Tietokilpa/GetQuestion.php"))
        {
            yield return www.SendWebRequest();
            if(www.isNetworkError || www.isHttpError)
            {
                Debug.Log("Network or HTTP error occured!");
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                qManager.ParseText(www.downloadHandler.text);
                byte[] results = www.downloadHandler.data;
            }
        }
    }
}
