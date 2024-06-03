namespace UniversityCommunity.Data.EntityFramework.Entities
{
    public class CommunityEvent
    {
        public int Id { get; set; }
        public int CommunityId { get; set; }
        public int EventTypeId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Speaker { get; set; }
        public string Panelist { get; set; }
        public DateTime? EventDate { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public string Wants { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public EventType EventType { get; set; }
        public Community Community { get; set; }
    }
}