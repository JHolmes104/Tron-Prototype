using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinPageScript : MonoBehaviour
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
    public void returnToMM()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void rematch()
    {
        SceneManager.LoadScene("Local Play");
    }
}
