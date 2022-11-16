using System;
using System.Collections.Generic;
using System.Linq;
using SPLAT.MVVM.ViewModel;
using SPLAT.MVVM.Models;
using System.Text;
using System.Threading.Tasks;

namespace SplatTest
{
    [TestClass]
    public class TicketsViewModelTests
    {
        private string _title = "test1Title";
        private string _description = "test1Description";
        private string _user_id = "test1Description";
        private string _Name = "test1Name";
        private string _created = DateTime.Today.Date.ToShortDateString();

        public TicketsViewModelTests()
        {

        }

        public TicketsViewModelTests(string title, string description, string user_id, string name, string created)
        {
            _title = title;
            _description = description;
            _user_id = user_id;
            _Name = name;
            _created = created;
        }

        public string Title
        {
            get => _title; set
            {
                if (value != _title)
                {
                    _title = value;

                }
            }
        }


        public string Created
        {
            get => _created; set
            {
                if (value != _created)
                {
                    _created = value;

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

                }
            }
        }

        TicketModel newTicket = new TicketModel();
        [TestMethod]
        public void AddTicketTestCreatedDATE()
        {

            newTicket.Title = Title;
            Title = string.Empty;
            newTicket.Description = Description;
            Description = string.Empty;
            newTicket.User_id = User_id;
            User_id = string.Empty;
            newTicket.Name = Name;
            Name = string.Empty;
            newTicket.Created = DateTime.Today;

            TicketsViewModelTests test1 = new TicketsViewModelTests(_title, _description, _user_id, _Name, _created);


            Assert.AreNotEqual(newTicket.Created, test1.Created);
        }
        [TestMethod]
        public void AddTicketTestName()
        {

            newTicket.Title = Title;
            Title = string.Empty;
            newTicket.Description = Description;
            Description = string.Empty;
            newTicket.User_id = User_id;
            User_id = string.Empty;
            newTicket.Name = Name;
            Name = string.Empty;
            newTicket.Created = DateTime.Today;

            Assert.IsNotNull(newTicket.Name);

        }

        [TestMethod]

        public void EditTicketTestMod()
        {
            if (Name == null)
            {
                Assert.IsNull(newTicket.Name);
            }

        }





    }
}
