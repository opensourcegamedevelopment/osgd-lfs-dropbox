using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dropbox.Api;

namespace LargeFileSync
{
    public partial class Form1 : Form
    {

        /// <summary>
        /// The Dropbox client.
        /// </summary>
        public static DropboxClient Client;


        /// <summary>
        /// The Dropbox app client.
        /// </summary>
        public static DropboxAppClient AppClient;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            AppClient = new DropboxAppClient("", "");
            //https://www.dropbox.com/sh/ayaopiheo87rup5/AABZPFfII4Lv2pC9fF1Jwk7ga

            DropboxClientConfig config = new DropboxClientConfig();

            Client = new DropboxClient("pajtJkEkNeAAAAAAAAAyPEybt-su8mBf-QVpzFPY3-wL30g7e2f1HEKoyjFfWVaJ");
            
        }

        async Task ListFiles(DropboxClient Client)
        {
            //Dropbox.Api.Sharing.ListFoldersArgs listFoldersArgs = new Dropbox.Api.Sharing.ListFoldersArgs();
            //var list = await Client.Sharing.ListFoldersAsync(listFoldersArgs);

            //Console.WriteLine(list);
            
                var list = await Client.Files.ListFolderAsync(String.Empty);

                Console.WriteLine(list.ToString());
            // show folders then files
            foreach (var item in list.Entries.Where(i => i.IsFolder))
            {
                Console.WriteLine("D  {0}/", item.Name);
            }

            foreach (var item in list.Entries.Where(i => i.IsFile))
            {
                Console.WriteLine("F{0,8} {1}", item.AsFile.Size, item.Name);
            }
        }

        async Task ListFiles2(DropboxClient Client)
        {
            //Dropbox.Api.Sharing.ListFoldersArgs listFoldersArgs = new Dropbox.Api.Sharing.ListFoldersArgs();
            //var list = await Client.Sharing.ListFoldersAsync(listFoldersArgs);

            //Console.WriteLine(list);

            string url = "https://www.dropbox.com/sh/ayaopiheo87rup5/AABZPFfII4Lv2pC9fF1Jwk7ga?dl=0";

            try
            {

                var list = await Client.Sharing.GetSharedLinkMetadataAsync(url);
                Dropbox.Api.Sharing.FolderLinkMetadata folderLink = list.AsFolder;

                // Dropbox.Api.Sharing.GetSharedLinksResult result = await Client.Sharing.GetSharedLinksAsync(folderLink.Url);

                //  Dropbox.Api.Files.ListFolderResult result2 = await Client.Files.ListFolderAsync(folderLink.Url);

                Console.WriteLine(folderLink.Url);
                Console.WriteLine(folderLink.Id);
                Console.WriteLine(folderLink.Name);

                //Dropbox.Api.Sharing.SharedFolderMetadata data =  Client.Sharing.GetFolderMetadataAsync(folderLink.Id).Result;

                //Console.WriteLine(data.LinkMetadata.Url);

                //foreach (var item in result2.Entries)
                //{
                //    Console.WriteLine(item.Name);
                //}


            }
            catch (ApiException<Dropbox.Api.Sharing.GetSharedLinkFileError> err)
            {
                Console.WriteLine(err);
            }
            catch (ApiException<Dropbox.Api.Sharing.GetSharedLinksResult> err)
            {
                Console.WriteLine(err);
            }
            catch (ApiException<Dropbox.Api.Files.ListFolderResult> err)
            {
                Console.WriteLine(err);
            }

            //Dropbox.Api.Sharing.GetSharedLinksResult result = await Client.Sharing.GetSharedLinksAsync();

            //foreach(var item in result.Links)
            //{
            //    Console.WriteLine(item.Url.ToString());
            //}

            // show folders then files
            //foreach (var item in list.Entries.Where(i => i.IsFolder))
            //{
            //    Console.WriteLine("D  {0}/", item.Name);
            //}

            //foreach (var item in list.Entries.Where(i => i.IsFile))
            //{
            //    Console.WriteLine("F{0,8} {1}", item.AsFile.Size, item.Name);
            //}
        }

        private void btnListFiles_Click(object sender, EventArgs e)
        {
           Task t = ListFiles2(Client);
        }
    }
}
