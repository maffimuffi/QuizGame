using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class CreateUser : MonoBehaviour
{
    public enum CurrentWindow { Register }
    public CurrentWindow currentWindow = CurrentWindow.Register;

    string registerUsername = "";
    string errorMessage = "";

    bool isWorking = false;
    bool isLoggedIn = false;

    //luotu käyttäjänimi
    string userName = "";

    string rootURL = "http://users.metropolia.fi/~tinoka/tietokilpailuprojekti/"; //php filejen sijainti

    //Leaderboard
    Vector2 leaderboardScroll = Vector2.zero;
    bool showLeaderboard = false;
    int currentScore = 0; 
    int previousScore = 0;
    float submitTimer; //Vivästytetään pisteiden lähettämistä peremman optimoinnin takia.
    bool submittingScore = false;
    int highestScore = 0;
    int playerRank = -1;
    [System.Serializable]
    public class LeaderboardUser
    {
        public string username;
        public int score;
    }
    LeaderboardUser[] leaderboardUsers;

    //Leaderboard
    void Update()
    {
        if (isLoggedIn)
        {
            //lähettää uudet pisteet jos ne ovat muuttuneet
            if (currentScore != previousScore && !submittingScore)
            {
                if (submitTimer > 0)
                {
                    submitTimer -= Time.deltaTime;
                }
                else
                {
                    previousScore = currentScore;
                    StartCoroutine(SubmitScore(currentScore));
                }
            }
            else
            {
                submitTimer = 3; //Odottaa 3 sekunttia ennen kuin pisteet lähetetään uudelleen.
            }

            //*** TÄSSÄ VAIN TESTAUSTA VARTEN!!! ***
            if (Input.GetKeyDown(KeyCode.Q))
            {
                currentScore += 5;
            }
        }
    }

    void OnGUI()
    {
        //katsoo onko pelaaja jo luonut käyttäjän.
        if(PlayerPrefs.GetInt("created") == 1)
        {
            isLoggedIn = true;
            userName = PlayerPrefs.GetString("name");
        }
        if (!isLoggedIn)
        {
            if (currentWindow == CurrentWindow.Register)
            {
                GUI.Window(0, new Rect(Screen.width / 2 - 125, Screen.height / 2 - 165, 250, 330), RegisterWindow, "Register");
            }
        }

        //*** TÄSSÄ VAIN TESTAUSTA VARTEN!!! ***
        if (isLoggedIn)
        {
            if (GUI.Button(new Rect(5, 30, 100, 25), "Log Out"))
            {
                isLoggedIn = false;
                userName = "";
            }
        }

        GUI.Label(new Rect(5, 5, 500, 25), (isLoggedIn ? userName : "Logged-out"));
       

        //Leaderboard
        if (showLeaderboard)
        {
            GUI.Window(1, new Rect(Screen.width / 2 - 300, Screen.height / 2 - 225, 600, 450), LeaderboardWindow, "Leaderboard");
        }
        if (!isLoggedIn)
        {
            showLeaderboard = false;
            currentScore = 0;
        }
        else
        {
            GUI.Box(new Rect(Screen.width / 2 - 65, 5, 120, 25), currentScore.ToString());

            if (GUI.Button(new Rect(5, 60, 100, 25), "Leaderboard"))
            {
                showLeaderboard = !showLeaderboard;
                if (!isWorking)
                {
                    StartCoroutine(GetLeaderboard());
                }
            }
        }
    }

    //Leaderboard
    void LeaderboardWindow(int index)
    {
        if (isWorking)
        {
            GUILayout.Label("Loading...");
        }
        else
        {
            GUILayout.BeginHorizontal();
            GUI.color = Color.green;
            GUILayout.Label("Your Rank: " + (playerRank > 0 ? playerRank.ToString() : "Not ranked yet"));
            GUILayout.Label("Highest Score: " + highestScore.ToString());
            GUI.color = Color.white;
            GUILayout.EndHorizontal();

            leaderboardScroll = GUILayout.BeginScrollView(leaderboardScroll, false, true);

            for (int i = 0; i < leaderboardUsers.Length; i++)
            {
                GUILayout.BeginHorizontal("box");
                if (leaderboardUsers[i].username == userName)
                {
                    GUI.color = Color.green;
                }
                GUILayout.Label((i + 1).ToString(), GUILayout.Width(30));
                GUILayout.Label(leaderboardUsers[i].username, GUILayout.Width(230));
                GUILayout.Label(leaderboardUsers[i].score.ToString());
                GUI.color = Color.white;
                GUILayout.EndHorizontal();
            }

            GUILayout.EndScrollView();
        }
    }

    void RegisterWindow(int index)
    {
        if (isWorking)
        {
            GUI.enabled = false;
        }

        if (errorMessage != "")
        {
            GUI.color = Color.red;
            GUILayout.Label(errorMessage);
        }

        GUI.color = Color.white;
        GUILayout.Label("Username:");
        registerUsername = GUILayout.TextField(registerUsername, 20);

        GUILayout.Space(5);

        if (GUILayout.Button("Submit", GUILayout.Width(85)))
        {
            StartCoroutine(RegisterEnumerator());
        }

        GUILayout.FlexibleSpace();
    }

    IEnumerator RegisterEnumerator()
    {
        isWorking = true;
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
                    
                }
                else
                {
                    errorMessage = responseText;
                }
            }
        }

        isWorking = false;
    }

    void ResetValues()
    {
        errorMessage = "";
        registerUsername = "";
    }

    //Leaderboard
    IEnumerator SubmitScore(int score_value)
    {
        submittingScore = true;

        print("Submitting Score...");

        WWWForm form = new WWWForm();
        form.AddField("nimimerkki", userName);
        form.AddField("score", score_value);

        using (UnityWebRequest www = UnityWebRequest.Post(rootURL + "submitScore.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                print(www.error);
            }
            else
            {
                string responseText = www.downloadHandler.text;

                if (responseText.StartsWith("Success"))
                {
                    print("New Score Submitted!");
                }
                else
                {
                    print(responseText);
                }
            }
        }

        submittingScore = false;
    }

    IEnumerator GetLeaderboard()
    {
        isWorking = true;

        WWWForm form = new WWWForm();
        form.AddField("nimimerkki", userName);

        using (UnityWebRequest www = UnityWebRequest.Post(rootURL + "leaderboard.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                print(www.error);
            }
            else
            {
                string responseText = www.downloadHandler.text;

                if (responseText.StartsWith("User"))
                {
                    string[] dataChunks = responseText.Split('|');

                    //Hakee pelaajan pisteet ja sijoituksen.
                    if (dataChunks[0].Contains(","))
                    {
                        string[] tmp = dataChunks[0].Split(',');
                        highestScore = int.Parse(tmp[1]);
                        playerRank = int.Parse(tmp[2]);
                    }
                    else
                    {
                        highestScore = 0;
                        playerRank = -1;
                    }

                    //Hakee leaderboardin.
                    leaderboardUsers = new LeaderboardUser[dataChunks.Length - 1];
                    for (int i = 1; i < dataChunks.Length; i++)
                    {
                        string[] tmp = dataChunks[i].Split(',');
                        LeaderboardUser user = new LeaderboardUser();
                        user.username = tmp[0];
                        user.score = int.Parse(tmp[1]);
                        leaderboardUsers[i - 1] = user;
                    }
                }
                else
                {
                    print(responseText);
                }
            }
        }

        isWorking = false;
    }
}