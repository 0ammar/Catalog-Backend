namespace Backend.DTOs.Items
{
	public class GetItemsDto
	{
		public string ItemNo { get; set; }
		public string Name { get; set; }
		public string FirstImage { get; set; }
        public ItemStatusDto? Status { get; set; }
    }

}
