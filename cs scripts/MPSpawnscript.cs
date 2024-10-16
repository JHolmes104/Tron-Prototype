using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MPSpawnscript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Creates the players inside the game with the player number being determined by the player being the host. 
        bool isHost = LobbyScript.host;
        if (isHost == true)
        {
            PhotonNetwork.Instantiate("Player1Online", new Vector3(-5.5f, 0f, 1f), Quaternion.identity);
            GameObject player = GameObject.FindWithTag("Player1");
            player.GetComponent<OnlinePlayerBehaviour>().setPlayerName(LogInPageScript.username);
        }
        else
        {
            PhotonNetwork.Instantiate("Player2Online", new Vector3(5.5f, 0f, 0f), Quaternion.identity);
            GameObject player = GameObject.FindWithTag("Player2");
            player.GetComponent<OnlinePlayerBehaviour>().setPlayerName(LogInPageScript.username);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
