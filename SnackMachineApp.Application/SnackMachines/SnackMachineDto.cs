using SnackMachineApp.Domain.SnackMachines;
using System;
using System.Collections.Generic;

namespace SnackMachineApp.Application.SnackMachines
{
    public class SnackMachineDto
    {
        public long SnackMachineId { get; set; }

        public decimal MoneyInside =>
            OneCentCount * 0.01m +
            TenCentCount * 0.1m +
            QuarterCount * 0.25m +
            OneDollarCount +
            FiveDollarCount * 5m +
            TwentyDollarCount * 20m;

        public int OneCentCount { get; set; }
        public int TenCentCount { get; set; }
        public int QuarterCount { get; set; }
        public int OneDollarCount { get; set; }
        public int FiveDollarCount { get; set; }
        public int TwentyDollarCount { get; set; }

        public decimal MoneyInTransaction { get; set; }

        internal static SnackMachine To(SnackMachineDto snackMachine)
        {
            throw new NotImplementedException();
        }

        internal static SnackMachineDto From(SnackMachine snackMachine)
        {
            throw new NotImplementedException();
        }

        public List<SlotDto> Slots { get; set; }
    }

    public class SlotDto
    {
        public long SlotId { get; set; }
        public long SnackMachineId { get; set; }
        //public SnackMachineDto SnackMachine { get; set; }

        public int Position { get; set; }
        //public SnackPileDto SnackPile { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public long SnackId { get; set; }
        public string SnackName { get; set; }
        public int ImageWidth { get; set; }
    }

    //public class SlotDto
    //{
    //    public long SlotId { get; set; }
    //    public long SnackMachineId { get; set; }
    //    //public SnackMachineDto SnackMachine { get; set; }

    //    public string Name { get; set; }
    //    public int Position { get; set; }

    //    //public SnackPileDto SnackPile { get; set; }
    //    public int Quantity { get; set; }
    //    public decimal Price { get; set; }
    //}


    //public class SnackPileDto
    //{
    //    public long SnackId { get; set; }

    //    //public SnackDto Snack { get; set; }

    //    public int Quantity { get; set; }
    //    public decimal Price { get; set; }
    //}

    //public class SnackDto
    //{
    //    public long SnackId { get; set; }
    //    public string Name { get; set; }
    //    public int ImageWidth { get; set; }
    //}

}
