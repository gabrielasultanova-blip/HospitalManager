using Microsoft.AspNetCore.Mvc;
using Data_Hospital_Manager;
using Data_Hospital_Manager.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hospital_Manager.ViewModels.Hospital;
using Hospital_Manager.ViewModels.Patient;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hospital_Manager.Controllers
{
    public class PatientController : Controller
    {
        private readonly HospitalDbContext context;

        public PatientController(HospitalDbContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            var patients = await context.Patients
                .Include(p => p.DoctorPatients)
                .ThenInclude(dp => dp.Doctor)
                .ToListAsync();

            var model = new List<PatientIndexViewModel>();

            foreach (var patient in patients)
            {
                model.Add(new PatientIndexViewModel
                {
                    Id = patient.Id,
                    FirstName = patient.FirstName,
                    LastName = patient.LastName,
                    Email = patient.Email,
                    PhoneNumber = patient.PhoneNumber,

                    DoctorName = string.Join(", ", patient.DoctorPatients
                    .Select(dp => dp.Doctor.FirstName + " " + dp.Doctor.LastName))
                });
            }
            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var patient = await context.Patients
                .Include(p => p.DoctorPatients)
                .ThenInclude(dp => dp.Doctor)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (patient == null)
            {
                return NotFound();
            }

            var model = new PatientDetailsViewModel
            {
                Id = patient.Id,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                Email = patient.Email,
                PhoneNumber = patient.PhoneNumber,
                DoctorName = string.Join(", ", patient.DoctorPatients
                    .Select(dp => dp.Doctor.FirstName + " " + dp.Doctor.LastName))
            };
            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            await LoadDoctors();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PatientCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var patient = new Patient
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber
                };
                foreach (var doctorId in model.DoctorIds)
                {
                    patient.DoctorPatients.Add(new DoctorPatient
                    {
                        DoctorId = doctorId,
                    });
                }
                context.Patients.Add(patient);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            await LoadDoctors(model.DoctorIds);
            return View(model);
            
        }

        public async Task<IActionResult> Edit(int id)
        {
            var patient = await context.Patients
                .Include(p => p.DoctorPatients)
                .FirstOrDefaultAsync(p => p.Id == id);

            if(patient == null)
            {
                return NotFound();
            }

            var model = new PatientEditViewModel
            {
                Id = patient.Id,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                Email = patient.Email,
                PhoneNumber = patient.PhoneNumber,

                DoctorIds = patient.DoctorPatients
                .Select(dp => dp.DoctorId)
                .ToList()
            };

            await LoadDoctors(model.DoctorIds);
            return View(model);
        }

        private async Task LoadDoctors(List<int> selectedDoctors = null)
        {
            var doctors = await context.Doctors
                .Select(d => new
                {
                    d.Id,
                    FullName = d.FirstName + " " + d.LastName
                }).ToListAsync();

            ViewBag.Doctors = new MultiSelectList(
                doctors,
                "Id",
                "FullName",
                selectedDoctors);
        }
    }
}
