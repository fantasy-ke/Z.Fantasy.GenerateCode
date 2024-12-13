using Microsoft.Win32;
using System;
using System.Linq;
using Wpf.Ui.Controls;
using Z.Fantasy.GenerateCode.Models;
using Z.Fantasy.GenerateCode.Services;
using Z.Fantasy.GenerateCode.ViewModels.Pages;
using MessageBox = Wpf.Ui.Controls.MessageBox;

namespace Z.Fantasy.GenerateCode.Views.Pages
{
    /// <summary>
    /// SunBlogPage.xaml 的交互逻辑
    /// </summary>
    public partial class SunBlogPage : INavigableView<SunBlogViewModel>
    {
        private readonly LogHelper _log = new LogHelper();
        List<SourceModel> _reqList;
        private readonly CodeService _codeManage = new CodeService();
        public SunBlogViewModel ViewModel { get; }
        public SunBlogPage(SunBlogViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;
            InitializeComponent();
        }

        private void DataSource_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Title = "选择数据源文件";
                openFileDialog.Filter = "excel文件|*.xlsx";
                openFileDialog.FileName = string.Empty;
                openFileDialog.FilterIndex = 1;
                openFileDialog.Multiselect = false;
                openFileDialog.RestoreDirectory = true;
                openFileDialog.DefaultExt = "xlsx";
                if (openFileDialog.ShowDialog() == false)
                {
                    return;
                }
                string fileName = openFileDialog.FileName;
                _reqList = CodeService.ImportExcel(fileName).Where(m => !string.IsNullOrWhiteSpace(m.TableName)).ToList();
                tableNameListBox.ItemsSource = _reqList;

                //table_name.Content = req.TableName;
                ViewModel.TableCount = _reqList?.Count() ?? 0;
                ViewModel.State = "就绪";
            }
            catch (Exception ex)
            {
                _log.Write($"{ex}");
                var msg = new Wpf.Ui.Controls.MessageBox();
                ShowMessageBox("错误", ex.Message);
            }
        }

        public void ShowMessageBox(string title, string message)
        {
            var messageBox = new MessageBox
            {
                Content = message,
                Title = title,
            };

            //messageBox.OnButtonClick += (sender, args) =>
            //{
            //    // Handle OK button click
            //    messageBox.Close();
            //};

            //messageBox.ButtonRightClick += (sender, args) =>
            //{
            //    // Handle Cancel button click
            //    messageBox.Close();
            //};

            messageBox.ShowDialogAsync();
        }

        private void Generate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_reqList == null)
                {
                    ShowMessageBox("错误", "请先选择数据源！");
                    return;
                }

                List<SourceModel> tableList = new List<SourceModel>();
                if (tableList == null) throw new ArgumentNullException(nameof(tableList));
                if (tableNameListBox.SelectedItems.Count >= 1)
                {
                    //多选
                    var index = 0;
                    for (; index < tableNameListBox.SelectedItems.Count; index++)
                    {
                        var sourceModel = (SourceModel)tableNameListBox.SelectedItems[index];
                        tableList.Add(sourceModel);
                    }

                    //单选
                    //var msg = ((ListBoxItem)tableNameListBox.SelectedItem).Content.ToString();
                }
                if (!tableList.Any())
                {
                    ShowMessageBox("错误", "请先选择表！");
                    return;
                }
                foreach (var item in tableList)
                {
                    item.CoreNamespace = TxtCore.Text;
                    item.AppNamespace = TxtApplication.Text;
                    _codeManage.GenerateAll(item);
                }
                ViewModel.State = "已完成";
            }
            catch (Exception ex)
            {
                _log.Write($"{ex}");
                ShowMessageBox("错误", ex.Message);
            }
        }

    }
}
