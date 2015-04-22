using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedCell.Devices.SainSmart
{
    /// <summary>
    /// A relay board
    /// </summary>
    public interface IRelayBoard
    {
        /// <summary>
        /// Gets the relays.
        /// </summary>
        /// <value>The relay.</value>
        bool this[int index] { get;}

        /// <summary>
        /// Initializes the relay board.
        /// </summary>
        Task Initialize();

        /// <summary>
        /// Sets the relay at the specified index to the specified value.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="state">The relay state.</param>
        Task Set(int index, bool state);        
        
 /*       /// <summary>
        /// Sets the relay at the specified index to the specified value.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="state">The relay state.</param>
        Task Set(NetRelays index, bool state);*/
    }
}
