using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFMigration
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var model = new KTStoreModel())
            {
                model.Database.Log = Console.Write;

                ShowDBSetting(model);

                //AddData(model);

                var products = from p in model.Product select p;
                foreach (var item in products)
                {
                    Console.WriteLine($"{item.Name}:{item.Price}");
                }

                //var product = model.Product.First();
                //product.Name = "ASUS ROG";
                //model.SaveChanges();
                //model.Entry(product).Reload();
                //product = model.Product.First();
                //Console.WriteLine($"{product.Name}:{product.Price}");


            }

            Console.Read();
        }

        public static void AddData(KTStoreModel model)
        {
            if (model.Product.Count() <= 0)
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
                model.Product.Add(data1);
                model.Product.Add(data2);
                model.SaveChanges();
            }
        }


        public static void ShowDBSetting(KTStoreModel model)
        {
            Console.WriteLine($@"
                連接字串:{model.Database.Connection}
                DB Name:{model.Database.Connection.Database}
                ServerName Name:{model.Database.Connection.DataSource}
                ConnectionState:{model.Database.Connection.State}
            ");
        }
    }
}
