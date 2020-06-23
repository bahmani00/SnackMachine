﻿using SnackMachineApp.Domain.SeedWork;

namespace SnackMachineApp.Domain.SnackMachines
{
    public class Snack : AggregateRoot
    {
        public static readonly Snack None = new Snack(0, "None");
        public static readonly Snack Chocolate = new Snack(1, "Chocolate");

        protected Snack()
        {
        }

        private Snack(long id, string name)
            : this()
        {
            Id = id;
            SnackName = name;
        }

        public virtual string SnackName { get; set; }
        public virtual int ImageWidth { get; set; } = 70;
    }
}
