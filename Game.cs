using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital_Escape
{
    // In this class making how does player have to move.
    // Making a map so it can be easier for player to play the game.
    class Game
    {
        Nurse nurse;
        Parser parser;
        bool player;
        int saved = 0;

        // creating the list for items.
        public static string[] items = { "Stethescope", 
                                         "BP machine", 
                                         "Heart machine", 
                                         "Brownie points", 
                                         "Clipboard",
                                         "Defibrilator"};
        public static int limitedPoint = 0;
        private static Game unique_game;
        private List<Rooms> observerRooms = new List<Rooms>();
        public static List<Rooms> rooms = new List<Rooms>();

        // initializing the things what we need to use.
        public Game()
        {
            player = false;
            parser = new Parser(new CommandWord());
            nurse = new Nurse(createWorld());
            register(nurse);
            NotificationCenter.Instance.AddObserver("patient saved", patientSaved);
        }
        // using getInstance class to creating the game. if there is no game then their will be no players.
        // ( using Null to give reference ).
        // Singleton pattern 
        // control object creation and in used via get instance class.
        public static Game getInstance()
        {
            if (unique_game == null)
            {
                unique_game = new Game();
            }
            return unique_game;
        }

        // creating new world for hospital escape and new rooms.
        public Rooms createWorld()
        {
            ItemCreator factory = new ItemCreator();
            // placing the items in room
            Rooms outside = new Rooms("Enterance");
            Rooms HelpDesk = new Rooms("HelpDesk");
            HelpDesk.placeItem(factory.FactoryMethod("Bp machine"));
            HelpDesk.placeItem(factory.FactoryMethod("Heart machine"));
            HelpDesk.placeItem(factory.FactoryMethod("Clipboard"));
            HelpDesk.placeItem(factory.FactoryMethod("Stethescope"));
            HelpDesk.placeItem(factory.FactoryMethod("defibrillator"));

            // placing the items in check up room
            Rooms Check_up_Room  = new Rooms("CheckupRoom");
            Check_up_Room.placeItem(factory.FactoryMethod("Bp machine"));
            Check_up_Room.placeItem(factory.FactoryMethod("Clipboard"));
            Check_up_Room.placeItem(factory.FactoryMethod("Stethescope"));
            Check_up_Room.AddPatient(new Patient());
            Check_up_Room.AddPatient(new Patient());

            // placing the items in doctor room
            Rooms Doctor_Office = new Rooms("DoctorOffice");
            Doctor_Office.placeItem(factory.FactoryMethod("Clipboard"));
            Doctor_Office.placeItem(factory.FactoryMethod("Stethescope"));
            Doctor_Office.AddPatient(new Patient());
            

            // placing the items in patient room
            // Adding two patients in this room.
            Rooms Patient_Room = new Rooms("PatientRoom");
            Patient_Room.AddPatient(new Patient());
            Patient_Room.AddPatient(new Patient());

            Patient_Room.placeItem(factory.FactoryMethod("ClipBoard"));
            // adding two patients in this room.
            // placing the items in emergency room.
            Rooms Emergency_room = new Rooms("Emergency_Room");
            Emergency_room.placeItem(factory.FactoryMethod("Bp machine"));
            Emergency_room.placeItem(factory.FactoryMethod("Heart machine"));
            Emergency_room.placeItem(factory.FactoryMethod("ClipBoard"));
            Emergency_room.placeItem(factory.FactoryMethod("Stethescope"));
            Emergency_room.placeItem(factory.FactoryMethod("defibrillator"));
            Emergency_room.AddPatient(new Patient());
            Emergency_room.AddPatient(new Patient());

            // placing the items in cafe room
            Rooms Cafe_room = new Rooms("CafeRoom");
            Cafe_room.placeItem(factory.FactoryMethod("Soda"));
            Cafe_room.placeItem(factory.FactoryMethod("Lunch"));
            Cafe_room.placeItem(factory.FactoryMethod("Dessert"));

            rooms.Add(HelpDesk);
            rooms.Add(Patient_Room);
            rooms.Add(Doctor_Office);
            rooms.Add(Check_up_Room);
            rooms.Add(Emergency_room);
            rooms.Add(Cafe_room);
            rooms.Add(outside);

            Door.CreateDoor(HelpDesk, outside);
            Door.CreateDoor(HelpDesk, Check_up_Room);
            Door.CreateDoor(HelpDesk, Patient_Room);
            Door.CreateDoor(HelpDesk, Cafe_room, true);

            Door.CreateDoor(Patient_Room, Doctor_Office);

            Door.CreateDoor(Check_up_Room, Emergency_room);

            
            Emergency_room.AddPatient(new Patient());
            rooms.Add(Emergency_room);
            Cafe_room.isAvailable = false;
            Cafe_room.Items["brownie"] = 1;
            this.observerRooms.Add(Cafe_room);

            Door.CreateDoor(Emergency_room, Cafe_room);

            return outside;
        }

        public void patientSaved(Notification notification)
        {
            Nurse nurse = (Nurse)notification.Object;
            // using notification pattern to send a notification.
            nurse.giveBrownie();
            saved++;
               //unlock cafe when 4 patients are saved

            nurse.outputMessage("you saved a patient! you get a brownie point.\n your total brownie points are"+nurse.brownies);
        }

        // registering the rooms for the player.

        public void register(Nurse nurse)
        {
            foreach(Rooms rooms in this.observerRooms)
            {
                nurse.registerObserver(rooms);
            }
        }
        // it's starting the game.
        public void play()
        {
            bool finished = false;
            while(!finished)
            {
                Console.Write("\n>");
                Command command = parser.parseCommand(Console.ReadLine());
                if (command == null)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine(" I don't quiet understand what you meant... ", Console.ForegroundColor);
                }
                else
                {
                    finished = command.Execute(nurse);
                }
                // update method design pattern
                // using here when game going to send a notification it's going to send up a notification of update
                NotificationCenter.Instance.PostNotification(new Notification("Update", null));
            }
        }
        // starting up the game.
        public void start()
        {
            player = true;
            nurse.outputMessage(welcome());
        }

        // ending the game.
        public void end()
        {
            player = false;
            nurse.outputMessage(goodbye());
        }
        // displaying the welcome message for the game.
        public string welcome()
        {
            string str = "\n\n";
            str += "     _________________________________________________________________________________________________________ \n";
            str += "    |                                           | Hospital Escape |                                           |\n";
            str += "    |                                           |_________________|                                           |\n";
            str += "    |                                                                                                         |\n";
            str += "    |                                                                                                         |\n";
            str += "    |                                                                                                         |\n";
            str += "    |                                                                                                         |\n";
            str += "    |                                                                                                         |\n";
            str += "    |                                             CafeRoom                           DoctorOffice             |\n";
            str += "    |                                                |                               |                        |\n";
            str += "    |                                                |                               |                        |\n";
            str += "    |                                                |                               |                        |\n";
            str += "    |                                                |                               |                        |\n";
            str += "    |                                                |                               |                        |\n";
            str += "    |                                                |                               |                        |\n";
            str += "    |                         CheckupRoom<________ HelpDesk ______________> PatientRoom                       |\n";
            str += "    |                              |                 |                                                        |\n";
            str += "    |                              |                 |                                                        |\n";
            str += "    |                              |                 |                                                        |\n";
            str += "    |                              |                 |                                                        |\n";
            str += "    |                              |                 |                                                        |\n";
            str += "    |                              |                 |                                                        |\n";
            str += "    |                         Emergency           Enterance                                                   |\n";
            str += "    |                                                                                                         |\n";
            str += "    |                                                                                                         |\n";
            str += "    |_________________________________________________________________________________________________________|\n";
            str += "\n";
            return " Welcome to Hospital Escape! \n\n " +
                str+
                    "\n\n Type 'help' if you need help. " + nurse.currentRoom.description();
            
        }

        // displaying the good bye message.
        public string goodbye()
        {
            return "\n Thank you for playing, Goodbye. \n";
        }
        // creating patients in the room.

        public void createPatients (Rooms rooms)
        {
            Random random = new Random();
            for(int i=0; i<= random.Next(1,10); i++)
            {
                rooms.patients.Add(new Patient());
            }
        }
        
    }
}
