namespace Entities.RequestFeatures
{
    public class ApartmentParameters : RequestParameters
	{
		public uint MinPrice { get; set; }
		public uint MaxPrice { get; set; } = 1000;
		public bool ValidPriceRange => MaxPrice > MinPrice;

		public String? SearchTerm { get; set; }

		public ApartmentParameters()
		{
			OrderBy = "id";
		}
	}
}
