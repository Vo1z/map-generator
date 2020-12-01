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

namespace MapGenerator
{
    namespace Core
    {
        //Class which is responsible for collecting data for creating room 
        abstract class Room
        {
            #region Fields
            
            public readonly int RoomHeightY; //Variable that stores width of the room of Y dimension
            public readonly int RoomLengthX; //Variable that stores width of the room of X dimension

            public readonly List<Layer> RoomLayers;
            public readonly List<SExitInformation> RoomExits;
            
            public ComplexObject DefaultExitComplexObject { get; protected set; }
            public int DefaultLayerForExit { get; protected set; }

            public bool IsSpawned { get; set; } = true;

            #endregion

            protected Room(int roomHeightY, int roomLengthX)
            {
                DefaultLayerForExit = SConstants.NOT_IMPLEMENTED;
                DefaultExitComplexObject = null;

                RoomLayers = new List<Layer>();
                RoomExits = new List<SExitInformation>();
                RoomHeightY = roomHeightY;
                RoomLengthX = roomLengthX;

                CreateRoomObjectMap();

                CheckIfDefaultExitAndLayerExists();
            }

            // Creates new layer on a top of the previous (with higher Z-Index)
            protected void AddRoomLayer(int layerHeightY, int layerLenghtX)
            {
                if (layerHeightY > RoomHeightY)
                {
                    throw new LayerIsBiggerThanRoomException(RoomHeightY); //Exceptions
                }
                else if (layerLenghtX > RoomLengthX)
                {
                    throw new LayerIsBiggerThanRoomException(RoomLengthX);
                }
                else if (layerHeightY > RoomHeightY || layerLenghtX > RoomLengthX)
                {
                    throw new LayerIsBiggerThanRoomException(RoomLengthX, RoomHeightY);
                }
                else
                {
                    RoomLayers.Add(new Layer(layerHeightY, layerLenghtX));
                }
            }

            // Creates new layer on top of previous (with higher Z-Index)
            protected void AddRoomLayer()
            {
                RoomLayers.Add(new Layer(RoomHeightY, RoomLengthX));
            }

            //Removes given layer
            protected void RemoveRoomLayer(int numberOfLayer)
            {
                RoomLayers.RemoveAt(numberOfLayer);
            }

            //======ComplexObject======

            protected void SetComplexObject(ComplexObject cObj, int layerZ, int posY, int posX)
            {
                if ((cObj.COLayers.Count + layerZ) > RoomLayers.Count)
                {
                    throw new NotEnoughLayersException(RoomLayers.Count, cObj.COLayers.Count, this);
                }
                else if ((posX + cObj.COLengthX) > RoomLengthX)
                {
                    throw new NotEnoughSpaceInRoomException("X", (posX + cObj.COLengthX + 1), RoomLengthX);
                }
                else if ((posY + cObj.COHeightY) > RoomHeightY)
                {
                    throw new NotEnoughSpaceInRoomException("Y", (posY + cObj.COHeightY + 1), RoomHeightY);
                }
                else
                {
                    for (int z = 0; z < cObj.COLayers.Count; z++)
                    {
                        for (int y = 0; y < cObj.COLayers[z].LayerHeightY; y++)
                        {
                            for (int x = 0; x < cObj.COLayers[z].LayerLengthX; x++)
                            {
                                RoomLayers[layerZ + z].LayerObjectMap[posY + y, posX + x] =
                                    cObj.COLayers[z].LayerObjectMap[y, x];
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
                    foreach (SExitInformation sExit in room.RoomExits)
                    {
                        if (sExit.WallPosition == EPosition.LEFT && sExit.ExitIndexZ < RoomHeightY)
                        {
                            SetExit(DefaultExitComplexObject, DefaultLayerForExit, EPosition.RIGHT, sExit.ExitIndexZ);
                        }
                    }
                }
            }

            public void AddNeighborExitsOnLeftWall(Room room)
            {
                if (room != null)
                {
                    foreach (SExitInformation sExit in room.RoomExits)
                    {
                        if (sExit.WallPosition == EPosition.RIGHT && sExit.ExitIndexZ < RoomHeightY)
                        {
                            SetExit(DefaultExitComplexObject, DefaultLayerForExit, EPosition.LEFT, sExit.ExitIndexZ);
                        }
                    }
                }
            }
            
            protected void SetDefaultExitAndLayerZ(ComplexObject cObj, int layerZ)
            {
                this.DefaultExitComplexObject = cObj;
                this.DefaultLayerForExit = layerZ;
            }

            protected void SetExit(ComplexObject cObj, int layerZ, EPosition wallPosition, int exitIndex)
            {
                switch (wallPosition)
                {
                    case EPosition.TOP:
                        SetComplexObject(cObj, layerZ, RoomHeightY - 1, exitIndex);
                        RoomExits.Add(new SExitInformation(wallPosition, exitIndex));
                        break;
                    case EPosition.RIGHT:
                        SetComplexObject(cObj, layerZ, exitIndex, RoomLengthX - 1);
                        RoomExits.Add(new SExitInformation(wallPosition, exitIndex));
                        break;
                    case EPosition.BOTTOM:
                        SetComplexObject(cObj, layerZ, 0, exitIndex);
                        RoomExits.Add(new SExitInformation(wallPosition, exitIndex));
                        break;
                    case EPosition.LEFT:
                        SetComplexObject(cObj, layerZ, exitIndex, 0);
                        RoomExits.Add(new SExitInformation(wallPosition, exitIndex));
                        break;
                }
            }

            private void CheckIfDefaultExitAndLayerExists() //Exception
            {
                if (DefaultLayerForExit == SConstants.NOT_IMPLEMENTED)
                {
                    throw new DefaultLayerZWasNotFoundException();
                }

                if (DefaultExitComplexObject == null)
                {
                    throw new DefaultExitWasNotFoundException();
                }
            }

            //======End of methods relative to exits======
            
            //Abstract methods
            protected abstract void CreateRoomObjectMap();
        }
    }
}