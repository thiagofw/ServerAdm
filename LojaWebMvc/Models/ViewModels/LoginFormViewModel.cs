using LojaWebMvc.Models;
using System.Collections.Generic;

namespace LojaWebMvc.Models.ViewModels;

public class LoginFormViewModel
{
    public Login Login{get; set;}
    public ICollection<Server> Servers {get; set;}
}


/*

using LojaWebMvc.Models;
using System.Collections.Generic;

namespace LojaWebMvc.Models.ViewModels;

public class SellerFormViewModel
{
    public Seller Seller { get; set; }
    
    public ICollection<Department> Departments { get; set; }

}



*/