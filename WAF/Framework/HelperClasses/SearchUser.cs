/*
--------------------------------------------------------------------------------------------------------------------
Web Automation Framework - WAF v2.0.8
Designed and Developd by Davron Aliyev
Copyright (c) 2017 Document Storage Systems, Inc.
All rights reserved
--------------------------------------------------------------------------------------------------------------------
*/

using OpenQA.Selenium;
using WAF.BaseClasses;
using WAF.Framework.BaseClasses;

namespace WAF.Framework.HelperClasses
{
    class SearchUser
    {
        internal static void ByName(string _name)
        {
            int collection = 5;

            for (int i = 0; i <= collection; i++)
            {
                if (VerifyElement.IsElementPresent(By.XPath("//td[contains(., '" + _name + "')]")))
                {
                    ReportHelper.PassLog(_name + " - matching user is found.");
                    break;
                }
                Driver.ClickOn(By.XPath("//a[contains(., 'Next')]"));
            }
        }
    }
}
