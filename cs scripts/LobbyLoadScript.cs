using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class LobbyLoadScript : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Joins lobby when connected to the server.
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    //Starts lobby scene once connected to the server.
    public override void OnJoinedLobby()
    {
        SceneManager.LoadScene("Lobby");
    }
}
