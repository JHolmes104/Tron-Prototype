using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //The following subroutines are executed when the corresponding buttons on the GUI are clicked.
    public void localButtonPress()
    {
        SceneManager.LoadScene("Local Play");
    }

    public void clanButtonPress()
    {
        SceneManager.LoadScene("Clan Manager");
    }

    public void onlineButtonPress()
    {
        SceneManager.LoadScene("Lobby Load Scene");
    }

    public void exitButtonPress()
    {
        Application.Quit();
    }
}