using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using S20L.lib;
namespace S20L.wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            book.Text = "PhoneBook appears here :\n";
        }
        public void Add(object sender,RoutedEventArgs args)
        {
            string fname = user_fname.Text;
            string lname = user_lname.Text;
            string number = user_number.Text;
            string email = user_email.Text;
            string state = user_state.Text;
            string relationship = user_relationship.Text;
            if(!checkfields(sender,args))
                return;
            HttpClient client = new HttpClient();
            Person newP = new Person(fname,lname,number,email,state,relationship);
            client.PostAsJsonAsync("http://localhost:5268/addcontact", newP);
            MessageBox.Show("added");
            Load(sender,args);
        }
        public void Delete(object sender,RoutedEventArgs args)
        {
            string number = user_number.Text;
            if(string.IsNullOrWhiteSpace(number))
            {
                MessageBox.Show("enter the number");
                return;
            }
            HttpClient client = new HttpClient();
            string xy = client.GetStringAsync(new Uri($"http://localhost:5268/delete/{number}")).Result;
            if(xy=="false")
                MessageBox.Show("not found");
            else
                MessageBox.Show("contact deleted");
            Load(sender,args);
        }

        public void Edit(object sender,RoutedEventArgs args)
        {
            string fname = user_fname.Text;
            string lname = user_lname.Text;
            string number = user_number.Text;
            string email = user_email.Text;
            string state = user_state.Text;
            string relationship = user_relationship.Text;
            if(!checkfields(sender,args))
                return;
            HttpClient client = new HttpClient();
            Person newP = new Person(fname,lname,number,email,state,relationship);
            client.PostAsJsonAsync("http://localhost:5268/edit", newP);
            MessageBox.Show("Edited");
            Load(sender,args);
        }
        private void Find(object sender, RoutedEventArgs args)
        {
            
            string number = user_number.Text;
            string firstName = user_fname.Text;
            string lastName = user_lname.Text;
            if (string.IsNullOrWhiteSpace(user_fname.Text) && 
                string.IsNullOrWhiteSpace(user_number.Text))
            {
                MessageBox.Show("No Name or Number to find");
                return;
                
            }
        
            
            else if (!string.IsNullOrWhiteSpace(firstName) || !string.IsNullOrWhiteSpace(lastName))
            {
                HttpClient client = new HttpClient();
                string xy = client.GetStringAsync(new Uri($"http://localhost:5268/contactname/{firstName}/name")).Result;
                string[] xu = xy.Split(" ");
                if(xu[xu.Length-1]=="!")
                {   
                    MessageBox.Show($"{xy}");
                    return;
                }
                var xy1 = xy.Split(':');
                var xy2 = xy1[1];
                var xy3 = xy2.Split(",");
                user_fname.Text = xy3[0];
                user_lname.Text = xy3[1];
                string x = client.GetStringAsync(new Uri($"http://localhost:5268/contactname/{firstName}/email")).Result;
                var y = x.Split(':');
                user_email.Text = y[1];
                string x1 = client.GetStringAsync(new Uri($"http://localhost:5268/contactname/{firstName}/number")).Result;
                string[] y1 = x1.Split(':');
                user_number.Text = y1[1];
                string x2 = client.GetStringAsync(new Uri($"http://localhost:5268/contactname/{firstName}/email")).Result;
                var y2 = x2.Split(':');
                user_email.Text = y2[1];
                string x3 = client.GetStringAsync(new Uri($"http://localhost:5268/contactname/{firstName}/state")).Result;
                string[] y3 = x3.Split(':');
                
                user_state.Text =y3[1];
                string x4 = client.GetStringAsync(new Uri($"http://localhost:5268/contactname/{firstName}/relationship")).Result;
                string[] y4 = x4.Split(':');
                user_relationship.Text =y4[1];
    

            }
            else if(string.IsNullOrWhiteSpace(firstName) && string.IsNullOrWhiteSpace(lastName) && !string.IsNullOrWhiteSpace(number))
            {
                HttpClient client = new HttpClient();
                string xy = client.GetStringAsync(new Uri($"http://localhost:5268/contactnumber/{number}/name")).Result;
                string[] xu = xy.Split(" ");                
                if(xu[xu.Length-1]=="!")
                {   
                    MessageBox.Show($"{xy}");
                    return;
                }
                string x = client.GetStringAsync(new Uri($"http://localhost:5268/contactnumber/{number}/name")).Result;
                string[] y = x.Split(':');
                var yy = y[1];
                var yy1 = yy.Split(',');
                user_fname.Text = yy1[0];
                user_lname.Text = yy1[1];
                string x1 = client.GetStringAsync(new Uri($"http://localhost:5268/contactnumber/{number}/email")).Result;
                var y1 = x1.Split(':');
                user_email.Text = y1[1];
                string x3 = client.GetStringAsync(new Uri($"http://localhost:5268/contactnumber/{number}/state")).Result;
                string[] y3 = x3.Split(':');
                user_state.Text = y3[1];
                string x4 = client.GetStringAsync(new Uri($"http://localhost:5268/contactnumber/{number}/relationship")).Result;
                string[] y4 = x4.Split(':');
                user_relationship.Text = y4[1];
            }
            else
            {
                MessageBox.Show($"Contact was Not Found");
            }
        }
        public void Load(object sender,RoutedEventArgs args)
        {
            this.resetview();
            HttpClient client = new HttpClient();
            var stream = client.GetStreamAsync("http://localhost:5268/allcontacts/all").Result;
            var ps = JsonSerializer.Deserialize<List<Person>>(stream);
            if(ps.Count!=0)
            {
                foreach(var p in ps)
                book.Text += $"{p}\n";
            }
            else
            {
                MessageBox.Show("no contacts available in phonebook");
            }

        }
        public void Clear(object sender,RoutedEventArgs args)
        {
            user_fname.Text = string.Empty;
            user_lname.Text = string.Empty;
            user_number.Text = string.Empty;
            user_email.Text = string.Empty;
            user_state.Text = string.Empty;
            user_relationship.Text = string.Empty;
        }
        public bool checkfields(object sender,RoutedEventArgs args)
        {
            string fname = user_fname.Text;
            string lname = user_lname.Text;
            string number = user_number.Text;
            string email = user_email.Text;
            string state = user_state.Text;
            string relationship = user_relationship.Text;
            if(string.IsNullOrEmpty(fname) || string.IsNullOrEmpty(lname) ||string.IsNullOrEmpty(number) ||string.IsNullOrEmpty(email) ||
            string.IsNullOrEmpty(state) || string.IsNullOrEmpty(relationship)) 
            {
                MessageBox.Show("fill all the fields");
                return false;
            }
            return true;
        }
        private void resetview()
        {
            book.Text = string.Empty;
        }
    }
}
