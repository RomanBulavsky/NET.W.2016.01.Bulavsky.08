using System;
using System.Globalization;
using NUnit.Framework;

namespace Task1.Tests
{
    [TestFixture]
    public static class CustomerTests
    {
        [TestCase("N", "Jhon Smith")]
        [TestCase("NP", "Jhon Smith Contact Phone: +1 (425) 555-0911")]
        [TestCase("NPR", "Jhon Smith Contact Phone: +1 (425) 555-0911 Revenue: 1000")]
        [TestCase("G", "Jhon Smith Phone:+1 (425) 555-0911  Revenue: 1000")]
        [TestCase(" ", "Jhon Smith Phone:+1 (425) 555-0911  Revenue: $1,000.00")]
        [TestCase("ASD", "Jhon Smith Phone:+1 (425) 555-0911  Revenue: $1,000.00")]
        [TestCase("C", " ")]//for my CurrentCulture == en-us     
        public static void Customer_Format_CorrectFormat(string format, string expected)
        {
            if(expected.Equals(" "))
                expected = string.Format(CultureInfo.CurrentCulture,"Jhon Smith Phone:+1 (425) 555-0911  Revenue: {0:C}", 1000);

            var result = string.Format(new CustomerFormat(), "{0:" + format + "}", new Customer());
            Assert.AreEqual(result, expected);
        }

        [TestCase("")]
        public static void Customer_Format_CorrectFormat(string format)
        {
            Assert.That(() => string.Format(new CustomerFormat(), "{0:" + format + "}", new Customer()), Throws.TypeOf<FormatException>());
        }
    }
}
