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
            var count = 0;
            while (size >= 1024) {
                count++;
                size /= 1024;
            }

            return count switch
            {
                0 => $"{Math.Round(size)} Bytes",
                1 => $"{Math.Round(size)} KB",
                2 => $"{Math.Round(size)} MB",
                3 => $"{Math.Round(size)} GB",
                5 => $"{Math.Round(size)} TB",
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