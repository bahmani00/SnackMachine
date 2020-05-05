using System.Data;

namespace SnackMachineApp.Infrastructure.Data
{
    public interface IDbConnectionFactory
    {
        IDbConnection GetOpenConnection();
    }
}