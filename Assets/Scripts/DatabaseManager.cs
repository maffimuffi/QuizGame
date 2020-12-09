using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DatabaseManager : MonoBehaviour
{
    public Question currentQuestion;
    [TextArea(1,5)]
    public string SQLsearch = "Select id,kysymys,vaihtoehto1,vaihtoehto2,vaihtoehto3,vaihtoehto4,vaihtoehto5,vaihtoehto6,vaihtoehto7,vaihtoehto8,vaihtoehto9,vaihtoehto10 FROM kysymykset Where vaikeusaste = ";
    public string difficulty ="1";
    // Start is called before the first frame update
    void Start()
    {
        currentQuestion = GetComponent<Question>();
        //StartCoroutine(GetQuestion());
        
    }
    // FetchQuestion() aloittaa prosessin GetQuestion(), joka hakee tietokannasta php-tiedostoa käyttäen. Post() sisään tulee osoite mistä tämä tiedosto löytyy.
    public void FetchQuestion()
    {
        StartCoroutine(GetQuestion());
    }
    IEnumerator GetQuestion()
    {  
        string tempString = "";
        tempString = SQLsearch+difficulty; 
        Debug.Log(tempString);
       WWWForm form = new WWWForm();
       form.AddField("var1", tempString);
        
       
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
