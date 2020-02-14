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
        private int[,] roomCoordinates;                                 //Array that stores coordinates of location blocks(floor, walls, enviroment and so on) of a room
        private int[,] roomObjectId;                                    // floor - 0 wals - 1; left corner - 2;  right corner - 3; object4 - 5 and so on;

        public RoomGeneratorClass(int roomHightY, int roomLengthX)      //Constructor
        {
            this.roomHightY = roomHightY;
            this.roomLengthX = roomLengthX;
            roomCoordinates = new int[roomHightY, roomLengthX];
            roomObjectId = new int[roomHightY, roomLengthX];
            setRoomCoordinates();
            setRoomWalls();
            setRoomCorners();
        }

        public int[,] getRoom()                                         //Method which returns array that stores coordinates of location blocks of a room
        {
            return roomCoordinates;
        }

        public int[,] getObjectId()                                     //Method which returns array that stores an ID of an object that is located on universal coordinate 
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

        private void setRoomCoordinates()                         //Sets coordinates for each block
        {
            for (int y = 0; y < roomHightY; y++)
            {
                for (int x = 0; x < roomLengthX; x++)
                {
                    roomCoordinates[y, x] = x;
                }
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

        private void setRoomCorners() 
        {                                                              //Left corner - 3; Right corner 4; 
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

    public class LocationGenerator 
    {

    }
}

/*
            TO DO LIST
1. add corners left, right, top and down wals
2. add doors
3. add inner objects
4. add random (not sure)
*/