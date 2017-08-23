using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WcfAESJobs.Client.WebService;

namespace WcfAESJobs.Client.Controllers
{
    [TestClass]
    public class ScreeningController : Controller
    {
        // GET: Screening

        JobServiceClient js = new JobServiceClient();

        [TestMethod]
        public ActionResult Index()
        {
            Job_Application[] applications = js.Get_Job_Application_List_By_Stage(1);

            return View(applications.ToList());
        }

        [TestMethod]
        public ActionResult PhoneScreen()
        {
            Job_Application[] applications = js.Get_Job_Application_List_By_Stage(2);

            return View(applications.ToList());
        }

        [TestMethod]
        public ActionResult Grade(int? id)
        {
            ViewBag.AppID = id;
            Question_Answer[] QA = js.Get_Answers_By_Application_ID(id);

            return View(QA.ToList());
        }

        [TestMethod]
        public ActionResult SubmitGrade(string result, int App_ID)
        {

            if (result == "Pass")
            {
                js.Update_Application_Stage(App_ID, 2);
                ViewBag.StatusMessage = "Applicant Passed";
            }
            else if (result == "Fail")
            {
                js.Update_Application_Stage(App_ID, 0);
                ViewBag.StatusMessage = "Applicant Failed";
            }
            else
            {
                ViewBag.StatusMessage = "Request could not be processed";
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");

        }
    }
}