using FileManager.Models;
using Prism.Commands;

namespace FileManager.ViewModels;

public class RenameViewModel
{
    public DelegateCommand RenameCommand { get; }
    public string NewName { get; set; }
    private MainModel _mainModel { get; }
    private MainViewModel _mainViewModel;
    
    public RenameViewModel(MainModel mainModel, MainViewModel mainViewModel)
    {
        _mainModel = mainModel;
        _mainViewModel = mainViewModel;
        RenameCommand = new DelegateCommand(() =>
        {
            if (NewName != null) _mainModel.Rename(_mainViewModel.SelectedItem, NewName);
        });
    }
}