using System.Collections.Generic;
using System.Linq;

namespace SnackMachineApp.Logic.Utils
{
    public static class Helper
    {
        public static string Project(this IList<string> list)
        {
            return string.Join(System.Environment.NewLine, list.ToArray());
        }
    }

    public static class Constants
    {
        public static readonly string NotEnoughMoneyInserted = "Not enough money inserted.";
        public static readonly string NotEnoughChange = "Not enough change in the machine.";
        public static readonly string NoSnackAvailableToBuy = "The snack pile is empty.";

        public static readonly string InvalidAmount = "Invalid amount";
    }
}