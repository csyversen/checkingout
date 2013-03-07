using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckingOut
{
    class Store
    {
        public List<Customer> Customers { get; private set; }
        public int NumRegisters { get; private set; }
        public Register[] registers { get; private set; }

        public Store(int argRegisters)
        {
            Customers = new List<Customer>();
            NumRegisters = argRegisters;
            registers = new Register[NumRegisters];
            for (int i = 0; i < NumRegisters - 1; i++)
            {
                registers[i] = new Register(false);
            }
            registers[NumRegisters - 1] = new Register(true);
        }

        public void addCustomer(Customer c)
        {
            Customers.Add(c);
             
            // I know sorting every time an element is added is inefficient, but i don't want to force the user of this class to remember to call a 'sort' function after they've added all their customers. 
            // For an app like this, the benefit of guaranteeing that the list is always sorted outweighs the performance penalty 
            sortCustomers();
        }

        private void sortCustomers()
        {
            Customers = Customers.OrderBy(c => c.Arrival).ThenBy(c => c.NumItems).ThenBy(c => c.Type).ToList();
        }

        public void CheckOutAllCustomers()
        {
            foreach (Customer c in Customers)
            {
                //check the queue for each register, if the queue has 0 entries then that customer goes in it
                //if no queue has 0 entries, then customer A will go to the queue that has the shortest line
                if (c.Type == "A")
                {
                    registers.OrderBy(r => r.GetNumPeopleInLineAtTime(c.Arrival)).First().GetInLine(c);
                }
                if (c.Type == "B")
                {
                    if (registers.Any(r => r.GetNumPeopleInLineAtTime(c.Arrival) == 0))
                    {
                        registers.First(r => r.GetNumPeopleInLineAtTime(c.Arrival) == 0).GetInLine(c);
                    }
                    else
                    {
                        // get the person last in line with the fewest amount of items, type B goes in that line
                        registers.OrderBy(r => r.GetLastCustomerInLine().NumItems).First().GetInLine(c);                        
                    }
                }
            }
            // At this point the Store's check out history has been mapped, go through the registers and see who has the highest 'Time' value
            Console.WriteLine(String.Format("Finished at: t={0} minutes", registers.Max(r => r.Time)));
        }
    }
}
