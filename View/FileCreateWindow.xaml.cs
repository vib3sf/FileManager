using System.Windows;
using FileManager.Models;
using FileManager.ViewModels;

namespace FileManager.View;

public partial class FileCreateWindow : Window
{

    public FileCreateWindow(MainModel mainModel)
    {
        InitializeComponent();
        DataContext = new FileCreateViewModel(mainModel);
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        Close();
    }
}