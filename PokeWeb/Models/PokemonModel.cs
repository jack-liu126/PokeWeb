using System.ComponentModel.DataAnnotations;

namespace PokeWeb.Models
{
    public class PokemonType_Website
    {
        public Type Type { get; set; }
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
}
