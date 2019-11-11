using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WeatherForecast
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            AddCity();

        }
        public void AddCity()
        {
            cmbCityName.Items.Add("İstanbul");
            cmbCityName.Items.Add("Paris");
            cmbCityName.Items.Add("London");
            cmbCityName.Items.Add("Ankara");
            cmbCityName.Items.Add("İzmir");
        }

        public class City
        {
            public int woeid { get; set; }
            public string title { get; set; }
        }
        public class Weather
        {
            public string id { get; set; }
            public double the_temp { get; set; }

        }

        private async void BtnSearch_Click(object sender, EventArgs e)
        {
            //    var client = new HttpClient();
            //    client.BaseAddress = new Uri("https://www.metaweather.com/");
            //    HttpResponseMessage response = await client.GetAsync("/api/location/");
            //    string result = await response.Content.ReadAsStringAsync();
            //    label1.Text = result;
            try
            {
                WebClient client = new WebClient();
                client.Encoding = Encoding.UTF8;

                string url = "https://www.metaweather.com/api/location/search/?query=";
                string city = cmbCityName.Text;
                string Text = client.DownloadString(url + city);
                JArray lst = JArray.Parse(Text);
                City nesne = JsonConvert.DeserializeObject<City>(lst[0].ToString());
                //label1.Text = nesne.title + "  " + nesne.woeid;

                WebClient client2 = new WebClient();
                client2.Encoding = Encoding.UTF8;
                string url2 = "https://www.metaweather.com/api/location/";
                string id = nesne.woeid.ToString();
                int year = date.Value.Year;
                int mount = date.Value.Month;
                int day = date.Value.Day;
                string url3 = url2 + id + "/" + year.ToString() + "/" + mount.ToString() + "/" + day.ToString();
                string Text2 = client2.DownloadString(url3);
                JArray lst2 = JArray.Parse(Text2);
                Weather nesne2 = JsonConvert.DeserializeObject<Weather>(lst2[0].ToString());
                //label2.Text = nesne2.id + " " + nesne2.the_temp;

                dataGridViewList.ReadOnly = true;
                
                dataGridViewList.DataSource = lst2;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
               
            }
        }
    }
}
