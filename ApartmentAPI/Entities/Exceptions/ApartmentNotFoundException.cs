namespace Entities.Exceptions
{
    public sealed class ApartmentNotFoundException : NotFoundException
    {
        public ApartmentNotFoundException(int id) 
            : base($"The apartments with id : {id} could not found.")
        {
        }
    }
}
