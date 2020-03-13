/*
 * Sirex production code:
 * Project: Spy-Do
 * Author: Voiz (Viktor Lishchuk)
 * Email: vitya.voody@gmail.com
 * Twitter: @V0IZ_
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MapGeneration;

//Inner class that creates floor for room
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

    public GameObject innerObject;

    public int numberOfRooms;
    public int minRoomHeightY;
    public int maxRoomHeightY;
    public int minRoomLengthX;
    public int maxRoomLengthX;

    public int minLocationHeightY;
    public int maxLocationHeightY;
    public int minLocationLengthX;
    public int maxLocationLengthX;

    private Room room;
    private Location location;
    private SLocationOfRoomsInformation slori;

    void Awake()
    {
        setStruct(); //constructor for sLocationOfRoomsInformation struct

        room = new Gym(Random.Range(minRoomHeightY, maxRoomHeightY), Random.Range(minRoomLengthX, maxRoomLengthX));
        location = new Location(slori, numberOfRooms, minRoomHeightY, maxRoomHeightY, minRoomLengthX, maxRoomLengthX);

    }

    void Start()
    {
        //location.Test(slori);
        createMap(location);

        //createRoom(room);

    }

    private void createLayer(string[,] objectMap, int heightY, int lengthX) 
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
                    case "innerObject":
                        Instantiate(innerObject, new Vector2(x, y), Quaternion.identity);
                        break;
                }
            }
        }
    }

    private void createRoom(Room room)
    {
        for (int layerNumber = 0; layerNumber < room.Layers.Count; layerNumber++)
        {
            for (int y = 0; y < room.Layers[layerNumber].LayerHeightY; y++)
            {
                for (int x = 0; x < room.Layers[layerNumber].LayerLengthX; x++)
                {
                    switch (room.Layers[layerNumber].LayerObjectMap[y, x])
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
                        case "innerObject":
                            Instantiate(innerObject, new Vector2(x, y), Quaternion.identity);
                            break;
                    }
                }
            }
        }
    }

    private void createMap(Location location)
    {
        for (int z = 0; z < location.MaxLocationNumberOfLayers; z++) 
        {
            for (int y = 0; y < location.LocationHeightY; y++)
            {
                for (int x = 0; x < location.LocationLengthX; x++)
                {
                    switch (location.LocationObjectMap[z, y, x])
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
                        case "innerObject":
                            Instantiate(innerObject, new Vector2(x, y), Quaternion.identity);
                            break;
                    }
                }
            }
        }
    }

    private void setStruct() 
    {
        slori.minHeightY = this.minLocationHeightY;
        slori.maxHeightY = this.maxLocationHeightY;
        slori.minLengthX = this.minLocationLengthX;
        slori.maxLengthX = this.maxLocationLengthX;
    }
}