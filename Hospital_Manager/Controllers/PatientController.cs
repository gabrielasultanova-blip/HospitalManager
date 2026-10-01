using Microsoft.AspNetCore.Mvc;
using Data_Hospital_Manager;
using Data_Hospital_Manager.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hospital_Manager.ViewModels.Hospital;

namespace Hospital_Manager.Controllers
{
    public class PatientController : Controller
    {
        private readonly HospitalDbContext context;

        public PatientController(HospitalDbContext context)
        {
            this.context = context;
        }
    }
}
