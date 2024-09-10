using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autokereskedes.Classes
{
    public enum CarBrand
    {
        AUDI,
        MAZDA,
        SKODA
    }
    public enum CarFuel
    {
        PETROL,
        DIESEL,
        ELECTRIC,
        HYBRID
    }
    public class Car
    {
        public string Identifier { get; set; }
        public CarBrand Brand { get; set; }
        public int YearOfProduction { get; set; }
        public CarFuel Fuel { get; set; }
        public double Price { get; set; }
        public double CurrentPrice { get; set; }

        private static readonly double[,] Factor = new double[3, 4]
        {
            {1, 0.9, 1.2, 1.3},
            {2, 2, 2.5, 2.3},
            {3, 3.1, 3.8, 4}
        };

        public Car(string identifier, CarBrand brand, int yearOfProduction, CarFuel fuel, double price)
        {
            Identifier = identifier;
            Brand = brand;
            YearOfProduction = yearOfProduction;
            Fuel = fuel;
            Price = price;

            CurrentPrice = Math.Round(Price * (100 - (DateTime.Now.Year - YearOfProduction)) / (100 * GetFactor()));
        }

        public override string ToString()
        {
            return $"{Identifier};{Brand};{YearOfProduction};{Fuel};{Price};{CurrentPrice}";
        }

        private double GetFactor()
        {
            int carIndex = 0;
            int fuelIndex = 0;

            switch (Brand)
            {
                case CarBrand.MAZDA:
                    carIndex = 1;
                    break;
                case CarBrand.SKODA:
                    carIndex = 2;
                    break;
                default:
                    break;
            }

            switch (Fuel)
            {
                case CarFuel.DIESEL:
                    fuelIndex = 1;
                    break;
                case CarFuel.ELECTRIC:
                    fuelIndex = 2;
                    break;
                case CarFuel.HYBRID:
                    fuelIndex = 3;
                    break;
                default:
                    break;
            }

            return Factor[carIndex, fuelIndex];
        }
    }
}