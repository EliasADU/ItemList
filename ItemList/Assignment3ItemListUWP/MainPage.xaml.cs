using Assignment3ItemListUWP.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Diagnostics;
using Assignment3ItemListUWP.Dialogs;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using Assignment3ItemListUWP.Models;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Assignment3ItemListUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        MainViewModel myViewModel;

        public MainPage()
        {
            InitializeComponent();
            myViewModel = new MainViewModel();
            DataContext = myViewModel;

            var handler = new WebRequestHandler();
            List<Item> items = JsonConvert.DeserializeObject<List<Item>>(handler.Get("http://localhost/ItemsAPI/Items/getall").Result, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            });
            var context = DataContext as MainViewModel;

            items.ForEach(context.ItemListToDisplay.Add);
        }

        private async void AddTask_Click(object sender, RoutedEventArgs e)
        {
            if (myViewModel.modificationsAllowed)
            {
                var diag = new TaskDialog((DataContext as MainViewModel).ItemListToDisplay);
                await diag.ShowAsync();
                myViewModel.Update_List_From_API();
            }
        }
        private async void AddAppointment_Click(object sender, RoutedEventArgs e)
        {
            if (myViewModel.modificationsAllowed)
            {
                var diag = new AppointmentDialog((DataContext as MainViewModel).ItemListToDisplay);
                await diag.ShowAsync();
                myViewModel.Update_List_From_API();
            }

        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (myViewModel.modificationsAllowed)
            {
                myViewModel.Delete_Selected_Item();
            }
        }
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (myViewModel.modificationsAllowed)
            {
                (DataContext as MainViewModel).Edit_Current_Item();
            }
        }
        private void MarkOutstanding_Click(object sender, RoutedEventArgs e)
        {
            myViewModel.Toggle_Selected_Task_Outstanding();
        }
        private void SortByPriority_Click(object sender, RoutedEventArgs e)
        {
            if (myViewModel.modificationsAllowed)
            {
                myViewModel.Sort_By_Priority();
            }
        }
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            myViewModel.Filter_For_Search_String();
        }
        private void ShowOutstanding_Check(object sender, RoutedEventArgs e)
        {
            if (myViewModel.modificationsAllowed)
            {
                myViewModel.ShowOutstandingOnly = true;
            }
        }
        private void ShowOutstanding_Uncheck(object sender, RoutedEventArgs e)
        {
            myViewModel.ShowOutstandingOnly = false;
        }
    }
}
