using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MapGeneration;

//inner class that creates floor for room
class MapGenerationController : MonoBehaviour
{   
    public GameObject floor;
    public GameObject wall;

    public GameObject leftWall;
    public GameObject rightWall;

    public GameObject leftCorner;
    public GameObject rightCorner;

    public int roomLenght;
    public int roomHight;

    private Room room;

    void Awake()
    {
        room = new Room(roomLenght, roomHight);  
    }

    void Start()
    {
        createRoom();
        room.Test();
    }

    private void createRoom() 
    {
        for (int y = 0; y < room.GetRoomHeightY(); y++) 
        {
            for (int x = 0; x < room.GetRoomLengthX(); x++) 
            {
                switch (room.GetObjectId()[y, x]) 
                {
                    case "floor":
                        Instantiate(floor, new Vector2(x, y), Quaternion.identity);
                        break;
                    case "wall":
                        Instantiate(wall, new Vector2(x, y), Quaternion.identity);
                        break;
                    case "leftWall":
                        Instantiate(leftWall, new Vector2(x, y), Quaternion.identity);
                        break;
                    case "rightWall":
                        Instantiate(rightWall, new Vector2(x, y), Quaternion.identity);
                        break;
                    case "leftCorner":
                        Instantiate(leftCorner, new Vector2(x, y), Quaternion.identity);
                        break;
                    case "rightCorner":
                        Instantiate(rightCorner, new Vector2(x, y), Quaternion.identity);
                        break;
                }
            }
        }
    }

    /*private void createRoomFloor() 
    {
        for (int y = 0; y < roomGeneratorClass.GetRoomHeightY(); y++) 
        {
            for (int x = 0; x < roomGeneratorClass.GetRoomLengthX(); x++) 
            {
                if (roomGeneratorClass.GetObjectId()[y, x] == 0)
                Instantiate(floor, new Vector3(x, y), Quaternion.identity);
            }
        }
    }

    private void createRoomWalls()
    {
        for (int y = 0; y < roomGeneratorClass.GetRoomHeightY(); y++)
        {
            for (int x = 0; x < roomGeneratorClass.GetRoomLengthX(); x++)
            {
                //Creates corners
                if (roomGeneratorClass.GetObjectId()[y, x] == 2)
                {
                    Instantiate(leftCorner, new Vector3(x, y), Quaternion.identity);
                }
                else if (roomGeneratorClass.GetObjectId()[y, x] == 3)
                {
                    Instantiate(rightCorner, new Vector3(x, y), Quaternion.identity);
                }
                //Creates walls
                else if (roomGeneratorClass.GetObjectId()[y, x] == 1)
                {
                    Instantiate(wall, new Vector3(x, y), Quaternion.identity);
                }
                else if (roomGeneratorClass.GetObjectId()[y, x] == 4) 
                {
                    Instantiate(leftWall, new Vector3(x, y), Quaternion.identity);
                }
                else if (roomGeneratorClass.GetObjectId()[y, x] == 5)
                {
                    Instantiate(rightWall, new Vector3(x, y), Quaternion.identity);
                }
            }
        }
    }*/
}