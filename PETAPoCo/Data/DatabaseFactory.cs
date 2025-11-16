namespace PETAPoCo.ApplicationSR.Data
{
    public class DatabaseFactory //: IDatabase
    {
        private readonly IDatabase _db;

        public DatabaseFactory(IConfiguration config)
        {
            var cs = config.GetConnectionString("DefaultConnection");

            _db = new Database(cs, "Microsoft.Data.SqlClient");
        }

        public void Dispose() => _db.Dispose();

        // Forward all PetaPoco methods
        public object Insert(object poco) => _db.Insert(poco);
        public int Update(object poco) => _db.Update(poco);
        public int Delete(object poco) => _db.Delete(poco);
        public T SingleOrDefault<T>(string sql, params object[] args) => _db.SingleOrDefault<T>(sql, args);
        public List<T> Fetch<T>(string sql, params object[] args) => _db.Fetch<T>(sql, args);
    }
}
