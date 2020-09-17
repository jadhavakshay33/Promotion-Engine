using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promotion_Engine
{
    class Program
    {
        public static void Main(string[] args)
        {
            
            decimal TotalPriceOfD = 0;
            decimal TotalPriceOfC = 0;
            decimal TotalPriceOfCD = 0;


            Dictionary<String, int> d1 = new Dictionary<String, int>();
            d1.Add("A", 3);
            Dictionary<String, int> d2 = new Dictionary<String, int>();
            d2.Add("B", 2);
            Dictionary<String, int> d3 = new Dictionary<String, int>();
            d3.Add("C", 1);
            d3.Add("D", 1);

            List<Promotion> promotions = new List<Promotion>()
                {
                    new Promotion(1, d1, 130),
                    new Promotion(2, d2, 45),
                     new Promotion(3, d3, 30)
                 };

          
            List<Order> orders = new List<Order>();
            Order order1 = new Order(1, new List<Product>() { new Product("A"), new Product("B"), new Product("C") });
            Order order2= new Order(2, new List<Product>() { new Product("A"), new Product("A"), new Product("A"), new Product("A"), new Product("A"), new Product("B"), new Product("B"), new Product("B"), new Product("B"), new Product("B"), new Product("C") });
            Order order3 = new Order(3, new List<Product>() { new Product("A"), new Product("A"), new Product("A"), new Product("B"), new Product("B"), new Product("B"), new Product("B"), new Product("B"), new Product("C"), new Product("D") });
            orders.AddRange(new Order[] {order1,order2,order3 });
         

            foreach (Order ord in orders)
            {
                List<decimal> promoprices = promotions
                    .Select(promo => PromotionChecker.GetTotalPrice(ord, promo))
                    .ToList();


                decimal origprice = ord.Products.Sum(x => x.Price);
                int CountOfA = ord.Products.Where(x => x.Id == "A").Count();
                int CountOfB = ord.Products.Where(x => x.Id == "B").Count();
                int CountOfC = ord.Products.Where(x => x.Id == "C").Count();
                int CountOfD = ord.Products.Where(x => x.Id == "D").Count();
                decimal TotalPriceOFA = (CountOfA / 3) * 130 + (CountOfA % 3) * 50;
                decimal TotalPriceOfB = (CountOfB / 2) * 45 + (CountOfB % 2) * 30;
                
                if(CountOfC!=0 &&CountOfD!=0)
                {
                     TotalPriceOfCD = (CountOfC / CountOfD) * 30 + (CountOfC % CountOfD) * 30;
                    TotalPriceOfC = 0;
                }
                else
                {
                    TotalPriceOfD = (CountOfD * 15);
                    TotalPriceOfC = (CountOfC * 20);
                }
               
                decimal FinalPrice = TotalPriceOFA + TotalPriceOfB + TotalPriceOfC + TotalPriceOfD + TotalPriceOfCD;

               // decimal remainingPromotions=
                decimal promoprice = promoprices.Sum();
                Console.WriteLine($"OrderID: {ord.OrderID} => Original price: {origprice.ToString("0.00")} | Rebate: {origprice-FinalPrice} | Final price: {FinalPrice}");
            }
        }


    }
}
