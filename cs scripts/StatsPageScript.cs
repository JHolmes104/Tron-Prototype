using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class StatsPageScript : MonoBehaviour
{
    public Text playerText;
    public Text clanText;
    string clanName;
    int clanWins;
    int clanGames;
    int clanKills;
    int clanDeaths;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(getInfo());
        setText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator getInfo()
    {
        //Stores Clan ID to a text file on the apache server using a http post request.
        string clanID = LogInPageScript.clanID.ToString();
        WWWForm info = new WWWForm();
        info.AddField("clanID", clanID);
        using (UnityWebRequest writeToFile = UnityWebRequest.Post("http://192.168.0.22/SQLConnect/storeClanID.php", info))
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

                //Uses a http get request to retrieve the clan stats.
                using (UnityWebRequest database = UnityWebRequest.Get("http://192.168.0.22/SQLConnect/getStats.php"))
                {
                    yield return database.SendWebRequest();
                    if (database.downloadHandler.text == "Error retrieving clanID" || database.downloadHandler.text == "Connection failed" || database.downloadHandler.text == "Error retrieving clan stats")
                    {
                        Debug.Log(database.downloadHandler.text);
                    }
                    else
                    {
                        string echoedText = database.downloadHandler.text;
                        Debug.Log(echoedText);
                        string[] clanInfo = echoedText.Split('\t');
                        clanName = clanInfo[0];
                        clanGames = int.Parse(clanInfo[1]);
                        clanWins = int.Parse(clanInfo[2]);
                        clanKills = int.Parse(clanInfo[3]);
                        clanDeaths = int.Parse(clanInfo[4]);
                    }
                }
            }
        }
    }

    void setText()
    {
        //Changes the text boxes on the GUI to match the stats from the database.
        double winLoss = 100;
        double killDeathRatio = LogInPageScript.kills;
        if (LogInPageScript.gamesPlayed > 0 && LogInPageScript.deaths > 0)
        {
            winLoss = LogInPageScript.gamesWon / LogInPageScript.gamesPlayed;
            winLoss = winLoss * 100;
            killDeathRatio = LogInPageScript.kills / LogInPageScript.deaths;
        }
        playerText.text = "Player Name: " + LogInPageScript.username + "\nPlayer Win/Loss %: " + winLoss.ToString() + "%\nPlayer Kill/Death Ratio: " + killDeathRatio.ToString();

        double clanWinLoss = 100;
        double clanKillDeathRatio = clanKills;
        if (clanGames > 0 && clanDeaths > 0)
        {
            clanWinLoss = clanWins / clanGames;
            clanWinLoss = clanWinLoss * 100;
            clanKillDeathRatio = clanKills / clanDeaths;
        }
        clanText.text = "Clan Name: " + clanName +"\nClan Win/Loss %: " + clanWinLoss.ToString() + "%\nClan Kill/Death Ratio:" + clanKillDeathRatio.ToString(); 
    }

    //Changes the scene to Main Menu if the button on the GUI is clicked.
    public void MMButtonPress()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
