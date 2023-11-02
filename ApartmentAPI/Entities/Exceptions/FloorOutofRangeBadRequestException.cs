namespace Entities.Exceptions
{
    public class FloorOutofRangeBadRequestException : BadRequestException
    {
        public FloorOutofRangeBadRequestException()
            : base("Maximum Floor should be less than 1000 and greater than 10.")
        {

        }
    }

}
