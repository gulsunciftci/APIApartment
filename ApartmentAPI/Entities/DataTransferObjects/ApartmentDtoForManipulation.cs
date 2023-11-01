using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public abstract record ApartmentDtoForManipulation
    {
        

        [Required(ErrorMessage = "Status is a required field.")]
        public string Status { get; init; }

        [Required(ErrorMessage = "Type is a required field.")]
        public string Type { get; init; }
        
        [Required(ErrorMessage = "Floor is a required field.")]
        public int Floor { get; init; }
       
        [Required(ErrorMessage = "No is a required field.")]
        public int No { get; init; }
    }
}
