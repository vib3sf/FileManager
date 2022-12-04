using System.IO;

namespace FileManager.Models;

public sealed class FileModel : BaseModel
{

    public long Size => new FileInfo(FullPath).Length;
    public FileModel(string name, string fullPath) : base(name, fullPath)
    {
    }
}