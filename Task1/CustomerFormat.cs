using System;
using System.Globalization;

namespace Task1
{
    /// <summary>
    /// Class that provides additional features for formatting Customer.
    /// </summary>
    public class CustomerFormat : IFormatProvider, ICustomFormatter
    {
        public object GetFormat(Type formatType)=> formatType == typeof(ICustomFormatter) ? this : null;
        /// <summary>
        /// Provide Format Method for Customer type arguments.
        /// </summary>
        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            if (ReferenceEquals(format,null))
                throw new FormatException($"The format of '{format}' is invalid.");

            // Provide default formatting if arg is not an Customer.
            if (arg.GetType() != typeof(Customer))
                try
                {
                    return HandleOtherFormats(format, arg);
                }
                catch (FormatException e)
                {
                    throw new FormatException($"The format of '{format}' is invalid.", e);
                }
            

            // Provide default formatting for unsupported format strings.
            var ufmt = format.ToUpper(CultureInfo.InvariantCulture);

            if (!(ufmt == "N" || ufmt == "P" || ufmt == "R" || ufmt == "NP" || ufmt == "NR" || ufmt == "PR" || ufmt == "NPR"))
                try
                {
                    return HandleOtherFormats(format, arg);
                }
                catch (FormatException e)
                {
                    throw new FormatException($"The format of '{format}' is invalid.", e);
                } 
            var customer = arg as Customer;
            if (ReferenceEquals(customer, null))
                throw new ArgumentNullException();

            switch (ufmt)
            {
                case "N":
                    return customer.Name;
                case "P":
                    return customer.ContactPhone;
                case "R":
                    return customer.Revenue.ToString("G");
                case "NP":
                    return customer.Name + " Contact Phone: " + customer.ContactPhone;
                case "NR":
                    return customer.Name + " Revenue: " + customer.Revenue.ToString("G");
                case "PR":
                    return " Contact Phone: " + customer.ContactPhone + " Revenue: " + customer.Revenue.ToString("G");
                case "NPR":
                    return customer.Name + " Contact Phone: " + customer.ContactPhone + " Revenue: " + customer.Revenue.ToString("G");

                default:
                    return customer.ToString();
            }
        }
        /// <summary>
        /// Handles formats not supported by CustomerFormat.
        /// </summary>
        private string HandleOtherFormats(string format, object arg)
        {
            if (arg is IFormattable)
                return ((IFormattable) arg).ToString(format.Length > 1 ? "C" : format, CultureInfo.CurrentCulture); 
            else if (arg != null)
                return arg.ToString();
            else
                return string.Empty;
        }
    }
}
