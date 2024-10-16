using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class OnlineWinPageScript : MonoBehaviour
{
    public Text textBox;

    // Start is called before the first frame update
    void Start()
    {
        textBox.text = OnlineGameScript.winnerName + " Wins!!!";
        StartCoroutine(addStats());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Loads the main menu when the button on the GUI is pressed.
    public void MMButton()
    {
        SceneManager.LoadScene("Main Menu");
    }

    IEnumerator addStats()
    {

        //Updates the stats and the uses a http post request to update the stats on the database.
        LogInPageScript.gamesPlayed += 1;
        if (OnlineGameScript.winnerName.Equals(LogInPageScript.username))
        {
            LogInPageScript.gamesWon += 1;
        }
        WWWForm updatedInfo = new WWWForm();
        updatedInfo.AddField("playerID", LogInPageScript.playerID.ToString());
        updatedInfo.AddField("GamesPlayed", LogInPageScript.gamesPlayed.ToString());
        updatedInfo.AddField("GamesWon", LogInPageScript.gamesWon.ToString());
        updatedInfo.AddField("Kills", LogInPageScript.kills.ToString());
        updatedInfo.AddField("Deaths", (LogInPageScript.deaths).ToString());
        using (UnityWebRequest addStats = UnityWebRequest.Post("http://192.168.0.22/SQLConnect/updateStats.php", updatedInfo))
        {
            yield return addStats.SendWebRequest();
            if (addStats.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(addStats.error);
                Debug.Log(addStats.result);
            }
            else
            {
                Debug.Log("Success updating player info");
            }
        }
    }
}