using Kolokwium2_poprawa.DTOs;
using Kolokwium2_poprawa.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kolokwium2_poprawa.Services
{
    public interface IPetsDbService
    {
        public IList GetPetsByYear(int? year);
        public bool AddPet(AddPetRequest pet);
    }
}
