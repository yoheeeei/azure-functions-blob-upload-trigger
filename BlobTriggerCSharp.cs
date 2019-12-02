using System;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace Company.Function {
    public static class BlobTriggerCSharp {

        [FunctionName ("BlobTriggerCSharp")]
        public static void Run ([BlobTrigger ("samples-workitems/{name}", Connection = "hogeyoheinakamura_STORAGE")] Stream myBlob, string name, ILogger log) {
            var nameWithoutExtension = GetPathWithoutExtension (name);
            var splittedNames = nameWithoutExtension.Split ("_");

            var splittedYear = GetSplittedDate (splittedNames[0]);
            var splittedStartTimes = GetSplittedTime (splittedNames[1]);
            var splittedEndTimes = GetSplittedTime (splittedNames[2]);

            var startDateAndTime = GenerateStringDate (splittedYear, splittedStartTimes);
            var endDateAndTime = GenerateStringDate (splittedYear, splittedEndTimes);

            var start = DateTime.Parse (startDateAndTime);
            var end = DateTime.Parse (endDateAndTime);
        }

        private static string GenerateStringDate (string[] date, string[] time) {
            return $"20{date[0]}/{date[1]}/{date[2]} {time[0]}:{time[1]}:{time[2]}";
        }

        private static string[] GetSplittedTime (string rawTime) {
            return Regex.Split (rawTime, @"(?<=\G.{2})(?!$)");
        }

        private static string[] GetSplittedDate (string rawDate) {
            var date = rawDate.Trim ('P');
            return Regex.Split (date, @"(?<=\G.{2})(?!$)");
        }
        private static string GetPathWithoutExtension (string path) {
            var extension = Path.GetExtension (path);
            if (string.IsNullOrEmpty (extension)) {
                return path;
            }
            return path.Replace (extension, string.Empty);
        }
    }
}