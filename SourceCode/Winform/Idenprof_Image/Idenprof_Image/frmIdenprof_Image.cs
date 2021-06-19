using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using RestSharp;


namespace Idenprof_Image
{
    public partial class frm_Idenprof_Image : Form
    {
        public frm_Idenprof_Image()
        {
            InitializeComponent();
        }
        string path;

        public WebClient CreateWebClient()
        {
            var mClient = new WebClient() { Encoding = Encoding.UTF8 };
            mClient.Headers.Add("Content-Type", "multipart/form-data");
            mClient.Headers.Add("Accept", "*/*");
            return mClient;
        }

        private void btnAddPicture_Click(object sender, EventArgs e)
        {
            dataGridView.Rows.Clear();
            OpenFileDialog opnfd = new OpenFileDialog();
            //To where your opendialog box get starting location. My initial directory location is desktop.
            opnfd.InitialDirectory = "C://Desktop";
            //Your opendialog box title name.
            opnfd.Title = "Select image to be upload.";
            //which type image format you want to upload in database. just add them.
            opnfd.Filter = "Image Only(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";
            //FilterIndex property represents the index of the filter currently selected in the file dialog box.
            opnfd.FilterIndex = 1;
            try
            {
                if (opnfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (opnfd.CheckFileExists)
                    {
                        path = System.IO.Path.GetFullPath(opnfd.FileName);
                        pictureBox.Image = new Bitmap(opnfd.FileName);
                        pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                        //string nameImage = opnfd.FileName.Substring(opnfd.FileName.LastIndexOf('\\') + 1);
                    }
                }
                //else
                //{
                //    MessageBox.Show("Please Upload image.");
                //}
            }
            catch (Exception ex)
            {
                //it will give if file is already exits..5 
                MessageBox.Show(ex.Message);
            }

        }

        void get_data(string response)
        {
            dataGridView.Rows.Clear();
            if (response == "" | response == null)
                MessageBox.Show("Please check API server.");
            else
            {
                try
                {
                    JArray fetch = JArray.Parse(response);
                    if (fetch.Count() > 0)
                    {
                        for (int i = 0; fetch.Count() > i; i++)
                        {
                            int n = dataGridView.Rows.Add();
                            dataGridView.Rows[n].Cells[0].Value = fetch[i].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    //it will give if file is already exits..5 
                    MessageBox.Show(ex.Message);
                }
            }
        }

        void process(string path, int count)
        {
            var client = new RestClient("http://127.0.0.1:5000/file-upload");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddFile("file", path);
            request.AddParameter("result_count", count);
            IRestResponse response = client.Execute(request);
            get_data(response.Content);
        }

        private void btnDiagnostic_Click(object sender, EventArgs e)
        {
            if (path == "" || path == null)
                MessageBox.Show("Please Upload image.");
            else if (textBoxCount.Text == null || textBoxCount.Text == "" || Int32.Parse(textBoxCount.Text) <= 0 || Int32.Parse(textBoxCount.Text) > 10)
                MessageBox.Show("Please modify or edit count.");
            else
            {
                try
                {
                    process(path, Int32.Parse(textBoxCount.Text));
                    dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                }
                catch (Exception ex)
                {
                    //it will give if file is already exits..5 
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
