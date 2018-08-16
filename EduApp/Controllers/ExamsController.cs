using DevExpress.XtraReports.UI;
using EduApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EduApp.Controllers
{
    public class ExamsController : Controller
    {
        EduNationEntity db = new EduNationEntity();
        // GET: Subjects
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult TopicExamsGridViewPartial()
        {
            var mymodel = db.Exams.ToList();

            return PartialView("_TopicExamsGridViewPartial", mymodel);
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
                exists.Year = item.Year;
                exists.Type = item.Type;
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

    }
}