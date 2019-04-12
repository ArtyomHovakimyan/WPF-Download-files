using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

namespace Mic.Volo.WPFdownloadfiles
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
        private void BtnDownS_Click(object sender, RoutedEventArgs e)
        {

            //using (WebClient _client=new WebClient())
            //{
            //    Uri adress = new Uri(TextUrlS.Text);

            //    string filenameUri = adress.Segments.Last();
            //    string filename = String.Concat("C:/", filenameUri);

            //    _client.DownloadFileAsync(adress, filename);


            //}
            // A web URL with a file response
            string myWebUrlFile = TextUrlS.Text;
            // Local path where the file will be saved
            Uri uri = new Uri(myWebUrlFile);

            string myLocalFilePath = "E:/" + uri.Segments.Last();

            using (WebClient client = new WebClient())
            {
                client.DownloadProgressChanged += Client_DownloadProgressChanged;
                // client.DownloadStringCompleted += Client_DownloadStringCompleted;
                client.DownloadFileCompleted += Client_DownloadFileCompleted;
                client.DownloadFile(myWebUrlFile, myLocalFilePath);
            }

            BtnDownS.IsEnabled = false;
        }

        private void Client_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            MessageBox.Show("Download Completed");
            BtnDownS.IsEnabled = true;
        }

        private void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            double bytesIn = double.Parse(e.BytesReceived.ToString());
            double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
            double percentage = bytesIn / totalBytes * 100;

            PbStatus.Value = int.Parse(Math.Truncate(percentage).ToString());

            //PbStatus.Value = e.ProgressPercentage;
        }
    }
}
