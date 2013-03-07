using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckingOut
{
    class Customer
    {        
        public string Type { get; private set; }
        public int Arrival { get; private set; }
        public int NumItems { get; private set; }
        public int CheckOutStartTime { get; set; }

        public Customer(string argType, int argArrival, int argItems)
        {
            Type =  argType;
            Arrival = argArrival;
            NumItems = argItems;
        }

        public override string ToString()
        {
            return String.Format("Customer Type: {0} Arrival Time: {1} Items: {2} CheckoutStartTime: {3}", Type, Arrival, NumItems, CheckOutStartTime);
        }
    }
}
