using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital_Escape
{
    /*Factory design pattern.
      Used to create all the items in the game  
       like stethoscope or heart machine. We can find this pattern in item creator file.*/
    class ItemCreator
    {

        public Item FactoryMethod(String item)
        {
            item = item.ToLower();
            switch(item){
                case "heart machine":
                    return new Item("HeartMachine", 4f, 5f, 20f);     // f = float
                case "bp machine":
                    return new Item("BpMachine", 2f, 2f, 25f);
                case "stethescope":
                    return new Item(item, 1f, 1f, 5f);
                case "defibrillator":
                    return new Item(item, 2f, 2f, 50f);
                case "clipboard":
                    return new Item(item, 1f, 3f,1f);
                case "soda":
                    return new Item(item, 1f, 1f, 2f);
                case "lunch":
                    return new Item(item, 2f, 3f, 2f);
                case "dessert":
                    return new Item(item, 1f, 1f, 4f);
                  

                default:
                    Console.WriteLine(item);
                    return null;
            }
        }
    }
}
