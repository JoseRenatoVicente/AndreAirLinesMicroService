using AndreAirLines.Domain.Entities;
using AndreAirLines.Domain.Repository;
using MongoDB.Driver;

namespace Identity.API.Repository
{
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(IMongoDatabase database) : base(database)
        {
        }
    }
}
