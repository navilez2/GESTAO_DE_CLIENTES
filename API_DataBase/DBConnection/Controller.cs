using API_DataBase.Models;

namespace API_DataBase.DBConnection
{
    public class Controller
    {
        public Controller() { }
        public IEnumerable<Cliente> SPS_CLIENTE()
        {
            return new Dao().SPS_CLIENTE();
        }
        public IEnumerable<Cliente> SPS_CLIENTE(int ID)
        {
            return new Dao().SPS_CLIENTE(ID);
        }
        public void SPI_CLIENTE(Cliente cliente)
        {
            new Dao().SPI_CLIENTE(cliente);
        }
        public void SPU_CLIENTE(Cliente cliente)
        {
            new Dao().SPU_CLIENTE(cliente);
        }
        public void SPD_CLIENTE(int ID)
        {
            new Dao().SPD_CLIENTE(ID);
        }


        public IEnumerable<Situacao> SPS_SITUACAO()
        {
            return new Dao().SPS_SITUACAO();
        }
        public IEnumerable<Situacao> SPS_SITUACAO(int ID)
        {
            return new Dao().SPS_SITUACAO(ID);
        }
        public void SPI_SITUACAO(Situacao situacao)
        {
            new Dao().SPI_SITUACAO(situacao);
        }
        public void SPU_SITUACAO(Situacao situacao)
        {
            new Dao().SPU_SITUACAO(situacao);
        }
        public void SPD_SITUACAO(int ID)
        {
            new Dao().SPD_SITUACAO(ID);
        }
    }
}
