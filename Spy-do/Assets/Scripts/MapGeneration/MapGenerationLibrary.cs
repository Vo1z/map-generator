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
        
        public readonly int NumberOfLocationRooms;
        public readonly int MinRoomHeightY;
        public readonly int MaxRoomHeightY;
        public readonly int MinRoomLengthX;
        public readonly int MaxRoomLengthX;

        public readonly string[,,] LocationObjectMap;
        public readonly Room[] LocationRooms;

        public readonly int MaxLocationNumberOfLayers;
        public readonly int LocationHeightY;
        public readonly int LocationLengthX;      

        public Location(SLocationOfRoomsInformation slori, int NumberOfLocationRooms,int MinRoomHeightY, int MaxRoomHeightY, int MinRoomLengthX, int MaxRoomLengthX) 
        {
            this.MinRoomHeightY = MinRoomHeightY;
            this.MaxRoomHeightY = MaxRoomHeightY;
            this.MinRoomLengthX = MinRoomLengthX;
            this.MaxRoomLengthX = MaxRoomLengthX;
            this.NumberOfLocationRooms = NumberOfLocationRooms;

            this.LocationRooms = generateLocationRooms();
            
            this.MaxLocationNumberOfLayers = calculateMaxNumberOfLayers();
            this.LocationHeightY = calculateLocationHeightY();
            this.LocationLengthX = calculateLocationLenghtX();
            
            this.LocationObjectMap = createLocationObjectMap(slori);
        }

        public void Test(SLocationOfRoomsInformation slori) //delete
        {
            int[,] testArray = createRoomPosition(slori);
            for (int y = 0; y < testArray.GetLength(0); y++)
                for (int x = 0; x < testArray.GetLength(1); x++)
                    Debug.Log(testArray[y, x] + "Y is " + y + "X is " + x);
        }

        //Generates all types of room in the location
        private Room[] generateLocationRooms()
        {
            Room[] generatedRooms = new Room[NumberOfLocationRooms];
            
            for (int i = 0; i < NumberOfLocationRooms; i++) 
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

            for (int room = 0; room < NumberOfLocationRooms; room++) 
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
            for (int room = 0; room < NumberOfLocationRooms; room++)
            {
                locationHeightY += LocationRooms[room].RoomHeightY;             
            }
            return locationHeightY;
        }

        private int calculateLocationLenghtX()
        {
            int locationLengthX = 0;
            for (int room = 0; room < NumberOfLocationRooms; room++)
            {
                locationLengthX += LocationRooms[room].RoomLengthX + 6; //Spacing  
            }

            return locationLengthX;
        }

        private string[,,] createLocationObjectMap(SLocationOfRoomsInformation slori) //rewr cw
        {
            string[,,] objectMap = new string[MaxLocationNumberOfLayers, LocationHeightY, LocationLengthX];            
            int[,] roomsPosition = createRoomPosition(slori);                                               //also known as "rp"(roomPosition)
            
            int spacingY = 0;
            int spacingX = 0;
            
            int maxHeightInRow = 0;
            int roomNumber = 0;

            bool isAnyRoomExist = true;

            for (int rowY = 0; rowY < roomsPosition.GetLength(0) && isAnyRoomExist; rowY++) 
            {
                if (checkIfArrayHasRoomsInARow(roomsPosition, rowY)) //checks if row has any rooms
                {
                    for (int rowElementX = 0; rowElementX < roomsPosition.GetLength(1); rowElementX++)
                    {
                        if (roomsPosition[rowY, rowElementX] == 1) //main condition
                        {
                            for (int layer = 0; layer < LocationRooms[roomNumber].Layers.Count; layer++) //looks through layers in the room
                            {
                                for (int y = 0; y < LocationRooms[roomNumber].Layers[layer].LayerHeightY; y++) // looks through y-axis in the room
                                {
                                    if (maxHeightInRow < y)
                                        maxHeightInRow = y;

                                    for (int x = 0; x < LocationRooms[roomNumber].Layers[layer].LayerLengthX; x++) //looks through x-axis in the room
                                    {
                                        objectMap[layer, y + spacingY, x + spacingX] = LocationRooms[roomNumber].Layers[layer].LayerObjectMap[y, x];   // inst room
                                    }
                                }
                            }

                            spacingX += LocationRooms[roomNumber].RoomLengthX; // sets X-spacing to locate next room in a row

                            if (roomNumber < NumberOfLocationRooms - 1) // sets next room
                            {
                                roomNumber++;
                            }
                            else
                            {
                                isAnyRoomExist = false;
                                break;
                            }
                        }
                        else if (roomsPosition[rowY, rowElementX] == 0 && checkIfArrayHasRoomsInARow(roomsPosition, rowY, rowElementX) == true) //main condition IF ELEMENT EQUALS 0 
                        {
                            spacingX += 4; // Random.Range(MinRoomLengthX, MaxRoomLengthX); // SPACING
                        }
                        else if (rowElementX == roomsPosition.GetLength(1) - 1) //main condition IF LAST ELEMENT IN THE ROW
                        {
                            spacingY += maxHeightInRow + 1;
                            maxHeightInRow = 0;
                            spacingX = 0;

                            if (roomNumber < NumberOfLocationRooms - 1)  // sets next room
                            {
                                roomNumber++;
                            }
                            else
                            {
                                isAnyRoomExist = false;
                                break;
                            }
                        }
                    }                  
                }
            }

            return objectMap;
        }

        private int[,] createRoomPosition(SLocationOfRoomsInformation RoomInformation) //rewr // ADD EXCEPTIONS
        {
            try
            {
                int randRowLengthX = Random.Range(RoomInformation.minLengthX, RoomInformation.maxLengthX);
                int randNumberOfRowsY = Random.Range(RoomInformation.minHeightY, RoomInformation.maxHeightY);
                int numberOfLeftRooms = NumberOfLocationRooms;

                if (numberOfLeftRooms > (randNumberOfRowsY * randRowLengthX))
                    throw new NumberOfRoomsInsideLocationIsSmalerThenNumberOfAllRoomsException(randNumberOfRowsY, randRowLengthX, numberOfLeftRooms);
                else
                {
                    int[,] roomPosition = new int[randNumberOfRowsY, randRowLengthX];

                    for (int y = 0; y < randNumberOfRowsY; y++)
                    {
                        for (int x = 0; x < randRowLengthX; x++)
                        {
                            roomPosition[y, x] = Random.Range(0, 2);
                        }
                    }
                    return roomPosition;
                }
            }
            catch (NumberOfRoomsInsideLocationIsSmalerThenNumberOfAllRoomsException exception) 
            {
                return createRoomPosition(RoomInformation);
            }
            }

        private bool checkIfArrayHasRoomsInARow(int[,] array, int rowToCheckY) 
        {
            bool isRoomPresentInARow = false;

            for (int x = 0; x < array.GetLength(1); x++)
            {
                if (array[rowToCheckY, x] == 1)
                {
                    isRoomPresentInARow = true;
                    break;
                }
                else 
                {
                    isRoomPresentInARow = false;
                }
            }

            return isRoomPresentInARow;
        }

        private bool checkIfArrayHasRoomsInARow(int[,] array, int rowToCheckY, int fromPositionToCheck)
        {
            bool isRoomPresentInARow = false;

            for (int x = fromPositionToCheck; x < array.GetLength(1); x++)
            {
                if (array[rowToCheckY, x] == 1)
                {
                    isRoomPresentInARow = true;
                    break;
                }
                else
                {
                    isRoomPresentInARow = false;
                }
            }

            return isRoomPresentInARow;
        }
    }

    //Structs
    struct SLocationOfRoomsInformation
    {
        public int minHeightY, maxHeightY, minLengthX, maxLengthX;
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

    class NumberOfRoomsInsideLocationIsSmalerThenNumberOfAllRoomsException : System.Exception
    {

        public NumberOfRoomsInsideLocationIsSmalerThenNumberOfAllRoomsException(int locationHeightY, int locationLengthX, int numberOfRoomsInLocation)
            : base("Number of rooms [ " + numberOfRoomsInLocation + " ] is bigger then area of location : (" + (locationHeightY * locationLengthX) + ") where locatioHeightY is [ " + locationHeightY + " ] and locationLengthX is [ " + locationLengthX + " ]")                                                                                 
        { }
    }
}

/*
            TO DO LIST
1. add corners - done
2. left, right, top and down walls - done;
3. add layers - done;
4. add doors
5. add inner objects - rework is needed;
6. add top side of the wall - done;
7. add map generation - done;
8. add spacing;
9. rewrite Room generation - done;
10. rewrite room;
*/