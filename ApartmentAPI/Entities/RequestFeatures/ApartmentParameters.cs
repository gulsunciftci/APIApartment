namespace Entities.RequestFeatures
{
    public class ApartmentParameters : RequestParameters
	{
		public uint MinFloor { get; set; }
		public uint MaxFloor { get; set; } = 1000;
		public bool ValidFloorRange => MaxFloor > MinFloor;


        public String? SearchTerm { get; set; }

		public ApartmentParameters()
		{
			OrderBy = "id";
		}
	}
}
