using System.Collections.Generic;
using System.Linq;

namespace Interview_Exercise
{
    public static class Parser
    {
        public static List<IPersist> ParseCsv<T>(string csv, T t) where T : IPersist
        {
            var csvItems = csv.Split(',');
            return csvItems.Where(x => x.Length > 0).Select(t.SplitProperties).ToList();
        }

        public static string CreateCsv(List<IPersist> items)
        {
            return string.Join(",", items.Select(x => x.JoinProperties()));
        }
    }
}
