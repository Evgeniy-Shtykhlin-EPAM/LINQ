using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
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
            var result2 = from customer in customers
                          join supplier in suppliers on new { customer.Country , customer.City } equals new { supplier.Country, supplier.City} into temp
                          from t in temp.DefaultIfEmpty()
                          select new
                          {
                              Customer=customer,
                              Supplier = t
                          };
            var x = result2.GroupBy(r => r.Customer)
                .Select(g => (g.Key, g.Select(s => s.Supplier?? new Supplier(){Country = g.Key.Country,City = g.Key.City})));

            return x;
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
                                              || string.IsNullOrWhiteSpace(c.Region) 
                                              || !c.Phone.Contains('(') && !c.Phone.Contains(')'));
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

           var result = products.GroupBy(p => p.Category)
                .Select(g=> new Linq7CategoryGroup
                {
                    Category = g.Key,
                    UnitsInStockGroup = g.GroupBy(p=>p.UnitsInStock)
                        .Select(x=>new Linq7UnitsInStockGroup
                        {
                            UnitsInStock = x.Key,
                            Prices =x.GroupBy(p=>p.UnitPrice)
                                .Select(z=>z.Key)
                        })
                    
                });

            return result;
        }

        public static IEnumerable<(decimal category, IEnumerable<Product> products)> Linq8(
            IEnumerable<Product> products,
            decimal cheap,
            decimal middle,
            decimal expensive
        )
        {
            if (products is null)
            {
                throw new ArgumentNullException();

            }
            foreach (var product in products)
            {
                if (product.UnitPrice <= cheap)
                {
                    product.UnitPrice = cheap;
                }
                else if (product.UnitPrice > cheap && product.UnitPrice <= middle)
                {
                    product.UnitPrice = middle;
                }
                else
                {
                    product.UnitPrice = expensive;
                }
            }

            var result = products.GroupBy(p => p.UnitPrice)
                .Select(g =>
                        (
                            g.Key,
                            products = g.Select(x => x)));

            return result;
        }

        public static IEnumerable<(string city, int averageIncome, int averageIntensity)> Linq9(
            IEnumerable<Customer> customers
        )
        {
            var result = customers.GroupBy(c => c.City)
                .Select(g => 
                (
                   g.Key,
                   (int)Math.Round(g.Select(c => c.Orders.Sum(o => o.Total)).Sum(x => x) / g.Select(c => c).Count()),
                   (int)g.Select(c => c.Orders.Count()).Average(o=>o)
                ));

            return result;
        }

        public static string Linq10(IEnumerable<Supplier> suppliers)
        {
            var result = string.Join("",
                (suppliers.OrderBy(s => s.Country.Length).ThenBy(s => s.Country).Select(s => s.Country).Distinct()));

            return result;
        }
    }
}