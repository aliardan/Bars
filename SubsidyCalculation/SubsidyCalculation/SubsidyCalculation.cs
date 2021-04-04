using System;

namespace SubsidyCalculation
{
    class SubsidyCalculation : ISubsidyCalculation
    {
        public event EventHandler<string> OnNotify;
        public event EventHandler<Tuple<string, Exception>> OnException;

        private void ExceptionMessage(string message)
        {
            OnException?.Invoke(this, new Tuple<string, Exception>(message, new Exception(message)));
        }

        private bool DataValidator(Volume volume, Tariff tariff, out string message)
        {
            message = String.Empty;

            if (volume.HouseId != tariff.HouseId)
                message += "<HouseId> Идентификаторы домов объема и тарифа не совпадают!\n";

            if (volume.ServiceId != tariff.ServiceId)
                message += "<ServiceId> Идентификаторы услуг объема и тарифа не совпадают!\n";

            if (tariff.PeriodBegin <= volume.Month || volume.Month <= tariff.PeriodEnd)
                message += "<Month> Месяц объема не входит в период действия тарифа!\n";

            if (tariff.Value <= 0)
                message += "<Tariff Value> Значение тарифа должно быть больше нуля!\n";

            if (volume.Value < 0)
                message += "<Volume Value> Значение объема должно быть больше нуля!\n";

            return message == String.Empty;
        }

        public Charge CalculateSubsidy(Volume volume, Tariff tariff)
        {
            Charge charge = null;
            try
            {
                OnNotify?.Invoke(this, $"Расчёт начат в {DateTime.UtcNow:G}");

                if (!DataValidator(volume, tariff, out string message))
                {
                    ExceptionMessage(message);
                    return null;
                }

                charge = new Charge()
                {
                    HouseId = volume.HouseId,
                    ServiceId = volume.ServiceId,
                    Month = volume.Month,
                    Value = volume.Value * tariff.Value
                };

                OnNotify?.Invoke(this, $"Расчёт успешно завершён в {DateTime.UtcNow:G}");
            }
            catch (Exception ex)
            {
                OnException?.Invoke(this, new Tuple<string, Exception>(ex.Message, ex));
            }

            return charge;
        }
    }
}
