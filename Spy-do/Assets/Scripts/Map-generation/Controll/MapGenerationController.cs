/*
 * Sirex production code:
 * Project: Spy-Do
 * Author: Voiz (Viktor Lishchuk)
 * Email: vitya.voody@gmail.com
 * Twitter: @V0IZ_
 */

using UnityEngine;
using MapGenerator.Core;
using MapGenerator.Exceptions;

namespace MapGenerator
{
    namespace Controll
    {
    //Inner class that creates floor for room
        class MapGenerationController : MonoBehaviour
        {
            [Header("Location Propertirties")] public int minLocationHeightY;
            public int maxLocationHeightY;
            public int minLocationLengthX;
            public int maxLocationLengthX;

            [Header("General Room")] public GameObject GRFloor1;
            public GameObject GRFloor2;
            public GameObject GRFloor3;

            public GameObject GRInnerObject;

            public GameObject GRTopWall;
            public GameObject GRBottomWall;
            public GameObject GRLeftWall;
            public GameObject GRRightWall;
            public GameObject GRTopWallBrink;

            [Header("Office Objects")] public bool isOffice;
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

            [Header("Gym Objects")] public bool isGym;
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

            [Header("Empty Space")] public bool isEmptySpace;
            public int emptySpaceQuantity;

            public int emptySpaceMinHeightY;
            public int emptySpaceMaxHeightY;
            public int emptySpaceMinLengthX;

            public int emptySpaceMaxLengthX;
            //[Header("<ROOM> Objects")]
            //<ROOM> GAME OBJECTS

            private Room _room;
            private Location _location;

            void Awake()
            {
                //room_ = new Gym(Random.Range(gymMinHeightY, gymMaxHeightY), Random.Range(gymMinLengthX, gymMaxLengthX));        
                //room_ = new Office(Random.Range(officeMinHeightY, officeMaxHeightY), Random.Range(officeMinLengthX, officeMaxLengthX));
                //room_ = new GeneralRoom(Random.Range(officeMinHeightY, officeMaxHeightY), Random.Range(officeMinLengthX, officeMaxLengthX));

                //_location = new Location();
            }

            void Start()
            {
                //location.Test(slori);
                createMap(_location);

                //createRoom(room_);
            }

            //ADD ROOM CONDITION
            private void createRoom(Room room)
            {
                for (int layerNumber = 0; layerNumber < room.RoomLayers.Count; layerNumber++)
                {
                    for (int y = 0; y < room.RoomLayers[layerNumber].LayerHeightY; y++)
                    {
                        for (int x = 0; x < room.RoomLayers[layerNumber].LayerLengthX; x++)
                        {
                            switch (room.RoomLayers[layerNumber].LayerObjectMap[y, x])
                            {
                                //-------------General Room-------------
                                case "GRFloor1":
                                    Instantiate(GRFloor1, new Vector2(x, y), Quaternion.identity);
                                    break;
                                case "GRFloor2":
                                    Instantiate(GRFloor2, new Vector2(x, y), Quaternion.identity);
                                    break;
                                case "GRFloor3":
                                    Instantiate(GRFloor3, new Vector2(x, y), Quaternion.identity);
                                    break;
                                case "GRTopWall":
                                    Instantiate(GRTopWall, new Vector2(x, y), Quaternion.identity);
                                    break;
                                case "GRBottomWall":
                                    Instantiate(GRBottomWall, new Vector2(x, y), Quaternion.identity);
                                    break;
                                case "GRLeftWall":
                                    Instantiate(GRLeftWall, new Vector2(x, y), Quaternion.identity);
                                    break;
                                case "GRRightWall":
                                    Instantiate(GRRightWall, new Vector2(x, y), Quaternion.identity);
                                    break;
                                case "GRTopWallBrink":
                                    Instantiate(GRTopWallBrink, new Vector2(x, y), Quaternion.identity);
                                    break;
                                case "GRInnerObject":
                                    Instantiate(GRInnerObject, new Vector2(x, y), Quaternion.identity);
                                    break;

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

            //ADD ROOM CONDITION
            private void createMap(Location location)
            {
                for (int z = 0; z < location.LocationLayersZ; z++)
                {
                    for (int y = 0; y < location.LocationHeightY; y++)
                    {
                        for (int x = 0; x < location.LocationLengthX; x++)
                        {
                            switch (location.LocationObjectMap[z, y, x])
                            {
                                //-------------General Room-------------
                                case "GRFloor1":
                                    Instantiate(GRFloor1, new Vector2(x, y), Quaternion.identity);
                                    break;
                                case "GRFloor2":
                                    Instantiate(GRFloor2, new Vector2(x, y), Quaternion.identity);
                                    break;
                                case "GRFloor3":
                                    Instantiate(GRFloor3, new Vector2(x, y), Quaternion.identity);
                                    break;
                                case "GRTopWall":
                                    Instantiate(GRTopWall, new Vector2(x, y), Quaternion.identity);
                                    break;
                                case "GRBottomWall":
                                    Instantiate(GRBottomWall, new Vector2(x, y), Quaternion.identity);
                                    break;
                                case "GRLeftWall":
                                    Instantiate(GRLeftWall, new Vector2(x, y), Quaternion.identity);
                                    break;
                                case "GRRightWall":
                                    Instantiate(GRRightWall, new Vector2(x, y), Quaternion.identity);
                                    break;
                                case "GRTopWallBrink":
                                    Instantiate(GRTopWallBrink, new Vector2(x, y), Quaternion.identity);
                                    break;
                                case "GRInnerObject":
                                    Instantiate(GRInnerObject, new Vector2(x, y), Quaternion.identity);
                                    break;

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
        }
    }
}

/* How to add new room:
 * 1) Create room inside Rooms.cs as inherited class from Room
 * 2) Add objets of the room to the field
 * 3) Adjust findSumOfAllLocationRooms()
 * 4) Adjust slori
 * 5) Adjust setStruct()
 * 6) Adjust createMap() method with objects of your room
 * 7) Increase numberOfRoomTypes in setStruct() method
 * 8) Add else if structure to  MapGenerationLibrary.generateLocationRooms()
 */