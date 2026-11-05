using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Hospital_Manager.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data_Hospital_Manager.Configurations
{
    public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.HasData(
                 new Patient
                 {
                     Id = 1,
                     FirstName = "Petar",
                     LastName = "Ivanov",
                     Email = "petar.ivanov@gmail.com",
                     PhoneNumber = "0899123456"
                 },
                 new Patient
                 {
                     Id = 2,
                     FirstName = "Anna",
                     LastName = "Petrova",
                     Email = "anna.petrova@gmail.com",
                     PhoneNumber = "0899234567"
                 },
                 new Patient
                 {
                     Id = 3,
                     FirstName = "Nikolay",
                     LastName = "Georgiev",
                     Email = "nikolay.georgiev@gmail.com",
                     PhoneNumber = "0899345678"
                 }
            );
        }
    }
}
