using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MapGeneration 
{
    public class RoomGenerationController : MonoBehaviour
    {
        private void Start()
        {
            Debug.Log("asda");
        }
    }

    public class RoomGeneratorClass                                     //Class which is responsible for collecting data for creating room 
    {
        private int roomHightY;                                         //Variable that stores width of the room of Y dimension
        private int roomLengthX;                                        //Variable that stores width of the room of X dimension
        private int[,] roomObjectId;                                    // floor - 0 wals - 1; left corner - 2;  right corner - 3; left wall - 4; right wall 5 and so on;
        private Layer layer1;

        public RoomGeneratorClass(int roomHightY, int roomLengthX)      //Constructor
        {
            this.roomHightY = roomHightY;
            this.roomLengthX = roomLengthX;
            this.roomObjectId = new int[roomHightY, roomLengthX];
            this.layer1 = new Layer(roomHightY, roomLengthX);
            setRoomWalls();
            setleftAndRightRoomWalls();
            setRoomCorners();            
        }

        public int GetRoomHeightY()
        {
            return roomHightY;
        }

        public int GetRoomLengthX() 
        {
            return roomLengthX;
        }

        public int[,] GetObjectId()                                     //Method which returns array that stores an ID of an object that is located on universal coordinate 
        {
            return roomObjectId;
        }

        public void Test()
        {
            foreach (int num in roomObjectId)
            {
                Debug.Log(num);
            }
        }

        private void setRoomWalls()                                   //Sets coordinates for each block
        {
            for (int y = 0; y < roomHightY; y++) 
            {
                for (int x = 0; x < roomLengthX; x++) 
                {
                    if (y == 0 || y == roomHightY - 1)
                        roomObjectId[y, x] = 1;
                    if (x == 0 || x == roomLengthX - 1)
                        roomObjectId[y, x] = 1;
                }
            }
        }
        // 4 - left; 5 - right;
        private void setleftAndRightRoomWalls() //shit is here
        {
            for (int y = 0; y < roomHightY; y++) 
            {
                for (int x = 0; x < roomLengthX; x++) 
                {
                    if (y != 0 || y != roomHightY - 1) 
                    {
                        if (x == 0)
                            roomObjectId[y, x] = 4;
                        else if (x == roomLengthX - 1)
                            roomObjectId[y, x] = 5;
                    }
                }
            }
        }

        private void setRoomCorners() 
        {                                                              
            for (int y = 0; y < roomHightY; y++)
            {
                for (int x = 0; x < roomLengthX; x++)
                {
                    if ((y == 0 || y == roomHightY - 1) && x == 0)
                    {
                        roomObjectId[y, x] = 2; // sets left corners                   
                    }

                    if ((y == 0 || y == roomHightY - 1) && x == roomLengthX - 1) 
                    {
                        roomObjectId[y, x] = 3; // sets right corner
                    }                    
                }
            } 
        }
    }

    public class Layer
    {
        public char[,] objectMap;

        public Layer(int heightY, int lengthX) 
        {
            this.objectMap = new char[heightY, lengthX];
        }
    }
}

/*
            TO DO LIST
1. add corners - done
2. left, right, top and down walls;
3. add doors
4. add inner objects
5. add random (not sure)
*/