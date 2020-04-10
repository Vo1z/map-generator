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
    [Header("Location Propertirties")]
    public int minLocationHeightY;
    public int maxLocationHeightY;
    public int minLocationLengthX;
    public int maxLocationLengthX;

    [Header("Office Objects")]
    public bool isOffice;
    public int officeQuantity;

    public int officeMinHeightY;
    public int officeMaxHeightY;
    public int officeMinLengthX;
    public int officeMaxLengthX;

    public GameObject OfficeFloor;
    public GameObject OfficeTopWallBrink;
    public GameObject OfficeWall;
    public GameObject OfficeLeftWall;
    public GameObject OfficeRightWall;
    public GameObject OfficeTable;
    public GameObject OfficeComputer;

    [Header("Gym Objects")]
    public bool isGym;
    public int gymQuantity;

    public int gymMinHeightY;
    public int gymMaxHeightY;
    public int gymMinLengthX;
    public int gymMaxLengthX;

    public GameObject GymFloor;
    public GameObject GymTopWallBrink;
    public GameObject GymWall;
    public GameObject GymLeftWall;
    public GameObject GymRightWall;
    public GameObject GymInnerObject;

    //[Header("<ROOM> Objects")]
    //<ROOM> GAME OBJECTS

    private Room room;
    private Location location;
    private SLocationOfRoomsInformation slori;

    void Awake()
    {
        setStruct(); //constructor for sLocationOfRoomsInformation struct

        //room = new Gym(Random.Range(gymMinHeightY, gymMaxHeightY), Random.Range(gymMinLengthX, gymMaxLengthX));        
        //room = new Office(Random.Range(officeMinHeightY, officeMaxHeightY), Random.Range(officeMinLengthX, officeMaxLengthX));

        location = new Location(slori);
    }

    void Start()
    {
        //location.Test(slori);
        
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

    private void createRoom(Room room)//ADD ROOM CONDITION
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

    private void createMap(Location location)//ADD ROOM CONDITION
    {
        for (int z = 0; z < location.LocationLayersZ; z++) 
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

                        //-------------<ROOM>-------------
                        //<SWITCH OF <ROOM>>
                    }
                }
            }
        }
    }

    private int findSumOfAllLocationRooms()//ADD ROOM CONDITION
    {
        int sumOfAllLocationRooms = 0;

        if (isGym == true)
        {
            sumOfAllLocationRooms += gymQuantity;
        }
        if (isOffice == true) 
        {
            sumOfAllLocationRooms += officeQuantity;
        }
        /*if (is<ROOM> == true)
        {
            sumOfAllLocationRooms += <ROOM>Quantity;
        }*/


        return sumOfAllLocationRooms;
    }

    private void setStruct()//ADD ROOM CONDITION
    {
        slori.sumOfAllLocationRooms = findSumOfAllLocationRooms(); //responsible for sum of all rooms in the location
        slori.minLocationHeightY = this.minLocationHeightY;
        slori.maxLocationHeightY = this.maxLocationHeightY;
        slori.minLocationLengthX = this.minLocationLengthX;
        slori.maxLocationLengthX = this.maxLocationLengthX;

        //=====Gym======
        slori.gymQuantity = this.gymQuantity;

        slori.gymMinHeightY = this.gymMinHeightY;
        slori.gymMaxHeightY = this.gymMaxHeightY;
        slori.gymMinLengthX = this.gymMinLengthX;
        slori.gymMaxLengthX = this.gymMaxLengthX;

        //=====Office======
        slori.officeQuantity = this.officeQuantity;
        
        slori.officeMinHeightY = this.officeMinHeightY;
        slori.officeMaxHeightY = this.officeMaxHeightY;
        slori.officeMinLengthX = this.officeMinLengthX;
        slori.officeMaxLengthX = this.officeMaxLengthX;

        //=====<ROOM>======
        //<OBJECTS OF <ROOM>>

        slori.numberOfRoomTypes = 2; //Depends on number of rooms

        if (!isGym && !isOffice /*&& !is<ROOM>*/)
        {
            throw new AnyRoomsWereNotChoosenException();
        }
        else
        {
            slori.isGym = this.isGym;
            slori.isOffice = this.isOffice;
            /*slori.is<ROOM> = this.is<ROOM>;*/
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
