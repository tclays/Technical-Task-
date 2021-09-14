using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachineTask
{
    public class Coin
    
        private string name;
        private decimal mvalue;

        public Coin()
        {
            MValue = "10p";
        }
        public Coin(string userValue)
        {
            name = userValue;
            
            
            switch userValue
                
             case  5p
          mvalue = 0.05;
             default 
                   mvALUE = 0.00m;
            
        }

        public string MValue { get => mValue; set => mValue = value; }

    }
}
