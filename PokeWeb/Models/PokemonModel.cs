using System.ComponentModel.DataAnnotations;
using static PokeWeb.Models.DbModel;

namespace PokeWeb.Models
{
    public class PokemonType_Website
    {
        public Type Type { get; set; }
    }

    public class PokemonAdd_Website
    {
        public db_Pokemon Pokemon { get; set; }
    }

    public class PokemonEdit_Website
    {
        public PokemonList PokemonList { get; set; }
    }

    public class Detail_Website
    {
        //public int Page { get; set; }
        public string SelectPage { get; set; }
        public PokemonList PokemonList { get; set; }
    }

    public class Type
    {
        [Display(Name = "編號")]
        public int No { get; set; }
        [Display(Name = "中文屬性名稱")]
        [Required(ErrorMessage = "中文屬性不可為空")]
        public string TwName { get; set; }
        [Display(Name = "日文屬性名稱")]
        [Required(ErrorMessage = "日文屬性不可為空")]
        public string JpName { get; set; }
        [Display(Name = "英文屬性名稱")]
        [Required(ErrorMessage = "英文屬性不可為空")]
        public string EnName { get; set; }
        [Display(Name = "屬性圖片路徑")]
        public List<IFormFile> Image { get; set; }
        [Display(Name = "屬性ICON圖片路徑")]
        public List<IFormFile> ImageIcon { get; set; }
    }

    public class PokemonList
    {
        [Display(Name = "編號")]
        public string No { get; set; }
        public int Count { get; set; }
        [Display(Name = "中文名稱")]
        public string TwName { get; set; }
        [Display(Name = "英文名稱")]
        public string EnName { get; set; }
        [Display(Name = "日文名稱")]
        public string JpName { get; set; }
        [Display(Name = "圖片")]
        public string Image { get; set; }
        [Display(Name = "屬性1")]
        public string Type_1 { get; set; }
        [Display(Name = "屬性2")]
        public string Type_2 { get; set; }
        [Display(Name = "屬性1圖片路徑")]
        public string Type_1_Img { get; set; }
        [Display(Name = "屬性2圖片路徑")]
        public string Type_2_Img { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
