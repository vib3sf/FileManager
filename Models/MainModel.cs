using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Prism.Mvvm;

namespace FileManager.Models;

public class MainModel : BindableBase
{
    private string _currentDirectory = "";
    private readonly ObservableCollection<BaseModel> _directoriesAndFiles = new();
    private readonly ListBox _listBox;
    private readonly TextBox _textBox;

    public MainModel(ListBox listBox, TextBox textBox)
    {
        _listBox = listBox;
        _textBox = textBox;
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
        _currentDirectory = directoryPath;
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
        _listBox.ItemsSource = _directoriesAndFiles;
        _textBox.Text = _currentDirectory;
        
    }

    private static void OpenFile(string filePath)
    {
        Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
    }
}