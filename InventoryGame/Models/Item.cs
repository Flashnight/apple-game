namespace InventoryGame.Models
{
    /// <summary>
    /// Model of the item in the inventory.
    /// </summary>
    /// <param name="Id">Identifier.</param>
    /// <param name="ItemName">Type of item.</param>
    /// <param name="ImageSource">Link to image of item in the file system.</param>
    public record Item(int Id, string ItemName, string ImageSource);
}
