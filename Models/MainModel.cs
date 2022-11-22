using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using Prism.Mvvm;

namespace FileManager.Models;

public class MainModel : BindableBase
{
    public string CurrentDirectory { get; set; } = "";
    private readonly ObservableCollection<BaseModel> _directoriesAndFiles = new();

    public readonly ReadOnlyObservableCollection<BaseModel> ReadOnlyObservableCollection;


    public MainModel()
    {
        ReadOnlyObservableCollection = new ReadOnlyObservableCollection<BaseModel>(_directoriesAndFiles);
        OpenDirectory(@"C:\");
    }
    
    public void Open(BaseModel model)
    {
        switch (model)
        {
            case DirectoryModel:
                OpenDirectory(model.FullPath);
                break;
            case FileModel:
                OpenFile(model.FullPath);
                break;
        }
    }
    
    private void OpenDirectory(string directoryPath)
    {
        CurrentDirectory = directoryPath;
        var directoryInfo = new DirectoryInfo(directoryPath);
        try
        {
            directoryInfo.GetDirectories();
        }
        catch (UnauthorizedAccessException)
        {
            MessageBox.Show("Access is denied.");
            return;
        }
        
        _directoriesAndFiles.Clear();
        foreach (var directory in directoryInfo.GetDirectories())
        {
            _directoriesAndFiles.Add(new DirectoryModel(directory.Name, directory.FullName));
        }

        foreach (var file in directoryInfo.GetFiles())
        {
            _directoriesAndFiles.Add(new FileModel(file.Name, file.FullName));
        }
        
        
    }

    private static void OpenFile(string filePath)
    {
        Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
    }
}