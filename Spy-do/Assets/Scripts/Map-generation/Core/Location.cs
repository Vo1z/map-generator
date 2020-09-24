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

            public int LocationLayersZ { get; protected set; }
            public int LocationHeightY { get; protected set; }
            public int LocationLengthX { get; protected set; }
            
            public string[,,] LocationObjectMap { get; protected set; }
            public List<Room> LocationRooms { get; set; }

            public Location(int numberOfRoomsInARowX, int numberOfRoomRowsY) => throw new NotImplementedException();

            //Not tested
            private int GetLocationNumberOfLayers()
            {
                int numberOfLocationLayers = 0;

                LocationRooms.ForEach(
                    room =>
                    {
                        if (numberOfLocationLayers < room.RoomLayers.Count)
                            numberOfLocationLayers = room.RoomLayers.Count;
                    }
                );

                return numberOfLocationLayers;
            }

            //Not tested
            private int GetLocationHeightY()
            {
                int locationHeightY = 0;
                int maxHeightInARow = 0;

                for (int roomNumber = 0; roomNumber < LocationRooms.Count; roomNumber++)
                {
                    if (maxHeightInARow < LocationRooms[roomNumber].RoomHeightY)
                        maxHeightInARow = LocationRooms[roomNumber].RoomHeightY;
                    
                    if (roomNumber % NumberOfRoomsInARowX == 0 || roomNumber == LocationRooms.Count)
                    {
                        locationHeightY += maxHeightInARow;
                        maxHeightInARow = 0;
                    }
                }

                return locationHeightY;
            }

            //Not tested
            private int GetLocationLengthX()
            {
                int locationLengthX = 0;                                                
                int lengthInARow = 0;                                                   
                                                                        
                for (int roomNumber = 0; roomNumber < LocationRooms.Count; roomNumber++)
                {
                    lengthInARow += LocationRooms[roomNumber].RoomLengthX;               
                                                                        
                    if (roomNumber % NumberOfRoomsInARowX == 0 || roomNumber == LocationRooms.Count)
                    {
                        if (locationLengthX < lengthInARow)
                            locationLengthX = lengthInARow;

                        lengthInARow = 0;
                    }                                                                   
                }
                
                return locationLengthX;
            }

            private string[,,] CreateObjectMap() => throw new NotImplementedException();
        }
    }
}