using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace WpfApplication3
{
    /// <summary>
    /// Interaction logic for AddOrder.xaml
    /// </summary>
    public partial class UpdateOrder : Window
    {
        public UpdateOrder()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
