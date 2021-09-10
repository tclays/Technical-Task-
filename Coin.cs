using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachineTask
{
    public class Coin
    {
        private string mValue = null;

        public Coin()
        {
            MValue = "10p";
        }
        public Coin(string userValue)
        {
            mValue = userValue;
        }

        public string MValue { get => mValue; set => mValue = value; }

    }
}
