using LojaWebMvc.Models.Enums;

namespace LojaWebMvc.Models;

public class Server{



    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public ServerStatus Status { get; set; }
    public ICollection<Login> Logins{get; set;} = new List<Login>();

    //public int ServerId {get; set;}
    public Server(){

    }
    public Server(int id, string name, string address, ServerStatus status){
        Id = id;
        Name = name;
        Address = address;
        Status = status;
    }
}