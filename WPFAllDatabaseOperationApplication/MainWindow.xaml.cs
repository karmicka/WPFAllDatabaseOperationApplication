using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;

namespace WPFAllDatabaseOperationApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        SqlConnection conn = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\MyDatabase.mdf;Integrated Security=True;User Instance=True");
        public MainWindow()
        {
            InitializeComponent();
        }
        public void BindMyData()
        {
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand("SELECT * FROM Student", conn);
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(comm);
                da.Fill(ds);
                myDataGrid.ItemsSource = ds.Tables[0].DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                conn.Close();
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            BindMyData();
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand("INSERT INTO Student VALUES(" + txtStudId.Text + ",'" + txtStudName.Text + "'," + txtStudResult.Text + ")", conn);
                comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                conn.Close();
                BindMyData();
            }
        }

       private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand("UPDATE Student SET StudName='" + txtStudName.Text + "',StudResult=" + txtStudResult.Text + "WHERE StudId=" + txtStudId.Text + "", conn);
                comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                conn.Close();
                BindMyData();
            }
       }

       private void btnDelete_Click(object sender, RoutedEventArgs e)
       {
           try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand("DELETE FROM Student WHERE StudId=" + txtStudId.Text + "", conn);
                comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                conn.Close();
                BindMyData();
            }
       }
    }
}
