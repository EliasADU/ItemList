using Assignment3ItemListUWP.Dialogs;
using Assignment3ItemListUWP.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using Newtonsoft.Json;
using Windows.Storage;

namespace Assignment3ItemListUWP.ViewModels
{
    class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Item> ItemListToDisplay { get; set; }
        public string SearchText { get; set; }
        public Item SelectedItem { get; set; }


        private bool _modificationsAllowed;
        public bool modificationsAllowed
        {
            get
            {
                return _modificationsAllowed;
            }
        }

        private bool filterON;

        private bool _ShowOutstandingOnly;
        public bool ShowOutstandingOnly
        {
            get
            {
                return _ShowOutstandingOnly;
            }
            set
            {
                _ShowOutstandingOnly = value;
                ToggleShowOnlyOutstanding(value);
            }
        }


        public MainViewModel()
        {
            ItemListToDisplay = new ObservableCollection<Item>(new List<Item>());
            filterON = false;
            _modificationsAllowed = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async void Toggle_Selected_Task_Outstanding()
        {
            //modify the task and use AddOrUpdate, then UpdateList() REGULAR

            if (SelectedItem != null)
            {
                Models.Task taskToModify = SelectedItem as Models.Task;
                if (taskToModify != null)
                {
                    taskToModify.isCompleted = !taskToModify.isCompleted;

                    var handler = new WebRequestHandler();
                    await handler.Post("http://localhost/ItemsAPI/Items/AddOrUpdate", taskToModify);

                    Update_List_From_API();
                }
            }
        }

        public void Update_List_From_API()
        {
            var handler = new WebRequestHandler();
            List<Item> items = JsonConvert.DeserializeObject<List<Item>>(handler.Get("http://localhost/ItemsAPI/Items/getall").Result, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            });

            ItemListToDisplay.Clear();

            items.ForEach(ItemListToDisplay.Add);
        }

        public void Update_List_From_List(List<Item> toUpdate)
        {
            ItemListToDisplay.Clear();
            toUpdate.ForEach(ItemListToDisplay.Add);
        }

        public async void Delete_Selected_Item()
        {
            if (SelectedItem != null)
            {
                var handler = new WebRequestHandler();
                await handler.Post("http://localhost/ItemsAPI/Items/delete", SelectedItem.id);

                Update_List_From_API();
            }
        }

        public async void Edit_Current_Item()
        {
            if (SelectedItem != null)
            {
                if (SelectedItem is Assignment3ItemListUWP.Models.Task)
                {
                    var diag = new TaskDialog(ItemListToDisplay, (Assignment3ItemListUWP.Models.Task)SelectedItem);
                    await diag.ShowAsync();
                }
                else
                {
                    var diag = new AppointmentDialog(ItemListToDisplay, (Assignment3ItemListUWP.Models.Appointment)SelectedItem);
                    await diag.ShowAsync();
                }
            }

            Update_List_From_API();
        }

        public void Sort_By_Priority()
        {
            var handler = new WebRequestHandler();
            List<Item> items = JsonConvert.DeserializeObject<List<Item>>(handler.Get("http://localhost/ItemsAPI/Items/GetSorted").Result, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            });

            ItemListToDisplay.Clear();

            items.ForEach(ItemListToDisplay.Add);
        }

        //filtering mess
        private void ToggleShowOnlyOutstanding(bool t)
        {
            //run GetAllOutstanding, and use result to call update list

            //to "restore", just call regular get list

            if (!filterON)
            {
                if (t)
                {
                    var handler = new WebRequestHandler();
                    _modificationsAllowed = false;
                    List<Item> intermediary = JsonConvert.DeserializeObject<List<Item>>(
                        handler.Get("http://localhost/ItemsAPI/Items/GetAllOutstanding").Result, 
                        new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All
                    });

                    Update_List_From_List(intermediary);
                }
                else
                {
                    Update_List_From_API();
                    _modificationsAllowed = true;
                }
            }
        }

        public void Filter_For_Search_String()
        {
            //call getfromsearch and use updatelist override if showing list
            //else call the regular getlist

            if (SearchText != "" && !ShowOutstandingOnly)
            {
                filterON = true;
                _modificationsAllowed = false;
                ItemListToDisplay.Clear();


                var handler = new WebRequestHandler();
                _modificationsAllowed = false;
                List<Item> intermediary = JsonConvert.DeserializeObject<List<Item>>(
                    handler.Post("http://localhost/ItemsAPI/Items/GetFromSearch", SearchText).Result,
                    new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All
                    });

                Update_List_From_List(intermediary);
            }
            else
            {
                if (filterON)
                {
                    Update_List_From_API();
                }
                filterON = false;
                _modificationsAllowed = true;
            }
        }
    }
}
