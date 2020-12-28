/*
 * Sirex production code:
 * Project: Spy-Do
 * Author: Voiz (Viktor Lishchuk)
 * Email: vitya.voody@gmail.com
 * GitHub: Vo1z
 * Twitter: @V0IZ_
 */

/*
 * Hint for adding rooms to map generator:
 * 1. Add tiles as GameObjects to the fields at MapGeneratorController class
 * 2. Add additional required fields for room (for example: probability)
 * 3. Add tiles to _mapObjects inside MapGeneratorController.AddTilesToDatabase() method
 * 4. Create RoomLogic extending Room class as inner class inside MapGeneratorController class
 * 5. Using Location logic class create room array passing required arguments for such generation
 * 6. Create instance of Location class and path created before array of rooms and all other required parameters
 * 7. Invoke CreateLocation method from MapGeneratorController pathing created location before 
 */

using System.Collections.Generic;
using UnityEngine;
using MapGenerator.Core;
using MapGenerator.DataTypes;
using Random = UnityEngine.Random;

namespace MapGenerator
{
    namespace Controll
    {
        //Inner class that creates floor for room
        class MapGenerationController : MonoBehaviour
        {
            // [Header("GenerationProperties")]

            [Header("General Room")] 
            public GameObject GRFloor1;
            public GameObject GRFloor2;
            public GameObject GRFloor3;
            public GameObject GRInnerObject;
            public GameObject GRTopWall;
            public GameObject GRBottomWall;
            public GameObject GRLeftWall;
            public GameObject GRRightWall;
            public GameObject GRTopWallBrink;

            [Header("Office Objects")]
            public GameObject OfficeFloor;
            public GameObject OfficeTopWallBrink;
            public GameObject OfficeWall;
            public GameObject OfficeLeftWall;
            public GameObject OfficeRightWall;
            public GameObject OfficeTable;
            public GameObject OfficeComputer;

            [Header("Gym Objects")]
            public GameObject GymFloor;
            public GameObject GymTopWallBrink;
            public GameObject GymWall;
            public GameObject GymLeftWall;
            public GameObject GymRightWall;
            public GameObject GymInnerObject;

            private Dictionary<string, GameObject> _mapObjects = new Dictionary<string, GameObject>();

            private Room[,] _roomsArray;
            private Room _room;
            private Location _location;
            
            void Awake()
            {
                AddTilesToDatabase();
                #region Debug
                //todo debug creates _roomsArray
                _roomsArray = LocationLogic.CreateRoomMapByDefaultLogic(5, 5,
                    (typeof(Office), 5, 10, 5, 10, 20), 
                    (typeof(Gym), 5, 10, 5, 10, 3), 
                    (typeof(GeneralRoom), 5,10,5,10,20));
                Office office = new Office();
                _room = new Office(10,10); 
                _location = new Location(office, _roomsArray, true, 0, 5, 0, 4);
                #endregion
            }

            void Start()
            {
                //Sets start position for generation
                transform.position = new Vector3(.0f,.0f,.0f);
                CreateMap(_location);
            }
            
            private void AddTilesToDatabase()
            {
                //Office tiles
                _mapObjects.Add(nameof(OfficeFloor), OfficeFloor);
                _mapObjects.Add(nameof(OfficeTopWallBrink), OfficeTopWallBrink);
                _mapObjects.Add(nameof(OfficeWall), OfficeWall);
                _mapObjects.Add(nameof(OfficeLeftWall), OfficeLeftWall);
                _mapObjects.Add(nameof(OfficeRightWall), OfficeRightWall);
                _mapObjects.Add(nameof(OfficeTable), OfficeTable);
                _mapObjects.Add(nameof(OfficeComputer), OfficeComputer);
                
                //Gym tiles
                _mapObjects.Add(nameof(GymFloor), GymFloor);
                _mapObjects.Add(nameof(GymTopWallBrink), GymTopWallBrink);
                _mapObjects.Add(nameof(GymWall), GymWall);
                _mapObjects.Add(nameof(GymLeftWall), GymLeftWall);
                _mapObjects.Add(nameof(GymRightWall), GymRightWall);
                _mapObjects.Add(nameof(GymInnerObject), GymInnerObject);
                
                //GeneralRoom tile
                _mapObjects.Add(nameof(GRFloor1),GRFloor1);
                _mapObjects.Add(nameof(GRFloor2), GRFloor2);
                _mapObjects.Add(nameof(GRFloor3), GRFloor3);
                _mapObjects.Add(nameof(GRInnerObject), GRInnerObject);
                _mapObjects.Add(nameof(GRTopWall), GRTopWall);
                _mapObjects.Add(nameof(GRBottomWall), GRBottomWall);
                _mapObjects.Add(nameof(GRLeftWall), GRLeftWall);
                _mapObjects.Add(nameof(GRRightWall), GRRightWall);
                _mapObjects.Add(nameof(GRTopWallBrink), GRTopWallBrink);
            }

            //Tested
            private void CreateRoom(Room room)
            {
                for (int layerNumber = 0; layerNumber < room.Layers.Count; layerNumber++)
                {
                    for (int y = 0; y < room.Layers[layerNumber].HeightY; y++)
                    {
                        for (int x = 0; x < room.Layers[layerNumber].LengthX; x++)
                        {
                            if (room.Layers[layerNumber].ObjectMap[y, x] != null)
                                Instantiate(_mapObjects[room.Layers[layerNumber].ObjectMap[y, x]],
                                    new Vector2(x, y), Quaternion.identity).transform.SetParent(transform);;
                        }
                    }
                }
            }
            
            //Tested
            private void CreateMap(Location location)
            {
                for (int z = 0; z < location.LocationObjectMap.GetLength(0); z++)
                {
                    for (int y = 0; y < location.LocationObjectMap.GetLength(1); y++)
                    {
                        for (int x = 0; x < location.LocationObjectMap.GetLength(2); x++)
                        {
  
                            if(location.LocationObjectMap[z, y, x] != null)
                                Instantiate(_mapObjects[location.LocationObjectMap[z, y, x]], 
                                    new Vector2(x, y), Quaternion.identity).transform.SetParent(transform);
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