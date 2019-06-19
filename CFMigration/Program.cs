using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CFMigration
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new KTStoreModel())
            {
                ObjectContextDemo(context);

                DbContextDemo(context);
            }

            Console.Read();
        }

        public static void OldTest(KTStoreModel context)
        {
            context.Database.Log = Console.Write;

            ShowDBSetting(context);

            //AddData(context);

            var products = from p in context.Product select p;
            foreach (var item in products)
            {
                Console.WriteLine($"{item.Name}:{item.Price}");
            }

            //var product = context.Product.First();
            //product.Name = "ASUS ROG";
            //context.SaveChanges();
            //context.Entry(product).Reload();
            //product = context.Product.First();
            //Console.WriteLine($"{product.Name}:{product.Price}");
        }

        public static void AddData(KTStoreModel context)
        {
            if (context.Product.Count() <= 0)
            {
                var data1 = new Product()
                {
                    Name = "ASUS",
                    Price = 39000M,
                    Quantity = 1
                };
                var data2 = new Product()
                {
                    Name = "APPLE MAC",
                    Price = 60000M,
                    Quantity = 1
                };
                context.Product.Add(data1);
                context.Product.Add(data2);
                context.SaveChanges();
            }
        }

        public static void ShowDBSetting(KTStoreModel context)
        {
            Console.WriteLine($@"
                連接字串:{context.Database.Connection}
                DB Name:{context.Database.Connection.Database}
                ServerName Name:{context.Database.Connection.DataSource}
                ConnectionState:{context.Database.Connection.State}
            ");
        }

        public static void ObjectContextDemo(KTStoreModel context)
        {
            ObjectContext objectContext = (context as IObjectContextAdapter).ObjectContext;
            ObjectSet<Product> products = objectContext.CreateObjectSet<Product>();
            var oq = (ObjectQuery)products.Where(p => p.Price > 1000 && p.Price < 50000);
            Console.WriteLine(oq.ToTraceString());
            foreach (Product product in oq)
            {
                Console.WriteLine(product.Name);
            }
        }

        public static void DbContextDemo(KTStoreModel context)
        {

            var oq = context.Product.Where(p => p.Price > 1000 && p.Price < 50000);
            Console.WriteLine(oq.ToString());
            foreach (Product product in oq)
            {
                Console.WriteLine(product.Name);
            }
        }


    }
}
