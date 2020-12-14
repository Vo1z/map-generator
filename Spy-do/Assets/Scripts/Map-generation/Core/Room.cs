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
using UnityEngine;

namespace MapGenerator
{
    namespace Core
    {
        //Class which is responsible for collecting data for creating room 
        public abstract class Room
        {
            #region Fields
            
            public int HeightY {get; private set; } //Variable that stores width of the room of Y dimension
            public int LengthX { get; private set; } //Variable that stores width of the room of X dimension

            public readonly List<Layer> Layers;
            public readonly List<SExitInformation> Exits;
            
            public ComplexObject DefaultExitComplexObject { get; protected set; }
            public int DefaultLayerForExit { get; protected set; }

            public bool IsSpawned { get; set; } = true;

            #endregion

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
            //Constructor for rooms that is expected to generate object map after being initialized 
            protected Room()
            {
                DefaultLayerForExit = SConstants.EXIT_IS_NOT_IMPLEMENTED;
                DefaultExitComplexObject = null;

                Layers = new List<Layer>();
                Exits = new List<SExitInformation>();
            }
            
            //Method that generates object map
            public List<Layer> GenerateRoom(int heightY, int lengthX)
            {
                HeightY = heightY;
                LengthX = lengthX;
                
                CreateRoomObjectMap();
                CheckIfDefaultExitAndLayerExists();

                return this.Layers;
            }
            #endregion

            // Creates new layer on a top of the previous (with higher Z-Index)
            protected void AddRoomLayer(int layerHeightY, int layerLenghtX)
            {
                if (layerHeightY > HeightY)
                {
                    throw new LayerIsBiggerThanRoomException(HeightY); //Exceptions
                }
                else if (layerLenghtX > LengthX)
                {
                    throw new LayerIsBiggerThanRoomException(LengthX);
                }
                else if (layerHeightY > HeightY || layerLenghtX > LengthX)
                {
                    throw new LayerIsBiggerThanRoomException(LengthX, HeightY);
                }
                else
                {
                    Layers.Add(new Layer(layerHeightY, layerLenghtX));
                }
            }

            // Creates new layer on top of previous (with higher Z-Index)
            protected void AddRoomLayer()
            {
                Layers.Add(new Layer(HeightY, LengthX));
            }

            //Removes given layer
            protected void RemoveRoomLayer(int numberOfLayer)
            {
                Layers.RemoveAt(numberOfLayer);
            }

            //======ComplexObject======

            protected void SetComplexObject(ComplexObject cObj, int layerZ, int posY, int posX)
            {
                if ((cObj.COLayers.Count + layerZ) > Layers.Count)
                {
                    throw new NotEnoughLayersException(Layers.Count, cObj.COLayers.Count, this);
                }
                else if ((posX + cObj.COLengthX) > LengthX)
                {
                    throw new NotEnoughSpaceInRoomException("X", (posX + cObj.COLengthX + 1), LengthX);
                }
                else if ((posY + cObj.COHeightY) > HeightY)
                {
                    throw new NotEnoughSpaceInRoomException("Y", (posY + cObj.COHeightY + 1), HeightY);
                }
                else
                {
                    for (int z = 0; z < cObj.COLayers.Count; z++)
                    {
                        for (int y = 0; y < cObj.COLayers[z].HeightY; y++)
                        {
                            for (int x = 0; x < cObj.COLayers[z].LengthX; x++)
                            {
                                Layers[layerZ + z].ObjectMap[posY + y, posX + x] =
                                    cObj.COLayers[z].ObjectMap[y, x];
                            }
                        }
                    }
                }
            }

            //======Methods relative to exits======
            
            public void AddNeighborExitsOnRightWall(Room room)
            {
                if (room != null)
                {
                    foreach (SExitInformation sExit in room.Exits)
                    {
                        if (sExit.WallPosition == ExitPosition.LEFT && sExit.ExitIndexZ < HeightY)
                        {
                            SetExit(DefaultExitComplexObject, DefaultLayerForExit, ExitPosition.RIGHT, sExit.ExitIndexZ);
                        }
                    }
                }
            }

            public void AddNeighborExitsOnLeftWall(Room room)
            {
                if (room != null)
                {
                    foreach (SExitInformation sExit in room.Exits)
                    {
                        if (sExit.WallPosition == ExitPosition.RIGHT && sExit.ExitIndexZ < HeightY)
                        {
                            SetExit(DefaultExitComplexObject, DefaultLayerForExit, ExitPosition.LEFT, sExit.ExitIndexZ);
                        }
                    }
                }
            }
            
            protected void SetDefaultExitAndLayerZ(ComplexObject cObj, int layerZ)
            {
                this.DefaultExitComplexObject = cObj;
                this.DefaultLayerForExit = layerZ;
            }

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

            private void CheckIfDefaultExitAndLayerExists() //Exception
            {
                if (DefaultLayerForExit == SConstants.EXIT_IS_NOT_IMPLEMENTED || DefaultExitComplexObject == null)
                {
                    throw new DefaultLayerZWasNotFoundException();
                }
            }

            //======End of methods relative to exits======
            
            //Abstract methods
            protected abstract void CreateRoomObjectMap();
        }
    }
}