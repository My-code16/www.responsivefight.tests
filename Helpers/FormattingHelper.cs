using www.responsivefight.tests.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace www.responsivefight.tests.Helpers
{
   public class FormattingHelper
    {
        public void LogTestCycle(IntCycleEn testCycleEn, string testName)
        {
            Debug.WriteLine(string.Format("{0} {1} Test:", testName ?? "TestName NA", Enum.GetName(typeof(IntCycleEn), testCycleEn)));
        }
    }
}
