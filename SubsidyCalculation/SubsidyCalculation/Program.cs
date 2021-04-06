#nullable enable
using System;

namespace SubsidyCalculation
{
    class Program
    {
        static void Main(string[] args)
        {
            Tariff tariff = new Tariff() { ServiceId = 1, HouseId = 1, PeriodBegin = DateTime.Today, PeriodEnd = DateTime.Now, Value = 10 };
            Volume volume = new Volume() { ServiceId = 1, HouseId = 1, Month = DateTime.UtcNow, Value = 10 };
            SubsidyCalculation subsidyCalculation = new SubsidyCalculation();
            subsidyCalculation.OnNotify += Notify;
            subsidyCalculation.OnException += Exception;
            Charge charge = subsidyCalculation.CalculateSubsidy(volume, tariff);

            if (charge != null)
                Console.WriteLine($"Расчет: ServiceId - {charge.ServiceId}, " +
                                  $"HouseId - {charge.HouseId}, " +
                                  $"Month - {charge.Month}, " +
                                  $"Value - {charge.Value}");
            else
                Console.WriteLine("Ошибка");

        }

        private static void Notify(object? sender, string e)
        {
            Console.WriteLine(e);
        }
        private static void Exception(object? sender, Tuple<string, Exception> e)
        {
            Console.WriteLine(e.Item2);
        }
    }
}
