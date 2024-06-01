namespace UniversityCommunity.Data.EntityFramework.Entities
{
    public class CommunityMember
    {
        public int Id { get; set; }
        public int CommunityId { get; set; }
        public int UserTypeId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string? Password { get; set; }
        public string? StudentNumber { get; set; }
        public string? Department { get; set; }
        public string? Message { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
