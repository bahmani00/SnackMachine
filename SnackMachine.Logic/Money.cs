﻿using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;

namespace SnackMachine.Logic
{
    public class Money: ValueObject<Money>
    {
        public static readonly Money None = new Money(0, 0, 0, 0, 0, 0);
        public static readonly Money Cent = new Money(1, 0, 0, 0, 0, 0);
        public static readonly Money TenCent = new Money(0, 1, 0, 0, 0, 0);        
        public static readonly Money Quarter = new Money(0, 0, 1, 0, 0, 0);
        public static readonly Money Dollar = new Money(0, 0, 0, 1, 0, 0);
        public static readonly Money FiveDollar = new Money(0, 0, 0, 0, 1, 0);
        public static readonly Money TwentyDollar = new Money(0, 0, 0, 0, 0, 1);

        public int OneCentCount { get; }
        public int TenCentCount { get; }
        public int QuarterCount { get; }
        public int OneDollarCount { get; }
        public int FiveDollarCount { get; }
        public int TwentyDollarCount { get; }

        public decimal Amount =>
            OneCentCount * 0.01m +
            TenCentCount * 0.1m +
            QuarterCount * 0.25m +
            OneDollarCount +
            FiveDollarCount * 5m +
            TwentyDollarCount * 20m;

        public Money(int oneCentCount, int tenCentCount, int quarterCount, int oneDollarCount, int fiveDollarCount, int twentyDollarCount)
        {
            Guard.Against.Negative(oneCentCount, nameof(oneCentCount));
            Guard.Against.Negative(tenCentCount, nameof(tenCentCount));
            Guard.Against.Negative(quarterCount, nameof(quarterCount));
            Guard.Against.Negative(oneDollarCount, nameof(oneDollarCount));
            Guard.Against.Negative(fiveDollarCount, nameof(fiveDollarCount));
            Guard.Against.Negative(twentyDollarCount, nameof(twentyDollarCount));

            this.OneCentCount += oneCentCount;
            this.TenCentCount += tenCentCount;
            this.QuarterCount += quarterCount;
            this.OneDollarCount += oneDollarCount;
            this.FiveDollarCount += fiveDollarCount;
            this.TwentyDollarCount += twentyDollarCount;
        }

        internal static bool Validate(Money money)
        {
            var valids = new List<Money> { Cent, TenCent, Quarter, Dollar, FiveDollar, TwentyDollar };
            return valids.Contains(money);
        }

        protected override bool EqualsCore(Money other)
        {
            return this.OneCentCount == other.OneCentCount &&
                    this.TenCentCount == other.TenCentCount &&
                    this.QuarterCount == other.QuarterCount &&
                    this.OneDollarCount == other.OneDollarCount &&
                    this.FiveDollarCount == other.FiveDollarCount &&
                    this.TwentyDollarCount == other.TwentyDollarCount;
        }

        protected override int GetHashCodeCore()
        {
            unchecked
            {
                // 23 & 31 should be coprime
                var hash = 23;
                hash = (hash * 31) + OneCentCount;
                hash = (hash * 31) + TenCentCount;
                hash = (hash * 31) + QuarterCount;
                hash = (hash * 31) + OneDollarCount;
                hash = (hash * 31) + FiveDollarCount;
                hash = (hash * 31) + TwentyDollarCount;
                return hash;
            };
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

        public static implicit operator decimal(Money money)
        {
            return money.Amount;
        }

    }
}