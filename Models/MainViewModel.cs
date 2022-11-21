using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace FileManager.Models;

public class MainViewModel : DependencyObject
{
    private string _currentDirectory = "";
    private readonly ObservableCollection<BaseViewModel> _directoriesAndFiles = new();
    private readonly ListBox _listBox;
    private readonly TextBox _textBox;

    public MainViewModel(ListBox listBox, TextBox textBox)
    {
        _listBox = listBox;
        _textBox = textBox;
        Update(@"C:\");
    }

    private void Update(string newDirectory)
    {
        _currentDirectory = newDirectory;
        var directoryInfo = new DirectoryInfo(newDirectory);
        _directoriesAndFiles.Clear();
        foreach (var directory in directoryInfo.GetDirectories())
        {
            _directoriesAndFiles.Add(new DirectoryViewModel(directory.Name, directory.FullName));
        }

        foreach (var file in directoryInfo.GetFiles())
        {
            _directoriesAndFiles.Add(new FileViewModel(file.Name, file.FullName));
        }
        _listBox.ItemsSource = _directoriesAndFiles;
        _textBox.Text = _currentDirectory;
    }

    
    public void Open(BaseViewModel model)
    {
        if (model is DirectoryViewModel)
            Update(model.FullPath);
    }
    
    
}