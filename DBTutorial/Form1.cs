using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBTutorial
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            using (var classicContext = new classicmodelsEntities())
            {
                lstBox1.Items.Clear();
                var data = from list in classicContext.orderdetails
                           group list by list.orderNumber into g
                           select new { g.FirstOrDefault().orderNumber, priceSum = g.Sum(i => i.priceEach) }

                    foreach (var orderNum in data.ToList())
                {
                    lstBox1.Items.Add(orderNum.orderNumber + " " + orderNum.priceSum);
                }

                var data1 = classicContext.orderdetails.GroupBy(i => i.orderNumber).Select(g => new
                {
                    orderNum = g.FirstOrDefault().orderNumber,
                    priceSum = g.Sum(i => i.priceEach)
                });

                foreach (var orderNum in data1.ToList())
                {
                    lstBox2.Items.Add(orderNum.orderNum + "= " + orderNum.priceSum);
                }
                
                var orderDetail = (from list in classicContext.orderdetails
                                   select list).FirstOrDefault();
                MessageBox.Show("Order:" + orderDetail.orders.orderNumber + " / Status:" + orderDetail.orders.status);

                var data2 = classicContext.orders.Select(i => i).OrderBy(i => i.orderDate).Take(2);
                foreach (var order in data2.ToList())
                {
                    foreach (var orderdetail in order.orderdetails.ToList())
                    {
                        MessageBox.Show("Orderline num: " + orderdetail.orderLineNumber, order.orderDate + "");
                    }
                }


            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
