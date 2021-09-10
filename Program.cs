using System;
using System.Collections;
using System.Collections.Generic;

namespace VendingMachineTask
{
    class Program : ICustomer
    {

        private static VendingMachine myVendingMachine;
        private static string userMessage;
        private static string errorMessage;
        private static bool isValid;
        private static int userChoice;

        static void Main(string[] args)
        {

            while (true)
            {
                myVendingMachine = VendingMachine.GetVendingMachine();

                userMessage = "1:Insert a coin:\n2:views products";
                errorMessage = "Sorry you must pick a number from this list\n";

                Console.WriteLine("Start: " + myVendingMachine.Display);
                Console.WriteLine(userMessage);

                userChoice = 0;
                bool isValid = int.TryParse(Console.ReadLine(), out userChoice);

                while (isValid == false)
                {
                    Console.WriteLine(errorMessage + userMessage);
                    isValid = int.TryParse(Console.ReadLine(), out userChoice);
                }
                if (userChoice == 1)
                {
                    CoinMenu();
                }
                else
                {
                    BuyProduct();
                }
                Console.ReadKey();
            }
        }

        public static void CoinMenu()
        {
            while (true)
            {
                string userMessage = "INSERT COIN\n1: 5p\n2: 10p\n3: 20p\n4: 50p\n5: £1\n6: £2\n";
                Console.Clear();
                Console.WriteLine(userMessage);
                int optionValue = 0;
                isValid = int.TryParse(Console.ReadLine(), out optionValue);
                while (isValid == false)
                {
                    Console.WriteLine(errorMessage + userMessage);
                    isValid = int.TryParse(Console.ReadLine(), out optionValue);
                }

                switch (optionValue)
                {
                    case 1:
                        InsertCoin("5p");
                        break;
                    case 2:
                        InsertCoin("10p");
                        break;
                    case 3:
                        InsertCoin("20p");
                        break;
                    case 4:
                        InsertCoin("50p");
                        break;
                    case 5:
                        InsertCoin("£1");
                        break;
                    case 6:
                        InsertCoin("£2");
                        break;
                    default:
                        InsertCoin("2p");
                        break;
                }
                Console.WriteLine("INSERT ANOTHER COIN?\n Y or N");

                string yesNo;
                yesNo = (Console.ReadLine().ToUpper());

                while (yesNo != "Y" && yesNo != "N")
                {
                    Console.WriteLine("Sorry you must use ether the Y key or the N key\nINSERT ANOTHER COIN?\n Y or N");
                    yesNo = (Console.ReadLine().ToUpper());
                }
                if ( yesNo == "Y")
                {
                    continue;
                }
                break;
            }
        }

         public static void BuyProduct()
        {

            while (true)
            {

                Console.WriteLine("Select a product \n1: Cola \n2: Crips \n3: Chcolate ");
                Console.WriteLine("Return Coins press 4");
                int customerChoice = 0;
                bool isValid = int.TryParse(Console.ReadLine(), out customerChoice);

                while (isValid == false || customerChoice > 4 || customerChoice < 1)
                {
                    Console.WriteLine("Sorry you must enter a valid option number: 1 2 or 3\nplease try again");
                    isValid = int.TryParse(Console.ReadLine(), out customerChoice);
                }

                Console.WriteLine("Result:");
                if (customerChoice == 1)
                {
                    myVendingMachine.choice(1);
                    Console.WriteLine(1);
                }
                else if (customerChoice == 2)
                {
                    myVendingMachine.choice(2);
                    Console.WriteLine(2);
                }
                else if (customerChoice == 3)
                {
                    myVendingMachine.choice(3);
                    Console.WriteLine(3);
                }
                else
                {
                    try
                    {
                        myVendingMachine.ReturnCoins();
                    }
                    catch
                    {
                        Console.WriteLine("Bug");
                    }
                }

                Console.WriteLine("BUY ANOTHER PRODUCT?\n Y or N");

                string yesNo;
                yesNo = (Console.ReadLine().ToUpper());

                while (yesNo != "Y" && yesNo != "N")
                {
                    Console.WriteLine("Sorry you must use ether the Y key or the N key\nBUY ANOTHER PRODUCT?\n Y or N");
                    yesNo = (Console.ReadLine().ToUpper());
                }
                if (yesNo == "Y")
                {
                    continue;
                }
                break;

            }
        }


        public static void InsertCoin(string coinValue)
        {
            //Create the coin that the customer has inserted 
            Coin insertedCoin = new Coin(coinValue);

            //Try to insert the coin 
            if (myVendingMachine.CheckValidInsertCoin(insertedCoin) == false)
            {
                Console.WriteLine("Coin Rejected!");
            }
            Console.WriteLine(myVendingMachine.Display);
        }

    }
}
