using System.Text.RegularExpressions;

namespace Interview_Exercise
{
    public class Country : IPersist, IValidate
    {
        private string _code { get; set; }
        public string Code
        {
            get { return _code; }
            set
            {
                _code = value.Trim().ToUpper();
            }
        }

        public string Name { get; set; }

        public Country()
        {
        }

        public Country(string code, string name = "")
        {
            Code = code;
            Name = name;
        }

        public string FileName()
        {
            return Code[0] + ".csv";
        }

        public IPersist SplitProperties(string delimitedString)
        {
            var elements = delimitedString.Split(':');
            Code = elements[0];
            Name = elements[1];
            return new Country(Code, Name);
        }

        public string JoinProperties()
        {
            return Code + ":" + Name;
        }

        public bool IsValid()
        {
            if (Code == null || Code.Length != 3 ||
                Name == null || Name.Length == 0)
            {
                return false;
            }

            if (Regex.IsMatch(Code, "A[A-Z][A-Z]") ||
                Regex.IsMatch(Code, "Q[M-Z][A-Z]") ||
                Regex.IsMatch(Code, "Z[A-Z][A-Z]"))
            {
                return false;
            }

            return true;
        }
    }
}