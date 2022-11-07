using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPLAT.Core;

namespace SPLAT.MVVM.Models
{
    public class TicketModel: ObservableObject
    {
       
            private string _key;
            private string _title;
            private string _description;
            private string _user_id;
            private string _Name;
            private DateTime _created;

            public string Key
            {
                get => _key; set
                {
                    if (value != _key)
                    {
                        _key = value;
                        OnPropertyChanged("Key");
                    }
                }
            }

            public DateTime Created
             { get => _created; set
            {
                if (value != _created)
                {
                    _created = value;
                    OnPropertyChanged("Created");
                }
            }
        
        
        
        
        
        }


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
            public override string ToString()
            {
                return this.Title + " " + this.Description + " " + this.Name + " " + this.User_id;
            }

        }
    }

