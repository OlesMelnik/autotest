using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v2;
using Google.Apis.Drive.v2.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using NUnit.Framework;

namespace GoogleDriveV2
{
    public class MyClass
    {

        static string[] Scopes = { DriveService.Scope.DriveReadonly };
        static string ApplicationName = "Drive API .NET Quickstart";

        public List<Google.Apis.Drive.v2.Data.File> retrieveAllFiles()
        {
            UserCredential credential;

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

            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            ////////////////////////////////////////////////////////////////////
            List<Google.Apis.Drive.v2.Data.File> result = new List<Google.Apis.Drive.v2.Data.File>();
            FilesResource.ListRequest request = service.Files.List();

            do
            {
                try
                {
                    FileList files = request.Execute();

                    result.AddRange(files.Items);
                    request.PageToken = files.NextPageToken;
                }
                catch (Exception e)
                {
                    Console.WriteLine("An error occurred: " + e.Message);
                    request.PageToken = null;
                }
            } while (!String.IsNullOrEmpty(request.PageToken));
            return result;
        }

        public static IEnumerable<TestCaseData> TestCases()

        {
            var testCases = new List<TestCaseData>();

            using (var fs = System.IO.File.OpenRead("C:/Users/olesm/source/repos/GoogleDriveApi/data.csv"))
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

            // act

            List<Google.Apis.Drive.v2.Data.File> file = retrieveAllFiles();
            if (file.Count == 0)
                Assert.Fail("Something wrong on google disk");

            // assert

            bool equal = false;


            foreach (var f in file)
            {
                TestContext.WriteLine(f.Title);
                if (f.Title == fileName)
                {
                    equal = true;
                }
            }

            Assert.IsTrue(equal == true);
        }
    }
}
