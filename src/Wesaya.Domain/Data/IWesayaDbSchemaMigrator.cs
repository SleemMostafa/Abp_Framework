using System.Threading.Tasks;

namespace Wesaya.Data;

public interface IWesayaDbSchemaMigrator
{
    Task MigrateAsync();
}
