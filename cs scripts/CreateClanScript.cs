using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class CreateClanScript : MonoBehaviour
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

    public void createButtonPress()
    {
        //Executes Create Clan when the create button is pressed.
        StartCoroutine(createClan());
    }

    IEnumerator createClan()
    {
        //Creates a clan using the name input by the user by sending a html post request to the apache server.
        string clanName = clanNameInput.text;
        int playerID = LogInPageScript.playerID;
        WWWForm info = new WWWForm();
        info.AddField("clanName", clanName);
        info.AddField("playerID", playerID);
        using (UnityWebRequest database = UnityWebRequest.Post("http://192.168.0.22/SQLConnect/CreateClan.php", info))
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
