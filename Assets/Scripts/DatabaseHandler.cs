using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DatabaseHandler : MonoBehaviour
{

    [TextArea(1,5)]
    public string SQLsearch = "Select kysymys,vaihtoehto1,vaihtoehto2,vaihtoehto3,vaihtoehto4 FROM kysymykset Where aihepiiri = 'PO'";
    [SerializeField] private QuestionManager qManager;

    private void Awake()
    {
        qManager = gameObject.GetComponent<QuestionManager>();
    }

    public void GetQuestion()
    {
        StartCoroutine(GetQuestionFromDB());
    }

    IEnumerator GetQuestionFromDB()
    {
        WWWForm form = new WWWForm();
        form.AddField("var1", SQLsearch);
        
       
        using (UnityWebRequest www = UnityWebRequest.Post("http://users.metropolia.fi/~niklaslm/Tietokilpa/GetQuestion.php", form))
        {
           
            yield return www.SendWebRequest();
            

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                
                Debug.Log(www.downloadHandler.text);
                qManager.ParseText(www.downloadHandler.text);
                //byte[] results = www.downloadHandler.data;
            }

        }
        /*
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
        }*/
    }
}
