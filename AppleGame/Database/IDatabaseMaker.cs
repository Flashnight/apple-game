using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppleGame.Database
{
    /// <summary>
    /// It initializes new database.
    /// </summary>
    public interface IDatabaseMaker
    {
        /// <summary>
        /// Initializes new database if it doesnt exist for SQLLite.
        /// </summary>
        void CreateDatabase();
    }
}
