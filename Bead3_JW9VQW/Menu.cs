using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using Autokereskedes.Classes;
using static Autokereskedes.Classes.Dealership;

namespace Bead3_JW9VQW
{
    public class Menu
    {
        private Dealership Dealership;
        private List<Partner> Partners = new List<Partner>();

        public Menu(Dealership dealership)
        {
            this.Dealership = dealership;
        }

        public void Start()
        {
            bool running = true;
            while (running)
            {
                Console.Clear();
                Console.WriteLine("Válasszon a következő lehetőségek közül:");
                Console.WriteLine("\t(1) Partner létrehozása");
                Console.WriteLine("\t(2) Partnerek kiiratása");
                Console.WriteLine();
                Console.WriteLine("\t(3) Vételi szerződés írása autóra");
                Console.WriteLine("\t(4) Eladási szerződés írása autóra");
                Console.WriteLine("\t(5) Szerződések kiiratása");
                Console.WriteLine();
                Console.WriteLine("\t(6) Audik értéke összesen");
                Console.WriteLine("\t(7) Van-e adott évnél fiatalabb skoda");
                Console.WriteLine("\t(8) Profit");
                Console.WriteLine("\t(9) Szerződések mennyisége adott partnerrel");
                Console.WriteLine();
                Console.WriteLine("\t(0) Kilépés");
                Console.WriteLine();
                Console.Write("Választott opció: ");
                string input = Console.ReadLine();
                int choice;
                if (int.TryParse(input, out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            AddPartner();
                            Console.WriteLine("\nNyomja le bármelyik billentyűt a folytatáshoz...");
                            Console.ReadKey();
                            break;
                        case 2:
                            PrintPartners();
                            Console.WriteLine("\nNyomja le bármelyik billentyűt a folytatáshoz...");
                            Console.ReadKey();
                            break;
                        case 3:
                            BuyCarFromDealership();
                            Console.WriteLine("\nNyomja le bármelyik billentyűt a folytatáshoz...");
                            Console.ReadKey();
                            break;
                        case 4:
                            SellCarFromDealership();
                            Console.WriteLine("\nNyomja le bármelyik billentyűt a folytatáshoz...");
                            Console.ReadKey();
                            break;
                        case 5:
                            PrintContracts();
                            Console.WriteLine("\nNyomja le bármelyik billentyűt a folytatáshoz...");
                            Console.ReadKey();
                            break;
                        case 6:
                            Task1();
                            Console.WriteLine("\nNyomja le bármelyik billentyűt a folytatáshoz...");
                            Console.ReadKey();
                            break;
                        case 7:
                            Task2();
                            Console.WriteLine("\nNyomja le bármelyik billentyűt a folytatáshoz...");
                            Console.ReadKey();
                            break;
                        case 8:
                            Task3();
                            Console.WriteLine("\nNyomja le bármelyik billentyűt a folytatáshoz...");
                            Console.ReadKey();
                            break;
                        case 9:
                            Task4();
                            Console.WriteLine("\nNyomja le bármelyik billentyűt a folytatáshoz...");
                            Console.ReadKey();
                            break;
                        case 0:
                            Console.WriteLine("Kilépés...");
                            running = false;
                            break;
                        default:
                            Console.WriteLine("Érvénytelen választás. Próbálja újra!");
                            Console.ReadKey();
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Érvénytelen választás. Próbálja újra!");
                    Console.ReadKey();
                }
            }
        }

        private void AddPartner()
        {
            string identifier = String.Empty;
            bool existing = false;

            do
            {
                existing = false;
                Console.Write("Adjon meg egy Partner azonosítót: ");
                identifier = Console.ReadLine();

                foreach (Partner partner in Partners)
                {
                    if (partner.Identifier == identifier)
                    {
                        existing = true;
                        Console.WriteLine("Már létezik ilyen azonosítóval Partner!");
                        break;
                    }
                }

                if (!existing)
                {
                    Partner newPartner = new Partner(identifier);
                    Partners.Add(newPartner);
                    Console.WriteLine("Partner hozzáadva: " + identifier);
                }

            } while (existing);
        }

        private void PrintPartners()
        {
            Console.WriteLine($"Partnerek listája ({Partners.Count}): ");
            foreach (Partner partner in Partners)
            {
                Console.WriteLine($"\t{partner}");
            }
        }

        private void BuyCarFromDealership()
        {
            try
            {
                if (Partners.Count <= 0) throw new Exception("Először adjon meg partnereket, mielőtt autót próbálna vásárolni!");
                // Console-ról beolvasás és validáció
                Console.Write("Adja meg az autó azonosítóját: ");
                string carIdentifier = Console.ReadLine();

                Console.Write("Adja meg az autó márka azonosítóját (0 - Audi, 1 - Mazda, 2 - Skoda): ");
                int brandId = Convert.ToInt32(Console.ReadLine());
                if (!Enum.IsDefined(typeof(CarBrand), brandId))
                {
                    throw new ArgumentException("Érvénytelen autó márka azonosító!");
                }
                CarBrand brand = (CarBrand)brandId;

                Console.Write("Adja meg az autó gyártási évét: ");
                int year = Convert.ToInt32(Console.ReadLine());

                Console.Write("Adja meg az autó árát: ");
                double carPrice = Convert.ToDouble(Console.ReadLine());

                Console.Write("Adja meg a partner azonosítóját: ");
                string partnerIdentifier = Console.ReadLine();

                // Partner azonosítójának ellenőrzése a Partners listában
                Partner partner = Partners.FirstOrDefault(p => p.Identifier == partnerIdentifier);
                if (partner == null)
                {
                    throw new Exception("Nincs ilyen nevű partner a listában.");
                }

                // CarFuel típus beolvasása és ellenőrzése
                Console.Write("Adja meg az autó üzemanyag típusát (0 - Benzin, 1 - Dízel, 2 - Elektromos, 3 - Hibrid): ");
                int fuelTypeId = Convert.ToInt32(Console.ReadLine());
                if (!Enum.IsDefined(typeof(CarFuel), fuelTypeId))
                {
                    throw new ArgumentException("Érvénytelen üzemanyag típus azonosító!");
                }
                CarFuel fuelType = (CarFuel)fuelTypeId;

                Console.Write("Adja meg az autó véleti árát: ");
                double price = Convert.ToDouble(Console.ReadLine());

                // Autó vásárlása a Dealership osztály BuyCar metódusával
                Car car = new Car(carIdentifier, brand, year, fuelType, carPrice);
                Dealership.BuyCar(partner, car, price);

                Console.WriteLine("Az autó sikeresen vásárolva!");
            }
            catch (CarAlreadyExistsWithTheGivenIdentifierException)
            {
                Console.WriteLine("Hiba: Az adott azonosítóval már létezik autó az autókereskedésben!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hiba: " + ex.Message);
            }
        }
        private void SellCarFromDealership()
        {
            try
            {
                if (Partners.Count <= 0) throw new Exception("Először adjon meg partnereket, mielőtt autót próbálna eladni!");

                Console.Write("Adja meg az autó azonosítóját: ");
                string carIdentifier = Console.ReadLine();

                Car car = Dealership.Cars.FirstOrDefault(c => c.Identifier == carIdentifier);
                if (car == null)
                {
                    throw new CarNotFoundException();
                }

                Console.Write("Adja meg a partner azonosítóját: ");
                string partnerIdentifier = Console.ReadLine();

                Partner partner = Partners.FirstOrDefault(p => p.Identifier == partnerIdentifier);
                if (partner == null)
                {
                    throw new Exception("Nem található ilyen nevű partner a listában.");
                }

                Console.Write("Adja meg az eladási árat: ");
                double price = Convert.ToDouble(Console.ReadLine());

                Dealership.SellCar(partner, car.Identifier, price);

                Console.WriteLine("Az autó sikeresen eladva!");
            }
            catch (CarNotFoundException)
            {
                Console.WriteLine("Hiba: Az adott azonosítóval nem található autó az autókereskedésben!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hiba: " + ex.Message);
            }
        }

        private void PrintContracts()
        {
            Console.WriteLine($"Szerződések listája ({Dealership.Contracts.Count}): ");
            foreach (Autokereskedes.Classes.Contract contract in Dealership.Contracts)
            {
                Console.WriteLine($"\t{contract}");
            }
        }

        private void Task1()
        {
            Console.WriteLine($"A kereskedésben találhaó Audik értéke összesen: {Dealership.GetTotalAudiPrice()}");
        }

        private void Task2()
        {
            Console.Write("Kérem az évszámot: ");
            int year = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("A kereskedésben " + (Dealership.HasYoungerSkoda(year) ? "van " : "nincs ") + year + " előtt legyártott Skoda.");
        }

        private void Task3()
        {
            Console.WriteLine($"Az autókereskedés jelenlegi bevétele: {Dealership.GetProfit()}");
        }

        private void Task4()
        {
            Console.Write("Kérem a partner nevét: ");
            string partnerName = Console.ReadLine();
            Partner partner = Partners.FirstOrDefault(p => p.Identifier == partnerName);

            if (partner == null)
            {
                Console.WriteLine("Nincs ilyen nevű partner a listában.");
            }
            else
            {
                Console.WriteLine($"{partner.Identifier} nevű partnerrel összesen {Dealership.GetNumberOfContractsWith(partner)}x történt kereskedés.");
            }
        }
    }
}