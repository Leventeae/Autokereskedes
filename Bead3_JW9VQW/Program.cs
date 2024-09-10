using Bead3_JW9VQW;
using Autokereskedes.Classes;
using System;


public partial class Program
{
    public static void Main(string[] args)
    {
        string dealershipName = "";
        while (dealershipName == null || dealershipName.Length == 0)
        {
            Console.Write("Kérem, adja meg az autókereskedés nevét: ");
            dealershipName = Console.ReadLine();
        }

        Dealership dealership = new Dealership(dealershipName);

        Menu menu = new Menu(dealership);
        menu.Start();
    }
}