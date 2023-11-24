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
using Lab4.Models;

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
        private async Task Save()
        {
            Group group = new Group
            {
                Name = NameGroup.Text,
                Faculty = Faculty.Text,
                Speciality = Speciality.Text
            };
            JsonContent content = JsonContent.Create(group);
            using var response = await client.PostAsync("http://localhost:5079/api/group", content);
            string responseText = await response.Content.ReadAsStringAsync();
            await Load();
        }
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            await Save();
        }

        private void ListGroups_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            group=ListGroups .SelectedItem as Group;
            NameGroup.Text=group?.Name;
            Faculty.Text=group?.Faculty;
            Speciality.Text=group?.Speciality;
        }

        private async Task Edit()
        {
            group!.Name = NameGroup.Text;
            group!.Faculty = Faculty.Text;
            group!.Speciality = Speciality.Text;
            JsonContent content = JsonContent.Create(group);
            using var response = await client.PutAsync("http://localhost:5079/api/group", content);
            string responseText = await response.Content.ReadAsStringAsync();
            await Load();
        }
        private async Task Delete()
        {
            using var response = await client.DeleteAsync("http://localhost:5079/api/group/" + group?.Id);
            string responseText = await response.Content.ReadAsStringAsync();
            await Load();
        }
        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            await Edit();
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            await Delete();
        }
    }
}
