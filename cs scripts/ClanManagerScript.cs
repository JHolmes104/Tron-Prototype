using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClanManagerScript : MonoBehaviour
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
    public void joinButtonPress()
    {
        SceneManager.LoadScene("Join Clan");
    }

    public void createButtonPress()
    {
        SceneManager.LoadScene("Create Clan");
    }

    public void menuButtonPress()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void viewButtonPress()
    {
        SceneManager.LoadScene("Stats Page");
    }
}
