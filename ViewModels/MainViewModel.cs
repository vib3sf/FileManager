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
    
    #region Commands

    public DelegateCommand OpenCommand { get; }
    public DelegateCommand BackCommand { get; }
    public DelegateCommand ForwardCommand { get; }
    public DelegateCommand FindCommand { get; }
    public DelegateCommand AddFavoriteCommand { get; }
    public DelegateCommand OpenFavoriteCommand { get; }
    public DelegateCommand RemoveFavoriteCommand { get; } 
    public DelegateCommand DeleteCommand { get; }
    public DelegateCommand FileCreateWindowCommand { get; }
    public DelegateCommand DirectoryCreateWindowCommand { get; }
    public DelegateCommand RenameCommand { get; }

    #endregion
    
    #region SelectedItems
    
    public BaseModel SelectedItem { get; set; }
    public DirectoryModel SelectedFavoriteItem { get; set; }
    
    #endregion


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
    public MainViewModel()
    {
        SearchTextBox = CurrentDirectory;
        _mainModel.PropertyChanged += (s, e) => { RaisePropertyChanged(e.PropertyName); };
        OpenCommand = new DelegateCommand(() =>
        {
            if (SelectedItem != null) _mainModel.Open(SelectedItem);
            SearchTextBox = CurrentDirectory;
        });
        BackCommand = new DelegateCommand(() =>
        {
            _mainModel.BackDirectory();
            SearchTextBox = CurrentDirectory;
        });
        FindCommand = new DelegateCommand(() =>
        {
            _mainModel.FindAndOpenDirectory(SearchTextBox);
        });
        OpenFavoriteCommand = new DelegateCommand(() =>
        {
            if (SelectedFavoriteItem != null) _mainModel.Open(SelectedFavoriteItem);
            SearchTextBox = CurrentDirectory;
        });
        AddFavoriteCommand = new DelegateCommand(() =>
        {
            if (SelectedItem is DirectoryModel model) _mainModel.AddFavorite(model);
        });
        RemoveFavoriteCommand = new DelegateCommand(() =>
        {
            if (SelectedFavoriteItem != null) _mainModel.RemoveFavorite(SelectedFavoriteItem);
        });
        DeleteCommand = new DelegateCommand(() =>
        {
            if (SelectedItem != null) _mainModel.Delete(SelectedItem);
        });
        DirectoryCreateWindowCommand = new DelegateCommand(() =>
        {
            var createWindow = new DirectoryCreateWindow(_mainModel);
            createWindow.ShowDialog();
        });
        FileCreateWindowCommand = new DelegateCommand(() =>
        {
            var createWindow = new FileCreateWindow(_mainModel);
            createWindow.ShowDialog();
        });
        ForwardCommand = new DelegateCommand(() =>
        {
            _mainModel.ForwardDirectory();
            SearchTextBox = CurrentDirectory;
        });
        RenameCommand = new DelegateCommand(() =>
        {
            var createWindow = new RenameWindow(_mainModel, this);
            createWindow.ShowDialog();
        });
    }
}