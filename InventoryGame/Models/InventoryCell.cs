using System;

namespace InventoryGame.Models
{
    /// <summary>
    /// Model of a cell in the inventory.
    /// </summary>
    public class InventoryCell(int id, int row, int column, int amount, int inventoryId) : IModel, ICloneable
    {
        /// <summary>
        /// Identifier.
        /// </summary>
        public int Id { get; } = id;

        public int Row { get; } = row;

        public int Column { get; } = column;

        /// <summary>
        /// Model of item.
        /// </summary>
        public Item Item { get; private set; }

        /// <summary>
        /// Amount of the item in the cell.
        /// </summary>
        public int Amount { get; private set; } = amount;

        public int InventoryId { get; } = inventoryId;

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

        public object Clone()
        {
            return MemberwiseClone();
        }

        public void RestoreFromClone(InventoryCell inventoryCell)
        {
            if (inventoryCell.Id != Id)
                throw new ArgumentException("Ids are different!", nameof(inventoryCell.Id));
            else if (inventoryCell.Row != Row)
                throw new ArgumentException("Row numbers are different!", nameof(inventoryCell.Row));
            else if (inventoryCell.Column != Column)
                throw new ArgumentException("Column numbers are different!", nameof(inventoryCell.Column));
            else if (inventoryCell.InventoryId != InventoryId)
                throw new ArgumentException("InventoryIds are different!", nameof(inventoryCell.InventoryId));

            Amount = inventoryCell.Amount;
            Item = inventoryCell.Item;
        }
    }
}
