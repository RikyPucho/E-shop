using System.Threading.Tasks;

namespace Eshop.Data;

public interface IEshopDbSchemaMigrator
{
    Task MigrateAsync();
}
