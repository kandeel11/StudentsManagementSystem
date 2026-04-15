namespace MVC01.BL
{
    public interface IRepositry<T> 
    {
        List<T> GetAll();
        bool Add(T entity);
        bool Update(T entity);
        bool Delete(int id);
        T GetById(int id);
        void Save();




    }
}
