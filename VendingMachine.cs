using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachineTask
{
    class VendingMachine
    {
        private const int PRODUCT_SIZE = 3;
        private const int COIN_SIZE = 20;

        private string display;  //PRICE, THANK YOU, SOLD OUT, EXACT CHANGE ONLY
        private double amount;  //Current amount of money inserted by customer 

        private List<Coin> fivePenceCoins;
        private List<Coin> tenPenceCoins;
        private List<Coin> twentyPenceCoins;
        private List<Coin> fiftyPenceCoins;
        private List<Coin> onePoundCoins;
        private List<Coin> twoPoundCoins;

        private List<Product> colaProducts;
        private List<Product> crispsProducts;
        private List<Product> chocolateProducts;

        private double COLAPRICE = 1.00;
        private double CRISPSPRICE = 0.50;
        private double CHOCOLATEPRICE = 0.65;

        private static VendingMachine VMachine = null;

        private VendingMachine()
        {
            colaProducts = new List<Product>();
            crispsProducts = new List<Product>();
            chocolateProducts = new List<Product>();

            //Create the products
            Product newColaProduct = new Product("cola", COLAPRICE);
            Product newCripsProduct = new Product("crisps", CRISPSPRICE);
            Product newChcolateroduct = new Product("chocolate", CHOCOLATEPRICE);

            //Stack up each of the products
            for (int i = 0; i < PRODUCT_SIZE; i++)
            {
                colaProducts.Add(newColaProduct);
                crispsProducts.Add(newCripsProduct);
                chocolateProducts.Add(newChcolateroduct);
            }

            Coin newfivePenceCoin = new Coin("5p");
            Coin newtenPenceCoin = new Coin("10p");
            Coin newtwentyPenceCoin = new Coin("20p");
            Coin newfiftyPenceCoin = new Coin("50p");
            Coin newOnePoundCoin = new Coin("£1");
            Coin newTwoPoundCoin = new Coin("£2");

            fivePenceCoins = new List<Coin>(); 
            tenPenceCoins = new List<Coin>();
            twentyPenceCoins = new List<Coin>();
            fiftyPenceCoins = new List<Coin>(); 
            onePoundCoins = new List<Coin>(); 
            twoPoundCoins = new List<Coin>();

            for (int i = 0; i < COIN_SIZE; i++)
            {
                fivePenceCoins.Add(newfivePenceCoin);
                tenPenceCoins.Add(newtenPenceCoin);
                twentyPenceCoins.Add(newtwentyPenceCoin);
                fiftyPenceCoins.Add(newfiftyPenceCoin);
                onePoundCoins.Add(newOnePoundCoin);
                twoPoundCoins.Add(newTwoPoundCoin);
            }

            Display = "INSERT COIN";
            amount = 0.00;
            amount = Math.Round(amount, 2); 
        }

        public static VendingMachine GetVendingMachine()
        {
            if (VMachine == null)
            {
                VMachine = new VendingMachine();
                return VMachine;
            }   
            return VMachine;      
        }

        public string Display { get => display; set => display = value; }

        public bool CheckValidInsertCoin(Coin insertedCoin)
        {
            //Reject these coins
            if (insertedCoin.MValue == "1p" || insertedCoin.MValue == "2p" || insertedCoin.MValue == "invalidcoin")
            {
                return false;
            }
            else
            {
                //create a new coin and add to list 
                RecordAmountFromCoin(insertedCoin);
                return true;
            }

        }

        private string RecordAmountFromCoin(Coin insertedCoin)
        {
            if (insertedCoin.MValue == "5p")
            {
                amount += 0.05;
                fivePenceCoins.Add(insertedCoin);
            }
            else if (insertedCoin.MValue == "10p")
            {
                amount += 0.10;
                tenPenceCoins.Add(insertedCoin);
            }
            else if (insertedCoin.MValue == "20p")
            {
                amount += 0.20;
                twentyPenceCoins.Add(insertedCoin);
            }
            else if (insertedCoin.MValue == "50p")
            {
                amount += 0.50;
                fiftyPenceCoins.Add(insertedCoin);
            }
            else if (insertedCoin.MValue == "£1")
            {
                amount += 1;
                onePoundCoins.Add(insertedCoin);

            }
            else if (insertedCoin.MValue == "£2")
            {
                amount += 2;
                onePoundCoins.Add(insertedCoin);
            }
            else
            {
                return Display;
            }

       //   Convert double to string in the correct format 
            Display = amount.ToString("£0.00p");
            return Display;
        }

        private void DispenseProduct(List<Product> productsInStock, double priceProductSelected)
        {
            double amountToDeduct = 0;

            double changeLeft = 0.0;
            try
            {
                amountToDeduct = priceProductSelected;
                //before the next step can we check that the remain amount can be deducted
                changeLeft = (amount - amountToDeduct);
                productsInStock.RemoveAt(productsInStock.Count - 1);
            }
            catch
            {
                //Something went wrong
                return;
            }
            Console.WriteLine("before:" + amount);
            //Deduct from cust amount 
            amount -= amountToDeduct;
            Console.WriteLine("after:" + amount);
            Display = "THANK YOU";
            Display = amount.ToString("£0.00p");
        }

        private void returnChange(double changeLeft)
        {
           
            List<Coin> returnCoins = new List<Coin>();  

            double[] coinsValues = new double[] { 0.05, 0.10, 0.20, 0.50, 1, 2 };

            const int TWOPOUNDCOIN = 5;
            const int ONEPOUNDCOIN = 4;
            const int FIFTYPENCECOIN = 3;
            const int TWENTYPENCECOIN = 2;
            const int TENPENCECOIN = 1;
            const int FIVEPENCECOIN = 0;

            const int TOPSIZE = 1;

            bool amountLeft = true;

            while (amountLeft)
            {
                Console.WriteLine("amount to change: " + (amount = (Math.Round(amount, 2))));

                if (amount >= coinsValues[TWOPOUNDCOIN])
                {
                    amount -= coinsValues[TWOPOUNDCOIN];
                    twoPoundCoins.RemoveAt(twoPoundCoins.Count - TOPSIZE);

                    Coin newTwoPoundCoin = new Coin("£2");
                    returnCoins.Add(newTwoPoundCoin);
                }
                else if (amount >= coinsValues[ONEPOUNDCOIN])
                {
                    amount -= coinsValues[ONEPOUNDCOIN];
                    onePoundCoins.RemoveAt(onePoundCoins.Count - TOPSIZE);

                    Coin newOnePoundCoin = new Coin("£1");
                    returnCoins.Add(newOnePoundCoin);
                }
                else if (amount >= coinsValues[FIFTYPENCECOIN])
                {
                    amount -= coinsValues[FIFTYPENCECOIN];
                    fiftyPenceCoins.RemoveAt(fiftyPenceCoins.Count - TOPSIZE);

                    Coin newFiftyPenceCoin = new Coin("50p");
                    returnCoins.Add(newFiftyPenceCoin);
                }
                else if (amount >= coinsValues[TWENTYPENCECOIN])
                {
                    amount -= coinsValues[TWENTYPENCECOIN];
                    twentyPenceCoins.RemoveAt(twentyPenceCoins.Count - TOPSIZE);

                    Coin newTwentyPoundCoin = new Coin("20p");
                    returnCoins.Add(newTwentyPoundCoin);
                }
                else if (amount >= coinsValues[TENPENCECOIN])
                {
                    amount -= coinsValues[TENPENCECOIN];
                    tenPenceCoins.RemoveAt(tenPenceCoins.Count - TOPSIZE);

                    Coin newTenPenceCoin = new Coin("10p");
                    returnCoins.Add(newTenPenceCoin);
                }
                else if (amount >= coinsValues[FIVEPENCECOIN])
                {
                    amount -= coinsValues[FIVEPENCECOIN];
                    fivePenceCoins.RemoveAt(fivePenceCoins.Count - TOPSIZE);

                    Coin newFivePencePoundCoin = new Coin("5p");
                    returnCoins.Add(newFivePencePoundCoin);

                }
                else
                {
                    //The amount is less then 5p
                       //return the stacks using local stac
                        if(returnCoins.Count > 0 )
                        {
                            RollBack(returnCoins);
                        }
                    Display = "EXACT CHANGE ONLY";
                }
                return;
            }
        }

        private void RollBack( List<Coin> CoinsToReturn)
        {
            for (int i = 0; i < CoinsToReturn.Count - 1; i++)
                //re add the coins to each stack
                if (CoinsToReturn[i].MValue == "10p")
                {
                    tenPenceCoins.Add(CoinsToReturn[i]);
                }
                else if (CoinsToReturn[i].MValue == "20p")
                {
                    twentyPenceCoins.Add(CoinsToReturn[i]);
                }
                else if (CoinsToReturn[i].MValue == "50p")
                {
                    fiftyPenceCoins.Add(CoinsToReturn[i]);
                }
                else if (CoinsToReturn[i].MValue == "£1")
                {
                    onePoundCoins.Add(CoinsToReturn[i]);
                }
                else if (CoinsToReturn[i].MValue == "£2")
                {
                    twoPoundCoins.Add(CoinsToReturn[i]);
                }
                else 
                {
                    //error
                    Console.WriteLine("cannot find the correct stack");
                }
        }

        public void choice(int Choice)
        {
            if (Choice == 1 && colaProducts.Count > 0 && amount >= COLAPRICE)
            {
                DispenseProduct(colaProducts, COLAPRICE);
            }
            else if (Choice == 2 && crispsProducts.Count > 0 && amount >= CRISPSPRICE)
            {
                DispenseProduct(crispsProducts, CRISPSPRICE);
            }
            else if (Choice == 3 && chocolateProducts.Count > 0 && amount >= CHOCOLATEPRICE)
            {
                DispenseProduct(chocolateProducts, CHOCOLATEPRICE);
            }
            else
            {  //Not enough money to buy product
                if(Choice == 1 && amount < COLAPRICE) 
                {
                    Display = "PRICE: " + COLAPRICE;
                }               
                else if (Choice == 2 && amount < CRISPSPRICE)
                {
                    Display = "PRICE: " + CRISPSPRICE;
                }
                else if (Choice == 3 && amount < CHOCOLATEPRICE)
                {
                    Display = "PRICE: " + CHOCOLATEPRICE;
                }
                else
                {
                    Display = "SOLD OUT";
                    return;
                }
                Console.WriteLine("current amount left " + amount);
                return;
            }
        }
        private void RemainingAmountToCoins()
        {

            double[] coinsValues = new double[] { 0.05, 0.10, 0.20, 0.50, 1, 2 };

            const int TWOPOUNDCOIN = 5;
            const int ONEPOUNDCOIN = 4;
            const int FIFTYPENCECOIN = 3;
            const int TWENTYPENCECOIN = 2;
            const int TENPENCECOIN = 1;
            const int FIVEPENCECOIN = 0;

            const int TOPSIZE = 1;

            bool amountLeft = true;

            while (amountLeft)
            {
                Console.WriteLine("amount to return: "+ (amount = (Math.Round(amount, 2))));

                if (amount >= coinsValues[TWOPOUNDCOIN])
                {
                    amount -= coinsValues[TWOPOUNDCOIN];
                    twoPoundCoins.RemoveAt(twoPoundCoins.Count - TOPSIZE);
                }
                else if (amount >= coinsValues[ONEPOUNDCOIN])
                {
                    amount -= coinsValues[ONEPOUNDCOIN];
                    onePoundCoins.RemoveAt(onePoundCoins.Count - TOPSIZE);
                }
                else if (amount >= coinsValues[FIFTYPENCECOIN])
                {
                    amount -= coinsValues[FIFTYPENCECOIN];
                    fiftyPenceCoins.RemoveAt(fiftyPenceCoins.Count - TOPSIZE);
                }
                else if (amount >= coinsValues[TWENTYPENCECOIN])
                {               
                    amount -= coinsValues[TWENTYPENCECOIN];
                    twentyPenceCoins.RemoveAt(twentyPenceCoins.Count - TOPSIZE);               
                }
                else if (amount >= coinsValues[TENPENCECOIN])
                {
                    amount -= coinsValues[TENPENCECOIN];
                    tenPenceCoins.RemoveAt(tenPenceCoins.Count - TOPSIZE);
                }
                else if (amount >= coinsValues[FIVEPENCECOIN])
                {
                    amount -= coinsValues[FIVEPENCECOIN];
                    fivePenceCoins.RemoveAt(fivePenceCoins.Count - TOPSIZE);
                }
                else
                {
                    //The amount is less then 5p cannot change rollback 
                    return;
                }

                if (amount == 0)
                {
                    amountLeft = false;
                }
            }
        }

        public void ReturnCoins()
        {
            Console.WriteLine("ReturnCoins");
            RemainingAmountToCoins();
            display = "INSERT COIN";
            Console.WriteLine(display);
        } 

    }
}
