using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;


namespace LayoutBugs
{
    public sealed partial class Page : Microsoft.UI.Xaml.Controls.Page, INotifyPropertyChanged
    {
        #region INotifyProperyChanged support
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(info));
        }
        #endregion


        #region NumberOfItems Property
        public int numberOfItems = 10;
        public int NumberOfItems { 
            get => numberOfItems;
            set
            {
                if (value == numberOfItems) return;
                numberOfItems = value;
                BuildItems();
            }
        }
        #endregion

        #region Enforce UpdateLayout()
        private bool enforceListViewsUpdateLayout = false;
        public bool EnforceListViewsUpdateLayout
        {
            get => enforceListViewsUpdateLayout;
            set
            {
                if (value == enforceListViewsUpdateLayout) return;
                enforceListViewsUpdateLayout = value;
                NotifyPropertyChanged("EnforceListViewsUpdateLayout");
            }
        }

        private void UpdateListViewsLayoutsIfEnforced()
        {
            if (EnforceListViewsUpdateLayout == false) return;

            VerticalListView.UpdateLayout();
            HorizontalListView.UpdateLayout();
        }
        #endregion

        #region MaxLinesPerItem Property
        public int maxLinesPerItem = 10;
        public int MaxLinesPerItem
        {
            get => maxLinesPerItem;
            set
            {
                if (value == maxLinesPerItem) return;
                maxLinesPerItem = value;
                BuildItems();
            }
        }
        #endregion

        #region Items Property
        private ObservableCollection<Item> items = new();
        public ObservableCollection<Item> Items
        {
            get => items;
            set
            {
                if (value == items) return;
                items = value;
                NotifyPropertyChanged("Items");
                UpdateListViewsLayoutsIfEnforced();
            }
        }

        private int lastItemNumber = -1;
        public int LastItemNumber
        {
            get => lastItemNumber;
            set
            {
                if (value == lastItemNumber) return;
                lastItemNumber = value;
                NotifyPropertyChanged("LastItemNumber");
            }
        }

        private void BuildItems()
        {
            Random rndGen = new();

            Items.Clear();
            LastItemNumber = -1;

            for (int i = 0; i < NumberOfItems; i++)
            {
                Items.Add(new(++LastItemNumber, rndGen.Next(MaxLinesPerItem)));
            }

            UpdateListViewsLayoutsIfEnforced();
        }
        #endregion

        #region DataTemplateText Property
        public string DataTemplateText {
            get => DataTemplateByTextSelector.Text;
            set
            {
                if (value == DataTemplateByTextSelector.Text) return;

                DataTemplateByTextSelector.Text = value;

                // change the Items collection to force an update of the ItemRepeater/ListView
                // whose TemplateSelector will then use this new value to select the DataTemplate

                var items = Items.ToList();

                Items = new ObservableCollection<Item>(items);

                /*
                    Items.Clear();
                    foreach (var item in items) Items.Add(item);
                    UpdateListViewsLayoutsIfEnforced();
                */

                UpdateListViewsLayoutsIfEnforced();
            }
        }
        #endregion

        #region Randomized Item Actions
        private int randomItemsActions=20;
        public int RandomItemsActions
        {
            get => randomItemsActions;
            set
            {
                if (value == randomItemsActions) return;
                randomItemsActions = value;
                NotifyPropertyChanged("RandomItemsActions");
            }
        }

        private void OnRandomItemsActionsClick(object sender, RoutedEventArgs args)
        {
            Random rndGen = new();

            for(int i = 0; i < RandomItemsActions; i++)
            {
                switch(rndGen.Next(3))
                {
                    case 0:
                        if (Items.Count > 0) Items.RemoveAt(rndGen.Next(Items.Count));
                        break;
                    case 1:
                        Items.Insert(rndGen.Next(Items.Count), new Item(++LastItemNumber, rndGen.Next(MaxLinesPerItem)));
                        break;
                    case 2:
                        if (Items.Count > 0) Items.Move(rndGen.Next(Items.Count), rndGen.Next(Items.Count));
                        break;
                }
            }

            UpdateListViewsLayoutsIfEnforced();
        }
        #endregion

        #region ScrollPositions Reset
        private void OnResetScrollPositions(object sender, RoutedEventArgs args)
        {
            UpdateListViewsLayoutsIfEnforced();

            VerticalItemsRepeatersScrollViewer.ChangeView(0,0, null);
            if (Items.Count > 0) VerticalListView.ScrollIntoView(Items[0]);
            HorizontalItemsRepeatersScrollViewer.ChangeView(0, 0, null);
            if (Items.Count > 0) HorizontalListView.ScrollIntoView(Items[0]);
        }
        #endregion

        public Page()
        {
            this.InitializeComponent();

            BuildItems();
        }
    }
}
