using System;
using System.Collections.Generic;
using Interview_Exercise;
using NUnit.Framework;
using Rhino.Mocks;

namespace Interview_Exercise_Tests.UnitTests
{
    public class FileIoTests
    {
        public class FileIoTest : TestFixtureBase
        {
            private bool _actualResult;

            protected override void Act()
            {
                _actualResult = new DoSomething().Execute();
            }

            [Test]
            public void Should_write_and_read_file()
            {                
                var fileIo = new FileIo();

                var listCountries = new List<IPersist>
                {
                    new Country("URA", "Uraguay"),
                    new Country("USA", "United States")
                };
                fileIo.WriteFile(((Country)listCountries[0]).FileName(), listCountries);

                var savedList = fileIo.ReadFile("U.csv", new Country());
                Assert.That(savedList.Count.Equals(2), Is.True);
            }

            [Test]
            public void Should_read_all_file()
            {              
                var fileIo = new FileIo();

                var listCountries1 = new List<IPersist>
                {
                    new Country("URA", "Uraguay"),
                    new Country("USA", "United States")
                };
                fileIo.WriteFile(((Country)listCountries1[0]).FileName(), listCountries1);

                var listCountries2 = new List<IPersist>
                {
                    new Country("ENG", "England")
                };
                fileIo.WriteFile(((Country)listCountries2[0]).FileName(), listCountries2);

                var savedList = fileIo.ReadAllFiles(new Country());
                Assert.That(savedList.Count.Equals(3), Is.True);
            }

            [Test]
            public void Should_delete_all_files()
            {
                var fileIo = new FileIo();                

                var listCountries1 = new List<IPersist>
                {
                    new Country("URA", "Uraguay"),
                    new Country("USA", "United States")
                };
                fileIo.WriteFile(((Country)listCountries1[0]).FileName(), listCountries1);

                fileIo.DeleteAllFiles();

                var savedList = fileIo.ReadAllFiles(new Country());
                Assert.That(savedList, Is.Empty);
            }
           
        }        

        public interface IDependency
        {
            bool Execute();
        }

        private class DoSomething
        {
            public bool Execute()
            {
                return true;
            }
        }        
    }
}