using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MapGeneration
{
    class Gym : Room
    {
        public Gym(int roomHeightY, int roomLengthX) : base(roomHeightY, roomLengthX) { }

        private protected override void instRoom()
        {
            //========================Layer 0========================
            AddRoomLayer(RoomHeightY - 1, RoomLengthX);
            Layers[0].FillWholeLayerMap("floor");
            Layers[0].SetVerticalLayerLine(0, "null");
            Layers[0].SetVerticalLayerLine(RoomLengthX - 1, "null");

            //========================Layer 1========================
            AddRoomLayer(RoomHeightY - 1, RoomLengthX);
            Layers[1].SetOnRandomLayerID("innerObject");

            //========================Layer 2========================
            AddRoomLayer(RoomHeightY, RoomLengthX);
            Layers[2].SetHorizontalLayerLine(0, "wall");
            Layers[2].SetHorizontalLayerLine(1, "topWallBrink");
            Layers[2].SetHorizontalLayerLine(RoomHeightY - 2, "wall");
            Layers[2].SetHorizontalLayerLine(RoomHeightY - 1, "topWallBrink");
            Layers[2].SetVerticalLayerLine(0, "leftWall");
            Layers[2].SetVerticalLayerLine(RoomLengthX - 1, "rightWall");
            Layers[2].SetUniqueCorners("leftTopBrinkCorner", "rightTopBrinkCorner", "leftCorner", "rightCorner");
        }
    }
}
