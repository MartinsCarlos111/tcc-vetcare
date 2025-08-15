using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tcc_demo.Dados;
using tcc_demo.Models;

namespace tcc_demo.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        AcProduct acProduct = new AcProduct();

        public void loadSupplier()
        {
            List<SelectListItem> supplier = new List<SelectListItem>();

            using (MySqlConnection con = new MySqlConnection("Server=localhost;DataBase=teste2;User=root;pwd=12345678"))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select * from tbSupplier", con);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    supplier.Add(new SelectListItem
                    {
                        Text = rdr[1].ToString(), //nome
                        Value = rdr[0].ToString() //id do autor
                    });
                }
                con.Close(); //fechando conexÃ£o
            }

            ViewBag.supplier = new SelectList(supplier, "Value", "Text");
        }

        //[Authorize(Roles = "Admin")]
        public ActionResult CadProduct()
        {
            loadSupplier();
            return View();
        }

        [HttpPost]
        public ActionResult CadProduct(Product product, HttpPostedFileBase file)
        {
            loadSupplier();
            string arquivo = Path.GetFileName(file.FileName);
            string file2 = "/Files/" + Path.GetFileName(file.FileName);
            string _path = Path.Combine(Server.MapPath("/Files"), arquivo);
            file.SaveAs(_path);
            product.image = file2;
            product.idSupplier= Request["supplier"];

            if (ModelState.IsValid)
            {
                ViewBag.msg = "Erro ao realizar cadastro do produto";
                return View(product);


            }
            else
            {
                acProduct.insertProduct(product);
                ViewBag.msg = "Cadastro efetuado com sucesso";
                return RedirectToAction("Index", "Home");

            }
          
        }

        public ActionResult ListProduct()
        {
            return View(acProduct.getProduct());
        }

        public ActionResult DeleteProduct(int id)
        {
            acProduct.deleteProduct(id);
            return RedirectToAction("ListProduct");
        }


        public ActionResult UpdateProduct(string id)
        {
            loadSupplier();
            return View(acProduct.getProduct().Find(model => model.idProduct == id));
        }

        [HttpPost]
        public ActionResult UpdateProduct(int id, Product product)
        {
            loadSupplier();
            product.idProduct = id.ToString();
            acProduct.updateProduct(product);
            return View();
        }
    }
}