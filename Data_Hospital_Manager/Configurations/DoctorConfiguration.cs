using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Data_Hospital_Manager.Entities;

namespace Data_Hospital_Manager.Configurations
{
    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.HasData(
                new Doctor
                {
                    Id = 1,
                    FirstName = "Ivan",
                    LastName = "Petrov",
                    Specialty = "Cardiology",
                    Email = "ivan.petrov@hospital.bg",
                    PhoneNumber = "0888123456",
                    HospitalId = 1
                },
                new Doctor
                {
                    Id = 2,
                    FirstName = "Maria",
                    LastName = "Ivanova",
                    Specialty = "Neurology",
                    Email = "maria.ivanova@hospital.bg",
                    PhoneNumber = "0888234567",
                    HospitalId = 1
                },
                new Doctor
                {
                    Id = 3,
                    FirstName = "Georgi",
                    LastName = "Dimitrov",
                    Specialty = "Surgery",
                    Email = "georgi.dimitrov@hospital.bg",
                    PhoneNumber = "0888345678",
                    HospitalId = 2
                }
            );
        }
    }
}
