using Assignment3ItemListUWP.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Assignment3ItemListUWP.Dialogs
{
    public sealed partial class AppointmentDialog : ContentDialog
    {
        private IList<Item> itemList;

        bool itemIsNew;

        public AppointmentDialog(IList<Item> myItemList, Appointment appointmentToEdit = null)
        {
            InitializeComponent();
            if (appointmentToEdit != null)
            {
                DataContext = appointmentToEdit;
                itemIsNew = false;
            }
            else
            {
                DataContext = new Appointment();
                itemIsNew = true;
            }
            itemList = myItemList;
            if (itemList == null)
            {
                itemList = new List<Item>();
            }
        }

        private async void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            var handler = new WebRequestHandler();
            await handler.Post("http://localhost/ItemsAPI/Items/AddOrUpdate", DataContext as Appointment);

            //if (itemIsNew)
            //{
            //    itemList.Add(DataContext as Appointment);
            //}
            //else
            //{
            //    itemList[itemList.IndexOf(DataContext as Appointment)] = DataContext as Appointment;
            //}
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        public void AddCurrentAttendeeToMyList(object sender, RoutedEventArgs e)
        {
            (DataContext as Appointment).attendees.Add((DataContext as Appointment).attendeeToAdd);
            (DataContext as Appointment).attendeeToAdd = "";
        }
    }
}
