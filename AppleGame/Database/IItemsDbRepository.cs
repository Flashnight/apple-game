using AppleGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppleGame.Database
{
    /// <summary>
    /// Encapsulates DB operations.
    /// </summary>
    public interface ItemsRepository
    {
        /// <summary>
        /// Returns image's data by id.
        /// </summary>
        /// <param name="id">Identifier of item.</param>
        /// <returns>Model's data model.</returns>
        Item GetItemById(int id);
    }
}
