/*
 * Sirex production code:
 * Project: Spy-Do
 * Author: Voiz (Viktor Lishchuk)
 * Email: vitya.voody@gmail.com
 * GitHub: Vo1z
 * Twitter: @V0IZ_
 */

using System;
using MapGenerator.Exceptions;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MapGenerator
{
    namespace Core
    {
        // Class that is responsible for sorting all data about rooms on game level
        class Location
        {
            #region Fields
            
            #region VariablesThatAreResponsibleForHoldingInformationAboutGeneration
            //Array that holds map of identifiers of particular tiles 
            public readonly string[,,] LocationObjectMap;
            //Array that holds rooms that have to be spawned
            private readonly Room[,] _locationRooms;
            #endregion

            #region VariablesThatAreResponsibleForStoringInformationAboutSize
            //Field that holds actual number of layers (not including areas with empty tiles)
            public int ActualLocationLayersZ { get; private set; }
            //Field that holds actual height (not including areas with empty tiles)
            public int ActualLocationHeightY { get; private set; }
            //Field that holds actual length (not including areas with empty tiles)
            public int ActualLocationLengthX { get; private set; }
            //Field that holds worst number (Total size of object map including areas that does not contain any tiles)
            //Of layers of an ObjectMap (in number of layers)
            private readonly int _worstLocationLayersZ;
            //Field that holds worst height (Total size of object map including areas that does not contain any tiles) of an ObjectMap (in tiles)
            private readonly int _worstLocationHeightY;
            //Field that holds worst length (Total size of object map including areas that does not contain any tiles) of an ObjectMap (in tiles)
            private readonly int _worstLocationLengthX;
            #endregion
            
            #region VariablesTahtAreResponsibleForSpacing
            //Variables that are holding start value for randomizing X-spacing
            private readonly int _randomSpacingFromX;
            //Variables that are holding end value for randomizing X-spacing
            private readonly int _randomSpacingToX;
            //Variables that are holding start value for randomizing Y-spacing
            private readonly int _randomSpacingFromY;
            //Variables that are holding end value for randomizing Y-spacing
            private readonly int _randomSpacingToY;
            //Variable that is responsible for holding value of spacing between room rows
            private readonly int _verticalRoomSpacingY;
            //Variable that is responsible for holding value of spacing between rooms
            private readonly int _horizontalRoomSpacingX;
            //Variable that identifies if spacing between rooms is randomized
            private readonly bool _spacingIsRandom;
            //Variable that identifies if Y-spacing is enabled in rows 
            private readonly bool _randomSpacingIsUsedInRows;
            #endregion
            
            #endregion

            //Constructor for non-randomized spacing
            public Location(Room[,] locationRooms, bool randomSpacingIsUsedInRows, int verticalRoomSpacingY, int horizontalRoomSpacingX)
            {
                //Todo replace debug
                Debug.Log("Entering Constructor");

                _locationRooms = locationRooms;

                _horizontalRoomSpacingX = horizontalRoomSpacingX;
                _verticalRoomSpacingY = verticalRoomSpacingY;
                _randomSpacingIsUsedInRows = randomSpacingIsUsedInRows;
                _spacingIsRandom = false;

                _worstLocationLayersZ = GetLocationNumberOfLayersZ();
                _worstLocationHeightY = GetLocationHeightY();
                _worstLocationLengthX = GetLocationLengthX();

                LocationObjectMap = CreateObjectMap();

                //Todo replace debug
                Test();
            }

            //Constructor for randomized spacing
            public Location(Room[,] locationRooms,
                bool randomSpacingIsUsedInRows,
                int randomSpacingFromY, int randomSpacingToY,
                int randomSpacingFromX, int randomSpacingToX)
            {
                //Todo replace debug
                Debug.Log("Entering Constructor");

                _locationRooms = locationRooms;

                _randomSpacingFromY = randomSpacingFromY;
                _randomSpacingToY = randomSpacingToY;
                _randomSpacingFromX = randomSpacingFromX;
                _randomSpacingToX = randomSpacingToX;
                _randomSpacingIsUsedInRows = randomSpacingIsUsedInRows;
                _spacingIsRandom = true;

                _worstLocationLayersZ = GetLocationNumberOfLayersZ();
                _worstLocationHeightY = GetLocationHeightY();
                _worstLocationLengthX = GetLocationLengthX();

                LocationObjectMap = CreateObjectMap();

                //Todo replace debug
                Test();
            }

            public void Test()
            {
                Debug.Log("Number of layers Z: " + _worstLocationLayersZ);
                Debug.Log("Height Y: " + _worstLocationHeightY);
                Debug.Log("Length X: " + _worstLocationLengthX);
                
                Debug.Log("Actual number of layers Z: " + ActualLocationLayersZ);
                Debug.Log("Actual height Y: " + ActualLocationHeightY);
                Debug.Log("Actual length X: " + ActualLocationLengthX);
            }

            //Tested
            private string[,,] CreateObjectMap()
            {
                string[,,] objectMap = new string[_worstLocationLayersZ, _worstLocationHeightY, _worstLocationLengthX];
                int locationY = 0, locationX = 0;

                //Goes through Y dimension in Rooms array
                for (int roomNumY = 0; roomNumY < _locationRooms.GetLength(0); roomNumY++)
                {
                    int highestHighOfTheRoomInARow = MapGeneratorUtils.FindHighestRoomInARow(_locationRooms, roomNumY);

                    //Goes through X dimension in Rooms array
                    for (int roomNumX = 0; roomNumX < _locationRooms.GetLength(1); roomNumX++)
                    {
                        //Checks if room is supposed to be spawned
                        if (_locationRooms[roomNumY, roomNumX].IsSpawned)
                        {
                            //Variable that is responsible for computing random available spacing between bottom of the row and
                            //the top of the row for rooms that is smaller than the highest room in the row 
                            //In assigning it checks if given spacing is bigger than 0
                            //and if space is randomized comparing to _randomSpacingIsUsedInRows
                            int randomYSpacing =
                                highestHighOfTheRoomInARow - _locationRooms[roomNumY, roomNumX].RoomHeightY > 0 && _randomSpacingIsUsedInRows
                                    ? Random.Range(0, highestHighOfTheRoomInARow - _locationRooms[roomNumY, roomNumX].RoomHeightY)
                                    : 0;

                            //Goes through Z dimension in particular Room
                            for (int z = 0; z < _locationRooms[roomNumY, roomNumX].RoomLayers.Count; z++)
                            {
                                //Goes through Y dimension in particular Room
                                for (int y = 0; y < _locationRooms[roomNumY, roomNumX].RoomHeightY; y++)
                                {
                                    //Goes through X dimension in particular Room
                                    for (int x = 0; x < _locationRooms[roomNumY, roomNumX].RoomLengthX; x++)
                                    {
                                        objectMap[z, locationY + y + randomYSpacing, locationX + x] =
                                            _locationRooms[roomNumY, roomNumX].RoomLayers[z].LayerObjectMap[y, x];
                                    }
                                }
                            }
                        }

                        locationX += _locationRooms[roomNumY, roomNumX].RoomLengthX;
                        //Checks if spacing is randomized and sets proper value of X-spacing 
                        if (_spacingIsRandom)
                            locationX += Random.Range(_randomSpacingFromX, _randomSpacingToX);
                        else
                            locationX += _horizontalRoomSpacingX;
                    }
                    
                    //Block tht assigns actual sizes of the object map
                    if (ActualLocationLengthX < locationX)
                        ActualLocationLengthX = locationX;
                    
                    locationX = 0;
                    locationY += highestHighOfTheRoomInARow;
                    //Checks if spacing is randomized and sets proper value of Y-spacing 
                    if (_spacingIsRandom)
                        locationY += Random.Range(_randomSpacingFromY, _randomSpacingToY);
                    else
                        locationY += _verticalRoomSpacingY;
                }

                //Block tht assigns actual sizes of the object map
                ActualLocationLayersZ = objectMap.GetLength(0);
                ActualLocationHeightY = locationY;

                return objectMap;
            }
            
            //Tested
            private int GetLocationNumberOfLayersZ()
            {
                int numberOfLayers = 0;

                foreach (var room in _locationRooms)
                {
                    if ((room.IsSpawned) && (numberOfLayers < room.RoomLayers.Count))
                        numberOfLayers = room.RoomLayers.Count;
                }

                return numberOfLayers;
            }

            //Tested
            private int GetLocationHeightY()
            {
                int locationHeightY = 0;
                int maxHeightInARow = 0;

                for (int y = 0; y < _locationRooms.GetLength(0); y++)
                {
                    for (int x = 0; x < _locationRooms.GetLength(1); x++)
                    {
                        if ((_locationRooms[y, x].IsSpawned) && (maxHeightInARow < _locationRooms[y, x].RoomHeightY))
                            maxHeightInARow = _locationRooms[y, x].RoomHeightY + _randomSpacingToY;
                    }

                    locationHeightY += maxHeightInARow + _verticalRoomSpacingY;
                    maxHeightInARow = 0;
                }

                //Removes spacing after the last row
                locationHeightY -= _verticalRoomSpacingY;

                return locationHeightY;
            }

            //Tested
            private int GetLocationLengthX()
            {
                int locationLengthX = 0;
                int lengthInARow = 0;

                for (int y = 0; y < _locationRooms.GetLength(0); y++)
                {
                    for (int x = 0; x < _locationRooms.GetLength(1); x++)
                    {
                        if (_locationRooms[y, x].IsSpawned)
                            lengthInARow += _locationRooms[y, x].RoomLengthX + _horizontalRoomSpacingX + _randomSpacingToX;
                    }

                    //Removes spacing after the last room in a row
                    lengthInARow -= _horizontalRoomSpacingX;

                    if (locationLengthX < lengthInARow)
                        locationLengthX = lengthInARow;

                    lengthInARow = 0;
                }

                return locationLengthX;
            }
        }
    }
}