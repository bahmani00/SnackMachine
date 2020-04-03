namespace SnackMachine.Logic
{
    public class Money
    {
        public static readonly Money Null = new Money(0, 0, 0, 0, 0, 0, 0);

        public int OneCentCount { get; private set; }
        public int FiveCentCount { get; private set; }
        public int TenCentCount { get; private set; }
        public int QuarterCentCount { get; private set; }
        public int OneDollarCentCount { get; private set; }
        public int FiveDollarCentCount { get; private set; }
        public int TwentyDollarCentCount { get; private set; }

        public Money(int oneCentCount, int fiveCentCount, int tenCentCount, int quarterCentCount, int oneDollarCentCount, int fiveDollarCentCount, int twentyDollarCentCount)
        {
            this.OneCentCount += oneCentCount;
            this.FiveCentCount += fiveCentCount;
            this.TenCentCount += tenCentCount;
            this.QuarterCentCount += quarterCentCount;
            this.OneDollarCentCount += oneDollarCentCount;
            this.FiveDollarCentCount += fiveDollarCentCount;
            this.TwentyDollarCentCount += twentyDollarCentCount;
        }

        public static Money operator +(Money m1, Money m2) =>
            new Money(
                     m1.OneCentCount + m2.OneCentCount,
                     m1.FiveCentCount + m2.FiveCentCount,
                     m1.TenCentCount + m2.TenCentCount,
                     m1.QuarterCentCount + m2.QuarterCentCount,
                     m1.OneDollarCentCount + m2.OneDollarCentCount,
                     m1.FiveDollarCentCount + m2.FiveDollarCentCount,
                     m1.TwentyDollarCentCount + m2.TwentyDollarCentCount);

        public static Money operator -(Money m1, Money m2) =>
            new Money(
                    m1.OneCentCount - m2.OneCentCount,
                    m1.FiveCentCount - m2.FiveCentCount,
                    m1.TenCentCount - m2.TenCentCount,
                    m1.QuarterCentCount - m2.QuarterCentCount,
                    m1.OneDollarCentCount - m2.OneDollarCentCount,
                    m1.FiveDollarCentCount - m2.FiveDollarCentCount,
                    m1.TwentyDollarCentCount - m2.TwentyDollarCentCount);

    }
}