using Kolokwium2_poprawa.DTOs;
using Kolokwium2_poprawa.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kolokwium2_poprawa.Services
{
    public class EfPetsDbService : IPetsDbService
    {
        private readonly PetsDbContext _context;
        public EfPetsDbService(PetsDbContext context)
        {
            _context = context;
        }
        public bool AddPet(AddPetRequest pet)
        {
            var trans = _context.Database.BeginTransaction();
            try
            {
                if (_context.BreedTypes.Where(e => e.Name == pet.BreedName) == null)
                {
                    _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT BreedTypes ON");
                    _context.SaveChanges();
                    _context.BreedTypes.Add(new BreedType {Name = pet.BreedName, IdBreedType = _context.BreedTypes.Max(e=>e.IdBreedType)+1});
                    _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT BreedTypes OFF");
                    _context.SaveChanges();
                }
                _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Pets ON");
                _context.SaveChanges();
                _context.Pets.Add(
                    new Pet
                    {
                        IdPet = _context.Pets.Max(e => e.IdPet) + 1,
                        Name = pet.Name,
                        ApprocimateDateOfBirth = pet.ApprocimatedDateOfBirth,
                        IsMale = pet.IsMale,
                        IdBreedType = _context.BreedTypes.Where(e => e.Name == pet.BreedName).First().IdBreedType,
                        DateRegistered = DateTime.Now
                    }) ;
                _context.SaveChanges();
                _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Pets OFF");
                _context.SaveChanges();
                trans.Commit();
                return true;
            }
            catch
            {
                trans.Rollback();
                return false;
            }
        }

        public IList GetPetsByYear(int? year)
        {
            if (year != null)
            {
                var tmp = _context.Pets.Where(e => e.DateRegistered.Year == year).ToList();
                var result = _context.Volunteer_Pets.Where(e => e.Pet.DateRegistered.Year == year)
                    .Select(e => new GetPetsRequest{Pet = e.Pet, ImieVol = e.Volunteer.Name, NazwiskoVol = e.Volunteer.Surname, NrTelefonuVol = e.Volunteer.Phone})
                    .OrderBy(e=>e.Pet.DateRegistered).ToList();
                return result;
            }
            var result1 = _context.Volunteer_Pets.OrderBy(e=>e.Pet.DateRegistered).ToList();
            return result1;
        }
    }
}
