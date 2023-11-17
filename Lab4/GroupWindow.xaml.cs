using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Lab4
{
    /// <summary>
    /// Логика взаимодействия для GroupWindow.xaml
    /// </summary>
    public partial class GroupWindow : Window
    {
        private HttpClient client;
        private Group? group;
        public GroupWindow(string token)
        {
            InitializeComponent();
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            Task.Run(() => Load());
        }
        private async Task Load()
        {
            List<Group>? list = await client.GetFromJsonAsync<List<Group>>("http://localhost:5079/api/groups");
            Dispatcher.Invoke(() =>
            {
                ListGroups.ItemsSource = null;
                ListGroups.Items.Clear();
                ListGroups.ItemsSource = list;
            });
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
