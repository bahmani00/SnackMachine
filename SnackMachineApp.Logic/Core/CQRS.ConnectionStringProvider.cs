namespace Logic.Utils
{
    public interface IConnectionStringProvider
    {
        string Value { get; }
    }

    public sealed class CommandsConnectionStringProvider : IConnectionStringProvider
    {
        public string Value { get; }

        public CommandsConnectionStringProvider(string value)
        {
            Value = value;
        }
    }

    public sealed class QueriesConnectionStringProvider : IConnectionStringProvider
    {
        public string Value { get; }

        public QueriesConnectionStringProvider(string value)
        {
            Value = value;
        }
    }
}
