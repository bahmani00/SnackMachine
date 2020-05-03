namespace SnackMachineApp.Application
{
    public interface IPaymentGateway
    {
        void ChargePayment(decimal amount);
    }

    //A template for PaymentGateway
    internal class PaymentGateway : IPaymentGateway
    {
        public void ChargePayment(decimal amount)
        {
            //TODO: call the corresponding institution to declare charges
        }
    }
}
