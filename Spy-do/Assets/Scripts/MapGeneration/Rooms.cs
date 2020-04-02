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
            //========================Layer 0=======================
            AddRoomLayer();
            Layers[0].FillWholeLayerMap("GymFloor");
            Layers[0].SetHorizontalLayerLine(RoomHeightY - 1, "null");
            Layers[0].SetHorizontalLayerLine(RoomHeightY - 2, "null");

            //========================Layer 1========================
            AddRoomLayer();
            Layers[1].SetOnRandomLayerID("GymInnerObject");
            Layers[1].SetHorizontalLayerLine(RoomHeightY - 2, "GymWall");
            Layers[1].SetHorizontalLayerLine(RoomHeightY - 1, "GymTopWallBrink");
            Layers[1].SetHorizontalLayerLine(0, "GymWall");
            Layers[1].SetHorizontalLayerLine(1, "GymTopWallBrink");

            //========================Layer 2=======================
            AddRoomLayer();
            Layers[2].SetVerticalLayerLine(0,"GymLeftWall");
            Layers[2].SetVerticalLayerLine(RoomLengthX - 1, "GymRightWall");
            Layers[2].SetHorizontalLayerLine(RoomHeightY - 1, "null");
            Layers[2].SetHorizontalLayerLine(0, "null");
        }
    }

    class Office : Room 
    {
        public Office(int roomHeightY, int roomLengthX) : base(roomHeightY, roomLengthX) { }

        private protected override void instRoom()
        {
            //========================Layer 0=======================
            AddRoomLayer();
            Layers[0].FillWholeLayerMap("OfficeFloor");
            Layers[0].SetHorizontalLayerLine(RoomHeightY - 1, "null");
            Layers[0].SetHorizontalLayerLine(RoomHeightY - 2, "null");
            
            //========================Layer 1=======================
            AddRoomLayer();            
            Layers[1].SetOnRandomLayerID("OfficeTable");
            Layers[1].SetHorizontalLayerLine(RoomHeightY - 1, "null");
            Layers[1].SetHorizontalLayerLine(RoomHeightY - 2, "null");
            Layers[1].SetHorizontalLayerLine(RoomHeightY - 2, "OfficeWall");
            Layers[1].SetHorizontalLayerLine(0, "OfficeWall");

            //========================Layer 2=======================
            AddRoomLayer();
            Layers[2].SetVerticalLayerLine(0, "OfficeLeftWall");
            Layers[2].SetVerticalLayerLine(RoomLengthX - 1, "OfficeRightWall");
            Layers[2].SetHorizontalLayerLine(RoomHeightY - 1, "null");
            Layers[2].SetHorizontalLayerLine(0, "null");
            Layers[2].SetOnUniqueObject(Layers[1].LayerObjectMap, "OfficeTable", "OfficeComputer", 2);

            //========================Layer 3========================
            AddRoomLayer();
            Layers[3].SetHorizontalLayerLine(1, "OfficeTopWallBrink");
            Layers[3].SetHorizontalLayerLine(RoomHeightY - 1, "OfficeTopWallBrink");
            
        }
    }
}
