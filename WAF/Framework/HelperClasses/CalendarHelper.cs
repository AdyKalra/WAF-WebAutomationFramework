using OpenQA.Selenium;
using System;
using System.Globalization;
using WAF.Framework.BaseClasses;

namespace WAF.Framework.HelperClasses
{
    public class CalendarHelper
    {
        public static void DatePicker(By _calendarIcon, string _date)
        {
            if (_date != "")
            {
                // Substring the date
                string _mounth = _date.Substring(0, 2);
                string _day = _date.Substring(3, 2);
                string _year = _date.Substring(6, 4);
                // Convert month int to month name
                string _mounthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Convert.ToInt32(_mounth));
                // Click the Calendar
                Driver.ClickOn(_calendarIcon);
                Driver.ClickOn(By.XPath("//button[@class='btn btn-default btn-sm uib-title']"));
                // Select the year and month
                Driver.WaitForElement(By.XPath("//span[contains(.,'March')]"));
                // Get the year from calendar
                string entryYear = Driver.Instance.FindElement(By.XPath("//strong[@class='ng-binding']")).Text.ToString();
                if (_year == entryYear)
                {
                    Driver.ClickOn(By.XPath("//span[contains(.,'" + _mounthName + "')]"));
                }
                else if ((Convert.ToInt32(entryYear)) > (Convert.ToInt32(_year)))
                {
                    Driver.ClickOn(By.XPath("//button[@class='btn btn-default btn-sm pull-left uib-left']"));
                    Driver.ClickOn(By.XPath("//span[contains(.,'" + _mounthName + "')]"));
                }
                else
                {
                    Driver.ClickOn(By.XPath("//button[@class='btn btn-default btn-sm pull-right uib-right']"));
                    Driver.ClickOn(By.XPath("//span[contains(.,'" + _mounthName + "')]"));
                }
                // Pick the Date
                Driver.ClickOn(By.XPath("//span[@class='ng-binding' and contains(.,'" + _day + "')]//parent::button"));
            }
        }
    }
}
