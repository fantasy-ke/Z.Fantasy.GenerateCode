using Wpf.Ui.Controls;
using Z.Fantasy.GenerateCode.ViewModels.Pages;

namespace Z.Fantasy.GenerateCode.Views.Pages
{
    public partial class DashboardPage : INavigableView<DashboardViewModel>
    {
        public DashboardViewModel ViewModel { get; }

        public DashboardPage(DashboardViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;
            InitializeComponent();
        }
    }
}
