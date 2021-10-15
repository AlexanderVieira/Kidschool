using System.Collections.Generic;
using System.Data;
using System.Linq;
using Universal.EBI.Reports.API.Models;

namespace Universal.EBI.Reports.API.Services
{
    public static class SeedDataTable
    {
        public static DataTable GetClassroomChilds(List<Classroom> classrooms)
        {
            var dt = new DataTable();
            dt.Columns.Add("FullName");
            dt.Columns.Add("BurthDate");
            dt.Columns.Add("GenderType");
            dt.Columns.Add("Id");
            dt.Columns.Add("StartTimeMeeting");
            dt.Columns.Add("EndTimeMeeting");
            dt.Columns.Add("ResponsibleId");
            DataRow row;
            for (int i = 0; i < classrooms.Count; i++)
            {
                for (int j = 0; j < classrooms[i].Childs.Count; j++)
                {
                    var child = classrooms[i].Childs.ToList()[j];
                    row = dt.NewRow();
                    row["FullName"] = child.FullName;
                    row["BurthDate"] = child.BirthDate.ToShortDateString();
                    row["GenderType"] = child.GenderType;
                    row["Id"] = child.Id.ToString();
                    row["StartTimeMeeting"] = child.StartTimeMeeting.Value.ToShortTimeString();
                    row["EndTimeMeeting"] = child.EndTimeMeeting.Value.ToShortTimeString();
                    row["ResponsibleId"] = child.Responsibles.ToList()[0].Id.ToString();
                    dt.Rows.Add(row);
                    
                }
            }
            return dt;
        }

        public static DataTable GetClassroomResponsibles(List<Classroom> classrooms)
        {
            var dt = new DataTable();
            dt.Columns.Add("ResponsibleId");
            dt.Columns.Add("Cpf");
            dt.Columns.Add("FullName");
            dt.Columns.Add("KinshipType");
            dt.Columns.Add("PhoneNumber");
            
            DataRow row;
            for (int i = 0; i < classrooms.Count; i++)
            {
                for (int j = 0; j < classrooms[i].Childs.Count; j++)
                {
                    for (int k = 0; k < classrooms[j].Childs.ToList()[j].Responsibles.Count; k++)
                    {
                        var responsible = classrooms[k].Childs.ToList()[k].Responsibles.ToList()[k];
                        row = dt.NewRow();
                        row["ResponsibleId"] = responsible.Id.ToString();
                        row["Cpf"] = responsible.Cpf.Number;
                        row["FullName"] = responsible.FullName;
                        row["KinshipType"] = responsible.KinshipType;
                        row["PhoneNumber"] = responsible.Phones.ToList()[k].Number;
                        dt.Rows.Add(row);
                    }
                }
            }
            return dt;
        }
    }
}
