using SPLAT.Core;
using SPLAT.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using FireSharp.Config;
using Firebase.Database;
using Firebase.Database.Query;
using System.Net.Http;

namespace SPLAT.MVVM.ViewModel
{
    public class TicketsViewModel : ObservableObject
    {
        private string _title;
        private string _description;
        private string _user_id;
        private string _Name;
        private DateTime _created;
        private TicketModel _selectedTicket;
        private List<TicketModel> _tickets = new List<TicketModel>();
        private List<string> _strTickets = new List<string>();
        private ObservableCollection<string> _stringCollection = new ObservableCollection<string>();
        private ICommand _addTicketCommand;
        private ICommand _deleteTicketCommand;
        private ICommand _editTicketCommand;
        private ICommand _updateTicketCommand;
        private ICommand _showTicketCommand;
        private ObservableCollection<TicketModel> _dbTickets = new ObservableCollection<TicketModel>();
        private delegate ObservableCollection<string> TicketDelegate(IReadOnlyCollection<FirebaseObject<TicketModel>> dbTickets);
        FirebaseConfig ifc = new FirebaseConfig()
        {
            AuthSecret = "uJWNBumr3wGD2xBWuJylCByI238krzBZZoHm1kXc",
            BasePath = "https://splattest-68d44-default-rtdb.firebaseio.com/"
        };
        FirebaseClient _fbclient;


        public string Title
        {
            get => _title; set
            {
                if (value != _title)
                {
                    _title = value;
                    OnPropertyChanged("Title");
                }
            }
        }


        public DateTime Created
        {
            get => _created; set
            {
                if (value != _created)
                {
                    _created = value;
                    OnPropertyChanged("Created");
                }
            }

        }


        public string Description
        {
            get => _description; set
            {
                if (value != _description)
                {
                    _description = value;
                    OnPropertyChanged("Description");
                }
            }
        }
        public string User_id
        {
            get => _user_id; set
            {
                if (value != _user_id)
                {
                    _user_id = value;
                    OnPropertyChanged("User_id");
                }
            }
        }
        public string Name
        {
            get => _Name; set
            {
                if (value != _Name)
                {
                    _Name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        public TicketModel SelectedTicket
        {
            get => _selectedTicket; set
            {
                if (value != _selectedTicket)
                {
                    _selectedTicket = value;
                    OnPropertyChanged("SelectedTicket");
                }
            }
        }
        public List<TicketModel> Tickets
        {
            get { return _tickets; }
            set
            {
                if (value != _tickets)
                {
                    _tickets = value;
                    OnPropertyChanged("Tickets");
                }
            }
        }

        public List<string> strTickets
        {
            get { return _strTickets; }
            set
            {
                if (value != _strTickets)
                {
                    _strTickets = value;
                    OnPropertyChanged("strUsers");
                }
            }
        }

        public ObservableCollection<TicketModel> DbTickets
        {
            get { return _dbTickets; }
            set
            {
                if (value != _dbTickets)
                {
                    _dbTickets = value;
                    OnPropertyChanged("DbTickets");
                }
            }
        }

        public ObservableCollection<string> StrCollection
        {
            get { return _stringCollection; }
            set
            {
                if (value != _stringCollection)
                {
                    _stringCollection = value;
                    OnPropertyChanged("StrCollection");
                }
            }
        }

        public ICommand AddTicketCommand
        {
            get
            {
                if (_addTicketCommand == null)
                {
                    _addTicketCommand = new RelayCommand(
                        param => AddTicket());
                }
                return _addTicketCommand;
            }
        }



        public ICommand DeleteTicketCommand
        {
            get
            {
                if (_deleteTicketCommand == null)
                {
                    _deleteTicketCommand = new RelayCommand(
                        param => DeleteTicket());
                }
                return _deleteTicketCommand;
            }
        }

        public ICommand EditTicketCommand
        {
            get
            {
                if (_editTicketCommand == null)
                {
                    _editTicketCommand = new RelayCommand(
                        param => EditTicket());
                }
                return _editTicketCommand;
            }
        }

        public ICommand UpdateTicketCommand
        {
            get
            {
                if (_updateTicketCommand == null)
                {
                    _updateTicketCommand = new RelayCommand(
                        param => UpdateTicket());
                }
                return _updateTicketCommand;
            }
        }
        public ICommand ShowTicketCommand
        {
            get
            {
                if (_showTicketCommand == null)
                {
                    _showTicketCommand = new RelayCommand(
                        param => GetTickets());
                }
                return _showTicketCommand;
            }

        }

        public FirebaseClient FBClient
        {
            get { return _fbclient; }
        }

        

        public TicketsViewModel()
        {
            {
                try
                {
                    _fbclient = new FirebaseClient("https://splattest-68d44-default-rtdb.firebaseio.com/");
                    GetTickets();
                }
                catch
                {
                    MessageBox.Show("No Internet or Connection Problem");
                }
            }
        }




        private async void AddTicket()
        {
          
             TicketModel newTicket = new TicketModel();
             newTicket.Title = Title;
             Title = string.Empty;
             newTicket.Description = Description;
             Description = string.Empty;
             newTicket.User_id = User_id;
             User_id = string.Empty;
             newTicket.Name = Name;
             Name = string.Empty;
             newTicket.Created = DateTime.Today;


            await FBClient.Child("Tickets").PostAsync(newTicket,false);
            strTickets.Add("one more");
            GetTickets();



         /*    await FBClient
                 .Child("Tickets")
                 .PostAsync(newTicket, false);
             strTickets.Add("one more");
             GetTickets();
           */ 
        }



        DateTime selectedTicketEditDate;
        String selectedKey;
        public async void EditTicket()
        {

            if (SelectedTicket != null && SelectedTicket.Key != null)
            {
                Title = SelectedTicket.Title;
                Description = SelectedTicket.Description;
                User_id = SelectedTicket.User_id;
                Name = SelectedTicket.Name;
                selectedKey = SelectedTicket.Key;
                selectedTicketEditDate = SelectedTicket.Created;
                }

        }
        private async void UpdateTicket()
        {


            TicketModel existTicket = new TicketModel();
            existTicket.Title = Title;
            Title = string.Empty;
            existTicket.Description = Description;
            Description = string.Empty;
            existTicket.User_id = User_id;
            User_id = string.Empty;
            existTicket.Name = Name;
            Name = string.Empty;
            existTicket.Created = selectedTicketEditDate;

            await FBClient
                 .Child("Tickets").Child(selectedKey)
                 .DeleteAsync();
            await FBClient.Child("Tickets").Child(selectedKey).PutAsync(existTicket);
            GetTickets();            
     
        }



            private async void DeleteTicket()
        {
            if (SelectedTicket != null && SelectedTicket.Key != null)
            {
                await FBClient.Child("Tickets").Child(SelectedTicket.Key).DeleteAsync();
                GetTickets();
            }
        }
        private async void GetTickets()
        {
            try
            {
                var temp = await FBClient.Child("Tickets").OrderByKey().OnceAsync<TicketModel>();
                await App.Current.Dispatcher.BeginInvoke((Action)delegate () { DbTickets.Clear(); });

                foreach (var e in temp)
                {
                    await App.Current.Dispatcher.BeginInvoke((Action)delegate ()
                    {
                        DbTickets.Add(new TicketModel
                        {
                            Key= e.Key,
                            Created = e.Object.Created,
                            Title = e.Object.Title,
                            Description = e.Object.Description,
                            User_id = e.Object.User_id,
                            Name = e.Object.Name                            
                        });
                    });
                }
            }
            catch (Exception)
            { }
        }
        public void Window_ContentRendered(object sender, EventArgs e)
        {

        }
    }
}


/* public static async Task<QuestionModel> LoadStackOverflowQuestion()
 {

     string currentTags = QuestionModel.inputTags;
     string URL = $"https://api.stackexchange.com/2.3/questions?order=desc&sort={currentTags}activity&tagged=&site=stackoverflow";

     using (HttpResponseMessage response = await APIHelper.APIClient.GetAsync(URL))
     {
         if (response.IsSuccessStatusCode)
         {
             QuestionModel questions = await response.Content.ReadAsStreamAsync<Qust

             re
             return questions;
         }
         else
         {
             throw new Exception(response.ReasonPhrase);
         }
     }
 }*/






/*
 * private async Task GetTodos()
        {
            using (var httpClient = new HttpClient())
            {

                var todosJson = await httpClient.GetStringAsync("https://jsonplaceholder.typicode.com/todos");
                var todoItems = JsonConvert.DeserializeObject<Todo[]>(todosJson);
                foreach (var item in todoItems)
                {
                    Todo.Add(item);
                }
            }
        }

 * 
 * */