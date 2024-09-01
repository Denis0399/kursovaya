using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace kursovaya
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            Thread t1 = new Thread(ServerOn);
            t1.IsBackground = true;
            t1.Start();
        }

        private void ServerOn()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8888);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(ep);
            socket.Listen(10);
            while (true)
            {
                Socket ns = socket.Accept();
                Console.WriteLine();
                StringBuilder sb = new StringBuilder();
                int count = 0;
                byte[] data = new byte[1024];
                do
                {
                    count = ns.Receive(data);
                    listBox1.Items.Add("врач " + " " + Encoding.Unicode.GetString(data, 0, count));
                } while (socket.Available > 0);

                Console.WriteLine(sb);
                ns.Close();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8005);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(ep);

            string message = textBox1.Text;
            byte[] data = Encoding.Unicode.GetBytes(message);
            socket.Send(data);
            listBox1.Items.Add("Я: " + message);
            socket.Close();
        }
    }
}
