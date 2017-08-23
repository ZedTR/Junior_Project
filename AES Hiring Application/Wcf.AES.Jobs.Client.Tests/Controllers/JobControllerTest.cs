using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WcfAESJobs.Client.WebService;

namespace WcfAESJobs.Client.Controllers
{
    
    [TestClass]
    public class JobController : Controller
    {

        JobServiceClient js = new JobServiceClient();

        // GET: Job
        [TestMethod]
        public ActionResult Index()
        {
            JobTemplates[] All_Jobs = js.Get_Job_Template_List();
            IEnumerable<JobTemplates> jobList = All_Jobs.ToList();

            return View(jobList);
        }

        // GET: Job/Details/5
        [TestMethod]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobTemplates jobTemp = js.Get_JobTemplate_By_ID(id);
            if (jobTemp == null)
            {
                return HttpNotFound();
            }
            return View(jobTemp);
        }

        // GET: Job/Create
        [TestMethod]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Job/Create
        [TestMethod]
        [HttpPost]
        public ActionResult Create([Bind(Include = "Job_Title,Job_Description,Job_Qualifications")] JobTemplates jobTemp)
        {
            try
            {
                js.Create_Job_Template(jobTemp);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Job/Edit/5
        [TestMethod]
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobTemplates jobTemp = js.Get_JobTemplate_By_ID(id);
            if (jobTemp == null)
            {
                return HttpNotFound();
            }
            return View(jobTemp);
        }

        // POST: Job/Edit/5
        [TestMethod]
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Job_ID, Job_Title,Job_Description,Job_Qualifications")] JobTemplates job)
        {
            try
            {
                js.Update_Job_Template(job);

                return RedirectToAction("Details", "Job", new { id = job.Job_ID });
            }
            catch
            {
                return View();
            }
        }

        // GET: Job/Delete/5
        [TestMethod]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobTemplates job = js.Get_JobTemplate_By_ID(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // POST: Job/Delete/5
        [TestMethod]
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            
            try
            {
                js.Delete_Job_Template(id);
                return RedirectToAction("Index");
            }
            catch
            {
                JobTemplates job = js.Get_JobTemplate_By_ID(id);
                ViewBag.StatusMessage = "WARNING !Cannot delete jobs with current openings";
                if (job == null)
                {
                    return HttpNotFound();
                }
                return View(job);
            }
        }
    }
}
