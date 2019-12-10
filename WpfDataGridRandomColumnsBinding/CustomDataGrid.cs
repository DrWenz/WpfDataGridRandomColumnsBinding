using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfDataGridRandomColumnsBinding
{
    public partial class CustomDataGrid : DataGrid
    {
        public static readonly DependencyProperty BindableColumnsProperty = DependencyProperty.Register("BindableColumns", typeof(ObservableCollection<DataGridColumn>), typeof(CustomDataGrid), new PropertyMetadata(null, OnBindablePropertyChanged));

        private static void OnBindablePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if(d is CustomDataGrid grid)
            {
                if(e.NewValue != null)
                {
                    foreach (var item in (ObservableCollection<DataGridColumn>)e.NewValue)
                    {
                        grid.Columns.Add((DataGridColumn)item);
                    }

                    ((ObservableCollection<DataGridColumn>)e.NewValue).CollectionChanged += (x, y) =>
                    {
                        if (y.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
                        {
                            foreach (var item in y.NewItems)
                            {
                                grid.Columns.Add((DataGridColumn)item);
                            }
                        }
                        else if (y.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
                        {
                            foreach (var item in y.OldItems)
                            {
                                for (int i = grid.Columns.Count - 1; i >= 0; i--)
                                {
                                    if (grid.Columns[i].Header == ((DataGridColumn)item).Header)
                                    {
                                        grid.Columns.Remove((DataGridColumn)item);
                                        break;
                                    }
                                }
                            }
                        }
                    };
                }
                else
                {
                    foreach (var item in (ObservableCollection<DataGridColumn>)e.OldValue)
                    {
                        for (int i = grid.Columns.Count - 1; i >= 0; i--)
                        {
                            if (grid.Columns[i].Header == ((DataGridColumn)item).Header)
                            {
                                grid.Columns.Remove((DataGridColumn)item);
                                break;
                            }
                        }
                    }
                }
            }
        }

        public ObservableCollection<DataGridColumn> BindableColumns
        {
            get => (ObservableCollection<DataGridColumn>)this.GetValue(BindableColumnsProperty);
            set
            {
                this.SetValue(BindableColumnsProperty, value);
                if(value != null)
                {
                    
                }
            }
        }
    }
}
