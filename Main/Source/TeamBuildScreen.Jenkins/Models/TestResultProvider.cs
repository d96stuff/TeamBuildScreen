// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestResultProvider.cs" company="Jim Liddell">
//   Copyright © 2011 Jim Liddell. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TeamBuildScreen.Hudson.Models
{
    using System;
    using System.IO;
    using System.Net;
    using System.Xml.Serialization;

    using TeamBuildScreen.Hudson.Models.Tasks.JUnit;

    public static class TestResultProvider
    {
        public static TestResult Get(string hudsonUri, string name, int number)
        {
            var client = new WebClient();
            string testResultString;
            try
            {
                testResultString =
                    client.DownloadString(
                        hudsonUri + "job/" + Uri.EscapeUriString(name) + "/" + number + "/testReport/api/xml");
            }
            catch (WebException)
            {
                return null;
            }

            TestResult testResult = null;
            var reader = new StringReader(testResultString);

            if (testResultString.StartsWith("<testResult>"))
            {
                testResult = new TestResult();
                var serializer = new XmlSerializer(testResult.GetType());
                testResult = (TestResult)serializer.Deserialize(reader);
            }
            else if (testResultString.StartsWith("<matrixTestResult>"))
            {
                testResult = new MatrixTestResult();
                var serializer = new XmlSerializer(testResult.GetType());
                testResult = (MatrixTestResult)serializer.Deserialize(reader);
            }
            else
            {
                var testResultType = testResultString.Substring(1, testResultString.IndexOf('>') - 1);
                throw new NotSupportedException(string.Format("Unrecognised test result type '{0}'", testResultType));
            }

            return testResult;
        }
    }
}