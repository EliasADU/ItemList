using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Newtonsoft.Json;

namespace Assignment3ItemListUWP.Models
{
    public class Appointment : Item
    {
        public static readonly DependencyProperty startProperty = DependencyProperty.Register(
            "StartTime",
            typeof(DateTimeOffset),
            typeof(Appointment),
            new PropertyMetadata(null));
        public static readonly DependencyProperty endProperty = DependencyProperty.Register(
            "EndTime",
            typeof(DateTimeOffset),
            typeof(Appointment),
            new PropertyMetadata(null));
        public static readonly DependencyProperty attendeeToAddProperty = DependencyProperty.Register(
            "attendeeToAdd",
            typeof(string),
            typeof(Appointment),
            new PropertyMetadata(null));

        public DateTimeOffset start
        {
            get
            {
                return (DateTimeOffset)GetValue(startProperty);
            }
            set
            {
                SetValue(startProperty, value);
            }
        }

        public DateTimeOffset end
        {
            get
            {
                return (DateTimeOffset)GetValue(endProperty);
            }
            set
            {
                SetValue(endProperty, value);
            }
        }

        public string attendeeToAdd
        {
            get
            {
                return (string)GetValue(attendeeToAddProperty);
            }
            set
            {
                SetValue(attendeeToAddProperty, value);
            }
        }

        public ObservableCollection<string> attendees { get; set; }

        public Appointment()
        {
            attendees = new ObservableCollection<string>();
        }

        public override string ToString()
        {
            string output = "Appt. Name: " + name;
            output += " | PRIORITY: " + priority + '\n';
            output += '\t' + "    | Description: " + description + '\n';
            output += '\t' + "    | Start at: " + start + '\n';
            output += '\t' + "    | End at: " + end + '\n';
            output += '\t' + "    | Attendees: ";

            foreach (string att in attendees)
            {
                output +="\n\t\t  " + att;
                output += ", ";
            }
            return output;
        }


    }
}
