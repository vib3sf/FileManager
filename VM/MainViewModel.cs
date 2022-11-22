using System.Collections.ObjectModel;
using FileManager.Models;
using Prism.Commands;
using Prism.Mvvm;

namespace FileManager.VM;

public class MainViewModel : BindableBase
{
    private readonly MainModel _mainModel = new();
    public DelegateCommand<BaseModel> OpenCommand { get; }
    public BaseModel SelectedItem { get; set; }
    public DelegateCommand<BaseModel> BackCommand { get; }
    public DelegateCommand<string> FindCommand { get; }

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


    public ReadOnlyObservableCollection<BaseModel> ReadOnlyObservableCollection =>
        _mainModel.ReadOnlyObservableCollection;

    

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
    }
}