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
using UnityEngine;

namespace MapGenerator
{
    namespace Core
    {
        static class MapGeneratorUtils
        {
            //Tested
            public static int FindHighestRoomInARow(in Room[,] roomRow, int rowIndex)
            {
                int maxHeightX = 0;

                for (int roomNumber = 0; roomNumber < roomRow.GetLength(1); roomNumber++)
                {
                    if (maxHeightX < roomRow[rowIndex, roomNumber].HeightY)
                        maxHeightX = roomRow[rowIndex, roomNumber].HeightY;
                }

                return maxHeightX;
            }

            public static Room[,] GenerateRoomArray(int numberOfRowsY, int numberOfRoomsInARowX, Type[] roomTypes)
            {
                throw new NotImplementedException();
            }

            //Tested
            public static T[,,] Resize3DArray<T>(in T[,,] source, ref T[,,] destination)
            {
                //Checks if destination array is not null and satisfies size of the source 
                if (destination.GetLength(0) < source.GetLength(0) ||
                    destination.GetLength(1) < source.GetLength(1) ||
                    destination.GetLength(2) < source.GetLength(2) || destination != null)
                    destination = new T[source.GetLength(0), source.GetLength(1), source.GetLength(2)];
                else
                    destination = new T[source.GetLength(0), source.GetLength(1), source.GetLength(2)];
                
                //Copies elements from source to destination
                for (int iterZ = 0; iterZ < source.GetLength(0); iterZ++)
                    for (int iterY = 0; iterY < source.GetLength(1); iterY++)
                        for (int iterX = 0; iterX < source.GetLength(2); iterX++)
                            destination[iterZ, iterY, iterX] = source[iterZ, iterY, iterX];

                return destination;
            }
        }
    }
}