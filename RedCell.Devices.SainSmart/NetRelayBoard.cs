using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace RedCell.Devices.SainSmart
{
    /// <summary>
    /// An ethernet relay board.
    /// </summary>
    public class NetRelayBoard : IRelayBoard
    {
        #region Fields
        private bool[] _states;
        private int _relayCount;
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="NetRelayBoard"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public NetRelayBoard(Models model)
        {
            switch(model)
            {
                case Models.Net8:
                    Port = 30000;
                    _states = new bool[8];
                    _relayCount = 8;
                    break;
                case Models.Net16:
                    Port = 3000;
                    _states = new bool[16];
                    _relayCount = 16;
                    break;
            }
        }
        #endregion

        #region Indexer
        /// <summary>
        /// Gets or sets the <see cref="System.Boolean"/> at the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool this[int index]
        {
            get { return !_states[index]; }
            set {
                _states[index] = !value;
                Set(index, value);
            }
        }

        /// <summary>
        /// Gets the <see cref="System.Boolean"/> with the specified relay.
        /// </summary>
        /// <param name="relay">The relay.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool this[Relays relay]
        {
            get { return this[(UInt16)relay - 1]; }
            set { this[(UInt16)relay - 1] = value; }
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the port.
        /// </summary>
        /// <value>The port.</value>
        public UInt16 Port { get; set; }

        /// <summary>
        /// Gets or sets the host.
        /// </summary>
        /// <value>The host.</value>
        public string Host { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Initializes the relay board.
        /// </summary>
        public async Task Initialize()
        {
            await InitializeAsync();
        }

        public async Task InitializeAsync()
        {
            // Ping the host.
            using (var ping = new Ping())
            {
                PingReply reply = await ping.SendPingAsync(Host, 2000);
                if (reply.Status != IPStatus.Success)
                    throw new RelayBoardException("The relay board host is not responding on the network.");
            }
        }

        /// <summary>
        /// Sets the specified relay.
        /// </summary>
        /// <param name="relay">The relay.</param>
        /// <param name="state">if set to <c>true</c> [state].</param>
        public async Task Set(Relays relay, bool state)
        {
            // Net board is one-indexed and does not have a bitmask, so we need to separate each bit.
            for (int i = 0; i < 0x10; i++)
            {
                if ((((int)relay >> i) & 0x01) == 0x01)
                    await Set(i, state);
            }
        }

        /// <summary>
        /// Sets the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="state">if set to <c>true</c> [state].</param>
        public async Task Set(int index, bool state)
        {
            if (Port == 0)
                throw new ArgumentOutOfRangeException("Port property must be a valid TCP port.");
            if (index > _relayCount - 1 || index < 0)
                throw new ArgumentOutOfRangeException("Relay index is out of range.", "index");

            await Task.Run(() =>
            {
                var message = new byte[] { 0xfd, 0x02, 0x20, (byte)(index + 1), (byte)(state ? 0x01 : 0x00), 0x5d };
                using (var socket = new Socket(SocketType.Stream, ProtocolType.Tcp))
                {
                    //socket.Connect(Host, Port);
                    socket.Connect(new IPAddress(new byte[] { 192, 168, 0, 4 }), Port);
                    socket.Send(message);
                    byte[] response = new byte[4];
                    int count = socket.Receive(response);
                    socket.Close();
                }
            });
        }
        #endregion
    }
}
