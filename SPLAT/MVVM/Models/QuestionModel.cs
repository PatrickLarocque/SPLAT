using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SPLAT.MVVM.Models
{
    public class QuestionModel : INotifyPropertyChanged
    {

        public List<Questions> items;

        public class Questions
        {
            public string[] tags;
            public string is_answered;
            public string Link;
            public string Title;
        }

        public event PropertyChangedEventHandler PropertyChanged;


        public static string inputTags;
        public string Tags
        {
            get => inputTags;
            set
            {
                if (inputTags == value)
                    return;
                inputTags = value;
                RaisePropertyChanged();
            }
        }

        private void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
