namespace VueTest.Data.Interfaces
{
    public interface IDaoFactory
    {
        IUserDao UserDao { get; }
    }
}