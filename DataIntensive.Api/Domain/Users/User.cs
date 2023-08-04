using DataIntensive.Api.Infrastructure.Data.EF.Maps;

namespace DataIntensive.Api.Domain.Users
{
    public class User : BaseEntity
    {
        public string Username { get; set; } = default!;
    }
}