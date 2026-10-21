using Microsoft.AspNetCore.Mvc;
using Data_Hospital_Manager;
using Data_Hospital_Manager.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hospital_Manager.ViewModels.Hospital;
using Hospital_Manager.ViewModels.Patient;
using Microsoft.AspNetCore.Mvc.Rendering;
using Hospital_Manager_Core.Interfaces;

namespace Hospital_Manager.Controllers
{
    public class PatientController : Controller
    {
        private readonly IPatientService patientService;
        private readonly IDoctorService doctorService;

        public PatientController(IPatientService patientService, IDoctorService doctorService)
        {
            this.patientService = patientService;
            this.doctorService = doctorService;
        }

        public async Task<IActionResult> Index()
        {
            var patients = await patientService.GetAllPatientsAsync();

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
            var patient = await patientService.GetPatientByIdAsync(id);

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
                await patientService.AddPatientAsync(patient);
                return RedirectToAction(nameof(Index));
            }
            await LoadDoctors(model.DoctorIds);
            return View(model);

        }

        public async Task<IActionResult> Edit(int id)
        {
            var patient = await patientService.GetPatientByIdAsync(id);

            if (patient == null)
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

        [HttpPost]
        public async Task<IActionResult> Edit(int id, PatientEditViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var patient = await patientService.GetPatientByIdAsync(id);

                if (patient == null)
                {
                    return NotFound();
                }

                patient.FirstName = model.FirstName;
                patient.LastName = model.LastName;
                patient.Email = model.Email;
                patient.PhoneNumber = model.PhoneNumber;

                patient.DoctorPatients.Clear();

                foreach (var doctorId in model.DoctorIds)
                {
                    patient.DoctorPatients.Add(new DoctorPatient
                    {
                        DoctorId = doctorId,
                        PatientId = patient.Id
                    });
                }

                await patientService.UpdatePatientAsync(patient);
                return RedirectToAction(nameof(Index));
            }

            await LoadDoctors(model.DoctorIds);
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var patient = await patientService.GetPatientByIdAsync(id);

            if (patient == null)
            {
                return NotFound();
            }

            var model = new PatientDeleteViewModel
            {
                Id = patient.Id,
                FirstName = patient.FirstName,
                LastName = patient.LastName
            };

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var patient = await patientService.GetPatientByIdAsync(id);

            if (patient != null)
            {
                await patientService.DeletePatientAsync(patient.Id);
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task LoadDoctors(List<int> selectedDoctors = null)
        {
            var doctors = await doctorService.GetAllDoctorsAsync();
            var doctorList = doctors.Select(d => new
            {
                d.Id,
                FullName = d.FirstName + " " + d.LastName
            }).ToList();

            ViewBag.Doctors = new MultiSelectList(
                doctorList,
                "Id",
                "FullName",
                selectedDoctors);
        }
    }
}
