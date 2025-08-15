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
    public class ScheduleController : Controller
    {
        AcSchedule acSchedule = new AcSchedule();
        public void loadCustumer()
        {
            List<SelectListItem> customer = new List<SelectListItem>();

            using (MySqlConnection con = new MySqlConnection("Server=localhost;DataBase=teste2;User=root;pwd=12345678"))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select * from tbCustomer", con);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    customer.Add(new SelectListItem
                    {
                        Text = rdr[1].ToString(), //nome
                        Value = rdr[0].ToString() //id do autor
                    });
                }
                con.Close(); //fechando conexÃ£o
            }

            ViewBag.customer = new SelectList(customer, "Value", "Text");
        }
        
        public void loadAnimal()
        {
            List<SelectListItem> animal = new List<SelectListItem>();

            using (MySqlConnection con = new MySqlConnection("Server=localhost;DataBase=teste2;User=root;pwd=12345678"))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select * from tbPet", con);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    animal.Add(new SelectListItem
                    {
                        Text = rdr[1].ToString(), //nome
                        Value = rdr[0].ToString() //id do autor
                    });
                }
                con.Close(); //fechando conexÃ£o
            }

            ViewBag.animal = new SelectList(animal, "Value", "Text");
        }
        
        public void loadService()
        {
            List<SelectListItem> service = new List<SelectListItem>();

            using (MySqlConnection con = new MySqlConnection("Server=localhost;DataBase=teste2;User=root;pwd=12345678"))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select * from tbService", con);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    service.Add(new SelectListItem
                    {
                        Text = rdr[1].ToString(), //nome
                        Value = rdr[0].ToString() //id do autor
                    });
                }
                con.Close(); //fechando conexÃ£o
            }

            ViewBag.service = new SelectList(service, "Value", "Text");
        }

        //[Authorize(Roles = "Customer")]
        public ActionResult CadSchedule()
        {
            loadCustumer();
            loadAnimal();
            loadService();
            return View();
        }

        [HttpPost]
        public ActionResult CadSchedule(Schedule schedule)
        {


            loadCustumer();
            loadAnimal();
            loadService();

            schedule.idCustomer = Request["customer"];
            schedule.idService = Request["service"];
            schedule.idAnimal = Request["animal"];

          
            
                Schedule newSchedule = new Schedule()
                {
                    idCustomer = schedule.idCustomer,
                    idAnimal = schedule.idAnimal,
                    idService = schedule.idService,
                    dateSchedule = schedule.dateSchedule,
                    timeSchedule = schedule.timeSchedule,
                    observations = schedule.observations
                };

            if (ModelState.IsValid)
            {
                acSchedule.insertSchedule(newSchedule);
                return RedirectToAction("Index", "Home");
            }
            

            // Se ocorrer um erro de validação, você pode retornar a View atual com o ModelState inválido
            return View(schedule);
        }

        public ActionResult ListSchedule()
        {
            return View(acSchedule.GetSchedule());
        }

        public ActionResult DeleteSchedule(int id)
        {
            acSchedule.DeleteSchedule(id);
            return RedirectToAction("ListSchedule");
        }


        public ActionResult UpdateSchedule(string id)
        {
            loadCustumer();
            loadAnimal();
            loadService();
            return View(acSchedule.GetSchedule().Find(model => model.idSchedule == id));
        }

        [HttpPost]
        public ActionResult UpdateSchedule(int id, Schedule schedule)
        {
            schedule.idCustomer = Request["customer"];
            schedule.idService = Request["service"];
            schedule.idAnimal = Request["animal"];
            loadCustumer();
            loadAnimal();
            loadService();
            schedule.idSchedule = id.ToString();
            acSchedule.UpdateSchedule(schedule);
            return View();
        }


        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }
    }
}