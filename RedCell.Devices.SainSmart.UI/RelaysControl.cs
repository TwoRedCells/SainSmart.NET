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
            Relays[] relays = { Relays.K01, Relays.K02, Relays.K03, Relays.K04, Relays.K05, Relays.K06, Relays.K07, Relays.K08, 
                                        Relays.K09, Relays.K10, Relays.K11, Relays.K12, Relays.K13, Relays.K14, Relays.K15, Relays.K16 };
            for(int i=0; i<boxes.Length; i++)
            {
                boxes[i].Tag = relays[i];
                //boxes[i].Tag = i;
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
            Relays relay = (Relays)box.Tag;

            // USB board is zero-indexed and uses a bitmask.
            if (Board is UsbRelayBoard)
                await (Board as UsbRelayBoard).Set(relay, box.Checked);

            // Net board is one-indexed and does not have a bitmask, so we need to separate each bit.
            if (Board is NetRelayBoard)
            {
                for (int i = 0; i < 0x10; i++)
                {
                    if((((int)relay >> i) & 0x01) == 0x01)
                        await Board.Set(i, box.Checked);
                }
            }
        }
        #endregion
    }
}
