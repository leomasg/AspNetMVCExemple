using HelloMCV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web;
using System.Web.Mvc;

namespace HelloMCV.Controllers
{
    public class HomeController : Controller
    {
        ObjectCache cache = MemoryCache.Default;
        List<Customer> customers;

        private string retornaId()
        {
            return Guid.NewGuid().ToString();
        }            
        public HomeController()
        {
            customers = cache["customers"] as List<Customer>;
            if (customers == null)
            {
                customers = new List<Customer>();
            }
        }

        public void SaveCache()
        {
            cache["customers"] = customers;
        }
        
        public  PartialViewResult Basket()
        {
            BasketViewModel model = new BasketViewModel();
            model.BasketCount = 5;
            model.BasketTotal = "R$ 500,00";

            return PartialView(model);
        }
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            ViewBag.MySuperProprety = "This is my first app !"; 
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //public ActionResult ViewCustumer(String Name, String Telephone)
        //{
        //    Customer customer = new Customer();

        //    customer.Id = Guid.NewGuid().ToString();
        //    customer.Name = Name;
        //    customer.Telephone = Telephone;

        //    return View(customer);

        //}
        //public ActionResult ViewCustumer(Customer postedCustumer)
        //{
        //    Customer customer = new Customer();

        //    customer.Id = retornaId();
        //    customer.Name = postedCustumer.Name;
        //    customer.Telephone = postedCustumer.Telephone;

        //    return View(customer);
        //}

        public ActionResult ViewCustumer(string id)
        {
            Customer customer = customers.FirstOrDefault<Customer>(c => c.Id.Equals(id));
            if (customer==null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(customer);
            }
        }


        public ActionResult AddCustumer()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddCustumer(Customer customer)
        {   
            if (!ModelState.IsValid)
            {
                return View(customer);
            }
            customer.Id = retornaId();
            customers.Add(customer);
            SaveCache();
            return RedirectToAction("CustumerList");
        }

        public ActionResult EditCustumer(string id)
        {
            Customer customer = customers.FirstOrDefault<Customer>(c => c.Id.Equals(id));
            if (customer == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(customer);
            }
        } 

        [HttpPost]
        public ActionResult EditCustumer(Customer customer,string id)
        {
            var CustomerToEdit = customers.FirstOrDefault<Customer>(c => c.Id.Equals(id));

            if (CustomerToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(customer);
                }

                CustomerToEdit.Name = customer.Name;
                CustomerToEdit.Telephone = customer.Telephone;
                SaveCache();
                return RedirectToAction("CustumerList");
            }
        }

        public ActionResult DeleteCustoumer(string id)
        {
            Customer customer = customers.FirstOrDefault<Customer>(c => c.Id.Equals(id));
            if (customer == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(customer);
            }
        }
        [HttpPost]
        [ActionName("DeleteCustoumer")]
        public ActionResult ConfirmDeleteCustoumer(string id)
        {
            Customer customer = customers.FirstOrDefault<Customer>(c => c.Id.Equals(id));
            if (customer == null)
            {
                return HttpNotFound();
            }
            else
            {
                customers.Remove(customer);
                return RedirectToAction("CustumerList");
            }
        }


        public ActionResult CustumerList()
        {
            //List<Customer> customers = new List<Customer>();
            //customers.Add(new Customer() { Id = retornaId() , Telephone = "(21)98315-6014" , Name = "Leonardo Mercês de Almeida"});
            //customers.Add(new Customer() { Id = retornaId(), Telephone = "(21)98062-0478", Name = "Luciene Barbosa Passeri" });
            //customers.Add(new Customer() { Id = retornaId(), Telephone = "(21)00000-0000", Name = "Samuel Barbosa Passeri Mercês de Almeida" });

            return View(customers);
        }


    }
}