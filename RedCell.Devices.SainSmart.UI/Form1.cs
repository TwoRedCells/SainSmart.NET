using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RedCell.Devices.SainSmart.UI
{
    /// <summary>
    /// Class Form1.
    /// </summary>
    public partial class Form1 : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Form1"/> class.
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            StatusLabel.Text = "Ready.";
        }

        /// <summary>
        /// Handles the Click event of the ConnectButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void ConnectButton_Click(object sender, EventArgs e)
        {
            try
            {
                ConnectButton.Enabled = false;
                StatusLabel.Text = "Connecting to relay board.";

                if(UsbRadio.Checked)
                {
                    RelayControl.Board = new UsbRelayBoard();
                }
                if(Net8Radio.Checked || Net16Radio.Checked)
                {
                    NetRelayBoard board = null;
                    if (Net8Radio.Checked)
                        RelayControl.Board = board = new NetRelayBoard(Models.Net8);
                    if (Net16Radio.Checked)
                        RelayControl.Board = board = new NetRelayBoard(Models.Net16);

                    board.Host = HostText.Text;
                    board.Port = ushort.Parse(PortText.Text);
                }

                await RelayControl.Board.Initialize();

                ControlsGroup.Enabled = true;
                StatusLabel.Text = "Successfully connected to relay board.";
            }
            catch (Exception ex)
            {
                StatusLabel.Text = "Connection to relay board failed.";
                MessageBox.Show(ex.ToString(), ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                ConnectButton.Enabled = true;
            }
        }

        /// <summary>
        /// Boards the radio changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void BoardRadioChanged(object sender, EventArgs e)
        {
            if(UsbRadio.Checked)
            {
                HostText.Text = "";
                PortText.Text = "";
                HostText.Enabled = false; 
                PortText.Enabled = false;
            }
            if (Net8Radio.Checked)
            {
                HostText.Text = "192.168.0.4";
                PortText.Text = "30000";
                HostText.Enabled = true;
                PortText.Enabled = true;
            }
            if (Net16Radio.Checked)
            {
                HostText.Text = "192.168.0.4";
                PortText.Text = "3000";
                HostText.Enabled = true;
                PortText.Enabled = true;
            }
        }
    }
}

