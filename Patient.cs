using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital_Escape
{
    // state design pattern is being used.
    public enum State
    {
        FINE,
        CHECK,
        DYING
    }
    // state design pattern is being used.
    public class Patient: subject
    {
        private State _state;

        public State state
        {
            get
            {
                return _state;
            }
            set
            {
                _state = value;

                switch (_state)
                {
                    case State.DYING:
                        Console.WriteLine("a patient is Dying");
                        break;
                    case State.CHECK:
                        Console.WriteLine("a Patient needs check up");
                        break;
                }
            }
        }
        Random random = new Random();
        public Dictionary<string, int> Items { get; set; } = new Dictionary<string, int>();
        // Generic type where it store items.

        public Patient()
        {
            foreach(string key in Game.items)
            {
                Items[key] = 0;
            }

            for(int i=0; i<= random.Next(0,7); i++)
            {
                string myName = Game.items[0];
                Items[myName] += 1;
            }

            List<string> deskItems = new List<string>()
            {
                "stethescope", "HeartMachine", "BpMachine", "Clipboard"
            };
            //Items[deskItems[random.Next(5)]] = 1;
        }

        // update method is being used here 
        public override void update(Notification notification)
        {
            Random random = new Random();
            double st = random.NextDouble();
            if (state == State.FINE)
            {
                if (st > 0.8 && st < 1.0)
                {
                    state = State.DYING;
                }
                else if (st > 0.6 && st < 0.8)
                {
                    state = State.CHECK;
                }
            }
        }
        // state design pattern being used here.
        public void save()
        {
            if (_state == State.DYING)
            {
                Console.WriteLine("Patient got saved");
                _state = State.FINE;
            }
        }
        public void checkup()
        {
            if (_state == State.CHECK)
            {
                Console.WriteLine("Patient got checked up");
                _state = State.FINE;
            }

        }
    }
}
