using MongoDB.Driver;

namespace Points.Infra.Data.NoSQL.Repositories
{
    public abstract class BaseRepository<T> where T : class
    {
        private const string DATABASE_NAME = "dotz";
        protected readonly IMongoClient client;
        protected readonly IMongoDatabase db;
        protected readonly IMongoCollection<T> collection;


        public BaseRepository(string collectionName)
        {
            //client = new MongoClient(Environment.GetEnvironmentVariable("ConnectionString"));
            client = new MongoClient("mongodb://dotzuser:123mudar@ds046357.mlab.com:46357/dotz");
            db = client.GetDatabase(DATABASE_NAME);
            collection = db.GetCollection<T>(collectionName);
        }
    }
}
