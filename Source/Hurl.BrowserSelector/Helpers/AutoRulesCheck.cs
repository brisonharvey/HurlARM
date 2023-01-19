﻿using DotNet.Globbing;
using Hurl.Library.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Hurl.BrowserSelector.Helpers
{
    class AutoRulesCheck
    {
        static bool Check(string link, string[] rules)
        {
            var value = rules.FirstOrDefault(rule =>
            {
                return Glob.Parse(rule).IsMatch(link);
            }, null);
            return value != null;
        }

        /*
        public static bool CheckAndRun(string link, string[] rules)
        {
            var linkPattern = Check(link, rules);
            if (linkPattern != null)
            {
                Process.Start(linkPattern.Browser.ExePath, link);
                return true;
            }
            return false;
        }
        */

        public static bool CheckAllBrowserRules(string link, IEnumerable<Browser> browsers)
        {
            foreach (var browser in browsers)
            {
                if (browser.Rules != null)
                {
                    if (Check(link, browser.Rules))
                    {
                        Process.Start(browser.ExePath, link);
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
