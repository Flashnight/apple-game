using System;

namespace InventoryGame.Models
{
    /// <summary>
    /// Model of a cell in the inventory.
    /// </summary>
    public class InventoryCell : IModel
    {
        /// <summary>
        /// Identifier.
        /// </summary>
        public int Id { get; set; }

        public int Row { get; set; }

        public int Column { get; set; }

        /// <summary>
        /// Model of item.
        /// </summary>
        public Item Item { get; set; }

        /// <summary>
        /// Amount of the item in the cell.
        /// </summary>
        public int Amount { get; set; }

        public int InventoryId { get; set; }

        /// <summary>
        /// Adds item to cell.
        /// </summary>
        /// <param name="item">Item type.</param>
        /// <exception cref="ArgumentException">Incorrect item type.</exception>
        public void AddItem(Item item)
        {
            ArgumentNullException.ThrowIfNull(item);

            if (Item is null)
                Item = item;
            else if (item.Id != Item.Id)
                throw new ArgumentException("Cell can save only the same type item.", nameof(item));

            Amount++;
        }

        /// <summary>
        /// Tries to remove single item.
        /// </summary>
        /// <returns></returns>
        public bool TryRemoveItem()
        {
            if (Amount == 0)
            {
                return false;
            }

            Amount--;

            if (Amount == 0)
            {
                Item = null;
            }

            return true;
        }

        public void CopyFrom(InventoryCell cell)
        {
            ArgumentNullException.ThrowIfNull(cell);
            ArgumentNullException.ThrowIfNull(cell.Item);

            if (Item is null)
                Item = cell.Item;
            else if (cell.Item.Id != Item.Id)
                throw new ArgumentException("Cell can save only the same type item.", nameof(cell.Item));

            Amount += cell.Amount;
        }

        /// <summary>
        /// Clear the cell.
        /// </summary>
        public void Clear()
        {
            Amount = 0;
            Item = null;
        }
    }
}
