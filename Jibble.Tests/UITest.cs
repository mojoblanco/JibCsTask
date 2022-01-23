using Jibble.Core.Helpers;
using NUnit.Framework;
using System.Linq;

namespace Jibble.Tests
{
    public class UITest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public void Test_Valid_Menu_Option_Should_Return_Null(int optionId)
        {
            var option = AppHelper.GetSelectedMenuOption(optionId);

            Assert.NotNull(option);
        }

        [Test]
        [TestCase(10)]
        [TestCase(7)]
        public void Test_Invalid_Menu_Option_Should_Return_Null(int optionId)
        {
            var option = AppHelper.GetSelectedMenuOption(optionId);

            Assert.IsNull(option);
        }


        [Test]
        [TestCase("FirstName")]
        [TestCase("LastName")]
        [TestCase("UserName")]
        public void Test_Search_Fields_Are_Available(string field)
        {
            var fields = AppHelper.FieldSearchOptions;
            var isAvailable = fields.Any(x => x.Field == field);

            Assert.IsTrue(isAvailable);
        }
    }
}