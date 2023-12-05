using System.ComponentModel.DataAnnotations;

namespace LojaWebMvc.Models;

public class Seller
{
//
    public int Id { get; set; }
    [Required(ErrorMessage ="{0} required")]
     [StringLength(60, MinimumLength = 3, ErrorMessage ="{0} size shold be between {2} and {1}")]
    public string Name { get; set; }
    [Required(ErrorMessage ="{0} required")]
    [EmailAddress(ErrorMessage = "Enter a valid email")]
    public string Email { get; set; }
    [Display(Name="Birth Date")]
   [DataType(DataType.Date)]
   [Required(ErrorMessage ="{0} required")]
    public DateTime BirthDate { get; set; }
    [Display(Name = "Base Salary")]
   [DisplayFormat(DataFormatString ="{0:F2}")]
    [Required(ErrorMessage ="{0} required")]
    [Range(100.0, 5000.0, ErrorMessage ="{0} must be from {1} to {2}")]
     public double BaseSalary { get; set; }
    public Department? Department {get; set;}
    [Display(Name ="Department")]
    public int DepartmentId { get; set; }

    public ICollection<SalesRecord> Sales {get; set;} = new List<SalesRecord>();

    public Seller()
    {

    }
    public Seller(int id, string name, string email, DateTime birthdate, double salary, Department department)
    {
        Id = id;
        Name = name;
        Email = email;
        BirthDate = birthdate;
        BaseSalary = salary;
        Department = department;

    }
    public void AddSales(SalesRecord sr)
        {
            Sales.Add(sr);
        }

        public void RemoveSales(SalesRecord sr)
        {
            Sales.Remove(sr);
        }

        public double TotalSales(DateTime initial, DateTime final)
        {
            return Sales.Where(sr => sr.Date >= initial && sr.Date <= final).Sum(sr => sr.Amount);
        }
}