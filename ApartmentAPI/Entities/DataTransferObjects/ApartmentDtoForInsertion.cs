using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public record ApartmentDtoForInsertion : ApartmentDtoForManipulation 
    {
        [JsonIgnore]
        public int Id { get; init; }
    }
}
