using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Assert = NUnit.Framework.Assert;

namespace TestGoogleDriveApi
{
    [TestClass]
    public class UnitTest1
    {

        static string[] Scopes = { DriveService.Scope.DriveReadonly };
        static string ApplicationName = "Drive API .NET Quickstart";

        List<string> getFileNames()
        {
            UserCredential credential;

            List<string> filesNames = new List<string>();


            using (var stream =
                new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Drive API service.
            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            // Define parameters of request.
            FilesResource.ListRequest listRequest = service.Files.List();
            listRequest.PageSize = 50;
            listRequest.Fields = "nextPageToken, files(name, parents, fileExtension)";
            //listRequest.Q = "mimeType = 'application/vnd.google-apps.folder'";

            // List files.
            IList<Google.Apis.Drive.v3.Data.File> files = listRequest.Execute()
                .Files;
            Console.WriteLine("Files:");
            if (files != null && files.Count > 0)
            {
                foreach (var file in files)
                {
                    List<string> folders = new List<string>();

                    //if (file.Parents != null)
                    //{

                    //    foreach (string p in file.Parents)
                    //    {
                    //        var folder = service.Files.Get(p).Execute();
                    //        folders.Add(folder.Name);
                    //    }

                    //    //Console.WriteLine("{0}/{1}", String.Join(",", folders.ToArray()), file.Name);
                    //    filesNames.Add(String.Join(",", folders.ToArray()) + "/" + file.Name);
                    //}
                    //else
                     //Console.WriteLine("{0}", file.Name);
                     filesNames.Add(file.Name);
                }
                return filesNames;
            }
            else
            {
                //Console.WriteLine("No files found.");
                return new List<string>();
            }
            //Console.Read();
        }
    public static IEnumerable<TestCaseData> TestCases()

        {
            var testCases = new List<TestCaseData>();

            using (var fs = File.OpenRead("C:/Users/olesm/source/repos/GoogleDriveApi/data.csv"))
            using (var sr = new StreamReader(fs))
            {
                string line = string.Empty;
                while (line != null)
                {
                    line = sr.ReadLine();
                    if (line != null)
                    {
                        string[] split = line.Split(new char[] { ',' },
                            StringSplitOptions.None);

                        string expectedNumber = Convert.ToString(split[0]);

                        var testCase = new TestCaseData(expectedNumber);
                        testCases.Add(testCase);
                    }
                }

                return testCases;
            }
        }

        [TestCaseSource("TestCases")]

        public void TestGoogleDriveV3(string fileName)
        {

            Console.WriteLine("Some text here");
            // arrange

            //var test = new GoogleDriveApi();

            // act

            List<string> file = getFileNames();
            if (file.Count == 0)
                Assert.Fail("Something wrong on google disk");

            // assert

            bool equal = false;

            foreach(var f in file)
            {
                NUnit.Framework.TestContext.WriteLine(f); ;
                if(f == fileName)
                {
                    equal = true;
                }
            }

            Assert.IsTrue(equal == true);
        }

    }
}
