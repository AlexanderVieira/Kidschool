using FastReport.Export.PdfSimple;
using FastReport.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using Universal.EBI.Core.DomainObjects;

namespace Universal.EBI.Reports.API.Services
{
    public static class HelperFastReport
    {
        public static WebReport WebReport(string nomeDoRelatorioFrx)
        {
            var result = new WebReport();
            result.Report.Load(Path.Combine("Reports", nomeDoRelatorioFrx));            
            return result;
        }
        public static byte[] ExportarPdf(WebReport webReport)
        {
            webReport.Report.Prepare();

            using (MemoryStream ms = new MemoryStream())
            {
                var pdfExport = new PDFSimpleExport();
                pdfExport.Export(webReport.Report, ms);
                ms.Flush();
                return ms.ToArray();
            }
        }

        public static DataTable GetDataTable<TEntity>(IEnumerable<TEntity> table, string name) where TEntity : class
        {
            var offset = 78;
            DataTable result = new DataTable(name);
            PropertyInfo[] infos = typeof(TEntity).GetProperties();
            foreach (PropertyInfo info in infos)
            {
                if (info.PropertyType.IsGenericType && info.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    result.Columns.Add(new DataColumn(info.Name, Nullable.GetUnderlyingType(info.PropertyType)));                   
                }                
                else
                {                    
                    if (info.PropertyType.BaseType == typeof(Enum))
                    {                        
                        var str = string.Empty.GetType();
                        result.Columns.Add(new DataColumn(info.Name, str));
                    }
                    else if (info.PropertyType == typeof(Cpf))
                    {
                        var str = string.Empty.GetType();
                        result.Columns.Add(new DataColumn(info.Name, str));
                    }
                    else 
                    {
                        result.Columns.Add(new DataColumn(info.Name, info.PropertyType));
                    }                    
                }
            }

            foreach (var el in table)
            {
                DataRow row = result.NewRow();
                foreach (PropertyInfo info in infos)
                {
                    if (info.PropertyType.IsGenericType && info.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        object t = info.GetValue(el);
                        if (t == null)
                        {
                            t = Activator.CreateInstance(Nullable.GetUnderlyingType(info.PropertyType));
                        }

                        row[info.Name] = t;
                    }
                    else
                    {                        
                        if (info.PropertyType == typeof(byte[]))
                        {
                            var imageData = (byte[])info.GetValue(el);
                            var bytes = new byte[imageData.Length - offset];
                            Array.Copy(imageData, offset, bytes, 0, bytes.Length);
                            row[info.Name] = bytes;
                        }                        
                        else if (info.PropertyType.BaseType == typeof(Enum))
                        {                            
                            var type = (Enum)info.GetValue(el);
                            var eType = type.GetType();
                            var eName = Enum.GetName(eType, type);
                            row[info.Name] = eName;
                        }
                        else if (info.PropertyType == typeof(Cpf))
                        {
                            var type = (Cpf)info.GetValue(el);
                            if (type != null)
                            {                               
                                var param = type.Number;
                                row[info.Name] = param;
                            }
                            
                        }
                        else
                        {
                            row[info.Name] = info.GetValue(el);
                        }

                    }
                }
                result.Rows.Add(row);
            }

            return result;
        }
    }
}
