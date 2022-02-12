using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokeWeb.Controllers
{
    public class DbController : Controller
    {
        public class Pokemon
        {
            [Key]
            [Display(Name = "編號")]
            [Required]
            public string No { get; set; }
            [Display(Name = "中文名稱")]
            [Required]
            public string TwName { get; set; }
            [Display(Name = "英文名稱")]
            [Required]
            public string EnName { get; set; }
            [Display(Name = "日文名稱")]
            [Required]
            public string JpName { get; set; }
            [Column(TypeName = "int")]
            [Display(Name = "屬性1")]
            [Required(ErrorMessage = "屬性1不可為空")]
            public int Type_1 { get; set; }
            [Column(TypeName = "int")]
            [Display(Name = "屬性2")]
            [Required]
            public int Type_2 { get; set; }
            [Display(Name = "建立時間")]
            [Required]
            public DateTime CreatTime { get; set; }
        }

        public class PokemonType
        {
            [Key]
            [Display(Name = "編號")]
            public int No { get; set; }
            [Display(Name = "中文屬性名稱")]
            [Required(ErrorMessage = "中文屬性不可為空")]
            public string TwName { get; set; }
            [Display(Name = "英文屬性名稱")]
            [Required(ErrorMessage = "英文屬性不可為空")]
            public string EnName { get; set; }
            [Display(Name = "日文屬性名稱")]
            [Required(ErrorMessage = "日文屬性不可為空")]
            public string JpName { get; set; }
            [Display(Name = "屬性圖片路徑")]
            public string Image { get; set; }
            [Display(Name = "屬性ICON圖片路徑")]
            public string ImageIcon { get; set; }
        }
    }
}
