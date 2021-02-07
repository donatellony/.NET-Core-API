using Kolokwium2_poprawa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kolokwium2_poprawa.DTOs
{
    public class GetPetsRequest
    {
        public Pet Pet { get; set; }
        public string ImieVol { get; set; }
        public string NazwiskoVol { get; set; }
        public string NrTelefonuVol { get; set; }
    }
}
