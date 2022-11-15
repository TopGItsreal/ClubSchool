using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace Core
{
    public static class ExportService
    {
        public static void ExportStudentStatistics(List<StudentStatistic> statistics)
        {
            var application = new Excel.Application();
            Excel.Workbook workbook = application.Workbooks.Add(Type.Missing);
            Excel.Worksheet worksheet = application.Worksheets.Item[1];
            int rowIndex = 2;

            worksheet.Name = $"Статистика студентов";
            worksheet.Columns.AutoFit();
            worksheet.Rows.AutoFit();
            worksheet.Cells[1][1] = "Фамилия";
            worksheet.Cells[2][1] = "Имя";
            worksheet.Cells[3][1] = "Посещаемость";

            for (int i = 0; i < statistics.Count; i++)
            {
                worksheet.Cells[1][rowIndex] = statistics[i].Student.LastName;
                worksheet.Cells[2][rowIndex] = statistics[i].Student.FirstName;
                worksheet.Cells[3][rowIndex] = statistics[i].AttendanceValue;
                rowIndex++;
            }

            worksheet.Columns.AutoFit();
            worksheet.Rows.AutoFit();
            application.Visible = true;
        }

        public static void ExportClubStatistics(List<ClubStatistic> statistics)
        {
            var application = new Excel.Application();
            Excel.Workbook workbook = application.Workbooks.Add(Type.Missing);
            Excel.Worksheet worksheet = application.Worksheets.Item[1];
            int rowIndex = 2;

            worksheet.Name = $"Статистика кружков";
            worksheet.Columns.AutoFit();
            worksheet.Rows.AutoFit();
            worksheet.Cells[1][1] = "Название крружка";
            worksheet.Cells[2][1] = "Посещаемость";

            for (int i = 0; i < statistics.Count; i++)
            {
                worksheet.Cells[1][rowIndex] = statistics[i].Club.Name;
                worksheet.Cells[2][rowIndex] = statistics[i].AttendanceValue;
                rowIndex++;
            }

            worksheet.Columns.AutoFit();
            worksheet.Rows.AutoFit();
            application.Visible = true;
        }
    }
}
