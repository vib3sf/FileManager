namespace FileManager.Models;

public sealed class FileModel : BaseModel
{
    public FileModel(string name, string fullPath) : base(name, fullPath)
    {
    }
}