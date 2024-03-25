using DataAccessLayer.Abstract;
using Microsoft.EntityFrameworkCore;


namespace DataAccessLayer.Concrete
{
    public class GenericRepostory<T>:IGenericRepostory<T> where T: class
    {
        //private readonly DataContext _datacontext;
        //public GenericRepostory(DataContext datacontext)
        //{
        //    _datacontext = datacontext;
        //}
        private readonly DbContextOptions _options;
        public GenericRepostory(DbContextOptions options)
        {
            _options = options;
        }

        public async Task Create(T entites) 
        {
            //await _datacontext.Set<T>().AddAsync(entites);
            using (var db = new DataContext(_options)) 
            {
                await   db.Set<T>().AddAsync(entites);
              await  db.SaveChangesAsync();
            }
        }
        public async Task Delete(T entites) 
        {
            using (var db = new DataContext(_options))
            {
                db.Set<T>().Remove(entites); 
                await db.SaveChangesAsync();
            }
        }
        public async Task<List<T>> GetAllData()
        {
            using (var db = new DataContext(_options)) {
               return await  db.Set<T>().ToListAsync();
            }
        }
        public async Task Update(T entites) 
        { 
            using (var db = new DataContext(_options)) 
            {
                     db.Set<T>().Update(entites);
            await    db.SaveChangesAsync();
            }
        }
        public async Task<T> GetById(int Id) 
        {
            using (var db = new DataContext(_options)) { 
              var data = await db.Set<T>().FindAsync(Id);
                db.Entry(data).State = EntityState.Detached;
                return data;
            }
        }
    }
}
