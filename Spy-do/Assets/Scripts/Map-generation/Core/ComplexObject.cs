/*
 * Sirex production code:
 * Project: Spy-Do
 * Author: Voiz (Viktor Lishchuk)
 * Email: vitya.voody@gmail.com
 * Twitter: @V0IZ_
 */

using System.Collections.Generic;
using MapGenerator.Exceptions;

namespace MapGenerator
{
    namespace Core
    {
        //Class that describes ComplexObjects(Is used in Room class)
        abstract class ComplexObject
        {
            public readonly int COHeightY;
            public readonly int COLengthX;

            public readonly List<Layer> COLayers;

            public ComplexObject(int heightY, int lengthX)
            {
                this.COLayers = new List<Layer>();
                this.COHeightY = heightY;
                this.COLengthX = lengthX;
                instCO();
            }

            protected void
                AddCOLayer(int layerHeightY,
                    int layerLenghtX) // Creates new layer on a top of the previous (with higher Z-Index)
            {
                if (layerHeightY > COHeightY)
                {
                    throw new LayerIsBiggerThanRoomException(COHeightY); //Exceptions
                }
                else if (layerLenghtX > COLengthX)
                {
                    throw new LayerIsBiggerThanRoomException(COLengthX);
                }
                else if (layerHeightY > COHeightY || layerLenghtX > COLengthX)
                {
                    throw new LayerIsBiggerThanRoomException(COLengthX, COHeightY);
                }
                else
                {
                    COLayers.Add(new Layer(layerHeightY, layerLenghtX));
                }
            }

            protected void AddCOLayer()
            {
                AddCOLayer(COHeightY, COLengthX);
            }

            abstract protected void instCO();
        }
    }
}