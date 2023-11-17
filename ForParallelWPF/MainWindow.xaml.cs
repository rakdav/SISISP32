using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
using Path = System.IO.Path;

namespace ForParallelWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CancellationTokenSource _cancelToken=new CancellationTokenSource();
       public MainWindow()
        {
            InitializeComponent();
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            _cancelToken.Cancel();
        }

        private void cmdProcess_Click(object sender, RoutedEventArgs e)
        {
            Task.Factory.StartNew(()=> ProcessFiles());
            this.Title = "Преобразование началось...";
        }
        private void ProcessFiles()
        {
            var basePath=Directory.GetCurrentDirectory();
            var pictureDirectory = Path.Combine(basePath, "TestPictures");
            var outputDirectory = Path.Combine(basePath, "ModifiedPictures");
            if(Directory.Exists(outputDirectory))
            {
                Directory.Delete(outputDirectory, true);
            }
            Directory.CreateDirectory(outputDirectory);
            ParallelOptions parOptions=new ParallelOptions();
            parOptions.CancellationToken=_cancelToken.Token;
            parOptions.MaxDegreeOfParallelism=Environment.ProcessorCount;
            string[] files=Directory.GetFiles(pictureDirectory,"*.jpg",SearchOption.AllDirectories);
            //foreach(string file in files)
            //{
            try
            {
                Parallel.ForEach(files, parOptions, file =>
                {
                    parOptions.CancellationToken.ThrowIfCancellationRequested();
                    string fileName = Path.GetFileName(file);
                    Dispatcher?.Invoke(new Action(() =>
                    this.Title = $"Обработка {fileName} в потоке" +
                        $" {Thread.CurrentThread.ManagedThreadId}"
                        ));
                    using (Bitmap bitmap = new Bitmap(file))
                    {
                        bitmap.RotateFlip(RotateFlipType.Rotate180FlipNone);
                        bitmap.Save(Path.Combine(outputDirectory, fileName));
                    }
                });
                Dispatcher?.Invoke(()=>this.Title="Закончено");
            }
            catch (OperationCanceledException ex)
            {
                Dispatcher?.Invoke(() => this.Title = ex.Message);
            }
        }
    }
}
