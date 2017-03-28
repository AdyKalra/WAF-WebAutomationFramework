using System;
using System.Configuration;
using Excel = Microsoft.Office.Interop.Excel;

namespace WAF.Framework.HelperClasses
{
    public class ExcelReader
    {
        public static string ReadFrom(int _sheetNumber, int _columnNumber, int _rowNumber)
        {
            // Excel File Path
            string excelPath = AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings["ExcelPath"];
            // Setup
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            Excel.Range range;
            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Open(excelPath, 0, true, 5, "", "", true, Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(_sheetNumber);
            range = xlWorkSheet.UsedRange;
            int columnNumber = _columnNumber;
            string _value = ((range.Cells[_rowNumber, _columnNumber] as Excel.Range).Text);
            xlApp.Quit();
            return _value;
        }



        public static void Write()
        {
            Excel.Application oXL;
            Excel._Workbook oWB;
            Excel._Worksheet oSheet;
            Excel.Range oRng;
            object misvalue = System.Reflection.Missing.Value;
            try
            {
                //Start Excel and get Application object.
                oXL = new Excel.Application();
                oXL.Visible = true;

                //Get a new workbook.
                oWB = (Excel._Workbook)(oXL.Workbooks.Add(""));
                oSheet = (Excel._Worksheet)oWB.ActiveSheet;

                //Add table headers going cell by cell.
                oSheet.Cells[1, 1] = "First Name";
                oSheet.Cells[1, 2] = "Last Name";
                oSheet.Cells[1, 3] = "Full Name";
                oSheet.Cells[1, 4] = "Salary";

                //Format A1:D1 as bold, vertical alignment = center.
                oSheet.get_Range("A1", "D1").Font.Bold = true;
                oSheet.get_Range("A1", "D1").VerticalAlignment =
                    Excel.XlVAlign.xlVAlignCenter;

                // Create an array to multiple values at once.
                string[,] saNames = new string[5, 2];

                saNames[0, 0] = "John";
                saNames[0, 1] = "Smith";
                saNames[1, 0] = "Tom";

                saNames[4, 1] = "Johnson";

                //Fill A2:B6 with an array of values (First and Last Names).
                oSheet.get_Range("A2", "B6").Value2 = saNames;

                //Fill C2:C6 with a relative formula (=A2 & " " & B2).
                oRng = oSheet.get_Range("C2", "C6");
                oRng.Formula = "=A2 & \" \" & B2";

                //Fill D2:D6 with a formula(=RAND()*100000) and apply format.
                oRng = oSheet.get_Range("D2", "D6");
                oRng.Formula = "=RAND()*100000";
                oRng.NumberFormat = "$0.00";

                //AutoFit columns A:D.
                oRng = oSheet.get_Range("A1", "D1");
                oRng.EntireColumn.AutoFit();

                oXL.Visible = false;
                oXL.UserControl = false;
                oWB.SaveAs(@"C:\Automation\WAF\WAF v2.0.5\Reports\test505.xlsx", Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing,
                    false, false, Excel.XlSaveAsAccessMode.xlNoChange,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                oWB.Close();
                oXL.Quit();

            }
            catch
            { }
        }

    }
}
