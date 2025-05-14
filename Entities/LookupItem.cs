namespace Backend.Entities
{
	public class LookupItem : BaseEntity
	{
		public string Name { get; set; }
		public string Code { get; set; }
		public string? IconPath { get; set; }
		public string LookupTypeId { get; set; }
	}
}
