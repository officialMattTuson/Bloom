using MongoDB.Driver;
using Bloom.Core.Models;
using Microsoft.Extensions.Options;
using Bloom.Persistence.Settings;

namespace Bloom.Persistence
{
  public class BloomDbContext
  {
    private readonly IMongoDatabase _database;

    public BloomDbContext(IOptions<MongoDbSettings> settings)
    {
      var client = new MongoClient(settings.Value.ConnectionString);
      _database = client.GetDatabase(settings.Value.DatabaseName);// You can customize this DB name
    }

    // 👇 This exposes the database so repositories can access collections
    public IMongoDatabase Database => _database;
  }
}