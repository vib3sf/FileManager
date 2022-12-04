using System.Collections.ObjectModel;
using FileManager.Models;
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
    public DirectoryModel SelectedFavoriteItem { get; set; }

    private string _textBox;
    public string TextBox
    {
        get => _textBox;
        set
        {
            _textBox = value;
            CurrentDirectory = _textBox; 
            RaisePropertyChanged("TextBox");
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
        TextBox = CurrentDirectory;
        _mainModel.PropertyChanged += (s, e) => { RaisePropertyChanged(e.PropertyName); };
        OpenCommand = new DelegateCommand<BaseModel>(_ =>
        {
            if (SelectedItem != null) _mainModel.Open(SelectedItem);
            TextBox = CurrentDirectory;
        });
        BackCommand = new DelegateCommand<BaseModel>(_ =>
        {
            _mainModel.BackDirectory(CurrentDirectory);
            TextBox = CurrentDirectory;
        });
        FindCommand = new DelegateCommand<string>(str =>
        {
            _mainModel.FindAndOpenDirectory(TextBox);
        });
        OpenFavoriteCommand = new DelegateCommand<DirectoryModel>(_ =>
        {
            if (SelectedFavoriteItem != null) _mainModel.Open(SelectedFavoriteItem);
            TextBox = CurrentDirectory;
        });
        AddFavoriteCommand = new DelegateCommand<BaseModel>(_ =>
        {
            _mainModel.AddFavorite((SelectedItem as DirectoryModel)!);
        });
    }
}