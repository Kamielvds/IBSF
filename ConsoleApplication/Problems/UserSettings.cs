using System.Collections.Generic;

namespace ConsoleApplication.Problems
{
    public class UserSettings
    {
        // bool
        public static bool ShowErrors = true;
        public static bool ShowWarnings = true;

        // int
        public static int LinesBeforeUser = 0;
        public static int LinesAfterUser = 0;

        // only used for listing all settings
        public static Dictionary<string, object> Settings =>
            new Dictionary<string, object>
            {
                { "ShowErrors", ShowErrors },
                { "ShowWarnings", ShowWarnings },
                { "LinesBeforeUser", LinesBeforeUser },
                { "LinesAfterUser", LinesAfterUser }
            };
    }
}

}