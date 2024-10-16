using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class LobbyScript : MonoBehaviourPunCallbacks
{
    public InputField roomNameInput;
    public static bool host;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Creates room with the given name when the corresponding button is pressed on the GUI.
    public void createRoom()
    {
        host = true;
        PhotonNetwork.CreateRoom(roomNameInput.text);
    }

    //Joins room with the given name when the corresponding button is pressed on the GUI.
    public void joinRoom()
    {
        host = false;
        PhotonNetwork.JoinRoom(roomNameInput.text);
    }

    //Changes scene once the player has joined the room
    public override void OnJoinedRoom()
    {
        SceneManager.LoadScene("Game Load");
    }
}
