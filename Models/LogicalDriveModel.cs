namespace FileManager.Models;

public class LogicalDriveModel : DirectoryModel
{
    public override string CreationDate => "";
    public override string Icon => @"..\Icons\logicalDrive.png";

    public LogicalDriveModel(string name, string fullPath) : base(name, fullPath) { }
}