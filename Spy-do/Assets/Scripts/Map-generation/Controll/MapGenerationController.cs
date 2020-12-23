/*
 * Sirex production code:
 * Project: Spy-Do
 * Author: Voiz (Viktor Lishchuk)
 * Email: vitya.voody@gmail.com
 * GitHub: Vo1z
 * Twitter: @V0IZ_
 */

using System;
using System.Collections.Generic;
using UnityEngine;
using MapGenerator.Core;
using MapGenerator.Exceptions;
using Random = UnityEngine.Random;

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

            private Dictionary<GameObject, string> _mapObjects = new Dictionary<GameObject, string>();

            private Room[,] _roomsArray = new Room[5,5];
            private Room _room;
            private Location _location;

            void Awake()
            {
                #region Debug
                //todo debug creates _roomsArray
                // for (int y = 0; y < _roomsArray.GetLength(0); y++)
                // {
                //     for (int x = 0; x < _roomsArray.GetLength(1); x++)
                //     {
                //         //_roomsArray[y, x] = new Office(Random.Range(5,10), Random.Range(5,10));
                //         _roomsArray[y,x] = new Gym(Random.Range(5,10), Random.Range(5,10));
                //     }
                // }
                _roomsArray = LocationLogic.CreateRoomMapByDefaultLogic(5, 5,
                    (typeof(Office), 5, 10, 5, 10, 3), 
                                    (typeof(Gym), 5, 10, 5, 10, 3),
                                    (typeof(GeneralRoom), 5,10,5,10,3));
                Office office = new Office();
                //_location = new Location(_roomsArray, true, 2, 0);
                _location = new Location(office, _roomsArray, true, 0, 5, 0, 4);
                //_location.Test();
                #endregion
            }

            void Start()
            {
                CreateMap(_location);
            }

            //ADD ROOM CONDITION 


            private void CreateRoom(Room room)
            {
                for (int layerNumber = 0; layerNumber < room.Layers.Count; layerNumber++)
                {
                    for (int y = 0; y < room.Layers[layerNumber].HeightY; y++)
                    {
                        for (int x = 0; x < room.Layers[layerNumber].LengthX; x++)
                        {
                            switch (room.Layers[layerNumber].ObjectMap[y, x])
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

            private void CreateMap(Location location)
            {
                for (int z = 0; z < location.LocationObjectMap.GetLength(0); z++)
                {
                    for (int y = 0; y < location.LocationObjectMap.GetLength(1); y++)
                    {
                        for (int x = 0; x < location.LocationObjectMap.GetLength(2); x++)
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
                                case nameof(OfficeFloor):
                                    Instantiate(OfficeFloor, new Vector2(x, y), Quaternion.identity);
                                    break;
                                case nameof(OfficeWall):
                                    Instantiate(OfficeWall, new Vector2(x, y), Quaternion.identity);
                                    break;
                                case nameof(OfficeTopWallBrink):
                                    Instantiate(OfficeTopWallBrink, new Vector2(x, y), Quaternion.identity);
                                    break;
                                case nameof(OfficeLeftWall):
                                    Instantiate(OfficeLeftWall, new Vector2(x, y), Quaternion.identity);
                                    break;
                                case nameof(OfficeRightWall):
                                    Instantiate(OfficeRightWall, new Vector2(x, y), Quaternion.identity);
                                    break;
                                case nameof(OfficeTable):
                                    Instantiate(OfficeTable, new Vector2(x, y), Quaternion.identity);
                                    break;
                                case nameof(OfficeComputer):
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

            //Rooms
            class EmptySpace : Room
            {
                public EmptySpace(int heightY, int lengthX) : base(heightY, lengthX)
                {
                }

                protected override void CreateRoomObjectMap()
                {
                    //========================Layer 0=======================
                    AddRoomLayer();
                    Layers[0].FillWholeLayerMap("GRFloor2");
                    Layers[0].SetOnRandomLayerID("GRFloor2", 2);
                    Layers[0].SetOnRandomLayerID("GRFloor3", 2);

                    //========================Layer 1=======================
                    AddRoomLayer();
                    Layers[1].SetOnRandomLayerID("GRInnerObject", 8);

                    AddRoomLayer();
                    AddRoomLayer();
                    SetDefaultExitAndLayerZ(new Test(), 2);
                }
            }

            class Gym : Room
            {
                public Gym(int heightY, int lengthX) : base(heightY, lengthX)
                {
                }

                protected override void CreateRoomObjectMap()
                {
                    //========================Layer 0=======================
                    AddRoomLayer();
                    Layers[0].FillWholeLayerMap("GymFloor");

                    //========================Layer 1=======================
                    AddRoomLayer();
                    Layers[1].SetOnRandomLayerID("GymInnerObject", 5);

                    //========================Layer 2=======================
                    AddRoomLayer();
                    Layers[2].SetHorizontalLayerLine(0, "GymWall");
                    Layers[2].SetHorizontalLayerLine(HeightY - 2, "GymWall");

                    //========================Layer 3=======================
                    AddRoomLayer();
                    Layers[3].SetVerticalLayerLine(0, "GymLeftWall");
                    Layers[3].SetVerticalLayerLine(LengthX - 1, "GymRightWall");
                    Layers[3].SetHorizontalLayerLine(0, null);
                    Layers[3].SetHorizontalLayerLine(HeightY - 1, null);

                    //========================Layer 4=======================
                    AddRoomLayer();
                    Layers[4].SetHorizontalLayerLine(1, "GymTopWallBrink");
                    Layers[4].SetHorizontalLayerLine(HeightY - 1, "GymTopWallBrink");

                    SetDefaultExitAndLayerZ(new Test(), 2);

                    SetExit(new Test(), 2, ExitPosition.LEFT, Random.Range(1, HeightY - 2));
                    SetExit(new Test(), 2, ExitPosition.RIGHT, Random.Range(1, HeightY - 2));
                }
            }

            class Office : Room
            {
                public Office(int heightY, int lengthX) : base(heightY, lengthX)
                {
                }

                public Office() : base()
                {
                }

                protected override void CreateRoomObjectMap()
                {
                    IsSpawned = true;
                    //========================Layer 0=======================
                    AddRoomLayer();
                    Layers[0].FillWholeLayerMap(nameof(OfficeFloor));
                    Layers[0].SetHorizontalLayerLine(HeightY - 1, null);

                    //========================Layer 1=======================
                    AddRoomLayer();
                    Layers[1].SetOnRandomLayerID(nameof(OfficeTable), 5);
                    Layers[1].SetHorizontalLayerLine(HeightY - 1, null);

                    //========================Layer 2=======================
                    AddRoomLayer();
                    Layers[2].SetOnUniqueObject(Layers[1].ObjectMap, nameof(OfficeTable), nameof(OfficeComputer), 4);
                    Layers[2].SetHorizontalLayerLine(0, nameof(OfficeWall));
                    Layers[2].SetHorizontalLayerLine(HeightY - 2, nameof(OfficeWall));

                    //========================Layer 3=======================
                    AddRoomLayer();
                    Layers[3].SetVerticalLayerLine(0, nameof(OfficeLeftWall));
                    Layers[3].SetVerticalLayerLine(LengthX - 1, nameof(OfficeRightWall));
                    Layers[3].SetHorizontalLayerLine(0, null);
                    Layers[3].SetHorizontalLayerLine(HeightY - 1, null);

                    //========================Layer 4=======================
                    AddRoomLayer();
                    Layers[4].SetHorizontalLayerLine(1, nameof(OfficeTopWallBrink));
                    Layers[4].SetHorizontalLayerLine(HeightY - 1, nameof(OfficeTopWallBrink));

                    SetDefaultExitAndLayerZ(new Test(), 2);

                    SetExit(new Test(), 2, ExitPosition.LEFT, Random.Range(1, HeightY - 2));
                    SetExit(new Test(), 2, ExitPosition.RIGHT, Random.Range(1, HeightY - 2));
                }
            }

            class GeneralRoom : Room
            {
                public GeneralRoom(int heightY, int lengthX) : base(heightY, lengthX)
                {
                }

                protected override void CreateRoomObjectMap()
                {
                    //========================Layer 0=======================
                    AddRoomLayer();
                    Layers[0].FillWholeLayerMap("GRFloor1");
                    Layers[0].SetOnRandomLayerID("GRFloor2", 2);
                    Layers[0].SetOnRandomLayerID("GRFloor3", 2);

                    //========================Layer 1=======================
                    AddRoomLayer();

                    //========================Layer 2=======================
                    AddRoomLayer();
                    Layers[2].SetHorizontalLayerLine(HeightY - 2, "GRTopWall");
                    Layers[2].SetOnRandomLayerID("GRInnerObject", 8);
                    Layers[2].SetHorizontalLayerLine(HeightY - 2, "GRTopWall");

                    //========================Layer 3=======================
                    AddRoomLayer();
                    Layers[3].SetHorizontalLayerLine(0, "GRBottomWall");
                    Layers[3].SetHorizontalLayerLine(HeightY - 2, "GRTopWall");

                    //========================Layer 4=======================
                    AddRoomLayer();

                    //========================Layer 5=======================
                    AddRoomLayer();
                    Layers[5].SetVerticalLayerLine(0, "GRLeftWall");
                    Layers[5].SetVerticalLayerLine(LengthX - 1, "GRRightWall");
                    Layers[5].SetHorizontalLayerLine(0, null);
                    Layers[5].SetHorizontalLayerLine(HeightY - 1, null);

                    //========================Layer 6=======================
                    AddRoomLayer();
                    Layers[6].SetHorizontalLayerLine(1, "GRTopWallBrink");
                    Layers[6].SetHorizontalLayerLine(HeightY - 1, "GRTopWallBrink");

                    SetDefaultExitAndLayerZ(new Test(), 2);
                }
            }


            //ComplexObjects
            class Test : ComplexObject
            {
                public Test() : base(1, 1)
                {
                }

                protected override void instCO()
                {
                    //========================Layer 0=======================
                    AddCOLayer();
                    COLayers[0].FillWholeLayerMap("GymFloor");
                    //========================Layer 1=======================
                    AddCOLayer();
                    COLayers[1].FillWholeLayerMap("GymFloor");
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