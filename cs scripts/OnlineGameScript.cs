using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OnlineGameScript : MonoBehaviour
{
    GameObject player1;
    GameObject player2;
    GameObject RBStack;
    public Text player1UI;
    public Text player2UI;

    float speed = 0.0835f;
    int player1Lives;
    int player2Lives;
    int frame;
    int delay;
    int p1RotateFrame;
    int p2RotateFrame;

    public static string winnerName;

    // Start is called before the first frame update
    void Start()
    {
        RBStack = GameObject.FindWithTag("Stack");

        player1Lives = 3;
        player2Lives = 3;
        frame = 0;
        p1RotateFrame = 0;
        p2RotateFrame = 0;

    }

    // Update is called once per frame
    void Update()
    {
        player1 = GameObject.FindWithTag("Player1");
        player2 = GameObject.FindWithTag("Player2");
        checkDamage();
        storeFrameData();
        checkFrames();
        setUI();
        frame++;
    }

    //Checks to see if the players have taken damage and either resets the game or declares a winner.
    void checkDamage()
    {
        Debug.Log("Player1lives: " + player1Lives);
        Debug.Log("Player2lives: " + player2Lives);
        int currentLives1 = player1.GetComponent<OnlinePlayerBehaviour>().getLives();
        int currentLives2 = player2.GetComponent<OnlinePlayerBehaviour>().getLives();
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
            winnerName = player1.GetComponent<OnlinePlayerBehaviour>().getPlayerName();
            SceneManager.LoadScene("Online Win Page");
        }
        else if (currentLives1 == 0)
        {
            Debug.Log("Player 2 Wins");
            winnerName = player1.GetComponent<OnlinePlayerBehaviour>().getPlayerName();
            SceneManager.LoadScene("Online Win Page");
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
        player1.GetComponent<OnlinePlayerBehaviour>().setOrientation(2);
        player1.GetComponent<OnlinePlayerBehaviour>().createTrail(-5.5f);
        player1.GetComponent<OnlinePlayerBehaviour>().move();

        player2.transform.position = new Vector3(5.5f, 0f, 1f);
        player2.transform.eulerAngles = Vector3.forward * 180;
        player2.GetComponent<OnlinePlayerBehaviour>().setOrientation(4);
        player2.GetComponent<OnlinePlayerBehaviour>().createTrail(5.5f);
        player2.GetComponent<OnlinePlayerBehaviour>().move();
    }

    void storeFrameData()
    {
        //Stores the data of each frame into the Rollback Stack.
        float p1x = player1.GetComponent<OnlinePlayerBehaviour>().getXLocation();
        float p1y = player1.GetComponent<OnlinePlayerBehaviour>().getYLocation();
        int p1Orientation = player1.GetComponent<OnlinePlayerBehaviour>().getOrientation();
        float p2x = player2.GetComponent<OnlinePlayerBehaviour>().getXLocation();
        float p2y = player2.GetComponent<OnlinePlayerBehaviour>().getYLocation();
        int p2Orientation = player2.GetComponent<OnlinePlayerBehaviour>().getOrientation();

        string frameStr = frame.ToString();
        string p1xStr = p1x.ToString();
        string p1yStr = p1y.ToString();
        string p1OrientationStr = p1Orientation.ToString();
        string p2xStr = p2x.ToString();
        string p2yStr = p2y.ToString();
        string p2OrientationStr = p2Orientation.ToString();

        RBStack.GetComponent<RollBackStack>().push(frameStr, p1xStr, p1yStr, p1OrientationStr, p2xStr, p2yStr, p2OrientationStr);
    }

    //Checks to see if a rollback should occur.
    public void checkFrames()
    {
        int inputFrame1 = player1.GetComponent<OnlinePlayerBehaviour>().getLIF();
        if (inputFrame1 != frame || inputFrame1 != p1RotateFrame)
        {
            int frameDiff = frame - inputFrame1;
            bool inStack = RBStack.GetComponent<RollBackStack>().isInStack(inputFrame1);
            if (frameDiff > 3 && inStack == true)
            {
                string[] frameData = new string[7];
                frameData = RBStack.GetComponent<RollBackStack>().peek();
                while (!frameData[0].Equals(inputFrame1.ToString()))
                {
                    frameData = RBStack.GetComponent<RollBackStack>().pop();
                }
                int delayedFrame = int.Parse(frameData[0]);
                float p1x = float.Parse(frameData[1]);
                float p1y = float.Parse(frameData[2]);
                int p1orientation = int.Parse(frameData[3]);
                for (int i = 0; i < frameDiff; i++)
                {
                    if (p1orientation == 1)
                    {
                        p1y += speed;
                    }
                    if (p1orientation == 2)
                    {
                        p1x += speed;
                    }
                    if (p1orientation == 3)
                    {
                        p1y -= speed;
                    }
                    if (p1orientation == 4)
                    {
                        p1x -= speed;
                    }
                }
                player1.transform.position = new Vector3(p1x, p1y, 1f);
                p1RotateFrame = inputFrame1;
            }
        }
        int inputFrame2 = player2.GetComponent<OnlinePlayerBehaviour>().getLIF();
        if (inputFrame2 != frame || inputFrame2 != p2RotateFrame)
        {
            int frameDiff = frame - inputFrame2;
            bool inStack = RBStack.GetComponent<RollBackStack>().isInStack(inputFrame2);
            if (frameDiff > 3 && inStack == true)
            {
                string[] frameData = new string[7];
                frameData = RBStack.GetComponent<RollBackStack>().peek();
                while (!frameData[0].Equals(inputFrame2.ToString()))
                {
                    frameData = RBStack.GetComponent<RollBackStack>().pop();
                }
                int delayedFrame = int.Parse(frameData[0]);
                float p2x = float.Parse(frameData[4]);
                float p2y = float.Parse(frameData[5]);
                int p2orientation = int.Parse(frameData[6]);
                for (int i = 0; i < frameDiff; i++)
                {
                    if (p2orientation == 1)
                    {
                        p2y += speed;
                    }
                    if (p2orientation == 2)
                    {
                        p2x += speed;
                    }
                    if (p2orientation == 3)
                    {
                        p2y -= speed;
                    }
                    if (p2orientation == 4)
                    {
                        p2x -= speed;
                    }
                }
                player2.transform.position = new Vector3(p2x, p2y, 1f);
                p2RotateFrame = inputFrame2;
            }
        }
    }


    //Allows other objects to check the current frame.
    public int getFrame()
    {
        return frame;
    }

    //Updates the UI with the players' usernames and number of lives
    public void setUI()
    {
        string p1Name = player1.GetComponent<OnlinePlayerBehaviour>().getPlayerName();
        string p1LivesStr = player1Lives.ToString();
        player1UI.text = p1Name + "\n" + p1LivesStr;

        string p2Name = player2.GetComponent<OnlinePlayerBehaviour>().getPlayerName();
        string p2LivesStr = player2Lives.ToString();
        player2UI.text = p2Name + "\n" + p2LivesStr;
    }
}
