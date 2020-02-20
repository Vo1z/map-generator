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

    public GameObject topWallBrink;
    public GameObject leftTopBrinkCorner;
    public GameObject rightTopBrinkCorner;

    public int roomHeight;
    public int roomLength;

    private Room room;

    void Awake()
    {
        room = new Room(roomHeight, roomLength);  
    }

    void Start()
    {
        createRoomLayer(room.GetRoomLayer(0).LayerObjectMap, room.GetRoomLayer(0).LayerHeightY, room.GetRoomLayer(0).LayerLengthX);
        createRoomLayer(room.GetRoomLayer(1).LayerObjectMap, room.GetRoomLayer(1).LayerHeightY, room.GetRoomLayer(1).LayerLengthX);
        createRoomLayer(room.GetRoomLayer(2).LayerObjectMap, room.GetRoomLayer(2).LayerHeightY, room.GetRoomLayer(2).LayerLengthX);
    }

    private void createRoomLayer(string[,] objectMap, int heightY, int lengthX) 
    {
        for (int y = 0; y < heightY; y++) 
        {
            for (int x = 0; x < lengthX; x++) 
            {
                switch (objectMap[y, x]) 
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
                    case "leftTopBrinkCorner":
                        Instantiate(leftTopBrinkCorner, new Vector2(x, y), Quaternion.identity);
                        break;
                    case "rightTopBrinkCorner":
                        Instantiate(rightTopBrinkCorner, new Vector2(x, y), Quaternion.identity);
                        break;
                    case "topWallBrink":
                        Instantiate(topWallBrink, new Vector2(x, y), Quaternion.identity);
                        break;
                }
            }
        }
    }
}