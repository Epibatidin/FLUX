using DataAccess.FileSystem;
using DataAccess.Interfaces;
using Extension.Test;
using FLUX.DomainObjects;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Tests.FileSystem
{
    public class PatternProviderTests : FixtureBase<PatternProvider>
    {
        protected override PatternProvider CreateSUT()
        {
            return new PatternProvider();
        }

        [Test]
        public void should_return_collection_with_replaced_values()
        {
            var values = new List<Tuple<string, string>>();
            values.Add(Tuple.Create("Foo", "Bar"));

            var result = SUT.FormatInternal(values, new[] { "{Foo}" });

            Assert.That(result[0], Is.EqualTo("Bar"));
        }

        [Test]
        public void should_replace_values_that_does_not_exists_with_empty()
        {
            var values = new List<Tuple<string, string>>();
            values.Add(Tuple.Create("F2oo", "Bar"));

            var result = SUT.FormatInternal(values, new[] { "{Foo}" });

            Assert.That(result[0], Is.Empty);
        }

        [Test]
        public void should_replace_values_that_exists_but_are_null_with_not_adding_to_result()
        {
            var values = new List<Tuple<string, string>>();
            values.Add(Tuple.Create<string,string>("Foo", null));

            var result = SUT.FormatInternal(values, new[] { "{Foo}" });

            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void should_replace_values_that_exists_but_are_null_with_not_adding_to_result_but_only_if_all_values_are_null()
        {
            var values = new List<Tuple<string, string>>();
            values.Add(Tuple.Create<string, string>("Foo", null));
            values.Add(Tuple.Create<string, string>("Bar", "Blubber"));

            var result = SUT.FormatInternal(values, new[] { "{Foo} - {Bar}" });

            Assert.That(result[0], Is.EqualTo(" - Blubber"));
        }
    }
}
