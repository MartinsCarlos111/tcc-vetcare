using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using tcc_demo.Models;
using tcc_demo.Dados;

namespace tcc_demo.Dados
{
    public class AcAnimal
    {
            Banco con = new Banco();

            public void insertAnimal(Animal animal)
            {
            // @idCustomer,
                MySqlCommand cmd = new MySqlCommand("insert into tbPet values(default,  @nameAnimal, @breedAnimal,@ageAnimal, @genderAnimal, @speciesAnimal)", con.ConectarBD());
                //cmd.Parameters.Add("@idCustomer", MySqlDbType.VarChar).Value = animal.idCustomer;
                cmd.Parameters.Add("@nameAnimal", MySqlDbType.VarChar).Value = animal.nameAnimal;
                cmd.Parameters.Add("@breedAnimal", MySqlDbType.VarChar).Value = animal.breedAnimal;
                cmd.Parameters.Add("@speciesAnimal", MySqlDbType.VarChar).Value = animal.speciesAnimal;
                cmd.Parameters.Add("@genderAnimal", MySqlDbType.VarChar).Value = animal.genderAnimal;
                cmd.Parameters.Add("@ageAnimal", MySqlDbType.VarChar).Value = animal.ageAnimal;
                cmd.ExecuteNonQuery();
                con.DesconectarBD();
            }

            public List<Animal> getAnimal()
            {
                List<Animal> listaClientes = new List<Animal>();
                MySqlCommand cmd = new MySqlCommand("select * from tbPet", con.ConectarBD());

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
                    new Animal
                    {
                        idAnimal = Convert.ToString(dr["idAnimal"]),
                        //idCustomer = Convert.ToString(dr["idCustomer"]),
                        nameAnimal = Convert.ToString(dr["nameAnimal"]),
                        breedAnimal = Convert.ToString(dr["breedAnimal"]),
                        speciesAnimal = Convert.ToString(dr["speciesAnimal"]),
                        genderAnimal = Convert.ToString(dr["genderAnimal"]),
                        ageAnimal = Convert.ToString(dr["ageAnimal"])

                    });
                }
                return listaClientes;

            }

            public bool deleteAnimal(int id)
            {
                MySqlCommand cmd = new MySqlCommand("delete from tbPet where idAnimal=@id", con.ConectarBD());
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

            public bool atualizarAnimal(Animal animal)
                                                                              
            {
                MySqlCommand cmd = new MySqlCommand("update tbPet set nameAnimal=@nameAnimal, breedAnimal=@breedAnimal,  ageAnimal=@ageAnimal, genderAnimal=@genderAnimal," +
                    "speciesAnimal=@speciesAnimal where " +
                    "idAnimal=@idAnimal", con.ConectarBD());

                cmd.Parameters.AddWithValue("@nameAnimal", animal.nameAnimal);
                cmd.Parameters.AddWithValue("@breedAnimal", animal.breedAnimal);
                cmd.Parameters.AddWithValue("@speciesAnimal", animal.speciesAnimal);
                cmd.Parameters.AddWithValue("@genderAnimal", animal.genderAnimal);
                cmd.Parameters.AddWithValue("@ageAnimal", animal.ageAnimal);
                cmd.Parameters.AddWithValue("@idAnimal", animal.idAnimal);

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
