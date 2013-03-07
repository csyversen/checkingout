using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CheckingOut
{
    class Program
    {
        const char SPLITCHAR = ' ';

        static void Main(string[] args)
        {
            if (args.Length == 1)
            {
                // TODO: should i put in more checks when reading in the config file or can i assume it's in the correct format?

                string[] lines = System.IO.File.ReadAllLines(args[0]);
                
                int numRegisters = int.Parse(lines[0]);
                Store store = new Store(numRegisters);

                lines = lines.Skip(1).ToArray();

                foreach (string line in lines)
                {
                    string[] entry = line.Split(SPLITCHAR);
                    Customer c = new Customer(entry[0], int.Parse(entry[1]), int.Parse(entry[2]));
                    store.addCustomer(c);
                }

                List<Customer> custs = store.getCustomers();
                foreach (Customer c in custs)
                {
                    Console.WriteLine(c.ToString());
                }
                Console.WriteLine(String.Format("Number of registers: {0}", store.NumRegisters));
                
                store.distributeCustomers();
                // TODO: uncomment this out for testing
                //store.PrintRegisterQueues();
                
            }
            else
            {
                Console.WriteLine("Improper arguments were defined, please run again with the correct arguments");
            }
            
        }
    }
}
