using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientOS
{
    public partial class FormServer1 : Form
    {
        #region Field

        private Form _parentForm;
        private TcpClient _clientSocket;

        #endregion

        #region Construct

        public FormServer1(Form parentForm, TcpClient socket)
        {
            InitializeComponent();

            _parentForm = parentForm;
            _clientSocket = socket;
        }

        #endregion

        #region PrivateMethods

        private void ButtonHideServerConsole(object sender, EventArgs e)
        {
            var result = ServerReqest.ReqestToServer((int)numericUpDown1.Value+Reqest.HideServerConsole,_clientSocket);
            MessageBox.Show(result);
        }

        private void OnFormClosed(object sender, FormClosedEventArgs e)
        {
            _clientSocket.Close();
            _parentForm.Show();
        }

        private void ButtonClose(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ShowGPUNameOnServer()
        {
            textBoxGPUName.Text = ServerReqest.ReqestToServer(Reqest.GetGPUName, _clientSocket);
        }

        private void OnLoadForm(object sender, EventArgs e)
        {
            ShowGPUNameOnServer();
        }

        private bool TryReqestToServer(out string result, string reqestToServer, TcpClient clientSocket)
        {
            result = ServerReqest.ReqestToServer(reqestToServer, clientSocket);


        }
        #endregion
    }
}
