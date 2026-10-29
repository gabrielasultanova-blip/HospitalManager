using Data_Hospital_Manager.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Hospital_Manager.Configuration
{
    public class HospitalConfiguration : IEntityTypeConfiguration<Hospital>
    {
        public void Configure(EntityTypeBuilder<Hospital> builder)
        {
            new Hospital
            {
                Id = 1,
                Name = "ATAL Kazanlak",
                Address = "Kazanlak",
                Email = "hospital1@example.com",
                PhoneNumber = "0880123456",
                BedsCount = 200,
                CreatedOn = new DateTime(2025, 1, 1),
                IsDeleted = false
            };
            new Hospital
            {
                Id = 2,
                Name = "UMBAL Stara Zagora",
                Address = "Stara Zagora",
                Email = "hospital2@example.com",
                PhoneNumber = "0880654321",
                BedsCount = 150,
                CreatedOn = new DateTime(2026, 1, 1),
                IsDeleted = false
            };
        }
    }
}
