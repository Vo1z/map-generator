/*
 * Sirex production code:
 * Project: Spy-Do
 * Author: Voiz (Viktor Lishchuk)
 * Email: vitya.voody@gmail.com
 * Twitter: @V0IZ_
 */
namespace MapGenerator
{
    public struct SLocationOfRoomsInformation
    {
        public int minLocationHeightY;
        public int maxLocationHeightY;
        public int minLocationLengthX;
        public int maxLocationLengthX;

        public int sumOfAllLocationRooms; //responsible for SUM of all rooms in the location
        public int numberOfRoomTypes; //responsible for number of TYPES of the rooms on location

        //=====Gym=====        
        public bool isGym;
        public int gymQuantity;

        public int gymMinHeightY;
        public int gymMaxHeightY;
        public int gymMinLengthX;
        public int gymMaxLengthX;

        //=====Office=====
        public bool isOffice;
        public int officeQuantity;

        public int officeMinHeightY;
        public int officeMaxHeightY;
        public int officeMinLengthX;
        public int officeMaxLengthX;

        //======EmptySpace======
        public bool isEmptySpace;
        public int emptySpaceQuantity;

        public int emptySpaceMinHeightY;
        public int emptySpaceMaxHeightY;
        public int emptySpaceMinLengthX;
        public int emptySpaceMaxLengthX;


        //=====<ROOM>=====
        /*public bool is<ROOM>;
        public int <ROOM>Quantity;

        public int <ROOM>MinHeightY;
        public int <ROOM>MaxHeightY;
        public int <ROOM>MinLengthX;
        public int <ROOM>MaxLengthX;*/
    }
}