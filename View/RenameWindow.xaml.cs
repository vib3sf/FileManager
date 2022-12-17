using System.Windows;
using FileManager.Models;
using FileManager.ViewModels;

namespace FileManager.View;

public partial class RenameWindow : Window
{

    public RenameWindow(MainModel mainModel, MainViewModel mainViewModel)
    {
        InitializeComponent();
        DataContext = new RenameViewModel(mainModel, mainViewModel);
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        Close();
    }
}