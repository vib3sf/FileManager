using System;
using System.IO;

namespace FileManager.Models;

public sealed class FileModel : BaseModel
{
    public string Size
    {
        get
        {
            var size = (double) new FileInfo(FullPath).Length;
            var count = (int)Math.Log(size, 1024);
            size = Math.Round(size / Math.Pow(1024, count));

            return count switch
            {
                0 => $"{size} Bytes",
                1 => $"{size} KB",
                2 => $"{size} MB",
                3 => $"{size} GB",
                5 => $"{size} TB",
                _ => ""
            };
        }
    }

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
    public FileModel(string name, string fullPath) : base(name, fullPath) { }
}