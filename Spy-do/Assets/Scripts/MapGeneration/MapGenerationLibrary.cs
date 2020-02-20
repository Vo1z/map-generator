using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MapGeneration
{
    public class Room                                     //Class which is responsible for collecting data for creating room 
    {

        public readonly int RoomHeightY;                                         //Variable that stores width of the room of Y dimension
        public readonly int RoomLengthX;                                        //Variable that stores width of the room of X dimension

        private List<Layer> layers = new List<Layer>();

        public Room(int roomHeightY, int roomLengthX)      //Constructor
        {
            this.RoomHeightY = roomHeightY - 1;
            this.RoomLengthX = roomLengthX;
            instRoom();
        }

        public void AddRoomLayer(int layerHeightY, int layerLenghtX)
        {
            if (layerHeightY > RoomHeightY)
            {
                throw new LayerIsBiggerThanRoomException(RoomHeightY);                       //Exceptions
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
                layers.Add(new Layer(layerHeightY, layerLenghtX)); // not sure
            }
        }

        public void RemoveRoomLayer(int numberOfLayer)
        {
            layers.RemoveAt(numberOfLayer); //not sure
        }

        public Layer GetRoomLayer(int layerNumber)
        {
            return layers[layerNumber];
        }

        private void instRoom() //shit is here 
        {
            //======================Layer 0======================
            AddRoomLayer(RoomHeightY - 1, RoomLengthX - 1);
            layers[0].FillWholeLayerMap("floor");
            layers[0].SetHorizontalLayerLine(layers[0].LayerHeightY - 1, "wall");
            layers[0].SetVerticalLayerLine(0, "leftWall");
            layers[0].SetVerticalLayerLine(layers[0].LayerLengthX - 1, "rightWall");

            //======================Layer 1======================
            AddRoomLayer(RoomHeightY - 1, RoomLengthX - 1);
            

            //======================Layer 2======================
            AddRoomLayer(RoomHeightY, RoomLengthX - 1);
            //Sets top side of the room
            layers[2].SetHorizontalLayerLine(layers[2].LayerHeightY - 1, "topWallBrink");
            layers[2].SetOnUniqueLayerID(layers[2].LayerHeightY - 1, 0, "leftTopBrinkCorner");
            layers[2].SetOnUniqueLayerID(layers[2].LayerHeightY - 1, layers[2].LayerLengthX - 1, "rightTopBrinkCorner");
            //Sets bottom sides
            layers[2].SetHorizontalLayerLine(0, "wall");
            layers[2].SetHorizontalLayerLine(1, "topWallBrink");
            layers[2].SetOnUniqueLayerID(1, 0, "0");
            layers[2].SetOnUniqueLayerID(1, layers[2].LayerLengthX - 1, "0");
            layers[2].SetOnUniqueLayerID(0, 0, "leftCorner");
            layers[2].SetOnUniqueLayerID(0, layers[2].LayerLengthX - 1, "rightCorner");
        }       
    }

    // Layer ============================================================================================================================

    public class Layer
    {
        public readonly string[,] LayerObjectMap;
        public readonly int LayerHeightY;
        public readonly int LayerLengthX;

        internal Layer(int heightY, int lengthX)
        {
            this.LayerObjectMap = new string[heightY + 1, lengthX];
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
    }

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
3. add layers
4. add doors
5. add inner objects
6. add top side of the wall
*/