using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WcfAESJobs.Client.Models;
using WcfAESJobs.Client.ViewModels;
using WcfAESJobs.Client.WebService;

namespace WcfAESJobs.Client.Controllers
{
    [TestClass]
    public class ApplicantController : Controller
    {
        JobServiceClient js = new JobServiceClient();


         
        // GET: JobsPosting
        [TestMethod]
        public ActionResult Index(string searchString, string selectedLocation)
        {
            OpenJobs[] All_Jobs = js.Get_Job_Posting_List();
            IEnumerable<OpenJobs> jobList = All_Jobs.ToList();

            var allUniqueLocations = jobList.Select(x => x.Job_Location).Distinct().ToList();

            if (!String.IsNullOrEmpty(searchString))
            {
                if (selectedLocation != "All Locations")
                {
                    jobList =
                        jobList.Where(
                            x => x.Job_Location.Contains(selectedLocation) && x.Job_Title.Contains(searchString));
                }
                if (selectedLocation == "All Locations")
                {
                    jobList = jobList.Where(x => x.Job_Title.Contains(searchString));
                }

                //jobList = jobList.Where(s => s.Job_Title.Contains(searchString)
                //                       || s.Job_Location.Contains(searchString));
            }
            else
            {
                if (selectedLocation == null)
                {
                    selectedLocation = "All Locations";
                }

                if (selectedLocation != "All Locations")
                {
                    jobList = jobList.Where(x => x.Job_Location.Contains(selectedLocation));
                }
            }

            JobPostingViewModel finalModel = new JobPostingViewModel { AllJobs = jobList, Locations = allUniqueLocations };

            return View(finalModel);
        }


        // GET: Applicant/Details/5
        [TestMethod]
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OpenJobs job = js.Get_Posting_By_ID(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        
        // GET: Applicant/Create
        [TestMethod]
        public ActionResult Create(int? id)
        {
            ViewBag.Posting_ID = id;
            return View();
        }

        // POST: Applicant/Create
        [TestMethod]
        [HttpPost]
        public ActionResult Create(int Posting_ID, ApplicantModel person)
        {
            try
            {
                /*Applicant applicant = new Applicant
                {
                    First_Name = person.First_Name,
                    Last_Name = person.Last_Name,
                    Address1 = person.Address1,
                    Address2 = person.Address2,
                    City = person.City,
                    State = person.State,
                    DOB = person.DOB,
                    Email = person.Email,
                    Phone = person.Phone

                };*/
                //int ApplicationID = js.Create_Application(applicant, Posting_ID);
                return RedirectToAction("Apply", "Application");
            }
            catch
            {
                return View();
            }
        }

        // GET: Applicant/Edit/5
        [TestMethod]
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Applicant/Edit/5
        [TestMethod]
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
            
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Applicant/Delete/5
        [TestMethod]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Applicant/Delete/5
        [TestMethod]
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
