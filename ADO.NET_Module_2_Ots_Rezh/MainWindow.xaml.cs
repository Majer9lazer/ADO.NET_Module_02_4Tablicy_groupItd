using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace ADO.NET_Module_2_Ots_Rezh
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _connstring = "";
        public MainWindow()
        {
            _connstring = "data source=192.168.1.201;initial catalog=CRCMS_new;persist security info=True;user id=sa;password=lisa1999;";
            SqlConnection con=new SqlConnection(_connstring);
            con.Open();
            SqlDataAdapter adapter= new SqlDataAdapter("Select * from dic_Group",con);
            DataSet ds=new DataSet();
            adapter.Fill(ds, "dic_Group");
      
      
            //DicGroupListView.ItemsSource = col;
            InitializeComponent();
            
        }
    }
}
