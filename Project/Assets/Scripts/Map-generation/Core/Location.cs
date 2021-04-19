/*
 * Sirex production code:
 * Project: map-generator (Spy-Do asset)
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
    ///<summary>Class that responsible for floor generation and for sorting all data about rooms on game level</summary>
    public sealed class Location
    {
        #region Fields

        #region VariablesThatAreResponsibleForHoldingInformationAboutGeneration
        ///<summary>Array that holds map of identifiers of particular tiles</summary>
        public readonly string[,,] LocationObjectMap;
        ///<summary>Variable that responsible for holding</summary>
        private readonly Room _backgroundRoom;
        ///<summary>Array that holds rooms that have to be spawned</summary>
        private readonly Room[,] _locationRooms;
        #endregion

        #region VariablesThatAreResponsibleForStoringInformationAboutSize
        ///<summary>Field that holds actual number of layers (not including areas with empty tiles)</summary>
        public int ActualLocationLayersZ { get; private set; }
        ///<summary>Field that holds actual height (not including areas with empty tiles)</summary>
        public int ActualLocationHeightY { get; private set; }
        ///<summary>Field that holds actual length (not including areas with empty tiles)</summary>
        public int ActualLocationLengthX { get; private set; }
        ///<summary>Field that holds worst number (Total size of object map including areas that does not contain any tiles)
        ///Of layers of an ObjectMap (in number of layers)
        ///</summary>
        private readonly int _worstLocationLayersZ;
        ///<summary>
        /// Field that holds worst height (Total size of object map including areas that does not contain any tiles) of an ObjectMap (in tiles)
        ///</summary>
        private readonly int _worstLocationHeightY;
        ///<summary>
        /// Field that holds worst length (Total size of object map including areas that does not contain any tiles) of an ObjectMap (in tiles)
        ///</summary>
        private readonly int _worstLocationLengthX;
        #endregion

        #region VariablesTahtAreResponsibleForSpacing
        ///<summary>Variables that are holding start value for randomizing X-spacing</summary>
        private readonly int _randomSpacingFromX;
        ///<summary>Variables that are holding end value for randomizing X-spacing</summary>
        private readonly int _randomSpacingToX;
        ///<summary>Variables that are holding start value for randomizing Y-spacing</summary>
        private readonly int _randomSpacingFromY;
        ///<summary>Variables that are holding end value for randomizing Y-spacing</summary>
        private readonly int _randomSpacingToY;
        ///<summary>Variable that is responsible for holding value of spacing between room rows</summary>
        private readonly int _verticalRoomSpacingY;
        ///<summary>Variable that is responsible for holding value of spacing between rooms</summary>
        private readonly int _horizontalRoomSpacingX;
        ///<summary>Variable that identifies if spacing between rooms is randomized</summary>
        private readonly bool _spacingIsRandom;
        ///<summary>Variable that identifies if Y-spacing is enabled in rows </summary>
        private readonly bool _randomSpacingIsUsedInRows;
        #endregion
        
        #endregion

        ///<summary>Constructor for non-randomized spacing</summary>
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

        ///<summary>Constructor for randomized spacing</summary>
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
        
        //Tested
        ///<summary>Generates location based on given input values of the class</summary>
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
                                    catch (Exception)
                                    {
                                        // ignored
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
        ///<summary>Generates room that holds all other rooms</summary>
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
                for (int y = 0; y < backgroundRoomLayers[z].HeightY; y++)
                    for (int x = 0; x < backgroundRoomLayers[z].LengthX; x++)
                        if (String.IsNullOrEmpty(objectMapWithBackGround[z, y, x]) || objectMapWithBackGround[z, y, x].Equals("null"))
                            objectMapWithBackGround[z, y, x] = backgroundRoomLayers[z].ObjectMap[y, x];

            return objectMapWithBackGround;
        }

        //Tested
        ///<summary>Returns maximum number of layers</summary>
        private int GetLocationNumberOfLayersZ()
        {
            int numberOfLayers = 0;

            foreach (var room in _locationRooms)
                if ((room.IsSpawned) && (numberOfLayers < room.Layers.Count))
                    numberOfLayers = room.Layers.Count;

            return numberOfLayers;
        }

        //Tested
        ///<summary>Returns maximum location height</summary>
        private int GetLocationHeightY()
        {
            int locationHeightY = 0;
            int maxHeightInARow = 0;

            for (int y = 0; y < _locationRooms.GetLength(0); y++)
            {
                for (int x = 0; x < _locationRooms.GetLength(1); x++)
                    if ((_locationRooms[y, x].IsSpawned) && (maxHeightInARow < _locationRooms[y, x].HeightY))
                        maxHeightInARow = _locationRooms[y, x].HeightY + _randomSpacingToY;

                locationHeightY += maxHeightInARow + _verticalRoomSpacingY;
                maxHeightInARow = 0;
            }

            //Removes spacing after the last row
            locationHeightY -= _verticalRoomSpacingY;

            return locationHeightY;
        }

        //Tested
        ///<summary>Returns maximum location length</summary>
        private int GetLocationLengthX()
        {
            int locationLengthX = 0;
            int lengthInARow = 0;

            for (int y = 0; y < _locationRooms.GetLength(0); y++)
            {
                for (int x = 0; x < _locationRooms.GetLength(1); x++)
                    if (_locationRooms[y, x].IsSpawned)
                        lengthInARow += _locationRooms[y, x].LengthX + _horizontalRoomSpacingX + _randomSpacingToX;

                //Removes spacing after the last room in a row
                lengthInARow -= _horizontalRoomSpacingX;

                if (locationLengthX < lengthInARow)
                    locationLengthX = lengthInARow;

                lengthInARow = 0;
            }

            return locationLengthX;
        }
        
        public override string ToString() =>
            $"Number of layers Z: {_worstLocationLayersZ}\n" +
            $"Height Y: {_worstLocationHeightY}\n" +
            $"Length X: {_worstLocationLengthX}\n" +
            $"\n" +
            $"Actual number of layers Z: {ActualLocationLayersZ}\n" +
            $"Actual height Y: {ActualLocationHeightY}\n" +
            $"Actual length X: {ActualLocationLengthX}";

    }
}