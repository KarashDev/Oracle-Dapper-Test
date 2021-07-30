using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OracleTest
{
    class Entity
    {
        public int ID;
        public string TYPE;
        public string APIRESOURCEID;
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnDbResult_Click(object sender, RoutedEventArgs e)
        {
            
            //string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            var connectionString = "Data Source=TESTCN;User Id=karash_is;Password=qwe123;";

            // Прямое подключение (ADO.NET)
            //using (OracleConnection connection = new OracleConnection(connectionString))
            //{
            //    connection.Open();

            //    OracleCommand command = new OracleCommand();
            //    command.Connection = connection;
            //    command.CommandText = "select * from CN.IS4$APIRESOURCECLAIM";
            //    command.CommandType = CommandType.Text;

            //    OracleDataReader dataReader = command.ExecuteReader();
            //    dataReader.Read();

            //    lblDbResult.Content = dataReader.GetString(1);

            //    connection.Close();
            //}



            // Через Dapper
            var inputID = Convert.ToInt32(txtboxInputID.Text);

            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                connection.Open();

                var entities = connection.Query<Entity>($"select ID, TYPE, APIRESOURCEID from CN.IS4$APIRESOURCECLAIM where ID = {inputID}").ToList();

                lblDbResult.Content = entities.FirstOrDefault(e => e.ID == inputID).TYPE;
               
                connection.Close();
            }

            




        }
    }
}
