namespace API_DataBase.DBConnection.DBConfig
{
    public class DataBase
    {


        public DataBase() { }

        internal string GetConnectionString(string dataBaseName)
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            return configuration.GetConnectionString(dataBaseName).ToString();
        }
    }
}
