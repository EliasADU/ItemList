using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Newtonsoft.Json;
namespace Assignment3ItemListUWP.Models
{
    [JsonConverter(typeof(ProductJsonConverter))]
    public class Item : DependencyObject
    {
        public static readonly DependencyProperty nameProperty = DependencyProperty.Register(
            "Name",
            typeof(string),
            typeof(Item),
            new PropertyMetadata(null));
        public static readonly DependencyProperty descriptionProperty = DependencyProperty.Register(
            "Description",
            typeof(string),
            typeof(Item),
            new PropertyMetadata(null));
        public static readonly DependencyProperty priorityProperty = DependencyProperty.Register(
            "Priority",
            typeof(string),
            typeof(Item),
            new PropertyMetadata(null));

        public int id { get; set; }
        public string name
        {
            get
            {
                return (string)GetValue(nameProperty);
            }
            set
            {
                SetValue(nameProperty, value);
            }
        }
        public string description
        {
            get
            {
                return (string)GetValue(descriptionProperty);
            }
            set
            {
                SetValue(descriptionProperty, value);
            }
        }
        public string priority
        {
            get
            {
                return (string)GetValue(priorityProperty);
            }
            set
            {
                SetValue(priorityProperty, value);
            }
        }
    }
}
