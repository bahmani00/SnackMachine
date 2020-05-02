using Ardalis.GuardClauses;
using SnackMachineApp.Domain.Core;

namespace SnackMachineApp.Domain.SnackMachines
{
    public class SnackPile : ValueObject<SnackPile>
    {
        public static readonly SnackPile Empty = new SnackPile(Snack.None, 0, 0m);

        public virtual Snack Snack { get; }

        public int Quantity { get; }
        public decimal Price { get; }

        protected SnackPile()
        {
        }

        public SnackPile(Snack snack, int quantity, decimal price)
        {
            Guard.Against.Null(snack, nameof(snack));
            Guard.Against.Negative(quantity, nameof(quantity));
            Guard.Against.Negative(price, nameof(price));
            //TODO: Price cannot be less than 0.01
            //Guard.Against.OutOfRange(price, nameof(price), 0.01m, 0);

            Snack = snack;
            Quantity = quantity;
            Price = price;
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
                hash = hash * 31 ^ Snack.GetHashCode();
                hash = hash * 31 ^ Quantity;
                hash = hash * 31 ^ Price.GetHashCode();
                return hash;
            };
        }

        public SnackPile SubtaractOne()
        {
            return new SnackPile(Snack, Quantity - 1, Price);
        }
    }
}
