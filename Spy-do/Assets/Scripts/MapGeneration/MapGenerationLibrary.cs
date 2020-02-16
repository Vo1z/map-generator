using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MapGeneration 
{
    public class Room                                     //Class which is responsible for collecting data for creating room 
    {
        private int roomHightY;                                         //Variable that stores width of the room of Y dimension
        private int roomLengthX;                                        //Variable that stores width of the room of X dimension
        private string[,] roomObjectId;                                    // floor - 0 wals - 1; left corner - 2;  right corner - 3; left wall - 4; right wall 5 and so on;
        private Layer layer1;

        public Room(int roomHightY, int roomLengthX)      //Constructor
        {
            this.roomHightY = roomHightY;
            this.roomLengthX = roomLengthX;
            this.roomObjectId = new string[roomHightY, roomLengthX];
            this.layer1 = new Layer(roomHightY, roomLengthX);
            setRoomFloor();
            setRoomWalls();            
        }

        public int GetRoomHeightY()
        {
            return roomHightY;
        }

        public int GetRoomLengthX() 
        {
            return roomLengthX;
        }

        public string[,] GetObjectId()                                     //Method which returns array that stores an ID of an object that is located on universal coordinate 
        {
            return roomObjectId;
        }

        public void Test()
        {
            foreach (string objectid in roomObjectId)
            {
                Debug.Log(objectid);
            }
        }

        private void setRoomFloor()
        {
            for (int y = 0; y < roomHightY; y++) 
            {
                for (int x = 0; x < roomLengthX; x++) 
                {
                    roomObjectId[y, x] = "floor";
                }
            }
        }

        //walls ===========================================================================================================
        private void setRoomWalls()                                   //Sets coordinates for each block
        {
            for (int y = 0; y < roomHightY; y++) 
            {
                for (int x = 0; x < roomLengthX; x++) 
                {
                    if (y == 0 || y == roomHightY - 1)
                        roomObjectId[y, x] = "wall";
                    if (x == 0 || x == roomLengthX - 1)
                        roomObjectId[y, x] = "wall";
                }
                setleftAndRightRoomWalls();
                setRoomCorners();
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
                            roomObjectId[y, x] = "leftWall";
                        else if (x == roomLengthX - 1)
                            roomObjectId[y, x] = "rightWall";
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
                    if (y == 0 && x == 0)
                    {
                        roomObjectId[y, x] = "leftCorner"; // sets left corners                   
                    }

                    if (y == 0 && x == roomLengthX - 1) 
                    {
                        roomObjectId[y, x] = "rightCorner"; // sets right corner
                    }                    
                }
            } 
        }
        //walls ===========================================================================================================
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