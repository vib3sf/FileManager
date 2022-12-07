using System;
using System.IO;
using Microsoft.VisualBasic;

namespace FileManager.Models;

public sealed class FileModel : BaseModel
{ 
    public long Size => new FileInfo(FullPath).Length;
    public override string CreationDate => new FileInfo(FullPath).CreationTime.ToLongDateString();
    public FileModel(string name, string fullPath) : base(name, fullPath)
    {
    }
}