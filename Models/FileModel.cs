using System.IO;

namespace FileManager.Models;

public sealed class FileModel : BaseModel
{ 
    public long Size => new FileInfo(FullPath).Length;
    public override string CreationDate => new FileInfo(FullPath).CreationTime.ToLongDateString();

    public override string Icon
    {
        get
        {
            switch (Path.GetExtension(FullPath))
            {
                case ".png": case ".jpg": case ".jpeg":
                    return "../Icons/image.png";
                case ".docx":
                    return "../Icons/docs.png";
                case ".pdf":
                    return "../Icons/pdf.png";
                case ".txt":
                    return "../Icons/notepad.jpg";
                default:
                    return "../Icons/unknown.png";
                    
            }
        }
    }

    public FileModel(string name, string fullPath) : base(name, fullPath)
    {
    }
}