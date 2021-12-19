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
    public partial class FormServer2 : Form
    {
        private Form _parentForm;
        private TcpClient _clientSocket;

        public FormServer2(Form parentForm, TcpClient socket)
        {
            InitializeComponent();
            _parentForm = parentForm;
            _clientSocket = socket;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            _clientSocket.Close();

            _parentForm.Show();
        }

        private void FormServer2_FormClosed(object sender, FormClosedEventArgs e)
        {
            _clientSocket.Close();
            _parentForm.Show();
        }

        private void buttonGetSystemInfo(object sender, EventArgs e)
        {
            textBoxPhysicMemory.Text = ServerReqest.ReqestToServer(Reqest.GetPhysicMemory,_clientSocket);
            textBoxVirtualMemory.Text = ServerReqest.ReqestToServer(Reqest.GetVirtualMemory, _clientSocket);

            textBoxPhysicMemory.Text+="%";
            textBoxVirtualMemory.Text+="%";
        }
    }
}
