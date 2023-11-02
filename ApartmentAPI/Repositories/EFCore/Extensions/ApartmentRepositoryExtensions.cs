using Entities.Models;
using System.Reflection;
using System.Text;
using System.Linq.Dynamic.Core;

namespace Repositories.EFCore.Extensions
{
    public static class ApartmentRepositoryExtensions
    {
        public static IQueryable<Apartment> FilterApartments(this IQueryable<Apartment> apartments,
            uint minFloor, uint maxFloor) =>
            apartments.Where(apartment =>
            apartment.Floor >= minFloor &&
            apartment.Floor <= maxFloor);

        public static IQueryable<Apartment> Search(this IQueryable<Apartment> apartments, 
            string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return apartments;

            var lowerCaseTerm = searchTerm.Trim().ToLower();
            return apartments
                .Where(b => b.Status
                .ToLower()
                .Contains(searchTerm));
        }

        public static IQueryable<Apartment> Sort(this IQueryable<Apartment> apartments, 
            string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return apartments.OrderBy(b => b.Id);

            var orderQuery = OrderQueryBuilder
                .CreateOrderQuery<Apartment>(orderByQueryString);

            if (orderQuery is null)
                return apartments.OrderBy(b => b.Id);

            return apartments.OrderBy(orderQuery);
        }
    }
}
