using System.Data.SqlClient;
using System.Data;
using API_DataBase.Models;
using API_DataBase.DBConnection.DBConfig;
using System.Collections.Generic;

namespace API_DataBase.DBConnection
{
    public class Dao : DataBase
    {
        public Dao() { }

        internal IEnumerable<Cliente> SPS_CLIENTE()
        {
            DataTable dt = new DataTable("table");            
            List<Cliente> listaClientes = new List<Cliente>();
            SqlCommand cmd = new SqlCommand();

            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString("ClienteConnection")))
                {
                    cmd.Parameters.Clear();
                    cmd = new SqlCommand("SPS_CLIENTE", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = cmd;
                    adapter.SelectCommand.CommandTimeout = 0;
                    adapter.Fill(dt);

                }

                foreach (DataRow row in dt.Rows)
                {

                    Cliente cliente = new Cliente
                    {
                        ID = int.Parse(row["CD_CLIENTE"].ToString()),
                        Nome = row["NM_CLIENTE"].ToString(),
                        CPF = long.Parse(row["NR_CPF"].ToString()),
                        Sexo = row["DC_SEXO"].ToString(),
                        Nascimento = DateTime.Parse(row["DT_NASCIMENTO"].ToString()),
                        ID_Situacao= int.Parse(row["CD_SITUACAO_CLIENTE"].ToString()),
                        Situacao = row["DC_SITUACAO"].ToString(),
                        Alteracao = DateTime.Parse(row["DT_ALTERACAO"].ToString())
                    };

                    listaClientes.Add(cliente);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return listaClientes;
        }
        internal IEnumerable<Cliente> SPS_CLIENTE(int ID)
        {
            DataTable dt = new DataTable("table");           
            List<Cliente> listaClientes = new List<Cliente>();
            SqlCommand cmd = new SqlCommand();

            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString("ClienteConnection")))
                {
                    cmd.Parameters.Clear();
                    cmd = new SqlCommand("SPS_CLIENTE", conn);
                    cmd.Parameters.AddWithValue("@CD_CLIENTE", ID);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = cmd;
                    adapter.SelectCommand.CommandTimeout = 0;
                    adapter.Fill(dt);

                }

                foreach (DataRow row in dt.Rows)
                {

                    Cliente cliente = new Cliente
                    {
                        ID = int.Parse(row["CD_CLIENTE"].ToString()),
                        Nome = row["NM_CLIENTE"].ToString(),
                        CPF = long.Parse(row["NR_CPF"].ToString()),
                        Sexo = row["DC_SEXO"].ToString(),
                        Nascimento = DateTime.Parse(row["DT_NASCIMENTO"].ToString()),
                        ID_Situacao = int.Parse(row["CD_SITUACAO_CLIENTE"].ToString()),
                        Situacao = row["DC_SITUACAO"].ToString(),
                        Alteracao = DateTime.Parse(row["DT_ALTERACAO"].ToString())
                    };

                    listaClientes.Add(cliente);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return listaClientes;
        }
        internal void SPI_CLIENTE(Cliente cliente)
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString("ClienteConnection")))
                {
                    cmd.Parameters.Clear();
                    cmd = new SqlCommand("SPI_CLIENTE", conn);                    

                    cmd.Parameters.AddWithValue("@NM_CLIENTE", cliente.Nome);
                    cmd.Parameters.AddWithValue("@NR_CPF", cliente.CPF);
                    cmd.Parameters.AddWithValue("@DC_SEXO", cliente.Sexo);
                    cmd.Parameters.AddWithValue("@DT_NASCIMENTO", cliente.Nascimento);
                    cmd.Parameters.AddWithValue("@CD_SITUACAO_CLIENTE", cliente.ID_Situacao);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = conn;
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        internal void SPU_CLIENTE(Cliente cliente)
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString("ClienteConnection")))
                {
                    cmd.Parameters.Clear();
                    cmd = new SqlCommand("SPU_CLIENTE", conn);
                  
                    cmd.Parameters.AddWithValue("@CD_CLIENTE", cliente.ID);
                    cmd.Parameters.AddWithValue("@NM_CLIENTE", cliente.Nome);
                    cmd.Parameters.AddWithValue("@NR_CPF", cliente.CPF);
                    cmd.Parameters.AddWithValue("@DC_SEXO", cliente.Sexo);
                    cmd.Parameters.AddWithValue("@DT_NASCIMENTO", cliente.Nascimento);
                    cmd.Parameters.AddWithValue("@CD_SITUACAO_CLIENTE", cliente.ID_Situacao);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = conn;
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        internal void SPD_CLIENTE(int ID)
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString("ClienteConnection")))
                {
                    cmd.Parameters.Clear();
                    cmd = new SqlCommand("SPD_CLIENTE", conn);
                  
                    cmd.Parameters.AddWithValue("@CD_CLIENTE", ID);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = conn;
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();

                    cmd.Connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        internal IEnumerable<Situacao> SPS_SITUACAO()
        {
            DataTable dt = new DataTable("table");
            List<Situacao> listaSituacao = new List<Situacao>();
            SqlCommand cmd = new SqlCommand();

            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString("ClienteConnection")))
                {
                    cmd.Parameters.Clear();
                    cmd = new SqlCommand("SPS_SITUACAO", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = cmd;
                    adapter.SelectCommand.CommandTimeout = 0;
                    adapter.Fill(dt);

                }

                foreach (DataRow row in dt.Rows)
                {

                    Situacao situacao = new Situacao
                    {
                        CD_SITUACAO = int.Parse(row["CD_SITUACAO_CLIENTE"].ToString()),
                        DC_SITUACAO = row["DC_SITUACAO"].ToString(),
                        DT_ALTERACAO = DateTime.Parse(row["DT_ALTERACAO"].ToString())
                    };

                    listaSituacao.Add(situacao);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return listaSituacao;
        }
        internal IEnumerable<Situacao> SPS_SITUACAO(int ID)
        {
            DataTable dt = new DataTable("table");
            List<Situacao> listaSituacao = new List<Situacao>();
            SqlCommand cmd = new SqlCommand();

            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString("ClienteConnection")))
                {
                    cmd.Parameters.Clear();
                    cmd = new SqlCommand("SPS_SITUACAO", conn);
                    cmd.Parameters.AddWithValue("@CD_SITUACAO_CLIENTE", ID);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = cmd;
                    adapter.SelectCommand.CommandTimeout = 0;
                    adapter.Fill(dt);

                }

                foreach (DataRow row in dt.Rows)
                {

                    Situacao situacao = new Situacao
                    {
                        CD_SITUACAO = int.Parse(row["CD_SITUACAO_CLIENTE"].ToString()),
                        DC_SITUACAO = row["DC_SITUACAO"].ToString(),
                        DT_ALTERACAO = DateTime.Parse(row["DT_ALTERACAO"].ToString())
                    };

                    listaSituacao.Add(situacao);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return listaSituacao;
        }
        internal void SPI_SITUACAO(Situacao situacao)
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString("ClienteConnection")))
                {
                    cmd.Parameters.Clear();
                    cmd = new SqlCommand("SPI_SITUACAO", conn);

                    cmd.Parameters.AddWithValue("@DC_SITUACAO", situacao.DC_SITUACAO);
                    
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = conn;
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        internal void SPU_SITUACAO(Situacao situacao)
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString("ClienteConnection")))
                {
                    cmd.Parameters.Clear();
                    cmd = new SqlCommand("SPU_SITUACAO", conn);

                    cmd.Parameters.AddWithValue("@CD_SITUACAO_CLIENTE", situacao.CD_SITUACAO);
                    cmd.Parameters.AddWithValue("@DC_SITUACAO", situacao.DC_SITUACAO);


                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = conn;
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        internal void SPD_SITUACAO(int ID)
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString("ClienteConnection")))
                {
                    cmd.Parameters.Clear();
                    cmd = new SqlCommand("SPD_SITUACAO", conn);

                    cmd.Parameters.AddWithValue("@CD_SITUACAO_CLIENTE", ID);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = conn;
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
