/*
 * Sirex production code:
 * Project: Spy-Do
 * Author: Voiz (Viktor Lishchuk)
 * Email: vitya.voody@gmail.com
 * GitHub: Vo1z
 * Twitter: @V0IZ_
 */

using System.Collections.Generic;
using MapGenerator.Exceptions;

namespace MapGenerator.Core
{
    ///<summary>
    /// Class that describes logic behind small objects that requires complex logic in generation
    /// (usually used in Room class)
    /// </summary>
    public abstract class ComplexObject
    {
        public readonly int COHeightY;
        public readonly int COLengthX;

        public readonly List<Layer> COLayers;

        public ComplexObject(int heightY, int lengthX)
        {
            COLayers = new List<Layer>();
            COHeightY = heightY;
            COLengthX = lengthX;
            InstComplexObject();
        }

        ///<summary>Creates new layer on a top of the previous (with higher Z-Index)</summary>
        protected void AddComplexObjectLayer(int layerHeightY, int layerLenghtX)
        {
            if (layerHeightY > COHeightY)
                throw new LayerIsBiggerThanRoomException(COHeightY);

            if (layerLenghtX > COLengthX)
                throw new LayerIsBiggerThanRoomException(COLengthX);

            if (layerHeightY > COHeightY || layerLenghtX > COLengthX)
                throw new LayerIsBiggerThanRoomException(COLengthX, COHeightY);
            
            COLayers.Add(new Layer(layerHeightY, layerLenghtX));
        }

        ///<summary>Adds new layer </summary>
        protected void AddCOLayer() => AddComplexObjectLayer(COHeightY, COLengthX);

        ///<summary>Method that describes generation logic</summary>
        protected abstract void InstComplexObject();
    }
}