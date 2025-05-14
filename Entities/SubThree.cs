namespace Backend.Entities
{
    public class SubThree : BaseEntity
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public string GroupId { get; set; }
        public string SubOneId { get; set; }
        public string? SubTwoId { get; set; }
    }
}
