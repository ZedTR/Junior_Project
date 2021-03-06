﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WcfAESJobs.Client.WebService;
using WcfAESJobs.Client.ViewModels;



namespace WcfAESJobs.Client.Controllers
{
    [TestClass]
    public class JobsPostingController : Controller
    {
        JobServiceClient js = new JobServiceClient();
     
        // GET: JobsPosting
        [TestMethod]
        public ActionResult Index(string searchString,string selectedLocation)
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

            JobPostingViewModel finalModel = new JobPostingViewModel {AllJobs = jobList , Locations = allUniqueLocations };
            
            return View(finalModel);
        }


        // GET: JobsPosting/Details/5
        [TestMethod]
        public ActionResult Details(int? id)
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

        [TestMethod]
        public ActionResult QuestionDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question[] question = js.getQuestionsByJobID(id);

            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }


        // GET: JobsPosting/Create
        [TestMethod]
        public ActionResult Create()
        {
            ViewBag.Location_ID = new SelectList(js.Get_Store_Location_List(), "Location_ID", "Location_Name");
            ViewBag.Job_ID = new SelectList(js.Get_Job_Template_List(), "Job_ID", "Job_Title");
            ViewBag.Close_Date = new DateTime();
            return View();
        }


        // POST: JobsPosting/Create
        [TestMethod]
        [HttpPost]
        public ActionResult Create([Bind(Include = "Location_ID, Job_ID, Close_Date")] NewPosting job)
        { 
            try
            {
                js.Create_Job_Posting(job);
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.Location_ID = new SelectList(js.Get_Store_Location_List(), "Location_ID", "Location_Name");
                ViewBag.Job_ID = new SelectList(js.Get_Job_Template_List(), "Job_ID", "Job_Title");
                ViewBag.Close_Date = new DateTime();
                return View(job);
            }
        }


        // GET: JobsPosting/Edit/5
        [TestMethod]
        public ActionResult Edit(int id)
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


        // POST: JobsPosting/Edit/5
        [TestMethod]
        [HttpPost]
        public ActionResult Edit(OpenJobs job)
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


        // GET: JobsPosting/Delete/5
        [TestMethod]
        public ActionResult Delete(int? id)
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


        // POST: JobsPosting/Delete/5
        [TestMethod]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                js.Delete_Job_Posting(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
