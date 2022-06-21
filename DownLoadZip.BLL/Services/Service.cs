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

        public int GetZipArchives(string path, int maxlengthzip, Dictionary<string, string> referens, string savepath)
        {
            /*
             тут Dictionary<string, string> referens условие взаимосвязи один (key) ко многи (value). 
             В донном примере Key это файлы в папке Blog, а Value - данные в папке Article. 
             */
            int numbername = 0;
            long archivesize = 0;
            List<string> Dir = new List<string>();
            try
            {
                Dir = Directory.GetFileSystemEntries(path, "*", SearchOption.AllDirectories).Where(x => x.Contains(".")).ToList();
            }
            catch(Exception ex)
            { return 0; }

            var listNotConditions = (referens!=null && referens.Count > 0) ? Dir.Where(x => !referens.Any(d => x.Contains(d.Key)) && !referens.Any(d => x.Contains(d.Value))).ToList() : Dir;
            //var listNotConditions = Dir.RemoveAll(x => referens.Any(d => x.Contains(d.Key)));

            foreach (var file in listNotConditions)
            {
                GetArchive(file, savepath +  "ZipArchive_" + numbername.ToString(), out archivesize);
                if (archivesize >= maxlengthzip) numbername++;
            }

            numbername++;
            if (referens != null && referens.Count > 0)
            {
                foreach (var r in referens)
                {
                    var listWithConditions = Dir.Where(x => x.Contains(r.Key)).ToList();

                    foreach (var list in listWithConditions)
                    {
                        var name = list.Replace(localpathdownload + "\\", "");
                        name = name.Substring(name.LastIndexOf("\\")).Replace(".json", "");
                        var block = Dir.Where(x => x.Contains(name) && x.Contains(r.Value)).ToList();

                        GetArchive(list, savepath +  "ZipArchive_" + numbername.ToString(), out archivesize);
                        foreach (var file in block)
                        {
                            GetArchive(file, savepath + "ZipArchive_" + numbername.ToString(), out archivesize);
                        }

                        if (archivesize >= maxlengthzip) numbername++;

                    }
                }
            }


            return Directory.GetFiles(savepath).Length;
        }



        private ZipArchive GetArchive(string path, string savename, out long lengtharchive)
        {
            using (var stream = new FileStream(savename + ".zip", FileMode.OpenOrCreate))
            using (ZipArchive archive = new ZipArchive(stream, System.IO.Compression.ZipArchiveMode.Update))
            {
                var zipEntry = archive.CreateEntry(path.Replace(localpathdownload + @"\", ""));
                using (Stream entryStream = zipEntry.Open())
                {
                    var blockbyte = File.ReadAllBytes(path);
                    entryStream.Write(blockbyte, 0, blockbyte.Count());
                }

                lengtharchive = stream.Length;
                return archive;
            }
        }

    }
}
