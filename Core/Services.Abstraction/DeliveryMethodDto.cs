namespace Services.Abstraction
{
	public class DeliveryMethodDto
	{
		public string ShortName { get; set; }
		public string Description { get; set; }
		public string DeliveryTime { get; set; }
		public decimal Cost { get; set; }
	}
}