/*
 * Sirex production code:
 * Project: map-generator (Spy-Do asset)
 * Author: Voiz (Viktor Lishchuk)
 * Email: vitya.voody@gmail.com
 * GitHub: Vo1z
 * Twitter: @V0IZ_
 */

using MapGenerator.Exceptions;
using UnityEngine;

namespace MapGenerator.Core
{
    ///<summary>Class that provides tools for managing objects' ids</summary>
    public sealed class Layer
    {
        public readonly string[,] ObjectMap;

        public readonly int HeightY;
        public readonly int LengthX;

        internal Layer(int heightY, int lengthX)
        {
            ObjectMap = new string[heightY, lengthX];
            HeightY = heightY;
            LengthX = lengthX;
        }

        ///<summary>Fills whole layer in with given id</summary>
        public void FillWholeLayerMap(string objectName)
        {
            for (int y = 0; y < HeightY; y++)
                for (int x = 0; x < LengthX; x++)
                    ObjectMap[y, x] = objectName;
        }

        ///<summary>Fills layer edges with given id</summary>
        public void SetLayerEdges(int numberOfInnerCircle, string objectName)
        {
            for (int y = 0; y < HeightY; y++)
            {
                for (int x = 0; x < LengthX; x++)
                {
                    if ((y == numberOfInnerCircle || y == (HeightY - 1) - numberOfInnerCircle) &&
                        (x >= numberOfInnerCircle && x <= (LengthX - 1) - numberOfInnerCircle))
                        ObjectMap[y, x] = objectName;

                    if ((x == numberOfInnerCircle || x == (LengthX - 1) - numberOfInnerCircle) &&
                        (y >= numberOfInnerCircle && y <= (HeightY - 1) - numberOfInnerCircle))
                        ObjectMap[y, x] = objectName;
                }
            }
        }

        ///<summary>Fills given vertical line with given id</summary>
        public void SetVerticalLayerLine(int Xindex, string objectName)
        {
            for (int y = 0; y < HeightY; y++)
                for (int x = 0; x < LengthX; x++)
                    if (x == Xindex)
                        ObjectMap[y, x] = objectName;
        }

        ///<summary>Fills given horizontal line with given id</summary>
        public void SetHorizontalLayerLine(int Yindex, string objectName)
        {
            for (int y = 0; y < HeightY; y++)
                for (int x = 0; x < LengthX; x++)
                    if (y == Yindex)
                        ObjectMap[y, x] = objectName;
        }

        ///<summary>Fills rectangle between two given points with given id</summary>
        public void SetUniqueRectangle(int startY, int endY, int startX, int endX, string objectName)
        {
            if (startY > endY)
                throw new StartPointIsSmallerThenEndPointException(startY, endY);
            if (startX > endX)
                throw new StartPointIsSmallerThenEndPointException(startX, endX);

            for (int y = startY; y < endY; y++)
                for (int x = startX; x < endX; x++)
                    ObjectMap[y, x] = objectName;
        }

        ///<summary>Sets given ids on the corners</summary>
        public void SetUniqueCorners(string leftTopCorner, string rightTopCorner, string leftBottomCorner,
            string rightBottomCorner)
        {
            ObjectMap[0, 0] = leftBottomCorner;
            ObjectMap[0, LengthX - 1] = rightBottomCorner;
            ObjectMap[HeightY - 1, 0] = leftTopCorner;
            ObjectMap[HeightY - 1, LengthX - 1] = rightTopCorner;
        }

        ///<summary>Sets given id on the bottom corners</summary>
        public void SetBottomLayerCorners(string objectName)
        {
            ObjectMap[0, 0] = objectName;
            ObjectMap[0, LengthX - 1] = objectName;
        }

        ///<summary>Sets given id on the Top corners</summary>
        public void SetTopLayerCorners(string objectName)
        {
            ObjectMap[HeightY - 1, 0] = objectName;
            ObjectMap[HeightY - 1, LengthX - 1] = objectName;
        }

        ///<summary>Sets given id on the given position</summary>
        public void SetOnUniqueLayerID(int y, int x, string objectName) => ObjectMap[y, x] = objectName;

        ///<summary>Randomly sets given id in random position with given probability</summary>
        public void SetOnRandomLayerID(string objectName, int probability)
        {
            probability = Mathf.Clamp(probability, 0, 100);
            probability = 100 / probability;
            
            for (int y = 0; y < HeightY; y++)
                for (int x = 0; x < LengthX; x++)
                    if ((Random.Range(0, probability) == 1) && x != 0 && x != LengthX - 1)
                        ObjectMap[y, x] = objectName;
        }

        ///<summary>Sets given id only upon another given id with given probability</summary>
        public void SetOnUniqueObject(string[,] objectMap, string objectToFindName, string objectToPlaceName, int probability)
        {
            probability = Mathf.Clamp(probability, 0, 100);
            probability = 100 / probability;
            
            for (int y = 0; y < objectMap.GetLength(0); y++)
                for (int x = 0; x < objectMap.GetLength(1); x++)
                    if (objectMap[y, x] == objectToFindName && Random.Range(0, probability) == 0)
                       SetOnUniqueLayerID(y, x, objectToPlaceName);
        }
    }
}