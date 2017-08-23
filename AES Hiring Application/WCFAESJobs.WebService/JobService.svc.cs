using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCFAESJobs.WebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "JobService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select JobService.svc or JobService.svc.cs at the Solution Explorer and start debugging.
    public class JobService : IJobService
    {
        public const int TRUE = 1;
        public const int FALSE = 0;

        DB_9E5950_aes01Entities db = new DB_9E5950_aes01Entities();

        public void Create_Job_Posting(NewPosting posting)
        {
            using (db)
            {
                AvailableJob job = new AvailableJob
                {
                    LocationID = posting.Location_ID,
                    JobID = posting.Job_ID,
                    CloseDate = posting.Close_Date,
                    IsActive = TRUE
                };

                db.AvailableJobs.Add(job);
                db.SaveChanges();
            }

        }


        public void Create_Job_Template(JobTemplates newJob)
        {
            using (db)
            {
                JobApplication job = new JobApplication
                {
                    Title = newJob.Job_Title,
                    Description = newJob.Job_Description,
                    Qualifications = newJob.Job_Qualifications,
                    IsActive = TRUE
                };

                db.JobApplications.Add(job);
                db.SaveChanges();
            }
        }


        public int Create_Application(string User_ID, int Posting_ID)
        {
            using (db)
            {
                /*ApplicantTable newApplicant = new ApplicantTable
                {
                    First_Name = applicant.First_Name,
                    Last_name = applicant.Last_Name,
                    DOB = applicant.DOB,
                    Email = applicant.Email,
                    Address1 = applicant.Address1,
                    Address2 = applicant.Address2,
                    City = applicant.City,
                    State = applicant.State,
                    Phone = applicant.Phone,

                };
                //db.SaveChanges();
                db.ApplicantTables.Add(newApplicant);*/
                Application app = new Application
                {
                    UserID = User_ID,
                    ApplicantID = 29,
                    JobID = Posting_ID,
                    Stage = 1
                };

                db.Applications.Add(app);
                db.SaveChanges();
                return app.Id;
            }
        }


        public void Delete_Job_Posting(int? value)
        {

            if (value != null)
            {
                try
                {
                    db = new DB_9E5950_aes01Entities();
                    AvailableJob job = db.AvailableJobs.Find(value);
                    db.AvailableJobs.Remove(job);
                    db.SaveChanges();
                }
                catch
                {
                    db = new DB_9E5950_aes01Entities();
                    AvailableJob job = db.AvailableJobs.Find(value);
                    job.IsActive = FALSE;
                    db.SaveChanges();
                }
            }
            else
                throw new Exception("Jobs not Removed!!!");

        }


        public void Delete_Job_Template(int? value)
        {
            if (value != null)
            {
                try
                {
                    db = new DB_9E5950_aes01Entities();
                    JobApplication job = db.JobApplications.Find(value);
                    db.JobApplications.Remove(job);
                    db.SaveChanges();
                }
                catch
                {
                    db = new DB_9E5950_aes01Entities();
                    JobApplication job = db.JobApplications.Find(value);
                    job.IsActive = FALSE;
                    db.SaveChanges();
                }
            }
            else
                throw new Exception("Jobs not Removed!!!");


        }


        public List<OpenJobs> Get_Job_Posting_List()
        {
            using (db)
            {

                IQueryable<OpenJobs> Job_Entity = db.AvailableJobs.Where(x => x.IsActive == TRUE).Select(x => new OpenJobs
                {
                    Posting_ID = x.PostingID,
                    Job_ID = x.JobID,
                    Job_Title = x.JobApplication.Title,
                    Job_Description = x.JobApplication.Description,
                    Job_Qualification = x.JobApplication.Qualifications,
                    Job_Location = x.Location.LocationName,
                    Job_Location_City = x.Location.City,
                    Close_Date = x.CloseDate

                });

                try
                {
                    return Job_Entity.ToList();
                }
                catch
                {
                    throw new Exception("No Avaliable Jobs");
                }

            }
        }


        public List<JobTemplates> Get_Job_Template_List()
        {
            using (db)
            {
                IQueryable<JobTemplates> Job_Entity = db.JobApplications.Where(x => x.IsActive == TRUE).Select(x => new JobTemplates
                {
                    Job_ID = x.Id,
                    Job_Title = x.Title

                });

                if (Job_Entity != null)
                    return Job_Entity.ToList();
                else
                    throw new Exception("Job Template not found");

            }
        }


        public OpenJobs Get_Posting_By_ID(int? value)
        {
            using (db)
            {
                var JobEntity = (from j in db.AvailableJobs where j.PostingID == value select j).FirstOrDefault();
                if (JobEntity != null)
                    return Translate_JobEntity_to_Job(JobEntity);
                else
                    throw new Exception("Invalid id");

            }

        }


        public JobTemplates Get_JobTemplate_By_ID(int? value)
        {
            using (db)
            {
                var JobEntity = (from j in db.JobApplications where j.Id == value select j).FirstOrDefault();
                if (JobEntity != null)
                    return Translate_JobEntity_to_Job(JobEntity);
                else
                    throw new Exception("Invalid id");
            }
        }


        public List<QuestionForm> Get_Question_Form(int? value)
        {
            using (DB_9E5950_aes01Entities db = new DB_9E5950_aes01Entities())
            {
                List<Question> JobQlist = getQuestionsByJobID(value);
                var qList = db.QuestionLists.ToList().Where(x => x.Type == "APP").Select(x => new QuestionForm
                {
                    question = new Question(x.Id, x.Title, x.Question, x.Type),
                    checkedQuestion = false
                }).ToList();

                //List<QuestionForm> qList = questionEntity.ToList();


                foreach (QuestionForm item in qList)
                {
                    foreach (Question q in JobQlist)
                    {
                        if (q.QuestionID == item.question.QuestionID)
                            item.checkedQuestion = true;
                    }

                }
                //List<QuestionForm> qList = questionEntity.ToList();
                return qList;
            }
        }


        public List<StoreLocations> Get_Store_Location_List()
        {
            using (db)
            {
                IQueryable<StoreLocations> storeEntity = db.Locations.Select(x => new StoreLocations
                {
                    Location_ID = x.Id,
                    Location_Name = x.LocationName


                });
                if (storeEntity != null)
                    return storeEntity.ToList();
                else
                    throw new Exception("Invalid Location id");
            }


        }

        // Gets Job Application info given the application ID.
        public Job_Application Get_Job_Application_By_ID(int? value)
        {
            using (db)
            {

                var job_entity = (from j in db.Applications where j.Id == value select j).FirstOrDefault();
                if (job_entity != null)
                {
                    Job_Application job_app = new Job_Application
                    {
                        ID = job_entity.Id,
                        ApplicantID = 0,
                        JobID = job_entity.JobID,
                        UserID = job_entity.UserID
                                                
                        //Stage = app.stage
                    };
                    return job_app;
                }
                else
                    throw new Exception("Invalid id");
            }
            
        }


        public List<Question> getQuestionsByJobID(int? JobID)
        {
            using (DB_9E5950_aes01Entities db = new DB_9E5950_aes01Entities())
            {
                IQueryable<Question> query = from questions in db.QuestionLists
                                             where questions.JobApplications.Any(c => c.Id == JobID)
                                             select new Question()
                                             {
                                                 QuestionID = questions.Id,
                                                 QuestionTitle = questions.Title,
                                                 FullQuestion = questions.Question
                                             };

                return query.ToList();

            }
        }


        public Question getQuestionByID(int? value)
        {
            using (db)
            {
                var questionEntity = (from j in db.QuestionLists where j.Id == value select j).FirstOrDefault();
                if (questionEntity != null)
                {
                    Question q = new Question();
                    q.QuestionID = questionEntity.Id;
                    q.QuestionTitle = questionEntity.Title;
                    q.FullQuestion = questionEntity.Question;
                    return q;
                }
                else
                    throw new Exception("Invalid id");
            }
        }


        public void deleteQuestionByID(int? value)
        {
            using (db)
            {
                try
                {
                    QuestionList question = db.QuestionLists.Find(value);
                    db.QuestionLists.Remove(question);
                    db.SaveChanges();
                }
                catch
                {
                    db = new DB_9E5950_aes01Entities();
                    QuestionList q = db.QuestionLists.Find(value);
                    q.IsActive = FALSE;
                    db.SaveChanges();
                }


            }
        }


        public List<Question> getAllQuestions()
        {
            using (db)
            {
                IQueryable<Question> questionEntity = db.QuestionLists.Where(x => x.Type == "APP" && x.IsActive == TRUE).Select(x => new Question
                {
                    QuestionID = x.Id,
                    QuestionTitle = x.Title,
                    FullQuestion = x.Question

                });

                if (questionEntity != null)
                    return questionEntity.ToList();
                else
                    throw new Exception("No Questions");
            }
        }


        public List<Question> Get_PreApp_Questions()
        {
            using (db)
            {
                IQueryable<Question> questionEntity = db.QuestionLists.Where(x => x.Type == "PREAPP" && x.IsActive == TRUE).Select(x => new Question
                {
                    QuestionID = x.Id,
                    QuestionTitle = x.Title,
                    FullQuestion = x.Question,
                    Type = x.Type
                });

                if (questionEntity != null)
                    return questionEntity.ToList();
                else
                    throw new Exception("No Questions");
            }
        }


        // Gets A list of job Applications of the same stage
        public List<Job_Application> Get_Job_Application_List_By_Stage(int? stage)
        {
            using (db)
            {
                IQueryable<Job_Application> applicationEntity = db.Applications.Where(x => x.Stage == stage ).Select(x => new Job_Application
                {
                    ID = x.Id,
                    //JobID = x.JobID,
                    JobTitle = x.AvailableJob.JobApplication.Title,
                    ApplicantID = 0,
                    UserID = x.UserID,
                    //ApplicantName = x.ApplicantTable.First_Name + " " + x.ApplicantTable.Last_name,
                    Stage = x.Stage
                });

                if (applicationEntity != null)
                    return applicationEntity.ToList();
                else
                    throw new Exception("No Applications");
            }
        }

        public List<Question_Answer> Get_Answers_By_Application_ID(int? Application_ID)
        {
            using (db)
            {
                IQueryable<Question_Answer> answerEntity = db.AnswerTables.Where(x => x.ApplicationID == Application_ID).Select(x => new Question_Answer
                {
                    Answer = x.Answer,
                    Question = x.QuestionList.Question
                });

                if (answerEntity != null)
                    return answerEntity.ToList();
                else
                    throw new Exception("Error geting answers");
            }
        }

        public void addNewQuestion(Question question)
        {
            using (db)
            {
                QuestionList ql = new QuestionList
                {
                    Title = question.QuestionTitle,
                    Question = question.FullQuestion,
                    Id = question.QuestionID,
                    IsActive = TRUE,
                    Type = "APP"
                };
                db.QuestionLists.Add(ql);
                db.SaveChanges();
            }

        }

        public void addNewPreAppQuestion(Question question)
        {
            using (db)
            {
                QuestionList ql = new QuestionList
                {
                    Title = question.QuestionTitle,
                    Question = question.FullQuestion,
                    Id = question.QuestionID,
                    IsActive = TRUE,
                    Type = "PREAPP"
                };
                db.QuestionLists.Add(ql);
                db.SaveChanges();
            }
        }

        public void addQuestionToJob(QuestionIDJobID questionIDjobID)
        {
            using (DB_9E5950_aes01Entities db = new DB_9E5950_aes01Entities())
            {
                //var jobEntity = (from p in db.JobApplications where p.Id == questionIDjobID.Job_ID select p).FirstOrDefault();
                var questionEntity = (from p in db.QuestionLists where p.Id == questionIDjobID.QuestionID select p).FirstOrDefault();

                var query =
                    from ja in db.JobApplications
                    where ja.Id == questionIDjobID.Job_ID
                    select ja;

                foreach (JobApplication japp in query)
                {
                    if (japp.Id == questionIDjobID.Job_ID)
                        japp.QuestionLists.Add(questionEntity);
                }

                db.SaveChanges();

            }
        }


        public void removeQuestionFromJob(QuestionIDJobID questionIDjobID)
        {
            using (DB_9E5950_aes01Entities db = new DB_9E5950_aes01Entities())
            {

                var questionEntity = (from p in db.QuestionLists where p.Id == questionIDjobID.QuestionID select p).FirstOrDefault();

                var query =
                    from ja in db.JobApplications
                    where ja.Id == questionIDjobID.Job_ID
                    select ja;

                foreach (JobApplication japp in query)
                {
                    if (japp.Id == questionIDjobID.Job_ID)
                        japp.QuestionLists.Remove(questionEntity);
                }

                db.SaveChanges();

            }

        }

        public void Add_Answer_To_Table(List<Answer> AnswerList)
        {
            using (DB_9E5950_aes01Entities db = new DB_9E5950_aes01Entities())
            {
                try
                {
                    AnswerTable entry;
                    foreach (Answer a in AnswerList)
                    {
                        entry = new AnswerTable
                        {
                            ApplicationID = a.Application_ID,
                            QuestionID = a.Question_ID,
                            Answer = a.Answer_Text
                        };
                        db.AnswerTables.Add(entry);
                        db.SaveChanges();
                    }
                    
                }
                catch
                {
                    throw new Exception("Error adding to database");
                }

            }
        }

        public void Update_Job_Template(JobTemplates jobTemp)
        {
            using (db)
            {
                JobApplication ja = (from j in db.JobApplications where j.Id == jobTemp.Job_ID select j).FirstOrDefault();
                ja.Qualifications = jobTemp.Job_Qualifications;
                ja.Description = jobTemp.Job_Description;
                ja.Title = jobTemp.Job_Title;
                db.SaveChanges();
            }
        }

        public void Update_Question(Question question)
        {
            using (db)
            {
                QuestionList ja = (from j in db.QuestionLists where j.Id == question.QuestionID select j).FirstOrDefault();
                ja.Title = question.QuestionTitle;
                ja.Question = question.FullQuestion;

                db.SaveChanges();
            }
        }


        public void Update_Job_Questions_By_Form(int Job_ID, List<QuestionForm> formList)
        {
            using (db)
            {
                foreach (QuestionForm form in formList)
                {
                    QuestionIDJobID QJ = new QuestionIDJobID();
                    QJ.QuestionID = form.question.QuestionID;
                    QJ.Job_ID = Job_ID;

                    if (form.checkedQuestion == true)
                        addQuestionToJob(QJ);
                    else if (form.checkedQuestion == false)
                        removeQuestionFromJob(QJ);
                }
            }
        }

        public void Update_Application_Stage(int? Application_ID, int stage)
        {
            using (db)
            {
                try
                {
                    Application app = (from j in db.Applications where j.Id == Application_ID select j).FirstOrDefault();
                    app.Stage = stage;

                    db.SaveChanges();
                }
                catch
                {
                    throw new Exception("Application Update Failed");
                }
                
            }
        }


        private OpenJobs Translate_JobEntity_to_Job(AvailableJob jobentity)
        {
            OpenJobs job = new OpenJobs();
            job.Posting_ID = jobentity.PostingID;
            job.Job_ID = jobentity.JobID;
            job.Job_Title = jobentity.JobApplication.Title;
            job.Job_Description = jobentity.JobApplication.Description;
            job.Job_Qualification = jobentity.JobApplication.Qualifications;
            job.Job_Location = jobentity.Location.LocationName;
            job.Close_Date = jobentity.CloseDate;
            job.Job_Location_City = jobentity.Location.City;
            return job;
        }


        private JobTemplates Translate_JobEntity_to_Job(JobApplication jobentity)
        {
            JobTemplates job = new JobTemplates();
            job.Job_ID = jobentity.Id;
            job.Job_Title = jobentity.Title;
            job.Job_Description = jobentity.Description;
            job.Job_Qualifications = jobentity.Qualifications;
            job.Job_Questions = getQuestionsByJobID(jobentity.Id);
            return job;
        }



    }
}
