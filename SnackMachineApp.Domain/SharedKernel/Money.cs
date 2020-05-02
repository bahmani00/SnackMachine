using Ardalis.GuardClauses;
using SnackMachineApp.Domain.Core;
using System;
using System.Collections.Generic;

namespace SnackMachineApp.Domain.SharedKernel
{
    public class Money : ValueObject<Money>
    {
        public static readonly Money None = new Money(0, 0, 0, 0, 0, 0);
        public static readonly Money Cent = new Money(1, 0, 0, 0, 0, 0);
        public static readonly Money TenCent = new Money(0, 1, 0, 0, 0, 0);
        public static readonly Money Quarter = new Money(0, 0, 1, 0, 0, 0);
        public static readonly Money Dollar = new Money(0, 0, 0, 1, 0, 0);
        public static readonly Money FiveDollar = new Money(0, 0, 0, 0, 1, 0);
        public static readonly Money TwentyDollar = new Money(0, 0, 0, 0, 0, 1);

        public virtual int OneCentCount { get; }
        public virtual int TenCentCount { get; }
        public virtual int QuarterCount { get; }
        public virtual int OneDollarCount { get; }
        public virtual int FiveDollarCount { get; }
        public virtual int TwentyDollarCount { get; }

        public virtual decimal Amount =>
            OneCentCount * 0.01m +
            TenCentCount * 0.1m +
            QuarterCount * 0.25m +
            OneDollarCount +
            FiveDollarCount * 5m +
            TwentyDollarCount * 20m;

        protected Money() { }

        public Money(int oneCentCount, int tenCentCount, int quarterCount, int oneDollarCount, int fiveDollarCount, int twentyDollarCount)
        {
            Guard.Against.Negative(oneCentCount, nameof(oneCentCount));
            Guard.Against.Negative(tenCentCount, nameof(tenCentCount));
            Guard.Against.Negative(quarterCount, nameof(quarterCount));
            Guard.Against.Negative(oneDollarCount, nameof(oneDollarCount));
            Guard.Against.Negative(fiveDollarCount, nameof(fiveDollarCount));
            Guard.Against.Negative(twentyDollarCount, nameof(twentyDollarCount));

            OneCentCount += oneCentCount;
            TenCentCount += tenCentCount;
            QuarterCount += quarterCount;
            OneDollarCount += oneDollarCount;
            FiveDollarCount += fiveDollarCount;
            TwentyDollarCount += twentyDollarCount;
        }

        internal static bool Validate(Money money)
        {
            var valids = new List<Money> { Cent, TenCent, Quarter, Dollar, FiveDollar, TwentyDollar };
            return valids.Contains(money);
        }

        protected override bool EqualsCore(Money other)
        {
            return OneCentCount == other.OneCentCount &&
                    TenCentCount == other.TenCentCount &&
                    QuarterCount == other.QuarterCount &&
                    OneDollarCount == other.OneDollarCount &&
                    FiveDollarCount == other.FiveDollarCount &&
                    TwentyDollarCount == other.TwentyDollarCount;
        }

        protected override int GetHashCodeCore()
        {
            unchecked
            {
                // 23 & 31 should be coprime
                var hash = 23;
                hash = hash * 31 ^ OneCentCount;
                hash = hash * 31 ^ TenCentCount;
                hash = hash * 31 ^ QuarterCount;
                hash = hash * 31 ^ OneDollarCount;
                hash = hash * 31 ^ FiveDollarCount;
                hash = hash * 31 ^ TwentyDollarCount;
                return hash;
            };
        }

        public override string ToString()
        {
            if (Amount < 1)
                return "¢" + (Amount * 100).ToString("0");

            return "$" + Amount.ToString("0.00");
        }

        public bool CanAllocate(decimal amount)
        {
            var money = AllocateCore(amount);
            return money.Amount == amount;
        }

        public Money Allocate(decimal amount)
        {
            if (!CanAllocate(Amount))
                throw new InvalidOperationException();

            return AllocateCore(amount);
        }

        private Money AllocateCore(decimal amount)
        {
            int twentyDollarCount = Math.Min((int)(amount / 20), TwentyDollarCount);
            amount = amount - twentyDollarCount * 20;

            int fiveDollarCount = Math.Min((int)(amount / 5), FiveDollarCount);
            amount = amount - fiveDollarCount * 5;

            int oneDollarCount = Math.Min((int)amount, OneDollarCount);
            amount = amount - oneDollarCount;

            int quarterCount = Math.Min((int)(amount / 0.25m), QuarterCount);
            amount = amount - quarterCount * 0.25m;

            int tenCentCount = Math.Min((int)(amount / 0.1m), TenCentCount);
            amount = amount - tenCentCount * 0.1m;

            int oneCentCount = Math.Min((int)(amount / 0.01m), OneCentCount);

            return new Money(
                oneCentCount,
                tenCentCount,
                quarterCount,
                oneDollarCount,
                fiveDollarCount,
                twentyDollarCount);
        }

        public static Money operator +(Money m1, Money m2) =>
            new Money(
                     m1.OneCentCount + m2.OneCentCount,
                     m1.TenCentCount + m2.TenCentCount,
                     m1.QuarterCount + m2.QuarterCount,
                     m1.OneDollarCount + m2.OneDollarCount,
                     m1.FiveDollarCount + m2.FiveDollarCount,
                     m1.TwentyDollarCount + m2.TwentyDollarCount);

        public static Money operator -(Money m1, Money m2) =>
            new Money(
                    m1.OneCentCount - m2.OneCentCount,
                    m1.TenCentCount - m2.TenCentCount,
                    m1.QuarterCount - m2.QuarterCount,
                    m1.OneDollarCount - m2.OneDollarCount,
                    m1.FiveDollarCount - m2.FiveDollarCount,
                    m1.TwentyDollarCount - m2.TwentyDollarCount);

        public static Money operator *(Money m1, int multiplier) =>
            new Money(
              m1.OneCentCount * multiplier,
              m1.TenCentCount * multiplier,
              m1.QuarterCount * multiplier,
              m1.OneDollarCount * multiplier,
              m1.FiveDollarCount * multiplier,
              m1.TwentyDollarCount * multiplier);

    }
}