namespace Backend.Entities
{
	public class BaseEntity
	{
		public string Id { get; set; }
		public DateTime CreationDate { get; set; }
		public bool IsDeleted { get; set; }
	}
}
