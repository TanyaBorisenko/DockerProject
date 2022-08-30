using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DockerProject.Utils
{
    public class Parser
    {
        public static IList<string> StringParse(string parseString, string pattern)
        {
            var split = Regex.Split(parseString, pattern);

            return split;
        }
    }
}