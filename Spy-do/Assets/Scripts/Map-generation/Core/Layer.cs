/*
 * Sirex production code:
 * Project: Spy-Do
 * Author: Voiz (Viktor Lishchuk)
 * Email: vitya.voody@gmail.com
 * GitHub: Vo1z
 * Twitter: @V0IZ_
 */

using MapGenerator.Exceptions;
using UnityEngine;

namespace MapGenerator
{
    namespace Core
    {
        //Class that describes layers (Is used in Room class)
        public class Layer
        {
            public readonly string[,] ObjectMap;

            public readonly int HeightY;
            public readonly int LengthX;

            internal Layer(int heightY, int lengthX)
            {
                this.ObjectMap = new string[heightY, lengthX];
                this.HeightY = heightY;
                this.LengthX = lengthX;
            }

            public void FillWholeLayerMap(string objectName)
            {
                for (int y = 0; y < HeightY; y++)
                {
                    for (int x = 0; x < LengthX; x++)
                    {
                        ObjectMap[y, x] = objectName;
                    }
                }
            }

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

            public void SetVerticalLayerLine(int Xindex, string objectName)
            {
                for (int y = 0; y < HeightY; y++)
                {
                    for (int x = 0; x < LengthX; x++)
                    {
                        if (x == Xindex)
                        {
                            ObjectMap[y, x] = objectName;
                        }
                    }
                }
            }

            public void SetHorizontalLayerLine(int Yindex, string objectName)
            {
                for (int y = 0; y < HeightY; y++)
                {
                    for (int x = 0; x < LengthX; x++)
                    {
                        if (y == Yindex)
                        {
                            ObjectMap[y, x] = objectName;
                        }
                    }
                }
            }

            public void SetUniqueRectangle(int startY, int endY, int startX, int endX, string objectName)
            {
                if (startY > endY)
                    throw new StartPointIsSmallerThenEndPointException(startY, endY);
                if (startX > endX)
                    throw new StartPointIsSmallerThenEndPointException(startX, endX);

                for (int y = startY; y < endY; y++)
                {
                    for (int x = startX; x < endX; x++)
                    {
                        ObjectMap[y, x] = objectName;
                    }
                }
            }

            public void SetUniqueCorners(string leftTopCorner, string rightTopCorner, string leftBottomCorner,
                string rightBottomCorner)
            {
                ObjectMap[0, 0] = leftBottomCorner;
                ObjectMap[0, LengthX - 1] = rightBottomCorner;
                ObjectMap[HeightY - 1, 0] = leftTopCorner;
                ObjectMap[HeightY - 1, LengthX - 1] = rightTopCorner;
            }

            public void SetBottomLayerCorners(string objectName)
            {
                ObjectMap[0, 0] = objectName;
                ObjectMap[0, LengthX - 1] = objectName;
            }

            public void SetTopLayerCorners(string objectName)
            {
                ObjectMap[HeightY - 1, 0] = objectName;
                ObjectMap[HeightY - 1, LengthX - 1] = objectName;
            }

            public void SetOnUniqueLayerID(int y, int x, string objectName)
            {
                ObjectMap[y, x] = objectName;
            }

            public void SetOnRandomLayerID(string objectName, int probability)
            {
                for (int y = 0; y < HeightY; y++)
                {
                    for (int x = 0; x < LengthX; x++)
                    {
                        if ((Random.Range(0, probability) == 1) && x != 0 && x != LengthX - 1
                        ) //Randomizer of inner objects
                        {
                            ObjectMap[y, x] = objectName;
                        }
                    }
                }
            }

            public void SetOnUniqueObject(string[,] objectMap, string objectToFindName, string objectToPlaceName,
                int probability)
            {
                for (int y = 0; y < objectMap.GetLength(0); y++)
                {
                    for (int x = 0; x < objectMap.GetLength(1); x++)
                    {
                        if ((objectMap[y, x] == objectToFindName) && Random.Range(0, probability) == 0)
                        {
                            SetOnUniqueLayerID(y, x, objectToPlaceName);
                        }
                    }
                }
            }
        }
    }
}