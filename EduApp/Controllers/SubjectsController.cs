using DevExpress.XtraReports.UI;
using EduApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EduApp.Controllers
{
    public class SubjectsController : Controller
    {
        EduNationEntity db = new EduNationEntity();
        // GET: Subjects
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult TurtorialsLanding(string headerObjId)
        {
            var mymodel = db.SchoolSubs;
            var ApplicationsInfo = mymodel.Where(s => s.ObjId.ToString() == headerObjId).FirstOrDefault();

            ViewBag.TopicLink = ApplicationsInfo.Link;
            return PartialView("TurtorialsLanding");
        }
        public ActionResult TopicExamsGridViewPartial(string headerObjId)
        {
            var mymodel = db.Exams.Where(s => s.SubjectID.ToString() == headerObjId).ToList();
 
            return PartialView("_TopicExamsGridViewPartial", mymodel);
        }
        public ActionResult ExamsLanding()
        {


            return PartialView("ExamsLanding");
        }
        [HttpGet]
        public ActionResult ReportViewPartial(string reportParams)
        {

            var report = GetReport(reportParams);

            return PartialView("ReportViewPartial", report);
        }
        XtraReport GetReport(string passed_params)
        {
            List<string> p = passed_params.Split(',').ToList();
            var rep = new StudentsReportXrMvc();
            rep.ObjId.Value = p[0];
            rep.Attention.Value = p[1];
          //  rep.FromDate.Value = p[2];
            //rep.ToDate.Value = p[4];
            return rep;
        }
        public ActionResult EditHeaderFormPartial(Guid ObjId, SchoolSub model)
		{
			//ViewData["Mortuaries"] = GetMortuary();

			var mymodel = db.SchoolSubs;
			var ApplicationsInfo = mymodel.Where(s => s.ObjId == ObjId).FirstOrDefault();

			if (ApplicationsInfo == null)
			{
				model.ObjId = ObjId;
				return PartialView("CreateSubjectsEditPartial", model);
			}

			if (ApplicationsInfo != null)
			{
                return PartialView("CreateSubjectsEditPartial", ApplicationsInfo);
			}

			return null;

		}      
        public ActionResult TopicExamsEditHeaderFormPartial(Guid ObjId, Exam model)
        {

            var mymodel = db.Exams;
            var ApplicationsInfo = mymodel.Where(s => s.ObjId == ObjId).FirstOrDefault();

            if (ApplicationsInfo == null)
            {
                model.ObjId = ObjId;
                return PartialView("CreateTopicExamsEditPartial", model);
            }

            if (ApplicationsInfo != null)
            {
                return PartialView("CreateTopicExamsEditPartial", ApplicationsInfo);
            }

            return null;

        }
        public ActionResult SubjectsAddEdit(SchoolSub item, string headerObjId)
        {
            var modelRepo = db.SchoolSubs;
            var exists = modelRepo.Where(c => c.ObjId == item.ObjId).SingleOrDefault();
            if (exists == null)
            {
                modelRepo.Add(item);
                db.SaveChanges();
            }
            if (exists != null)
            {
                exists.Grade = item.Grade;
                exists.SubjectName = item.SubjectName;
                exists.Topics = item.Topics;
                exists.Link = item.Link;
                exists.Term = item.Term;
                exists.Weeks = item.Weeks;
                exists.ExamPaperLink = item.ExamPaperLink;
                this.UpdateModel(exists);
                db.SaveChanges();
            }

            // DXCOMMENT: Pass a data model for GridView in the PartialView method's second parameter
            return PartialView("_SubjectsGridViewPartial", modelRepo.ToList());

        }
        public ActionResult TopicExamsAddEdit(Exam item, string headerObjId)
        {
            var SchoolSubs = db.SchoolSubs.Where(c => c.ObjId.ToString() == headerObjId).SingleOrDefault();

            var modelRepo = db.Exams;
            var exists = modelRepo.Where(c => c.ObjId == item.ObjId).SingleOrDefault();
            if (exists == null)
            {
                item.Subject = SchoolSubs.SubjectName;
                item.Link = item.Link;
                item.SubjectID = Guid.Parse(headerObjId);
                item.Term = SchoolSubs.Term;
                modelRepo.Add(item);
                db.SaveChanges();
            }
            if (exists != null && SchoolSubs != null)
            {
                exists.Subject = SchoolSubs.SubjectName;
                exists.Link = SchoolSubs.Link;
                exists.PaperNo = item.PaperNo;
                exists.Term = SchoolSubs.Term;
                this.UpdateModel(exists);
                db.SaveChanges();
            }

            var mymodel = db.Exams.Where(s => s.SubjectID.ToString() == headerObjId).ToList();

            // DXCOMMENT: Pass a data model for GridView in the PartialView method's second parameter
            return PartialView("_TopicExamsGridViewPartial", mymodel);

        }

        [HttpPost, ValidateInput(false)]
        public ActionResult TopicExamsDelete(Guid ObjId, string headerObjId)
        {
            var model = db.Exams;
            if (ObjId != null)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.ObjId == ObjId);
                    if (item != null)
                    {
                        model.Remove(item);

                    }

                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }


            var mymodel = db.Exams.Where(s => s.SubjectID.ToString() == headerObjId).ToList();
            // DXCOMMENT: Pass a data model for GridView in the PartialView method's second parameter
            return PartialView("_TopicExamsGridViewPartial", mymodel);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult SubjectsDelete(Guid ObjId)
        {
            var model = db.SchoolSubs;
            if (ObjId != null)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.ObjId == ObjId);
                    if (item != null)
                    {
                        model.Remove(item);

                    }

                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            var BurialRecords = db.SchoolSubs.ToList();
            // DXCOMMENT: Pass a data model for GridView in the PartialView method's second parameter
            return PartialView("_SubjectsGridViewPartial", BurialRecords);
        }
        public ActionResult SubjectsGridViewPartial()
        {
            var model = db.SchoolSubs;

            // DXCOMMENT: Pass a data model for GridView in the PartialView method's second parameter
            return PartialView("_SubjectsGridViewPartial", model.ToList().OrderBy(r => r.Term).ThenBy(y => y.Weeks));
        }
    }
}