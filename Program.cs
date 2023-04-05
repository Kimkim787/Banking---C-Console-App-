using System;
using System.ComponentModel.Design;

namespace Bank
{
    class Gate
    {
        public static void Main(string[] args)
        {

            var client = new Clients();

            var front = new FrontEnd();
            var server = new ServerSide();
            bool repeat = true;
            while (repeat)
            {
                Console.WriteLine("Choose: ");
                Console.WriteLine("1. Sign");
                Console.WriteLine("2. Create Account");
                Console.WriteLine("3. Get Names");
                Console.WriteLine("4. Get Ids");
                Console.WriteLine("Otherwise End Program");
                int choice = Convert.ToInt32(Console.ReadLine());
                Console.Clear();
                switch (choice)
                {
                    case 1:
                        int valid = server.SignIn();
                        if (valid >= 0)
                        {
                            front.menu(valid);
                        }
                        else
                        {
                            Console.WriteLine("Invalid user name or id");
                        }
                        break;
                    case 2:
                        server.createAccount();
                        break;
                    case 3:
                        string[] nameList = client.getNames();
                        Console.WriteLine(nameList[nameList.Length - 1]);
                        break;
                    case 4:
                        int[] idList = client.getIds();
                        Console.WriteLine(idList[idList.Length - 1]);
                        break;
                }

            }
        }
    }
    class FrontEnd
    {
        public void menu(int index)
        {
            Console.Clear();
            bool repeat = true;
            while (repeat)
            {
                var client = new Clients();
                var nameList = client.getNames();
                Console.WriteLine("Hello " + nameList[index]);
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("1. Balance");
                Console.WriteLine("2. Withdraw");
                Console.WriteLine("3. Deposit");
                Console.WriteLine("4. Log Out");
                int choice = Convert.ToInt32(Console.ReadLine());
                Console.Clear();
                switch (choice)
                {
                    case 1:
                        double balance = client.getBalance(index);
                        Console.WriteLine("Your balance is: " + balance);
                        break;
                    case 2:
                        Console.Write("Enter amount to withdraw: ");
                        double withdrawAmount = Convert.ToDouble(Console.ReadLine());
                        double currentBalance = client.withdraw(withdrawAmount, index);
                        Console.WriteLine("Your balance is: " + currentBalance);
                        break;
                    case 3:
                        Console.Write("Enter amount to deposit: ");
                        double depositAmount = Convert.ToDouble(Console.ReadLine());
                        double currentBalance2 = client.deposit(depositAmount, index);
                        Console.WriteLine("Your balance is: " + currentBalance2);

                        break;
                    case 4: repeat = false; break;
                    default:
                        Console.WriteLine("invalid input! try again: \n");
                        break;
                }
            } // while closing
        }

    }
    class ServerSide // Supposedly Host: Mistake; Kapoy ilis ngalan
    {
        public int SignIn()
        {
            var clients = new Clients();
            int[] id = clients.getIds();
            string[] names = clients.getNames();

            Console.Write("Enter account name: ");
            string inputName = Convert.ToString(Console.ReadLine());
            Console.Write("Enter account ID: ");
            int inputedId = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < id.Length; i++)
            {
                if (id[i] == inputedId && names[i] == inputName)
                {
                    return i;
                }
            }
            return -1;
        }
        public void createAccount()
        {
            var clients = new Clients();
            Random rand = new Random();
            Console.WriteLine("------------ Create Account --------------");
            Console.WriteLine("Enter Account Name");
            string newName = Convert.ToString(Console.ReadLine());
            int randomUserId = rand.Next(100, 1000);
            Console.WriteLine("Please take note of your user ID => " + randomUserId);
            clients.addId(randomUserId);
            clients.addAccNames(newName);
            clients.addBalances();
        }
    }
        // public void testIdValidity(); //
        // }
        class Clients // Supposedly Server: Mistaken names
        {
            private static int[] id = { 123, 132, 787 };
            private static string[] accNames = { "Clement", "Flores", "Kimkim" };
            private static double[] balance = { 100.00, 1500.00, 0.50 };
            public string[] getNames()
            {
                return accNames;
            }
            public int[] getIds()
            {
                return id;
            }
            public double getBalance(int index)
            {
                return balance[index];
            }
        public double withdraw(double amount, int index)
        {
            if (balance[index] >= amount)
            {
                balance[index] = amount;
            } else
            {
                Console.Clear();
                Console.WriteLine("Not enough amount \n");
            }
                return balance[index];
            }
            public double deposit(double amount, int index)
            {
                balance[index] += amount;
                return balance[index];
            }
            public void addId(int newId)
            {
                Array.Resize(ref id, id.Length + 1);
                id[id.Length - 1] = newId;
            }
            public void addAccNames(string name)
            {
                Array.Resize(ref accNames, accNames.Length + 1);
                accNames[accNames.Length - 1] += name;
            }
            public void addBalances()
            {
                Array.Resize(ref balance, balance.Length + 1);
                for(int i = 0; i <= balance.Length - 1; i++)
                {
                    balance[i] = balance[i];
                }
                balance[balance.Length - 1] = 0.00;
            }
        }
}


