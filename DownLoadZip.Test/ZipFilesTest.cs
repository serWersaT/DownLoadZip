using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DownLoadZip.BLL.Services;
using System.IO;
using System.Collections.Generic;

namespace DownLoadZip.Test
{
    [TestClass]
    public class ZipFilesTest
    {
        public string localpathdownload = @"D:\archive";
        

        [TestMethod]
        public void GetArchivesNormalTest()
        {
            //arrange
            System.IO.DirectoryInfo directory = new System.IO.DirectoryInfo(@"D:\С# примеры\Работа\SaveTest\GetArchivesNormalTest\");
            foreach (FileInfo file in directory.GetFiles())
            {
                file.Delete();
            }
            Service service = new Service();
            Dictionary<string, string> referer = new Dictionary<string, string>()
            {
                ["Blog"] = "Article"
            };
            int countfiles = 37; //заданном размере 1024*100 и одном и томже архиве
            //act
            var getallfiles = service.GetZipArchives(localpathdownload, 1024 * 100, referer, @"D:\С# примеры\Работа\SaveTest\GetArchivesNormalTest\");    

            //assert
            Assert.AreEqual(getallfiles, countfiles);
        }


        [TestMethod]
        public void GetArchives_Null_Comditioms_Test()
        {
            //arrange
            System.IO.DirectoryInfo directory = new System.IO.DirectoryInfo(@"D:\С# примеры\Работа\SaveTest\GetArchives_Null_Comditioms_Test\");
            foreach (FileInfo file in directory.GetFiles())
            {
                file.Delete();
            }
            Service service = new Service();
            int countfiles = 38; //заданном размере 1024*100 и одном и томже архиве
            //act
            var getallfiles = service.GetZipArchives(localpathdownload, 1024 * 100, null, @"D:\С# примеры\Работа\SaveTest\GetArchives_Null_Comditioms_Test\");

            //assert
            Assert.AreEqual(getallfiles, countfiles);
        }

        [TestMethod]
        public void GetArchives_Null_FilePath_Test()
        {
            //arrange
            System.IO.DirectoryInfo directory = new System.IO.DirectoryInfo(@"D:\С# примеры\Работа\SaveTest\GetArchives_Null_FilePath_Test\");
            foreach (FileInfo file in directory.GetFiles())
            {
                file.Delete();
            }
            Service service = new Service();
            int countfiles = 0; //заданном размере 1024*100 и одном и томже архиве
            //act
            var getallfiles = service.GetZipArchives("", 1024 * 100, null, @"D:\С# примеры\Работа\SaveTest\GetArchives_Null_FilePath_Test\");

            //assert
            Assert.AreEqual(getallfiles, countfiles);
        }

    }
}
