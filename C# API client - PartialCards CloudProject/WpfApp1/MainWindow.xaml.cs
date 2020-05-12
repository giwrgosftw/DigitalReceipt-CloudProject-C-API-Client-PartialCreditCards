using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
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


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Regex rx = new Regex("\\*+([0-9]{4})"); // text contains the symbol * and last 4 digits only
            Regex rx2 = new Regex("x+([0-9]{4})");  // text contains the letter x and last 4 digits only
            var res = rx.Match(textbox.Text);
            var res2 = rx2.Match(textbox.Text);
            if (res.Success)
            {
                // take the part of the whole text which matches with the regural expression and create a new string
                string str0 = res.ToString();

                // create a new string with the last 4 digits of the string str0
                string tralingCardDigits = str0.Substring(str0.Length - 4);

                // complete the url by adding the last 4 digits
                string url = "http://localhost:5000/customer/" + tralingCardDigits;
                
                // take the data from the url and show them to the textblock
                textblock.Text = Get(url);
            }
            else if (res2.Success)
            {
                string str0 = res2.ToString();
                string tralingCardDigits = str0.Substring(str0.Length - 4);
                string url = "http://localhost:5000/customer/" + tralingCardDigits;
                textblock.Text = Get(url);
            }
            else
            {
                textblock.Text = "Wrong details, please try again";
            }

        }

        public string Get(string uri)
        {   
            //Initializes a new WebRequest instance for the specified URI scheme
            var request = WebRequest.Create(uri);
            
            try
            {
                // Send the 'WebRequest' and wait for response
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())

                // Obtain a 'Stream' object associated with the response object
                using (Stream stream = response.GetResponseStream())

                // Create an instance of StreamReader to read the data from the server
                // The using statement also returns all these data
                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
            catch(Exception e)
            {
               return textblock.Text = "The server is not responding!" + System.Environment.NewLine + "(maybe the customer does not exist or the server is down)";
            }

            
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }

    internal class HttpWebRequest
    {
        public DecompressionMethods AutomaticDecompression { get; internal set; }
    }
}
