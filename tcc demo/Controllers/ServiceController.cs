using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tcc_demo.Dados;
using tcc_demo.Models;

namespace tcc_demo.Controllers
{
    public class ServiceController : Controller
    {
        AcService acService = new AcService();

        //[Authorize(Roles = "Admin")]
        public ActionResult CadService()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CadService(Service service)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.msg = "Erro ao realizar cadastro do serviço";
                return View(service);


            }
            else
            {
                acService.insertService(service);
                ViewBag.msg = "Cadastro efetuado com sucesso";
                return RedirectToAction("Index", "Home");

            }
           
        }

        public ActionResult ListService()
        {
            return View(acService.getService());
        }

        public ActionResult DeleteService(int id)
        {
            acService.deleteService(id);
            return RedirectToAction("ListService");
        }


        public ActionResult UpdateService(string id)
        {
            return View(acService.getService().Find(model => model.idService == id));
        }

        [HttpPost]
        public ActionResult UpdateService(int id, Service service)
        {
            service.idService = id.ToString();
            acService.updateService(service);
            return View();
        }
    }
}