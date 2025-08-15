using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using tcc_demo.Dados;
using tcc_demo.Models;

namespace tcc_demo.Controllers
{
    public class CustomerController : Controller
    {


        AcCustomer acCustomer = new AcCustomer();

        //[Authorize(Roles = "Customer")]
        public ActionResult CadCustomer()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CadCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
                return View(customer);
            string  cpf = acCustomer.SelectCPFCustomer(customer.cpfCustomer);
            string email = acCustomer.SelectEmailCustomer(customer.emailCustomer);
            if (cpf == customer.cpfCustomer && email == customer.emailCustomer)
            {
                ViewBag.Email = "Email ja existente";
                ViewBag.CPF = "CPF já existente";
                return View(customer);
            }
            else if (cpf == customer.cpfCustomer)
            {
                ViewBag.CPF = "CPF já existente";
                return View(customer);
            }

            else if (email == customer.emailCustomer)
            {
                ViewBag.Email = "Email já existente";
                return View(customer);
            }

            Customer newCustomer = new Customer()
            {
                nameCustomer = customer.nameCustomer,
                cpfCustomer = customer.cpfCustomer,
                emailCustomer = customer.emailCustomer,
                passwordCustomer = customer.passwordCustomer,
                phoneCustomer = customer.phoneCustomer


            };
            acCustomer.insertCustomer(newCustomer);
            return RedirectToAction("Index", "Home");

        }

        public ActionResult ListCustomer()
        {
            return View(acCustomer.GetCustomer());
        }

        public ActionResult DeleteCustomer(int id)
        {
            acCustomer.DeleteCustomer(id);
            return RedirectToAction("ListCustomer");
        }


        public ActionResult UpdateCustomer(string id)
        {
            return View(acCustomer.GetCustomer().Find(model => model.idCustomer == id));
        }

        [HttpPost]
        public ActionResult UpdateCustomer(int id, Customer customer)
        {
            customer.idCustomer = id.ToString();
            acCustomer.UpdateCustomer(customer);
            return View();
        }


        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }
    }
}