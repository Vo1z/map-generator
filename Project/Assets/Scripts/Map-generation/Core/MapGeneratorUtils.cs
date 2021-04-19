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

namespace MapGenerator.Core
{
    ///<summary>Class that provides different tools for working with map generation structures</summary>
    public static class MapGeneratorUtils
    {
        //Tested
        ///<summary>Finds highest room in a given row</summary>
        public static int FindHighestRoomInARow(in Room[,] roomRow, int rowIndex)
        {
            if (roomRow == null)
                throw new NullReferenceException("roomRow can not be null");

            int maxHeightX = 0;

            for (int roomNumber = 0; roomNumber < roomRow.GetLength(1); roomNumber++)
                if (maxHeightX < roomRow[rowIndex, roomNumber].HeightY)
                    maxHeightX = roomRow[rowIndex, roomNumber].HeightY;

            return maxHeightX;
        }

        //Tested
        ///<summary>Resizes 3d array</summary>
        public static void Resize3DArray<T>(in T[,,] source, ref T[,,] destination)
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
        }

        //Tested
        ///<summary>Finds list with the longest length in array</summary>
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
        ///<summary>Swaps two elements between each other</summary>
        public static void Swap<T>(ref T first, ref T second)
        {
            T intermediate = first;
            first = second;
            second = intermediate;
        }

        //Tested
        ///<summary>Swaps two elements in collections</summary>
        public static void Swap<T>(IList<T> collection, int firstIndex, int secondIndex)
        {
            if (collection == null || firstIndex == secondIndex)
                return;

            T intermediate = collection[firstIndex];
            collection[firstIndex] = collection[secondIndex];
            collection[secondIndex] = intermediate;
        }
        
        //Tested
        ///<summary>Finds maximum X and Y for given coordinates</summary>
        public static (int yBound, int xBound) FindUpperPositionBound((int y, int x)[] insertionPoints)
        {
            int yBound = 0, xBound = 0;

            foreach (var insPoint in insertionPoints)
            {
                if (yBound < insPoint.y)
                    yBound = insPoint.y;

                if (xBound < insPoint.x)
                    xBound = insPoint.x;
            }

            return (yBound, xBound);
        }
        
        //Tested
        ///<summary>Mixes items in given list</summary>
        public static void Randomize<T>(IList<T> items)
        {
            Random rand = new Random();
            
            for (int i = 0; i < items.Count - 1; i++)
            {
                int j = rand.Next(i, items.Count);
                T temp = items[i];
                items[i] = items[j];
                items[j] = temp;
            }
        }
    }
}