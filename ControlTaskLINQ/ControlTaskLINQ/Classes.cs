using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlTaskLINQ
{
    public static class Helpers
    {
        public static T Create<T>(string str)
            where T:new()
        {
            var result = new T();

            var data = str.Split(' ');
            var properties = typeof(T).GetProperties();
            for(int i=0; i<properties.Length; i++)
            {
                properties[i].SetValue(result,Convert.ChangeType(data[i], properties[i].PropertyType),null);
            }
            
            return result;
        }

        public static IEnumerable<T> ReadItems<T>(string filename)
            where T : new()
        {
            return File
                .ReadAllLines(filename)
                .Select(Create<T>).ToList();
        }
    }
    public class Customer
    {
        public int Id { get; set; }
        public int Birth { get; set; }
        public string Street { get; set; }
    }

    public class Item
    {
        public string Article { get; set; }
        public string Category { get; set; }
        public string Country { get; set; }
    }

    public class Discount
    {
        public int CustomerId { get; set; }
        public string Shop { get; set; }
        public int DiscountPercent { get; set; }
    }

    public class Cost
    {
        public string ItemArticle { get; set; }
        public string ShopName { get; set; }
        public double Price { get; set; }
    }

    public class Order
    {
        public int CustomerId { get; set; }
        public string ShopName { get; set; }
        public string ItemArticle { get; set; }
    }
}
