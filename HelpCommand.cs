using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital_Escape
{ 
    // Help command is to help the lost user to find their path.
    // implements the command.
    public class HelpCommand : Command
    {
        CommandWord words;
        public HelpCommand() : base()
        {
        //words = commands;
            this.name = "help"; // setting an intitial variable name.
        }
        // The override modifier extends the base class virtual method.
        override
        public bool Execute(Nurse nurse)
        {
            
           // Console.ForegroundColor = ConsoleColor.DarkGreen;
            nurse.outputMessage("\n You are lost. You are alone. Now you will lost in the Hospital \n" +
                                " Your available commands are :- \n");
            nurse.outputMessage("- Type 'go *room name*' to move to a different room in the Hospital\n ");
            nurse.outputMessage("- Type 'take *item name*' to pick up the item from a room \n"); 
            nurse.outputMessage("- Type 'save Patient ' to save the patient\n");
            nurse.outputMessage("-'Hope this information help you :-) :-) :-) :-)\n");
           // Console.ForegroundColor = ConsoleColor.White;
            return false; 
        }
    }
}