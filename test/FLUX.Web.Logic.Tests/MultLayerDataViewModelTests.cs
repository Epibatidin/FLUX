using FLUX.DomainObjects;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace FLUX.Web.Logic.Tests
{
    [TestFixture]
    public class MultLayerDataViewModelTests 
    {
        public MultiLayerDataViewModel SUT { get; private set; }

        [SetUp]
        public void Setup()
        {
            SUT = new MultiLayerDataViewModel(null, null, 0, null);
        }

        [Test]
        public void should_not_throw_for_null()
        {
            Assert.DoesNotThrow(() => SUT.BuildContainer(null));
        }

        [Test]
        public void should_support_empty_and_null_values()
        {
            var values = new List<string>();
            values.Add("");
            values.Add(null);

            Assert.DoesNotThrow(() => SUT.BuildContainer(values));
        }

        [Test]
        public void should_add_dummy_element_if_all_Results_are_invalid()
        {
            var values = new List<string>();
            values.Add("");
            values.Add(null);

            var result = SUT.BuildContainer(values).First();

            Assert.That(result.Value, Is.Null);
        }

        [Test]
        public void should_only_add_dummy_element_when_no_Results()
        {
            var values = new List<string>();
            values.Add("value");
            values.Add("");
            values.Add(null);

            var result = SUT.BuildContainer(values).First();

            Assert.That(result.Value, Is.Not.Empty);
        }

        [Test]
        public void should_return_one_value()
        {
            var values = new List<string>();
            values.Add("value");

            var result = SUT.BuildContainer(values).First();

            Assert.That(result.Value, Is.EqualTo("value"));
            Assert.That(result.ColorCodeActiveFlags[0], Is.True);
        }


        [Test]
        public void should_have_layer_value_for_each_unique_value()
        {
            var values = new List<string>();
            values.Add("baz");
            values.Add("foo");

            var result = SUT.BuildContainer(values).ToList();

            Assert.That(result[0].Value, Is.EqualTo("baz"));
            Assert.That(result[1].Value, Is.EqualTo("foo"));
        }

        [Test]
        // sounds super complex but means :
        // there is a colorflag for every value and its active if the value matches
        public void should_have_color_count_flags_with_value_index_correlation()
        {
            var values = new List<string>();
            values.Add("baz");
            values.Add("foo");

            var result = SUT.BuildContainer(values).ToList();

            Assert.That(result[0].ColorCodeActiveFlags[0], Is.True);
            Assert.That(result[0].ColorCodeActiveFlags[1], Is.False);

            Assert.That(result[1].ColorCodeActiveFlags[0], Is.False);
            Assert.That(result[1].ColorCodeActiveFlags[1], Is.True);
        }

        [Test]
        public void should_have_all_colors_marked_if_value_equals()
        {
            var values = new List<string>();
            values.Add("value");
            values.Add("value");

            var result = SUT.BuildContainer(values).Single();

            Assert.That(result.ColorCodeActiveFlags[0], Is.True);
            Assert.That(result.ColorCodeActiveFlags[1], Is.True);
        }

        [Test]
        public void should_compare_with_ignore_case()
        {
            var values = new List<string>();
            values.Add("vAlUe");
            values.Add("VaLuE");

            var result = SUT.BuildContainer(values).Single();

            Assert.That(result.ColorCodeActiveFlags[0], Is.True);
            Assert.That(result.ColorCodeActiveFlags[1], Is.True);
        }

        [Test]
        public void should_drop_duplicated_values_and_mark_duplicate_as_color()
        {
            var values = new List<string>();
            values.Add("baz");
            values.Add("foo");
            values.Add("baz");
            values.Add("foo");

            var result = SUT.BuildContainer(values).ToList();

            Assert.That(result[0].Value, Is.EqualTo("baz"));
            Assert.That(result[1].Value, Is.EqualTo("foo"));

            CollectionAssert.AreEqual(result[0].ColorCodeActiveFlags, new[] { true, false, true, false });
            CollectionAssert.AreEqual(result[1].ColorCodeActiveFlags, new[] { false, true, false, true });
        }

        [Test]
        public void should_order_by_count()
        {
            var values = new List<string>();
            values.Add("foo");
            values.Add("baz");
            values.Add("baz");

            var result = SUT.BuildContainer(values).ToList();

            Assert.That(result[0].Value, Is.EqualTo("baz"));
            Assert.That(result[1].Value, Is.EqualTo("foo"));

            CollectionAssert.AreEqual(result[0].ColorCodeActiveFlags, new[] { false, true, true });
            CollectionAssert.AreEqual(result[1].ColorCodeActiveFlags, new[] { true, false, false });
        }

    }
}