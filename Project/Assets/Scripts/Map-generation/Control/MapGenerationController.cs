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
using System.Linq;
using UnityEngine;
using MapGenerator.Core;
using MapGenerator.DataTypes;
using Random = UnityEngine.Random;

namespace MapGenerator.Control
{
    //Inner class that creates floor for room
    class MapGenerationController : MonoBehaviour
    {
        [Header("Location generation properties")]
        public uint numberOfRoomRows = 4;
        public uint numberOfRoomColumns = 4;
        [Space(10)]
        public bool randomSpacingUsedInRows = true;
        [Space(10)]
        [Range(0, 100)]
        public int randomSpacingFromY;
        [Range(0, 100)]
        public int randomSpacingToY;
        [Space(10)]
        [Range(0, 100)]
        public int randomSpacingFromX;
        [Range(0, 100)]
        public int randomSpacingToX;
        
        [Header("Ventilation properties")] 
        [Range(1, 100)]
        public int turnProbability;
        [Range(0, 100)]
        public int maximumNumberOfPaths;
        public int sortingLayerForVentilationFloor = 100;
        public string ventilationEntranceTag = "VentilationEntrance";
        public GameObject ventilationFloor;
        public GameObject ventilationEntrance;

        [Header("General Room")] 
        public GameObject grFloor1;
        public GameObject grFloor2;
        public GameObject grFloor3;
        public GameObject grInnerObject;
        public GameObject grTopWall;
        public GameObject grBottomWall;
        public GameObject grLeftWall;
        public GameObject grRightWall;
        public GameObject grTopWallBrink;

        [Header("Office Objects")] 
        public GameObject officeFloor;
        public GameObject officeTopWallBrink;
        public GameObject officeWall;
        public GameObject officeLeftWall;
        public GameObject officeRightWall;
        public GameObject officeTable;
        public GameObject officeComputer;

        [Header("Gym Objects")]
        public GameObject gymFloor;
        public GameObject gymTopWallBrink;
        public GameObject gymWall;
        public GameObject gymLeftWall;
        public GameObject gymRightWall;
        public GameObject gymInnerObject;
        
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
            //Sets start position for generation
            transform.position = new Vector3(.0f, .0f, .0f);
            
            //Sets tabs for game object to arrange them in inspector 
            _ventilationTab = new GameObject("Ventilation");
            _locationTab = new GameObject("Location");
            _ventilationTab.transform.SetParent(transform);
            _locationTab.transform.SetParent(transform);
            
            //Binds name of the tiles with their game objects 
            AddTilesToDatabase();
            
            //Random generated array of rooms
            _roomsArray = LocationLogic.CreateRoomMapByDefaultLogic(numberOfRoomRows, numberOfRoomColumns, 
                (typeof(Office), 5, 10, 5, 10, 20),
            (typeof(Gym), 5, 10, 5, 10, 3),
            (typeof(GeneralRoom), 5, 10, 5, 10, 20));
            
            _location = new Location(new Office(), _roomsArray, randomSpacingUsedInRows,
                randomSpacingFromY, randomSpacingToY,
                randomSpacingFromX, randomSpacingToX);
        }

        void Start()
        {
            //Creates tiles in scene
            CreateMap(_location);
            //CreateVentilation(nameof(VentilationFloor), (new Vector2(20,20), new Vector2(30,30), 2));
            CreateVentilation(nameof(ventilationFloor), 
                LocationLogic.CreatePairsForVentilation(_ventilationEntrances, maximumNumberOfPaths, turnProbability));
        }

        //Method that store tiles and their name in map for future generation process 
        private void AddTilesToDatabase()
        {
            //Ventilation
            _mapObjects.Add(nameof(ventilationFloor), ventilationFloor);
            _mapObjects.Add(nameof(ventilationEntrance), ventilationEntrance);
            
            //Office tiles
            _mapObjects.Add(nameof(officeFloor), officeFloor);
            _mapObjects.Add(nameof(officeTopWallBrink), officeTopWallBrink);
            _mapObjects.Add(nameof(officeWall), officeWall);
            _mapObjects.Add(nameof(officeLeftWall), officeLeftWall);
            _mapObjects.Add(nameof(officeRightWall), officeRightWall);
            _mapObjects.Add(nameof(officeTable), officeTable);
            _mapObjects.Add(nameof(officeComputer), officeComputer);

            //Gym tiles
            _mapObjects.Add(nameof(gymFloor), gymFloor);
            _mapObjects.Add(nameof(gymTopWallBrink), gymTopWallBrink);
            _mapObjects.Add(nameof(gymWall), gymWall);
            _mapObjects.Add(nameof(gymLeftWall), gymLeftWall);
            _mapObjects.Add(nameof(gymRightWall), gymRightWall);
            _mapObjects.Add(nameof(gymInnerObject), gymInnerObject);

            //GeneralRoom tile
            _mapObjects.Add(nameof(grFloor1), grFloor1);
            _mapObjects.Add(nameof(grFloor2), grFloor2);
            _mapObjects.Add(nameof(grFloor3), grFloor3);
            _mapObjects.Add(nameof(grInnerObject), grInnerObject);
            _mapObjects.Add(nameof(grTopWall), grTopWall);
            _mapObjects.Add(nameof(grBottomWall), grBottomWall);
            _mapObjects.Add(nameof(grLeftWall), grLeftWall);
            _mapObjects.Add(nameof(grRightWall), grRightWall);
            _mapObjects.Add(nameof(grTopWallBrink), grTopWallBrink);
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

                            if (objectToInstantiate.tag.Equals(ventilationEntranceTag))
                            {
                                _ventilationEntrances.Add(Instantiate(objectToInstantiate,
                                        new Vector2(x, y), Quaternion.identity, _ventilationTab.transform).transform.position);
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
                .Select(edge => new DirectPathFinder(edge.startPos, edge.endPos, turnProbability))
                .ToList();

            //Instantiates tiles according to paths
            foreach (var directPathFinder in directPathFinders)
            {
                foreach (var tilePos in directPathFinder.Path)
                {
                    GameObject objectToInstantiate = _mapObjects[tile];
                    Renderer gameObjectRenderer = objectToInstantiate.GetComponent<Renderer>();

                    gameObjectRenderer.sortingOrder = sortingLayerForVentilationFloor;
                    Instantiate(objectToInstantiate, new Vector2(tilePos.x, tilePos.y), Quaternion.identity)
                        .transform.SetParent(_ventilationTab.transform);
                }
            }
        }
        
        #region Rooms
        class EmptySpace : Room
        {
            public EmptySpace(int heightY, int lengthX) : base(heightY, lengthX)
            {
            }

            protected override void CreateRoomObjectMap()
            {
                //========================Layer 0=======================
                AddRoomLayer();
                Layers[0].FillWholeLayerMap(nameof(grFloor2));
                Layers[0].SetOnRandomLayerID(nameof(grFloor2), 2);
                Layers[0].SetOnRandomLayerID(nameof(grFloor3), 2);

                //========================Layer 1=======================
                AddRoomLayer();
                Layers[1].SetOnRandomLayerID(nameof(grInnerObject), 8);

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
                Layers[0].FillWholeLayerMap(nameof(gymFloor));

                //========================Layer 1=======================
                AddRoomLayer();
                Layers[1].SetOnRandomLayerID(nameof(gymInnerObject), 5);

                //========================Layer 2=======================
                AddRoomLayer();
                Layers[2].SetHorizontalLayerLine(0, nameof(gymWall));
                Layers[2].SetHorizontalLayerLine(HeightY - 2, nameof(gymWall));

                //========================Layer 3=======================
                AddRoomLayer();
                Layers[3].SetVerticalLayerLine(0, nameof(gymLeftWall));
                Layers[3].SetVerticalLayerLine(LengthX - 1, nameof(gymRightWall));
                Layers[3].SetHorizontalLayerLine(0, null);
                Layers[3].SetHorizontalLayerLine(HeightY - 1, null);

                //========================Layer 4=======================
                AddRoomLayer();
                Layers[4].SetHorizontalLayerLine(1, nameof(gymTopWallBrink));
                Layers[4].SetHorizontalLayerLine(HeightY - 1, nameof(gymTopWallBrink));

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
                Layers[0].FillWholeLayerMap(nameof(officeFloor));
                Layers[0].SetHorizontalLayerLine(HeightY - 1, null);

                //========================Layer 1=======================
                AddRoomLayer();
                Layers[1].SetOnRandomLayerID(nameof(officeTable), 5);
                Layers[1].SetHorizontalLayerLine(HeightY - 1, null);

                //========================Layer 2=======================
                AddRoomLayer();
                Layers[2].SetOnUniqueObject(Layers[1].ObjectMap, nameof(officeTable), nameof(officeComputer), 4);
                Layers[2].SetHorizontalLayerLine(0, nameof(officeWall));
                Layers[2].SetHorizontalLayerLine(HeightY - 2, nameof(officeWall));

                //========================Layer 3=======================
                AddRoomLayer();
                Layers[3].SetVerticalLayerLine(0, nameof(officeLeftWall));
                Layers[3].SetVerticalLayerLine(LengthX - 1, nameof(officeRightWall));
                Layers[3].SetHorizontalLayerLine(0, null);
                Layers[3].SetHorizontalLayerLine(HeightY - 1, null);

                //========================Layer 4=======================
                AddRoomLayer();
                Layers[4].SetHorizontalLayerLine(1, nameof(officeTopWallBrink));
                Layers[4].SetHorizontalLayerLine(HeightY - 1, nameof(officeTopWallBrink));
                Layers[4].SetOnUniqueLayerID(4,4, nameof(ventilationEntrance));
                
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
                Layers[0].FillWholeLayerMap(nameof(grFloor1));
                Layers[0].SetOnRandomLayerID(nameof(grFloor2), 2);
                Layers[0].SetOnRandomLayerID(nameof(grFloor3), 2);
                Layers[0].SetHorizontalLayerLine(HeightY - 1, null);

                //========================Layer 1=======================
                AddRoomLayer();

                //========================Layer 2=======================
                AddRoomLayer();
                Layers[2].SetHorizontalLayerLine(HeightY - 2, nameof(grTopWall));
                Layers[2].SetOnRandomLayerID(nameof(grInnerObject), 8);
                Layers[0].SetHorizontalLayerLine(HeightY - 1, null);
                Layers[2].SetHorizontalLayerLine(HeightY - 2, nameof(grTopWall));

                //========================Layer 3=======================
                AddRoomLayer();
                Layers[3].SetHorizontalLayerLine(0, nameof(grBottomWall));
                Layers[3].SetHorizontalLayerLine(HeightY - 2, nameof(grTopWall));

                //========================Layer 4=======================
                AddRoomLayer();

                //========================Layer 5=======================
                AddRoomLayer();
                Layers[5].SetVerticalLayerLine(0, nameof(grLeftWall));
                Layers[5].SetVerticalLayerLine(LengthX - 1, nameof(grRightWall));
                Layers[5].SetHorizontalLayerLine(0, null);
                Layers[5].SetHorizontalLayerLine(HeightY - 1, null);

                //========================Layer 6=======================
                AddRoomLayer();
                Layers[6].SetHorizontalLayerLine(1, nameof(grTopWallBrink));
                Layers[6].SetHorizontalLayerLine(HeightY - 1, nameof(grTopWallBrink));

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
                COLayers[0].FillWholeLayerMap(nameof(gymFloor));
                //========================Layer 1=======================
                AddCOLayer();
                COLayers[1].FillWholeLayerMap(nameof(gymFloor));
            }
        }
        #endregion
    }
}