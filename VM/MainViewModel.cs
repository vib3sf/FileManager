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
            _currentDirectory = _textBox; 
        }
    }

    private string _currentDirectory;
    public string CurrentDirectory
    {
        get => _mainModel.CurrentDirectory;
        set
        {
            _currentDirectory = value;
            RaisePropertyChanged("CurrentDirectory");
        }
    }


    public ReadOnlyObservableCollection<BaseModel> ReadOnlyObservableCollection =>
        _mainModel.ReadOnlyObservableCollection;

    

    public MainViewModel()
    {
        _mainModel.PropertyChanged += (s, e) => { RaisePropertyChanged(e.PropertyName); };
        OpenCommand = new DelegateCommand<BaseModel>(_ =>
        {
            if (SelectedItem != null) _mainModel.Open(SelectedItem);
        });
        BackCommand = new DelegateCommand<BaseModel>(_ =>
        {
            _mainModel.BackDirectory(CurrentDirectory);
        });
        FindCommand = new DelegateCommand<string>(str =>
        {
            _mainModel.FindAndOpenDirectory(TextBox);
        });
    }
}