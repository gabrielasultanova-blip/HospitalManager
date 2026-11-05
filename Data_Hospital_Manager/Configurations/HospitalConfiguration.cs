using Data_Hospital_Manager.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Hospital_Manager.Configurations
{
    public class HospitalConfiguration : IEntityTypeConfiguration<Hospital>
    {
        public void Configure(EntityTypeBuilder<Hospital> builder)
        {
            builder.HasData(
                new Hospital
                {
                    Id = 1,
                    Name = "City Hospital",
                    Address = "Plovdiv, Bulgaria",
                    Email = "city@hospital.bg",
                    PhoneNumber = "032123456",
                    BedsCount = 300,
                    CreatedOn = new DateTime(2025, 1, 1),
                    IsDeleted = false
                },
                new Hospital
                {
                    Id = 2,
                    Name = "Central Hospital",
                    Address = "Sofia, Bulgaria",
                    Email = "central@hospital.bg",
                    PhoneNumber = "029876543",
                    BedsCount = 500,
                    CreatedOn = new DateTime(2025, 2, 1),
                    IsDeleted = false
                },
                new Hospital
                {
                    Id = 3,
                    Name = "University Hospital",
                    Address = "Stara Zagora, Bulgaria",
                    Email = "university@hospital.bg",
                    PhoneNumber = "042123456",
                    BedsCount = 250,
                    CreatedOn = new DateTime(2025, 3, 1),
                    IsDeleted = false
                }
            );
        }
    }
}
