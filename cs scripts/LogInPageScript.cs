using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class LogInPageScript : MonoBehaviour
{
    public InputField emailInput;
    public InputField passwordInput;

    public static string username;
    public static int playerID;
    public static int gamesPlayed;
    public static int gamesWon;
    public static int kills;
    public static int deaths;
    public static int clanID;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //Executes logIn() Once the button on the GUI is pressed.
    public void logInButtonPress()
    {
        StartCoroutine(logIn());
    }

    IEnumerator logIn()
    {
        //Uses a http post request to store the email and password on a text file inside the apache server.
        string email = emailInput.text;
        string password = passwordInput.text;
        WWWForm info = new WWWForm();
        info.AddField("email", email);
        info.AddField("pWord", password);
        using (UnityWebRequest writeToFile = UnityWebRequest.Post("http://192.168.0.22/SQLConnect/writeToFile.php", info))
        {
            yield return writeToFile.SendWebRequest();
            if (writeToFile.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(writeToFile.error);
                Debug.Log(writeToFile.result);
            }
            else
            {
                Debug.Log("Success");

                //Uses a http get request to retrieve player info.
                using (UnityWebRequest database = UnityWebRequest.Get("http://192.168.0.22/SQLConnect/logIn.php"))
                {
                    yield return database.SendWebRequest();
                    if (database.downloadHandler.text == "Error retrieving username" || database.downloadHandler.text == "Connection failed")
                    {
                        Debug.Log(database.downloadHandler.text);
                    }
                    else
                    {
                        string echoedText = database.downloadHandler.text;
                        Debug.Log(echoedText);
                        string[] playerInfo = echoedText.Split('\t');
                        username = playerInfo[0];
                        Debug.Log(playerInfo[1]);
                        playerID = int.Parse(playerInfo[1]);
                        gamesPlayed = int.Parse(playerInfo[2]);
                        gamesWon = int.Parse(playerInfo[3]);
                        kills = int.Parse(playerInfo[4]);
                        deaths = int.Parse(playerInfo[5]);
                        clanID = int.Parse(playerInfo[6]);
                        Debug.Log(username);
                        Debug.Log(playerID);
                        SceneManager.LoadScene("Main Menu");

                    }
                }
            }
        }
    }
}
