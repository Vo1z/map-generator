/*
 * Sirex production code:
 * Project: Spy-Do
 * Author: Voiz (Viktor Lishchuk)
 * Email: vitya.voody@gmail.com
 * Twitter: @V0IZ_
 */
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace MapGenerator
{
    //Class that describes ComplexObjects(Is used in Room class)
    abstract class ComplexObject
    {
        public readonly int COHeightY;
        public readonly int COLengthX;

        public readonly List<Layer> COLayers;

        public ComplexObject(int heightY, int lengthX) 
        {
            this.COLayers = new List<Layer>();
            this.COHeightY = heightY;
            this.COLengthX = lengthX;
            instCO();
        }

        protected void AddCOLayer(int layerHeightY, int layerLenghtX)            // Creates new layer on a top of the previous (with higher Z-Index)
        {
            if (layerHeightY > COHeightY)
            {
                throw new LayerIsBiggerThanRoomException(COHeightY);               //Exceptions
            }
            else if (layerLenghtX > COLengthX)
            {
                throw new LayerIsBiggerThanRoomException(COLengthX);
            }
            else if (layerHeightY > COHeightY || layerLenghtX > COLengthX)
            {
                throw new LayerIsBiggerThanRoomException(COLengthX, COHeightY);
            }
            else
            {
                COLayers.Add(new Layer(layerHeightY, layerLenghtX));
            }
        }

        protected void AddCOLayer() 
        {
            AddCOLayer(COHeightY, COLengthX);
        }

        abstract protected void instCO();
    }

    //Class that describes layers (Is used in Room class)
    class Layer
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

        public void SetUniqueRectangle(int startY, int endY, int startX, int endX, string objectName)
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

        public void SetOnUniqueObject(string[,] objectMap, string objectToFindName, string objectToPlaceName, int probability)
        {
            for (int y = 0; y < objectMap.GetLength(0); y++)
            {
                for (int x = 0; x < objectMap.GetLength(1); x++)
                {
                    if ((objectMap[y, x] == objectToFindName) && Random.Range(0, probability) == 0)
                    {
                        SetOnUniqueLayerID(y, x, objectToPlaceName);
                    }
                }
            }
        }
    }
    //Class which is responsible for collecting data for creating room 
    abstract class Room
    {
        public readonly int RoomHeightY;                                        //Variable that stores width of the room of Y dimension
        public readonly int RoomLengthX;                                        //Variable that stores width of the room of X dimension

        public readonly List<Layer> RoomLayers;
        public readonly List<SExitInformation> RoomExits;

        public ComplexObject DefaultExitComplexObject { get; protected set; }
        public int DefaultLayerForExit { get; protected set; }
        
        public Room(int roomHeightY, int roomLengthX)                           //Constructor
        {
            this.DefaultLayerForExit = CONSTANTS.NOT_IMPLEMENTED;
            this.DefaultExitComplexObject = null;

            this.RoomLayers = new List<Layer>();
            this.RoomExits = new List<SExitInformation>();
            this.RoomHeightY = roomHeightY;
            this.RoomLengthX = roomLengthX;
            
            this.instRoom();

            this.checkIfDefaultExitAndLayerExists();
        }

        public void AddNeighborExitsOnRightWall(Room room)
        {
            if (room != null)
            {
                foreach (SExitInformation sExit in room.RoomExits) 
                {
                    if (sExit.WallPosition == EPosition.LEFT && sExit.ExitIndexZ < RoomHeightY) 
                    {
                        SetExit(DefaultExitComplexObject, DefaultLayerForExit, EPosition.RIGHT, sExit.ExitIndexZ);
                    }
                }
            }
        }   
        public void AddNeighborExitsOnLeftWall(Room room)
        {
            if (room != null)
            {
                foreach (SExitInformation sExit in room.RoomExits) 
                {
                    if (sExit.WallPosition == EPosition.RIGHT && sExit.ExitIndexZ < RoomHeightY) 
                    {
                        SetExit(DefaultExitComplexObject, DefaultLayerForExit, EPosition.LEFT, sExit.ExitIndexZ);
                    }                  
                }
            }
        }

        protected void AddRoomLayer(int layerHeightY, int layerLenghtX)            // Creates new layer on a top of the previous (with higher Z-Index)
        {
            if (layerHeightY > RoomHeightY)
            {
                throw new LayerIsBiggerThanRoomException(RoomHeightY);          //Exceptions
            }
            else if (layerLenghtX > RoomLengthX)
            {
                throw new LayerIsBiggerThanRoomException(RoomLengthX);
            }
            else if (layerHeightY > RoomHeightY || layerLenghtX > RoomLengthX)
            {
                throw new LayerIsBiggerThanRoomException(RoomLengthX, RoomHeightY);
            }
            else
            {
                RoomLayers.Add(new Layer(layerHeightY, layerLenghtX));
            }
        }

        protected void AddRoomLayer()                                              // Creates new layer on top of previous (with higher Z-Index)
        {
            RoomLayers.Add(new Layer(RoomHeightY, RoomLengthX));
        }

        protected void RemoveRoomLayer(int numberOfLayer)                          //Removes given layer
        {
            RoomLayers.RemoveAt(numberOfLayer);
        }

        //======CO======
        protected void SetComplexObject(ComplexObject cObj, int layerZ, int posY, int posX) 
        {
            if ((cObj.COLayers.Count + layerZ) > RoomLayers.Count)
            {
                throw new NotEnoughLayersException(RoomLayers.Count, cObj.COLayers.Count, this);
            }
            else if ((posX + cObj.COLengthX) > RoomLengthX)
            {
                throw new NotEnoughSpaceInRoomException("X", (posX + cObj.COLengthX + 1), RoomLengthX);
            }
            else if ((posY + cObj.COHeightY) > RoomHeightY) 
            {
                throw new NotEnoughSpaceInRoomException("Y", (posY + cObj.COHeightY + 1), RoomHeightY);
            }
            else
            {
                for (int z = 0; z < cObj.COLayers.Count; z++)
                {
                    for (int y = 0; y < cObj.COLayers[z].LayerHeightY; y++)
                    {
                        for (int x = 0; x < cObj.COLayers[z].LayerLengthX; x++)
                        {
                            RoomLayers[layerZ + z].LayerObjectMap[posY + y, posX + x] = cObj.COLayers[z].LayerObjectMap[y, x];
                        }
                    }
                }
            }
        }

        //======Exit======
        protected void SetDefaultExitAndLayerZ(ComplexObject cObj, int layerZ) 
        {
            this.DefaultExitComplexObject = cObj;
            this.DefaultLayerForExit = layerZ;
        }

        protected void SetExit(ComplexObject cObj, int layerZ, EPosition wallPosition, int exitIndex) 
        {
            switch (wallPosition) 
            {
                case EPosition.TOP:
                    SetComplexObject(cObj, layerZ, RoomHeightY - 1, exitIndex);
                    RoomExits.Add(new SExitInformation(wallPosition, exitIndex));
                    break;
                case EPosition.RIGHT:
                    SetComplexObject(cObj, layerZ, exitIndex, RoomLengthX - 1);
                    RoomExits.Add(new SExitInformation(wallPosition, exitIndex));
                    break;
                case EPosition.BOTTOM:
                    SetComplexObject(cObj, layerZ, 0, exitIndex);
                    RoomExits.Add(new SExitInformation(wallPosition, exitIndex));
                    break;
                case EPosition.LEFT:
                    SetComplexObject(cObj, layerZ, exitIndex, 0);
                    RoomExits.Add(new SExitInformation(wallPosition, exitIndex));
                    break;              
            }
        }

        private void checkIfDefaultExitAndLayerExists()                             //Exception
        {
            if (DefaultLayerForExit == CONSTANTS.NOT_IMPLEMENTED)
            {
                throw new DefaultLayerZWasNotFoundException();
            }
            if (DefaultExitComplexObject == null) 
            {
                throw new DefaultExitWasNotFoundException();
            }
        }

        abstract protected void instRoom();
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

            this.LocationRooms = setProperExitsToLocationRooms(generateLocationRooms(slori));
            this.LocationIdMap = createLocationIdMap(slori);

            this.NumberOfActualRooms = calculateNumberOfActualRooms();

            this.LocationLengthX = calculateLocationLengthX();
            this.LocationHeightY = calculateLocationHeightY();
            this.LocationLayersZ = calculateMaxNumberOfLayers();

            this.LocationObjectMap = createLocationObjectMap(slori);
        }

        public void Test(SLocationOfRoomsInformation slori) //DELETEME
        {
            // Debug.Log(calculateNumberOfActualRooms());
        }

        //Generates all types of room in the location
        private Room[] generateLocationRooms(SLocationOfRoomsInformation slori)
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
                else if ((randomRoom == 2) && (slori.isEmptySpace) && (localSLORI.emptySpaceQuantity <= slori.emptySpaceQuantity))
                {
                    generatedRooms[room] = new EmptySpace(Random.Range(slori.emptySpaceMinHeightY, slori.emptySpaceMaxHeightY), Random.Range(slori.emptySpaceMinLengthX, slori.emptySpaceMaxLengthX));
                    room++;
                    localSLORI.emptySpaceQuantity--;
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

        private Room[] setProperExitsToLocationRooms(Room[] locationRooms)
        {
            Room[] roomsWithExits = locationRooms;

            for (int roomNumber = 0; roomNumber < locationRooms.Length; roomNumber++)           //goes through left walls
            {
                if (roomNumber + 1 < locationRooms.Length)
                {
                    roomsWithExits[roomNumber].AddNeighborExitsOnRightWall(roomsWithExits[roomNumber + 1]);
                }
            }

            for (int rightWalls = locationRooms.Length - 1; rightWalls >= 0; rightWalls--)
            {
                if (rightWalls - 1 >= 0)
                {
                    roomsWithExits[rightWalls].AddNeighborExitsOnRightWall(locationRooms[rightWalls - 1]);
                }
            }

            return roomsWithExits;
        }

        private int[,] createLocationIdMap(SLocationOfRoomsInformation slori)
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

        private int calculateNumberOfActualRooms()
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

                if (LocationRooms[room] != null)
                {
                    for (int layer = 0; layer < LocationRooms[room].RoomLayers.Count; layer++)
                    {
                        count++;
                    }
                }
            }

            if (maxNumberLayers < (count = new GeneralRoom(1,1).RoomLayers.Count))
            {
                maxNumberLayers = count;
            }

            return maxNumberLayers;
        }

        private int calculateLocationHeightY()
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

        private int calculateLocationLengthX()
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

        private string[,,] createLocationObjectMap(SLocationOfRoomsInformation slori)
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
                        for (int layerZ = 0; layerZ < LocationRooms[roomNumber].RoomLayers.Count; layerZ++) //goes through layers (or Z dimension) in objectMap
                        {
                            for (int omY = 0; omY < LocationRooms[roomNumber].RoomLayers[layerZ].LayerObjectMap.GetLength(0); omY++) //goes through Y dimension in objectMap
                            {
                                for (int omX = 0; omX < LocationRooms[roomNumber].RoomLayers[layerZ].LayerObjectMap.GetLength(1); omX++) //goes through X dimension in objectMap 
                                {
                                    objectMap[layerZ, omY + spacingY, omX + spacingX] = LocationRooms[roomNumber].RoomLayers[layerZ].LayerObjectMap[omY, omX];
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

            //Adds DefaultRoom strings to the bottom of the Generated room
            GeneralRoom defaultRoom = new GeneralRoom(LocationHeightY, LocationLengthX); 
            for (int x = 0; x < objectMap.GetLength(2); x++) 
            {
                objectMap[objectMap.GetLength(0) - 1, 0, x] = "GRBottomWall";               //SETS BUTTOM WALL TO PREVENT BUGS WITH EMPTY SPACE (has to fixed in the future)
            }

            return objectMap;
        }

        private string[,,] createLocationDefaultRoom()
        {
            string[,,] objectMap = new string[LocationLayersZ, LocationHeightY, LocationLengthX];
            Room defaultRoom = new GeneralRoom(LocationHeightY, LocationLengthX);                   //Creates general rooms

            for (int z = 0; z < defaultRoom.RoomLayers.Count; z++) 
            {
                for (int y = 0; y < defaultRoom.RoomLayers[z].LayerObjectMap.GetLength(0); y++) 
                {
                    for (int x = 0; x < defaultRoom.RoomLayers[z].LayerObjectMap.GetLength(1); x++) 
                    {
                        objectMap[z, y, x] = defaultRoom.RoomLayers[z].LayerObjectMap[y, x];
                    }
                }
            }

            return objectMap;
        }
    }
}