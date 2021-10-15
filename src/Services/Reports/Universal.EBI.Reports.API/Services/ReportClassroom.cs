using Microsoft.Reporting.NETCore;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Universal.EBI.Reports.API.Models;
using Universal.EBI.Reports.API.Services.Interfaces;

namespace Universal.EBI.Reports.API.Services
{
    public class ReportClassroom : IReportClassroom
    {
        public ICollection<Classroom> Classrooms { get; set; }

        public async Task<byte[]> GenerateReport(string renderFormat)
        {
            var dt = SeedDataTable.GetClassroomResponsibles(Classrooms.ToList());
            var folderName = Path.Combine("Reports");
            var path = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            var fullPath = Path.Combine(path, "ReportClassroomResponsible.rdlc");
            var parameters = new[] 
            { 
                new ReportParameter("prRegion", Classrooms.ToList()[0].Region), 
                new ReportParameter("prChurch", Classrooms.ToList()[0].Church),
                new ReportParameter("prMeetingDate", Classrooms.ToList()[0].MeetingTime.ToShortDateString()),
                new ReportParameter("prMeetingTime", Classrooms.ToList()[0].MeetingTime.ToShortTimeString()),
                new ReportParameter("prFirstName", Classrooms.ToList()[0].Educator.FirstName),
                new ReportParameter("prClassroomType", Classrooms.ToList()[0].ClassroomType),
                new ReportParameter("prLunch", Classrooms.ToList()[0].Lunch)
            };
            LocalReport lr = new LocalReport();            
            lr.DataSources.Add(new ReportDataSource("dsClassroomResponsible", dt));
            lr.ReportPath = fullPath;
            lr.SetParameters(parameters);
            
            lr.SubreportProcessing += new SubreportProcessingEventHandler(SubReportProcessing);
            lr.Refresh();
            var file = lr.Render(renderFormat);
            return await Task.FromResult(file);
        }

        private void SubReportProcessing(object sender, SubreportProcessingEventArgs e)
        {
            //int empId = int.Parse(e.Parameters["EmployeeId"].Values[0].ToString());
            //string ResponsibleId = e.Parameters["ResponsibleId"].Values[0].ToString();
            DataTable dt2 = null;
            var rows = SeedDataTable.GetClassroomChilds(Classrooms.ToList()).Select();
            if (rows.Any())
                dt2 = rows.CopyToDataTable();
            //foreach (var row in rows)
            //    dt2.ImportRow(row);
            var ds = new ReportDataSource("dsClassroomChild", dt2);
            e.DataSources.Add(ds);
        }
    }
}
