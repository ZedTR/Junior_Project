using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCFAESJobs.WebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IJobService" in both code and config file together.
    [ServiceContract]
    public interface IJobService
    {
        // GET
        [OperationContract]
        List<OpenJobs> Get_Job_Posting_List();

        [OperationContract]
        OpenJobs Get_Posting_By_ID(int? value);

        [OperationContract]
        List<StoreLocations> Get_Store_Location_List();

        [OperationContract]
        List<JobTemplates> Get_Job_Template_List();

        [OperationContract]
        JobTemplates Get_JobTemplate_By_ID(int? value);

        [OperationContract]
        List<QuestionForm> Get_Question_Form(int? value);

        [OperationContract]
        List<Question> Get_PreApp_Questions();

        [OperationContract]
        Job_Application Get_Job_Application_By_ID(int? value);

        [OperationContract]
        List<Job_Application> Get_Job_Application_List_By_Stage(int? stage);

        [OperationContract]
        List<Question_Answer> Get_Answers_By_Application_ID(int? Application_ID);
        

        // CREATE 
        [OperationContract]
        void Create_Job_Posting(NewPosting posting);

        [OperationContract]
        void Create_Job_Template(JobTemplates newJob);

        [OperationContract]
        int Create_Application(string UserID, int Posting_ID);


        // DELETE
        [OperationContract]
        void Delete_Job_Posting(int? value);

        [OperationContract]
        void Delete_Job_Template(int? value);


        // QUESTIONS
        [OperationContract]
        List<Question> getQuestionsByJobID(int? JobID);

        [OperationContract]
        Question getQuestionByID(int? value);

        [OperationContract]
        void deleteQuestionByID(int? value);

        [OperationContract]
        List<Question> getAllQuestions();

        [OperationContract]
        void addNewQuestion(Question question);

        [OperationContract]
        void addNewPreAppQuestion(Question question);

        [OperationContract]
        void addQuestionToJob(QuestionIDJobID questionIDjobID);

        [OperationContract]
        void removeQuestionFromJob(QuestionIDJobID questionIDjobID);

        // ANSWERS
        [OperationContract]
        void Add_Answer_To_Table(List<Answer> answer);


        // UPDATE
        [OperationContract]
        void Update_Job_Template(JobTemplates jobTemp);

        [OperationContract]
        void Update_Question(Question question);

        [OperationContract]
        void Update_Job_Questions_By_Form(int Job_ID, List<QuestionForm> form);

        // Moves application to next stage
        [OperationContract]
        void Update_Application_Stage(int? Application_ID, int stage);


    }

    [DataContract(IsReference = true)]
    public class OpenJobs
    {
        [DataMember]
        public int Posting_ID { get; set; }
        [DataMember]
        public int Job_ID { get; set; }
        [DataMember]
        public string Job_Title { get; set; }
        [DataMember]
        public string Job_Description { get; set; }
        [DataMember]
        public string Job_Qualification { get; set; }
        [DataMember]
        public string Job_Location_City { get; set; }
        [DataMember]
        public string Job_Location { get; set; }
        [DataMember]
        public DateTime Close_Date { get; set; }
    }


    [DataContract(IsReference = true)]
    public class StoreLocations
    {
        [DataMember]
        public int Location_ID { get; set; }

        [DataMember]
        public string Location_Name { get; set; }

    }


    [DataContract(IsReference = true)]
    public class JobTemplates
    {
        [DataMember]
        public int Job_ID { get; set; }

        [DataMember]
        public string Job_Title { get; set; }

        [DataMember]
        public string Job_Description { get; set; }

        [DataMember]
        public string Job_Qualifications { get; set; }

        [DataMember]
        public List<Question> Job_Questions { get; set; }

    }


    [DataContract(IsReference = true)]
    public class NewPosting
    {
        [DataMember]
        public int Job_ID { get; set; }

        [DataMember]
        public int Location_ID { get; set; }

        [DataMember]
        public DateTime Close_Date { get; set; }
    }


    [DataContract(IsReference = true)]
    public class Question
    {
        public Question() { QuestionID = 0; QuestionTitle = null; FullQuestion = null; }
        public Question(int id, string title, string question, string type) { QuestionID = id; QuestionTitle = title; FullQuestion = question; Type = type; }

        [DataMember]
        public int QuestionID { get; set; }

        [DataMember]
        public string QuestionTitle { get; set; }

        [DataMember]
        public string FullQuestion { get; set; }

        [DataMember]
        public string Type { get; set; }
    }


    [DataContract(IsReference = true)]
    public class JobQuestionList
    {
        [DataMember]
        List<QuestionList> JobQuestions { get; set; }
    }


    [DataContract(IsReference = true)]
    public class QuestionIDJobID
    {
        [DataMember]
        public int QuestionID { get; set; }

        [DataMember]
        public int Job_ID { get; set; }
    }

    [DataContract(IsReference = true)]
    public class QuestionForm
    {
        [DataMember]
        public bool checkedQuestion { get; set; }

        [DataMember]
        public Question question { get; set; }
    }

    [DataContract(IsReference = true)]
    public class Applicant
    {
        [DataMember]
        public string ID { get; set; }

        [DataMember]
        public string First_Name { get; set; }

        [DataMember]
        public string Last_Name { get; set; }

        [DataMember]
        public DateTime DOB { get; set; }

        [DataMember]
        public string Address1 { get; set; }

        [DataMember]
        public string Address2 { get; set; }

        [DataMember]
        public string City { get; set; }

        [DataMember]
        public string State { get; set; }

        [DataMember]
        public string Phone { get; set; }

        [DataMember]
        public string Email { get; set; }
    }

    [DataContract]
    public class Job_Application
    {
        [DataMember]
        public string UserID { get; set; }

        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public int JobID { get; set; }

        [DataMember]
        public string JobTitle { get; set; }

        [DataMember]
        public int ApplicantID { get; set; }

        [DataMember]
        public string ApplicantName { get; set; }

        [DataMember]
        public int Stage { get; set; }

        
    }

    [DataContract]
    public class Answer
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public int Application_ID { get; set; }

        [DataMember]
        public int Question_ID { get; set; }

        [DataMember]
        public string Answer_Text { get; set; }
    }

    [DataContract]
    public class Question_Answer
    {
        [DataMember]
        public string Question {get; set;}

        [DataMember]
        public string Answer {get; set;}

        //[DataMember]
        //public string Type { get; set; }
    }

}
