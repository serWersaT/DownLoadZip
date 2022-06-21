using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;
using System.Linq;
using DownLoadZip.BLL.Model;

namespace DownLoadZip.BLL.Services
{
    public class Service
    {
        public string localpathdownload = @"D:\archive";
        public long maxlength = 1024 * 100;

        public int GetFiles(string path)
        {
            List<string> blockfiles = new List<string>();

            long filesize = 0;
            int numbername = 0;
            bool addfile = true;
            var str = "";

            IEnumerable<string> Dir = Directory.GetFileSystemEntries(path, "*", SearchOption.AllDirectories).Where(x => x.Contains("."));
            var Blogs = Dir.Where(x => x.Contains("Blog")).Select(c => c.Substring(c.LastIndexOf('\\') + 1).Replace(".json", "")).ToList();

            try
            {

                foreach (var file in Dir.OrderBy(x => x))
                {
                    filesize += new System.IO.FileInfo(file).Length;

                    str = file.Replace(localpathdownload + @"\Article\", "");
                    str = str.Substring(0, str.IndexOf('\\'));

                    if (file.Contains("Article") && blockfiles.Any(x => x.Contains(str)) && file.Contains(str))
                    {
                        addfile = true;
                    }
                    else addfile = false;

                    if (filesize >= maxlength && addfile == false)
                    {
                        GetArchive(blockfiles.Distinct().ToList(), "SuperZip_" + numbername.ToString());
                        numbername++;
                        filesize = 0;
                        blockfiles.Clear();
                    }

                    blockfiles.Add(file);
                }
                return Directory.GetFiles(@"D:\С# примеры\Работа\SaveTest\").Length;
            }
            catch(Exception ex)
            {
                return 0;
            }
        }

        private ZipArchive GetArchive(List<string> path, string savename)
        {
            using (var stream = File.Create(@"D:\С# примеры\Работа\SaveTest\" + savename + ".zip"))
            using (ZipArchive archive = new ZipArchive(stream, System.IO.Compression.ZipArchiveMode.Create))
            {
                foreach (var item in path)
                {
                    var zipEntry = archive.CreateEntry(item.Replace(localpathdownload + @"\", ""));
                    using (Stream entryStream = zipEntry.Open())
                    {
                        var blockbyte = File.ReadAllBytes(item);
                        entryStream.Write(blockbyte, 0, blockbyte.Count());
                    }
                }
                return archive;
            }
           
        }
    }
}
