using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using tcc_demo.Models;

namespace tcc_demo.Dados
{
    public class AcPlan
    {
        Banco con = new Banco();

        public void insertPlan(Plan plan)
        {
            MySqlCommand cmd = new MySqlCommand("insert into tbPlan values(default, @namePlan, @descriptionPlan, @pricePlan)", con.ConectarBD());
            cmd.Parameters.Add("@namePlan", MySqlDbType.VarChar).Value = plan.namePlan;
            cmd.Parameters.Add("@descriptionPlan", MySqlDbType.VarChar).Value = plan.descriptionPlan;
            cmd.Parameters.Add("@pricePlan", MySqlDbType.Double).Value = plan.pricePlan;
            cmd.ExecuteNonQuery();
            con.DesconectarBD();
        }

        public List<Plan> getPlan()
        {
            List<Plan> listaClientes = new List<Plan>();
            MySqlCommand cmd = new MySqlCommand("select * from tbPlan", con.ConectarBD());

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
                    new Plan
                    {
                        idPlan = Convert.ToString(dr["idPlan"]),
                        namePlan = Convert.ToString(dr["namePlan"]),
                        descriptionPlan = Convert.ToString(dr["descriptionPlan"]),
                        pricePlan = Convert.ToDouble(dr["pricePlan"]),
                     
                    });
            }
            return listaClientes;

        }

        public bool deletePlan(int id)
        {
            MySqlCommand cmd = new MySqlCommand("delete from tbPlan where idPlan=@id", con.ConectarBD());
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

        public bool updatePlan(Plan plan)
        {
            MySqlCommand cmd = new MySqlCommand("update tbPlan set namePlan=@namePlan, descriptionPlan=@descriptionPlan, pricePlan=@pricePlan " +
                " where " +
                "idPlan=@idPlan", con.ConectarBD());

            cmd.Parameters.Add("@idPlan", MySqlDbType.VarChar).Value = plan.idPlan;
            cmd.Parameters.Add("@namePlan", MySqlDbType.VarChar).Value = plan.namePlan;
            cmd.Parameters.Add("@descriptionPlan", MySqlDbType.VarChar).Value = plan.descriptionPlan;
            cmd.Parameters.Add("@pricePlan", MySqlDbType.Double).Value = plan.pricePlan;

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