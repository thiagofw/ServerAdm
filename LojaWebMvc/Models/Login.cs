namespace LojaWebMvc.Models;


public class Login{


    public int Id { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public string Username { get; set; }
    public Server? Server { get; set; }
    public int ServerId {get; set;}

    public Login(){

    }
    public Login(int id, string name, string password, string username, Server server)
    {
        Id = id;
        Name = name;
        Password = password;
        Username = username;
        Server = server;
    }
}