using AndreAirLines.Domain.Entities.Base;
using AndreAirLines.Domain.Settings;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AndreAirLines.Domain.Repository
{
    public class BaseRepository<TEntity>: IBaseRepository<TEntity> where TEntity : EntityBase
    {
        private readonly string _database;
        private readonly IMongoClient _mongoClient;

        private readonly string _collection = typeof(TEntity).Name;

        public BaseRepository(IAppSettings appSettings)
        {
            _mongoClient = new MongoClient(appSettings.ConnectionString);
            _database = appSettings.DatabaseName;
        }

        public IMongoCollection<TEntity> Collection =>
         _mongoClient.GetDatabase(_database).GetCollection<TEntity>(_collection);

        public async Task<IEnumerable<TEntity>> GetAllAsync() =>
            await Collection.Find(entity => true).ToListAsync();

        public async Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> where) =>
            await Collection.Find(where).ToListAsync();

        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> where) =>
            await Collection.Find(where).FirstOrDefaultAsync();

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await Collection.InsertOneAsync(entity);
            return entity;
        }
        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            await Collection.ReplaceOneAsync(c => c.Id == entity.Id, entity);
            return entity;
        }

        public async Task RemoveAsync(TEntity entityIn) =>
            await Collection.DeleteOneAsync(entity => entity.Id == entityIn.Id);

        public async Task RemoveAsync(string id) =>
            await Collection.DeleteOneAsync(entity => entity.Id == id);
    }
}
