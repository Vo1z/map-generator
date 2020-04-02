﻿/*
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
    [Header("Location Propertirties")]
    public int numberOfRooms;
    public int minRoomHeightY;
    public int maxRoomHeightY;
    public int minRoomLengthX;
    public int maxRoomLengthX;

    public int minLocationHeightY;
    public int maxLocationHeightY;
    public int minLocationLengthX;
    public int maxLocationLengthX;

    [Header("Office Objects")]
    public bool isOffice;
    public GameObject OfficeFloor;

    public GameObject OfficeTopWallBrink;
    public GameObject OfficeWall;
    public GameObject OfficeLeftWall;
    public GameObject OfficeRightWall;

    public GameObject OfficeTable;
    public GameObject OfficeComputer;

    [Header("Gym Objects")]
    public bool IsGym;

    public GameObject GymFloor;

    public GameObject GymTopWallBrink;
    public GameObject GymWall;
    public GameObject GymLeftWall;
    public GameObject GymRightWall;
    public GameObject GymInnerObject;

    private Room room;
    private Location location;
    private SLocationOfRoomsInformation slori;

    void Awake()
    {
        setStruct(); //constructor for sLocationOfRoomsInformation struct

        //room = new Gym(Random.Range(minRoomHeightY, maxRoomHeightY), Random.Range(minRoomLengthX, maxRoomLengthX));
        location = new Location(slori, numberOfRooms, minRoomHeightY, maxRoomHeightY, minRoomLengthX, maxRoomLengthX);

    }

    void Start()
    {
        location.Test(slori);
        createMap(location);

        //createRoom(room);

    }

/*    private void createLayer(string[,] objectMap, int heightY, int lengthX) 
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
    }*/

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
                        //-------------Office-------------
                        case "OfficeFloor":
                            Instantiate(OfficeFloor, new Vector2(x, y), Quaternion.identity);
                            break;
                        case "OfficeWall":
                            Instantiate(OfficeWall, new Vector2(x, y), Quaternion.identity);
                            break;
                        case "OfficeTopWallBrink":
                            Instantiate(OfficeTopWallBrink, new Vector2(x, y), Quaternion.identity);
                            break;
                        case "OfficeLeftWall":
                            Instantiate(OfficeLeftWall, new Vector2(x, y), Quaternion.identity);
                            break;
                        case "OfficeRightWall":
                            Instantiate(OfficeRightWall, new Vector2(x, y), Quaternion.identity);
                            break;
                        case "OfficeTable":
                            Instantiate(OfficeTable, new Vector2(x, y), Quaternion.identity);
                            break;
                        case "OfficeComputer":
                            Instantiate(OfficeComputer, new Vector2(x, y), Quaternion.identity);
                            break;

                        //-------------Gym-------------
                        case "GymFloor":
                            Instantiate(GymFloor, new Vector2(x, y), Quaternion.identity);
                            break;
                        case "GymWall":
                            Instantiate(GymWall, new Vector2(x, y), Quaternion.identity);
                            break;
                        case "GymLeftWall":
                            Instantiate(GymLeftWall, new Vector2(x, y), Quaternion.identity);
                            break;
                        case "GymRightWall":
                            Instantiate(GymRightWall, new Vector2(x, y), Quaternion.identity);
                            break;
                        case "GymTopWallBrink":
                            Instantiate(GymTopWallBrink, new Vector2(x, y), Quaternion.identity);
                            break;
                        case "GymInnerObject":
                            Instantiate(GymInnerObject, new Vector2(x, y), Quaternion.identity);
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
                        //-------------Office-------------
                        case "OfficeFloor":
                            Instantiate(OfficeFloor, new Vector2(x, y), Quaternion.identity);
                            break;
                        case "OfficeWall":
                            Instantiate(OfficeWall, new Vector2(x, y), Quaternion.identity);
                            break;
                        case "OfficeTopWallBrink":
                            Instantiate(OfficeTopWallBrink, new Vector2(x, y), Quaternion.identity);
                            break;
                        case "OfficeLeftWall":
                            Instantiate(OfficeLeftWall, new Vector2(x, y), Quaternion.identity);
                            break;
                        case "OfficeRightWall":
                            Instantiate(OfficeRightWall, new Vector2(x, y), Quaternion.identity);
                            break;
                        case "OfficeTable":
                            Instantiate(OfficeTable, new Vector2(x, y), Quaternion.identity);
                            break;
                        case "OfficeComputer":
                            Instantiate(OfficeComputer, new Vector2(x, y), Quaternion.identity);
                            break;

                        //-------------Gym-------------
                        case "GymFloor":
                            Instantiate(GymFloor, new Vector2(x, y), Quaternion.identity);
                            break;
                        case "GymWall":
                            Instantiate(GymWall, new Vector2(x, y), Quaternion.identity);
                            break;
                        case "GymLeftWall":
                            Instantiate(GymLeftWall, new Vector2(x, y), Quaternion.identity);
                            break;
                        case "GymRightWall":
                            Instantiate(GymRightWall, new Vector2(x, y), Quaternion.identity);
                            break;
                        case "GymTopWallBrink":
                            Instantiate(GymTopWallBrink, new Vector2(x, y), Quaternion.identity);
                            break;
                        case "GymInnerObject":
                            Instantiate(GymInnerObject, new Vector2(x, y), Quaternion.identity);
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

        slori.numberOfRoomTypes = 2; //Depends on number of rooms

        if (!IsGym && !isOffice)
        {
            throw new AnyRoomsWereNotChoosenException();
        }
        else
        {
            slori.isGym = this.IsGym;
            slori.isOffice = this.isOffice;
        }
    }
}

/* How to add new room:
 * 1) Create room inside Rooms.cs as inherited class from Room
 * 2) Add objets of the room to the field
 * 3) Adjust createMap() method with objects of your room
 * 4) Increase numberOfRoomTypes in setStruct() method
 * 5) Add else if structure to  MapGenerationLibrary.generateLocationRooms()
 */
