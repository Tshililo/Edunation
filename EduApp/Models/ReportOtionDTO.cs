using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace EduApp.Models
{
    public class SummaryDto : ReportBaseDto
    {

        public SummaryDto()
        {
            Lines = new List<SummaryLineDto>();
        }

        public class SummaryLineDto
        {
            public string Grade { get; set; }
            public string SubjectName { get; set; }
            public string Topics { get; set; }
            public string Link { get; set; }
            public string Term { get; set; }
            public string Weeks { get; set; }
            public string ExamPaperLink { get; set; }
        }

        public List<SummaryLineDto> Lines
        {
            get; set;

        }



    }

    public class ReportBaseDto
    {
        public string OrganisationName { get; set; }
        public string Address { get; set; }
        public string TeNo { get; set; }
        public string Vat { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string ToCompany { get; set; }
        public string ForAttention { get; set; }

        public DateTime? DateFrom { get; set; }
        public DateTime? dateTo { get; set; }

    }

    public class ReportOtion
    {
        public string To { get; set; }

        public string Attention { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }
    }
}