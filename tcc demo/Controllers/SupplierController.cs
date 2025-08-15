using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tcc_demo.Dados;
using tcc_demo.Models;

namespace tcc_demo.Controllers
{
    public class SupplierController : Controller
    {
        AcSupplier acSupplier = new AcSupplier();

        //[Authorize(Roles = "Admin")]
        public ActionResult CadSupplier()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CadSupplier(Supplier supplier)
        {
            if (!ModelState.IsValid)
                if (!ModelState.IsValid)
                    return View(supplier);
            string cnpj = acSupplier.SelectCnpjSuppler(supplier.cnpjSupplier);
            if (cnpj == supplier.cnpjSupplier)
            {
                ViewBag.msg = "CNPJ já existente";
                return View(supplier);
            }
            
            else
            {
                acSupplier.insertSupplier(supplier);
                ViewBag.msg = "Cadastro efetuado com sucesso";
                return RedirectToAction("Index", "Home");

            }
        }

        public ActionResult ListSupplier()
        {
            return View(acSupplier.getSupplier());
        }

        public ActionResult DeleteSupplier(int id)
        {
            acSupplier.deleteSupplier(id);
            return RedirectToAction("ListSupplier");
        }


        public ActionResult UpdateSupplier(string id)
        {
            return View(acSupplier.getSupplier().Find(model => model.idSupplier == id));
        }

        [HttpPost]
        public ActionResult UpdateSupplier(int id, Supplier supplier)
        {
            supplier.idSupplier = id.ToString();
            acSupplier.updateSupplier(supplier);
            return View();
        }
    }
}