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

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using MapGenerator.Core;
using MapGenerator.DataTypes;
using UnityEditor.UIElements;
using Random = UnityEngine.Random;

namespace MapGenerator.Control
{
    //Inner class that creates floor for room
    class MapGenerationController : MonoBehaviour
    {
        [Header("Ventilation properties")] 
        [Range(0, 60)]
        public int TurnProbability = 4;
        [Range(0, 100)]
        public int MaximumNumberOfPaths = 0;
        public string VentilationEntranceTag = "VentilationEntrance";
        public GameObject VentilationFloor;
        public GameObject VentilationEntrance;

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
        
        //Map that maps name of the variables to its game objects
        private readonly Dictionary<string, GameObject> _mapObjects = new Dictionary<string, GameObject>();
        private readonly List<Vector2> _ventilationEntrances = new List<Vector2>();
        
        //Array that holds rooms of the location
        private Room[,] _roomsArray;
        private Location _location;
        
        //Game object for grouping ventilation tiles on scene
        private GameObject _ventilationTab;
        //Game object for grouping location tiles on scene
        private GameObject _locationTab;
        
        void Awake()
        {
            AddTilesToDatabase();

            #region TabsInstantiation

            _ventilationTab = new GameObject("Ventilation");
            _locationTab = new GameObject("Location");
            _ventilationTab.transform.SetParent(transform);
            _locationTab.transform.SetParent(transform);
            
            #endregion

            #region Debug
            
            //Random generated array of rooms
            _roomsArray = LocationLogic.CreateRoomMapByDefaultLogic(5, 5, 
                (typeof(Office), 5, 10, 5, 10, 20),
            (typeof(Gym), 5, 10, 5, 10, 3),
            (typeof(GeneralRoom), 5, 10, 5, 10, 20));

            _location = new Location(new Office(), _roomsArray, true,
                0, 5,
                0, 4);
            
            #endregion
        }

        void Start()
        {
            //Sets start position for generation
            transform.position = new Vector3(.0f, .0f, .0f);

            CreateMap(_location);
            
            //CreateVentilation(nameof(VentilationFloor), (new Vector2(20,20), new Vector2(30,30), 2));
            CreateVentilation(nameof(VentilationFloor), 
                LocationLogic.CreatePairsForVentilation(_ventilationEntrances, MaximumNumberOfPaths, TurnProbability));
        }

        private void AddTilesToDatabase()
        {
            //Ventilation
            _mapObjects.Add(nameof(VentilationFloor), VentilationFloor);
            _mapObjects.Add(nameof(VentilationEntrance), VentilationEntrance);
            
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
            _mapObjects.Add(nameof(GRFloor1), GRFloor1);
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
                                new Vector2(x, y), Quaternion.identity).transform.SetParent(_locationTab.transform);
                        ;
                    }
                }
            }
        }

        //Tested
        private void CreateMap(Location location)
        {
            for (int z = 0; z < location.LocationObjectMap.GetLength(0); z++)
                for (int y = 0; y < location.LocationObjectMap.GetLength(1); y++)
                    for (int x = 0; x < location.LocationObjectMap.GetLength(2); x++)
                    {
                        if (location.LocationObjectMap[z, y, x] != null)
                        {
                            GameObject objectToInstantiate = _mapObjects[location.LocationObjectMap[z, y, x]];
                            Renderer gameObjectRenderer = objectToInstantiate.GetComponent<Renderer>();
                            gameObjectRenderer.sortingOrder = z;

                            if (objectToInstantiate.tag.Equals(VentilationEntranceTag))
                            {
                                var ventilationEntrance = Instantiate(objectToInstantiate,
                                        new Vector2(x, y), Quaternion.identity);
                                ventilationEntrance.transform.SetParent(_ventilationTab.transform);
                                
                                _ventilationEntrances.Add(ventilationEntrance.transform.position);
                            }
                            else
                            {
                                Instantiate(objectToInstantiate,
                                    new Vector2(x, y), Quaternion.identity).transform.SetParent(_locationTab.transform);
                            }
                        }
                    }
        }

        //Tested
        //Instantiates tiles for ventilation
        private void CreateVentilation(string tile,
            params (Vector2 startPos, Vector2 endPos, int turnProbability)[] edges)
        {
            //Maps all given start and end position into List to generate all paths later
            List<DirectPathFinder> directPathFinders = edges
                .Select(edge => new DirectPathFinder(edge.startPos, edge.endPos, TurnProbability))
                .ToList();
            
            //todo replace debug
            Debug.Log(edges.Length);

            //Instantiates tiles according to paths
            foreach (var directPathFinder in directPathFinders)
            {
                foreach (var tilePos in directPathFinder.Path)
                {
                    GameObject objectToInstantiate = _mapObjects[tile];
                    Renderer gameObjectRenderer = objectToInstantiate.GetComponent<Renderer>();

                    gameObjectRenderer.sortingOrder = 100; //todo handle this
                    Instantiate(objectToInstantiate, new Vector2(tilePos.x, tilePos.y), Quaternion.identity)
                        .transform.SetParent(_ventilationTab.transform);
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
                Layers[0].FillWholeLayerMap(nameof(GRFloor2));
                Layers[0].SetOnRandomLayerID(nameof(GRFloor2), 2);
                Layers[0].SetOnRandomLayerID(nameof(GRFloor3), 2);

                //========================Layer 1=======================
                AddRoomLayer();
                Layers[1].SetOnRandomLayerID(nameof(GRInnerObject), 8);

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
                Layers[0].FillWholeLayerMap(nameof(GymFloor));

                //========================Layer 1=======================
                AddRoomLayer();
                Layers[1].SetOnRandomLayerID(nameof(GymInnerObject), 5);

                //========================Layer 2=======================
                AddRoomLayer();
                Layers[2].SetHorizontalLayerLine(0, nameof(GymWall));
                Layers[2].SetHorizontalLayerLine(HeightY - 2, nameof(GymWall));

                //========================Layer 3=======================
                AddRoomLayer();
                Layers[3].SetVerticalLayerLine(0, nameof(GymLeftWall));
                Layers[3].SetVerticalLayerLine(LengthX - 1, nameof(GymRightWall));
                Layers[3].SetHorizontalLayerLine(0, null);
                Layers[3].SetHorizontalLayerLine(HeightY - 1, null);

                //========================Layer 4=======================
                AddRoomLayer();
                Layers[4].SetHorizontalLayerLine(1, nameof(GymTopWallBrink));
                Layers[4].SetHorizontalLayerLine(HeightY - 1, nameof(GymTopWallBrink));

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
                Layers[4].SetOnUniqueLayerID(4,4, nameof(VentilationEntrance));
                
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
                Layers[0].FillWholeLayerMap(nameof(GRFloor1));
                Layers[0].SetOnRandomLayerID(nameof(GRFloor2), 2);
                Layers[0].SetOnRandomLayerID(nameof(GRFloor3), 2);

                //========================Layer 1=======================
                AddRoomLayer();

                //========================Layer 2=======================
                AddRoomLayer();
                Layers[2].SetHorizontalLayerLine(HeightY - 2, nameof(GRTopWall));
                Layers[2].SetOnRandomLayerID(nameof(GRInnerObject), 8);
                Layers[2].SetHorizontalLayerLine(HeightY - 2, nameof(GRTopWall));

                //========================Layer 3=======================
                AddRoomLayer();
                Layers[3].SetHorizontalLayerLine(0, nameof(GRBottomWall));
                Layers[3].SetHorizontalLayerLine(HeightY - 2, nameof(GRTopWall));

                //========================Layer 4=======================
                AddRoomLayer();

                //========================Layer 5=======================
                AddRoomLayer();
                Layers[5].SetVerticalLayerLine(0, nameof(GRLeftWall));
                Layers[5].SetVerticalLayerLine(LengthX - 1, nameof(GRRightWall));
                Layers[5].SetHorizontalLayerLine(0, null);
                Layers[5].SetHorizontalLayerLine(HeightY - 1, null);

                //========================Layer 6=======================
                AddRoomLayer();
                Layers[6].SetHorizontalLayerLine(1, nameof(GRTopWallBrink));
                Layers[6].SetHorizontalLayerLine(HeightY - 1, nameof(GRTopWallBrink));

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
                COLayers[0].FillWholeLayerMap(nameof(GymFloor));
                //========================Layer 1=======================
                AddCOLayer();
                COLayers[1].FillWholeLayerMap(nameof(GymFloor));
            }
        }
    }
}