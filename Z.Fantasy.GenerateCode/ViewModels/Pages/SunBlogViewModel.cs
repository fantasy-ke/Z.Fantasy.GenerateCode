using Microsoft.Win32;
using System.IO;
using Z.Fantasy.GenerateCode.Models;
using Z.Fantasy.GenerateCode.Services;

namespace Z.Fantasy.GenerateCode.ViewModels.Pages
{
    public partial class SunBlogViewModel : ObservableObject
    {
        [ObservableProperty]
        private LogHelper _log = new();

        [ObservableProperty]
        private int _tableCount = 0;

        [ObservableProperty]
        private string _state = string.Empty;

        [RelayCommand]
        private void OnOpenDirectory()
        {
            try
            {
                var path = AppDomain.CurrentDomain.BaseDirectory + "src";
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                System.Diagnostics.Process.Start("explorer.exe", path);
            }
            catch (Exception ex)
            {
                Log.Write($"{ex}");
                MessageBox.Show(ex.Message);
            }
        }
    }
}
