using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview_Exercise
{
    public class FileIo: IFileIo
    {
        private static readonly string DirectoryPath =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
                "Interview-Exercise");               

        public List<IPersist> ReadFile<T>(string fileName, T t) where T : IPersist
        {
            var filePath = Path.Combine(DirectoryPath, fileName);
            if (!File.Exists(filePath))
            {
                return null;
            }
            var contents = File.ReadAllText(filePath);
            return Parser.ParseCsv(contents, t);
        }

        public List<IPersist> ReadAllFiles<T>(T t) where T : IPersist
        {
            if (!Directory.Exists(DirectoryPath))
            {
                return null;
            }

            var masterList = new List<IPersist>();
            foreach (var file in Directory.GetFiles(DirectoryPath))
            {
                masterList.AddRange(ReadFile(file, t));
            }
            return masterList;
        }

        public void WriteFile(string fileName, List<IPersist> items)
        {
            var filePath = Path.Combine(DirectoryPath, fileName);
            if (!Directory.Exists(DirectoryPath))
            {
                Directory.CreateDirectory(DirectoryPath);
            }
            File.WriteAllText(filePath, Parser.CreateCsv(items));
        }

        public void DeleteAllFiles()
        {
            if (!Directory.Exists(DirectoryPath))
            {
                return;
            }
            foreach (var file in Directory.GetFiles(DirectoryPath))
            {
                var filePath = Path.Combine(DirectoryPath, file);
                File.Delete(filePath);
            }
        }

    }
    
}
