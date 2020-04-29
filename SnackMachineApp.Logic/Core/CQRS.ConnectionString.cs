namespace Logic.Utils
{
    public interface IConnectionString
    {
        string Value { get; }
    }

    public sealed class CommandsConnectionString : IConnectionString
    {
        public string Value { get; }

        public CommandsConnectionString(string value)
        {
            Value = value;
        }
    }

    public sealed class QueriesConnectionString : IConnectionString
    {
        public string Value { get; }

        public QueriesConnectionString(string value)
        {
            Value = value;
        }
    }
}
