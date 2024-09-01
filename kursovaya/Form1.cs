using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace kursovaya
{
    public partial class Form1 : Form
    {
        Thread t4;
        public Form1()
        {
            InitializeComponent();

            Thread t3 = new Thread(fillpatients);
            t3.IsBackground = true;
            t3.Start();
         
        }
        public void fillpatients()
        {

            DataClasses1DataContext db = new DataClasses1DataContext();
            var query = db.GetTable<patients>();
            while (true)
            {
                listBox1.Items.Clear();
                listBox1.Items.Add("\tФИО\t\t дата рождения\tдата поступления\tдиагноз\tлечение");
                foreach (var b in query)
                {
                    listBox1.Items.Add(b);
                }

                Thread.Sleep(1000);

            }
        }
        private void addpatient_Click(object sender, EventArgs e)
        {
            Thread t1 = new Thread(form2show);
            t1.IsBackground= true;
            t1.Start();
        }
        public void form2show()
        {
            Form2 newform2 = new Form2();
            newform2.ShowDialog();
        }

        public void form3show()
        {
            Form3 newform3 = new Form3();
            newform3.ShowDialog();
        }
       

        private void conndoctor_Click(object sender, EventArgs e)
        {
            Thread t2 = new Thread(form3show);
            t2.Start();
        }

       

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        public void line()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var stringChars = new char[4];
            var random = new Random();
            for (int i = 0; i < stringChars.Length; i++)
            {
                if (stringChars[i] == stringChars[0] || stringChars[i] == stringChars[1] || stringChars[i] == stringChars[2])
                {
                    stringChars[i] = chars[random.Next(27, 36)];
                }
                if (stringChars[i] == stringChars[3])
                {
                    stringChars[i] = chars[random.Next(1, 26)];
                }
            }
            var finalString = new String(stringChars);
            listBox2.Items.Add(finalString);
            MessageBox.Show($"выдан талон номер {finalString}");
        }
        public void onlineline( int number)
        {
            while (true)
            {
                for (int i = 0; i < number; i++)
                {
                    line();
                    Thread.Sleep(2000);
                }
                Thread.Sleep(2000);
                for (int i = 0; i < number; i++)
                {
                    var firstElement = listBox2.Items[listBox2.Items.Count - 1];
                    listBox2.Items.RemoveAt(listBox2.Items.Count - 1);
                    MessageBox.Show($" талон номер {firstElement} может пройти на прием к врачу");
                    Thread.Sleep(2000);
                }
                Thread.Sleep(2000);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int test = Convert.ToInt32(textBox1.Text);

                t4 = new Thread(() => onlineline(test));

                t4.IsBackground = true;
                t4.Start();
            }
            catch { MessageBox.Show($"  введите количество человек в очереди"); }
             
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
                t4.Abort();
           
            if (listBox2.Items.Count >0)
            {
				for (int i = listBox2.Items.Count; i >0; i--)
				{
					var firstElement = listBox2.Items[listBox2.Items.Count - 1];
					listBox2.Items.RemoveAt(listBox2.Items.Count - 1);
					MessageBox.Show($" талон номер {firstElement} может пройти на прием к врачу");
					Thread.Sleep(2000);
				}
			}
        }

       
    }
}
