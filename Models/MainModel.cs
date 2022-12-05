﻿using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Xml.Serialization;
using Prism.Mvvm;

namespace FileManager.Models;

public class MainModel : BindableBase
{
    public string CurrentDirectory { get; private set; } = "";
    public ObservableCollection<BaseModel> DirectoriesAndFiles { get; } = new();

    public ObservableCollection<DirectoryModel> FavoritesDirectories { get; private set;  } = new();

    private void SaveData()
    {
        var xmlSerializer = new XmlSerializer(typeof(ObservableCollection<DirectoryModel>));

        using var fs = new FileStream("favorites.xml", FileMode.Create);
        xmlSerializer.Serialize(fs, FavoritesDirectories);
    }

    private void LoadData() => FavoritesDirectories =
        (new XmlSerializer(typeof(ObservableCollection<DirectoryModel>)).Deserialize(
            new FileStream("favorites.xml", FileMode.OpenOrCreate)) as ObservableCollection<DirectoryModel>)!;
    


    public MainModel()
    {
        OpenDirectory(@"C:\");
        LoadData();
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

    private static void OpenFile(string filePath)
    {
        Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
    }

    public void BackDirectory(string directoryPath)
    {
        if (directoryPath != @"C:\")
            OpenDirectory(new DirectoryInfo(directoryPath).Parent!.FullName);
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

    public void AddFavorite(DirectoryModel directoryModel)
    {
        if (FavoritesDirectories.Contains(directoryModel))
        {
            MessageBox.Show($"{directoryModel} is exist.");
            return;
        }
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