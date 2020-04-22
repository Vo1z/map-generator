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

        public void SetUniqueSquare(int startY, int endY, int startX, int endX, string objectName) 
        {
            if (startY > endY)
                throw new StartPoitIsSmallerThenEndPointException(startY, endY);
            if (startX > endX)
                throw new StartPoitIsSmallerThenEndPointException(startX, endX);

            for (int y = startY; y < endY; y++) 
            {
                for (int x = startX; x < endX; x++) 
                {
                    LayerObjectMap[y, x] = objectName;
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

        public void SetOnRandomLayerID(string objectName, int probability)
        {
            for (int y = 0; y < LayerHeightY; y++)
            {
                for (int x = 0; x < LayerLengthX; x++)
                {
                    if ((Random.Range(0, probability) == 1) && x != 0 && x != LayerLengthX - 1)   //Randomizer of inner objects
                    {
                        LayerObjectMap[y, x] = objectName;
                    }
                }
            }
        }

        public void SetOnUniqueObject(string[,] objectMap, string objectToFindName, string objectToPlaceName, int probality)
        {
            for (int y = 0; y < objectMap.GetLength(0); y++)
            {
                for (int x = 0; x < objectMap.GetLength(1); x++)
                {
                    if ((objectMap[y, x] == objectToFindName) && Random.Range(0, probality) == 0)
                    {
                        SetOnUniqueLayerID(y, x, objectToPlaceName);
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

        public void AddRoomLayer()            // Creates new layer on top of previous (with higher Z-Index)
        {
            Layers.Add(new Layer(RoomHeightY, RoomLengthX));
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
        public readonly int NumberOfActualRooms;

        public readonly string[,,] LocationObjectMap;
        public readonly Room[] LocationRooms;
        public readonly int[,] LocationIdMap;

        public readonly int LocationIdMapHeightY;
        public readonly int LocationIdMapLengthX;

        public readonly int LocationLayersZ;
        public readonly int LocationHeightY;
        public readonly int LocationLengthX;

        public Location(SLocationOfRoomsInformation slori) //CONSTRUCTOR that creates default ID MAP with 1s
        {
            this.NumberOfLocationRooms = slori.sumOfAllLocationRooms;

            this.LocationIdMapHeightY = Random.Range(slori.minLocationHeightY, slori.maxLocationHeightY);
            this.LocationIdMapLengthX = Random.Range(slori.minLocationLengthX, slori.maxLocationLengthX);
            
            this.LocationRooms = generateLocationRooms(slori);
            this.LocationIdMap = createLocationIdMap(slori);

            this.NumberOfActualRooms = calculateNumberOfActualRooms();

            this.LocationLengthX = calculateLocationLengthX();
            this.LocationHeightY = calculateLocationHeightY();
            this.LocationLayersZ = calculateMaxNumberOfLayers();

            this.LocationObjectMap = createLocationObjectMap(slori);
        }

        public void Test(SLocationOfRoomsInformation slori) //DELETEME
        {
            Debug.Log(calculateNumberOfActualRooms());
        }

        //Generates all types of room in the location
        private Room[] generateLocationRooms(SLocationOfRoomsInformation slori) //REWORKED
        {
            SLocationOfRoomsInformation localSLORI = slori;

            Room[] generatedRooms = new Room[NumberOfLocationRooms];
            int room = 0;

            int i = 0;
            while (i < NumberOfLocationRooms)
            {
                int randomRoom = Random.Range(0, slori.numberOfRoomTypes);                                                    //Generates random room

                if ((randomRoom == 0) && (slori.isGym) && (localSLORI.gymQuantity <= slori.gymQuantity))                            //Checks if such a room is included in location generation
                {
                    generatedRooms[room] = new Gym(Random.Range(slori.gymMinHeightY, slori.gymMaxHeightY), Random.Range(slori.gymMinLengthX, slori.maxLocationLengthX));
                    room++;
                    localSLORI.gymQuantity--;
                    i++;
                }
                else if ((randomRoom == 1) && (slori.isOffice) && (localSLORI.officeQuantity <= slori.officeQuantity))               //Checks if such a room is included in location generation
                {
                    generatedRooms[room] = new Office(Random.Range(slori.officeMinHeightY, slori.officeMaxHeightY), Random.Range(slori.officeMinLengthX, slori.officeMaxLengthX));
                    room++;
                    localSLORI.officeQuantity--;
                    i++;
                }
                /*else if ((randomRoom == 1) && (slori.<ROOM>) && (localSLORI.<ROOM QUANTITY> <= slori.<ROOM QUANTITY>))              //Checks if such a room is included in location generation
                {
                    generatedRooms[room] = new <ROOM>(Random.Range(slori.<ROOM_MIN_Y>, slori.<ROOM_MAX_Y>), Random.Range(slori.<ROOM_MIN_X>, slori.<ROOM_MAX_X>));
                    room++;
                    localSLORI.<ROOM>--;
                    i++;
                }*/ //Add else if here
            }

            return generatedRooms;
        }

        private int[,] createLocationIdMap(SLocationOfRoomsInformation slori)//REWORKED
        {
            int[,] locationRoomMap = new int[LocationIdMapHeightY, LocationIdMapLengthX];

            for (int y = 0; y < LocationIdMapHeightY; y++)
            {
                for (int x = 0; x < LocationIdMapLengthX; x++)
                {
                    locationRoomMap[y, x] = 1;
                }
            }

            return locationRoomMap;
        }

        private int calculateNumberOfActualRooms() //ok
        {
            if (LocationIdMap.Length < NumberOfLocationRooms)
            {
                return LocationIdMap.Length;
            }
            else
            {
                return  NumberOfLocationRooms;
            }
        }

        private int calculateMaxNumberOfLayers() //ok
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

                if (LocationRooms[room] != null)
                {
                    for (int layer = 0; layer < LocationRooms[room].Layers.Count; layer++)
                    {
                        count++;
                    }
                }
            }

            if (maxNumberLayers < (count = new GeneralRoom(1,1).Layers.Count))
            {
                maxNumberLayers = count;
            }

            return maxNumberLayers;
        }

        private int calculateLocationHeightY() //REWORKED //doubts
        {
            int maxHeightInRowY = 0;
            int totalHeightY = 0;
            int roomNumber = 0;
            bool isThereAnyRoom = true;

            for (int y = 0; (y < LocationIdMapHeightY) && isThereAnyRoom; y++) 
            {
                for (int x = 0; x < LocationIdMapLengthX; x++) 
                {
                    if (maxHeightInRowY < LocationRooms[roomNumber].RoomHeightY) 
                    {
                        maxHeightInRowY = LocationRooms[roomNumber].RoomHeightY;
                    }

                    if (roomNumber < NumberOfActualRooms - 1)
                    {
                        roomNumber++;
                    }
                    else 
                    {
                        isThereAnyRoom = false;
                        break;
                    }
                }

                totalHeightY += maxHeightInRowY;

                maxHeightInRowY = 0;
            }

            return totalHeightY;
        }

        private int calculateLocationLengthX() //REWORKED //doubts
        {
            int lengthOfRowX = 0;
            int maxTotalLengthX = 0;
            int roomNumber = 0;
            bool isThereAnyRoom = true;

            for (int y = 0; (y < LocationIdMapHeightY) && isThereAnyRoom; y++) //goes through Y dimension of rooms
            {                
                for (int x = 0; x < LocationIdMapLengthX; x++) //goes through X dimension of rooms
                {
                    lengthOfRowX += LocationRooms[roomNumber].RoomLengthX; //calculates lengthX of the row

                    if (roomNumber < NumberOfActualRooms - 1)//? //room counter
                    {                        
                        roomNumber++;                        
                    }
                    else
                    {
                        isThereAnyRoom = false;
                        break;
                    }
                }

                if (maxTotalLengthX < lengthOfRowX) //finds the longest row
                {
                    maxTotalLengthX = lengthOfRowX;
                }
                lengthOfRowX = 0;
            }

            return maxTotalLengthX;
        }

        private string[,,] createLocationObjectMap(SLocationOfRoomsInformation slori)//TODO
        {
            string[,,] objectMap = createLocationDefaultRoom();//new string[LocationLayersZ, LocationHeightY, LocationLengthX];

            int spacingY = 0;
            int spacingX = 0;

            int maxHeightYInRow = 0;
            int roomNumber = 0;

            for (int idHeightY = 0; idHeightY < LocationIdMapHeightY; idHeightY++) //goes through ID MAP in Y dimension 
            {
                for (int idLengthX = 0; idLengthX < LocationIdMapLengthX; idLengthX++) //goes through ID MAP in X dimension 
                {
                    if ((LocationIdMap[idHeightY, idLengthX] == 1) && (roomNumber < NumberOfLocationRooms) ) //creates room if default value (1)
                    {
                        //finds max height in a row of rooms     
                        if (LocationRooms[roomNumber].RoomHeightY > maxHeightYInRow)
                            maxHeightYInRow = LocationRooms[roomNumber].RoomHeightY;

                        //creates room
                        for (int layerZ = 0; layerZ < LocationRooms[roomNumber].Layers.Count; layerZ++) //goes through layers (or Z dimension) in objectMap
                        {
                            for (int omY = 0; omY < LocationRooms[roomNumber].Layers[layerZ].LayerObjectMap.GetLength(0); omY++) //goes through Y dimension in objectMap
                            {
                                for (int omX = 0; omX < LocationRooms[roomNumber].Layers[layerZ].LayerObjectMap.GetLength(1); omX++) //goes through X dimension in objectMap 
                                {
                                    objectMap[layerZ, omY + spacingY, omX + spacingX] = LocationRooms[roomNumber].Layers[layerZ].LayerObjectMap[omY, omX];
                                }
                            }
                        }
                        
                        spacingX += LocationRooms[roomNumber].RoomLengthX;
                        
                        if(roomNumber < NumberOfLocationRooms)
                            roomNumber++;
                    }
                    /*else if () //for another conditions for LocationID*/

                    if (idLengthX == LocationIdMapLengthX - 1) 
                    {
                        spacingY += maxHeightYInRow;
                        spacingX = 0;
                        maxHeightYInRow = 0;
                    }
                }
            }

            return objectMap;
        }

        private string[,,] createLocationDefaultRoom()
        {
            string[,,] objectMap = new string[LocationLayersZ, LocationHeightY, LocationLengthX];
            Room defaultRoom = new GeneralRoom(LocationHeightY, LocationLengthX);

            for (int z = 0; z < defaultRoom.Layers.Count; z++) 
            {
                for (int y = 0; y < defaultRoom.Layers[z].LayerObjectMap.GetLength(0); y++) 
                {
                    for (int x = 0; x < defaultRoom.Layers[z].LayerObjectMap.GetLength(1); x++) 
                    {
                        objectMap[z, y, x] = defaultRoom.Layers[z].LayerObjectMap[y, x];
                    }
                }
            }

            return objectMap;
        }
    }

    

    //Structs
    public struct SLocationOfRoomsInformation
    {
        public int minLocationHeightY;
        public int maxLocationHeightY;
        public int minLocationLengthX;
        public int maxLocationLengthX;

        public int sumOfAllLocationRooms; //responsible for SUM of all rooms in the location
        public int numberOfRoomTypes; //responsible for number of TYPES of the rooms on location

        //=====Gym=====        
        public bool isGym;
        public int gymQuantity;

        public int gymMinHeightY;
        public int gymMaxHeightY;
        public int gymMinLengthX;
        public int gymMaxLengthX;

        //=====Office=====
        public bool isOffice;
        public int officeQuantity;

        public int officeMinHeightY;
        public int officeMaxHeightY;
        public int officeMinLengthX;
        public int officeMaxLengthX;

        //=====<ROOM>=====
        /*public bool is<ROOM>;
        public int <ROOM>Quantity;

        public int <ROOM>MinHeightY;
        public int <ROOM>MaxHeightY;
        public int <ROOM>MinLengthX;
        public int <ROOM>MaxLengthX;*/
    }

    //Exceptions 
    class LayerIsBiggerThanRoomException : System.Exception
    {
        public LayerIsBiggerThanRoomException(int coordinate)
            : base("Layer is bigger than room: " + "[" + coordinate + "]")
        { }

        public LayerIsBiggerThanRoomException(int coordinateX, int coordinateY)
            : base("Layer is bigger than room: " + "[" + coordinateX + ", " + coordinateY + "]")
        { }
    }

    class NumberOfRoomsInsideLocationIsSmalerThenNumberOfAllRoomsException : System.Exception
    {

        public NumberOfRoomsInsideLocationIsSmalerThenNumberOfAllRoomsException(int locationHeightY, int locationLengthX, int numberOfRoomsInLocation)
            : base("Number of rooms [ " + numberOfRoomsInLocation + " ] is bigger then area of location : (" + (locationHeightY * locationLengthX) + ") where locatioHeightY is [ " + locationHeightY + " ] and locationLengthX is [ " + locationLengthX + " ]")
        { }
    }

    class AnyRoomsWereNotChoosenException : System.Exception
    {
        public AnyRoomsWereNotChoosenException()
            : base("Select at least one room type")
        { }
    }

    class StartPoitIsSmallerThenEndPointException : System.Exception
    {
        public StartPoitIsSmallerThenEndPointException(int startPoint, int endPoint) : base(nameof(endPoint) + " is bigger then " + nameof(startPoint)) { }
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