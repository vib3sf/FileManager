using FileManager.Models;
using Prism.Commands;

namespace FileManager.ViewModels;

public class FileCreateViewModel
{
    public DelegateCommand FileCreateCommand { get; }
    public string FileCreateName { get; set; }
    private MainModel _mainModel { get; }
    
    public FileCreateViewModel(MainModel mainModel)
    {
        _mainModel = mainModel;
        FileCreateCommand = new DelegateCommand(() =>
        {
            if (FileCreateName != null) _mainModel.CreateFile(_mainModel.CurrentDirectory, FileCreateName);
        });
    }
}