﻿using System;
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
    public partial class Sales_Return : Form
    {
        Form opener;
        Int32 db_tiles = 0;
        Boolean data, cmb, sinvoice = false;
        Decimal userQty, c_id, p_tiles, cartQty, salesQty, p_deduct, p_price, total_products, total_disc, salesprice, sales_return_qty, meters_in_box, p_qty, cart_qty, total_products2;
        String p_code, delid, c_name, p_quality, tone, p_size;
        Boolean flag;
        UF.Class1 test = new UF.Class1();

        public Sales_Return(Main parentform)
        {
            InitializeComponent();
            opener = parentform;
        }

        private void Sales_Return_Load(object sender, EventArgs e)
        {
            opener.Hide();
            reset_cart();
        }

        private void Sales_Return_FormClosing(object sender, FormClosingEventArgs e)
        {
            opener.Show();
        }

        //private void set_qty()
        //{
        //    if (cmbcode.Text != "" && cmbsize.Text != "")
        //    {

        //        txtqty.Text = (Convert.ToInt32(txtqty.Text) + (Convert.ToInt32(txttiles.Text) / db_tiles)).ToString();
        //        txttiles.Text = (Convert.ToInt32(txttiles.Text) % db_tiles).ToString();
        //    }
        //}

        void reset_cart()
        {
            ckbx_CheckedChanged(this, null);
            ckbx2_CheckedChanged(this, null);
            txtinvoice.Text = txtsinvoice.Text = "";
            txttotal.Text = txtdeduct.Text = txtgtotal.Text = txtpaid.Text = txtbalance.Text = "0";
            btnsubmit.Enabled = data = cmb = sinvoice = false;
            dateTimePicker.Value = DateTime.Now;
            txtinvoice.Focus();
        }
        private void entercartdata()
        {
            //if (ckbx.Checked == true)
            //{
            //    if (cmbcode.Text != "" && cmbsize.Text != "" && cmbtone.Text != "" && cmbquality.Text != "" && (txtqty.Text != "0" || txttiles.Text != "0"))
            //    {
            //        if (txtinvoice.Text != "" && txtinvoice.Text != "0")
            //        {
            //            Login.con.Open();
            //            //Retreive Quantity from Sales Invoice
            //            SqlCommand cmd6 = new SqlCommand("SELECT isnull(meters,0) from product where p_id = '" + cmbcode.Text + "' AND Size= '" + cmbsize.Text + "'", Login.con);
            //            meters_in_box = (Decimal)cmd6.ExecuteScalar();
            //            SqlCommand cmd121 = new SqlCommand("SELECT isnull(pieces,0) from product where p_id = '" + p_code + "' AND Size= '" + cmbsize.Text + "' ", Login.con);
            //            SqlDataReader dr121 = cmd121.ExecuteReader();
            //            while (dr121.Read())
            //            {
            //                db_tiles = Convert.ToInt16(dr121[0]);
            //            }
            //            dr121.Close();
            //            SqlCommand cmd = new SqlCommand("select qty,price,discount,tiles from sale_stock_detail where p_id='" + cmbcode.Text + "' AND ss_id='" + Convert.ToDecimal(txtinvoice.Text) + "' AND quality='" + cmbquality.Text + "' AND tone='" + cmbtone.Text + "' AND size='" + cmbsize.Text + "' ", Login.con);
            //            SqlDataReader dr = cmd.ExecuteReader();
            //            if (dr.HasRows)
            //            {
            //                while (dr.Read())
            //                {
            //                    userQty = Convert.ToDecimal(txtqty.Text) + (Convert.ToDecimal(txttiles.Text) / Convert.ToDecimal(db_tiles));
            //                    salesQty = Convert.ToDecimal(dr[0]) + (Convert.ToDecimal(dr[3]) / Convert.ToDecimal(db_tiles));
            //                    salesprice = Math.Round((((((Convert.ToDecimal(dr[1]) * salesQty * meters_in_box) - (((Convert.ToDecimal(dr[1]) * salesQty * meters_in_box) * Convert.ToDecimal(dr[2])) / 100)) / salesQty) - (total_disc / (total_products+total_products2))) / meters_in_box), 2);
            //                }
            //                dr.Close();
            //                //Retreive Cart Quantity
            //                SqlCommand cmd2 = new SqlCommand("select isnull(Boxes,0),isnull(Tiles,0) from sr_cart where P_Code ='" + cmbcode.Text + "' AND Size ='" + cmbsize.Text + "' AND Tone ='" + cmbtone.Text + "' AND Quality='" + cmbquality.Text + "'", Login.con);
            //                SqlDataReader dr2 = cmd2.ExecuteReader();
            //                if (dr2.HasRows)
            //                {
            //                    while (dr2.Read())
            //                    {
            //                        cartQty = Convert.ToDecimal(dr2[0]) + (Convert.ToDecimal(dr2[1]) / Convert.ToDecimal(db_tiles));
            //                    }
            //                }
            //                else
            //                {
            //                    cartQty = 0;
            //                }
            //                dr2.Close();
            //                SqlCommand cmd3 = new SqlCommand("select isnull(sum(srd.qty),0),isnull(sum(srd.tiles),0) from sale_return_detail srd,sale_return sr where srd.sr_id=sr.sr_id AND sr.ss_id='" + Convert.ToDecimal(txtinvoice.Text) + "' AND srd.p_id ='" + cmbcode.Text + "'  AND srd.quality ='" + cmbquality.Text + "' AND srd.tone ='" + cmbtone.Text + "' AND srd.size ='" + cmbsize.Text + "' AND ss_id='" + Convert.ToDecimal(txtinvoice.Text) + "'", Login.con);
            //                SqlDataReader dr3 = cmd3.ExecuteReader();
            //                if (dr3.HasRows)
            //                {
            //                    while (dr3.Read())
            //                    {
            //                        sales_return_qty = Convert.ToDecimal(dr3[0]) + (Convert.ToDecimal(dr3[1]) / Convert.ToDecimal(db_tiles));
            //                    }
            //                }
            //                else
            //                {
            //                    sales_return_qty = 0;
            //                }
            //                dr3.Close();
            //                if (((cartQty + userQty) < (salesQty - sales_return_qty)) || ((cartQty + userQty) == (salesQty - sales_return_qty)))
            //                {
            //                    SqlCommand cmd4 = new SqlCommand("select * from product where p_id='" + cmbcode.Text + "' AND size='" + cmbsize.Text + "'", Login.con);
            //                    SqlDataReader dr4 = cmd4.ExecuteReader();
            //                    if (dr4.HasRows)
            //                    {
            //                        while (dr4.Read())
            //                        {
            //                            lbname.Text = dr4.GetString(1);
            //                        }
            //                        dr4.Close();
            //                        SqlCommand cmd5 = new SqlCommand("select P_Code,Tone,Quality,Size,Boxes,Tiles,Meters,Price,Deduct,Amount from sr_cart where P_Code ='" + cmbcode.Text + "' AND Tone ='" + cmbtone.Text + "' AND Size ='" + cmbsize.Text + "' AND Quality ='" + cmbquality.Text + "'", Login.con);
            //                        SqlDataReader dr5 = cmd5.ExecuteReader();
            //                        if (dr5.HasRows)
            //                        {
            //                            dr5.Close();
            //                            MessageBox.Show("This Product Already Exist in Sales Return Cart", "WONDER PLASTIC INDUSTRY", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //                        }
            //                        else
            //                        {
            //                            dr5.Close();
            //                            SqlCommand cmd9 = new SqlCommand("insert into sr_cart(P_Code,Tone,Quality,Size,Boxes,Meters,Price,Amount,Tiles) values('" + cmbcode.Text + "','" + cmbtone.Text + "','" + cmbquality.Text + "','" + cmbsize.Text + "','" + Convert.ToDecimal(txtqty.Text) + "','" + (userQty * meters_in_box) + "', '" + salesprice + "', '" + ((userQty * meters_in_box) * salesprice) + "', '" + Convert.ToDecimal(txttiles.Text) + "')", Login.con);
            //                            cmd9.ExecuteNonQuery();
            //                            SqlCommand cmd7 = new SqlCommand("select isnull(sum(Amount),0) from sr_cart", Login.con);
            //                            SqlCommand cmd8 = new SqlCommand("select isnull(sum(Amount),0) from sr_cart_s", Login.con);
            //                            txttotal.Text = Convert.ToString((Decimal)cmd7.ExecuteScalar() + (Decimal)cmd8.ExecuteScalar());
            //                            txtgtotal.Text = Convert.ToString(Convert.ToDecimal(txttotal.Text) - Convert.ToDecimal(txtdeduct.Text));
            //                            txtbalance.Text = Convert.ToString(Convert.ToDecimal(txtgtotal.Text) - Convert.ToDecimal(txtpaid.Text));
            //                            String command = "select P_Code,Tone,Quality,Size,Boxes,Tiles,Meters,Price,Deduct,Amount from sr_cart";
            //                            test.bindingcode(command, dataGridView, Login.con);
            //                            txtqty.Text = txttiles.Text = "0";
            //                            lbname.Text = cmbcode.Text = "";
            //                            cmbtone.Items.Clear();
            //                            cmbsize.Items.Clear();
            //                            cmbquality.Items.Clear();
            //                            Login.con.Close();
            //                            cmbcode.Focus();
            //                            Login.con.Open();
            //                            data = true;
            //                            if (data && cmb && sinvoice)
            //                            {
            //                                btnsubmit.Enabled = true;
            //                            }
            //                        }
            //                    }
            //                    else
            //                    {
            //                        dr4.Close();
            //                        MessageBox.Show("Record Not Found", "WONDER PLASTIC INDUSTRY", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //                    }

            //                }
            //                else
            //                {
            //                    MessageBox.Show("Your Quantity Increases from Sales Invoice", "WONDER PLASTIC INDUSTRY", MessageBoxButtons.OK, MessageBoxIcon.Question);
            //                }
            //            }
            //            else
            //            {
            //                dr.Close();
            //                MessageBox.Show("This Product is Not in the Sales Invoice", "WONDER PLASTIC INDUSTRY", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //            }
            //        }
            //        else
            //        {
            //            MessageBox.Show("Please Enter Sales Invoice No", "WONDER PLASTIC INDUSTRY", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        }
            //        Login.con.Close();
            //    }
            //}
            //if (ckbx2.Checked == true)
            //{
            if (txtcode.Text != "")
            {
                if (txtinvoice.Text != "" && txtinvoice.Text != "0")
                {
                    Login.con.Open();
                    //Retreive Quantity from Sales Invoice
                    SqlCommand cmd6 = new SqlCommand("select qty,price,discount from sale_stock_detail2 where p_id='" + txtcode.Text + "' AND ss_id='" + Convert.ToDecimal(txtinvoice.Text) + "'", Login.con);
                    SqlDataReader dr6 = cmd6.ExecuteReader();
                    if (dr6.HasRows)
                    {
                        while (dr6.Read())
                        {
                            userQty = Convert.ToDecimal(txtqty2.Text);
                            salesQty = Convert.ToDecimal(dr6[0]);
                            salesprice = Math.Round((((Convert.ToDecimal(dr6[1]) * salesQty) - (((Convert.ToDecimal(dr6[1]) * salesQty) * Convert.ToDecimal(dr6[2])) / 100)) / salesQty) - (total_disc / (total_products + total_products2)), 2);
                        }
                        dr6.Close();
                        //Retreive Cart Quantity
                        SqlCommand cmd7 = new SqlCommand("select isnull(Quantity,0) from sr_cart_s where P_Code ='" + txtcode.Text + "'", Login.con);
                        SqlDataReader dr7 = cmd7.ExecuteReader();
                        if (dr7.HasRows)
                        {
                            while (dr7.Read())
                            {
                                cartQty = Convert.ToDecimal(dr7[0]);
                            }

                        }
                        else
                        {
                            cartQty = 0;
                        }
                        dr7.Close();
                        SqlCommand cmd8 = new SqlCommand("select isnull(sum(srd.qty),0) from sale_return_detail2 srd,sale_return sr where srd.sr_id=sr.sr_id AND sr.ss_id='" + Convert.ToDecimal(txtinvoice.Text) + "' AND srd.p_id ='" + txtcode.Text + "' AND ss_id='" + Convert.ToDecimal(txtinvoice.Text) + "'", Login.con);
                        SqlDataReader dr8 = cmd8.ExecuteReader();
                        if (dr8.HasRows)
                        {
                            while (dr8.Read())
                            {
                                sales_return_qty = Convert.ToDecimal(dr8[0]);
                            }

                        }
                        else
                        {
                            sales_return_qty = 0;
                        }
                        dr8.Close();
                        if (((cartQty + userQty) < (salesQty - sales_return_qty)) || ((cartQty + userQty) == (salesQty - sales_return_qty)))
                        {
                            SqlCommand cmd = new SqlCommand("select * from product2 where p_id='" + txtcode.Text + "'", Login.con);
                            SqlDataReader dr = cmd.ExecuteReader();
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    cmbname.Text = dr.GetString(1);
                                }
                                dr.Close();
                                SqlCommand cmd1 = new SqlCommand("select * from sr_cart_s where P_Code ='" + txtcode.Text + "'", Login.con);
                                SqlDataReader dr1 = cmd1.ExecuteReader();
                                if (dr1.HasRows)
                                {
                                    dr1.Close();
                                    MessageBox.Show("This Product Already Exist in Sales Return Cart", "WONDER PLASTIC INDUSTRY", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                }
                                else
                                {
                                    dr1.Close();
                                    SqlCommand cmd2 = new SqlCommand("insert into sr_cart_s(Quantity,P_Code,P_Name,Price,Amount) values('" + userQty + "','" + txtcode.Text + "','" + cmbname.Text + "', '" + salesprice + "', '" + (userQty * salesprice) + "')", Login.con);
                                    cmd2.ExecuteNonQuery();
                                    //SqlCommand cmd10 = new SqlCommand("select isnull(sum(Amount),0) from sr_cart", Login.con);
                                    SqlCommand cmd11 = new SqlCommand("select isnull(sum(Amount),0) from sr_cart_s", Login.con);
                                    txttotal.Text = Convert.ToString((Decimal)cmd11.ExecuteScalar());
                                    txtgtotal.Text = Convert.ToString(Convert.ToDecimal(txttotal.Text) - Convert.ToDecimal(txtdeduct.Text));
                                    txtbalance.Text = Convert.ToString(Convert.ToDecimal(txtgtotal.Text) - Convert.ToDecimal(txtpaid.Text));
                                }
                                String command = "select * from sr_cart_s";
                                test.bindingcode(command, dataGridView2, Login.con);
                                txtqty2.Text = "1";
                                txtcode.Text = cmbname.Text = "";
                                Login.con.Close();
                                cmbname.Focus();
                                Login.con.Open();
                                data = true;
                                if (data && cmb && sinvoice)
                                {
                                    btnsubmit.Enabled = true;
                                }
                            }
                            else
                            {
                                dr.Close();
                                MessageBox.Show("Record Not Found", "Al Rehman Traders", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            }

                        }
                        else
                        {
                            MessageBox.Show("Your Quantity Increases from Sales Invoice", "Al Rehman Traders", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        }
                    }
                    else
                    {
                        dr6.Close();
                        MessageBox.Show("This Product is Not in the Sales Invoice", "Al Rehman Traders", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                }
                else
                {
                    MessageBox.Show("Please Enter Sales Invoice No.", "Al Rehman Traders", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                Login.con.Close();
            }
            //}
        }

        private void ckbx_CheckedChanged(object sender, EventArgs e)
        {
            //if (ckbx2.Checked == false)
            //    ckbx.Checked = true;
            //if (ckbx.Checked == true)
            //{
            //    txtqty.Enabled = label22.Enabled = txttiles.Enabled = cmbcode.Enabled = cmbsize.Enabled = cmbquality.Enabled = cmbtone.Enabled = lbname.Enabled = dataGridView.Enabled = label2.Enabled = label3.Enabled = label7.Enabled = label15.Enabled = label18.Enabled = btnenter.Enabled = true;
            //    cmbcode.Focus();
            //}
            //else
            //    txtqty.Enabled = label22.Enabled = txttiles.Enabled = cmbcode.Enabled = cmbsize.Enabled = cmbquality.Enabled = cmbtone.Enabled = lbname.Enabled = dataGridView.Enabled = label2.Enabled = label3.Enabled = label7.Enabled = label15.Enabled = label18.Enabled = btnenter.Enabled = false;
            //txtqty.Text = txttiles.Text = "0";
            //lbname.Text = cmbcode.Text = "";
            //cmbtone.Items.Clear();
            //cmbsize.Items.Clear();
            //cmbquality.Items.Clear();
            //Login.con.Open();
            //SqlCommand cmd = new SqlCommand("delete from sr_cart", Login.con);
            //cmd.ExecuteNonQuery();
            //Login.con.Close();
            //string command = "select P_Code,Tone,Quality,Size,Boxes,Tiles,Meters,Price,Deduct,Amount from sr_cart";
            //test.bindingcode(command, dataGridView, Login.con);
        }

        //private void txtqty_MouseDown(object sender, MouseEventArgs e)
        //{
        //    txtqty.SelectAll();
        //}

        //private void txtqty_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    test.digit_correction(sender, e);
        //}

        //private void txtqty_Leave(object sender, EventArgs e)
        //{
        //    if (txtqty2.Text == "" || Convert.ToDecimal(txtqty2.Text) == 0)
        //        txtqty2.Text = "0";
        //}

        //private void txttiles_MouseClick(object sender, MouseEventArgs e)
        //{
        //    txttiles.SelectAll();
        //}

        //private void txttiles_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    test.digit_correction(sender, e);
        //}

        //private void txttiles_Leave(object sender, EventArgs e)
        //{
        //    Login.con.Open();
        //    SqlCommand cmd = new SqlCommand("SELECT pieces from product where p_id='" + cmbcode.Text + "' AND size='" + cmbsize.Text + "'", Login.con);
        //    SqlDataReader dr = cmd.ExecuteReader();
        //    if (dr.HasRows)
        //    {
        //        while (dr.Read())
        //        {
        //            db_tiles = Convert.ToInt32(dr[0]);
        //        }
        //    }
        //    dr.Close();
        //    Login.con.Close();
        //    if (cmbcode.Text != "" && cmbsize.Text != "")
        //    {
        //        set_qty();
        //    }
        //    else if (txttiles.Text == "")
        //    {
        //        txttiles.Text = "0";
        //    }
        //}

        //private void cmbcode_Enter(object sender, EventArgs e)
        //{
        //    if (txtinvoice.Text != "")
        //    {
        //        string command = "select Distinct p_id from sale_stock_detail where ss_id='" + Convert.ToDecimal(txtinvoice.Text) + "'";
        //        test.cmbox_drop(command, cmbcode, Login.con);
        //    }
        //}

        //private void cmbcode_Leave(object sender, EventArgs e)
        //{
        //    if (cmbcode.Text == "")
        //    {
        //        lbname.Text = "";
        //        cmbsize.Items.Clear();
        //    }
        //    else
        //    {
        //        cmbcode.SelectAll();
        //        Login.con.Open();
        //        SqlCommand cmd = new SqlCommand("SELECT * from sale_stock_detail where p_id='" + cmbcode.Text + "'", Login.con);
        //        SqlDataReader dr = cmd.ExecuteReader();
        //        if (!dr.HasRows)
        //        {
        //            dr.Close();
        //            MessageBox.Show("Record Not Found", "WONDER PLASTIC INDUSTRY", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        //            lbname.Text = "";
        //            cmbsize.Items.Clear();
        //            cmbcode.Focus();
        //        }
        //        Login.con.Close();
        //    }
        //}

        //private void cmbname_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        cmbcode.Text = cmbcode.Text.ToUpper();
        //        cmbcode.SelectAll();
        //        cmbquality.Items.Clear();
        //        cmbtone.Items.Clear();
        //        cmbsize.Items.Clear();
        //        Login.con.Open();
        //        SqlCommand cmd = new SqlCommand("SELECT * from sale_stock_detail where p_id='" + cmbcode.Text + "'", Login.con);
        //        SqlDataReader dr = cmd.ExecuteReader();
        //        if (!dr.HasRows)
        //        {
        //            dr.Close();
        //            MessageBox.Show("Record Not Found", "WONDER PLASTIC INDUSTRY", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        //            lbname.Text = "";
        //        }
        //        Login.con.Close();
        //    }
        //}

        //private void cmbsize_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    Login.con.Open();
        //    SqlCommand cmd = new SqlCommand("SELECT p_name from product where p_id='" + cmbcode.Text + "' AND size='" + cmbsize.Text + "'", Login.con);
        //    SqlDataReader dr = cmd.ExecuteReader();
        //    if (dr.HasRows)
        //    {
        //        while (dr.Read())
        //        {
        //            lbname.Text = Convert.ToString(dr[0]);
        //        }
        //        dr.Close();
        //        cmbquality.Items.Clear();
        //        cmbtone.Items.Clear();
        //    }
        //    Login.con.Close();
        //}

        //private void cmbsize_Enter(object sender, EventArgs e)
        //{
        //    if (txtinvoice.Text != "" && cmbcode.Text != "")
        //    {
        //        string command = "select DISTINCT size from sale_stock_detail where ss_id='" + Convert.ToDecimal(txtinvoice.Text) + "' AND p_id='" + cmbcode.Text + "'";
        //        test.cmbox_drop(command, cmbsize, Login.con);
        //    }
        //}

        //private void cmbquality_Enter(object sender, EventArgs e)
        //{
        //    if (txtinvoice.Text != "" && cmbcode.Text != "" && cmbsize.Text != "")
        //    {
        //        string command = "select Distinct quality from sale_stock_detail where ss_id='" + Convert.ToDecimal(txtinvoice.Text) + "' AND p_id='" + cmbcode.Text + "' AND size='" + cmbsize.Text + "'";
        //        test.cmbox_drop(command, cmbquality, Login.con);
        //    }
        //}

        //private void cmbquality_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    cmbtone.Items.Clear();
        //}

        //private void cmbtone_Enter(object sender, EventArgs e)
        //{
        //    if (txtinvoice.Text != "" && cmbcode.Text != "" && cmbsize.Text != "" && cmbquality.Text != "")
        //    {
        //        string command = "select Distinct tone from sale_stock_detail where ss_id='" + Convert.ToDecimal(txtinvoice.Text) + "' AND p_id='" + cmbcode.Text + "' AND size='" + cmbsize.Text + "' AND quality='" + cmbquality.Text + "'";
        //        test.cmbox_drop(command, cmbtone, Login.con);
        //    }
        //}

        private void btnenter_Click(object sender, EventArgs e)
        {
            entercartdata();
        }

        //private void dataGridView_Enter(object sender, EventArgs e)
        //{
        //    dataGridView.Columns["P_Code"].ReadOnly = dataGridView.Columns["Tone"].ReadOnly = dataGridView.Columns["Quality"].ReadOnly = dataGridView.Columns["Size"].ReadOnly = dataGridView.Columns["Meters"].ReadOnly = dataGridView.Columns["Price"].ReadOnly = dataGridView.Columns["Amount"].ReadOnly = true;
        //}

        //private void dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (txtinvoice.Text != "" && txtinvoice.Text != "0")
        //    {
        //        if (e.RowIndex >= 0)
        //        {
        //            DataGridViewRow row = this.dataGridView.Rows[e.RowIndex];
        //            Login.con.Open();
        //            SqlCommand cmd121 = new SqlCommand("SELECT isnull(pieces,0) from product where p_id = '" + p_code + "' AND size='"+p_size+"' ", Login.con);
        //            SqlDataReader dr121 = cmd121.ExecuteReader();
        //            while (dr121.Read())
        //            {
        //                db_tiles = Convert.ToInt16(dr121[0]);
        //            }
        //            dr121.Close();
        //            Login.con.Close();
        //            if (row.Cells[8].Value.ToString() != "")
        //                p_deduct = Convert.ToDecimal(row.Cells[8].Value.ToString());
        //            else
        //                p_deduct = 0;
        //            if (row.Cells[4].Value.ToString() == "0" && row.Cells[5].Value.ToString() == "0")
        //                p_qty = 1;
        //            else if (row.Cells[4].Value.ToString() != "")
        //                p_qty = Convert.ToDecimal(row.Cells[4].Value.ToString());
        //            else
        //                p_qty = 1;
        //            if (row.Cells[5].Value.ToString() != "")
        //            {
        //                p_tiles = Convert.ToDecimal(row.Cells[5].Value.ToString());
        //                p_qty = Convert.ToDecimal(Convert.ToInt32(p_qty) + (Convert.ToInt32(p_tiles) / db_tiles));
        //                p_tiles = Convert.ToDecimal(Convert.ToInt32(p_tiles) % db_tiles);
        //            }
        //            else
        //                p_tiles = 0;
        //            if (row.Cells[7].Value.ToString() != "")
        //                p_price = Convert.ToDecimal(row.Cells[7].Value.ToString());
        //            else
        //                p_price = 0;
        //            p_code = row.Cells[0].Value.ToString();
        //            p_quality = row.Cells[2].Value.ToString();
        //            tone = row.Cells[1].Value.ToString();
        //            p_size = row.Cells[3].Value.ToString();
        //        }
        //        Login.con.Open();
        //        SqlCommand cmd = new SqlCommand("SELECT isnull(meters,0) from product where p_id = '" + p_code + "' AND size='" + p_size + "' ", Login.con);
        //        meters_in_box = (Decimal)cmd.ExecuteScalar();
        //        SqlCommand cmd3 = new SqlCommand("select isnull(sum(srd.qty),0),isnull(sum(srd.tiles),0) from sale_return_detail srd,sale_return sr where srd.sr_id=sr.sr_id AND sr.ss_id='" + Convert.ToDecimal(txtinvoice.Text) + "' AND srd.p_id ='" + p_code + "'  AND srd.quality ='" + p_quality + "' AND srd.tone ='" + tone + "' AND srd.size ='" + p_size + "' ", Login.con);
        //        SqlDataReader dr3 = cmd3.ExecuteReader();
        //        if (dr3.HasRows)
        //        {
        //            while (dr3.Read())
        //            {
        //                sales_return_qty = Convert.ToDecimal(dr3[0]) + (Convert.ToDecimal(dr3[1]) / Convert.ToDecimal(db_tiles));
        //            }
        //        }
        //        else
        //        {
        //            sales_return_qty = 0;
        //        }
        //        dr3.Close();
        //        SqlCommand cmd4 = new SqlCommand("select qty,tiles from sale_stock_detail where p_id='" + p_code + "' AND ss_id='" + Convert.ToDecimal(txtinvoice.Text) + "' AND quality='" + p_quality + "' AND tone='" + tone + "' AND size='" + p_size + "' ", Login.con);
        //        SqlDataReader dr4 = cmd4.ExecuteReader();
        //        if (dr4.HasRows)
        //        {
        //            while (dr4.Read())
        //            {
        //                salesQty = Convert.ToDecimal(dr4[0]) + (Convert.ToDecimal(dr4[1]) / Convert.ToDecimal(db_tiles));
        //            }
        //        }
        //        else
        //        {
        //            salesQty = 0;
        //        }
        //        dr4.Close();
        //        if (((p_qty + (p_tiles / Convert.ToDecimal(db_tiles))) < (salesQty - sales_return_qty)) || ((p_qty + (p_tiles / Convert.ToDecimal(db_tiles))) == (salesQty - sales_return_qty)))
        //        {
        //            SqlCommand cmd1 = new SqlCommand("update sr_cart set Deduct ='" + p_deduct + "',Amount='" + ((((meters_in_box) * (p_qty + (p_tiles / Convert.ToDecimal(db_tiles)))) * (p_price)) - ((((meters_in_box) * (p_qty + (p_tiles / Convert.ToDecimal(db_tiles)))) * (p_price) * (p_deduct)) / 100)) + "',Price='" + p_price + "',Boxes='" + p_qty + "',Tiles='" + p_tiles + "',Meters= '" + (meters_in_box * (p_qty + (p_tiles / Convert.ToDecimal(db_tiles)))) + "' where P_Code='" + p_code + "' AND Quality='" + p_quality + "' AND Tone='" + tone + "'AND Size='" + p_size + "'", Login.con);
        //            cmd1.ExecuteNonQuery();
        //            String command = "select P_Code,Tone,Quality,Size,Boxes,Tiles,Meters,Price,Deduct,Amount from sr_cart";
        //            test.bindingcode(command, dataGridView, Login.con);
        //            SqlCommand cmd2 = new SqlCommand("select isnull(sum(Amount),0) from sr_cart", Login.con);
        //            SqlCommand cmd5 = new SqlCommand("select isnull(sum(Amount),0) from sr_cart_s", Login.con);
        //            txttotal.Text = Convert.ToString((Decimal)cmd2.ExecuteScalar() + (Decimal)cmd5.ExecuteScalar());
        //            txtgtotal.Text = Convert.ToString(Convert.ToDecimal(txttotal.Text) - Convert.ToDecimal(txtdeduct.Text));
        //            txtbalance.Text = Convert.ToString(Convert.ToDecimal(txtgtotal.Text) - Convert.ToDecimal(txtpaid.Text));
        //        }
        //        else
        //        {
        //            MessageBox.Show("You Have Not Enough Quantity Against This Product Code in Sales Invoice", "WONDER PLASTIC INDUSTRY", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //            String command = "select P_Code,Tone,Quality,Size,Boxes,Tiles,Meters,Price,Deduct,Amount from sr_cart";
        //            test.bindingcode(command, dataGridView, Login.con);
        //        }
        //        Login.con.Close();
        //        dataGridView.Columns["P_Code"].ReadOnly = dataGridView.Columns["Tone"].ReadOnly = dataGridView.Columns["Quality"].ReadOnly = dataGridView.Columns["Size"].ReadOnly = dataGridView.Columns["Meters"].ReadOnly = dataGridView.Columns["Price"].ReadOnly = dataGridView.Columns["Amount"].ReadOnly = true;
        //    }
        //}

        //private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (e.RowIndex >= 0)
        //    {
        //        DataGridViewRow row = this.dataGridView.Rows[e.RowIndex];
        //        delid = row.Cells[0].Value.ToString();
        //        p_quality = row.Cells[2].Value.ToString();
        //        tone = row.Cells[1].Value.ToString();
        //        p_size = row.Cells[3].Value.ToString();
        //        Login.con.Open();
        //        SqlCommand cmd121 = new SqlCommand("SELECT isnull(pieces,0) from product where p_id = '" + p_code + "' ", Login.con);
        //        SqlDataReader dr121 = cmd121.ExecuteReader();
        //        while (dr121.Read())
        //        {
        //            db_tiles = Convert.ToInt16(dr121[0]);
        //        }
        //        dr121.Close();
        //        Login.con.Close();
        //        cart_qty = Convert.ToDecimal(row.Cells[4].Value.ToString()) + (Convert.ToDecimal(row.Cells[5].Value.ToString()) / Convert.ToDecimal(db_tiles));
        //    }
        //}

        //private void dataGridView_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Delete)
        //    {
        //        Login.con.Open();
        //        SqlCommand cmd = new SqlCommand("delete from sr_cart where P_Code='" + delid + "' AND Quality='" + p_quality + "' AND Tone='" + tone + "' AND Size='" + p_size + "' ", Login.con);
        //        cmd.ExecuteNonQuery();
        //        string command = "select P_Code,Tone,Quality,Size,Boxes,Tiles,Meters,Price,Deduct,Amount from sr_cart";
        //        test.bindingcode(command, dataGridView, Login.con);
        //        SqlCommand cmd2 = new SqlCommand("select isnull(sum(Amount),0) from sr_cart", Login.con);
        //        SqlCommand cmd5 = new SqlCommand("select isnull(sum(Amount),0) from sr_cart_s", Login.con);
        //        txttotal.Text = Convert.ToString((Decimal)cmd2.ExecuteScalar() + (Decimal)cmd5.ExecuteScalar());
        //        txtgtotal.Text = Convert.ToString(Convert.ToDecimal(txttotal.Text) - Convert.ToDecimal(txtdeduct.Text));
        //        txtbalance.Text = Convert.ToString(Convert.ToDecimal(txtgtotal.Text) - Convert.ToDecimal(txtpaid.Text));
        //        Login.con.Close();
        //        if (dataGridView.RowCount == 0)
        //            if (dataGridView2.RowCount == 0)
        //            {
        //                data = false;
        //                btnsubmit.Enabled = false;
        //            }
        //    }
        //}

        //private void dataGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        //{
        //    e.Control.KeyPress -= new KeyPressEventHandler(test.decimaldigit_correction);
        //    e.Control.KeyPress -= new KeyPressEventHandler(test.digit_correction);
        //    if (dataGridView.CurrentCell.ColumnIndex == 4)
        //    {
        //        TextBox tb = e.Control as TextBox;
        //        if (tb != null)
        //        {
        //            tb.KeyPress += new KeyPressEventHandler(test.digit_correction);
        //        }
        //    }
        //    else if (dataGridView.CurrentCell.ColumnIndex == 5)
        //    {
        //        TextBox tb = e.Control as TextBox;
        //        if (tb != null)
        //        {
        //            tb.KeyPress += new KeyPressEventHandler(test.digit_correction);
        //        }
        //    }
        //    else if (dataGridView.CurrentCell.ColumnIndex == 7)
        //    {
        //        TextBox tb = e.Control as TextBox;
        //        if (tb != null)
        //        {
        //            tb.KeyPress += new KeyPressEventHandler(test.decimaldigit_correction);
        //        }
        //    }
        //    else if (dataGridView.CurrentCell.ColumnIndex == 8)
        //    {
        //        TextBox tb = e.Control as TextBox;
        //        if (tb != null)
        //        {
        //            tb.KeyPress += new KeyPressEventHandler(test.decimaldigit_correction);
        //        }
        //    }
        //}

        private void ckbx2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void txtqty2_KeyPress(object sender, KeyPressEventArgs e)
        {
            test.digit_correction(sender, e);
        }

        private void txtqty2_Leave(object sender, EventArgs e)
        {
            if (txtqty2.Text == "" || Convert.ToDecimal(txtqty2.Text) == 0)
                txtqty2.Text = "1";
        }

        private void txtqty2_MouseDown(object sender, MouseEventArgs e)
        {
            txtqty2.SelectAll();
        }

        private void txtcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtcode.Text = txtcode.Text.ToUpper();
                txtcode.SelectAll();
                Login.con.Open();
                SqlCommand cmd = new SqlCommand("SELECT p_name from product2 where p_id='" + txtcode.Text.ToUpper() + "'", Login.con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        cmbname.Text = Convert.ToString(dr[0]);
                    }
                    dr.Close();
                }
                else
                {
                    dr.Close();
                    MessageBox.Show("Record Not Found", "WONDER PLASTIC INDUSTRY", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    cmbname.Text = "";
                    txtcode.Focus();
                }
                Login.con.Close();
            }
        }

        private void txtcode_Leave(object sender, EventArgs e)
        {
            if (txtcode.Text == "")
            {
                cmbname.Text = "";
            }
            else
            {
                txtcode.Text = txtcode.Text.ToUpper();
                txtcode.SelectAll();
                Login.con.Open();
                SqlCommand cmd = new SqlCommand("SELECT p_name from product2 where p_id='" + txtcode.Text.ToUpper() + "'", Login.con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        cmbname.Text = Convert.ToString(dr[0]);
                    }
                    dr.Close();
                }
                else
                {
                    dr.Close();
                    MessageBox.Show("Record Not Found", "WONDER PLASTIC INDUSTRY", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    cmbname.Text = "";
                    txtcode.Focus();
                }
                Login.con.Close();
            }
        }

        private void cmbname_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbname.Text = cmbname.Text.ToUpper();
                cmbname.SelectAll();
                Login.con.Open();
                SqlCommand cmd = new SqlCommand("SELECT p_id from product2 where p_name='" + cmbname.Text.ToUpper() + "'", Login.con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtcode.Text = Convert.ToString(dr[0]);
                    }
                    dr.Close();
                }
                else
                {
                    dr.Close();
                    MessageBox.Show("Record Not Found", "WONDER PLASTIC INDUSTRY", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtcode.Text = "";
                    cmbname.Focus();
                }
                Login.con.Close();
            }
        }

        private void cmbname_Leave(object sender, EventArgs e)
        {
            if (cmbname.Text == "")
            {
                txtcode.Text = "";
            }
            else
            {
                cmbname.Text = cmbname.Text.ToUpper();
                cmbname.SelectAll();
                Login.con.Open();
                SqlCommand cmd = new SqlCommand("SELECT p_id from product2 where p_name='" + cmbname.Text.ToUpper() + "'", Login.con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtcode.Text = Convert.ToString(dr[0]);
                    }
                    dr.Close();
                }
                else
                {
                    dr.Close();
                    MessageBox.Show("Record Not Found", "WONDER PLASTIC INDUSTRY", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtcode.Text = "";
                    cmbname.Focus();
                }
                Login.con.Close();
            }
        }

        private void cmbname_Enter(object sender, EventArgs e)
        {
            if (txtinvoice.Text != "")
            {
                string command = "select Distinct p.p_name from sale_stock_detail2 s, product2 p where s.ss_id='" + Convert.ToDecimal(txtinvoice.Text) + "' AND s.p_id=p.p_id";
                test.cmbox_drop(command, cmbname, Login.con);
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView2.Rows[e.RowIndex];
                delid = row.Cells[1].Value.ToString();
            }
        }

        private void dataGridView2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (txtinvoice.Text != "" && txtinvoice.Text != "0")
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = this.dataGridView2.Rows[e.RowIndex];
                    if (row.Cells[4].Value.ToString() != "")
                        p_deduct = Convert.ToDecimal(row.Cells[4].Value.ToString());
                    else
                        p_deduct = 0;
                    if (row.Cells[2].Value.ToString() == "0")
                        p_qty = 1;
                    else if (row.Cells[2].Value.ToString() != "")
                        p_qty = Convert.ToDecimal(row.Cells[2].Value.ToString());
                    else
                        p_qty = 1;
                    if (row.Cells[3].Value.ToString() != "")
                        p_price = Convert.ToDecimal(row.Cells[3].Value.ToString());
                    else
                        p_price = 0;
                    p_code = row.Cells[0].Value.ToString();
                }
                Login.con.Open();
                SqlCommand cmd3 = new SqlCommand("select isnull(sum(srd.qty),0) from sale_return_detail2 srd,sale_return sr where srd.sr_id=sr.sr_id AND sr.ss_id='" + Convert.ToDecimal(txtinvoice.Text) + "' AND srd.p_id ='" + p_code + "' ", Login.con);
                SqlDataReader dr3 = cmd3.ExecuteReader();
                if (dr3.HasRows)
                {
                    while (dr3.Read())
                    {
                        sales_return_qty = Convert.ToDecimal(dr3[0]);
                    }
                }
                else
                {
                    sales_return_qty = 0;
                }
                dr3.Close();
                SqlCommand cmd4 = new SqlCommand("select qty from sale_stock_detail2 where p_id='" + p_code + "' AND ss_id='" + Convert.ToDecimal(txtinvoice.Text) + "' ", Login.con);
                SqlDataReader dr4 = cmd4.ExecuteReader();
                if (dr4.HasRows)
                {
                    while (dr4.Read())
                    {
                        salesQty = Convert.ToDecimal(dr4[0]);
                    }
                }
                else
                {
                    salesQty = 0;
                }
                dr4.Close();
                if ((p_qty < (salesQty - sales_return_qty)) || (p_qty == (salesQty - sales_return_qty)))
                {
                    SqlCommand cmd1 = new SqlCommand("update sr_cart_s set Deduct ='" + p_deduct + "',Amount='" + ((p_qty * (p_price)) - (((p_qty) * (p_price) * (p_deduct)) / 100)) + "',Price='" + p_price + "',Quantity='" + p_qty + "' where P_Code='" + p_code + "' ", Login.con);
                    cmd1.ExecuteNonQuery();
                    String command = "select * from sr_cart_s";
                    test.bindingcode(command, dataGridView2, Login.con);
                    SqlCommand cmd2 = new SqlCommand("select isnull(sum(Amount),0) from sr_cart", Login.con);
                    SqlCommand cmd5 = new SqlCommand("select isnull(sum(Amount),0) from sr_cart_s", Login.con);
                    txttotal.Text = Convert.ToString((Decimal)cmd2.ExecuteScalar() + (Decimal)cmd5.ExecuteScalar());
                    txtgtotal.Text = Convert.ToString(Convert.ToDecimal(txttotal.Text) - Convert.ToDecimal(txtdeduct.Text));
                    txtbalance.Text = Convert.ToString(Convert.ToDecimal(txtgtotal.Text) - Convert.ToDecimal(txtpaid.Text));
                }
                else
                {
                    MessageBox.Show("You Have Not Enough Quantity Against This Product Code in Sales Invoice", "WONDER PLASTIC INDUSTRY", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    String command = "select * from sr_cart_s";
                    test.bindingcode(command, dataGridView2, Login.con);
                }
                Login.con.Close();
                dataGridView2.Columns["P_Code"].ReadOnly = dataGridView2.Columns["P_Name"].ReadOnly = dataGridView2.Columns["Price"].ReadOnly = dataGridView2.Columns["Amount"].ReadOnly = true;
            }
        }

        private void dataGridView2_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(test.decimaldigit_correction);
            e.Control.KeyPress -= new KeyPressEventHandler(test.digit_correction);
            if (dataGridView2.CurrentCell.ColumnIndex == 2)
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(test.digit_correction);
                }
            }
            else if (dataGridView2.CurrentCell.ColumnIndex == 3)
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(test.decimaldigit_correction);
                }
            }
            else if (dataGridView2.CurrentCell.ColumnIndex == 4)
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(test.decimaldigit_correction);
                }
            }
        }

        private void dataGridView2_Enter(object sender, EventArgs e)
        {
            dataGridView2.Columns["P_Code"].ReadOnly = dataGridView2.Columns["P_Name"].ReadOnly = dataGridView2.Columns["Price"].ReadOnly = dataGridView2.Columns["Amount"].ReadOnly = true;
        }

        private void dataGridView2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                Login.con.Open();
                SqlCommand cmd = new SqlCommand("delete from sr_cart_s where P_Code='" + delid + "' ", Login.con);
                cmd.ExecuteNonQuery();
                string command = "select * from sr_cart_s";
                test.bindingcode(command, dataGridView2, Login.con);
                //SqlCommand cmd4 = new SqlCommand("select isnull(sum(Amount),0) from sr_cart", Login.con);
                SqlCommand cmd5 = new SqlCommand("select isnull(sum(Amount),0) from sr_cart_s", Login.con);
                txttotal.Text = Convert.ToString((Decimal)cmd5.ExecuteScalar());
                txtgtotal.Text = Convert.ToString(Convert.ToDecimal(txttotal.Text) - Convert.ToDecimal(txtdeduct.Text));
                txtbalance.Text = Convert.ToString(Convert.ToDecimal(txtgtotal.Text) - Convert.ToDecimal(txtpaid.Text));
                Login.con.Close();
                if (dataGridView2.RowCount == 0)
                //if (dataGridView.RowCount == 0)
                {
                    data = false;
                    btnsubmit.Enabled = false;
                }
            }
        }

        private void txtinvoice_KeyPress(object sender, KeyPressEventArgs e)
        {
            test.digit_correction(sender, e);
        }

        private void txtinvoice_Leave(object sender, EventArgs e)
        {
            if (txtinvoice.Text != "")
            {
                if (Convert.ToDecimal(txtinvoice.Text) != 0)
                {
                    Login.con.Open();
                    SqlCommand cmd = new SqlCommand("select ss_id FROM sale_stock where ss_id='" + Convert.ToDecimal(txtinvoice.Text) + "'", Login.con);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        dr.Close();
                        //SqlCommand cmd2 = new SqlCommand("select isnull(sum(qty + (ssd.tiles/p.pieces)),0) from sale_stock_detail ssd, product p where p.p_id=ssd.p_id AND ssd.ss_id='" + Convert.ToDecimal(txtinvoice.Text) + "'", Login.con);
                        //total_products = (Decimal)cmd2.ExecuteScalar();
                        SqlCommand cmd7 = new SqlCommand("select isnull(sum(qty),0) from sale_stock_detail2 where ss_id='" + Convert.ToDecimal(txtinvoice.Text) + "'", Login.con);
                        total_products2 = (Decimal)cmd7.ExecuteScalar();
                        SqlCommand cmd3 = new SqlCommand("select isnull(ss.discount,0),c.c_name from sale_stock ss,customer c where c.c_id=ss.c_id AND ss_id='" + Convert.ToDecimal(txtinvoice.Text) + "'", Login.con);
                        SqlDataReader dr3 = cmd3.ExecuteReader();
                        if (dr3.HasRows)
                        {
                            while (dr3.Read())
                            {
                                total_disc = Convert.ToDecimal(dr3[0]);
                                c_name = Convert.ToString(dr3[1]);
                            }
                        }
                        dr3.Close();
                        SqlCommand cmd4 = new SqlCommand("select c.c_id from sale_stock ss,customer c where c.c_id=ss.c_id AND ss_id='" + Convert.ToDecimal(txtinvoice.Text) + "'", Login.con);
                        SqlDataReader dr4 = cmd4.ExecuteReader();
                        if (dr4.HasRows)
                        {
                            while (dr4.Read())
                            {
                                if (Convert.ToString(dr4[0]) != DBNull.Value.ToString())
                                {
                                    c_id = Convert.ToDecimal(dr4[0]);
                                    flag = true;
                                }
                            }
                        }
                        dr4.Close();
                        SqlCommand cmd5 = new SqlCommand("select c.m_id from sale_stock ss,manufacturer c where c.m_id=ss.m_id AND ss_id='" + Convert.ToDecimal(txtinvoice.Text) + "'", Login.con);
                        SqlDataReader dr5 = cmd5.ExecuteReader();
                        if (dr5.HasRows)
                        {
                            while (dr5.Read())
                            {
                                if (Convert.ToString(dr5[0]) != DBNull.Value.ToString())
                                {
                                    c_id = Convert.ToDecimal(dr5[0]);
                                    flag = false;
                                }
                            }
                        }
                        dr5.Close();
                        cmb = true;
                        if (data && cmb && sinvoice)
                        {
                            btnsubmit.Enabled = true;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please Enter Correct Invoice No", "WONDER PLASTIC INDUSTRY", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        dr.Close();
                        txtinvoice.Clear();
                        txtinvoice.Focus();
                        cmb = false;
                        btnsubmit.Enabled = false;
                    }
                    Login.con.Close();
                }
            }
        }

        private void txtinvoice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtsinvoice.Text = "";
                txttotal.Text = txtdeduct.Text = txtgtotal.Text = txtpaid.Text = txtbalance.Text = "0";
                btnsubmit.Enabled = data = cmb = sinvoice = false;
                dateTimePicker.Value = DateTime.Now;
                ckbx_CheckedChanged(this, null);
                ckbx2_CheckedChanged(this, null);
            }
        }

        private void txtdeduct_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtpaid.Focus();
            }
        }

        private void txtpaid_KeyPress(object sender, KeyPressEventArgs e)
        {
            test.decimaldigit_correction(sender, e);
        }

        private void txtpaid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtsinvoice.Focus();
            }
        }

        private void txtpaid_Leave(object sender, EventArgs e)
        {
            if (txtpaid.Text == "")
                txtpaid.Text = "0";
            else if (Convert.ToDecimal(txtpaid.Text) == 0)
                txtpaid.Text = "0";
            txtbalance.Text = Convert.ToString(Convert.ToDecimal(txtgtotal.Text) - Convert.ToDecimal(txtpaid.Text));
        }

        private void txtpaid_MouseDown(object sender, MouseEventArgs e)
        {
            txtpaid.SelectAll();
        }

        private void txtdeduct_KeyPress(object sender, KeyPressEventArgs e)
        {
            test.decimaldigit_correction(sender, e);
        }

        private void txtdeduct_MouseDown(object sender, MouseEventArgs e)
        {
            txtdeduct.SelectAll();
        }

        private void txtdeduct_Leave(object sender, EventArgs e)
        {
            if (txtdeduct.Text == "")
                txtdeduct.Text = "0";
            else if (Convert.ToDecimal(txtdeduct.Text) == 0)
                txtdeduct.Text = "0";
            txtgtotal.Text = Convert.ToString(Convert.ToDecimal(txttotal.Text) - Convert.ToDecimal(txtdeduct.Text));
            txtbalance.Text = Convert.ToString(Convert.ToDecimal(txtgtotal.Text) - Convert.ToDecimal(txtpaid.Text));
        }


        private void dateTimePicker_Leave(object sender, EventArgs e)
        {
            if (DateTime.Now < dateTimePicker.Value)
                dateTimePicker.Value = DateTime.Now;
        }

        private void txtsinvoiceno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtsinvoice.Text != "")
                {
                    if (Convert.ToDecimal(txtsinvoice.Text) != 0)
                    {
                        Login.con.Open();
                        SqlCommand cmd = new SqlCommand("SELECT sr_id from sale_return where sr_id='" + Convert.ToDecimal(txtsinvoice.Text) + "'", Login.con);
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            dr.Close();
                            MessageBox.Show("Sales Return No Already Exist", "WONDER PLASTIC INDUSTRY", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            txtsinvoice.Clear();
                            txtsinvoice.Focus();
                            sinvoice = false;
                            btnsubmit.Enabled = false;
                        }
                        else
                        {
                            dr.Close();
                            sinvoice = true;
                            if (data && cmb && sinvoice)
                            {
                                btnsubmit.Enabled = true;
                            }
                            btnsubmit.Focus();
                        }
                        Login.con.Close();
                    }
                }
            }
        }

        private void txtsinvoiceno_KeyPress(object sender, KeyPressEventArgs e)
        {
            test.digit_correction(sender, e);
        }

        private void btnsubmit_Click(object sender, EventArgs e)
        {
            Login.con.Open();
            if (txtpaid.Text == "0")
            {
                if (flag == true)
                {
                    SqlCommand cmd = new SqlCommand("INSERT into cus_transaction (datetime) values('" + dateTimePicker.Value + "')", Login.con);
                    cmd.ExecuteNonQuery();
                    SqlCommand cmd2 = new SqlCommand("SELECT cus_id from cus_transaction where datetime='" + dateTimePicker.Value + "'", Login.con);
                    Decimal cus_id = (Decimal)cmd2.ExecuteScalar();
                    SqlCommand cmd3 = new SqlCommand("insert into sale_return(sr_id,ss_id,datetime,total,deduct,paid,c_id,cus_id) values('" + Convert.ToDecimal(txtsinvoice.Text) + "','" + Convert.ToDecimal(txtinvoice.Text) + "', '" + dateTimePicker.Value + "', '" + Convert.ToDecimal(txttotal.Text) + "', '" + Convert.ToDecimal(txtdeduct.Text) + "', '" + Convert.ToDecimal(txtpaid.Text) + "', '" + c_id + "','" + cus_id + "')", Login.con);
                    cmd3.ExecuteNonQuery();
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("INSERT into man_transaction (datetime) values('" + dateTimePicker.Value + "')", Login.con);
                    cmd.ExecuteNonQuery();
                    SqlCommand cmd2 = new SqlCommand("SELECT man_id from man_transaction where datetime='" + dateTimePicker.Value + "'", Login.con);
                    Decimal cus_id = (Decimal)cmd2.ExecuteScalar();
                    SqlCommand cmd3 = new SqlCommand("insert into sale_return(sr_id,ss_id,datetime,total,deduct,paid,m_id,man_id) values('" + Convert.ToDecimal(txtsinvoice.Text) + "','" + Convert.ToDecimal(txtinvoice.Text) + "', '" + dateTimePicker.Value + "', '" + Convert.ToDecimal(txttotal.Text) + "', '" + Convert.ToDecimal(txtdeduct.Text) + "', '" + Convert.ToDecimal(txtpaid.Text) + "', '" + c_id + "','" + cus_id + "')", Login.con);
                    cmd3.ExecuteNonQuery();
                }
            }
            else
            {
                if (flag == true)
                {
                    SqlCommand cmd = new SqlCommand("insert into cash_transaction (datetime) values('" + dateTimePicker.Value + "')", Login.con);
                    cmd.ExecuteNonQuery();
                    SqlCommand cmd2 = new SqlCommand("select cat_id from cash_transaction where datetime='" + dateTimePicker.Value + "'", Login.con);
                    Decimal cat_id = (Decimal)cmd2.ExecuteScalar();
                    SqlCommand cmd3 = new SqlCommand("INSERT into cus_transaction (datetime) values('" + dateTimePicker.Value + "')", Login.con);
                    cmd3.ExecuteNonQuery();
                    SqlCommand cmd4 = new SqlCommand("SELECT cus_id from cus_transaction where datetime='" + dateTimePicker.Value + "'", Login.con);
                    Decimal cus_id = (Decimal)cmd4.ExecuteScalar();
                    SqlCommand cmd5 = new SqlCommand("insert into sale_return(sr_id,ss_id,datetime,total,deduct,paid,cat_id,c_id,cus_id) values('" + Convert.ToDecimal(txtsinvoice.Text) + "','" + Convert.ToDecimal(txtinvoice.Text) + "', '" + dateTimePicker.Value + "', '" + Convert.ToDecimal(txttotal.Text) + "', '" + Convert.ToDecimal(txtdeduct.Text) + "','" + Convert.ToDecimal(txtpaid.Text) + "','" + cat_id + "', '" + c_id + "','" + cus_id + "')", Login.con);
                    cmd5.ExecuteNonQuery();
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("insert into cash_transaction (datetime) values('" + dateTimePicker.Value + "')", Login.con);
                    cmd.ExecuteNonQuery();
                    SqlCommand cmd2 = new SqlCommand("select cat_id from cash_transaction where datetime='" + dateTimePicker.Value + "'", Login.con);
                    Decimal cat_id = (Decimal)cmd2.ExecuteScalar();
                    SqlCommand cmd3 = new SqlCommand("INSERT into man_transaction (datetime) values('" + dateTimePicker.Value + "')", Login.con);
                    cmd3.ExecuteNonQuery();
                    SqlCommand cmd4 = new SqlCommand("SELECT man_id from man_transaction where datetime='" + dateTimePicker.Value + "'", Login.con);
                    Decimal cus_id = (Decimal)cmd4.ExecuteScalar();
                    SqlCommand cmd5 = new SqlCommand("insert into sale_return(sr_id,ss_id,datetime,total,deduct,paid,cat_id,m_id,man_id) values('" + Convert.ToDecimal(txtsinvoice.Text) + "','" + Convert.ToDecimal(txtinvoice.Text) + "', '" + dateTimePicker.Value + "', '" + Convert.ToDecimal(txttotal.Text) + "', '" + Convert.ToDecimal(txtdeduct.Text) + "', '" + Convert.ToDecimal(txtpaid.Text) + "','" + cat_id + "', '" + c_id + "','" + cus_id + "')", Login.con);
                    cmd5.ExecuteNonQuery();
                }
            }
            //if (ckbx.Checked == true)
            //{
            //    if (dataGridView.Rows.Count != 0)
            //    {
            //while (true)
            //{
            //    SqlCommand cmd7 = new SqlCommand("select P_Code,Tone,Quality,Size,Boxes,Meters,Price,Deduct,Tiles from sr_cart", Login.con);
            //    SqlDataReader dr7 = cmd7.ExecuteReader();
            //    if (dr7.HasRows)
            //    {
            //        if (dr7.Read())
            //        {
            //            p_code = Convert.ToString(dr7[0]);
            //            p_qty = Convert.ToDecimal(dr7[4]);
            //            p_price = Convert.ToDecimal(dr7[6]);
            //            p_deduct = Convert.ToDecimal(dr7[7]);
            //            p_quality = Convert.ToString(dr7[2]);
            //            p_size = Convert.ToString(dr7[3]);
            //            tone = Convert.ToString(dr7[1]);
            //            p_tiles = Convert.ToDecimal(dr7[8]);
            //            dr7.Close();
            //            SqlCommand cmd8 = new SqlCommand("insert into sale_return_detail values('" + Convert.ToDecimal(txtsinvoice.Text) + "','" + p_code + "','" + p_qty + "','" + p_deduct + "','" + p_price + "','" + p_quality + "','" + tone + "','" + p_size + "','" + p_tiles + "')", Login.con);
            //            cmd8.ExecuteNonQuery();
            //            SqlCommand cmd9 = new SqlCommand("delete sr_cart where P_Code='" + p_code + "' AND Quality='" + p_quality + "' AND Tone='" + tone + "' AND Size='" + p_size + "'", Login.con);
            //            cmd9.ExecuteNonQuery();
            //        }
            //    }
            //    else
            //    {
            //        dr7.Close();
            //        break;
            //    }
            //}
            //    }
            //}
            //if (ckbx2.Checked == true)
            //{
            if (dataGridView2.Rows.Count != 0)
            {
                while (true)
                {
                    SqlCommand cmd5 = new SqlCommand("select * from sr_cart_s", Login.con);
                    SqlDataReader dr4 = cmd5.ExecuteReader();
                    if (dr4.HasRows)
                    {
                        if (dr4.Read())
                        {
                            p_code = Convert.ToString(dr4[0]);
                            p_qty = Convert.ToDecimal(dr4[2]);
                            p_price = Convert.ToDecimal(dr4[3]);
                            p_deduct = Convert.ToDecimal(dr4[4]);
                            dr4.Close();
                            SqlCommand cmd6 = new SqlCommand("insert into sale_return_detail2 values('" + Convert.ToDecimal(txtsinvoice.Text) + "','" + p_code + "','" + p_qty + "','" + p_deduct + "','" + p_price + "')", Login.con);
                            cmd6.ExecuteNonQuery();
                            SqlCommand cmd7 = new SqlCommand("delete sr_cart_s where P_Code='" + p_code + "'", Login.con);
                            cmd7.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        dr4.Close();
                        break;
                    }
                }
            }
            //}
            Login.con.Close();
            DialogResult dresult = MessageBox.Show("Press 'OK' to Print SALES RETURN INVOICE", "WONDER PLASTIC INDUSTRY", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dresult == DialogResult.OK)
            {
                try
                {
                    printDocument1.Print();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            reset_cart();
        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            reset_cart();
        }

        private int i, j = 0;
        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            Image imgPerson = Image.FromFile(@"C:\wonder.png");
            int startX = 50;
            int startY = 150;
            int Offset = 40;
            e.Graphics.DrawImage(imgPerson, 20, 28);
            e.Graphics.DrawString("WONDER PLASTIC INDUSTRY", new Font("Arial", 24, FontStyle.Bold), Brushes.Black, 210, 50);
            e.Graphics.DrawString("Manufacturers of Bathroom Accessories & Flush Tank", new Font("Arial", 10), Brushes.Black, 220, 90);
            e.Graphics.DrawString("Address: Nowshera Sansi Road, Gujranwala, Punjab, Pakistan.", new Font("Arial", 9, FontStyle.Italic), Brushes.Black, 220, 105);
            e.Graphics.DrawString("", new Font("Arial", 9, FontStyle.Regular), Brushes.Black, 220, 125);
            //e.Graphics.DrawString("Proprietor: ", new Font("Arial", 9, FontStyle.Bold), Brushes.Black, 265, 140);
            e.Graphics.DrawString("email: contactus@wonderplastic.pk   website: www.wonderplastic.pk", new Font("Arial", 9, FontStyle.Italic), Brushes.Black, 220, 140);
            e.Graphics.DrawString("Sales Return No:", new Font("Calibri", 12), new SolidBrush(Color.Black), startX, startY + Offset);
            e.Graphics.DrawString(txtsinvoice.Text, new Font("Calibri", 12, FontStyle.Bold), new SolidBrush(Color.Black), startX + 115, startY + Offset);
            Offset = Offset + 30;
            e.Graphics.DrawString("Invoice No:", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            e.Graphics.DrawString(txtinvoice.Text, new Font("Calibri", 10), new SolidBrush(Color.Black), startX + 70, startY + Offset);
            Offset = Offset + 30;
            e.Graphics.DrawString("Customer Name:", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            e.Graphics.DrawString(c_name, new Font("Calibri", 10), new SolidBrush(Color.Black), startX + 105, startY + Offset);
            Offset = Offset + 20;
            string underLine = "=======================================================================================";
            //if (dataGridView.Rows.Count != 0)
            //{
            //    e.Graphics.DrawString(underLine, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            //    Offset = Offset + 20;
            //    e.Graphics.DrawString("Sr#", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            //    e.Graphics.DrawString("P_Code", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 45, startY + Offset);
            //    e.Graphics.DrawString("Tone", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 215, startY + Offset);
            //    e.Graphics.DrawString("Quality", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 280, startY + Offset);
            //    e.Graphics.DrawString("Size", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 360, startY + Offset);
            //    e.Graphics.DrawString("Box/T", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 410, startY + Offset);
            //    e.Graphics.DrawString("Meters", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 465, startY + Offset);
            //    e.Graphics.DrawString("Price", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 530, startY + Offset);
            //    e.Graphics.DrawString("Ded%", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 605, startY + Offset);
            //    e.Graphics.DrawString("Amount", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 665, startY + Offset);
            //    Offset = Offset + 20;
            //    e.Graphics.DrawString(underLine, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            //    Offset = Offset + 20;
            //    int a = dataGridView.Rows.Count;
            //    for (; i < a; i++)
            //    {
            //        e.Graphics.DrawString(Convert.ToString(i + 1), new Font("Calibri", 9), new SolidBrush(Color.Black), startX, startY + Offset);
            //        e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[0].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 45, startY + Offset);
            //        e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[1].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 215, startY + Offset);
            //        e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[2].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 280, startY + Offset);
            //        e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[3].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 360, startY + Offset);
            //        e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[4].Value + " - " + dataGridView.Rows[i].Cells[5].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 410, startY + Offset);
            //        e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[6].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 465, startY + Offset);
            //        e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[7].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 530, startY + Offset);
            //        e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[8].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 605, startY + Offset);
            //        e.Graphics.DrawString(Convert.ToString(dataGridView.Rows[i].Cells[9].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 665, startY + Offset);
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
                e.Graphics.DrawString("Sr#", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                e.Graphics.DrawString("P_Code", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 45, startY + Offset);
                e.Graphics.DrawString("P_Name", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 160, startY + Offset);
                e.Graphics.DrawString("Qty", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 410, startY + Offset);
                e.Graphics.DrawString("Price", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 530, startY + Offset);
                e.Graphics.DrawString("Deduct", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 605, startY + Offset);
                e.Graphics.DrawString("Amount", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 665, startY + Offset);
                Offset = Offset + 20;
                e.Graphics.DrawString(underLine, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 20;
                int a = dataGridView2.Rows.Count;
                for (; j < a; j++)
                {
                    e.Graphics.DrawString(Convert.ToString(j + 1), new Font("Calibri", 9), new SolidBrush(Color.Black), startX, startY + Offset);
                    e.Graphics.DrawString(Convert.ToString(dataGridView2.Rows[j].Cells[0].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 45, startY + Offset);
                    e.Graphics.DrawString(Convert.ToString(dataGridView2.Rows[j].Cells[1].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 160, startY + Offset);
                    e.Graphics.DrawString(Convert.ToString(dataGridView2.Rows[j].Cells[2].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 410, startY + Offset);
                    e.Graphics.DrawString(Convert.ToString(dataGridView2.Rows[j].Cells[3].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 530, startY + Offset);
                    e.Graphics.DrawString(Convert.ToString(dataGridView2.Rows[j].Cells[4].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 605, startY + Offset);
                    e.Graphics.DrawString(Convert.ToString(dataGridView2.Rows[j].Cells[5].Value), new Font("Calibri", 9), new SolidBrush(Color.Black), startX + 665, startY + Offset);
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
            e.Graphics.DrawString("Total:", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 540, startY + Offset);
            e.Graphics.DrawString(txttotal.Text, new Font("Calibri", 10), new SolidBrush(Color.Black), startX + 665, startY + Offset);
            Offset = Offset + 20;
            e.Graphics.DrawString(underLine, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            e.Graphics.DrawString("Deduct:", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 540, startY + Offset);
            e.Graphics.DrawString(txtdeduct.Text, new Font("Calibri", 10), new SolidBrush(Color.Black), startX + 665, startY + Offset);
            Offset = Offset + 20;
            e.Graphics.DrawString("Grand Total:", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 540, startY + Offset);
            e.Graphics.DrawString(txtgtotal.Text, new Font("Calibri", 10), new SolidBrush(Color.Black), startX + 665, startY + Offset);
            Offset = Offset + 20;
            e.Graphics.DrawString("Debit:", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 540, startY + Offset);
            e.Graphics.DrawString(txtpaid.Text, new Font("Calibri", 10), new SolidBrush(Color.Black), startX + 665, startY + Offset);
            Offset = Offset + 20;
            e.Graphics.DrawString(underLine, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            e.Graphics.DrawString("Balance:", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 540, startY + Offset);
            e.Graphics.DrawString((Convert.ToDecimal(txtpaid.Text) - Convert.ToDecimal(txtgtotal.Text)).ToString(), new Font("Calibri", 10), new SolidBrush(Color.Black), startX + 665, startY + Offset);
            Offset = Offset + 20;
            e.Graphics.DrawString(underLine, new Font("Courier New", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            e.Graphics.DrawString("Signature:  __________________", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX, startY + Offset);
            e.Graphics.DrawString("Date & Time:  ", new Font("Calibri", 10, FontStyle.Bold), new SolidBrush(Color.Black), startX + 320, startY + Offset);
            e.Graphics.DrawString(DateTime.Now.ToString(), new Font("Calibri", 10), new SolidBrush(Color.Black), startX + 400, startY + Offset);
            Offset = Offset + 25;
            e.Graphics.DrawString("Developed By: Geeks IT Solutions 03126424443  ", new Font("Calibri", 8, FontStyle.Bold), new SolidBrush(Color.Black), startX + 280, startY + Offset + 5);
            e.Graphics.PageUnit = GraphicsUnit.Inch;
            i = j = 0;
        }
    }
}
