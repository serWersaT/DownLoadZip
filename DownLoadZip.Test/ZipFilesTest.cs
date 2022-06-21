using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DownLoadZip.BLL.Services;
using System.IO;

namespace DownLoadZip.Test
{
    [TestClass]
    public class ZipFilesTest
    {
        public string localpathdownload = @"D:\archive";
        

        [TestMethod]
        public void GetFiles()
        {
            //arrange
            Service service = new Service();
            int countfiles = 113; //заданном размере 1024*100 и одном и томже архиве
            //act
            var getallfiles = service.GetFiles(localpathdownload);           
            //assert
            Assert.AreEqual(getallfiles, countfiles);
        }



    }
}
