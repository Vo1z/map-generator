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
    public abstract class Room
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

        abstract private protected void instRoom();
    }


    // Class that is responible for sorting all data about rooms on game level
    class Location
    {
        
        public readonly int NumberOfRooms;
        public readonly int MinRoomHeightY;
        public readonly int MaxRoomHeightY;
        public readonly int MinRoomLengthX;
        public readonly int MaxRoomLengthX;

        public readonly string[,,] LocationObjectMap;
        public readonly Room[] LocationRooms;

        public readonly int MaxLocationNumberOfLayers;
        public readonly int LocationHeightY;
        public readonly int LocationLengthX;      

        public Location(int NumberOfRooms,int MinRoomHeightY, int MaxRoomHeightY, int MinRoomLengthX, int MaxRoomLengthX) 
        {
            this.MinRoomHeightY = MinRoomHeightY;
            this.MaxRoomHeightY = MaxRoomHeightY;
            this.MinRoomLengthX = MinRoomLengthX;
            this.MaxRoomLengthX = MaxRoomLengthX;
            this.NumberOfRooms = NumberOfRooms;

            this.LocationRooms = generateLocationRooms();
            
            this.MaxLocationNumberOfLayers = calculateMaxNumberOfLayers();
            this.LocationHeightY = calculateLocationHeightY();
            this.LocationLengthX = calculateLocationLenghtX();
            
            this.LocationObjectMap = createLocationObjectMap();
        }

        //Generates all types of room in the location
        private Room[] generateLocationRooms()                                                                 
        {
            Room[] generatedRooms = new Room[NumberOfRooms];
            
            for (int i = 0; i < NumberOfRooms; i++) 
            {
                int randomRoom = Random.Range(0, 2);
                
                if (randomRoom == 0)
                    generatedRooms[i] = new Gym(Random.Range(MinRoomHeightY,MaxRoomHeightY), Random.Range(MinRoomLengthX, MaxRoomLengthX));
                else if (randomRoom == 1)
                    generatedRooms[i] = new EmtyRoom(Random.Range(MinRoomHeightY, MaxRoomHeightY), Random.Range(MinRoomLengthX, MaxRoomLengthX));
            }

            return generatedRooms;
        }

        private int calculateMaxNumberOfLayers()
        {
            int count = 0;
            int maxNumberLayers = 0;

            for (int room = 0; room < NumberOfRooms; room++) 
            {
                if (maxNumberLayers < count) 
                {
                    maxNumberLayers = count;
                }

                count = 0;

                for (int layer = 0; layer < LocationRooms[room].Layers.Count; layer++) 
                {
                    count++;
                }
            }

            return maxNumberLayers;
        }

        private int calculateLocationHeightY() 
        {
            int locationHeightY = 0;
            for (int room = 0; room < NumberOfRooms; room++)
            {                
                locationHeightY += LocationRooms[room].RoomHeightY;                
            }

            return locationHeightY;
        }

        private int calculateLocationLenghtX()
        {
            int locationLengthX = 0;
            for (int room = 0; room < NumberOfRooms; room++)
            {
                locationLengthX += LocationRooms[room].RoomLengthX;
            }

            return locationLengthX;
        }

        private string[,,] createLocationObjectMap() 
        {
            string[,,] objectMap = new string[MaxLocationNumberOfLayers, LocationHeightY, LocationLengthX];
            int spacingY = 0;
            int spacingX = 0;

            for (int room = 0; room < NumberOfRooms; room++) 
            {
                for (int layer = 0; layer < LocationRooms[room].Layers.Count; layer++) 
                {
                    for (int y = 0; y < LocationRooms[room].Layers[layer].LayerHeightY; y++) 
                    {
                        for (int x = 0; x < LocationRooms[room].Layers[layer].LayerLengthX; x++) 
                        {
                            objectMap[layer, y + spacingY, x + spacingX] = LocationRooms[room].Layers[layer].LayerObjectMap[y, x];
                        }
                    }
                }

                spacingY += LocationRooms[room].RoomHeightY;
                spacingX += LocationRooms[room].RoomLengthX;
            }

            return objectMap;
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