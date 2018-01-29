using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
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
        private string _connstringshag = "";
        DataSet ds = new DataSet();
        public MainWindow()
        {
            InitializeComponent();
            _connstring = "data source=192.168.1.201;initial catalog=CRCMS_new;persist security info=True;user id=sa;password=lisa1999;";
            _connstringshag = @"data source=403_4_66\SQLEXPRESS;initial catalog=CRCMS_new;integrated security=True;MultipleActiveResultSets=True;";
            SqlConnection con = new SqlConnection(_connstringshag);
            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(
                "Select * from dic_Group;" +
                "Select * from dic_Model;" +
                "Select * from dic_Pavilion;" +
                "Select * from dic_Status", con);

            DataTableMapping dic_GrouptblMap;
            dic_GrouptblMap = adapter.TableMappings.Add("Table", "dic_Group");
            dic_GrouptblMap.ColumnMappings.Add("GroupId", "GroupIdNew");
            dic_GrouptblMap.ColumnMappings.Add("Name", "NamedNew");

            DataTableMapping dic_ModeltblMap;
            dic_ModeltblMap = adapter.TableMappings.Add("Table1", "dic_Model");
            dic_ModeltblMap.ColumnMappings.Add("ModelId", "ModelId");
            dic_ModeltblMap.ColumnMappings.Add("Code", "CodeModel");
            dic_ModeltblMap.ColumnMappings.Add("Name", "NameModel");
            dic_ModeltblMap.ColumnMappings.Add("SeriesId", "Series");
            adapter.Fill(ds);

            #region Второй вариант переименования названий таблиц
            // ds.Tables[0].TableName = "dic_Group";
            //ds.Tables[1].TableName = "dic_Model";
            ds.Tables[2].TableName = "dic_Pavilion";
            ds.Tables[3].TableName = "dic_Status";
            #endregion
            //var a = ds.Tables["dic_Group"].Rows[0]["GroupIdNew"];
            //foreach (DataTable table in ds.Tables)
            //{
            //    foreach (DataRow item in table.Rows)
            //    {
            //       var o= item["GroupIdNew"].ToString();
            //        var o1 = item["NamedNew"].ToString();
            //    }
            //}
            DicGroupListView.ItemsSource = dic_GroupList(ref ds);
            DicModelListView.ItemsSource = dic_ModelList(ref ds);
            DicPavillionListView.ItemsSource = dic_PavilionList(ref ds);
            DicStatusListView.ItemsSource = dic_StatusList(ref ds);
        }
        public static List<dic_Status> dic_StatusList(ref DataSet ds)
        {
            List<dic_Status> l = new List<dic_Status>();
            foreach (DataRow row in ds.Tables["dic_Status"].Rows)
            {
                dic_Status status = new dic_Status();
                status.StatusId = (int)row.ItemArray[0];
                status.NameEn = (string)row.ItemArray[1];
                status.NameRu = (string)row.ItemArray[2];
                int statusTypeId = 0;
                int.TryParse(row.ItemArray[3].ToString(), out statusTypeId);
                status.StatusTypeId = statusTypeId;
                l.Add(status);
            }
            return l;
        }
        public static List<dic_Pavilion> dic_PavilionList(ref DataSet ds)
        {
            List<dic_Pavilion> l = new List<dic_Pavilion>();
            foreach (DataRow row in ds.Tables["dic_Pavilion"].Rows)
            {
                dic_Pavilion pavilion = new dic_Pavilion();
                pavilion.PavilionId = (int)row.ItemArray[0];
                pavilion.Name = (string)row.ItemArray[1];
                l.Add(pavilion);
            }
            return l;
        }
        public static List<dic_Group> dic_GroupList(ref DataSet ds)
        {
            List<dic_Group> l = new List<dic_Group>();
            foreach (DataRow row in ds.Tables["dic_Group"].Rows)
            {
                dic_Group group = new dic_Group();
                group.GroupId = (int)row.ItemArray[0];
                group.Name = (string)row.ItemArray[1];
                l.Add(group);
            }
            return l;
        }
        public static List<dic_Model> dic_ModelList(ref DataSet ds)
        {
            List<dic_Model> l = new List<dic_Model>();
            foreach (DataRow row in ds.Tables["dic_Model"].Rows)
            {
                dic_Model model = new dic_Model();
                model.ModelId = (int)row.ItemArray[0];
                model.Code = (string)row.ItemArray[1];
                model.Name = (string)row.ItemArray[2];
                int seriesId = 0;
                int.TryParse(row.ItemArray[3].ToString(), out seriesId);
                model.SeriesId = seriesId;
                l.Add(model);
            }
            return l;
        }

        private void Group_GotFocus(object sender, RoutedEventArgs e)
        {
            DicGroupListView.ItemsSource = null;
            DicGroupListView.ItemsSource = dic_GroupList(ref ds);
        }

        private void Status_GotFocus(object sender, RoutedEventArgs e)
        {
            DicStatusListView.ItemsSource = null;
            DicStatusListView.ItemsSource = dic_StatusList(ref ds);
        }

        private void Pavillion_GotFocus(object sender, RoutedEventArgs e)
        {
            DicPavillionListView.ItemsSource = null;
            DicPavillionListView.ItemsSource = dic_PavilionList(ref ds);
        }

        private void Model_GotFocus(object sender, RoutedEventArgs e)
        {
            DicModelListView.ItemsSource = null;
            DicModelListView.ItemsSource = dic_ModelList(ref ds);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DicGroupListView.ItemsSource = null;
            DicModelListView.ItemsSource = null;
            DicPavillionListView.ItemsSource = null;
            DicStatusListView.ItemsSource = null;
            _connstringshag = @"data source=403_4_66\SQLEXPRESS;initial catalog=CRCMS_new;integrated security=True;MultipleActiveResultSets=True;";
            SqlConnection con = new SqlConnection(_connstringshag);
            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select * from dic_Group;" +
                "Select * from dic_Model;" +
                "Select * from dic_Pavilion;" +
                "Select * from dic_Status";
            cmd.Connection = con;

            adapter.SelectCommand = cmd;
            ds = new DataSet();
            adapter.Fill(ds);
            ds.Tables[0].TableName = "dic_Group";
            ds.Tables[1].TableName = "dic_Model";
            ds.Tables[2].TableName = "dic_Pavilion";
            ds.Tables[3].TableName = "dic_Status";

          
            GroupIdColumn.Header = "NewGroupIdUsingSqlCommand";
            NameColumn.Header = "NewNameUsingSqlCommand";

            DicGroupListView.ItemsSource = dic_GroupList(ref ds);
            DicModelListView.ItemsSource = dic_ModelList(ref ds);
            DicPavillionListView.ItemsSource = dic_PavilionList(ref ds);
            DicStatusListView.ItemsSource = dic_StatusList(ref ds);
            MessageBox.Show("Мы Использовали КОМАНДУ для выгрузки данных)");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            GroupIdColumn.Header = "NewGroupIdUsingSqlConstructor";
            NameColumn.Header = "NewNameUsingSqlConstructor";
            DicGroupListView.ItemsSource = null;
            DicModelListView.ItemsSource = null;
            DicPavillionListView.ItemsSource = null;
            DicStatusListView.ItemsSource = null;
            SqlConnection con = new SqlConnection(_connstringshag);
            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(
                "Select * from dic_Group;" +
                "Select * from dic_Model;" +
                "Select * from dic_Pavilion;" +
                "Select * from dic_Status", con);

            DataTableMapping dic_GrouptblMap;
            dic_GrouptblMap = adapter.TableMappings.Add("Table", "dic_Group");
            dic_GrouptblMap.ColumnMappings.Add("GroupId", "GroupIdNew");
            dic_GrouptblMap.ColumnMappings.Add("Name", "NamedNew");

            DataTableMapping dic_ModeltblMap;
            dic_ModeltblMap = adapter.TableMappings.Add("Table1", "dic_Model");
            dic_ModeltblMap.ColumnMappings.Add("ModelId", "ModelId");
            dic_ModeltblMap.ColumnMappings.Add("Code", "CodeModel");
            dic_ModeltblMap.ColumnMappings.Add("Name", "NameModel");
            dic_ModeltblMap.ColumnMappings.Add("SeriesId", "Series");
            ds = new DataSet();
            adapter.Fill(ds);

            #region Второй вариант переименования названий таблиц
            // ds.Tables[0].TableName = "dic_Group";
            //ds.Tables[1].TableName = "dic_Model";
            ds.Tables[2].TableName = "dic_Pavilion";
            ds.Tables[3].TableName = "dic_Status";
            #endregion
            //var a = ds.Tables["dic_Group"].Rows[0]["GroupIdNew"];
            //foreach (DataTable table in ds.Tables)
            //{
            //    foreach (DataRow item in table.Rows)
            //    {
            //       var o= item["GroupIdNew"].ToString();
            //        var o1 = item["NamedNew"].ToString();
            //    }
            //}
            DicGroupListView.ItemsSource = dic_GroupList(ref ds);
            DicModelListView.ItemsSource = dic_ModelList(ref ds);
            DicPavillionListView.ItemsSource = dic_PavilionList(ref ds);
            DicStatusListView.ItemsSource = dic_StatusList(ref ds);
            MessageBox.Show("Мы Использовали КОНСТРУКТОР для выгрузки данных)");
        }
    }

    public class dic_Model
    {
        public int ModelId { get; set; }


        public string Code { get; set; }


        public string Name { get; set; }

        public int? SeriesId { get; set; }
    }
    public class dic_Group
    {
        public int GroupId { get; set; }
        public string Name { get; set; }
    }
    public class dic_Pavilion
    {
        public int PavilionId { get; set; }
        public string Name { get; set; }
    }
    public class dic_Status
    {
        public int StatusId { get; set; }


        public string NameEn { get; set; }


        public string NameRu { get; set; }

        public int? StatusTypeId { get; set; }
    }
}
