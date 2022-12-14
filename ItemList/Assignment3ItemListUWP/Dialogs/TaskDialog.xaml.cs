using Assignment3ItemListUWP.Models;
using System;
using System.Collections.Generic;
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
using System.Diagnostics;
using Assignment3ItemListUWP.ViewModels;
using System.Collections.ObjectModel;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Assignment3ItemListUWP.Dialogs
{
    public sealed partial class TaskDialog : ContentDialog
    {
        private IList<Item> itemList;

        bool itemIsNew;

        public TaskDialog(IList<Item> myItemList, Task taskToEdit = null)
        {
            InitializeComponent();

            if(taskToEdit != null)
            {
                DataContext = taskToEdit;
                itemIsNew = false;
            }
            else
            {
                DataContext = new Task();
                itemIsNew = true;
            }


            itemList = myItemList;
            if(itemList == null)
            {
                itemList = new List<Item>();
            }
        }

        private async void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            var handler = new WebRequestHandler();
            await handler.Post("http://localhost/ItemsAPI/Items/AddOrUpdate", DataContext as Task);

            ////we STILL run this to force update the local list
            //if (itemIsNew)
            //{
            //    itemList.Add(DataContext as Task);
            //}
            //else
            //{
            //    //this way, the observablecollection is made aware that it is, in fact, being changed!
            //    //this feels hacky, better way to do it?
            //    itemList[itemList.IndexOf(DataContext as Task)] = DataContext as Task; 
            //}
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

    }
}
