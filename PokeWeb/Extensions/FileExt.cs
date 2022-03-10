namespace PokeWeb.Extensions;

public class FileExt : IFileExt
{
    private readonly IWebHostEnvironment _env;
    private string staticRoute(string FileRoute) => _env.WebRootPath + "\\" + FileRoute;

    public FileExt(IWebHostEnvironment env)
    {
        _env = env;
    }

    public async void SaveFile(IFormFile file, string Path)
    {
        if (file != null)
        {
            if (file.Length > 0)
            {
                using FileStream stream = new FileStream(staticRoute(Path) + file.FileName, FileMode.Create);
                await file.CopyToAsync(stream);
            }
        }
    }

    public async void SaveFile(IFormFile file, string Path, string FileName)
    {
        if (file != null)
        {
            if (file.Length > 0)
            {
                using FileStream stream = new FileStream(staticRoute(Path) + FileName, FileMode.Create);
                await file.CopyToAsync(stream);
            }
        }
    }
}
