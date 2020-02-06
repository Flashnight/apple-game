using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryGame.Models
{
    /// <summary>
    /// Model of the item in the inventory.
    /// </summary>
    public class Item : IModel
    {
        /// <summary>
        /// Identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Type of item.
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        /// Link to image of item in the file system.
        /// </summary>
        public string ImageSource { get; set; }
    }
}
