using AndreAirLines.Domain.Entities;
using AndreAirLines.Domain.Repository;
using MongoDB.Driver;

namespace Identity.API.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IMongoDatabase database) : base(database)
        {
        }
    }
}
