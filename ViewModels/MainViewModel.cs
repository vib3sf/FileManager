using System;
using System.Collections.ObjectModel;
using System.Windows;
using FileManager.Models;
using FileManager.View;
using Prism.Commands;
using Prism.Mvvm;

namespace FileManager.ViewModels;

public class MainViewModel : BindableBase
{
    private readonly MainModel _mainModel = new();
    public DelegateCommand<BaseModel> OpenCommand { get; }
    public BaseModel SelectedItem { get; set; }
    public DelegateCommand<BaseModel> BackCommand { get; }
    public DelegateCommand<string> FindCommand { get; }
    public DelegateCommand<BaseModel> AddFavoriteCommand { get; }

    public DelegateCommand<DirectoryModel> OpenFavoriteCommand { get; }

    public DelegateCommand<DirectoryModel> RemoveFavoriteCommand { get; }
    public DirectoryModel SelectedFavoriteItem { get; set; }
    
    public DelegateCommand<BaseModel> DeleteCommand { get; }
    public DelegateCommand<BaseModel> FileCreateWindowCommand { get; }
    
    public DelegateCommand DirectoryCreateWindowCommand { get; }
   

    private string _searchTextBox;
    public string SearchTextBox
    {
        get => _searchTextBox;
        set
        {
            _searchTextBox = value;
            CurrentDirectory = _searchTextBox; 
            RaisePropertyChanged("SearchTextBox");
        }
    }

    private string _currentDirectory;
    private string CurrentDirectory { get => _mainModel.CurrentDirectory;
        set => _currentDirectory = value;
    }


    public ObservableCollection<BaseModel> DirectoryAndFiles => _mainModel.DirectoriesAndFiles;

    public ObservableCollection<DirectoryModel> FavoritesDirectories => _mainModel.FavoritesDirectories;
    
    public DelegateCommand ForwardCommand { get; }
    
    public MainViewModel()
    {
        SearchTextBox = CurrentDirectory;
        _mainModel.PropertyChanged += (s, e) => { RaisePropertyChanged(e.PropertyName); };
        OpenCommand = new DelegateCommand<BaseModel>(_ =>
        {
            if (SelectedItem != null) _mainModel.Open(SelectedItem);
            SearchTextBox = CurrentDirectory;
        });
        BackCommand = new DelegateCommand<BaseModel>(_ =>
        {
            _mainModel.BackDirectory();
            SearchTextBox = CurrentDirectory;
        });
        FindCommand = new DelegateCommand<string>(str =>
        {
            _mainModel.FindAndOpenDirectory(SearchTextBox);
        });
        OpenFavoriteCommand = new DelegateCommand<DirectoryModel>(_ =>
        {
            if (SelectedFavoriteItem != null) _mainModel.Open(SelectedFavoriteItem);
            SearchTextBox = CurrentDirectory;
        });
        AddFavoriteCommand = new DelegateCommand<BaseModel>(_ =>
        {
            if (SelectedItem is DirectoryModel model) _mainModel.AddFavorite(model);
        });
        RemoveFavoriteCommand = new DelegateCommand<DirectoryModel>(_ =>
        {
            if (SelectedFavoriteItem != null) _mainModel.RemoveFavorite(SelectedFavoriteItem);
        });
        DeleteCommand = new DelegateCommand<BaseModel>(_ =>
        {
            if (SelectedItem != null) _mainModel.Delete(SelectedItem);
        });
        DirectoryCreateWindowCommand = new DelegateCommand(() =>
        {
            var createWindow = new DirectoryCreateWindow(_mainModel);
            createWindow.ShowDialog();
        });
        FileCreateWindowCommand = new DelegateCommand<BaseModel>(_ =>
        {
            var createWindow = new FileCreateWindow(_mainModel);
            createWindow.ShowDialog();
        });
        ForwardCommand = new DelegateCommand(() =>
        {
            _mainModel.ForwardDirectory();
            SearchTextBox = CurrentDirectory;
        });

    }
}