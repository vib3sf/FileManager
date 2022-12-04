using System;

namespace FileManager.Models;
[Serializable]
public sealed class DirectoryModel : BaseModel
{
    public DirectoryModel ()
    {}
    public DirectoryModel(string name, string fullPath) : base(name, fullPath)
    {
    }
    
}