using Data_Hospital_Manager;
using Data_Hospital_Manager.Entities;
using Hospital_Manager_Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_Manager_Core.Services
{
    public class HospitalService : IHospitalService
    {
        private readonly HospitalDbContext context_new;

        public HospitalService(HospitalDbContext context)
        {
            context_new = context;
        }

        public Task AddHospitalAsync(Hospital hospital)
        {
            throw new NotImplementedException();
        }

        public Task DeleteHospitalAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Hospital>> GetAllHospitalsAsync()
        {
            return await context_new.Hospitals.Where(h => !h.IsDeleted).ToListAsync();
        }

        public Task<Hospital?> GetHospitalByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateHospitalAsync(Hospital hospital)
        {
            throw new NotImplementedException();
        }
    }
}
