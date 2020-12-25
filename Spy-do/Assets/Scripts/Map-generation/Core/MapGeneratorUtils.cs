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

namespace MapGenerator.Core
{
    public static class MapGeneratorUtils
    {
        //Tested
        public static int FindHighestRoomInARow(in Room[,] roomRow, int rowIndex)
        {
            if (roomRow == null)
                throw new NullReferenceException();

            int maxHeightX = 0;

            for (int roomNumber = 0; roomNumber < roomRow.GetLength(1); roomNumber++)
            {
                if (maxHeightX < roomRow[rowIndex, roomNumber].HeightY)
                    maxHeightX = roomRow[rowIndex, roomNumber].HeightY;
            }

            return maxHeightX;
        }

        //Tested
        public static T[,,] Resize3DArray<T>(in T[,,] source, ref T[,,] destination)
        {
            //Checks if destination array is not null and sets proper sizes for destination if necessary
            if (destination != null)
            {
                int z = destination.GetLength(0) < source.GetLength(0)
                    ? source.GetLength(0)
                    : destination.GetLength(0);
                int y = destination.GetLength(1) < source.GetLength(1)
                    ? source.GetLength(1)
                    : destination.GetLength(1);
                int x = destination.GetLength(2) < source.GetLength(2)
                    ? source.GetLength(2)
                    : destination.GetLength(2);

                destination = new T[z, y, x];
            }
            else
                destination = new T[source.GetLength(0), source.GetLength(1), source.GetLength(2)];

            //Copies elements from source to destination
            for (int iterZ = 0; iterZ < source.GetLength(0); iterZ++)
            for (int iterY = 0; iterY < source.GetLength(1); iterY++)
            for (int iterX = 0; iterX < source.GetLength(2); iterX++)
                destination[iterZ, iterY, iterX] = source[iterZ, iterY, iterX];

            return destination;
        }

        //Tested
        //Finds list with the biggest length in array
        public static int FindLongestListSize<T>(params List<T>[] lists)
        {
            if (lists == null)
                throw new NullReferenceException();

            int longest = lists[0].Count;

            foreach (var list in lists)
                longest = longest < list.Count ? list.Count : longest;

            return longest;
        }

        //Tested
        //Swaps two elements between each other
        public static void Swap<T>(ref T first, ref T second)
        {
            T intermediate = first;
            first = second;
            second = intermediate;
        }

        //Tested
        //Swaps two elements in collections
        public static void Swap<T>(IList<T> collection, int firstIndex, int secondIndex)
        {
            if (collection == null || firstIndex == secondIndex)
                return;

            T intermediate = collection[firstIndex];
            collection[firstIndex] = collection[secondIndex];
            collection[secondIndex] = intermediate;
        }
    }
}