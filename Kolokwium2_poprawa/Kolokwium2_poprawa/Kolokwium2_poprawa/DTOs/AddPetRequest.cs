using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kolokwium2_poprawa.DTOs
{
    public class AddPetRequest
    {
        public string BreedName { get; set; }
        public string Name { get; set; }
        public bool IsMale { get; set; }
        public DateTime DateRegistered { get; set; }
        public DateTime ApprocimatedDateOfBirth { get; set; }
    }
}
