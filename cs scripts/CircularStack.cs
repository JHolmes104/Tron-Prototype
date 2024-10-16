using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollBackStack
{
	//The RollBackStack is a circular stack that will allow each user to store the data of a given number of frames in order to preserve the previous locations, so the rollback can occur
	int maxSize;
	string[,] circle2DStack;
	int front;
	int rear;
	int currentSize;
	int frame;
	int startP1X;
	int startP1Y;
	int startP1Direction;
	int startP2X;
	int startP2Y;
	int startP2Direction;


	//initializes the circular stack with a given size.
	public RollBackStack(int maxSize)
	{
		this.maxSize = maxSize;
		circle2DStack = new string[maxSize, 7];
		rear = 0;
		front = 0;
		currentSize = 0;
		frame = 0;
		startP1X = -8;
		startP1Y = 0;
		startP1Direction = 2;
		startP2X = 8;
		startP2Y = 0;
		startP2Direction = 4;
		circle2DStack[0,0] = frame.ToString();
		circle2DStack[0,1] = startP1X.ToString();
		circle2DStack[0,2] = startP1Y.ToString();
		circle2DStack[0,3] = startP1Direction.ToString();
		circle2DStack[0,4] = startP2X.ToString();
		circle2DStack[0,5] = startP2Y.ToString();
		circle2DStack[0,6] = startP2Direction.ToString();
	}

	//Checks to see if the stack is full.
	public bool isFull()
    {
		if (currentSize == maxSize)
        {
			return true;
        }
        else
        {
			return false;
        }
    }
	
	//Checks to see if the stack is empty.
	public bool isEmpty()
    {
		if (currentSize == 0)
        {
			return true;
        }
		else
        {
			return false;
        }
    }

	//Adds the coordinates to the stack.
	public void push(string frame,  string p1X, string p1Y, string p1Direction, string p2X, string p2Y, string p2Direction)
    {
		if (isFull() == true)
		{
			circle2DStack[rear,0] = frame;
			circle2DStack[rear,1] = p1X;
			circle2DStack[rear,2] = p1Y;
			circle2DStack[rear,3] = p1Direction;
			circle2DStack[rear,4] = p2X;
			circle2DStack[rear,5] = p2Y;
			circle2DStack[rear,6] = p2Direction;
			rear += 1;
			front += 1;
		}
        else
        {
			circle2DStack[rear,0] = frame;
			circle2DStack[rear,1] = p1X;
			circle2DStack[rear,2] = p1Y;
			circle2DStack[rear,3] = p1Direction;
			circle2DStack[rear,4] = p2X;
			circle2DStack[rear,5] = p2Y;
			circle2DStack[rear,6] = p2Direction;
			rear += 1;
			currentSize += 1;
		}
	}

	//Views the most recent coordinates.
	public string[] peek()
    {
		if (isEmpty() == false)
		{
			string[] tempArray = new string[7];
			int tempRear = rear - 1;
			tempArray[0] = circle2DStack[tempRear, 0];
			tempArray[1] = circle2DStack[tempRear, 1];
			tempArray[2] = circle2DStack[tempRear, 2];
			tempArray[3] = circle2DStack[tempRear, 3];
			tempArray[4] = circle2DStack[tempRear, 4];
			tempArray[5] = circle2DStack[tempRear, 5];
			tempArray[6] = circle2DStack[tempRear, 6];
			return tempArray;
		}
		else
        {
			string[] tempArray = new string[7];
			tempArray[0] = "0";
			tempArray[1] = startP1X.ToString();
			tempArray[2] = startP1Y.ToString();
			tempArray[3] = startP1Direction.ToString();
			tempArray[4] = startP2X.ToString();
			tempArray[5] = startP2Y.ToString();
			tempArray[6] = startP2Direction.ToString();
			return tempArray;
		}
    }

	//Removes the most recent coordinates from the stack.
	public string[] pop()
    {
		if (isEmpty() == false)
		{
			rear -= 1;
			string[] tempArray = new string[7];
			tempArray[0] = circle2DStack[rear, 0];
			tempArray[1] = circle2DStack[rear, 1];
			tempArray[2] = circle2DStack[rear, 2];
			tempArray[3] = circle2DStack[rear, 3];
			tempArray[4] = circle2DStack[rear, 4];
			tempArray[5] = circle2DStack[rear, 5];
			tempArray[6] = circle2DStack[rear, 6];
			currentSize -= 1;
			return tempArray;
		}
		else
        {
			string[] tempArray = new string[7];
			tempArray[0] = "0";
			tempArray[1] = startP1X.ToString();
			tempArray[2] = startP1Y.ToString(); 
			tempArray[3] = startP1Direction.ToString();
			tempArray[4] = startP2X.ToString();
			tempArray[5] = startP2Y.ToString();
			tempArray[6] = startP2Direction.ToString();
			return tempArray;
		}
    }
}
