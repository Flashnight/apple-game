using System.Collections.Generic;

namespace InventoryGame.Models
{
    /// <summary>
    /// Model of the inventory.
    /// </summary>
    public class Inventory(int id, int height, int width, List<InventoryCell> cells) : IModel
    {
        /// <summary>
        /// Identifier.
        /// </summary>
        public int Id { get; set; } = id;

        /// <summary>
        /// Height of the inventory.
        /// </summary>
        public int Height { get; set; } = height;

        /// <summary>
        /// Width of the inventory.
        /// </summary>
        public int Width { get; set; } = width;

        /// <summary>
        /// Cells in the inventory
        /// </summary>
        public List<InventoryCell> Cells { get; set; } = cells;
    }
}
