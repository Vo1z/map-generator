/*
 * Sirex production code:
 * Project: Spy-Do
 * Author: Voiz (Viktor Lishchuk)
 * Email: vitya.voody@gmail.com
 * GitHub: Vo1z
 * Twitter: @V0IZ_
 */

using System;
using MapGenerator.Core;
using MapGenerator.Exceptions;

namespace MapGenerator
{
    namespace Core
    {
        static class MapGeneratorUtils
        {
            //Not tested
            public static int FindHighestRoomInARow(in Room[,] roomRow, int rowIndex)
            {
                int maxHeightX = 0;

                for (int roomNumber = 0; roomNumber < roomRow.GetLength(1); roomNumber++)
                {
                    if (maxHeightX < roomRow[rowIndex, roomNumber].RoomHeightY)
                        maxHeightX = roomRow[rowIndex, roomNumber].RoomHeightY;
                }

                return maxHeightX;
            }

            public static Room[,] GenerateRoomArray(int numberOfRowsY, int numberOfRoomsInARowX, Type[] roomTypes)
            {
                throw new NotImplementedException();
            }
        }
    }
}