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
        public StudentWindow(String token,Student student)
        {
            InitializeComponent();
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            Task.Run(() => LoadGroups());
            Name.Text = student.Name;
            FirstName.Text = student.FirstName;
            Lastname.Text = student.LastName;
            DateBirth.SelectedDate = student.BirthDay;
            cbGroup.SelectedItem = student.Group!.Name;
        }
        private async void LoadGroups()
        {
            List<Group>? list = await client.GetFromJsonAsync<List<Group>>("http://localhost:5079/api/groups");
            Dispatcher.Invoke(() =>
            {
                cbGroup.ItemsSource = list?.Select(p=>p.Name);
            });
        }
        public string? NameProperty
        {
            get { return Name.Text; }
        }
        public string? FirstNameProperty
        {
            get { return FirstName.Text; }
        }
        public string? LastNameProperty
        {
            get { return Lastname.Text; }
        }
        public DateTime? DateBirthProperty
        {
            get { return DateTime.Parse(DateBirth.Text); }
        }
        public async Task<int> getIdGroup()
        {
                Group? group= await client.GetFromJsonAsync<Group>("http://localhost:5079/api/group/"+cbGroup.Text);
            return group!.Id;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.DialogResult= false;
        }
    }
}
