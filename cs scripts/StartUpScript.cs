using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartUpScript : MonoBehaviour
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
    public void logInButtonPress()
    {
        SceneManager.LoadScene("Log In Page");
    }

    public void registerButtonPress()
    {
        SceneManager.LoadScene("Register Page");
    }
}
