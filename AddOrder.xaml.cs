using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace WpfApplication3
{
    /// <summary>
    /// Interaction logic for AddOrder.xaml
    /// </summary>
    public partial class AddOrder : Window
    {
        public AddOrder()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = MainWindow.ConnectionString;
            string query = "INSERT INTO Orders " +
                    "(CustomerID, EmployeeID, OrderDate, RequiredDate, ShippedDate, ShipVia, Freight, ShipName, ShipAddress, " +
                    "ShipCity, ShipRegion, ShipPostalCode, ShipCountry) " +
                    "VALUES('vinet', 5, @orderDate, @requiredDate, @shippedDate, 3, @freight, @shipName," +
                    " @shipAddress, @shipCity, @shipRegion, @shipPostalCode, @shipCountry)";

            using (var conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();

                    cmd.Parameters.Add("@orderDate", SqlDbType.Date).Value = OrderDateDatePicker.SelectedDate.Value.Date ;
                    cmd.Parameters.Add("@requiredDate", SqlDbType.Date).Value = RequiredDateDatePicker.SelectedDate.Value.Date;
                    cmd.Parameters.Add("@shippedDate", SqlDbType.Date).Value = ShippedDateDatePicker.SelectedDate.Value.Date;
                    cmd.Parameters.Add("@freight", SqlDbType.Money).Value = $"{FreightTextBox.Text:C2}";
                    cmd.Parameters.Add("@shipName", SqlDbType.Text).Value = ShipNameTextBox.Text;
                    cmd.Parameters.Add("@shipAddress", SqlDbType.Text).Value = ShipAddressTextBox.Text;
                    cmd.Parameters.Add("@shipCity", SqlDbType.Text).Value = ShipCityTextBox.Text;
                    cmd.Parameters.Add("@shipRegion", SqlDbType.Text).Value = ShipRegionTextBox.Text;
                    cmd.Parameters.Add("@shipPostalCode", SqlDbType.Text).Value = ShipPostalTextBox.Text;
                    cmd.Parameters.Add("@shipCountry", SqlDbType.Text).Value = ShipCountryTextBox.Text;

                    cmd.ExecuteNonQuery();
                    conn.Close();
                    Close();
                }
            }
        }
    }
}
