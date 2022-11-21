namespace FileManager.Models;

public abstract class BaseViewModel
{
    public string Name { get; set; }
    public string FullPath { get; set; }

    protected BaseViewModel(string name, string fullPath)
    {
        Name = name;
        FullPath = fullPath;
    }

    public override string ToString()
    {
        return Name;
    }
}