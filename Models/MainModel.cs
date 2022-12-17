using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Xml.Serialization;
using Prism.Mvvm;

namespace FileManager.Models;

public class MainModel : BindableBase
{
    public string CurrentDirectory { get; private set; } = "";
    public ObservableCollection<BaseModel> DirectoriesAndFiles { get; } = new(); 
    public ObservableCollection<DirectoryModel> FavoritesDirectories { get; private set;  } = new(); 
    private Stack<string> PastDirectoriesStack { get; } = new();
    public MainModel()
    {
        OpenDirectory(@"C:\");
        LoadData();
    }
    
    private void SaveData()
    {
        var xmlSerializer = new XmlSerializer(typeof(ObservableCollection<DirectoryModel>));

        using var fs = new FileStream("favorites.xml", FileMode.Create);
        xmlSerializer.Serialize(fs, FavoritesDirectories);
    }
    
    private void LoadData()
    {
        var xmlSerializer = new XmlSerializer(typeof(ObservableCollection<DirectoryModel>));
        try
        {
            using var fs = new FileStream("favorites.xml", FileMode.OpenOrCreate);
            FavoritesDirectories = (xmlSerializer.Deserialize(fs) as ObservableCollection<DirectoryModel>)!;
        }
        catch (InvalidOperationException)
        {
            FavoritesDirectories = new ObservableCollection<DirectoryModel>();
        }
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
    
    private void OpenDirectory(string directoryPath, bool clearStack = true)
    {
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
        CurrentDirectory = directoryPath;
        if (clearStack)
            PastDirectoriesStack.Clear();
        RaisePropertyChanged("CurrentDirectory");
        DirectoriesAndFiles.Clear();
        foreach (var directory in directoryInfo.GetDirectories())
        {
            DirectoriesAndFiles.Add(new DirectoryModel(directory.Name, directory.FullName));
        }

        foreach (var file in directoryInfo.GetFiles())
        {
            DirectoriesAndFiles.Add(new FileModel(file.Name, file.FullName));
        }
    }
    
    public void BackDirectory()
    {
        if (CurrentDirectory == @"C:\") return;
        PastDirectoriesStack.Push(CurrentDirectory);
        OpenDirectory(new DirectoryInfo(CurrentDirectory).Parent!.FullName, false);
    }

    public void ForwardDirectory()
    {
        if (PastDirectoriesStack.Count != 0)
            OpenDirectory(PastDirectoriesStack.Pop(), false);
    }
    
    public void FindAndOpenDirectory(string directoryPath)
    {
        try
        {
            OpenDirectory(directoryPath);
        }
        catch (DirectoryNotFoundException)
        {
            MessageBox.Show("Directory not found.");
        }
    }

    private static void OpenFile(string filePath)
    {
        Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
    }
    public void CreateFile(string path, string name)
    {
        if (IsInvalidName(name)) return;
        try
        {
            if (!File.Exists($"{path}\\{name}"))
                File.Create($"{path}\\{name}");
            else
                MessageBox.Show("File is exist.");
        }
        catch (UnauthorizedAccessException)
        {
            MessageBox.Show("Access is denied.");
        }
        OpenDirectory(CurrentDirectory, false);
    }

    public void CreateDirectory(string path, string name)
    {
        if (IsInvalidName(name)) return;
        try
        {
            if (!Directory.Exists($"{path}\\{name}"))
                Directory.CreateDirectory($"{path}\\{name}");
            else
                MessageBox.Show("Directory is exist."); 
        }
        catch (UnauthorizedAccessException)
        {
            MessageBox.Show("Access is denied.");
        }
        OpenDirectory(CurrentDirectory);

    }

    public void Rename(BaseModel model, string newName)
    {
        if (IsInvalidName(newName)) return;
        try
        {
            switch (model)
            {
                case FileModel:
                    File.Move(model.FullPath, $"{CurrentDirectory}\\{newName}");
                    break;
                case DirectoryModel:
                    Directory.Move(model.FullPath,$"{CurrentDirectory}\\{newName}");
                    break;
            }
        }
        catch (UnauthorizedAccessException)
        {
            MessageBox.Show("Access is denied");
        }
        DirectoriesAndFiles.Remove(model);
        OpenDirectory(CurrentDirectory, false);
    }
    private static bool IsInvalidName(string name)
    {
        if (!Path.GetInvalidFileNameChars().Any(name.Contains)) return false;
        MessageBox.Show("Invalid name");
        return true;
    }

    public void Delete(BaseModel model)
    {
        try
        {
            switch (model)
            {
                case FileModel:
                    File.Delete(model.FullPath);
                    break;
                case DirectoryModel:
                    Directory.Delete(model.FullPath, true);
                    break;
            }
        }
        catch (UnauthorizedAccessException)
        {
            MessageBox.Show("Access is denied");
        }
        

        DirectoriesAndFiles.Remove(model);
    } 
    
    public void AddFavorite(DirectoryModel directoryModel)
    {
        FavoritesDirectories.Add(directoryModel);
        SaveData();
        RaisePropertyChanged("FavoritesDirectories");
    }

    public void RemoveFavorite(DirectoryModel directoryModel)
    {
        FavoritesDirectories.RemoveAt(FavoritesDirectories.IndexOf(directoryModel));
        SaveData();
        RaisePropertyChanged("FavoritesDirectories");
    }
    
}