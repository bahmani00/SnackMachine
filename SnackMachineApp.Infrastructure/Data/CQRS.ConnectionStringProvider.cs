namespace SnackMachineApp.Infrastructure.Data
{
    public interface IConnectionProvider
    {
        string Value { get; }
    }

    internal sealed class CommandsConnectionProvider : IConnectionProvider
    {
        public string Value { get; }

        public CommandsConnectionProvider(string value)
        {
            Value = value;
        }
    }

    internal sealed class QueriesConnectionProvider : IConnectionProvider
    {
        public string Value { get; }

        public QueriesConnectionProvider(string value)
        {
            Value = value;
        }
    }
}
