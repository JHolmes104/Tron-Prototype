using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class JoinClanScript : MonoBehaviour
{
    public InputField clanNameInput;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //Executes joinClan if the Button is pressed.
    public void joinButtonPress()
    {
        StartCoroutine(joinClan());
    }

    IEnumerator joinClan()
    {
        //Uses a http Post request to update the player's clan.
        string clanName = clanNameInput.text;
        int playerID = LogInPageScript.playerID;
        WWWForm info = new WWWForm();
        info.AddField("clanName", clanName);
        info.AddField("playerID", playerID);
        using (UnityWebRequest database = UnityWebRequest.Post("http://192.168.0.22/SQLConnect/joinClan.php", info))
        {
            yield return database.SendWebRequest();
            if (database.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(database.error);
                Debug.Log(database.result);
            }
            else
            {
                Debug.Log("Success");
                SceneManager.LoadScene("Main Menu");
            }
        }
    }

}