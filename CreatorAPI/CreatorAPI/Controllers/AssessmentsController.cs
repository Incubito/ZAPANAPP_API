using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CreatorAPI.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CreatorAPI.Controllers
{
    [RoutePrefix("api/Assessments")]
    public class AssessmentsController : ApiController
    {
        [Authorize]
        [Route("Rx")]
        public ProcessingResult PostRx([FromHeader] string TakenAssessment)
        {
            ProcessingResult RxAssessments = new ProcessingResult();

            try
            {
                JObject JSON = JObject.Parse(TakenAssessment);

                string TakenBy = JSON["TakenBy"].ToString();
                string TakenAt = JSON["TakenAt"].ToString();
                string Question1 = JSON["Question1"].ToString();
                string Question2 = JSON["Question2"].ToString();
                string Question3 = JSON["Question3"].ToString();
                string Question4 = JSON["Question4"].ToString();
                string Question5 = JSON["Question5"].ToString();
                string Question6 = JSON["Question6"].ToString();
                string Question7 = JSON["Question7"].ToString();

                CreatorEntities db = new CreatorEntities();
                MobileConnections mobileconnection = db.MobileConnections.Single(mc => mc.UUID == TakenBy);
                
                //Question 1
                Assessments NAQ1 = new Assessments();
                NAQ1.MobileConnections = mobileconnection;
                NAQ1.AssessmentDate = DateTime.Parse(TakenAt);
                NAQ1.Question = 1;
                NAQ1.Answer = Convert.ToInt16(Question1);

                db.Assessments.Add(NAQ1);

                ////Question 2
                Assessments NAQ2 = new Assessments();
                NAQ2.MobileConnections = mobileconnection;
                NAQ2.AssessmentDate = DateTime.Parse(TakenAt);
                NAQ2.Question = 2;
                NAQ2.Answer = Convert.ToInt16(Question2);

                db.Assessments.Add(NAQ2);

                ////Question 3
                Assessments NAQ3 = new Assessments();
                NAQ3.MobileConnections = mobileconnection;
                NAQ3.AssessmentDate = DateTime.Parse(TakenAt);
                NAQ3.Question = 3;
                NAQ3.Answer = Convert.ToInt16(Question3);

                db.Assessments.Add(NAQ3);

                ////Question 4
                Assessments NAQ4 = new Assessments();
                NAQ4.MobileConnections = mobileconnection;
                NAQ4.AssessmentDate = DateTime.Parse(TakenAt);
                NAQ4.Question = 4;
                NAQ4.Answer = Convert.ToInt16(Question4);

                db.Assessments.Add(NAQ4);

                ////Question 5
                Assessments NAQ5 = new Assessments();
                NAQ5.MobileConnections = mobileconnection;
                NAQ5.AssessmentDate = DateTime.Parse(TakenAt);
                NAQ5.Question = 5;
                NAQ5.Answer = Convert.ToInt16(Question5);

                db.Assessments.Add(NAQ5);

                ////Question 6
                Assessments NAQ6 = new Assessments();
                NAQ6.MobileConnections = mobileconnection;
                NAQ6.AssessmentDate = DateTime.Parse(TakenAt);
                NAQ6.Question = 6;
                NAQ6.Answer = Convert.ToInt16(Question6);

                db.Assessments.Add(NAQ6);

                ////Question 7
                Assessments NAQ7 = new Assessments();
                NAQ7.MobileConnections = mobileconnection;
                NAQ7.AssessmentDate = DateTime.Parse(TakenAt);
                NAQ7.Question = 7;
                NAQ7.Answer = Convert.ToInt16(Question7);

                db.Assessments.Add(NAQ7);

                db.SaveChanges();

                RxAssessments.Message = "Sucessfully Saved";
                RxAssessments.Status = "Assessments";
            }
            catch (Exception ex)
            {
                RxAssessments.Message = ex.Message;
                RxAssessments.Status = "Assessments";
            }

            return RxAssessments;
        }
    }
}
