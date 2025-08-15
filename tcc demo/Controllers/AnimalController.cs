using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tcc_demo.Dados;
using tcc_demo.Models;

namespace tcc_demo.Controllers
{
    public class AnimalController : Controller

    {
        AcAnimal acAnimal = new AcAnimal();
        AcCustomer acCustomer = new AcCustomer();

        //public void loadCustomers()
        //{
        //    List<SelectListItem> clientes = new List<SelectListItem>();


        //    using (MySqlConnection con = new MySqlConnection("Server=localhost;DataBase=teste2;User=root;pwd=12345678"))
        //    {
        //        con.Open();
        //        MySqlCommand cmd = new MySqlCommand("select * from tbCustomer where idCustomer=@id", con);
                
        //        MySqlDataReader rdr = cmd.ExecuteReader();

        //        while (rdr.Read())
        //        {
        //            clientes.Add(new SelectListItem
        //            {
        //                Text = rdr[1].ToString(), //nome
        //                Value = rdr[0].ToString() //id do autor
        //            });
        //        }
        //        con.Close(); //fechando conexÃƒÂ£o
        //    }

        //    ViewBag.clientes = new SelectList(clientes, "Value", "Text");
        //}

        //[Authorize(Roles = "Customer")]
        public ActionResult CadAnimal()
        {
           // var cliente = acCustomer.ListarID(id);
            //loadCustomers();
            return View();
        }

        [HttpPost]
        public ActionResult CadAnimal(Animal animal)
        {
            //var customer = acCustomer.ListarID(id);
            //loadCustomers();
            if (!ModelState.IsValid)
            {
                ViewBag.msg = "Erro ao realizar cadastro do animal";
                return View(animal);


            }
            else
            {
                acAnimal.insertAnimal(animal);
                ViewBag.msg = "Cadastro efetuado com sucesso";

            }
            return View();
        }

        public ActionResult ListAnimal()
        {
            return View(acAnimal.getAnimal());
        }

        public ActionResult DeleteAnimal(int id)
        {
            acAnimal.deleteAnimal(id);
            return RedirectToAction("ListAnimal");
        }


        public ActionResult UpdateAnimal(string id)
        {
            return View(acAnimal.getAnimal().Find(model => model.idAnimal == id));
        }

        [HttpPost]
        public ActionResult UpdateAnimal(int id, Animal animal)
        {
            animal.idAnimal = id.ToString();
            acAnimal.atualizarAnimal(animal);
            return View();
        }



        // GET: Animal
        public ActionResult Index()
        {
            return View();
        }
    }
}