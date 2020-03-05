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

namespace MapGeneration
{
    
    //Class that describes layers (Is used in Room class)
    public class Layer                                                          
    {
        public readonly string[,] LayerObjectMap;
        public readonly int LayerHeightY;
        public readonly int LayerLengthX;

        internal Layer(int heightY, int lengthX)
        {
            this.LayerObjectMap = new string[heightY, lengthX];
            this.LayerHeightY = heightY;
            this.LayerLengthX = lengthX;
        }

        public void FillWholeLayerMap(string objectName) 
        {
            for (int y = 0; y < LayerHeightY; y++)
            {
                for (int x = 0; x < LayerLengthX; x++)
                {
                    LayerObjectMap[y, x] = objectName;
                }
            }
        }                

        public void SetLayerEdges(int numberOfInnerCircle, string objectName) 
        {
            for (int y = 0; y < LayerHeightY; y++) 
            {
                for (int x = 0; x < LayerLengthX; x++) 
                {
                    if ((y == numberOfInnerCircle || y == (LayerHeightY - 1) - numberOfInnerCircle) && (x >= numberOfInnerCircle && x <= (LayerLengthX - 1) - numberOfInnerCircle))
                        LayerObjectMap[y, x] = objectName;                                           

                    if ((x == numberOfInnerCircle || x == (LayerLengthX - 1) - numberOfInnerCircle) && (y >= numberOfInnerCircle && y <= (LayerHeightY - 1) - numberOfInnerCircle))
                        LayerObjectMap[y, x] = objectName;                    
                }
            }
        }

        public void SetVerticalLayerLine(int Xindex, string objectName) 
        {
            for (int y = 0; y < LayerHeightY; y++)
            {
                for (int x = 0; x < LayerLengthX; x++)
                {
                    if (x == Xindex)
                    {
                        LayerObjectMap[y, x] = objectName;
                    }
                }
            }
        }

        public void SetHorizontalLayerLine(int Yindex, string objectName) 
        {
            for (int y = 0; y < LayerHeightY; y++)
            {
                for (int x = 0; x < LayerLengthX; x++)
                {
                    if (y == Yindex)
                    {
                        LayerObjectMap[y, x] = objectName;
                    }
                }
            }
        }

        public void SetUniqueCorners(string leftTopCorner, string rightTopCorner, string leftBottomCorner, string rightBottomCorner) 
        {
            LayerObjectMap[0, 0] = leftBottomCorner;
            LayerObjectMap[0, LayerLengthX - 1] = rightBottomCorner;
            LayerObjectMap[LayerHeightY - 1, 0] = leftTopCorner;
            LayerObjectMap[LayerHeightY - 1, LayerLengthX - 1] = rightTopCorner;
        }

        public void SetBottomLayerCorners(string objectName)
        {
            LayerObjectMap[0, 0] = objectName;
            LayerObjectMap[0, LayerLengthX - 1] = objectName;
        }

        public void SetTopLayerCorners(string objectName)
        {
            LayerObjectMap[LayerHeightY - 1, 0] = objectName;
            LayerObjectMap[LayerHeightY - 1, LayerLengthX - 1] = objectName;
        }

        public void SetOnUniqueLayerID(int y, int x, string objectName) 
        {
            LayerObjectMap[y, x] = objectName;
        }

        public void SetOnRandomLayerID(string objectName)                               //Randomly sets given object inside given area
        {
            for (int y = 0; y < LayerHeightY; y++) 
            {
                for (int x = 0; x < LayerLengthX; x++) 
                {
                    if ((Random.Range(0, 5) == 1) && x != 0 && x != LayerLengthX - 1)   //Randomizer of inner objects
                    {
                        LayerObjectMap[y, x] = objectName;
                    }
                }
            }
        }
    }


    //Class which is responsible for collecting data for creating room 
    public class Room
    {

        public readonly int RoomHeightY;                                        //Variable that stores width of the room of Y dimension
        public readonly int RoomLengthX;                                        //Variable that stores width of the room of X dimension

        public readonly List<Layer> Layers = new List<Layer>();

        public Room(int roomHeightY, int roomLengthX)                           //Constructor
        {
            this.RoomHeightY = roomHeightY;
            this.RoomLengthX = roomLengthX;
            instRoom();
        }

        public void AddRoomLayer(int layerHeightY, int layerLenghtX)            // Creates new layer on top of previous (with higher Z-Index)
        {
            if (layerHeightY > RoomHeightY)
            {
                throw new LayerIsBiggerThanRoomException(RoomHeightY);          //Exceptions
            }
            else if (layerLenghtX > RoomLengthX)
            {
                throw new LayerIsBiggerThanRoomException(RoomLengthX);
            }
            else if (layerHeightY > RoomHeightY && layerLenghtX > RoomLengthX)
            {
                throw new LayerIsBiggerThanRoomException(RoomLengthX, RoomHeightY);
            }
            else
            {
                Layers.Add(new Layer(layerHeightY, layerLenghtX));
            }
        }

        public void RemoveRoomLayer(int numberOfLayer)                          //Removes given layer
        {
            Layers.RemoveAt(numberOfLayer);
        }

        private protected virtual void instRoom() { }
    }


    // Class that is responible for sorting all data about rooms on game level
    class Location
    {
        public readonly string[,,] LocationObjectMap;
        public readonly int NumberOfRooms;
        public readonly int SpaceBetweenRooms;
        public readonly int LocationHeightY;
        public readonly int LocationLengthX;
        public readonly int LocationLevelZ;
        public readonly List<Layer> LocationLayers;

        private Room[] rooms;
        private int minRoomHeightY;
        private int minRoomLengthX;
        private int maxRoomHeightY;
        private int maxRoomLengthX;

        public Location(int numberOfRooms, int spaceBetweenRooms, int minRoomHeightY, int maxRoomHeightY, int minRoomLengthX, int maxRoomLengthX)
        {
            this.minRoomHeightY = minRoomHeightY;
            this.maxRoomHeightY = maxRoomHeightY;
            this.minRoomLengthX = minRoomLengthX;
            this.maxRoomLengthX = maxRoomLengthX;
            NumberOfRooms = numberOfRooms;
            SpaceBetweenRooms = spaceBetweenRooms;
            rooms = new Room[numberOfRooms];

            generateRooms();

            LocationLevelZ = getMaxNumberOfLayersLevelsZ();
            LocationHeightY = getLocationHeightY();
            LocationLengthX = getLocationLengthX();
            LocationObjectMap = new string[LocationLevelZ, LocationHeightY, LocationLengthX];

            createLayerObjectMap();
        }

        public void createLayerObjectMap() //Shit is here
        {
            int spacingY = 0;
            int spacingX = 0;

            for (int roomNumber = 0; roomNumber < NumberOfRooms; roomNumber++)
            {
                if (roomNumber != 0)
                {
                    spacingX += rooms[roomNumber].RoomLengthX;
                    spacingY += rooms[roomNumber].RoomHeightY;
                }

                for (int layerNumber = 0; layerNumber < rooms[roomNumber].Layers.Count; layerNumber++)
                {
                    for (int y = 0; y < rooms[roomNumber].Layers[layerNumber].LayerHeightY; y++)
                    {
                        for (int x = 0; x < rooms[roomNumber].Layers[layerNumber].LayerLengthX; x++)
                        {
                            LocationObjectMap[layerNumber, y + spacingY, x + spacingX] = rooms[roomNumber].Layers[layerNumber].LayerObjectMap[y, x];
                        }
                    }
                }
            }
        }

        private void generateRooms() //Shit is here
        {
            for (int i = 0; i < NumberOfRooms; i++)
            {      
                    rooms[i] = new Gym(Random.Range(minRoomHeightY, maxRoomHeightY), Random.Range(minRoomLengthX, maxRoomLengthX)); 
            }
        }

        private int getLocationHeightY()
        {
            int locationHeightY = 0;

            for (int i = 0; i < NumberOfRooms; i++)
            {
                locationHeightY += rooms[i].RoomHeightY;
                locationHeightY += SpaceBetweenRooms;
            }

            return locationHeightY;
        }

        private int getLocationLengthX()
        {
            int locationLengthX = 0;

            for (int i = 0; i < NumberOfRooms; i++)
            {
                locationLengthX += rooms[i].RoomLengthX;
                locationLengthX += SpaceBetweenRooms;
            }

            return locationLengthX;
        }

        private int getMaxNumberOfLayersLevelsZ()
        {
            int layersCount = 0;                                                                                //Variable that is responsible for counting layers in rooms

            for (int roomNumber = 0; roomNumber < NumberOfRooms; roomNumber++)                                  //Looks through all rooms in map 
            {
                for (int layerInRoom = 0; layerInRoom < rooms[roomNumber].Layers.Count; layerInRoom++)          //Looks through each layer in room and counts number if room layers
                {
                    layersCount++;
                }
            }

            return layersCount;
        }

        private int generateRandomDirection() 
        {
            if (Random.Range(1, 2) == 1)
            {
                return 1;
            }
            else 
            {
                return -1;
            }
        }
    }

    //Exceptions 
    class LayerIsBiggerThanRoomException : System.Exception
    {
        public LayerIsBiggerThanRoomException(int coordinate)
            : base("Layer is bigger than room: " + "[" + coordinate + "]")
        { }

        public LayerIsBiggerThanRoomException(int coordinateX, int coordinateY)
            : base("Layer is bigger than room: " + "[" +coordinateX + ", " + coordinateY+ "]")
        { }
    }
}

/*
            TO DO LIST
1. add corners - done
2. left, right, top and down walls - done;
3. add layers - done;
4. add doors
5. add inner objects - need rework;
6. add top side of the wall - done;
7. add map generation - done;
8. add spacing;
9. rewrite Room generation - done;
*/


/*
    protected void instRoom()
        {
            //======================Layer 0======================
            AddRoomLayer(RoomHeightY - 1, RoomLengthX - 1);
            Layers[0].FillWholeLayerMap("floor");
            Layers[0].SetHorizontalLayerLine(Layers[0].LayerHeightY - 1, "wall");
            Layers[0].SetVerticalLayerLine(0, "leftWall");
            Layers[0].SetVerticalLayerLine(Layers[0].LayerLengthX - 1, "rightWall");

            //======================Layer 1======================
            AddRoomLayer(RoomHeightY - 2, RoomLengthX - 2);
            Layers[1].SetOnRandomLayerID("innerObject");

            //======================Layer 2======================
            AddRoomLayer(RoomHeightY, RoomLengthX - 1);
            //Sets top side of the room            
            Layers[2].SetHorizontalLayerLine(Layers[2].LayerHeightY - 1, "topWallBrink");
            Layers[2].SetOnUniqueLayerID(Layers[2].LayerHeightY - 1, 0, "leftTopBrinkCorner");
            Layers[2].SetOnUniqueLayerID(Layers[2].LayerHeightY - 1, Layers[2].LayerLengthX - 1, "rightTopBrinkCorner");
            //Sets bottom sides
            Layers[2].SetHorizontalLayerLine(0, "wall");
            Layers[2].SetHorizontalLayerLine(1, "topWallBrink");
            Layers[2].SetOnUniqueLayerID(1, 0, "0");                            // removes odd objects            
            Layers[2].SetOnUniqueLayerID(1, Layers[2].LayerLengthX - 1, "0");   // removes odd objects
            Layers[2].SetOnUniqueLayerID(0, 0, "leftCorner");
            Layers[2].SetOnUniqueLayerID(0, Layers[2].LayerLengthX - 1, "rightCorner");
        }       
*/