using System;
using System.Net;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace CurrencyConverter
{
    public partial class Form1 : Form
    {
        private const string API_KEY = "e84200f83114407da71f75603c844683"; 
        private const string API_URL = "https://openexchangerates.org/api/latest.json";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Tambahkan pilihan mata uang pada comboBox "From"
            fromComboBox.Items.Add("USD");
            fromComboBox.Items.Add("EUR");
            fromComboBox.Items.Add("GBP");
            fromComboBox.Items.Add("JPY");
            fromComboBox.Items.Add("SGD");
            fromComboBox.Items.Add("IDR");

            // Tambahkan pilihan mata uang pada comboBox "To"
            toComboBox.Items.Add("USD");
            toComboBox.Items.Add("EUR");
            toComboBox.Items.Add("GBP");
            toComboBox.Items.Add("JPY");
            toComboBox.Items.Add("SGD");
            toComboBox.Items.Add("IDR");

            // Set default mata uang pada comboBox "From" dan "To"
            fromComboBox.SelectedItem = "USD";
            toComboBox.SelectedItem = "IDR";
        }

        private void convertButton_Click(object sender, EventArgs e)
        {
            string fromCurrency = fromComboBox.SelectedItem.ToString();
            string toCurrency = toComboBox.SelectedItem.ToString();
            double amount = double.Parse(amountTextBox.Text);

            string url = $"{API_URL}?app_id={API_KEY}&base={fromCurrency}&symbols={toCurrency}";

            using (var client = new WebClient())
            {
                try
                {
                    string response = client.DownloadString(url);
                    JObject json = JObject.Parse(response);
                    double exchangeRate = (double)json["rates"][toCurrency];
                    double result = amount * exchangeRate;

                    resultTextBox.Text = result.ToString("0.00");
                }
                catch (WebException ex)
                {
                    MessageBox.Show("Failed to connect to the API server. Please check your internet connection and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error has occurred while converting the currency. Please try again later.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }
    }
}
