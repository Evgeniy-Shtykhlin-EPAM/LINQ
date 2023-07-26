using System;
using System.Collections.Generic;
using System.Linq;
using Task1.DoNotChange;

namespace Task1
{
    public static class LinqTask
    {
        public static IEnumerable<Customer> Linq1(IEnumerable<Customer> customers, decimal limit)
        {
            return customers.Where(c => c.Orders.Select(o => o.Total).Sum() > limit);
        }

        public static IEnumerable<(Customer customer, IEnumerable<Supplier> suppliers)> Linq2(
            IEnumerable<Customer> customers,
            IEnumerable<Supplier> suppliers
        )
        {
            throw new NotImplementedException();
        }

        public static IEnumerable<(Customer customer, IEnumerable<Supplier> suppliers)> Linq2UsingGroup(
            IEnumerable<Customer> customers,
            IEnumerable<Supplier> suppliers
        )
        {
            throw new NotImplementedException();
        }

        public static IEnumerable<Customer> Linq3(IEnumerable<Customer> customers, decimal limit)
        {
            return (from customer in customers from order in customer.Orders where order.Total > limit select customer).Distinct().ToList();
        }

        public static IEnumerable<(Customer customer, DateTime dateOfEntry)> Linq4(
            IEnumerable<Customer> customers
        )
        {
            var result = from customer in customers
                where customer.Orders.Any()
                select ( customer, customer.Orders.OrderBy(o => o.OrderDate).First().OrderDate );

            return result;
        }

        public static IEnumerable<(Customer customer, DateTime dateOfEntry)> Linq5(
            IEnumerable<Customer> customers
        )
        {
            var result = customers.Where(c => c.Orders.Any())
                .OrderBy(x => x.Orders[0].OrderDate)
                .Select(c=> (c, c.Orders.OrderBy(o => o.OrderDate).First().OrderDate ));

            return result;
        }

        public static IEnumerable<Customer> Linq6(IEnumerable<Customer> customers)
        {
            var result = customers.Where(c => !c.PostalCode.All(char.IsNumber) 
                                              || string.IsNullOrEmpty(c.Region) 
                                              || !c.Phone.Contains('(') );
            return result;
        }

        public static IEnumerable<Linq7CategoryGroup> Linq7(IEnumerable<Product> products)
        {
            /* example of Linq7result

             category - Beverages
	            UnitsInStock - 39
		            price - 18.0000
		            price - 19.0000
	            UnitsInStock - 17
		            price - 18.0000
		            price - 19.0000
             */

            throw new NotImplementedException();
        }

        public static IEnumerable<(decimal category, IEnumerable<Product> products)> Linq8(
            IEnumerable<Product> products,
            decimal cheap,
            decimal middle,
            decimal expensive
        )
        {
            throw new NotImplementedException();
        }

        public static IEnumerable<(string city, int averageIncome, int averageIntensity)> Linq9(
            IEnumerable<Customer> customers
        )
        {
            throw new NotImplementedException();
        }

        public static string Linq10(IEnumerable<Supplier> suppliers)
        {
            var res1 = (from s in suppliers
                select s.Country).Distinct();

            return string.Join("", res1);
        }
    }
}