using CustomerService.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CustomerService.BL
{
    public class MongoActions : IMongoActions
    {
        IMongoCollection<Customer> _customerCollection;
        public MongoActions()
        {
            var dbHost = Environment.GetEnvironmentVariable("dbHost");//we can have an interface for Environemtn as well to make testing easier
            var dbName = Environment.GetEnvironmentVariable("dbName");
            var connectionString = $"mongodb://{dbHost}:27017/{dbName}";

            var connectionURL = new MongoUrl(connectionString);
            var mongoClient = new MongoClient(connectionURL);
            var db = mongoClient.GetDatabase(connectionURL.DatabaseName);
            _customerCollection = db.GetCollection<Customer>("customer");
        }
        public async Task<bool> DeleteCustomer(string id)
        {
            var filter = Builders<Customer>.Filter.Eq(x => x.Id, id);
            var result = await _customerCollection.DeleteOneAsync(filter);
            return result.DeletedCount > 0;
        }

        public async Task<Customer> GetCustomerById(string id)
        {
            var filterDefinition = Builders<Customer>.Filter.Eq(x => x.Id, id);
            return await _customerCollection.Find<Customer>(filterDefinition).SingleOrDefaultAsync();
        }

        public async Task<List<Customer>> GetCustomers()
        {

            return await _customerCollection.Find<Customer>(Builders<Customer>.Filter.Empty).ToListAsync();
        }

        public async Task InsertCustomer(Customer customer)
        {
            await _customerCollection.InsertOneAsync(customer);
        }

        public async Task<bool> UpdateCustomer(Customer customer)
        {
            var filter = Builders<Customer>.Filter.Eq(x=>x.Id, customer.Id);
            var update = Builders<Customer>.Update.Set(x=>x.Address, customer.Address).Set(x=>x.Name,customer.Name);
            var updateCount = await _customerCollection.UpdateOneAsync(filter, update);
            return updateCount.ModifiedCount > 0;
        }
    }
}
