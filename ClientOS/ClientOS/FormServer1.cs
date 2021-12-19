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
            string newReqest = (int)numericUpDown1.Value + Reqest.HideServerConsole;

            if (ServerReqest.TryReqestToServer(out var result, newReqest, _clientSocket))
            {
                MessageBox.Show(result, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                ShowMessageErrorConnection();
            }         
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
            string newReqest = Reqest.GetGPUName;

            if(ServerReqest.TryReqestToServer(out var result, newReqest, _clientSocket))
            {
                textBoxGPUName.Text = result;
            }
            else
            {
                ShowMessageErrorConnection();
            }
        }

        private void OnLoadForm(object sender, EventArgs e)
        {
            ShowGPUNameOnServer();
        }

        private void ShowMessageErrorConnection()
        {
            MessageBox.Show("Соеденение прервано!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            this.Text = "Офлайн";
        }

        #endregion
    }
}
