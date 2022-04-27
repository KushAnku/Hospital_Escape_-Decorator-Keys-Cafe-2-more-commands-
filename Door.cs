using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital_Escape
{
    public class Door
    {
        private bool _locked;
        public Rooms _roomA;
        public Rooms _roomB;

        public bool Locked { set { _locked = value; } get { return _locked; } }


        //constructors
        public Door(Rooms roomA, Rooms roomB) : this(roomA, roomB, false) { }
        /// <summary>
        /// constructor for door
        /// </summary>
        /// <param name="roomA">room on one side of the door</param>
        /// <param name="roomB">room on the other side of the door</param>
        /// <param name="locked">is the door locked or not</param>
        /// <param name="barricaded">is the door barracaded or nailed shut or not</param>
        public Door(Rooms roomA, Rooms roomB, bool locked)
        {
            _locked = locked;
            _roomA = roomA;
            _roomB = roomB;
        }
        public Rooms OtherSide(Rooms from)
        {
            //make sure the door is not locked
            if (_locked)
            {
                Console.ForegroundColor =  ConsoleColor.Blue;
                Console.WriteLine( "This door is locked ", Console.ForegroundColor);
                Console.ForegroundColor = ConsoleColor.White;
                return null;
            }
            if (from.tag.ToLower() == (_roomA.tag.ToLower()))
            {
                return _roomB;
            }
            else if (from.tag.ToLower() == _roomB.tag.ToLower())
            {
                return _roomA;
            }
            //if the door is locked return a null
            return null;
        }


        public static Door CreateDoor(Rooms room1, Rooms room2)
        {
            return CreateDoor(room1, room2, false);
        }


        /// <summary>
        /// helper function to create a door object
        /// </summary>
        /// <param name="room1">room on one side of the door</param>
        /// <param name="room2">room on the other side of the door</param>
        /// <returns></returns>
        public static Door CreateDoor(Rooms room1, Rooms room2, bool locked)
        {
            Door door = new Door(room1, room2, locked);
            room1.setExit(room2.tag, door);
            room2.setExit(room1.tag, door);
            return door;
        }
    }
}
