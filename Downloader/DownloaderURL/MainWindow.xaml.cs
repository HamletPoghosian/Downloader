using System;
using System.Collections.Generic;
using System.IO;
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

namespace DownloaderURL
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		string folderPath;
		public MainWindow()
		{
			 folderPath = FolderCreater();
			InitializeComponent();
		}

		private async  void ButtonDownloader_Click(object sender, RoutedEventArgs e)
		{
				ButtonDownloader.IsEnabled = false;
				TxtUrl.IsEnabled = false;
			try
			{
				Uri address = new Uri(TxtUrl.Text);
				string[] ar = address.Segments;
				string newFile = folderPath + @"\" + ar[ar.Length - 1];
				FileCreater(newFile);
				await Downloanding(address, newFile);
			}
			catch (UriFormatException ex)
			{
				MessageBox.Show(ex.Message);
			}
			catch (Exception)
			{

				throw;
			}


		}

		public  string FolderCreater()
		{
			string newFolder = "DownLoader";

			string path = System.IO.Path.Combine(
				Environment.GetFolderPath(Environment.SpecialFolder.Desktop), newFolder
			);

			if (!Directory.Exists(path))
			{
				try
				{
					 Directory.CreateDirectory(path);
				}
				catch (IOException ie)
				{
					Console.WriteLine("IO Error: " + ie.Message);
				}
				catch (Exception e)
				{
					Console.WriteLine("General Error: " + e.Message);
					throw;
				}
				
			}
			return  path;

		}
		public void FileCreater(string path)
		{

			if (!File.Exists(path))
			{
				try
				{
					File.Create(path);					
				}
				catch (IOException ie)
				{
					Console.WriteLine("IO Error: " + ie.Message);
				}
				catch (Exception e)
				{
					Console.WriteLine("General Error: " + e.Message);
					throw;
				}

			}
			
		}

		public async Task Downloanding(Uri url, string filename)
		{
			using (WebClient client = new WebClient())
			{
				
				try
				{

				    client.DownloadFileAsync(url, filename);
					
					client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);

				}
				catch (UriFormatException ex)
				{

					MessageBox.Show(ex.Message);
				}
				catch (WebException ex)
				{
					MessageBox.Show(ex.Message);

				}
				catch (InvalidOperationException ex)
				{
					MessageBox.Show(ex.Message);

				}
				catch (Exception)
				{

					throw;
				}
				finally
				{

				}


			}


		}

		private void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
		{					
			double bytesIn = double.Parse(e.BytesReceived.ToString());
			double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
			double percentage = bytesIn / totalBytes * 100;
			progresBarForDownloading.Value = int.Parse(Math.Truncate(percentage).ToString());					   
		}

		
	}
}
