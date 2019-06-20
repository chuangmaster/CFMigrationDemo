using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
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
                //ObjectContextDemo(context);

                //DbContextDemo(context);

                //DbSetFindDemo(context);

                //DbSetFindDemo(context, 1,"ASUS");

                //DbSetFindDemo(context, 5, "MSI");

                DbSetUpdateDemo(context);
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
        }

        public static void AddData(KTStoreModel context)
        {
            if (context.Product.Count() <= 5)
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
                var data3 = new Product()
                {
                    Name = "LENOVO",
                    Price = 32000M,
                    Quantity = 1
                };
                var data4 = new Product()
                {
                    Name = "HP",
                    Price = 39000M,
                    Quantity = 1
                };
                context.Product.AddRange(new List<Product>() { data1, data2, data3, data4 });
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

        public static void DbSqlQueryDemo(KTStoreModel context)
        {
            var sql = "Select * From Products Where price<=@price";
            var result = context.Product.SqlQuery(sql, new SqlParameter("price", 40000));
            foreach (var product in result)
            {
                Console.WriteLine(product.Name);
            }
        }

        public static void DbSetFindDemo(KTStoreModel context, int productIndex, string name = "")
        {
            Product product;
            product = context.Product.Find(productIndex, name);
            if (product != null)
            {
                Console.WriteLine(product.Name);
            }
        }

        public static void DbSetUpdateDemo(KTStoreModel context)
        {
            var product = context.Product.First();
            product.Price = 10000m;
            context.SaveChanges();
            context.Entry(product).Reload();
            product = context.Product.First();
            Console.WriteLine($"{product.Name}:{product.Price}");
            product.Price = 39000m;
            context.SaveChanges();
            context.Entry(product).Reload();
            product = context.Product.First();
            Console.WriteLine($"{product.Name}:{product.Price}");

        }

    }
}
