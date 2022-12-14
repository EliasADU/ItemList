using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Newtonsoft.Json;

namespace Assignment3ItemListUWP.Models
{
    public class Task : Item
    {
        public static readonly DependencyProperty deadlineProperty = DependencyProperty.Register(
            "Deadline",
            typeof(string),
            typeof(Task),
            new PropertyMetadata(null));
        public static readonly DependencyProperty isCompletedProperty = DependencyProperty.Register(
            "isCompleted",
            typeof(bool),
            typeof(Task),
            new PropertyMetadata(null));
        public string deadline
        {
            get
            {
                return (string)GetValue(deadlineProperty);
            }
            set
            {
                SetValue(deadlineProperty, value);
            }
        }
        public bool isCompleted
        {
            get
            {
                return (bool)GetValue(isCompletedProperty);
            }
            set
            {
                SetValue(isCompletedProperty, value);
            }
        }
        public Task()
        {
        }

        public override string ToString()
        {
            string output = "Task. Name: " + name;
            output += " | PRIORITY: " + priority + '\n';
            output += '\t' + "  " + " | Description: " + description + '\n';
            output += '\t' + "  " + " | Deadline: " + deadline + '\n';
            if (isCompleted)
            {
                output += '\t' + "  " + " | COMPLETED ";
            }
            else
            {
                output += '\t' + "  " + " | INCOMPLETE ";
            }
            return output;
        }
    }
}
