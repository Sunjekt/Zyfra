using ZyfraServer.Models;
using System.Diagnostics.Metrics;

namespace ZyfraServer.Data
{
    public static class ModelsManagerSeed
    {
        public static async Task SeedAsync(ModelsManager context)
        {
            try
            {
                context.Database.EnsureCreated();

                if (context.ZyfraData.Any())
                {
                    return;
                }
                var values = new ZyfraData[]
                {
                    new ZyfraData{Value = 1},
                    new ZyfraData{Value = 4},
                    new ZyfraData{Value = 7},
                    new ZyfraData{Value = 2},
                    new ZyfraData{Value = 13},
                    new ZyfraData{Value = 56},
                    new ZyfraData{Value = 45},
                    new ZyfraData{Value = 5},
                    new ZyfraData{Value = 101},
                    new ZyfraData{Value = 3},
                };
                foreach (ZyfraData v in values)
                {
                    context.ZyfraData.Add(v);
                }
                await context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
