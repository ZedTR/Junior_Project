using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WcfAESJobs.Client.Models;
using WcfAESJobs.Client.WebService;

namespace WcfAESJobs.Client.Controllers
{
    [TestClass]
    public class ApplicationController : Controller
    {
        JobServiceClient js = new JobServiceClient();

        [TestMethod]
        public ActionResult Apply(int? id)
        {
            // id = application id
            Job_Application job_app = js.Get_Job_Application_By_ID(id);
            OpenJobs jobPosting = js.Get_Posting_By_ID(job_app.JobID);
            int Job_ID = jobPosting.Job_ID;

            Question[] AppQuestions = js.getQuestionsByJobID(Job_ID);
            Question[] PreAppQuestions = js.Get_PreApp_Questions();

            ViewBag.Application_ID = id;

            ApplicationModel qView = new ApplicationModel();

            foreach( Question q in AppQuestions)
            {         
                qView.ApplicationQuestions.Add(new QuestionAnswer{
                    fullQuestion = q.FullQuestion,
                    questionID = q.QuestionID,
                    answer = ""
                });
            }
            foreach (Question q in PreAppQuestions)
            {
                qView.PreApplicationQuestions.Add(new QuestionAnswer
                {
                    fullQuestion = q.FullQuestion,
                    questionID = q.QuestionID, 
                    answer = ""
                });
            }


            return View(qView);

        }

        // POST: Job/Create
        [TestMethod]
        [HttpPost]
        public ActionResult Apply(int ApplicationID, ApplicationModel model)
        {
            try
            {
                ApplicationModel localModel = model;

                List<Answer> answerList = new List<Answer>();

                foreach (QuestionAnswer qa in localModel.ApplicationQuestions)
                {
                    Answer a = new Answer
                    {
                        Answer_Text = qa.answer,
                        Question_ID = qa.questionID,
                        Application_ID = ApplicationID
                    };
                    answerList.Add(a);
                    
                }

                foreach (QuestionAnswer qa in localModel.PreApplicationQuestions)
                {
                    Answer a = new Answer
                    {
                        Answer_Text = qa.answer,
                        Question_ID = qa.questionID,
                        Application_ID = ApplicationID
                    };
                    answerList.Add(a);

                }

                js.Add_Answer_To_Table(answerList.ToArray());

                //ViewBag.StatusMessage = "Sucessfully Applied to Job";
                return RedirectToAction("Index", "Applicant");
            }
            catch
            {
                return View();
            }
        }

    }
}