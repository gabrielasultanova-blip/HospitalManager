using Data_Hospital_Manager;
using Data_Hospital_Manager.Entities;
using Hospital_Manager.ViewModels.Doctor;
using Hospital_Manager_Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Manager.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IDoctorService doctorService;
        private readonly IHospitalService hospitalService;

        public DoctorController(IDoctorService doctorService, IHospitalService hospitalService)
        {
            this.doctorService = doctorService;
            this.hospitalService = hospitalService;
        }

        public async Task<IActionResult> Index()
        {
            var doctors = await doctorService.GetAllDoctorsAsync(); 
               

            var model = new List<DoctorIndexViewModel>();

            foreach (var doctor in doctors)
            {
                model.Add(new DoctorIndexViewModel
                {
                    Id = doctor.Id,
                    FirstName = doctor.FirstName,
                    LastName = doctor.LastName,
                    Specialty = doctor.Specialty,
                    HospitalName = doctor.Hospital.Name
                });
            }

            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var doctor = await doctorService.GetDoctorByIdAsync(id);

            if (doctor == null)
            {
                return NotFound();
            }

            var model = new DoctorDetailsViewModel
            {
                Id = doctor.Id,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                Specialty = doctor.Specialty,
                Email = doctor.Email,
                PhoneNumber = doctor.PhoneNumber,
                HospitalName = doctor.Hospital.Name,

            };

            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Hospitals = new SelectList(
            await hospitalService.GetAllHospitalsAsync(),
             "Id",
            "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DoctorCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var doctor = new Doctor
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Specialty = model.Specialty,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    HospitalId = model.HospitalId
                };

                await doctorService.AddDoctorAsync(doctor);

                return RedirectToAction(nameof(Index));
            }
            //Вземи болниците от БД и ги сложи във ViewBag.Hospitals.
            ViewBag.Hospitals = new SelectList(
    await hospitalService.GetAllHospitalsAsync(), "Id", "Name");


            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var doctor = await doctorService.GetDoctorByIdAsync(id);

            if (doctor == null)
            {
                return NotFound();
            }

            var model = new DoctorEditViewModel
            {
                Id = doctor.Id,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                Specialty = doctor.Specialty,
                Email = doctor.Email,
                PhoneNumber = doctor.PhoneNumber,
                HospitalId = doctor.HospitalId
            };

            ViewBag.Hospitals = new SelectList(
                    await hospitalService.GetAllHospitalsAsync(),
                    "Id",
                    "Name");

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, DoctorEditViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var doctor = await doctorService.GetDoctorByIdAsync(id);

                if (doctor == null)
                {
                    return NotFound();
                }

                doctor.FirstName = model.FirstName;
                doctor.LastName = model.LastName;
                doctor.Specialty = model.Specialty;
                doctor.Email = model.Email;
                doctor.PhoneNumber = model.PhoneNumber;
                doctor.HospitalId = model.HospitalId;

                await doctorService.UpdateDoctorAsync(doctor);

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Hospitals = new SelectList(
                  await hospitalService.GetAllHospitalsAsync(),
                  "Id",
                  "Name");

            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var doctor = await doctorService.GetDoctorByIdAsync(id);

            if (doctor == null)
            {
                return NotFound();
            }

            var model = new DoctorDeleteViewModel
            {
                Id = doctor.Id,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName
            };

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var doctor = await doctorService.GetDoctorByIdAsync(id);

            if (doctor != null)
            {
                await doctorService.DeleteDoctorAsync(doctor.Id);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

