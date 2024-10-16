using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class RegisterPageScript : MonoBehaviour
{
    public InputField emailInput;
    public InputField usernameInput;
    public InputField passwordInput;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Executes register() when the register button is pressed.
    public void registerButtonPress()
    {
        StartCoroutine(register());
    }
    
    IEnumerator register()
    {
        //Uses a http Post request to create a new player on the database.
        WWWForm info = new WWWForm();
        info.AddField("username", usernameInput.text);
        info.AddField("email", emailInput.text);
        info.AddField("pWord", passwordInput.text);
        using (UnityWebRequest database = UnityWebRequest.Post("http://192.168.0.22/SQLConnect/register.php", info))
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
                SceneManager.LoadScene("Log In Page");
            }
        }
    }

}
