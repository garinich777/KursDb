using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Windows;
using KursDb.VM;

namespace KursDb.Model
{
    public static class ExcelReport
    {
        public static void Report(List<OrderInfo> info, Filter filter)
        {
            Excel.Application oXL;
            Excel._Workbook oWB;
            Excel._Worksheet oSheet;
            Excel.Range oRng;

            try
            {
                oXL = new Excel.Application();
                oXL.Visible = true;
                oWB = oXL.Workbooks.Add(Missing.Value);
                oSheet = (Excel._Worksheet)oWB.ActiveSheet;

                oRng = oSheet.get_Range("A1", "E1");
                oRng.Merge();
                switch (filter)
                {
                    case Filter.Daily:
                        oRng.Value2 = $"Отчет на {DateTime.Now} за день";
                        break;
                    case Filter.Weekly:
                        oRng.Value2 = $"Отчет на {DateTime.Now} за неделю";
                        break;
                    case Filter.Monthly:
                        oRng.Value2 = $"Отчет на {DateTime.Now} за месяц";
                        break;
                    case Filter.Yearly:
                        oRng.Value2 = $"Отчет на {DateTime.Now} за год";
                        break;
                    case Filter.AllTime:
                        oRng.Value2 = $"Отчет на {DateTime.Now} за все время";
                        break;
                    default:
                        oRng.Value2 = $"Отчет на {DateTime.Now} за все время";
                        break;
                }

                oSheet.Cells[2, 1] = "№";
                oSheet.Cells[2, 2] = "Сумма заказа";
                oSheet.Cells[2, 3] = "Дата заказа";
                oSheet.Cells[2, 4] = "Приблеженное время выполнения, ч";
                oSheet.Cells[2, 5] = "Модели в заказе";

                int[,] value = new int[info.Count, 2];
                for (int i = 0; i < info.Count; i++)
                    value[i, 0] = info[i].Id;
                for (int i = 0; i < info.Count; i++)
                    value[i, 1] = int.Parse(info[i].Sum);
                string row_num = "B" + (info.Count + 2).ToString();
                oSheet.get_Range("A3", row_num).Value = value;

                DateTime[,] dateTimes = new DateTime[info.Count, 1];
                for (int i = 0; i < info.Count; i++)
                    dateTimes[i, 0] = info[i].Date;
                row_num = "C" + (info.Count + 2).ToString();
                oSheet.get_Range("C3", row_num).Value = dateTimes;
                oSheet.Range["C3", row_num].NumberFormat = @"DD.MM.YY h:mm:ss";

                int[,] value2 = new int[info.Count, 1];
                for (int i = 0; i < info.Count; i++)
                    value2[i, 0] = info[i].LeadTime;
                row_num = "D" + (info.Count + 2).ToString();
                oSheet.get_Range("D3", row_num).Value = value2;

                string[,] value3 = new string[info.Count, 1];
                for (int i = 0; i < info.Count; i++)
                    value3[i, 0] = info[i].Models;
                row_num = "E" + (info.Count + 2).ToString();
                oSheet.get_Range("E3", row_num).Value = value3;

                oSheet.get_Range("A2", "E2").Font.Bold = true;
                oSheet.get_Range("A2", "E2").VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                oRng = oSheet.get_Range("A1", row_num);
                oRng.EntireColumn.AutoFit();
            }
            catch (Exception theException)
            {
                string errorMessage;
                errorMessage = "Error: ";
                errorMessage = string.Concat(errorMessage, theException.Message);
                errorMessage = string.Concat(errorMessage, " Line: ");
                errorMessage = string.Concat(errorMessage, theException.Source);

                MessageBox.Show(errorMessage, "Error");
            }
        }
    }
}
