using System.ComponentModel;

namespace AuthHandlers56.Repositories;

public class UserRepository
{
    public List<User> Users { get; set; } = new List<User>();
    private static UserRepository? _instanance;
    
       
    public static UserRepository? Instanance => _instanance ?? (_instanance = new UserRepository());


}


public class User
{
    public Guid Id { get; set; }
    public string UserName { get; set; } 
}