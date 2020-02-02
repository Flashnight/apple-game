using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppleGame.Models
{
    public class InventoryCell
    {
        public IItem Item { get; set; }

        public int Count { get; set; }
    }
}
