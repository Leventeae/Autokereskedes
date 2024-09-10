using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Autokereskedes.Classes
{
    public class Dealership : Partner
    {
        public List<Car> Cars = new List<Car>();
        public List<Contract> Contracts = new List<Contract>();
        public class CarAlreadyExistsWithTheGivenIdentifierException : Exception { }
        public class CarNotFoundException : Exception { }
        public Dealership(string identifier) : base(identifier) { }

        public void BuyCar(Partner partner, Car car, double price)
        {
            int carIndex = GetCarIndexByIdentifier(car.Identifier);
            if (carIndex >= 0) throw new CarAlreadyExistsWithTheGivenIdentifierException();

            Contract contract = new Contract(this, ContractType.BUY, partner, car, price, DateTime.Now);
            Contracts.Add(contract);

            Cars.Add(car);
        }

        public void SellCar(Partner partner, string carIdentifier, double price)
        {
            int carIndex = GetCarIndexByIdentifier(carIdentifier);
            if (carIndex < 0) throw new CarNotFoundException();

            Contract contract = new Contract(this, ContractType.SELL, partner, Cars[carIndex], price, DateTime.Now);
            Contracts.Add(contract);

            Cars.RemoveAt(carIndex);
        }

        public double GetTotalAudiPrice()
        {
            double total = 0;
            foreach (Car car in Cars)
            {
                if (car.Brand == CarBrand.AUDI) total += car.CurrentPrice;
            }
            return total;
        }

        public bool HasYoungerSkoda(int year)
        {
            int index = 0;
            while (index < Cars.Count)
            {
                if (Cars[index].Brand == CarBrand.SKODA && Cars[index].YearOfProduction < year) break;
                index++;
            }

            return index < Cars.Count - 1;
        }

        public double GetProfit()
        {
            double profit = 0;
            foreach (Contract contract in Contracts)
            {
                if (contract.Type == ContractType.BUY) profit -= contract.Price;
                else profit += contract.Price;
            }
            return profit;
        }

        public int GetNumberOfContractsWith(Partner partner)
        {
            int count = 0;
            foreach (Contract contract in Contracts)
            {
                if (contract.Partner.Identifier == partner.Identifier) count++;
            }
            return count;
        }

        private int GetCarIndexByIdentifier(string identifier)
        {
            int index = 0;

            while (Cars.Count > index && Cars[index].Identifier != identifier) ++index;

            if (index >= Cars.Count) return -1;
            return index;
        }
    }
}