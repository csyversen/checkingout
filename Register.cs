using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckingOut
{
    class Register
    {
        public  List<Customer> line { get; private set; }
        public int Time { get; private set; }
        private int itemTime;

        public Register(bool isTrainee)
        {
            line = new List<Customer>();
            if (isTrainee)
                itemTime = 2;
            else
                itemTime = 1;
        }

        public void GetInLine(Customer c)
        {
            // c arrives at their arrival time.
            // check to see if the cashier is checking out anybody at c's arrival time
            // if yes, this person has to wait until the person in front of them is finished checking out before they start their own checkout process
            // if no, this person can check out right away.

            if (CanCheckOutNow(c.Arrival))
                c.CheckOutStartTime = c.Arrival;                
            else
                c.CheckOutStartTime = Time;
            
            line.Add(c);
            CheckOutCustomer();
        }        

        public bool CanCheckOutNow(int i)
        {
            // if i falls between anybody's checkoutstarttime and their end time, that means that somebody is already checking out right now
            if (line.Any(c => c.CheckOutStartTime <= i && i < c.CheckOutStartTime + c.NumItems * itemTime))
                return false;
            else
                return true;
        }

        // this needs to be called "GetNumPeopleInLineAtTime()"
        public int GetNumPeopleInLineAtTime(int i)
        {
            int lol = line.Count(c => c.Arrival <= i && i < c.CheckOutStartTime + c.NumItems * itemTime);
            return lol;
        }

        public Customer GetLastCustomerInLine()
        {
            return line.Last();
        }

        public void CheckOutCustomer()
        {
            Customer cust = line.Last();            

            // this gives you the time at which the customer was done checking out. 
            Console.WriteLine(cust.ToString());
            Time = cust.CheckOutStartTime + cust.NumItems * itemTime;
            Console.WriteLine("Customer will be done checking out at: " + Time);
        }
    }
}
