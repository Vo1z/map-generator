using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MapGeneration 
{
    public class Room                                     //Class which is responsible for collecting data for creating room 
    {
        private int roomHightY;                                         //Variable that stores width of the room of Y dimension
        private int roomLengthX;                                        //Variable that stores width of the room of X dimension
        private string[,] roomObjectMap;                                    // floor - 0 wals - 1; left corner - 2;  right corner - 3; left wall - 4; right wall 5 and so on;
        private Layer layer1;

        public Room(int roomHightY, int roomLengthX)      //Constructor
        {
            this.roomHightY = roomHightY;
            this.roomLengthX = roomLengthX;
            this.roomObjectMap = new string[roomHightY, roomLengthX];
            this.layer1 = new Layer(roomHightY, roomLengthX);      
            setRoomFloor();
            setRoomWalls();            
        }

        // Room methods ================================================================
        public int GetRoomHeightY()
        {
            return roomHightY;
        }

        public int GetRoomLengthX() 
        {
            return roomLengthX;
        }

        public string[,] GetRoomObjectMap()                                     //Method which returns array that stores an ID of an object that is located on universal coordinate 
        {
            return roomObjectMap;
        }

        public Layer GetRoomLayer() 
        {
            return layer1;
        }

        public void Test()
        {
            foreach (string objectid in roomObjectMap)
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
                    roomObjectMap[y, x] = "floor";
                }
            }
        }

        private void setRoomWalls()                                   //Sets coordinates for each block
        {
            for (int y = 0; y < roomHightY; y++) 
            {
                for (int x = 0; x < roomLengthX; x++) 
                {
                    if (y == roomHightY - 1)
                        roomObjectMap[y, x] = "wall";                   
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
                            roomObjectMap[y, x] = "leftWall";
                        else if (x == roomLengthX - 1)
                            roomObjectMap[y, x] = "rightWall";
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
                        roomObjectMap[y, x] = "leftCorner"; // sets left corners                   
                    }

                    if (y == 0 && x == roomLengthX - 1) 
                    {
                        roomObjectMap[y, x] = "rightCorner"; // sets right corner
                    }                    
                }
            } 
        }        
    }

    public class Layer
    {
        private string[,] layerObjectMap;
        private int layerHeightY;
        private int layerLengthX;

        internal Layer(int heightY, int lengthX) 
        {
            this.layerObjectMap = new string[heightY + 1, lengthX];
            this.layerHeightY = heightY + 1;
            this.layerLengthX = lengthX;
            setLayerObjects();
        }

        public string[,] getLayerObjectMap() 
        {
            return layerObjectMap;
        }

        public int GetLayerLengthX() 
        {
            return layerLengthX;
        }

        public int GetLayerHeightY() 
        {
            return layerHeightY;
        }

        private void setLayerObjects() 
        {
            setRoomBrinks();
            setBottomWalls();
            setTopBrinckCorners();
        }

        private void setRoomBrinks() 
        {
            for (int y = 0; y < layerHeightY; y++)
            {
                for (int x = 0; x < layerLengthX; x++)
                {
                    if ((y == layerHeightY - 1 || y == 1) && (x != 0 && x != layerLengthX - 1))
                    {
                        layerObjectMap[y, x] = "topWallBrink";
                    }
                }
            }
        }

        private void setBottomWalls() 
        {
            for (int y = 0; y < layerHeightY; y++)
            {
                for (int x = 0; x < layerLengthX; x++)
                {
                    if (y == 0 && x != 0 && x!= layerLengthX - 1)
                    {
                        layerObjectMap[y, x] = "wall";
                    }
                }
            }
        }

        private void setTopBrinckCorners() 
        {
            for (int y = 0; y < layerHeightY; y++)
            {
                for (int x = 0; x < layerLengthX; x++)
                {
                    if (y == layerHeightY - 1 && x == 0)
                    {
                        layerObjectMap[y, x] = "leftTopBrinkCorner";
                    }
                    if (y == layerHeightY - 1 && x == layerLengthX - 1) 
                    {
                        layerObjectMap[y, x] = "rightTopBrinkCorner";
                    }
                }
            }
        }
    }
}

/*
            TO DO LIST
1. add corners - done
2. left, right, top and down walls - done;
3. add layers
4. add doors
5. add inner objects
6. add top side of the wall
*/