using Ardalis.GuardClauses;

namespace SnackMachineApp.Logic
{
    public class SnackPile : ValueObject<SnackPile>
    {
        public Snack Snack { get; }
        public int Quantity { get; }
        public decimal Price { get; }

        protected SnackPile()
        {
        }

        public SnackPile(Snack snack, int quantity, decimal price)
        {
            Guard.Against.Null(snack, nameof(snack));
            Guard.Against.Negative(quantity, nameof(quantity));
            Guard.Against.OutOfRange(price, nameof(price), 0.01m, decimal.MaxValue);

            this.Snack = snack;
            this.Quantity = quantity;
            this.Price = price;
        }

        protected override bool EqualsCore(SnackPile other)
        {
            return Snack == other.Snack &&
                Quantity == other.Quantity &&
                Price == other.Price;
        }

        protected override int GetHashCodeCore()
        {
            unchecked
            {
                // 23 & 31 should be coprime
                var hash = 23;
                hash = (hash * 31) ^ Snack.GetHashCode();
                hash = (hash * 31) ^ Quantity;
                hash = (hash * 31) ^ Price.GetHashCode();
                return hash;
            };
        }

        public SnackPile SubtaractOne()
        {
            return new SnackPile(Snack, Quantity - 1, Price);
        }
    }
}
