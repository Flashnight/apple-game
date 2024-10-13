using InventoryGame.Database;
using InventoryGame.Models;
using Caliburn.Micro;
using System.Windows;
using System.Windows.Input;
using System.Threading.Tasks;

namespace InventoryGame.ViewModels
{
    /// <summary>
    /// ViewModel for the source of apples.
    /// </summary>
    public class ItemsSourceViewModel : Screen
    {
        private readonly Task<Item> _loadingDataTask;

        /// <summary>
        /// Model of an item.
        /// </summary>
        private Item _item;

        /// <summary>
        /// Model of an item.
        /// </summary>
        public Item Item
        {
            get => _item;
            set
            {
                if (_item == value)
                    return;
                _item = value;
                NotifyOfPropertyChange(() => Item);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemsRepository">Hides DB operations for items.</param>
        /// <param name="itemId">Identifier of item.</param>
        public ItemsSourceViewModel(IItemsDbRepository itemsRepository, int itemId)
        {
            _loadingDataTask = itemsRepository.GetItemByIdAsync(itemId);
        }

        protected override async void OnViewAttached(object view, object context)
        {
            Item = await _loadingDataTask;
            base.OnViewAttached(view, context);
        }

        /// <summary>
        /// Handler for MouseDown. It makes dragging from the source.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">Mouse event arguments.</param>
        public void HandleMouseDown(ItemsSourceViewModel sender, MouseEventArgs args)
        {
            DependencyObject dragSource = args.Source as DependencyObject;

            DragDrop.DoDragDrop(dragSource, this, DragDropEffects.Copy);
        }
    }
}
