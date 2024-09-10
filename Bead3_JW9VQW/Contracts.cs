using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Autokereskedes.Classes
{
    public enum ContractType
    {
        BUY,
        SELL
    }
    public class Contract
    {
        public Dealership Dealership { get; set; }
        public ContractType Type { get; set; }
        public Partner Partner { get; set; }
        public Car Car { get; set; }
        public double Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public Contract(Dealership dealership, ContractType type, Partner partner, Car car, double price, DateTime createdAt)
        {
            Dealership = dealership;
            Type = type;
            Partner = partner;
            Car = car;
            Price = price;
            CreatedAt = createdAt;
        }
        public override string ToString()
        {
            return $"{Dealership};{Type};{Partner};{Car.Identifier};{Price};{CreatedAt}";
        }
    }
}