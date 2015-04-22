using System;
using System.Windows.Forms;
using System.Threading.Tasks;
using RedCell.Devices;

namespace RedCell.Devices.SainSmart
{
    /// <summary>
    /// Class RelaysControl.
    /// </summary>
    public partial class RelaysControl : UserControl
    {
        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="RelaysControl"/> class.
        /// </summary>
        public RelaysControl()
        {
            InitializeComponent();

            // Initialize controls
            CheckBox[] boxes = { K01, K02, K03, K04, K05, K06, K07, K08, K09, K10, K11, K12, K13, K14, K15, K16 };
            UsbRelays[] relays = { UsbRelays.K01, UsbRelays.K02, UsbRelays.K03, UsbRelays.K04, UsbRelays.K05, UsbRelays.K06, UsbRelays.K07, UsbRelays.K08, 
                                        UsbRelays.K09, UsbRelays.K10, UsbRelays.K11, UsbRelays.K12, UsbRelays.K13, UsbRelays.K14, UsbRelays.K15, UsbRelays.K16 };
            for(int i=0; i<boxes.Length; i++)
            {
                //boxes[i].Tag = relays[i];
                boxes[i].Tag = i;
                boxes[i].CheckedChanged += Relays_CheckedChanged;
            }
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public async void Initialize()
        {
            try 
            {
                if (Board == null)
                    throw new Exception("Board property must be set before invoking Initialize method.");

                await Board.Initialize(); 
            }
            catch (RelayBoardException ex)
            {
                MessageBox.Show(ex.Message, "RelayBoardException", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the board.
        /// </summary>
        /// <value>The board.</value>
        public IRelayBoard Board { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Handles the CheckedChanged event of the Relays control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        async void Relays_CheckedChanged(object sender, EventArgs e)
        {
            var box = sender as CheckBox;
            //await (Board as UsbRelayBoard).Set((UsbRelays)box.Tag, box.Checked);
            int index = (int)box.Tag;

            // USB board is zero-indexed.
            // Net board is one-indexed.
            if (Board is UsbRelayBoard)
                index = (int) Math.Pow(2, index);

            await Board.Set(index, box.Checked);
        }
        #endregion
    }
}
