using System;
using System.Collections.Generic;
using Interview_Exercise;
using NUnit.Framework;
using Rhino.Mocks;

namespace Interview_Exercise_Tests.UnitTests
{
    public class ParserTests
    {
        public class ParserTest : TestFixtureBase
        {
            private bool _actualResult;

            protected override void Act()
            {
                _actualResult = new DoSomething().Execute();
            }

            [Test]
            public void Should_create_csv()
            {
                var listCountries = new List<IPersist>
                {
                    new Country("URA", "Uraguay"),
                    new Country("USA", "United States")
                };                
                var csv = Parser.CreateCsv(listCountries);
                Assert.That(csv.Equals("URA:Uraguay,USA:United States"), Is.True);
            }

            [Test]
            public void Should_create_Country()
            {                                
                var listCountries = Parser.ParseCsv("URA:Uraguay,USA:United States", new Country());
                Assert.That(listCountries.Count.Equals(2), Is.True);
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