using UniversityCommunity.Data.EntityFramework.Entities;

namespace UniversityCommunity.Data.Models.PageModel
{
    public class UserforListPage
    {
        public List<User> Users { get; set; }
    }

    public class UserforPage
    {
        public User User { get; set; }
        public int UserTypeId { get; set; }
        public string UserTypeName { get; set; }
    }
}

