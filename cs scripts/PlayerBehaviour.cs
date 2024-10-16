using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    //Key codes are used instead of keys so controls can be changed based on the player.
    public KeyCode up;
    public KeyCode right;
    public KeyCode down;
    public KeyCode left;
    public string playerName;
    
    public GameObject trailObject;
    Collider2D trailCollider;
    Vector3 trailEnd;

    public Vector3 location;
    public float xLocation;
    public float yLocation;
    int orientation;
    float moveSpeed;
    int lives;
    bool canTakeDamage;

    // Use this for initialization
    void Start()
    {
        
        updateLocation();
        //Determines if the object the script is used by is player 1 or player 2.
        if (xLocation > 0)
        {
            transform.eulerAngles = Vector3.forward * 180;
            orientation = 4;

        }
        else
        {
            orientation = 2;
        }
        moveSpeed = 5f;
        lives = 3;
        createTrail();
    }

    // Update is called once per frame
    void Update()
    {
        canTakeDamage = true;
        moveSpeed = 5f;
        setRotation();
        move();
        updateLocation();
        increaseTrailSize();
    }

    void setRotation()
    {
        //Rotates the player and creates a trail if an input is detected.
        //Uses orientation variable to prevent crashing into itself.
        //Rotations here assume the sprite at 0 degrees is facing right.
        if (Input.GetKeyDown(up) && orientation != 3)
        {
            transform.eulerAngles = Vector3.forward * -90;
            orientation = 1;
            createTrail();
        }

        if (Input.GetKeyDown(right) && orientation != 4)
        {
            transform.eulerAngles = Vector3.forward * 0;
            orientation = 2;
            createTrail();
        }

        if (Input.GetKeyDown(down) && orientation != 1)
        {
            transform.eulerAngles = Vector3.forward * 90;
            orientation = 3;
            createTrail();
        }

        if (Input.GetKeyDown(left) && orientation != 2)
        {
            transform.eulerAngles = Vector3.forward * 180;
            orientation = 4;
            createTrail();
        }

    }

    public void move()
    {
        // Changes player's velocity to match the direction.
        if (orientation == 1)
        {
            GetComponent<Rigidbody2D>().velocity = Vector3.up * moveSpeed;
        }
        else if (orientation == 2)
        {
            GetComponent<Rigidbody2D>().velocity = Vector3.right * moveSpeed;
        }
        else if (orientation == 3)
        {
            GetComponent<Rigidbody2D>().velocity = Vector3.up * -moveSpeed;
        }
        else if (orientation == 4)
        {
            GetComponent<Rigidbody2D>().velocity = Vector3.right * -moveSpeed;
        }

    }

    void updateLocation()
    {
        //Updates values for the 3D Vector storing the Location as well as the individual X and Y coordinates.
        location = transform.position;
        xLocation = transform.position.x;
        yLocation = transform.position.y;
    }


    public void createTrail()
    {
        //spawns a trail behind player.
        GameObject trail = Instantiate(trailObject, transform.position, Quaternion.identity);
        trailCollider = trail.GetComponent<Collider2D>();
        trailEnd = location;
    }

    void increaseTrailSize()
    {
        //Moves the trail to the midpoint between where the player turned and the player's current position. 
        try
        {

            trailCollider.transform.position = (trailEnd + (location - trailEnd) * 0.5f);
            float len = Vector3.Distance(trailEnd, location);
            len = len * 5f;
            len += 1;
            //Calculates the  Player's direction of travel and increase the size of the trail based on the 
            if (orientation == 2 || orientation == 4)
            {
                trailCollider.transform.localScale = new Vector3(len, 1, 1);
            }
            else
            {
                trailCollider.transform.localScale = new Vector3(1, len, 1);
            }
        }
        catch
        {

        }
    }

    void OnTriggerEnter2D(Collider2D crash)
    {
        //Prevents the collider from detecting the current trail. 
        if (crash != trailCollider && canTakeDamage == true)
        {
            lives--;
            canTakeDamage = false;
        }
    }

    //Allows the game script to access the players lives.
    public int getLives()
    {
        return lives;
    }

    //Allows the game script to change the player's orientation.
    public void setOrientation(int newOrientation)
    {
        orientation = newOrientation;
    }

    public void createTrail(float initialX)
    {
        //spawns a trail at the specified x coordinate and is used when resetting the positions of the players.
        GameObject trail = Instantiate(trailObject, new Vector3(initialX, 0f, 1f), Quaternion.identity);
        trailCollider = trail.GetComponent<Collider2D>();
        trailEnd = new Vector3(initialX, 0f, 1f);
    }

    public float getXLocation()
    {
        //Allows other objects to view the X coordinate of the player.
        return xLocation;
    }

    public float getYLocation()
    {
        //Allows other objects to view the Y coordinate of the player.
        return yLocation;
    }

    public int getOrientation()
    {
        //Allows the orientation to be altered by other objects.
        return orientation;
    }

    public void destroyCurrentTrail()
    {
        //Destroys the most recent trail.
        Destroy(trailObject);
    }

    public void setPlayerName(string name)
    {
        //Allows other objects to change the player's name.
        playerName = name;
    }
}

