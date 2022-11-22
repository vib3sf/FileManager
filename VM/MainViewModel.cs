using System.Collections.ObjectModel;
using FileManager.Models;
using Prism.Commands;
using Prism.Mvvm;

namespace FileManager.VM;

public class MainViewModel : BindableBase
{
    private readonly MainModel _mainModel = new();
    public DelegateCommand<BaseModel> OpenCommand { get; }
    public DelegateCommand<BaseModel> BackCommand { get; }

    public string CurrentDirectory => _mainModel.CurrentDirectory;

    public BaseModel Selected { get; set; }

    public ReadOnlyObservableCollection<BaseModel> ReadOnlyObservableCollection =>
        _mainModel.ReadOnlyObservableCollection;

    

    public MainViewModel()
    {
        _mainModel.PropertyChanged += (s, e) => { RaisePropertyChanged(e.PropertyName); };
        OpenCommand = new DelegateCommand<BaseModel>(_ =>
        {
            if (Selected != null) _mainModel.Open(Selected);
        });
        BackCommand = new DelegateCommand<BaseModel>(_ =>
        {
            _mainModel.BackDirectory(CurrentDirectory);
        });
    }
    
    
}