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

namespace kursovaya
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();

            Thread t1 = new Thread(fillpatients);
            t1.IsBackground = true;
            t1.Start();
            Thread t2 = new Thread(fillpatients2);
            t2.IsBackground = true;
            t2.Start();

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
        public void fillpatients2()
        {

            DataClasses1DataContext db = new DataClasses1DataContext();
            var query = db.GetTable<dischargedpatient>();
            while (true)
            {
                listBox2.Items.Clear();
                listBox2.Items.Add("\tФИО\t\t дата рождения\tдата поступления \tдата выписки \tдиагноз\tлечение");

                foreach (var b in query)
                {
                    listBox2.Items.Add(b);
                }

                Thread.Sleep(1000);

            }
        }


        private void addpatient_Click(object sender, EventArgs e)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            var query = db.GetTable<patients>();
          
            query.InsertOnSubmit(new patients(textBox1.Text, DateTime.Parse(textBox2.Text), DateTime.Parse(textBox3.Text), textBox4.Text, textBox5.Text));
            db.SubmitChanges();
           
        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            var query = db.GetTable<patients>();
            DataClasses1DataContext db1 = new DataClasses1DataContext();
            var query2 = db1.GetTable<dischargedpatient>();


            var res2 = from c in query
                       where c.FIO == $"{textBox6.Text}" && c.diagnosis == $"{textBox10.Text}" && c.medicine == $"{textBox11.Text}"
                       select c;

            res2.FirstOrDefault();
            try
            {
                if (res2.FirstOrDefault().FIO == textBox6.Text && res2.FirstOrDefault().diagnosis == textBox10.Text && res2.FirstOrDefault().medicine == textBox11.Text && res2.FirstOrDefault().birthdate== DateTime.Parse(textBox7.Text) && res2.FirstOrDefault().admissiondate == DateTime.Parse(textBox8.Text))
                {

                    query.DeleteAllOnSubmit(res2);
                    db.SubmitChanges();
                    query2.InsertOnSubmit(new dischargedpatient(textBox6.Text, DateTime.Parse(textBox7.Text), DateTime.Parse(textBox8.Text), DateTime.Parse(textBox9.Text), textBox10.Text, textBox11.Text));
                    db1.SubmitChanges();


                }
            } catch { MessageBox.Show($"  вы ввели неверные данные "); }
         
             


        }
    }
}
