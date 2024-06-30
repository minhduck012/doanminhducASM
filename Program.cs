using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace buu_nuoc
    {
        internal class Program
        {
            static void Main(string[] args)
            {
                while (true)
                {
                    Console.WriteLine("====== Water Bill ======");
                    GetuserName();
                    // Enter water indicators
                    double LMW = Getnumber("Enter the last month water meter: ");
                    double TMW = Getnumber("Enter the this month water meter: ");
                    while (TMW < LMW)
                    {
                        Console.WriteLine("Error. This month's water meter is larger than last month's water meter");
                        Console.Write("Enter the this month water meter: ");
                        TMW = double.Parse(Console.ReadLine());
                    }
                    double meterwater = TMW - LMW;
                    Typecustomer();
                    string choice = Getchoice();
                    if (choice == "5")
                    {
                        break;
                    }

                    double waterBill = CalculateWaterBill(choice, meterwater);
                    double environmentFee = waterBill * 0.1;
                    double vat = (waterBill + environmentFee) * 0.1;
                    double totalBill = waterBill + environmentFee + vat;

                    Console.WriteLine("-------------------------- Water Bill -----------------------------");
                    Console.WriteLine($"  Water consumed:                             {meterwater} m^3    ");
                    Console.WriteLine($"  Water bill:                                 {waterBill} VND     ");
                    Console.WriteLine($"  Environmental Fee:                          {environmentFee} VND");
                    Console.WriteLine($"  VAT:                                        {vat} VND  ");
                    Console.WriteLine("-------------------------------------------------------------------");
                    Console.WriteLine($"  Total bill:                                 {totalBill} VND     ");
                    Console.WriteLine("-------------------------------------------------------------------");

                    Console.WriteLine("Press the Enter key to continue");
                    Console.ReadLine();
                    Console.Clear();
                }
            }

            static string GetuserName()
            {

                Console.Write("Enter username: ");
                return Console.ReadLine();
            }

            static double Getnumber(string message)
            {
                Console.Write(message);
                double number;
                while (!double.TryParse(Console.ReadLine(), out number))
                {
                    Console.WriteLine("Error");

                }

                return number;
            }

            static void Typecustomer()
            {
                Console.WriteLine("1. Household");
                Console.WriteLine("2. Administrative agency");
                Console.WriteLine("3. Production unit");
                Console.WriteLine("4. Business service");
                Console.WriteLine("5. Exit");
            }

            static string Getchoice()
            {
                Console.Write("Enter your choose: ");
                string choice = Console.ReadLine();
                while (choice != "1" && choice != "2" && choice != "3" && choice != "4" && choice != "5")
                {
                    Console.Write("Enter your choose: ");
                    choice = Console.ReadLine();
                }
                return choice;
            }

            static double CalculateWaterBill(string choice, double meterwater)
            {
                double HOUSE_HOLD_1 = 5.973;
                double HOUSE_HOLD_2 = 7.052;
                double HOUSE_HOLD_3 = 8.699;
                double HOUSE_HOLD_4 = 15.929;
                double PRICE_AGENCIES = 9.955;
                double PRICE_PRODUCTION = 11.615;
                double PRICE_BUSINESS = 22.068;


                double price = 0;
                switch (choice)
                {
                    case "1":
                        if (meterwater <= 10)
                        {
                            price = meterwater * HOUSE_HOLD_1;
                        }
                        else if (meterwater <= 20)
                        {
                            price = (10 * HOUSE_HOLD_1) + (meterwater - 10) * HOUSE_HOLD_2;
                        }
                        else if (meterwater <= 30)
                        {
                            price = (10 * HOUSE_HOLD_1) + (10 * HOUSE_HOLD_2) + (meterwater - 20) * HOUSE_HOLD_3;
                        }
                        else
                        {
                            price = (10 * HOUSE_HOLD_1) + (10 * HOUSE_HOLD_2) + (10 * HOUSE_HOLD_3) + (meterwater - 30) * HOUSE_HOLD_4;
                        }
                        break;
                    case "2":
                        price = meterwater * PRICE_AGENCIES;
                        break;
                    case "3":
                        price = meterwater * PRICE_PRODUCTION;
                        break;
                    case "4":
                        price = meterwater * PRICE_BUSINESS;
                        break;
                }
                return price;
            }

        }
    }

    

