using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CAS.dekstop.Models;
using CAS.desktop.ViewModels;

namespace CAS.dekstop.Views
{
    public partial class DragAndDropTablePage : UserControl
    {
        private DragAndDropTableViewModel _dragAndDropTableViewModel;

        private ObservableCollection<Position> ToDoItems = new();
        private ObservableCollection<Position> InProgressItems = new();
        private ObservableCollection<Position> DoneItems = new();

        //private bool _isDragging = false;

        public DragAndDropTablePage()
        {
            InitializeComponent();
            _dragAndDropTableViewModel = new DragAndDropTableViewModel();
            DataContext = _dragAndDropTableViewModel;
        }

        private async Task LoadData()
        {
            await _dragAndDropTableViewModel.UpdateView();
            foreach (var item in _dragAndDropTableViewModel.Locations)
            {
                if (item.StatusType == Models.Enums.StatusType.ToDO)
                {
                    ToDoItems.Add(item);
                }
                else if (item.StatusType == Models.Enums.StatusType.InProgress)
                {
                    InProgressItems.Add(item);
                }
                else
                {
                    DoneItems.Add(item);
                }
            }
        }

        public void UpdateTable()
        {
            ToDoListView.ItemsSource = ToDoItems;
            InProgressListView.ItemsSource = InProgressItems;
            DoneListView.ItemsSource = DoneItems;
        }

        private void ListView_PreviewDragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(Position)))
            {
                e.Effects = DragDropEffects.Move;
                e.Handled = true;
            }
        }

        private void ListView_Drop(object sender, DragEventArgs e)
        {
            var targetListView = sender as ListView;
            var draggedItem = e.Data.GetData(typeof(Position)) as Position;

            if (draggedItem != null)
            {
                RemoveItemFromOtherLists(draggedItem);

                if (targetListView == ToDoListView)
                {
                    draggedItem.StatusType = Models.Enums.StatusType.ToDO;
                    ToDoItems.Add(draggedItem);
                }
                else if (targetListView == InProgressListView)
                {
                    draggedItem.StatusType = Models.Enums.StatusType.InProgress;
                    InProgressItems.Add(draggedItem);
                }
                else if (targetListView == DoneListView)
                {
                    draggedItem.StatusType = Models.Enums.StatusType.Done;
                    DoneItems.Add(draggedItem);
                }

                UpdateBackendStatus(draggedItem);
            }
        }

        private void RemoveItemFromOtherLists(Position item)
        {
            if (ToDoItems.Contains(item))
                ToDoItems.Remove(item);
            if (InProgressItems.Contains(item))
                InProgressItems.Remove(item);
            if (DoneItems.Contains(item))
                DoneItems.Remove(item);
        }

        private async void UpdateBackendStatus(Position item)
        {
            await _dragAndDropTableViewModel.DoUpdate(item);
            MessageBox.Show($"Item status updated to: {item.StatusType}");
        }
        private bool _isDragging = false;

        private void ListView_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var listViewItem = GetListViewItem(sender as ListView, e);
            if (listViewItem != null)
            {
                var draggedItem = listViewItem.DataContext as Position;
                if (draggedItem != null)
                {
                    // Jelöljük, hogy most egy drag műveletet kezdünk
                    _isDragging = true;

                    // A drag műveletet csak akkor indítjuk el, ha az item valóban létezik
                    DragDrop.DoDragDrop(listViewItem, draggedItem, DragDropEffects.Move);
                }
            }
        }

        private void ListView_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            // Ellenőrizzük, hogy a flag be van-e állítva
            if (_isDragging)
            {
                var listViewItem = GetListViewItem(sender as ListView, e);
                if (listViewItem != null)
                {
                    var draggedItem = listViewItem.DataContext as Position;
                    if (draggedItem != null)
                    {
                        // Ha valóban egy elem van, elindítjuk a drag-and-drop műveletet
                        DragDrop.DoDragDrop(listViewItem, draggedItem, DragDropEffects.Move);
                        _isDragging = false;  // Reset
                    }
                }
            }
        }

        private void ListView_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            // Ha felengedtük az egeret, akkor a drag véget ért
            _isDragging = false;
        }

        // A GetListViewItem metódus változtatása:
        private ListViewItem GetListViewItem(ListView listView, MouseEventArgs e)
        {
            var element = e.OriginalSource as DependencyObject;
            while (element != null && !(element is ListViewItem))
            {
                element = VisualTreeHelper.GetParent(element);
            }
            return element as ListViewItem;
        }




        private ListViewItem GetListViewItem(ListView listView, MouseButtonEventArgs e)
        {
            var element = e.OriginalSource as DependencyObject;
            while (element != null && !(element is ListViewItem))
            {
                element = VisualTreeHelper.GetParent(element);
            }
            return element as ListViewItem;
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            await _dragAndDropTableViewModel.InitializeAsync();
            await LoadData();
            UpdateTable();
        }
    }
}
