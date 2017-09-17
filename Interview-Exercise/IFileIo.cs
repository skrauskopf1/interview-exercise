using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview_Exercise
{
    public interface IFileIo
    {
        List<IPersist> ReadFile<T>(string fileName, T t) where T : IPersist;
        List<IPersist> ReadAllFiles<T>(T t) where T : IPersist;
        void WriteFile(string fileName, List<IPersist> items);
        void DeleteAllFiles();
    }
}
