using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DataWarehouseViewer
{
    public class EditableItem<T> : IEditableObject
    {
        public EditableItem(T @object)
        {
            Item = @object;
        }

        public T Item { get; }

        public void BeginEdit()
        {
        }

        public void CancelEdit()
        {
        }

        public void EndEdit()
        {
        }
    }


    public class EditableCollectionView<T> : IEditableCollectionView
    {
        private readonly IEditableCollectionView _collectionView;

        public EditableCollectionView(IEnumerable<T> editableItems)
        {
            List<EditableItem<T>> list = new List<EditableItem<T>>();
            foreach(var item in editableItems)
            {
                list.Add(new EditableItem<T>(item));
            }

            _collectionView = CollectionViewSource.GetDefaultView(list) as IEditableCollectionView;
        }

        public NewItemPlaceholderPosition NewItemPlaceholderPosition
        {
            get
            {
                return _collectionView.NewItemPlaceholderPosition;
            }

            set
            {
                _collectionView.NewItemPlaceholderPosition = value;
            }
        }

        public bool CanAddNew
        {
            get
            {
                return _collectionView.CanAddNew;
            }
        }

        public bool IsAddingNew
        {
            get
            {
                return _collectionView.IsAddingNew;
            }
        }

        public object CurrentAddItem
        {
            get
            {
                return _collectionView.CurrentAddItem;
            }
        }

        public bool CanRemove
        {
            get
            {
                return _collectionView.CanRemove;
            }
        }

        public bool CanCancelEdit
        {
            get
            {
                return _collectionView.CanCancelEdit;
            }
        }

        public bool IsEditingItem
        {
            get
            {
                return _collectionView.IsEditingItem;
            }
        }

        public object CurrentEditItem
        {
            get
            {
                return _collectionView.CurrentEditItem;
            }
        }

        public object AddNew()
        {
            return _collectionView.AddNew();
        }

        public void CancelEdit()
        {
            _collectionView.CancelEdit();
        }

        public void CancelNew()
        {
            _collectionView.CancelNew();
        }

        public void CommitEdit()
        {
            _collectionView.CommitEdit();
        }

        public void CommitNew()
        {
            _collectionView.CommitNew();
        }

        public void EditItem(object item)
        {
            _collectionView.EditItem(item);
        }

        public void Remove(object item)
        {
            _collectionView.Remove(item);
        }

        public void RemoveAt(int index)
        {
            _collectionView.RemoveAt(index);
        }
    }
}
