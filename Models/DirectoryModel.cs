using System;
using System.IO;

namespace FileManager.Models;
[Serializable]
public sealed class DirectoryModel : BaseModel
{
    public override string CreationDate => new DirectoryInfo(FullPath).CreationTime.ToLongDateString();
    public override string Icon => "../Icons/directory.png";
    public DirectoryModel () {}
    public DirectoryModel(string name, string fullPath) : base(name, fullPath) { }
}