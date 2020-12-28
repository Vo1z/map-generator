/*
 * Sirex production code:
 * Project: Spy-Do
 * Author: Voiz (Viktor Lishchuk)
 * Email: vitya.voody@gmail.com
 * GitHub: Vo1z
 * Twitter: @V0IZ_
 */

using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MapGenerator.Core
{
    // Class that is responsible for sorting all data about rooms on game level
    public sealed class Location
    {
        #region Fields

        #region VariablesThatAreResponsibleForHoldingInformationAboutGeneration

        //Array that holds map of identifiers of particular tiles 
        public readonly string[,,] LocationObjectMap;

        //Variable that responsible for holding
        private readonly Room _backgroundRoom;

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
        public Location(Room backgroundRoom, Room[,] locationRooms,
            bool randomSpacingIsUsedInRows,
            int verticalRoomSpacingY, int horizontalRoomSpacingX)
        {
            _backgroundRoom = backgroundRoom;
            _locationRooms = locationRooms;

            _horizontalRoomSpacingX = horizontalRoomSpacingX;
            _verticalRoomSpacingY = verticalRoomSpacingY;
            _randomSpacingIsUsedInRows = randomSpacingIsUsedInRows;
            _spacingIsRandom = false;

            _worstLocationLayersZ = GetLocationNumberOfLayersZ();
            _worstLocationHeightY = GetLocationHeightY();
            _worstLocationLengthX = GetLocationLengthX();

            LocationObjectMap = CreateObjectMap();
        }

        //Constructor for randomized spacing
        public Location(Room backgroundRoom, Room[,] locationRooms,
            bool randomSpacingIsUsedInRows,
            int randomSpacingFromY, int randomSpacingToY,
            int randomSpacingFromX, int randomSpacingToX)
        {
            _backgroundRoom = backgroundRoom;
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
        }

        //TODO remove debug
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
                            highestHighOfTheRoomInARow - _locationRooms[roomNumY, roomNumX].HeightY > 0 &&
                            _randomSpacingIsUsedInRows
                                ? Random.Range(0,
                                    highestHighOfTheRoomInARow - _locationRooms[roomNumY, roomNumX].HeightY)
                                : 0;

                        //Goes through Z dimension in particular Room
                        for (int z = 0; z < _locationRooms[roomNumY, roomNumX].Layers.Count; z++)
                        {
                            //Goes through Y dimension in particular Room
                            for (int y = 0; y < _locationRooms[roomNumY, roomNumX].HeightY; y++)
                            {
                                //Goes through X dimension in particular Room
                                for (int x = 0; x < _locationRooms[roomNumY, roomNumX].LengthX; x++)
                                {
                                    try
                                    {
                                        objectMap[z, locationY + y + randomYSpacing, locationX + x] =
                                            _locationRooms[roomNumY, roomNumX].Layers[z].ObjectMap[y, x];
                                    }
                                    catch (Exception ex)
                                    {
                                        //TODO remove debug
                                        Debug.Log(ex.Message);
                                    }
                                }
                            }
                        }
                    }

                    locationX += _locationRooms[roomNumY, roomNumX].LengthX;
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
            ActualLocationLengthX -= _horizontalRoomSpacingX;
            ActualLocationLayersZ = objectMap.GetLength(0);
            ActualLocationHeightY = locationY;

            //Creates background room for location
            objectMap = CreateBackgroundRoom(in objectMap);

            return objectMap;
        }

        //Tested
        private string[,,] CreateBackgroundRoom(in string[,,] objectMap)
        {
            //Creates background room
            List<Layer> backgroundRoomLayers =
                _backgroundRoom.GenerateRoom(ActualLocationHeightY, ActualLocationLengthX);
            //Checks if number of layers required for creating background room smaller than actual number of layers and sets proper value
            ActualLocationLayersZ = ActualLocationLayersZ < _backgroundRoom.Layers.Count
                ? _backgroundRoom.Layers.Count
                : ActualLocationLayersZ;
            //Final object map with background room
            string[,,] objectMapWithBackGround =
                new string[ActualLocationLayersZ, ActualLocationHeightY, ActualLocationLengthX];
            //Copies objectMap to objectMapWithBackGround and resizes it to proper sizes
            MapGeneratorUtils.Resize3DArray(in objectMap, ref objectMapWithBackGround);

            //Iterates through the map and sets proper objects from background roomLayers
            for (int z = 0; z < backgroundRoomLayers.Count; z++)
            {
                for (int y = 0; y < backgroundRoomLayers[z].HeightY; y++)
                {
                    for (int x = 0; x < backgroundRoomLayers[z].LengthX; x++)
                    {
                        if (String.IsNullOrEmpty(objectMapWithBackGround[z, y, x]) ||
                            objectMapWithBackGround[z, y, x].Equals("null"))
                        {
                            objectMapWithBackGround[z, y, x] = backgroundRoomLayers[z].ObjectMap[y, x];
                        }
                    }
                }
            }

            return objectMapWithBackGround;
        }

        //Tested
        private int GetLocationNumberOfLayersZ()
        {
            int numberOfLayers = 0;

            foreach (var room in _locationRooms)
            {
                if ((room.IsSpawned) && (numberOfLayers < room.Layers.Count))
                    numberOfLayers = room.Layers.Count;
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
                    if ((_locationRooms[y, x].IsSpawned) && (maxHeightInARow < _locationRooms[y, x].HeightY))
                        maxHeightInARow = _locationRooms[y, x].HeightY + _randomSpacingToY;
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
                        lengthInARow += _locationRooms[y, x].LengthX + _horizontalRoomSpacingX + _randomSpacingToX;
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