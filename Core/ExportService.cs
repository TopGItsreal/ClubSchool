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
        public static void ExportStatistics(List<StudentStatistic> studentStatistics, List<ClubStatistic> clubStatistics)
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

            for (int i = 0; i < studentStatistics.Count; i++)
            {
                worksheet.Cells[1][rowIndex] = studentStatistics[i].Student.LastName;
                worksheet.Cells[2][rowIndex] = studentStatistics[i].Student.FirstName;
                worksheet.Cells[3][rowIndex] = studentStatistics[i].AttendanceValue;
                rowIndex++;
            }

            worksheet.Columns.AutoFit();
            worksheet.Rows.AutoFit();

            worksheet = application.Worksheets.Add();

            rowIndex = 2;

            worksheet.Name = $"Статистика кружков";
            worksheet.Columns.AutoFit();
            worksheet.Rows.AutoFit();
            worksheet.Cells[1][1] = "Название крружка";
            worksheet.Cells[2][1] = "Посещаемость";

            for (int i = 0; i < clubStatistics.Count; i++)
            {
                worksheet.Cells[1][rowIndex] = clubStatistics[i].Club.Name;
                worksheet.Cells[2][rowIndex] = clubStatistics[i].AttendanceValue;
                rowIndex++;
            }

            worksheet.Columns.AutoFit();
            worksheet.Rows.AutoFit();

            application.Visible = true;
        }
    }
}
