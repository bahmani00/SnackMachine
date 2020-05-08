using SnackMachineApp.Domain.SeedWork;
using SnackMachineApp.Domain.SharedKernel;
using SnackMachineApp.Domain.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using static SnackMachineApp.Domain.SharedKernel.Money;

namespace SnackMachineApp.Domain.SnackMachines
{
    public class SnackMachine : AggregateRoot
    {
        //reveal Slots name since it's protected for db Mappings(strong-typing)
        public static readonly string Slots_Name = nameof(Slots);

        public virtual Money MoneyInside { get; protected set; }
        public virtual decimal MoneyInTransaction { get; protected set; }

        protected virtual IList<Slot> Slots { get; set; }

        public SnackMachine():this(None)
        {

        }
        public SnackMachine(Money moneyInside)
        {
            MoneyInside = moneyInside;
            MoneyInTransaction = 0;

            //By default, 3 slots
            Slots = new List<Slot>
            {
                new Slot(this, 1),
                new Slot(this, 2),
                new Slot(this, 3),
            };
        }

        public virtual void InsertMoney(Money money)
        {
            if (!Validate(money))
                throw new InvalidOperationException();

            MoneyInTransaction += money.Amount;
            MoneyInside += money;
        }

        public virtual void ReturnMoney()
        {
            var meneyToReturn = MoneyInside.Allocate(MoneyInTransaction);
            MoneyInTransaction = 0;
            MoneyInside -= meneyToReturn;
        }

        public virtual bool CanBuySnack(int position)
        {
            ValidationMessages.Clear();

            var snackPile = GetSnackPile(position);

            if (snackPile.Quantity == 0)
            {
                ValidationMessages.Add(Constants.NoSnackAvailableToBuy);
                return false;
            }

            if (snackPile.Price > MoneyInTransaction)
            {
                ValidationMessages.Add(Constants.NotEnoughMoneyInserted);
                return false;
            }

            if (!MoneyInside.CanAllocate(snackPile.Price))
            {
                ValidationMessages.Add(Constants.NotEnoughChange);
                return false;
            }

            return true;
        }

        public virtual void BuySnack(int position)
        {
            if (!CanBuySnack(position))
                return;

            var slot = GetSlot(position);
            slot.SnackPile = slot.SnackPile.SubtaractOne();

            var change = MoneyInside.Allocate(MoneyInTransaction - slot.SnackPile.Price);
            MoneyInside -= change;
            MoneyInTransaction = 0;
        }

        public virtual void LoadSnacks(int position, SnackPile snackPile)
        {
            var slot = GetSlot(position);
            slot.SnackPile = snackPile;
        }

        private Slot GetSlot(int position)
        {
            return Slots.Single(x => x.Position == position);
        }

        public virtual IReadOnlyList<SnackPile> GetAllSnackPiles()
        {
            return Slots
                .OrderBy(x => x.Position)
                .Select(x => x.SnackPile)
                .ToList();
        }

        public virtual SnackPile GetSnackPile(int position)
        {
            return GetSlot(position).SnackPile;
        }

        public virtual void LoadMoney(Money money)
        {
            MoneyInside += money;
        }

        public virtual Money UnloadMoney()
        {
            if (MoneyInTransaction > 0)
                throw new InvalidOperationException();

            var money = MoneyInside;
            MoneyInside = Money.None;
            return money;
        }
    }
}
