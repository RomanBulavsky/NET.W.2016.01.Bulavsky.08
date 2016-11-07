using System;

namespace Task1
{
    /// <summary>
    /// Class that represents Customer.
    /// </summary>
    public class Customer : IFormattable
    {
        public string Name { get; }
        public string ContactPhone { get; }
        public decimal Revenue { get; }
        public Customer()
        {
            Name = "Jhon Smith";
            ContactPhone = "+1 (425) 555-0911";
            Revenue = 1000;
        }
        public Customer(string name, string contactPhone, decimal revenue)
        {
            Name = name;
            ContactPhone = contactPhone;
            Revenue = revenue;
        }
        public override string ToString()
        {
            return this.Name + " Phone:" + this.ContactPhone + "  Revenue: " + this.Revenue.ToString("C");
        }
        public string ToString(string format)
        {
            return this.Name + " Phone:" + this.ContactPhone + "  Revenue: " + this.Revenue.ToString(format);
        }
        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (format.Equals(" ")) format = null;
            return string.Format(formatProvider, string.Format(this.ToString(format ?? "C")));
        }
    }
}
