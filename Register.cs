using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckingOut
{
    class Register
    {
        // TODO: this might have to get changed to a list, i don't want to ever get rid of any values
        public  List<Customer> line { get; private set; }
        public int Time { get; private set; }
        public bool Trainee { get; private set; }
        private int itemTime;
        private bool isProcessing { get; set; }

        public Register(bool isTrainee)
        {
            Trainee = isTrainee;
            line = new List<Customer>();
            if (Trainee)
            {
                itemTime = 2;
                Console.WriteLine("This is a trainee register");
            }
            else
            {
                itemTime = 1;
            }
            
        }

        public void GetInLine(Customer c)
        {
            
            // person gets in line. 
            // check to see if the cashier is checking out anybody at this time
            // if no, this person begins the checkout process. 
            // if yes, this person 'gets in line' and has the previous person's 'finish' time set as their own 'arrival' time?

            if (CanCheckOut(c.Arrival))
            {
                c.CheckOutStartTime = c.Arrival;
                line.Add(c);                
                CheckOutCustomer();
            }
            else
            {
                c.CheckOutStartTime = Time;                   
                line.Add(c);
                CheckOutCustomer();
            }
        }        

        //check to see how many customers are in line at what time, and if they are being processed or not
        public bool CanCheckOut(int i)
        {
            //if i falls between any customer's arrival time and their checkout time, then they get in line
            if (line.Any(c => c.Arrival <= i && i < c.Arrival + c.NumItems * itemTime))
            {
                return false;
            }
            return true;
        }


        public int GetNumPeopleInLine()
        {
            return line.Count;
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
