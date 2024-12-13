using Wpf.Ui.Controls;
using Z.Fantasy.GenerateCode.ViewModels.Pages;

namespace Z.Fantasy.GenerateCode.Views.Pages
{
    public partial class DataPage : INavigableView<DataViewModel>
    {
        public DataViewModel ViewModel { get; }

        public DataPage(DataViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();
        }
    }
}
