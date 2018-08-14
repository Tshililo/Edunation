using System;
using System.Collections.Generic;
using System.Linq;

namespace EduApp
{

    public partial class StudentsReportXrMvc : DevExpress.XtraReports.UI.XtraReport
    {
        EduNationEntity db = new EduNationEntity();
        public StudentsReportXrMvc()
		{
            InitializeComponent();
            this.ReportPrintOptions.DetailCount = 10;
        }

		private void StudentsReportXrMvc_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			Guid objId = Guid.Parse(this.ObjId.Value.ToString());
            string ForAttention = Attention.Value.ToString();

            //string dateFrom = FromDate.Value.ToString();         
            //string dateTo = ToDate.Value.ToString();

            //DateTime? searchfrom = DateTime.Parse(dateFrom);
            //DateTime? searchto = DateTime.Parse(dateTo);

            var orgSrc = GetReportDto(ForAttention);
			this.srcOrganization.DataSource = orgSrc;
		}

        private List<Models.SummaryDto.SummaryLineDto> GetApplicationsDto(string forAttention)
        {
            var model = (from ur in db.SchoolSubs
                         select new Models.SummaryDto.SummaryLineDto
                         {
                             Term = ur.Term,
                             Weeks = ur.Weeks,
                             Grade = ur.Grade,
                             SubjectName = ur.SubjectName,
                             Topics = ur.Topics,
                             ExamPaperLink = ur.ExamPaperLink,
                         }).OrderBy(c => c.Term).ThenBy(c => c.Weeks);

            return model.ToList();
        }

        public Models.SummaryDto GetReportDto(string forAttention)
        {
            var ReportHead = db.ReportHeaders.FirstOrDefault();
            var dto = new Models.SummaryDto();
            dto.ForAttention = forAttention;
            //dto.DateFrom = dateFrom;
            //dto.dateTo = dateTo;
            dto.OrganisationName = ReportHead.OrganisationName;
            dto.Fax = ReportHead.Fax;
            dto.TeNo = ReportHead.TeNo;
            dto.Vat = ReportHead.Vat;
            dto.Address = ReportHead.Address;
            var grouping =  GetApplicationsDto(forAttention);
            dto.Lines.AddRange(grouping);



            return dto;
        }

        }
}