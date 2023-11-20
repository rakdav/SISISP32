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
    /// Логика взаимодействия для StudentWindow.xaml
    /// </summary>
    public partial class StudentWindow : Window
    {
        private HttpClient client;
        private Group? group;
        public StudentWindow(String token)
        {
            InitializeComponent();
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            Task.Run(()=>LoadGroups());
        }
        private async void LoadGroups()
        {
            List<Group>? list = await client.GetFromJsonAsync<List<Group>>("http://localhost:5079/api/groups");
            Dispatcher.Invoke(() =>
            {
                Group.ItemsSource = list?.Select(p=>p.Name);
            });
        }
    }
}
