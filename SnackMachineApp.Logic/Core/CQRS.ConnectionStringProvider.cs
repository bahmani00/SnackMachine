namespace Logic.Utils
{
    public interface IConnectionProvider
    {
        string Value { get; }
    }

    public sealed class CommandsConnectionProvider : IConnectionProvider
    {
        public string Value { get; }

        public CommandsConnectionProvider(string value)
        {
            Value = value;
        }
    }

    public sealed class QueriesConnectionProvider : IConnectionProvider
    {
        public string Value { get; }

        public QueriesConnectionProvider(string value)
        {
            Value = value;
        }
    }
}
