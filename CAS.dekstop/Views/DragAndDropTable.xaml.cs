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

namespace CAS.dekstop.Views
{
    public partial class DragAndDropTable : UserControl
    {
        public ObservableCollection<TaskItem> ToDoItems { get; set; }
        public ObservableCollection<TaskItem> InProgressItems { get; set; }
        public ObservableCollection<TaskItem> DoneItems { get; set; }

        public DragAndDropTable()
        {
            InitializeComponent();
            LoadData();
            DataContext = this;
        }

        private void LoadData()
        {
            

            InProgressItems = new ObservableCollection<TaskItem>();
            DoneItems = new ObservableCollection<TaskItem>();

            ToDoListView.ItemsSource = ToDoItems;
            InProgressListView.ItemsSource = InProgressItems;
            DoneListView.ItemsSource = DoneItems;
        }

        // DragOver esemény, hogy a céllistában elfogadja a húzott elemeket
        private void ListView_PreviewDragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(TaskItem)))
            {
                e.Effects = DragDropEffects.Move;
                e.Handled = true;
            }
        }

        // Drop esemény, amikor az elemet a másik listára húzzuk
        private void ListView_Drop(object sender, DragEventArgs e)
        {
            var targetListView = sender as ListView;
            var draggedItem = e.Data.GetData(typeof(TaskItem)) as TaskItem;

            if (draggedItem != null)
            {
                // A cél listától függően módosítjuk az item státuszát
                if (targetListView == ToDoListView)
                {
                    draggedItem.Status = "To Do";
                    ToDoItems.Add(draggedItem);
                }
                else if (targetListView == InProgressListView)
                {
                    draggedItem.Status = "In Progress";
                    InProgressItems.Add(draggedItem);
                }
                else if (targetListView == DoneListView)
                {
                    draggedItem.Status = "Done";
                    DoneItems.Add(draggedItem);
                }

                // Eltávolítjuk az elemet az eredeti listából
                RemoveItemFromOtherLists(draggedItem);

                // Backend frissítés (pl. API hívás)
                UpdateBackendStatus(draggedItem);
            }
        }

        private void RemoveItemFromOtherLists(TaskItem item)
        {
            // Ellenőrizzük, hogy az elem valóban létezik az adott listákban
            if (ToDoItems.Contains(item)) ToDoItems.Remove(item);
            if (InProgressItems.Contains(item)) InProgressItems.Remove(item);
            if (DoneItems.Contains(item)) DoneItems.Remove(item);
        }

        private void UpdateBackendStatus(TaskItem item)
        {
            // Backend kommunikáció szimulálása
            // Itt kell API hívást végezni, hogy a backend adatai is frissüljenek
            MessageBox.Show($"Item status updated to: {item.Status}");
        }

        // Az elemek húzásának kezdeményezése (mouse left button down)
        private void ListView_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var listViewItem = GetListViewItem(sender as ListView, e);
            if (listViewItem != null)
            {
                var draggedItem = listViewItem.DataContext as TaskItem;
                if (draggedItem != null)
                {
                    // Csak akkor induljon el a drag-and-drop, ha ténylegesen elhúzzák az elemet
                    DragDrop.DoDragDrop(listViewItem, draggedItem, DragDropEffects.Move);
                }
            }
        }

        // Segédfüggvény a ListViewItem megtalálásához
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

