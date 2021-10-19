//using Microsoft.Reporting.NETCore;
using System;
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
            //ICollection<Child> childs = null;
            //ICollection<Responsible> responsibles = null;
            //var inicialDate = DateTime.Now.Date.AddDays(-3.0);
            //var finalDate = DateTime.Now.Date;
            //var classrooms = Classrooms.GroupBy(x => x.MeetingTime.Date).Select(x => new { Count = x.Count(), Date = (DateTime)x.Key }).ToList();
            //var classrooms1 = Classrooms.GroupBy(x => x.MeetingTime.Date >= inicialDate && x.MeetingTime.Date <= finalDate, y => y);
            //var classrooms2 = Classrooms.Where(x => x.MeetingTime.Date >= inicialDate && x.MeetingTime.Date <= finalDate).ToList();
            //foreach (var item in classrooms2)
            //{
            //    childs = item.Childs;

            //}
            //childs.ToList();
            //foreach (var item in childs)
            //{
            //    responsibles = item.Responsibles;
            //}
            //responsibles.ToList();
            //var newClassrooms = new List<Classroom>();
            //foreach (var item in classrooms1)
            //{
            //    var date = item.Key;
            //    if (date)
            //    {
            //        foreach (var classroom in item)
            //        {
            //            newClassrooms.Add(classroom);
            //        }
            //    }


            //}
            //newClassrooms.Select(x => x.Childs.ToList()[0]. == x.Childs.ToList()[0].Responsibles.ToList()[0].Id);
            //var dt = SeedDataTable.GetClassroomResponsibles(Classrooms.ToList());
            //var folderName = Path.Combine("Reports");
            //var path = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            //var fullPath = Path.Combine(path, "ReportClassroomResponsible.rdlc");
            //ReportParameter[] parameters = new ReportParameter[7];
            //LocalReport lr = new LocalReport();
            //byte[] file = Array.Empty<byte>();
            //for (int i = 0; i < Classrooms.ToList().Count; i++)
            //{
            //    for (int j = 0; j < Classrooms.ToList()[i].Childs.ToList().Count; j++)
            //    {
            //        var responsibleId = 0;
            //        for (int k = 0; k < Classrooms.ToList()[i].Childs.ToList()[j].Responsibles.ToList().Count; k++)
            //        {
            //            responsibleId = Classrooms.ToList()[i].Childs.ToList()[j].Responsibles.ToList()[k].Id;
            //            parameters = new[]
            //            {
            //                //new ReportParameter("prResponsibleId", responsibleId.ToString()),
            //                new ReportParameter("prRegion", Classrooms.ToList()[i].Region),
            //                new ReportParameter("prChurch", Classrooms.ToList()[i].Church),
            //                new ReportParameter("prMeetingDate", Classrooms.ToList()[i].MeetingTime.ToShortDateString()),
            //                new ReportParameter("prMeetingTime", Classrooms.ToList()[i].MeetingTime.ToShortTimeString()),
            //                new ReportParameter("prFirstName", Classrooms.ToList()[i].Educator.FirstName),
            //                new ReportParameter("prClassroomType", Classrooms.ToList()[i].ClassroomType),
            //                new ReportParameter("prLunch", Classrooms.ToList()[i].Lunch)
            //            };
            //            lr.DataSources.Add(new ReportDataSource("dsClassroomResponsible", dt));
            //        }
            //    }                

            //}

            //var parameters = new[]
            //{
            //    //new ReportParameter("prResponsibleId", responsibleId.ToString()),
            //    new ReportParameter("prRegion", Classrooms.ToList()[0].Region),
            //    new ReportParameter("prChurch", Classrooms.ToList()[0].Church),
            //    new ReportParameter("prMeetingDate", Classrooms.ToList()[0].MeetingTime.ToShortDateString()),
            //    new ReportParameter("prMeetingTime", Classrooms.ToList()[0].MeetingTime.ToShortTimeString()),
            //    new ReportParameter("prFirstName", Classrooms.ToList()[0].Educator.FirstName),
            //    new ReportParameter("prClassroomType", Classrooms.ToList()[0].ClassroomType),
            //    new ReportParameter("prLunch", Classrooms.ToList()[0].Lunch)
            //};
            //lr.DataSources.Add(new ReportDataSource("dsClassroomResponsible", dt));
            //lr.ReportPath = fullPath;
            //lr.SetParameters(parameters);
            //lr.SubreportProcessing += new SubreportProcessingEventHandler(SubReportProcessing);
            //lr.Refresh();
            //var file = lr.Render(renderFormat);            
            //return await Task.FromResult(file);
            return await Task.FromResult(new byte[] { });
        }

        //private void SubReportProcessing(object sender, SubreportProcessingEventArgs e)
        //{
        //    //int empId = int.Parse(e.Parameters["ResponsibleId"].Values[0].ToString());            
        //    //var responsibleId = int.Parse(e.Parameters["prResponsibleId"].Values[0].ToString());
        //    DataTable dt2 = null;
        //    var rows = SeedDataTable.GetClassroomChilds(Classrooms.ToList()).Select();  //.Select("ResponsibleId=" + responsibleId);
        //    if (rows.Any())
        //        dt2 = rows.CopyToDataTable();
        //    //foreach (var row in rows)
        //    //    dt2.ImportRow(row);
        //    var ds = new ReportDataSource("dsClassroomChild", dt2);
        //    e.DataSources.Add(ds);
        //}
    }
}
