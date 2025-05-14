namespace Backend.Entities
{
    public class Item
    {
        public string ItemNo { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> Images { get; set; }
        public string StatusId { get; set; }
        public LookupItem? Status { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsDeleted { get; set; }
        public string GroupId { get; set; }
        public string SubOneId { get; set; }
        public string? SubTwoId { get; set; }
        public string? SubThreeId { get; set; }
    }
}
