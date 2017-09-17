using System;
using System.Collections.Generic;
using System.IO;
using Interview_Exercise;
using NUnit.Framework;
using Rhino.Mocks;

namespace Interview_Exercise_Tests.UnitTests
{
    public class CountryCodeRepositoryTests
    {
        public class CountryCodeRepositoryTest : TestFixtureBase
        {
            private static IFileIo _suppliedFileIo;
                       
            protected override void Arrange()
            {
                _suppliedFileIo = Mock<IFileIo>();
                _suppliedFileIo.Expect(x => x.ReadAllFiles(new Country())).Return(null);
            }
                       
            protected override void Act()
            {
                var testSubject = new CountryCodeRepository(_suppliedFileIo);
                //var list = testSubject.GetAll();
                //testSubject.Get("BAL");
                //var country = new Country("TST", "Uraguay");
                //testSubject.Add(country);
            }

            [Test]
            public void Should_have_used_ReadAllFiles()
            {
                var repo = new CountryCodeRepository(_suppliedFileIo);
                //var countries = repo.GetAll();
                //_suppliedFileIo.AssertWasCalled(x => x.ReadAllFiles(new Country()), y=> y.Repeat.Once());
            }

            /*
            [Test]
            public void Should_have_used_WriteFile1()
            {
                var repo = new CountryCodeRepository(_suppliedFileIo);
                repo.Add(new Country("LIT", "Lithuania"));
                //_suppliedFileIo.AssertWasCalled(x => x.WriteFile("L.csv", new List<IPersist>()), y => y.Repeat.Once());
            }

            [Test]
            public void Should_have_used_WriteFile2()
            {
                var repo = new CountryCodeRepository(_suppliedFileIo);
                repo.Update(new Country("LIT", "Lithuania"));
                //_suppliedFileIo.AssertWasCalled(x => x.WriteFile("L.csv", new List<IPersist>()), y => y.Repeat.Once());
            }

            [Test]
            public void Should_have_used_ReadFile()
            {
                var repo = new CountryCodeRepository(_suppliedFileIo);
                var country = repo.Get("AAA");
                //_suppliedFileIo.AssertWasCalled(x => x.ReadFile("A.csv", country), y => y.Repeat.Once());
            }

            [Test]
            public void Should_have_used_DeleteAllFiles()
            {
                var repo = new CountryCodeRepository(_suppliedFileIo);
                repo.Clear();
                //_suppliedFileIo.AssertWasCalled(x => x.DeleteAllFiles(), y=> y.Repeat.Once());
            }
             */
           
        }

        public class CountryCodeRepositoryTestException : TestFixtureBase
        {
            protected override void Act()
            {
                throw new Exception("exception was thrown");
            }

            [Test]
            public void Should_set_actual_exception()
            {
                Assert.That(ActualException, Is.Not.Null);
            }

            [Test]
            public void Should_have_expected_message()
            {
                AssertAll(
                    () => Assert.That(ActualException.Message, Is.Not.Null),
                    () => Assert.That(ActualException.Message, Is.EqualTo("exception was thrown")));
            }
        }


        private class TestFileIo : IFileIo
        {
            private static readonly string DirectoryPath =
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
                    "Interview-Exercise-Test"); 
  
            public List<IPersist> ReadFile<T>(string fileName, T t) where T : IPersist
            {
                return new List<IPersist>
                {
                    new Country("BAL", "Bali")
                };
            }

            public List<IPersist> ReadAllFiles<T>(T t) where T : IPersist
            {
                return new List<IPersist>
                {
                    new Country("URA", "Uraguay"),
                    new Country("USA", "United States")
                };
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
            }
        }
    }
}