using SnackMachineApp.Logic.Core;
using System;
using System.Linq;

namespace SnackMachineApp.Logic.Utils
{
    public static class Helper
    {
        public static string Project(this Entity entity)
        {
            var validateions = entity.ValidationMessages.ToArray();
            entity.ValidationMessages.Clear();
            return string.Join(Environment.NewLine, validateions);
        }

        public static Type GetInterfaceOfGenericType(Type typeInstance, Type genericType)
        {
            return typeInstance.GetInterfaces()
                .Where(i => i.IsGenericType
                    && i.GetGenericTypeDefinition() == genericType)
                .Single().GetGenericTypeDefinition();
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