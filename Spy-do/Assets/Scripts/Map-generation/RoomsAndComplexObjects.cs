/*
 * Sirex production code:
 * Project: Spy-Do
 * Author: Voiz (Viktor Lishchuk)
 * Email: vitya.voody@gmail.com
 * Twitter: @V0IZ_
 */

using UnityEngine;

namespace MapGenerator
{
    //Rooms
    class EmptySpace : Room 
    {
        public EmptySpace(int roomHeightY, int roomLengthX) : base(roomHeightY, roomLengthX) { }

        protected override void instRoom() 
        {
            //========================Layer 0=======================
            AddRoomLayer();
            RoomLayers[0].FillWholeLayerMap("GRFloor2");
            RoomLayers[0].SetOnRandomLayerID("GRFloor2", 2);
            RoomLayers[0].SetOnRandomLayerID("GRFloor3", 2);
            
            //========================Layer 1=======================
            AddRoomLayer();
            RoomLayers[1].SetOnRandomLayerID("GRInnerObject", 8);

            AddRoomLayer();
            AddRoomLayer();
            SetDefaultExitAndLayerZ(new Test(), 2);
        }
    }

    class Gym : Room
    {
        public Gym(int roomHeightY, int roomLengthX) : base(roomHeightY, roomLengthX) { }

        protected override void instRoom()
        {
            //========================Layer 0=======================
            AddRoomLayer(RoomHeightY - 2, RoomLengthX);
            RoomLayers[0].FillWholeLayerMap("GymFloor");           
            
            //========================Layer 1=======================
            AddRoomLayer(RoomHeightY - 2, RoomLengthX);
            RoomLayers[1].SetOnRandomLayerID("GymInnerObject", 5);

            //========================Layer 2=======================
            AddRoomLayer();
            RoomLayers[2].SetHorizontalLayerLine(0, "GymWall");
            RoomLayers[2].SetHorizontalLayerLine(RoomHeightY - 2, "GymWall");

            //========================Layer 3=======================
            AddRoomLayer();
            RoomLayers[3].SetVerticalLayerLine(0, "GymLeftWall");
            RoomLayers[3].SetVerticalLayerLine(RoomLengthX - 1, "GymRightWall");
            RoomLayers[3].SetHorizontalLayerLine(0, null);
            RoomLayers[3].SetHorizontalLayerLine(RoomHeightY - 1, null);

            //========================Layer 4=======================
            AddRoomLayer();
            RoomLayers[4].SetHorizontalLayerLine(1, "GymTopWallBrink");
            RoomLayers[4].SetHorizontalLayerLine(RoomHeightY - 1, "GymTopWallBrink");

            SetDefaultExitAndLayerZ(new Test(), 2);

            SetExit(new Test(), 2, EPosition.LEFT, Random.Range(1, RoomHeightY - 2));
            SetExit(new Test(), 2, EPosition.RIGHT, Random.Range(1, RoomHeightY - 2));
        }
    }

    class Office : Room 
    {
        public Office(int roomHeightY, int roomLengthX) : base(roomHeightY, roomLengthX) { }

        protected override void instRoom()
        {
            //========================Layer 0=======================
            AddRoomLayer();
            RoomLayers[0].FillWholeLayerMap("OfficeFloor");
            RoomLayers[0].SetHorizontalLayerLine(RoomHeightY - 1, "GRFloor1");

            //========================Layer 1=======================
            AddRoomLayer();
            RoomLayers[1].SetOnRandomLayerID("OfficeTable", 5);
            RoomLayers[1].SetHorizontalLayerLine(RoomHeightY - 1, null);

            //========================Layer 2=======================
            AddRoomLayer();
            RoomLayers[2].SetOnUniqueObject(RoomLayers[1].LayerObjectMap, "OfficeTable", "OfficeComputer", 4);
            RoomLayers[2].SetHorizontalLayerLine(0, "OfficeWall");
            RoomLayers[2].SetHorizontalLayerLine(RoomHeightY - 2, "OfficeWall");            

            //========================Layer 3=======================
            AddRoomLayer();
            RoomLayers[3].SetVerticalLayerLine(0, "OfficeLeftWall");
            RoomLayers[3].SetVerticalLayerLine(RoomLengthX - 1, "OfficeRightWall");
            RoomLayers[3].SetHorizontalLayerLine(0, null);
            RoomLayers[3].SetHorizontalLayerLine(RoomHeightY - 1, null);

            //========================Layer 4=======================
            AddRoomLayer();
            RoomLayers[4].SetHorizontalLayerLine(1, "OfficeTopWallBrink");
            RoomLayers[4].SetHorizontalLayerLine(RoomHeightY - 1, "OfficeTopWallBrink");

            SetDefaultExitAndLayerZ(new Test(), 2);

            SetExit(new Test(), 2, EPosition.LEFT, Random.Range(1, RoomHeightY - 2));
            SetExit(new Test(), 2, EPosition.RIGHT, Random.Range(1, RoomHeightY - 2));
        }
    }

    class GeneralRoom : Room
    {        
        public GeneralRoom(int roomHeightY, int roomLengthX) : base(roomHeightY, roomLengthX) { }

        protected override void instRoom()
        {            
            //========================Layer 0=======================
            AddRoomLayer(RoomHeightY - 1, RoomLengthX);
            RoomLayers[0].FillWholeLayerMap("GRFloor1");
            RoomLayers[0].SetOnRandomLayerID("GRFloor2", 2);
            RoomLayers[0].SetOnRandomLayerID("GRFloor3", 2);

            //========================Layer 1=======================
            AddRoomLayer();

            //========================Layer 2=======================
            AddRoomLayer(RoomHeightY - 1, RoomLengthX);
            RoomLayers[2].SetHorizontalLayerLine(RoomHeightY - 2, "GRTopWall");
            RoomLayers[2].SetOnRandomLayerID("GRInnerObject", 8);
            RoomLayers[2].SetHorizontalLayerLine(RoomHeightY - 2, "GRTopWall");

            //========================Layer 3=======================
            AddRoomLayer();            
            RoomLayers[3].SetHorizontalLayerLine(0, "GRBottomWall");
            RoomLayers[3].SetHorizontalLayerLine(RoomHeightY - 2, "GRTopWall");

            //========================Layer 4=======================
            AddRoomLayer();

            //========================Layer 5=======================
            AddRoomLayer();
            RoomLayers[5].SetVerticalLayerLine(0, "GRLeftWall");
            RoomLayers[5].SetVerticalLayerLine(RoomLengthX - 1, "GRRightWall");
            RoomLayers[5].SetHorizontalLayerLine(0, null);
            RoomLayers[5].SetHorizontalLayerLine(RoomHeightY - 1, null);
            
            //========================Layer 6=======================
            AddRoomLayer();
            RoomLayers[6].SetHorizontalLayerLine(1, "GRTopWallBrink");
            RoomLayers[6].SetHorizontalLayerLine(RoomHeightY - 1, "GRTopWallBrink");

            SetDefaultExitAndLayerZ(new Test(), 2);
        }        
    }


    //ComplexObjects
    class Test : ComplexObject
    {
        public Test() : base(1, 1) { }
         
        protected override void instCO()
        {
        //========================Layer 0=======================
            AddCOLayer();
            COLayers[0].FillWholeLayerMap("GymFloor");
        //========================Layer 1=======================
            AddCOLayer();
            COLayers[1].FillWholeLayerMap("GymFloor");
        }
    }
}