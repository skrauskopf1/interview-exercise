
using System;
using System.Collections.Generic;
using System.Linq;

namespace Interview_Exercise
{
    public class CountryCodeRepository
    {
        private readonly IFileIo _fileIo;
        public CountryCodeRepository(IFileIo fileIo)
        {
            _fileIo = fileIo;
        }

        public void Add(Country country)
        {
            if (!country.IsValid())
            {
                throw new Exception("Add failed - invalid entry");
            }

            var listCountries = _fileIo.ReadFile(country.FileName(), country);

            if (listCountries == null)
            {
                listCountries = new List<IPersist>();
            }
            else if (listCountries.Exists(x => ((Country)x).Code == country.Code))
            {
                throw new Exception("Add failed - entry exists");
            }

            listCountries.Add(country);
            listCountries = listCountries.OrderBy(x => ((Country)x).Code).ToList();
            _fileIo.WriteFile(country.FileName(), listCountries);
        }

        public void Update(Country country)
        {
            if (!country.IsValid())
            {
                throw new Exception("Update failed - invalid entry");
            }

            var listCountries = _fileIo.ReadFile(country.FileName(), country);
            if (listCountries == null || !listCountries.Exists(x => ((Country)x).Code == country.Code))
            {
                throw new Exception("Update failed - country not found");
            }

            var foundCountry = (Country)listCountries.FirstOrDefault(x => ((Country)x).Code == country.Code);
            foundCountry.Name = country.Name;
            _fileIo.WriteFile(country.FileName(), listCountries);

        }
        public void Delete(string countryCode)
        {
            var country = new Country(countryCode);
            var listCountries = _fileIo.ReadFile(country.FileName(), country);
            if (listCountries == null || !listCountries.Exists(x => ((Country)x).Code == country.Code))
            {
                throw new Exception("Delete failed - country not found");
            }

            var index = listCountries.FindIndex(x => ((Country)x).Code == countryCode);
            listCountries.RemoveAt(index);
            _fileIo.WriteFile(country.FileName(), listCountries);
        }
        public Country Get(string countryCode)
        {
            var country = new Country(countryCode);
            var listCountries = _fileIo.ReadFile(country.FileName(), country);
            if (listCountries == null || !listCountries.Exists(x => ((Country)x).Code == country.Code))
            {
                throw new Exception("Get failed - country not found");
            }

            var foundCountry = (Country)listCountries.FirstOrDefault(x => ((Country)x).Code == countryCode);
            return foundCountry;
        }

        public List<Country> GetAll()
        {
            var country = new Country();
            var listCountries = _fileIo.ReadAllFiles(country);
            if (listCountries == null)
            {
                throw new Exception("No countries found");
            }
            return listCountries.ConvertAll(x => (Country)x);
        }

        public void Clear()
        {
            _fileIo.DeleteAllFiles();
        }        
    }
}
