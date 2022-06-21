using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Drawing.Printing;

namespace AhmadSanitary
{
    public partial class Invoice_Detail : Form
    {
        Decimal invoice_no, c_id, sub_id;
        UF.Class1 test = new UF.Class1();

        public Invoice_Detail(decimal Invoice_No)
        {
            InitializeComponent();
            invoice_no = Invoice_No;
            labelinvoiveno.Text = Convert.ToString(Invoice_No);
        }

        private void Invoice_Detail_Load(object sender, EventArgs e)
        {
            Login.con.Open();
            SqlCommand cmd = new SqlCommand("select * from sale_stock where ss_id='" + invoice_no + "'", Login.con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                labeldatetime.Text = Convert.ToString(dr[1]);
                labeltotal.Text = Convert.ToString(dr[2]);
                labeldiscount.Text = Convert.ToString(dr[3]);
                labelamount.Text = Convert.ToString(Convert.ToDecimal(dr[2]) - Convert.ToDecimal(dr[3]));
                labelreceive.Text = Convert.ToString(dr[4]);
                labelbalance.Text = Convert.ToString((Convert.ToDecimal(dr[2]) - Convert.ToDecimal(dr[3])) - Convert.ToDecimal(dr[4]));
                if (Convert.ToString(dr[0]) != DBNull.Value.ToString())
                {
                    c_id = Convert.ToDecimal(dr[0]);
                    sub_id = (Convert.ToString(dr[10]) != DBNull.Value.ToString()) ? Convert.ToDecimal(dr[10]) : 0;
                    dr.Close();
                    SqlCommand cmd2 = new SqlCommand("select c_name from customer where c_id='" + c_id + "'", Login.con);
                    SqlDataReader dr2 = cmd2.ExecuteReader();
                    while (dr2.Read())
                    {
                        labelcustomername.Text = Convert.ToString(dr2[0]);
                    }
                    dr2.Close();

                    SqlCommand cmd3 = new SqlCommand("select name from sub_account where sub_id='" + sub_id + "'", Login.con);
                    SqlDataReader dr3 = cmd3.ExecuteReader();
                    while (dr3.Read())
                    {
                        lbSubAc.Text = Convert.ToString(dr3[0]);
                    }
                    dr3.Close();
                }
                else if (Convert.ToString(dr[9]) != DBNull.Value.ToString())
                {
                    c_id = Convert.ToDecimal(dr[9]);
                    dr.Close();
                    SqlCommand cmd2 = new SqlCommand("select name from manufacturer where m_id='" + c_id + "'", Login.con);
                    SqlDataReader dr2 = cmd2.ExecuteReader();
                    while (dr2.Read())
                    {
                        labelcustomername.Text = Convert.ToString(dr2[0]);
                    }
                    dr2.Close();
                }
                break;
            }
            dr.Close();
            //string command = "select s.p_id P_Code,s.tone Tone,s.quality Quality,s.size Size,s.qty Boxes,s.tiles Tiles, CAST(ROUND((p.meters*(s.qty + (s.tiles / p.pieces))),2) as decimal(18,2)) Meters, s.price Price, s.discount Discount, CAST(ROUND((((s.qty + (s.tiles / p.pieces)) * s.price * p.meters) - ((((s.qty + (s.tiles / p.pieces)) * s.price * p.meters)* s.discount)/100)),2) as decimal(18,2)) Amount From sale_stock_detail s, product p Where s.p_id=p.p_id AND s.size=p.size AND s.ss_id ='" + invoice_no + "'";
            //test.bindingcode(command, dataGridView, Login.con);
            string command2 = "select s.p_id P_Code, p.p_name P_Name, s.White, s.Ivory, s.Blue, s.S_Green, s.Pink, s.Gray, s.Beige, s.Burgundy, s.Peacock, s.Mauve, s.Black, s.Brown, s.D_Blue, s.CP, s.T_Qty, s.price Price, (s.t_qty * s.price)  Amount From sale_stock_detail2 s, product2 p Where s.p_id=p.p_id AND s.ss_id ='" + invoice_no + "'";
            test.bindingcode(command2, dataGridView2, Login.con);
            Login.con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                printDocument1.DefaultPageSettings.Landscape = true;
                printDocument1.Print();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        decimal total;
        private int i, j = 0;
        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            Image imgPerson = Image.FromFile(@"C:\wonder.png");
            e.Graphics.DrawImage(imgPerson, 20, 28);
            e.Graphics.DrawString("WONDER PLASTIC INDUSTRY", new Font("Arial", 24, FontStyle.Bold), Brushes.Black, 210, 50);
            e.Graphics.DrawString("Manufacturers of Bathroom Accessories & Flush Tank", new Font("Arial", 10), Brushes.Black, 220, 90);
            e.Graphics.DrawString("Address: Nowshera Sansi Road, Gujranwala, Punjab, Pakistan.", new Font("Arial", 9, FontStyle.Italic), Brushes.Black, 220, 105);
            e.Graphics.DrawString("", new Font("Arial", 9, FontStyle.Regular), Brushes.Black, 220, 125);
            //e.Graphics.DrawString("Proprietor: ", new Font("Arial", 9, FontStyle.Bold), Brushes.Black, 265, 140);
            e.Graphics.DrawString("email: contactus@wonderplastic.pk   website: www.wonderplastic.pk", new Font("Arial", 9, FontStyle.Italic), Brushes.Black, 220, 140);
            total = 0;
            int startX = 50;
            int startY = 150;
            int Offset = 40;
            e.Graphics.DrawString("Invoice No:", new Font("Calibri", 12), new SolidBrush(Color.Black), startX, startY + Offset);
            e.Graphics.DrawString(labelinvoiveno.Text, new Font("Calibri", 12, FontStyle.Bold), new SolidBrush(Color.Black), startX + 85, startY + Offset);
            Offset = Offset + 30;
            e.Graphics.DrawString("A/c Name:", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            e.Graphics.DrawString(labelcustomername.Text, new Font("Calibri", 10), new SolidBrush(Color.Black), startX + 85, startY + Offset);
            Offset = Offset + 30;
            e.Graphics.DrawString("Sub A/c Name:", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            e.Graphics.DrawString(lbSubAc.Text, new Font("Calibri", 10), new SolidBrush(Color.Black), startX + 105, startY + Offset);
            Offset = Offset + 20;
            string underLine = "======================================================================================================================";
            //if (dataGridView.Rows.Count != 0)
            //{
            //    e.Graphics.DrawString(underLine, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            //    Offset = Offset + 20;
            //    e.Graphics.DrawString("Sr#", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            //    e.Graphics.DrawString("P_Code", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 45, startY + Offset);
            //    e.Graphics.DrawString("Tone", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 180, startY + Offset);
            //    e.Graphics.DrawString("Quality", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 230, startY + Offset);
            //    e.Graphics.DrawString("Size", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 300, startY + Offset);
            //    e.Graphics.DrawString("Box/T", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 355, startY + Offset);
            //    e.Graphics.DrawString("Meters", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 410, startY + Offset);
            //    e.Graphics.DrawString("Price", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 475, startY + Offset);
            //    e.Graphics.DrawString("Amount", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 550, startY + Offset);
            //    e.Graphics.DrawString("Disc%", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 625, startY + Offset);
            //    e.Graphics.DrawString("F_Amount", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 685, startY + Offset);
            //    Offset = Offset + 20;
            //    e.Graphics.DrawString(underLine, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            //    Offset = Offset + 20;
            //    int a = dataGridView.Rows.Count;
            //    for (; i < a; i++)
            //    {
            //        e.Graphics.DrawString(Convert.ToString(i + 1), new Font("Calibri", 9), new SolidBrush(Color.Black), startX, startY + Offset);
            //        e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[0].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 45, startY + Offset);
            //        e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[1].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 180, startY + Offset);
            //        e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[2].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 230, startY + Offset);
            //        e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[3].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 300, startY + Offset);
            //        e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[4].Value + " - " + dataGridView.Rows[i].Cells[5].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 355, startY + Offset);
            //        e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[6].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 410, startY + Offset);
            //        e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[7].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 475, startY + Offset);
            //        e.Graphics.DrawString(Convert.ToString(Math.Round(Convert.ToDecimal(dataGridView.Rows[i].Cells[6].Value ) * Convert.ToDecimal(dataGridView.Rows[i].Cells[7].Value), 2)), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 550, startY + Offset);
            //        e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[8].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 625, startY + Offset);
            //        e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[9].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 685, startY + Offset);
            //        total += Math.Round(Convert.ToDecimal(dataGridView.Rows[i].Cells[6].Value) * Convert.ToDecimal(dataGridView.Rows[i].Cells[7].Value), 2);

            //        Offset = Offset + 20;
            //        if (Offset >= e.MarginBounds.Height)
            //        {
            //            i++;
            //            e.HasMorePages = true;
            //            Offset = 0;
            //            return;
            //        }
            //        else
            //        {
            //            e.HasMorePages = false;
            //        }
            //    }
            //    Offset = Offset + 50;
            //}
            if (dataGridView2.Rows.Count != 0)
            {
                //if (dataGridView.Rows.Count != 0)
                //    Offset = Offset - 50;
                e.Graphics.DrawString(underLine, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 20;
                e.Graphics.DrawString("Sr#", new Font("Calibri", 9, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                //e.Graphics.DrawString("P_Code", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 45, startY + Offset);
                e.Graphics.DrawString("P_Name", new Font("Calibri", 9, FontStyle.Bold), new SolidBrush(Color.Black), startX + 30, startY + Offset);
                e.Graphics.DrawString("White", new Font("Calibri", 9, FontStyle.Bold), new SolidBrush(Color.Black), startX + 180, startY + Offset);
                e.Graphics.DrawString("Ivory", new Font("Calibri", 9, FontStyle.Bold), new SolidBrush(Color.Black), startX + 220, startY + Offset);
                e.Graphics.DrawString("Blue", new Font("Calibri", 9, FontStyle.Bold), new SolidBrush(Color.Black), startX + 260, startY + Offset);
                e.Graphics.DrawString("S_Green", new Font("Calibri", 9, FontStyle.Bold), new SolidBrush(Color.Black), startX + 300, startY + Offset);
                e.Graphics.DrawString("Pink", new Font("Calibri", 9, FontStyle.Bold), new SolidBrush(Color.Black), startX + 360, startY + Offset);
                e.Graphics.DrawString("Gray", new Font("Calibri", 9, FontStyle.Bold), new SolidBrush(Color.Black), startX + 400, startY + Offset);
                e.Graphics.DrawString("Beige", new Font("Calibri", 9, FontStyle.Bold), new SolidBrush(Color.Black), startX + 440, startY + Offset);
                e.Graphics.DrawString("Burgundy", new Font("Calibri", 9, FontStyle.Bold), new SolidBrush(Color.Black), startX + 480, startY + Offset);
                e.Graphics.DrawString("Peacock", new Font("Calibri", 9, FontStyle.Bold), new SolidBrush(Color.Black), startX + 540, startY + Offset);
                e.Graphics.DrawString("Mauve", new Font("Calibri", 9, FontStyle.Bold), new SolidBrush(Color.Black), startX + 590, startY + Offset);
                e.Graphics.DrawString("Black", new Font("Calibri", 9, FontStyle.Bold), new SolidBrush(Color.Black), startX + 640, startY + Offset);
                e.Graphics.DrawString("Brown", new Font("Calibri", 9, FontStyle.Bold), new SolidBrush(Color.Black), startX + 680, startY + Offset);
                e.Graphics.DrawString("D_Blue", new Font("Calibri", 9, FontStyle.Bold), new SolidBrush(Color.Black), startX + 730, startY + Offset);
                e.Graphics.DrawString("CP", new Font("Calibri", 9, FontStyle.Bold), new SolidBrush(Color.Black), startX + 780, startY + Offset);
                e.Graphics.DrawString("T_Qty", new Font("Calibri", 9, FontStyle.Bold), new SolidBrush(Color.Black), startX + 820, startY + Offset);
                e.Graphics.DrawString("Price", new Font("Calibri", 9, FontStyle.Bold), new SolidBrush(Color.Black), startX + 870, startY + Offset);
                e.Graphics.DrawString("Amount", new Font("Calibri", 9, FontStyle.Bold), new SolidBrush(Color.Black), startX + 940, startY + Offset);
                Offset = Offset + 20;
                e.Graphics.DrawString(underLine, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 20;
                int a = dataGridView2.Rows.Count;
                for (; j < a; j++)
                {
                    e.Graphics.DrawString(Convert.ToString(j + 1), new Font("Calibri", 9), new SolidBrush(Color.Black), startX, startY + Offset);
                    e.Graphics.DrawString(Convert.ToString(dataGridView2.Rows[j].Cells[1].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 30, startY + Offset);
                    e.Graphics.DrawString(Convert.ToString(dataGridView2.Rows[j].Cells[2].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 180, startY + Offset);
                    e.Graphics.DrawString(Convert.ToString(dataGridView2.Rows[j].Cells[3].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 220, startY + Offset);
                    e.Graphics.DrawString(Convert.ToString(dataGridView2.Rows[j].Cells[4].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 260, startY + Offset);
                    e.Graphics.DrawString(Convert.ToString(dataGridView2.Rows[j].Cells[5].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 300, startY + Offset);
                    e.Graphics.DrawString(Convert.ToString(dataGridView2.Rows[j].Cells[6].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 360, startY + Offset);
                    e.Graphics.DrawString(Convert.ToString(dataGridView2.Rows[j].Cells[7].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 400, startY + Offset);
                    e.Graphics.DrawString(Convert.ToString(dataGridView2.Rows[j].Cells[8].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 440, startY + Offset);
                    e.Graphics.DrawString(Convert.ToString(dataGridView2.Rows[j].Cells[9].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 480, startY + Offset);
                    e.Graphics.DrawString(Convert.ToString(dataGridView2.Rows[j].Cells[10].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 540, startY + Offset);
                    e.Graphics.DrawString(Convert.ToString(dataGridView2.Rows[j].Cells[11].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 590, startY + Offset);
                    e.Graphics.DrawString(Convert.ToString(dataGridView2.Rows[j].Cells[12].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 640, startY + Offset);
                    e.Graphics.DrawString(Convert.ToString(dataGridView2.Rows[j].Cells[13].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 680, startY + Offset);
                    e.Graphics.DrawString(Convert.ToString(dataGridView2.Rows[j].Cells[14].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 730, startY + Offset);
                    e.Graphics.DrawString(Convert.ToString(dataGridView2.Rows[j].Cells[15].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 780, startY + Offset);
                    e.Graphics.DrawString(Convert.ToString(dataGridView2.Rows[j].Cells[16].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 820, startY + Offset);
                    e.Graphics.DrawString(Convert.ToString(dataGridView2.Rows[j].Cells[17].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 870, startY + Offset);
                    e.Graphics.DrawString(Convert.ToString(Math.Round(Convert.ToDecimal(dataGridView2.Rows[j].Cells[16].Value) * Convert.ToDecimal(dataGridView2.Rows[j].Cells[17].Value), 2)), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 940, startY + Offset);
                    total += Math.Round(Convert.ToDecimal(dataGridView2.Rows[j].Cells[16].Value) * Convert.ToDecimal(dataGridView2.Rows[j].Cells[17].Value), 2);
                    Offset = Offset + 20;
                    if (Offset >= e.MarginBounds.Height)
                    {
                        j++;
                        e.HasMorePages = true;
                        Offset = 0;
                        return;
                    }
                    else
                    {
                        e.HasMorePages = false;
                    }
                }
                Offset = Offset + 50;
            }
            Offset = Offset - 50;
            e.Graphics.DrawString(underLine, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            e.Graphics.DrawString("Total:", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 830, startY + Offset);
            //e.Graphics.DrawString(total.ToString(), new Font("Calibri", 10), new SolidBrush(Color.Black), startX + 550, startY + Offset);
            e.Graphics.DrawString(labeltotal.Text, new Font("Calibri", 10), new SolidBrush(Color.Black), startX + 935, startY + Offset);
            Offset = Offset + 20;
            e.Graphics.DrawString(underLine, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            e.Graphics.DrawString("Discount:", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 830, startY + Offset);
            e.Graphics.DrawString(labeldiscount.Text, new Font("Calibri", 10), new SolidBrush(Color.Black), startX + 935, startY + Offset);
            Offset = Offset + 20;
            e.Graphics.DrawString("Grand Total:", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 830, startY + Offset);
            e.Graphics.DrawString(labelamount.Text, new Font("Calibri", 10), new SolidBrush(Color.Black), startX + 935, startY + Offset);
            Offset = Offset + 20;
            e.Graphics.DrawString("Credit:", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 830, startY + Offset);
            e.Graphics.DrawString(labelreceive.Text, new Font("Calibri", 10), new SolidBrush(Color.Black), startX + 935, startY + Offset);
            Offset = Offset + 20;
            e.Graphics.DrawString(underLine, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            e.Graphics.DrawString("Balance:", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 830, startY + Offset);
            e.Graphics.DrawString(((Convert.ToDecimal(labelamount.Text)) - Convert.ToDecimal(labelreceive.Text)).ToString(), new Font("Calibri", 10), new SolidBrush(Color.Black), startX + 935, startY + Offset);
            Offset = Offset + 20;
            e.Graphics.DrawString(underLine, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            e.Graphics.DrawString("Signature:  __________________", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            e.Graphics.DrawString("Date & Time:  ", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 800, startY + Offset);
            e.Graphics.DrawString(labeldatetime.Text, new Font("Calibri", 10), new SolidBrush(Color.Black), startX + 880, startY + Offset);
            Offset = Offset + 25;
            //e.Graphics.DrawString("Developed By: Geeks IT Solutions 03126424443  ", new Font("Calibri", 8, FontStyle.Bold), new SolidBrush(Color.Black), startX + 280, startY + Offset + 5);
            e.Graphics.PageUnit = GraphicsUnit.Inch;
            i = j = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                printDocument2.DefaultPageSettings.Landscape = true;
                printDocument2.Print();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //Gate Pass
        private void printDocument2_PrintPage(object sender, PrintPageEventArgs e)
        {
            Image imgPerson = Image.FromFile(@"C:\wonder.png");
            e.Graphics.DrawImage(imgPerson, 20, 28);
            e.Graphics.DrawString("WONDER PLASTIC INDUSTRY", new Font("Arial", 24, FontStyle.Bold), Brushes.Black, 210, 50);
            e.Graphics.DrawString("Manufacturers of Bathroom Accessories & Flush Tank", new Font("Arial", 10), Brushes.Black, 220, 90);
            e.Graphics.DrawString("Address: Nowshera Sansi Road, Gujranwala, Punjab, Pakistan.", new Font("Arial", 9, FontStyle.Italic), Brushes.Black, 220, 105);
            e.Graphics.DrawString("", new Font("Arial", 9, FontStyle.Regular), Brushes.Black, 220, 125);
            //e.Graphics.DrawString("Proprietor: ", new Font("Arial", 9, FontStyle.Bold), Brushes.Black, 265, 140);
            e.Graphics.DrawString("email: contactus@wonderplastic.pk   website: www.wonderplastic.pk", new Font("Arial", 9, FontStyle.Italic), Brushes.Black, 220, 140);
            int startX = 50;
            int startY = 150;
            int Offset = 40;
            e.Graphics.DrawString("Gate Pass", new Font("Calibri", 12, FontStyle.Underline), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 30;
            e.Graphics.DrawString("Invoice No:", new Font("Calibri", 12), new SolidBrush(Color.Black), startX, startY + Offset);
            e.Graphics.DrawString(labelinvoiveno.Text, new Font("Calibri", 12, FontStyle.Bold), new SolidBrush(Color.Black), startX + 85, startY + Offset);
            Offset = Offset + 30;
            e.Graphics.DrawString("A/c Name:", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            e.Graphics.DrawString(labelcustomername.Text, new Font("Calibri", 10), new SolidBrush(Color.Black), startX + 85, startY + Offset);
            Offset = Offset + 30;
            e.Graphics.DrawString("Sub A/c Name:", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            e.Graphics.DrawString(lbSubAc.Text, new Font("Calibri", 10), new SolidBrush(Color.Black), startX + 105, startY + Offset);
            e.Graphics.DrawString("Date & Time:  ", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 600, startY + Offset);
            e.Graphics.DrawString(labeldatetime.Text, new Font("Calibri", 10), new SolidBrush(Color.Black), startX + 680, startY + Offset);
            Offset = Offset + 20;
            string underLine = "=====================================================================================================";

            //if (dataGridView.Rows.Count != 0)
            //{
            //    e.Graphics.DrawString(underLine, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            //    Offset = Offset + 20;
            //    e.Graphics.DrawString("Sr#", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            //    e.Graphics.DrawString("P_Code", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 45, startY + Offset);
            //    e.Graphics.DrawString("Tone", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 280, startY + Offset);
            //    e.Graphics.DrawString("Quality", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 375, startY + Offset);
            //    e.Graphics.DrawString("Size", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 480, startY + Offset);
            //    e.Graphics.DrawString("Box/T", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 580, startY + Offset);
            //    e.Graphics.DrawString("Meters", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 680, startY + Offset);
            //    Offset = Offset + 20;
            //    e.Graphics.DrawString(underLine, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            //    Offset = Offset + 20;
            //    int a = dataGridView.Rows.Count;
            //    for (; i < a; i++)
            //    {
            //        e.Graphics.DrawString(Convert.ToString(i + 1), new Font("Calibri", 9), new SolidBrush(Color.Black), startX, startY + Offset);
            //        e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[0].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 45, startY + Offset);
            //        e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[1].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 280, startY + Offset);
            //        e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[2].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 375, startY + Offset);
            //        e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[3].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 480, startY + Offset);
            //        e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[4].Value + " - " + dataGridView.Rows[i].Cells[5].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 580, startY + Offset);
            //        e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[6].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 680, startY + Offset);
            //        Offset = Offset + 20;
            //        if (Offset >= e.MarginBounds.Height)
            //        {
            //            i++;
            //            e.HasMorePages = true;
            //            Offset = 0;
            //            return;
            //        }
            //        else
            //        {
            //            e.HasMorePages = false;

            //        }
            //    }
            //    Offset = Offset + 50;
            //}
            if (dataGridView2.Rows.Count != 0)
            {
                //if (dataGridView.Rows.Count != 0)
                //    Offset = Offset - 50;
                e.Graphics.DrawString(underLine, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 20;
                e.Graphics.DrawString("Sr#", new Font("Calibri", 9, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                //e.Graphics.DrawString("P_Code", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 45, startY + Offset);
                e.Graphics.DrawString("P_Name", new Font("Calibri", 9, FontStyle.Bold), new SolidBrush(Color.Black), startX + 30, startY + Offset);
                e.Graphics.DrawString("White", new Font("Calibri", 9, FontStyle.Bold), new SolidBrush(Color.Black), startX + 180, startY + Offset);
                e.Graphics.DrawString("Ivory", new Font("Calibri", 9, FontStyle.Bold), new SolidBrush(Color.Black), startX + 220, startY + Offset);
                e.Graphics.DrawString("Blue", new Font("Calibri", 9, FontStyle.Bold), new SolidBrush(Color.Black), startX + 260, startY + Offset);
                e.Graphics.DrawString("S_Green", new Font("Calibri", 9, FontStyle.Bold), new SolidBrush(Color.Black), startX + 300, startY + Offset);
                e.Graphics.DrawString("Pink", new Font("Calibri", 9, FontStyle.Bold), new SolidBrush(Color.Black), startX + 360, startY + Offset);
                e.Graphics.DrawString("Gray", new Font("Calibri", 9, FontStyle.Bold), new SolidBrush(Color.Black), startX + 400, startY + Offset);
                e.Graphics.DrawString("Beige", new Font("Calibri", 9, FontStyle.Bold), new SolidBrush(Color.Black), startX + 440, startY + Offset);
                e.Graphics.DrawString("Burgundy", new Font("Calibri", 9, FontStyle.Bold), new SolidBrush(Color.Black), startX + 480, startY + Offset);
                e.Graphics.DrawString("Peacock", new Font("Calibri", 9, FontStyle.Bold), new SolidBrush(Color.Black), startX + 540, startY + Offset);
                e.Graphics.DrawString("Mauve", new Font("Calibri", 9, FontStyle.Bold), new SolidBrush(Color.Black), startX + 590, startY + Offset);
                e.Graphics.DrawString("Black", new Font("Calibri", 9, FontStyle.Bold), new SolidBrush(Color.Black), startX + 640, startY + Offset);
                e.Graphics.DrawString("Brown", new Font("Calibri", 9, FontStyle.Bold), new SolidBrush(Color.Black), startX + 680, startY + Offset);
                e.Graphics.DrawString("D_Blue", new Font("Calibri", 9, FontStyle.Bold), new SolidBrush(Color.Black), startX + 730, startY + Offset);
                e.Graphics.DrawString("CP", new Font("Calibri", 9, FontStyle.Bold), new SolidBrush(Color.Black), startX + 780, startY + Offset);
                e.Graphics.DrawString("T_Qty", new Font("Calibri", 9, FontStyle.Bold), new SolidBrush(Color.Black), startX + 820, startY + Offset);
                Offset = Offset + 20;
                e.Graphics.DrawString(underLine, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 20;
                int a = dataGridView2.Rows.Count;
                for (; j < a; j++)
                {
                    e.Graphics.DrawString(Convert.ToString(j + 1), new Font("Calibri", 9), new SolidBrush(Color.Black), startX, startY + Offset);
                    //e.Graphics.DrawString(Convert.ToString(dataGridView2.Rows[j].Cells[0].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 45, startY + Offset);
                    e.Graphics.DrawString(Convert.ToString(dataGridView2.Rows[j].Cells[1].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 30, startY + Offset);
                    e.Graphics.DrawString(Convert.ToString(dataGridView2.Rows[j].Cells[2].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 180, startY + Offset);
                    e.Graphics.DrawString(Convert.ToString(dataGridView2.Rows[j].Cells[3].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 220, startY + Offset);
                    e.Graphics.DrawString(Convert.ToString(dataGridView2.Rows[j].Cells[4].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 260, startY + Offset);
                    e.Graphics.DrawString(Convert.ToString(dataGridView2.Rows[j].Cells[5].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 300, startY + Offset);
                    e.Graphics.DrawString(Convert.ToString(dataGridView2.Rows[j].Cells[6].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 360, startY + Offset);
                    e.Graphics.DrawString(Convert.ToString(dataGridView2.Rows[j].Cells[7].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 400, startY + Offset);
                    e.Graphics.DrawString(Convert.ToString(dataGridView2.Rows[j].Cells[8].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 440, startY + Offset);
                    e.Graphics.DrawString(Convert.ToString(dataGridView2.Rows[j].Cells[9].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 480, startY + Offset);
                    e.Graphics.DrawString(Convert.ToString(dataGridView2.Rows[j].Cells[10].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 540, startY + Offset);
                    e.Graphics.DrawString(Convert.ToString(dataGridView2.Rows[j].Cells[11].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 590, startY + Offset);
                    e.Graphics.DrawString(Convert.ToString(dataGridView2.Rows[j].Cells[12].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 640, startY + Offset);
                    e.Graphics.DrawString(Convert.ToString(dataGridView2.Rows[j].Cells[13].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 680, startY + Offset);
                    e.Graphics.DrawString(Convert.ToString(dataGridView2.Rows[j].Cells[14].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 730, startY + Offset);
                    e.Graphics.DrawString(Convert.ToString(dataGridView2.Rows[j].Cells[15].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 780, startY + Offset);
                    e.Graphics.DrawString(Convert.ToString(dataGridView2.Rows[j].Cells[16].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 820, startY + Offset);
                    Offset = Offset + 20;
                    if (Offset >= e.MarginBounds.Height)
                    {
                        j++;
                        e.HasMorePages = true;
                        Offset = 0;
                        return;
                    }
                    else
                    {
                        e.HasMorePages = false;
                    }
                }
                Offset = Offset + 50;
            }
            Offset = Offset - 50;
            e.Graphics.DrawString(underLine, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            e.Graphics.DrawString("نوٹ: (1) ڈرا‏‏ئیورنےمال‏چیک‏کرکےپورالوڈکیاہے۔ (2) مال‏میں‏کسی‏قسم‏کی‏توڑپھوڑنہیں‏ہے۔ (3) مال‏بلکل‏سہی‏لوڈکروایاگیاہے۔", new Font("Microsoft Uighur", 18), new SolidBrush(Color.Black), startX + 120, startY + Offset);
            Offset = Offset + 22;
            e.Graphics.DrawString("مال‏وصول‏کرنےوالامال‏چیک‏کرکے‏وصول‏کرےبعدمیں‏کمپنی‏اورڈرائیورکسی‏قسم‏کےکلیم‏کےذمہ‏دار‏نہیں‏ہونگے۔", new Font("Microsoft Uighur", 18), new SolidBrush(Color.Black), startX + 180, startY + Offset);
            Offset = Offset + 40;
            e.Graphics.DrawString("Customer Signature: ______________________", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            e.Graphics.DrawString("Driver Signature: ______________________", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 600, startY + Offset);
            Offset = Offset + 25;
            //e.Graphics.DrawString("Developed By: Geeks IT Solutions 03126424443  ", new Font("Calibri", 8, FontStyle.Bold), new SolidBrush(Color.Black), startX + 280, startY + Offset + 5);
            e.Graphics.PageUnit = GraphicsUnit.Inch;
            i = j = 0;
        }
    }
}
