namespace FileManager.Models;

public abstract class BaseModel
{
    private string Name { get; set; }
    public string FullPath { get; set; }

    protected BaseModel(string name, string fullPath)
    {
        Name = name;
        FullPath = fullPath;
    }

    public override string ToString()
    {
        return Name;
    }
}