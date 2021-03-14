using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
    public class mvcAlmacenModel
    {
        public int Id_Almacen { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string Estado { get; set; }
    }
}