using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;

public class CreateUser : MonoBehaviour
{
    public GameObject registerScreen;
    public TMP_InputField inputField;
    public TMP_Text displayUsername;

    string registerUsername = "";
    string errorMessage = "";

    bool isLoggedIn = false;

    //luotu käyttäjänimi
    string userName = "";

    string rootURL = "http://users.metropolia.fi/~tinoka/tietokilpailuprojekti/"; //php tiedostojen sijainti.



    void OnGUI()
    {
        //katsoo onko pelaaja jo luonut käyttäjän.
        if (PlayerPrefs.GetInt("created") == 1)
        {
            isLoggedIn = true;
            userName = PlayerPrefs.GetString("name");
            displayUsername.text = userName;
            registerScreen.SetActive(false);
        }
        if (!isLoggedIn)
        {
            registerScreen.SetActive(true);
            
        }
    }

    public void SubmitButton()
    {
        registerUsername = inputField.text;
        StartCoroutine(RegisterEnumerator());
    }

    IEnumerator RegisterEnumerator()
    {
        errorMessage = "";

        WWWForm form = new WWWForm();
        form.AddField("nimimerkki", registerUsername);

        using (UnityWebRequest www = UnityWebRequest.Post(rootURL + "registerUser.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                errorMessage = www.error;
            }
            else
            {
                string responseText = www.downloadHandler.text;

                if (responseText.StartsWith("Success"))
                {
                    ResetValues();
                    string[] dataChunks = responseText.Split('|');
                    userName = dataChunks[1];
                    PlayerPrefs.SetString("name", userName); //Tallentaa pelaajan luoman nimen.
                    
                    PlayerPrefs.SetInt("created", 1);        //Tallentaa numeron, mitä käytetään tunnistamaan pelaaja.
                    isLoggedIn = true;
                    registerScreen.SetActive(false);

                }
                else
                {
                    errorMessage = responseText;
                }
            }
        }
    }

    void ResetValues()
    {
        errorMessage = "";
        registerUsername = "";
    }
}