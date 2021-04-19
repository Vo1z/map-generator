/*
 * Sirex production code:
 * Project: map-generator (Spy-Do asset)
 * Author: Voiz (Viktor Lishchuk)
 * Email: vitya.voody@gmail.com
 * GitHub: Vo1z
 * Twitter: @V0IZ_
 */

using System.Collections.Generic;
using MapGenerator.DataTypes;
using MapGenerator.Exceptions;

namespace MapGenerator.Core
{
    ///<summary>Class which represents room and tools for creating it</summary>
    public abstract class Room
    {
        #region Fields
        ///<summary>Variable that stores width of the room of Y dimension</summary>
        public int HeightY { get; private set; }

        ///<summary>Variable that stores width of the room of X dimension</summary>
        public int LengthX { get; private set; }

        ///<summary>Layers of the room</summary>
        public readonly List<Layer> Layers;
        ///<summary>Exits of the room</summary>
        public readonly List<SExitInformation> Exits;

        ///<summary>Complex object that will considered as default exit in case there is no specified in generation logic</summary>
        public ComplexObject DefaultExitComplexObject { get; protected set; }
        ///<summary>Number of layer where exit will be set</summary>
        public int DefaultLayerForExit { get; protected set; }

        ///<summary>Variable that describes if room will be spawned</summary>
        public bool IsSpawned { get; set; } = true;
        #endregion

        ///<summary>Creates instance of the room with given sizes</summary>
        protected Room(int heightY, int lengthX)
        {
            DefaultLayerForExit = SConstants.EXIT_IS_NOT_IMPLEMENTED;
            DefaultExitComplexObject = null;

            Layers = new List<Layer>();
            Exits = new List<SExitInformation>();
            HeightY = heightY;
            LengthX = lengthX;

            CreateRoomObjectMap();

            CheckIfDefaultExitAndLayerExists();
        }

        #region LazyGenerationConstructorAndMethod
        ///<summary>Constructor for rooms that is expected to generate object map after being initialized</summary>
        protected Room()
        {
            DefaultLayerForExit = SConstants.EXIT_IS_NOT_IMPLEMENTED;
            DefaultExitComplexObject = null;

            Layers = new List<Layer>();
            Exits = new List<SExitInformation>();
        }

        ///<summary>Method that generates object map</summary>
        public List<Layer> GenerateRoom(int heightY, int lengthX)
        {
            HeightY = heightY;
            LengthX = lengthX;

            CreateRoomObjectMap();
            CheckIfDefaultExitAndLayerExists();

            return Layers;
        }
        #endregion

        //Tested
        ///<summary>Creates new layer on top of previous (with higher Z-Index)</summary>
        protected void AddRoomLayer() => Layers.Add(new Layer(HeightY, LengthX));

        //Tested
        ///<summary>Removes given layer</summary>
        protected void RemoveRoomLayer(int numberOfLayer) => Layers.RemoveAt(numberOfLayer);

        //======ComplexObject======

        //Tested
        ///<summary>Sets complex object on specific position</summary>
        protected void SetComplexObject(ComplexObject cObj, int layerZ, int posY, int posX)
        {
            if ((cObj.COLayers.Count + layerZ) > Layers.Count)
                throw new NotEnoughLayersException(Layers.Count, cObj.COLayers.Count, this);
            if ((posX + cObj.COLengthX) > LengthX)
                throw new NotEnoughSpaceInRoomException("X", (posX + cObj.COLengthX + 1), LengthX);
            if ((posY + cObj.COHeightY) > HeightY)
                throw new NotEnoughSpaceInRoomException("Y", (posY + cObj.COHeightY + 1), HeightY);

            for (int z = 0; z < cObj.COLayers.Count; z++)
                for (int y = 0; y < cObj.COLayers[z].HeightY; y++)
                    for (int x = 0; x < cObj.COLayers[z].LengthX; x++)
                        Layers[layerZ + z].ObjectMap[posY + y, posX + x] =
                            cObj.COLayers[z].ObjectMap[y, x];
        }

        //======Methods relative to exits======

        //Tested
        ///<summary>Adds exit on corresponded opposite wall</summary>
        public void AddNeighborExitsOnRightWall(Room room)
        {
            if (room != null)
                foreach (SExitInformation sExit in room.Exits)
                    if (sExit.WallPosition == ExitPosition.LEFT && sExit.ExitIndexZ < HeightY)
                        SetExit(DefaultExitComplexObject, DefaultLayerForExit, ExitPosition.RIGHT, sExit.ExitIndexZ);
        }

        //Tested
        ///<summary>Adds exit on corresponded opposite wall</summary>
        public void AddNeighborExitsOnLeftWall(Room room)
        {
            if (room != null)
                foreach (SExitInformation sExit in room.Exits)
                    if (sExit.WallPosition == ExitPosition.RIGHT && sExit.ExitIndexZ < HeightY)
                        SetExit(DefaultExitComplexObject, DefaultLayerForExit, ExitPosition.LEFT, sExit.ExitIndexZ);
        }

        //Tested
        ///<summary>Sets default exit(complex object) and default layer for it</summary>
        protected void SetDefaultExitAndLayerZ(ComplexObject cObj, int layerZ)
        {
            DefaultExitComplexObject = cObj;
            DefaultLayerForExit = layerZ;
        }

        //Tested
        protected void SetExit(ComplexObject cObj, int layerZ, ExitPosition wallPosition, int exitIndex)
        {
            switch (wallPosition)
            {
                case ExitPosition.TOP:
                    SetComplexObject(cObj, layerZ, HeightY - 1, exitIndex);
                    Exits.Add(new SExitInformation(wallPosition, exitIndex));
                    break;
                case ExitPosition.RIGHT:
                    SetComplexObject(cObj, layerZ, exitIndex, LengthX - 1);
                    Exits.Add(new SExitInformation(wallPosition, exitIndex));
                    break;
                case ExitPosition.BOTTOM:
                    SetComplexObject(cObj, layerZ, 0, exitIndex);
                    Exits.Add(new SExitInformation(wallPosition, exitIndex));
                    break;
                case ExitPosition.LEFT:
                    SetComplexObject(cObj, layerZ, exitIndex, 0);
                    Exits.Add(new SExitInformation(wallPosition, exitIndex));
                    break;
            }
        }

        //Tested
        ///<summary>Checks if all conditions are satisfied for exits</summary>
        private void CheckIfDefaultExitAndLayerExists()
        {
            if (DefaultLayerForExit == SConstants.EXIT_IS_NOT_IMPLEMENTED || DefaultExitComplexObject == null)
                throw new DefaultLayerZWasNotFoundException();
        }

        //======End of methods relative to exits======

        ///<summary>Method that describes room generation logic</summary>
        protected abstract void CreateRoomObjectMap();
    }
}