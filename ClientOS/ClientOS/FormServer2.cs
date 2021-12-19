using System;
using System.Net.Sockets;
using System.Windows.Forms;

namespace ClientOS
{
    public partial class FormServer2 : Form
    {
        #region Fields

        private Form _parentForm;
        private TcpClient _clientSocket;

        #endregion

        #region Constructors

        public FormServer2(Form parentForm, TcpClient socket)
        {
            InitializeComponent();
            _parentForm = parentForm;
            _clientSocket = socket;
        }

        #endregion

        #region PrivateMethods

        private void ButtonClose(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormServer2_FormClosed(object sender, FormClosedEventArgs e)
        {
            _clientSocket.Close();
            _parentForm.Show();
        }

        private void ButtonGetSystemInfo(object sender, EventArgs e)
        {
            SetResult(Reqest.GetPhysicMemory, ref textBoxPhysicMemory);
            SetResult(Reqest.GetVirtualMemory, ref textBoxVirtualMemory);
        }

        private void SetResult(string reqest, ref TextBox textBox)
        {
            if(ServerReqest.TryReqestToServer(out var result, reqest, _clientSocket))
            {
                textBox.Text = result;
                textBox.Text += "%";
            }
            else
            {
                if(!this.Text.Contains("Офлайн"))
                    ShowMessageErrorConnection();
            }
        }

        private void ShowMessageErrorConnection()
        {
            MessageBox.Show("Соеденение прервано!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            this.Text = "Офлайн";

            textBoxPhysicMemory.Text = "Офлайн";
            textBoxVirtualMemory.Text = "Офлайн";
        }

        #endregion
    }
}
