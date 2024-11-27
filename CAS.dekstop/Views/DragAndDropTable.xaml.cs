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
    public partial class DragAndDropTable : UserControl
    {
        private DragAndDropTableViewModel _dragAndDropTableViewModel;

        private ObservableCollection<Position> ToDoItems = new();
        private ObservableCollection<Position> InProgressItems = new();
        private ObservableCollection<Position> DoneItems = new();

        public DragAndDropTable()
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
                // Először töröljük az elemet a régi listából
                RemoveItemFromOtherLists(draggedItem);

                // Hozzáadjuk az elemet a megfelelő listához
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

        private void ListView_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var listViewItem = GetListViewItem(sender as ListView, e);
            if (listViewItem != null)
            {
                var draggedItem = listViewItem.DataContext as Position;
                if (draggedItem != null)
                {
                    DragDrop.DoDragDrop(listViewItem, draggedItem, DragDropEffects.Move);
                }
            }
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
