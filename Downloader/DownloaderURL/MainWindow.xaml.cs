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
		public MainWindow()
		{
			InitializeComponent();
		}

		private async  void ButtonDownloader_Click(object sender, RoutedEventArgs e)
		{
			using (WebClient client= new WebClient())
			{
				ButtonDownloader.IsEnabled = false;
				TxtUrl.IsEnabled = false;
				try
				{
					string folderPath = FolderCreater();
					Uri address = new Uri(TxtUrl.Text);
					string [] ar=address.Segments;
					
					string newFile = folderPath + @"\" + ar[ar.Length - 1];
					FileCreater(newFile);
					client.Credentials = CredentialCache.DefaultNetworkCredentials;
					//client.BaseAddress = address.ToString();

					await client.DownloadFileTaskAsync(address, newFile);
					client.DownloadFileCompleted += Client_DownloadFileCompleted;
					
					
				}
				catch (UriFormatException ex)
				{

					MessageBox.Show(ex.Message);
				}
				catch (WebException ex )
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
					ButtonDownloader.IsEnabled = true;
					TxtUrl.IsEnabled = true;
				}


			}


		}

		

		private void Client_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
		{
			MessageBox.Show("Downloading Completed");
		}

		private void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
		{
			
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
		}
}
