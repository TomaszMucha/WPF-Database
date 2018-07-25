using System.Windows;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Controls;
using System;
using System.Windows.Data;

namespace WpfApplication3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public static int SelectedOrderId;

        public static string ConnectionString
        {
            get
            {
                return @"Data Source=DESKTOP-24E48FP\SQLEXPRESS;Initial Catalog=Northwind;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            }
        }

        private void BaseLoaded(object sender, RoutedEventArgs e)
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Orders", conn);
                SqlDataReader dr = cmd.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Load(dr);
                dataGrid1.ItemsSource = dt.AsDataView();
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Orders", conn);
                SqlDataReader dr = cmd.ExecuteReader();

                DataTable dt = new DataTable();
                AddOrder ao = new AddOrder();
                ao.Show();
                dt.Load(dr);
                dataGrid1.ItemsSource = dt.AsDataView();
            }
        }

        private void ReadButton_Click(object sender, RoutedEventArgs e)
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Orders", conn);
                SqlDataReader dr = cmd.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Load(dr);
                dataGrid1.ItemsSource = dt.AsDataView();
                conn.Close();
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if ((DataRowView)dataGrid1.SelectedItem!=null)
            {
                using (var conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    DataRowView drv = (DataRowView)dataGrid1.SelectedItem;

                    UpdateOrder uo = new UpdateOrder();
                    uo.OrderDateDatePicker.DisplayDate = (DateTime)drv[3];
                    uo.RequiredDateDatePicker.DisplayDate = (DateTime)drv[4];
                    uo.ShippedDateDatePicker.DisplayDate = (DateTime)drv[5];
                    uo.FreightTextBox.Text = drv[7].ToString();
                    uo.ShipNameTextBox.Text = drv[8]?.ToString(); ;
                    uo.ShipAddressTextBox.Text = drv[9]?.ToString();
                    uo.ShipCityTextBox.Text = drv[10]?.ToString();
                    uo.ShipRegionTextBox.Text = drv[11]?.ToString();
                    uo.ShipPostalTextBox.Text = drv[12]?.ToString();
                    uo.ShipCountryTextBox.Text =drv[13]?.ToString();
                    uo.ShowDialog();
                    

                    string query = "UPDATE Orders " +
                            "SET OrderDate = @orderDate, RequiredDate = @requiredDate, ShippedDate = @shippedDate, Freight = @freight," +
                            " ShipName = @shipName, ShipAddress = @shipAddress, ShipCity = @shipCity, ShipRegion = shipRegion, " +
                            "ShipPostalCode = @shipPostalCode, ShipCountry = @shipCountry" +
                            " WHERE orderid=@id";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@orderDate", SqlDbType.Date).Value = uo.OrderDateDatePicker.DisplayDate;
                        cmd.Parameters.Add("@requiredDate", SqlDbType.Date).Value = uo.RequiredDateDatePicker.DisplayDate;
                        cmd.Parameters.Add("@shippedDate", SqlDbType.Date).Value = uo.ShippedDateDatePicker.DisplayDate;
                        cmd.Parameters.Add("@freight", SqlDbType.Money).Value = $"{uo.FreightTextBox.Text:C2}";
                        cmd.Parameters.Add("@shipName", SqlDbType.Text).Value = uo.ShipNameTextBox.Text;
                        cmd.Parameters.Add("@shipAddress", SqlDbType.Text).Value = uo.ShipAddressTextBox.Text;
                        cmd.Parameters.Add("@shipCity", SqlDbType.Text).Value = uo.ShipCityTextBox.Text;
                        cmd.Parameters.Add("@shipRegion", SqlDbType.Text).Value = uo.ShipRegionTextBox.Text;
                        cmd.Parameters.Add("@shipPostalCode", SqlDbType.Text).Value = uo.ShipPostalTextBox.Text;
                        cmd.Parameters.Add("@shipCountry", SqlDbType.Text).Value = uo.ShipCountryTextBox.Text;

                        cmd.Parameters.Add("@id",SqlDbType.Int).Value= ((DataRowView)dataGrid1.SelectedItem)[0];

                        cmd.ExecuteNonQuery();
                        SqlCommand cmd2 = new SqlCommand("SELECT * FROM Orders", conn);
                        SqlDataReader dr = cmd2.ExecuteReader();
                        DataTable dt = new DataTable();
                        dt.Load(dr);
                        dataGrid1.ItemsSource = dt.AsDataView();
                        conn.Close();
                    }

                }
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if ((DataRowView)dataGrid1.SelectedItem != null)
            {
                using (var conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    DataRowView drv = (DataRowView)dataGrid1.SelectedItem;

                    string query = "DELETE FROM Orders WHERE orderid=@id";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {

                        cmd.Parameters.Add("@id", SqlDbType.Int).Value = ((DataRowView)dataGrid1.SelectedItem)[0];

                        cmd.ExecuteNonQuery();
                        SqlCommand cmd2 = new SqlCommand("SELECT * FROM Orders", conn);
                        SqlDataReader dr = cmd2.ExecuteReader();
                        DataTable dt = new DataTable();
                        dt.Load(dr);
                        dataGrid1.ItemsSource = dt.AsDataView();
                        conn.Close();
                    }

                }
            }
        }
    }
}
