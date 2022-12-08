using FileManager.Models;
using Prism.Commands;

namespace FileManager.ViewModels;

public class DirectoryCreateViewModel
{
    public DelegateCommand DirectoryCreateCommand { get; }
    public string DirectoryCreateName { get; set; }
    private MainModel _mainModel { get; }

    public DirectoryCreateViewModel(MainModel mainModel)
    {
        _mainModel = mainModel;
        DirectoryCreateCommand = new DelegateCommand(() =>
        {
            if (DirectoryCreateName != null) _mainModel.CreateDirectory(_mainModel.CurrentDirectory, DirectoryCreateName);
            
        });
    }
}