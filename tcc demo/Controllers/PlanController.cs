using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using tcc_demo.Dados;
using tcc_demo.Models;

namespace tcc_demo.Controllers
{
    public class PlanController : Controller
    {
        AcPlan acPlan = new AcPlan();

        //[Authorize(Roles = "Admin")]
        public ActionResult CadPlan()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CadPlan(Plan plan)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.msg = "Erro ao realizar cadastro do plano";
                return View(plan);

            }
            else
            {
                acPlan.insertPlan(plan);
                ViewBag.msg = "Cadastro efetuado com sucesso";
                return RedirectToAction("Index", "Home");

            }
        }

        public ActionResult ListPlan()
        {
            return View(acPlan.getPlan());
        }

        public ActionResult DeletePlan(int id)
        {
            acPlan.deletePlan(id);
            return RedirectToAction("ListPlan");
        }


        public ActionResult UpdatePlan(string id)
        { 
            if (id == null)
            {
                return HttpNotFound();
            }
            return View(acPlan.getPlan().Find(model => model.idPlan == id));
        }

        [HttpPost]
        public ActionResult UpdatePlan(int id, Plan plan)
        {
            if (ModelState.IsValid)
            {
                plan.idPlan = id.ToString();
                acPlan.updatePlan(plan);
                return RedirectToAction("Index", "Home");
            }
            return View(plan);
        }
    
    public ActionResult Index()
        {
            return View();
        }
    }
}