using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DatabaseManager : MonoBehaviour
{
    public Question currentQuestion;
    [TextArea(1,5)]
    public string SQLsearch = "Select kysymys,vaihtoehto1,vaihtoehto2,vaihtoehto3,vaihtoehto4 FROM kysymykset Where aihepiiri = 'PO'";

    // Start is called before the first frame update
    void Start()
    {
       
        //StartCoroutine(GetQuestion());
        
    }
    public void FetchQuestion()
    {
        StartCoroutine(GetQuestion());
    }
    IEnumerator GetQuestion()
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
                currentQuestion.Parse(www.downloadHandler.text);
                //byte[] results = www.downloadHandler.data;
            }

        }
            
        
     
    }
    
}
