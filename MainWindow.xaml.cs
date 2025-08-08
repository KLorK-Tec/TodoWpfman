using System.Data.SqlTypes;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;
using MongoDB;
using MongoDB.Bson;
using MongoDB.Driver.Core.Servers;
using MongoDB.Driver.Linq;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;


namespace WpfApp1test
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

        [BsonIgnoreExtraElements]
        public class colis
        {
            [BsonId]
            [BsonRepresentation(BsonType.ObjectId)]
            public string Id { get; set; }

            [BsonElement("Todo")]
            public string Todo { get; set; }

            // Add other fields here, matching the exact names in your documents
        }

        public const string uri = "mongodb://localhost:27017/";
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            using (var client = new MongoClient(uri))
            {
                var DB = client.GetDatabase("Listo");
                var Col = DB.GetCollection<colis>("colis");
                Col.InsertOne(new colis {  Todo = $"{Tb1.Text}"  });
                MessageBox.Show($"{Tb1.Text} added");
                Button_Click1(sender, e);

            }
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            using (var client = new MongoClient(uri))
            {
                var DB = client.GetDatabase("Listo");
                var Col = DB.GetCollection<colis>("colis");
                var fil = Builders<colis>.Filter.Empty;
                var res = Col.Find(fil).Project(c => c.Todo);
                lv1.ItemsSource= res.ToList();

            }

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            using (var client = new MongoClient(uri))
            {
                var DB = client.GetDatabase("Listo");
                var Col = DB.GetCollection<colis>("colis");
                var sel = lv1.SelectedValue as string;
                var fil = Builders<colis>.Filter.Eq("Todo",$"{sel}");
                Col.DeleteOne(fil);
                Button_Click1(sender,e);

            }

        }
    }
}