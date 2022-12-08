using System.Windows;
using FileManager.Models;
using FileManager.ViewModels;

namespace FileManager.View;

public partial class DirectoryCreateWindow : Window
{
    public DirectoryCreateWindow(MainModel mainModel)
    {
        InitializeComponent();
        DataContext = new DirectoryCreateViewModel(mainModel);
    }


    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        Close();
    }
}