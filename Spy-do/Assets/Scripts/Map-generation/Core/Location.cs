/*
 * Sirex production code:
 * Project: Spy-Do
 * Author: Voiz (Viktor Lishchuk)
 * Email: vitya.voody@gmail.com
 * Twitter: @V0IZ_
 */

using System;
using System.Collections.Generic;
using MapGenerator.Exceptions;

namespace MapGenerator
{
    namespace Core
    {

        // Class that is responsible for sorting all data about rooms on game level
        class Location
        {
            public int NumberOfRoomsInARowX { get; protected set; }
            public int NumberOfRoomRowsY { get; protected set; }

            public int LocationLayersZ
            {
                get => GetLocationNumberOfLayersZ();
                protected set => LocationLayersZ = value;
            }
            public int LocationHeightY
            {
                get => GetLocationHeightY();
                protected set => LocationHeightY = value;
            }
            public int LocationLengthX
            {
                get => GetLocationLengthX();
                protected set => LocationLengthX = value;
            }

            public string[,,] LocationObjectMap { get; protected set; }
            public int HorizontalRoomSpacingX 
            { 
                get => HorizontalRoomSpacingX;
                set
                {
                    if (value < 0)
                        throw new BadInputException("HorizontalRoomSpacingX is smaller than 0");
                    else
                        HorizontalRoomSpacingX = value;
                }
            }
            public int VerticalRoomSpacingY
            {
                get => VerticalRoomSpacingY;
                set
                {
                    if (value < 0)
                        throw new BadInputException("VerticalRoomSpacingY is smaller than 0");
                    else
                        VerticalRoomSpacingY = value;
                }
            }
            public Room[,] LocationRooms { get; set; }

            public Location(int numberOfRoomsInARowX, int numberOfRoomRowsY)
            {
            }

            //Not tested
            private String[,,] CreateObjectMap()
            {
                string[,,] objectMap = new string[LocationLayersZ, LocationHeightY, LocationLengthX];
                int locationY = 0, locationX = 0;

                //Goes through Y dimension in LocationRooms
                for (int roomNumY = 0; roomNumY < LocationRooms.GetLength(0); roomNumY++)
                {
                    //Goes through X dimension in LocationRooms
                    for (int roomNumX = 0; roomNumX < LocationRooms.GetLength(1); roomNumX++)
                    {
                        //Checks if location is supposed to be spawned
                        if (LocationRooms[roomNumY, roomNumX].IsSpawned)
                        {
                            //Goes through Z dimension in Room
                            for (int z = 0; z < LocationRooms[roomNumY, roomNumX].RoomLayers.Count; z++)
                            {
                                //Goes through Y dimension in Room
                                for (int y = 0; y < LocationRooms[roomNumY, roomNumX].RoomHeightY; y++)
                                {
                                    //Goes through X dimension in Room
                                    for (int x = 0; x < LocationRooms[roomNumY, roomNumX].RoomLengthX; x++)
                                    {
                                        LocationObjectMap[z, locationY + y, locationX + x] =
                                            LocationRooms[roomNumY, roomNumX].RoomLayers[z].LayerObjectMap[y, x];
                                    }
                                }
                            }
                        }

                        locationX += LocationRooms[roomNumY, roomNumX].RoomLengthX;
                        locationX += HorizontalRoomSpacingX;
                    }

                    locationX = 0;
                    locationY += MapGeneratorUtils.FindHighestRoomInARow(LocationRooms, roomNumY);
                    locationY += VerticalRoomSpacingY;
                }

                return objectMap;
            }

            //Not tested
            private int GetLocationNumberOfLayersZ()
            {
                int numberOfLayers = 0;

                foreach (var room in LocationRooms)
                {
                    if ((room.IsSpawned) && (numberOfLayers < room.RoomLayers.Count))
                        numberOfLayers = room.RoomLayers.Count;
                }

                return numberOfLayers;
            }

            //Not tested
            private int GetLocationHeightY()
            {
                int locationHeightY = 0;
                int maxHeightInARow = 0;

                for (int y = 0; y < LocationRooms.GetLength(0); y++)
                {
                    for (int x = 0; x < LocationRooms.GetLength(1); x++)
                    {
                        if ((LocationRooms[y, x].IsSpawned) && (maxHeightInARow < LocationRooms[y, x].RoomHeightY))
                            maxHeightInARow = LocationRooms[y, x].RoomHeightY;
                    }

                    locationHeightY += maxHeightInARow + VerticalRoomSpacingY;
                    maxHeightInARow = 0;
                }

                return locationHeightY;
            }

            //Not tested
            private int GetLocationLengthX()
            {
                int locationLengthX = 0;
                int lengthInARow = 0;

                for (int y = 0; y < LocationRooms.GetLength(0); y++)
                {
                    for (int x = 0; x < LocationRooms.GetLength(1); x++)
                    {
                        if (LocationRooms[y, x].IsSpawned)
                            lengthInARow += LocationRooms[y, x].RoomLengthX + HorizontalRoomSpacingX;
                    }

                    if (locationLengthX < lengthInARow)
                        locationLengthX = lengthInARow;

                    lengthInARow = 0;
                }

                return locationLengthX;
            }
        }
    }
}