using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace inSSIDer.UnhandledException
{
    public class PathScrubber
    {
        #region Fields

        //This holds a list of folder names that will be the keys to finding the part of the path we don't want
        public static string[] keys = new string[] { "MetaScanner", "MetaGeek.WiFi", "MetaGeek.Gps", "ManagedWiFi", "IoctlNdis" };

        #endregion Fields

        #region Public Methods

        /// <summary>
        /// Strips the path leading up to the project folder out of the supplied text
        /// </summary>
        /// <param name="trace">The text to scrub</param>
        /// <returns>The sanitized text</returns>
        public static string Scrub(string trace)
        {
            try
            {
                StringBuilder sbOutput = new StringBuilder();

                foreach (string line in trace.Split(Environment.NewLine.ToCharArray()))
                {
                    //Skip empty lines
                    if (string.IsNullOrEmpty(line)) continue;

                    sbOutput.AppendLine(RemovePathLine(line));
                }

                return sbOutput.ToString();
            }
            catch
            {
                //If for any reason there a problem, just return the unmodified trace
                return trace;
            }
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Strips the path leading up to the project folder out of the supplied line
        /// </summary>
        /// <param name="line">A line to scrub</param>
        /// <returns>The sanitized line</returns>
        private static string RemovePathLine(string line)
        {
            try
            {
                if (string.IsNullOrEmpty(line)) return string.Empty;

                //Get the start and end of the path
                //TODO: Check for UNC paths if standard path isn't found
                int start = line.IndexOf(@":\") - 1;
                int count = line.Length /*LastIndexOf(@":")*/ - start;

                if (start < 0) return line;

                //Remove everything except the actual path
                string path = line.Substring(start, count);

                string[] parts = path.Split('\\');

                StringBuilder sbParts = new StringBuilder();

                //Start at the end
                foreach (string part in parts.Reverse())
                {
                    //insert is used because we're looping backwards
                    sbParts.Insert(0, part);

                    if (keys.ToList().Contains(part, StringComparer.InvariantCultureIgnoreCase))
                    {
                        //We found a keyword
                        break;
                    }
                    sbParts.Insert(0, "\\");
                }

                string newpath = line.Replace(path, sbParts.ToString());

                return newpath;
            }
            catch
            {
                //If for any reason there a problem, just return the unmodified line
                return line;
            }
        }

        #endregion Private Methods
    }
}