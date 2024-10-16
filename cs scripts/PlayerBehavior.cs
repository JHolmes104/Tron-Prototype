using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    float xLocation;
    float yLocation;
    int orientation;
    float moveSpeed;

    // Use this for initialization
    void Start()
    {
        orientation = 2;
        xLocation = -8;
        yLocation = 0;
        moveSpeed = 1.05f;
    }

    // Update is called once per frame
    void Update()
    {
        moveSpeed = 1.05f;
        setRotation();
        checkSpeedBoost();
        move();
        checkCollision();
    }

    void setRotation()
    {
        //Rotations here assume the sprite at 0 degrees is facing right
        if (Input.GetKey(KeyCode.W) && orientation != 3)
        {
            transform.eulerAngles = Vector3.forward * -90;
            orientation = 1;
        }

        if (Input.GetKey(KeyCode.D) && orientation != 4)
        {
            transform.eulerAngles = Vector3.forward * 0;
            orientation = 2;
        }

        if (Input.GetKey(KeyCode.S) && orientation != 1)
        {
            transform.eulerAngles = Vector3.forward * 90;
            orientation = 3;
        }

        if (Input.GetKey(KeyCode.A) && orientation != 2)
        {
            transform.eulerAngles = Vector3.forward * 180;
            orientation = 4;
        }

    }

    void move()
    {
        if (orientation == 1)
        {
            transform.position += Vector3.up * moveSpeed;
            yLocation += moveSpeed - 1;
        }
        else if (orientation == 2)
        {
            transform.position += Vector3.right * moveSpeed;
            xLocation += moveSpeed - 1;
        }
        else if (orientation == 3)
        {
            transform.position += Vector3.up * -moveSpeed;
            yLocation -= moveSpeed - 1;
        }
        else if (orientation == 4)
        {
            transform.position += Vector3.up * -moveSpeed;
            xLocation -= moveSpeed - 1;
        }

    }

    void checkSpeedBoost()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            moveSpeed += 0.05f;
        }
    }

    void createTrail()
    {
        gameObject trail =    
    }

    bool checkCollision()
    {

    }
}

