using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ClearDrive.shared.Models;
using ClearDrive.desktop.ViewModels;

namespace ClearDrive.desktop.Views.Content
{
    public partial class DragAndDropTablePage : UserControl
    {
        private DragAndDropTableViewModel _dragAndDropTableViewModel;
        private ObservableCollection<Position> ToDoItems = new();
        private ObservableCollection<Position> InProgressItems = new();
        private ObservableCollection<Position> DoneItems = new();

        private bool _isDataLoaded = false;
        private bool _isDragging = false;

        public DragAndDropTablePage(DragAndDropTableViewModel viewModel)
        {
            InitializeComponent();
            _dragAndDropTableViewModel = viewModel;
            DataContext = viewModel;
        }

        public async Task LoadData()
        {
            if (_isDataLoaded) return;

            await _dragAndDropTableViewModel.UpdateView();
            foreach (var item in _dragAndDropTableViewModel.Locations)
            {
                if (item.StatusType == shared.Models.Enums.StatusType.ToDO)
                {
                    ToDoItems.Add(item);
                }
                else if (item.StatusType == shared.Models.Enums.StatusType.InProgress)
                {
                    InProgressItems.Add(item);
                }
                else
                {
                    DoneItems.Add(item);
                }
            }

            _isDataLoaded = true;
        }

        public void UpdateTable()
        {
            ToDoListView.ItemsSource = ToDoItems;
            InProgressListView.ItemsSource = InProgressItems;
            DoneListView.ItemsSource = DoneItems;
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            await _dragAndDropTableViewModel.InitializeAsync();
            await LoadData();
            UpdateTable();
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
                    draggedItem.StatusType = shared.Models.Enums.StatusType.ToDO;
                    ToDoItems.Add(draggedItem);
                }
                else if (targetListView == InProgressListView)
                {
                    draggedItem.StatusType = shared.Models.Enums.StatusType.InProgress;
                    InProgressItems.Add(draggedItem);
                }
                else if (targetListView == DoneListView)
                {
                    draggedItem.StatusType = shared.Models.Enums.StatusType.Done;
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
                    _isDragging = true;

                    DragDrop.DoDragDrop(listViewItem, draggedItem, DragDropEffects.Move);
                }
            }
        }

        private void ListView_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (_isDragging)
            {
                var listViewItem = GetListViewItem(sender as ListView, e);
                if (listViewItem != null)
                {
                    var draggedItem = listViewItem.DataContext as Position;
                    if (draggedItem != null)
                    {
                        DragDrop.DoDragDrop(listViewItem, draggedItem, DragDropEffects.Move);
                        _isDragging = false;
                    }
                }
            }
        }

        private void ListView_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _isDragging = false;
        }


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
    }
}
