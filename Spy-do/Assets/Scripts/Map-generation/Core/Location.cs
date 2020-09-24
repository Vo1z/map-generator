/*
 * Sirex production code:
 * Project: Spy-Do
 * Author: Voiz (Viktor Lishchuk)
 * Email: vitya.voody@gmail.com
 * Twitter: @V0IZ_
 */

using System;
using System.Collections.Generic;

namespace MapGenerator
{
    namespace Core
    {

        // Class that is responible for sorting all data about rooms on game level
        class Location
        {
            public int NumberOfRoomsInARowX { get; protected set; }
            public int NumberOfRoomRowsY { get; protected set; }

            public int LocationLayersZ { get => GetLocationNumberOfLayersZ(); protected set => LocationLayersZ = value; }
            public int LocationHeightY { get => GetLocationHeightY();  protected set => LocationHeightY = value; }
            public int LocationLengthX { get => GetLocationLengthX();  protected set => LocationLengthX = value; }
            
            public string[,,] LocationObjectMap { get; protected set; }
            public string GeneralRoomName { get; set; }
            public Room[,] LocationRooms { get; set; }

            public Location(int numberOfRoomsInARowX, int numberOfRoomRowsY)
            {
            }

            private String[,,] CreateObjectMap()
            {
                string[,,] objectMap = new string[LocationLayersZ, LocationHeightY, LocationLengthX];

                return objectMap;
            }

            //Not tested
            private int GetLocationNumberOfLayersZ()
            {
                int numberOfLayers = 0;

                foreach (var room in LocationRooms)
                {
                    if (numberOfLayers < room.RoomLayers.Count)
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
                        if (maxHeightInARow < LocationRooms[y, x].RoomHeightY)
                            maxHeightInARow = LocationRooms[y, x].RoomHeightY;
                    }

                    locationHeightY += maxHeightInARow;
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
                        lengthInARow += LocationRooms[y,x].RoomLengthX;
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