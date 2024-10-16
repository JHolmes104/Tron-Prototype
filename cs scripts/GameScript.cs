using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameScript : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    GameObject RBStack;
    public Text player1UI;
    public Text player2UI;

    int player1Lives;
    int player2Lives;
    int frame;

    // Start is called before the first frame update
    void Start()
    {
        RBStack = GameObject.FindWithTag("Stack");

        player1Lives = 3;
        player2Lives = 3;
        frame = 0;
    }

    // Update is called once per frame
    void Update()
    {
        checkDamage();
        storeFrameData();
        setUI();
        frame++;
    }

    //Checks to see if the players have taken damage and either resets the game or declares a winner.
    void checkDamage()
    {
        Debug.Log("Player1lives: " + player1Lives);
        Debug.Log("Player2lives: " + player2Lives);
        int currentLives1 = player1.GetComponent<PlayerBehaviour>().getLives();
        int currentLives2 = player2.GetComponent<PlayerBehaviour>().getLives();
        //Resets game if players take damage.
        if (currentLives1 < player1Lives || currentLives2 < player2Lives)
        {
            player1Lives = currentLives1;
            player2Lives = currentLives2;
            reset();
        }
        //Checks to see if the game is over.
        else if (currentLives2 == 0)
        {
            Debug.Log("Player 1 Wins");
            SceneManager.LoadScene("P1 Win Page Offline");
        }
        else if (currentLives1 == 0)
        {
            Debug.Log("Player 2 Wins");
            SceneManager.LoadScene("P2 Win Page Offline");
        }
        else if (currentLives2 == 0 && currentLives1 == 0)
        {
            Debug.Log("Draw");
            SceneManager.LoadScene("Draw Page Offline");
        }
    }

    void reset()
    {
        //Stores all of the trails currently in the game into a trail and destroys them.
        GameObject[] trail = GameObject.FindGameObjectsWithTag("Trail");
        for (int i = 0; i < trail.Length; i++)
        {
            Destroy(trail[i]);
        }

        //Resets the positions and rotations of each player.
        player1.transform.position = new Vector3(-5.5f, 0f, 1f);
        player1.transform.eulerAngles = Vector3.forward * 0;
        player1.GetComponent<PlayerBehaviour>().setOrientation(2);
        player1.GetComponent<PlayerBehaviour>().createTrail(-5.5f);
        player1.GetComponent<PlayerBehaviour>().move();

        player2.transform.position = new Vector3(5.5f, 0f, 1f);
        player2.transform.eulerAngles = Vector3.forward * 180;
        player2.GetComponent<PlayerBehaviour>().setOrientation(4);
        player2.GetComponent<PlayerBehaviour>().createTrail(5.5f); 
        player2.GetComponent<PlayerBehaviour>().move();
    }

    void storeFrameData()
    {
        //Stores the data of each frame into the Rollback Stack.
        Debug.Log(frame);
        float p1x = player1.GetComponent<PlayerBehaviour>().getXLocation();
        float p1y = player1.GetComponent<PlayerBehaviour>().getYLocation();
        int p1Orientation = player1.GetComponent<PlayerBehaviour>().getOrientation();
        float p2x = player2.GetComponent<PlayerBehaviour>().getXLocation();
        float p2y = player2.GetComponent<PlayerBehaviour>().getYLocation();
        int p2Orientation = player2.GetComponent<PlayerBehaviour>().getOrientation();

        string frameStr = frame.ToString();
        string p1xStr = p1x.ToString();
        string p1yStr = p1y.ToString();
        string p1OrientationStr = p1Orientation.ToString();
        string p2xStr = p2x.ToString();
        string p2yStr = p2y.ToString();
        string p2OrientationStr = p2Orientation.ToString();

        if (frame == 1)
        {
            Debug.Log("Frame 1:   " + p1xStr);
        }

        RBStack.GetComponent<RollBackStack>().push(frameStr, p1xStr, p1yStr, p1OrientationStr, p2xStr, p2yStr, p2OrientationStr);
    }

    //Updates the UI with the players' number of lives
    public void setUI()
    {
        player1UI.text = player1Lives.ToString();

        player2UI.text = player2Lives.ToString();
    }
}
