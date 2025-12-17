namespace FoodOrderingApplicationFigma.Interfaces
{
    public interface IUsers<T> where T : class
    {
        Task<IEnumerable<T>> GetAllUsers();
        Task<T?> GetUserById(int id);
        Task<T> InsertUser(T entity);
        Task<T?> UpdateUser(T entity);
        Task<bool> DeleteUser(int id);
        Task<T?> GetUserByEmailOrPhone(string emailOrPhone);
    }
}
