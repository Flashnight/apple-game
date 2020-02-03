using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppleGame.Models
{
    /// <summary>
    /// Model of data.
    /// </summary>
    public interface IModel
    {
        /// <summary>
        /// Identifier.
        /// </summary>
        int Id { get; set; }
    }
}
