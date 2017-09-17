using System;
using Interview_Exercise;
using NUnit.Framework;
using Rhino.Mocks;

namespace Interview_Exercise_Tests.UnitTests
{
    public class CountryTests
    {
        public class CountryTest : TestFixtureBase
        {
            private bool _actualResult;

            protected override void Act()
            {
                _actualResult = new DoSomething().Execute();
            }

            [Test]
            public void Should_be_valid()
            {
                Country country = new Country("USA", "United States");
                Assert.That(country.IsValid(), Is.True);
            }

            [Test]
            public void Should_convert_code()
            {
                Country country = new Country("usa", "United States");
                Assert.That(country.Code.Equals("USA"), Is.True);
            }

            [Test]
            public void Should_create_delimited()
            {
                Country country = new Country("usa", "United States");
                Assert.That(country.JoinProperties().Equals("USA:United States"), Is.True);
            }

            [Test]
            public void Should_parse_delimited()
            {
                var country = new Country();
                var newCountry = (Country)country.SplitProperties("USA:United States");
                Assert.That(newCountry.Code.Equals("USA"), Is.True);
            }

            [Test]
            public void Should_be_invalid_code()
            {
                Country country = new Country("USA4", "United States");
                Assert.That(country.IsValid(), Is.False);
            }

            [Test]
            public void Should__be_invalid_entry1()
            {
                Country country = new Country("AAA", "test");
                Assert.That(country.IsValid(), Is.False);
            }

            [Test]
            public void Should__be_invalid_entry2()
            {
                Country country = new Country("QMA", "test");
                Assert.That(country.IsValid(), Is.False);
            }

            [Test]
            public void Should__be_invalid_entry3()
            {
                Country country = new Country("ZAA", "test");
                Assert.That(country.IsValid(), Is.False);
            }

            [Test]
            public void Should_be_invalid_name()
            {                
                var country = new Country() {Code = "USA"};                
                Assert.That(country.IsValid(), Is.False);
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