using AppleGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AppleGame.Misc
{
    /// <summary>
    /// Maker for items.
    /// </summary>
    public static class ItemsMaker
    {
        /// <summary>
        /// Makes items by item type.
        /// </summary>
        /// <param name="itemType">Type of item.</param>
        /// <returns>Item.</returns>
        public static Item MakeItem(ItemType itemType)
        {
            ResourceDictionary imagesDictionary = new ResourceDictionary();

            string imagesDictionaryUri = "Resources/InventoryItems.xaml";
            imagesDictionary.Source = new Uri(imagesDictionaryUri, UriKind.Relative);

            string keyName = itemType.ToString();
            Item item = (Item)imagesDictionary[keyName];

            return item;
        }
    }
}
