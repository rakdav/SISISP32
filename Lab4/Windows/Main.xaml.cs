using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        private HttpClient httpClient;
        private MainWindow mainWindow;
        private string? token;
        public Main(Response response,MainWindow window)
        {
            InitializeComponent();
            this.mainWindow = window;
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + response.access_token);
            token = response.access_token;
            Task.Run(() => Load());
        }
        private async Task Load()
        {
            List<Student>? list = await httpClient.GetFromJsonAsync<List<Student>>("http://localhost:5079/api/students");
            foreach (Student i in list!)
            {
                i.Group = await httpClient.GetFromJsonAsync<Models.Group>("http://localhost:5079/api/group/" + i.GroupId);
            }
            Dispatcher.Invoke(() =>
            {
                ListStudents.ItemsSource = null; 
                ListStudents.Items.Clear();
                ListStudents.ItemsSource = list;
            });
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.mainWindow.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GroupWindow groupWindow=new GroupWindow(token!);
            groupWindow.ShowDialog();
        }
        //добавление
        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            StudentWindow studentWindow=new StudentWindow(token!);
            if (studentWindow.ShowDialog() == true)
            {
                Student student = new Student
                {
                    Name = studentWindow.NameProperty,
                    FirstName=studentWindow.FirstNameProperty,
                    LastName=studentWindow.LastNameProperty,
                    BirthDay=studentWindow.DateBirthProperty,
                    GroupId=await studentWindow.getIdGroup()
                };
                JsonContent content = JsonContent.Create(student);
                using var response = await httpClient.PostAsync("http://localhost:5079/api/student", content);
                string responseText = await response.Content.ReadAsStringAsync();
                await Load();
            }
        }
        //изменение
        private async void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Student? st=ListStudents.SelectedItem as Student;
            StudentWindow studentWindow = new StudentWindow(token!,st!);
            if (studentWindow.ShowDialog() == true)
            {
                st!.Name = studentWindow.NameProperty;
                st!.FirstName = studentWindow.FirstNameProperty;
                st!.LastName = studentWindow.LastNameProperty;
                st!.BirthDay = studentWindow.DateBirthProperty;
                st!.GroupId = await studentWindow.getIdGroup();
                JsonContent content = JsonContent.Create(st);
                using var response = await httpClient.PutAsync("http://localhost:5079/api/student", content);
                string responseText = await response.Content.ReadAsStringAsync();
                await Load();
            }
        }
        //удаление
        private async void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            Student? st = ListStudents.SelectedItem as Student;
            JsonContent content = JsonContent.Create(st);
            using var response = await httpClient.DeleteAsync("http://localhost:5079/api/student/"+st!.Id);
            string responseText = await response.Content.ReadAsStringAsync();
            await Load();
        }
    }
}
