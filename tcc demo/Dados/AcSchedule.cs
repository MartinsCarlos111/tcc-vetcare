using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using tcc_demo.Models;
using static tcc_demo.Dados.AcSchedule;

namespace tcc_demo.Dados
{
    public class AcSchedule
    {
       
            Banco con = new Banco();
            public void insertSchedule(Schedule schedule)
            {
                MySqlCommand cmd = new MySqlCommand("insert into tbSchedule values(default, @idCustomer,@idAnimal, @idService, @dateSchedule, @timeSchedule, @observations)", con.ConectarBD());
                cmd.Parameters.Add("@idCustomer", MySqlDbType.VarChar).Value = schedule.idCustomer;
                cmd.Parameters.Add("@idAnimal", MySqlDbType.VarChar).Value = schedule.idAnimal;
                cmd.Parameters.Add("@idService", MySqlDbType.VarChar).Value = schedule.idService;
                cmd.Parameters.Add("@dateSchedule", MySqlDbType.VarChar).Value = schedule.dateSchedule;
                cmd.Parameters.Add("@timeSchedule", MySqlDbType.VarChar).Value = schedule.timeSchedule;
                cmd.Parameters.Add("@observations", MySqlDbType.VarChar).Value = schedule.observations;

                cmd.ExecuteNonQuery();
                con.DesconectarBD();
            }

            public List<Schedule> GetSchedule()
            {
                List<Schedule> listaSchedule = new List<Schedule>();
                MySqlCommand cmd = new MySqlCommand("select * from tbSchedule", con.ConectarBD());

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
                    listaSchedule.Add(
                        new Schedule
                        {
                            idSchedule = Convert.ToString(dr["idSchedule"]),
                            idCustomer = Convert.ToString(dr["idCustomer"]),
                            idAnimal = Convert.ToString(dr["idAnimal"]),
                            idService = Convert.ToString(dr["idService"]),
                            dateSchedule = Convert.ToString(dr["dateSchedule"]),
                            timeSchedule = Convert.ToString(dr["timeSchedule"]),
                            observations = Convert.ToString(dr["observations"]),

                        });
                }
                return listaSchedule;
            }

            public bool DeleteSchedule(int id)
            {
                MySqlCommand cmd = new MySqlCommand("delete from tbSchedule where idSchedule=@id", con.ConectarBD());
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

            public bool UpdateSchedule(Schedule schedule)
            {
                MySqlCommand cmd = new MySqlCommand("update tbSchedule set idCustomer=@idCustomer, idAnimal=@idAnimal, idService=@idService, " +
                    "dateSchedule=@dateSchedule, timeSchedule=@timeSchedule, observations=@observations where " +
                    "idSchedule=@idSchedule", con.ConectarBD());

                cmd.Parameters.AddWithValue("@idCustomer", schedule.idCustomer);
                cmd.Parameters.AddWithValue("@idAnimal", schedule.idAnimal);
                cmd.Parameters.AddWithValue("@idService", schedule.idService);
                cmd.Parameters.AddWithValue("@idSchedule", schedule.idSchedule);
                cmd.Parameters.AddWithValue("@dateSchedule", schedule.dateSchedule);
                cmd.Parameters.AddWithValue("@timeSchedule", schedule.timeSchedule);
                cmd.Parameters.AddWithValue("@observations", schedule.observations);

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
