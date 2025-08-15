using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Windows.Input;
using tcc_demo.Models;

namespace tcc_demo.Dados
{
    public class AcCustomer
    {
        Banco con = new Banco();
        public void insertCustomer(Customer customer)
        {
            MySqlCommand cmd = new MySqlCommand("insert into tbCustomer values(default, @nameCustomer,@cpfCustomer, @emailCustomer, @passwordCustomer, @phoneCustomer)", con.ConectarBD());
            cmd.Parameters.Add("@nameCustomer", MySqlDbType.VarChar).Value = customer.nameCustomer;
            cmd.Parameters.Add("@cpfCustomer", MySqlDbType.VarChar).Value = customer.cpfCustomer;
            cmd.Parameters.Add("@emailCustomer", MySqlDbType.VarChar).Value = customer.emailCustomer;
            cmd.Parameters.Add("@passwordCustomer", MySqlDbType.VarChar).Value = customer.passwordCustomer;
            cmd.Parameters.Add("@phoneCustomer", MySqlDbType.VarChar).Value = customer.phoneCustomer;

            cmd.ExecuteNonQuery();
            con.DesconectarBD();
        }

        public List<Customer> GetCustomer()
        {
            List<Customer> listaClientes = new List<Customer>();
            MySqlCommand cmd = new MySqlCommand("select * from tbCustomer", con.ConectarBD());

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
                    new Customer
                    {
                        idCustomer = Convert.ToString(dr["idCustomer"]),
                        nameCustomer = Convert.ToString(dr["nameCustomer"]),
                        cpfCustomer = Convert.ToString(dr["cpfCustomer"]),
                        emailCustomer = Convert.ToString(dr["emailCustomer"]),
                        passwordCustomer = Convert.ToString(dr["passwordCustomer"]),
                        phoneCustomer = Convert.ToString(dr["phoneCustomer"]),

                    });
            }
            return listaClientes;
        }

        public bool DeleteCustomer(int id)
        {
            MySqlCommand cmd = new MySqlCommand("delete from tbCustomer where idCustomer=@id", con.ConectarBD());
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

        public bool UpdateCustomer(Customer customer)
        {
            MySqlCommand cmd = new MySqlCommand("update tbCustomer set nameCustomer=@nameCustomer, cpfCustomer=@cpfCustomer, emailCustomer=@emailCustomer, " +
                "passwordCustomer=@passwordCustomer, phoneCustomer=@phoneCustomer where " +
                "idCustomer=@idCustomer", con.ConectarBD());

            cmd.Parameters.AddWithValue("@nameCustomer", customer.nameCustomer);
            cmd.Parameters.AddWithValue("@cpfCustomer", customer.cpfCustomer);
            cmd.Parameters.AddWithValue("@emailCustomer", customer.emailCustomer);
            cmd.Parameters.AddWithValue("@passwordCustomer", customer.passwordCustomer);
            cmd.Parameters.AddWithValue("@phoneCustomer", customer.phoneCustomer);
            cmd.Parameters.AddWithValue("@idCustomer", customer.idCustomer);

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

         public void testarCpf(Customer customer)
        {
            MySqlCommand cmd = new MySqlCommand("select cpfCustomer from tbCustomer where " +
                "cpfCustomer=@cpfCustomer", con.ConectarBD());
                
            cmd.Parameters.AddWithValue("@cpfCustomer", customer.cpfCustomer);
            cmd.ExecuteNonQuery();
            con.DesconectarBD();
        }

        MySqlConnection cn = new MySqlConnection("Server=localhost;DataBase=teste2;User=root;pwd=12345678");

        public string SelectCPFCustomer(string  cpf)
        {
            
            cn.Open();
            MySqlCommand cmd = new MySqlCommand("call spSelectCPFCustomer(@cpfCustomer);");
            cmd.Parameters.Add("@cpfCustomer", MySqlDbType.String).Value = cpf;
            cmd.Connection = cn;
            string CPF = (string)cmd.ExecuteScalar();
            cn.Close();
            if (CPF == null)

                CPF = "";
            return CPF;


        }


        public string SelectEmailCustomer(string email)
        {
            cn.Open();
            MySqlCommand cmd = new MySqlCommand("call spSelectEmailCustomer(@emailCustomer);");
            cmd.Parameters.Add("@emailCustomer", MySqlDbType.String).Value = email;
            cmd.Connection = cn;
            string Email = (string)cmd.ExecuteScalar();
            cn.Close();
            if (Email == null)

                Email = "";
            return Email;


        }





        public void testarEmail(Customer customer)
        {
            MySqlCommand cmd = new MySqlCommand("select emailCustomer from tbCustomer where " +
                "emailCustomer=@emailCustomer", con.ConectarBD());
                
            cmd.Parameters.AddWithValue("@emailCustomer", customer.emailCustomer);
            cmd.ExecuteNonQuery();
            con.DesconectarBD();
        }


    }


}
