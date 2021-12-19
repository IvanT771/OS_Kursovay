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
    public partial class FormConnection : Form
    {
        private NetworkStream _serverStream;

        public FormConnection()
        {
            InitializeComponent();
        }

        

        private void buttonConnection(object sender, EventArgs e)
        {
            button1.Enabled = false;

            TcpClient clientSocket = new TcpClient();

            string ip = textBox1.Text.Split(':')[0];
            int port = int.Parse(textBox1.Text.Split(':')[1]);

            try
            {
                clientSocket.Connect(ip, port);
                var returndata = ServerReqest.ReqestToServer(Reqest.GetNameServer,clientSocket);

                OpenForm(returndata,clientSocket);

            }
            catch 
            {
                MessageBox.Show(this,"Не удалось подключиться к серверу!","Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Error,MessageBoxDefaultButton.Button1);
                button1.Enabled = true;
            }
            
        }

        private void OpenForm(string formName,TcpClient clientSocket)
        {
            if (formName.Contains("Server2"))
            {
                FormServer2 formServer2 = new FormServer2(this, clientSocket);

                this.Hide();
                formServer2.Show();
                button1.Enabled = true;
            }
            else if (formName.Contains("Server1"))
            {
                FormServer1 formServer2 = new FormServer1(this, clientSocket);

                this.Hide();
                formServer2.Show();
                button1.Enabled = true;
            }
            else
            {
                MessageBox.Show(this, "Вы не можете управлять данным сервером.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }
    }
}
