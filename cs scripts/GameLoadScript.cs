using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class GameLoadScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Starts an Online Game when there are enough players in the lobby.
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            SceneManager.LoadScene("Online Play");
        }
    }
}
