using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital_Escape
{
    // Restricted class that cannot be used to create objects
    public abstract class Command
    {
        private string _name;
        public string name { get { return _name;  } set { _name = value; } }
        
        private string _secondWord, _thirdWord; 
        public string SecondWord { get { return _secondWord; } set { _secondWord = value; } }
        public string ThirdWord { get { return _thirdWord; } set { _thirdWord = value; } }
        
        public Command()
        {
            this.name = "";
            this.SecondWord = null; // The null value simply means that the variable does not refer to an object in memory.
        }

        public bool hasSecondWord()
        {
            return this.SecondWord != null;
        }

        public bool hasThirdWord()
        {
            return this.ThirdWord != null;
        }

        public abstract bool Execute(Nurse nurse);
        // To give a single base class definition that various derived classes can use.
    }
}
