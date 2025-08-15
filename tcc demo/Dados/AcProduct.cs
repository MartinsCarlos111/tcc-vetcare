using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using tcc_demo.Models;

namespace tcc_demo.Dados
{
    public class AcProduct
    {
        Banco con = new Banco();

        public void insertProduct(Product product)
        {
            MySqlCommand cmd = new MySqlCommand("insert into tbProduct values(default, @idSupplier, @nameProduct, @descriptionProduct, @unitPrice, @imageProduct)", con.ConectarBD());
            cmd.Parameters.Add("@idSupplier", MySqlDbType.Int64).Value = product.idSupplier;
            cmd.Parameters.Add("@nameProduct", MySqlDbType.VarChar).Value = product.nameProduct;
            cmd.Parameters.Add("@descriptionProduct", MySqlDbType.VarChar).Value = product.descriptionProduct;
            cmd.Parameters.Add("@unitPrice", MySqlDbType.Decimal).Value = product.unitPrice;
            cmd.Parameters.Add("@imageProduct", MySqlDbType.VarChar).Value = product.image;
            cmd.ExecuteNonQuery();
            con.DesconectarBD();
        }

        public List<Product> getProduct()
        {
            List<Product> listaClientes = new List<Product>();
            MySqlCommand cmd = new MySqlCommand("select * from tbProduct", con.ConectarBD());

            //adapter para lista
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

            //tabela virtual
            DataTable db = new DataTable();
            adapter.Fill(db);

            con.DesconectarBD();

            //enquanto existir linhas(registros) no banco
            //o foreach irá adicionar os valors vindo do banco nos atributos da ModelCliente

            foreach (DataRow dr in db.Rows)
            {
                listaClientes.Add(
                    new Product
                    {
                        idSupplier = Convert.ToString(dr["idSupplier"]),
                        idProduct = Convert.ToString(dr["idProduct"]),
                        nameProduct = Convert.ToString(dr["nameProduct"]),
                        descriptionProduct = Convert.ToString(dr["descriptionProduct"]),
                        unitPrice = Convert.ToDouble(dr["unitPrice"]),
                        image = Convert.ToString(dr["imageProduct"])
                    
                    });
            }
            return listaClientes;

        }

        public bool deleteProduct(int id)
        {
            MySqlCommand cmd = new MySqlCommand("delete from tbProduct where idProduct=@id", con.ConectarBD());
            cmd.Parameters.AddWithValue("id", id);

            int i = cmd.ExecuteNonQuery();
            con.DesconectarBD();
            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool updateProduct(Product product)
        {
            MySqlCommand cmd = new MySqlCommand("update tbProduct set idSupplier=@idSupplier, nameProduct=@nameProduct, descriptionProduct=@descriptionProduct, unitPrice=@unitPrice," +
                "imageProduct=@imageProduct where " +
                "idProduct=@idProduct", con.ConectarBD());

            cmd.Parameters.Add("@idSupplier", MySqlDbType.Int64).Value = product.idSupplier;
            cmd.Parameters.Add("@idProduct", MySqlDbType.Int64).Value = product.idProduct;
            cmd.Parameters.Add("@nameProduct", MySqlDbType.VarChar).Value = product.nameProduct;
            cmd.Parameters.Add("@descriptionProduct", MySqlDbType.VarChar).Value = product.descriptionProduct;
            cmd.Parameters.Add("@unitPrice", MySqlDbType.Decimal).Value = product.unitPrice;
            cmd.Parameters.Add("@imageProduct", MySqlDbType.VarChar).Value = product.image;

            int i = cmd.ExecuteNonQuery();
            con.DesconectarBD();
            if (i >= 1)
            {
                return true;

            }
            else
            {
                return false;
            }

        }
    }
}