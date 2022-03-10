
namespace PokeWeb.Extensions
{
    public interface IFileExt
    {
        void SaveFile(IFormFile file, string Path);
        void SaveFile(IFormFile file, string Path, string FileName);
    }
}