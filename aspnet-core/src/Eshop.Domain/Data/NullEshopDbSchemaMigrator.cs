using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Eshop.Data;

/* This is used if database provider does't define
 * IEshopDbSchemaMigrator implementation.
 */
public class NullEshopDbSchemaMigrator : IEshopDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
