using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppleGame.Models
{
    /// <summary>
    /// Model of the inventory.
    /// </summary>
    public class Inventory : IModel
    {
        /// <summary>
        /// Identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Height of the inventory.
        /// </summary>
        public int Height { get; private set; }

        /// <summary>
        /// Width of the inventory.
        /// </summary>
        public int Width { get; private set; }
    }
}
