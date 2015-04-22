using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace RedCell.Devices.SainSmart
{   
    /// <summary>
    /// SainSmart 5V USB Relay Board control
    /// 
    /// - Make sure FTDI D2XX drivers are installed: http://www.ftdichip.com/Drivers/D2XX.htm
    /// Based on code by Anthony Marshall
    /// 
    /// </summary>
    public class UsbRelayBoard : IRelayBoard
    {
        #region Constants
        public const int BaudRate = 9600;
        #endregion

        #region Fields
        private byte[] startup = { 0x00, 0x00, 0x00, 0x00 };
        private uint bytesToSend = 1;
        private bool initialized = false;
        private static UInt32 ftdiDeviceCount = 0;
        private static FTDI.FT_STATUS ftStatus = FTDI.FT_STATUS.FT_OK;
        private static FTDI ftdi = new FTDI();
        private bool[] _states = new bool[0x10];
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
            set
            {
                _states[index] = !value;
                Set(index, value);
            }
        }

        /// <summary>
        /// Gets the <see cref="System.Boolean"/> with the specified relay.
        /// </summary>
        /// <param name="relay">The relay.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool this[NetRelays relay]
        {
            get { return this[(UInt16)relay - 1]; }
            set { this[(UInt16)relay - 1] = value; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Initializes the relay board.
        /// </summary>
        public async Task Initialize()
        {
            await InitializeAsync(true);
        }

        /// <summary>
        /// Find the FDTI chip, connect, set baud to 9600, set sync bit-bang mode
        /// </summary>
        /// <returns></returns>
        public void Initialize(bool force = false)
        {
            if (initialized && !force)
                return;

            for (int tries = 3; tries >= 0; tries--)
            {
                try
                {
                    Debug.WriteLine("FTDI: Initializing, tries remaining = {0}", tries);

                    // Determine the number of FTDI devices connected to the machine
                    ftStatus = ftdi.GetNumberOfDevices(ref ftdiDeviceCount);
                    // Check status
                    if (ftStatus == FTDI.FT_STATUS.FT_OK)
                        Debug.WriteLine("FTDI: Number of FTDI devices: " + ftdiDeviceCount);
                    else
                        throw new RelayBoardException("Failed to get number of devices (error " + ftStatus + ")");

                    if (ftdiDeviceCount == 0)
                        throw new RelayBoardException("Relay board not found, try:\r\nchecking connections\r\nusing without a USB hub\r\nusing a powered USB hub");

                    // Allocate storage for device info list
                    FTDI.FT_DEVICE_INFO_NODE[] ftdiDeviceList = new FTDI.FT_DEVICE_INFO_NODE[ftdiDeviceCount];

                    // Populate our device list
                    ftStatus = ftdi.GetDeviceList(ftdiDeviceList);
                    var ftDevice = ftdiDeviceList[0];

                    if (ftDevice.Type == FTDI.FT_DEVICE.FT_DEVICE_UNKNOWN)
                        throw new RelayBoardException("The relay board device is unknown.");

                    // Open first device in our list by serial number
                    Debug.WriteLine("FTDI: Opening device by serial number #{0}", ftDevice.SerialNumber);
                    ftStatus = ftdi.OpenBySerialNumber(ftDevice.SerialNumber);

                    if (ftStatus != FTDI.FT_STATUS.FT_OK)
                        throw new RelayBoardException("The relay board is reporting an error: " + Enum.GetName(typeof(FTDI.FT_STATUS), ftStatus));

                    // Set Baud rate to 9600
                    ftStatus = ftdi.SetBaudRate(BaudRate);
                    Debug.WriteLine("FTDI: Setting baud rate to {0}", BaudRate);

                    Debug.WriteLine("FTDI: Turning all relays off");
                    // Set FT245RL to synchronous bit-bang mode, used on sainsmart relay board
                    ftdi.SetBitMode(0xFF, FTDI.FT_BIT_MODES.FT_BIT_MODE_SYNC_BITBANG);
                    // Switch off all the relays
                    ftdi.Write(startup, bytesToSend, ref bytesToSend);
                    initialized = true;
                    Debug.WriteLine("FTDI: Initialization successful");
                    return;
                }
                catch (RelayBoardException ex)
                {
                    if (tries <= 0)
                    {
                        Debug.WriteLine("FTDI: Initialization failed");
                        throw ex;
                    }

                    // Reset the USB port.
                    Debug.Write("FTDI: Cycling USB port, ");
                    ftStatus = ftdi.CyclePort();
                    Debug.WriteLine(ftStatus == FTDI.FT_STATUS.FT_OK ? "successful" : "failed");

                    Debug.Write("FTDI: Rescanning, ");
                    ftStatus = ftdi.Rescan();
                    Debug.WriteLine(ftStatus == FTDI.FT_STATUS.FT_OK ? "successful" : "failed");

                    Debug.Write("FTDI: Resetting, ");
                    ftStatus = ftdi.Rescan();
                    System.Threading.Thread.Sleep(2000);
                    Debug.WriteLine(ftStatus == FTDI.FT_STATUS.FT_OK ? "successful" : "failed");
                }
            }
        }

        /// <summary>
        /// initialize as an asynchronous operation.
        /// </summary>
        public async Task InitializeAsync(bool force = false)
        {
            await Task.Run(() => Initialize(force));
        }

        /// <summary>
        /// Gets or sets the pins.
        /// </summary>
        /// <value>The pins.</value>
        public byte Pins { get; set; }

        /// <summary>
        /// Activate/De-activate a specific relay
        /// </summary>
        /// <param name="relay"></param>
        /// <param name="state"></param>
        public async Task Set(int index, bool state)
        {
            await Task.Run(() =>
            {
                uint numBytes = 1;
                uint relayAddr = (uint)index;
                byte[] @out = { 0x00 };
                byte pins = 0x00;
                byte output = 0x00;

                // Get current pin state.
                ftdi.GetPinStates(ref pins);

                switch (state)
                {
                    case true: output = (byte)(pins | relayAddr); break;
                    case false: output = (byte)(pins & ~(relayAddr)); break;
                }
                ftdi.GetPinStates(ref pins);
                Pins = pins;

                @out[0] = output;
                ftdi.Write(@out, 1, ref numBytes);
            });
        }

        /// <summary>
        /// Sets the specified relay.
        /// </summary>
        /// <param name="relay">The relay.</param>
        /// <param name="state">if set to <c>true</c> [state].</param>
        public async Task Set(UsbRelays relay, bool state)
        {
            int index = (int)relay;
            await Set(index, state);
        }
        #endregion
    }
}
