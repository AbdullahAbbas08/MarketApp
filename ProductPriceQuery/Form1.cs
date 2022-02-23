using ProductPriceQuery.entityframework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProductPriceQuery
{
    public partial class Form1 : Form
    {
        MarketEntities Context = new MarketEntities();
        Product product = new Product();
        string destinationPath = @"C:\DestinationIamges\";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            maintabcontrol.SelectedTab = querytab;

            maintabcontrol.Appearance = TabAppearance.FlatButtons;
            maintabcontrol.ItemSize = new Size(0, 1);
            maintabcontrol.SizeMode = TabSizeMode.Fixed;

            #region Fill AutoComplete QName Text
            AutoCompleteStringCollection MyCollection2 = new AutoCompleteStringCollection();

            var Query2 = (from item in Context.Products
                          where item.Name.Contains(queryinputname.Text)
                          select new { item.Name, item.Parcode, item.Price, item.Image }).ToList();

            if (Query2.Count != 0)
            {
                foreach (var item in Query2)
                {
                    MyCollection2.Add(item.Name);
                }
                queryinputname.AutoCompleteMode = AutoCompleteMode.Suggest;
                queryinputname.AutoCompleteSource = AutoCompleteSource.CustomSource;
                queryinputname.AutoCompleteCustomSource = MyCollection2;


            }
            else
            {

            }
            #endregion

            #region Fill AutoComplete QName Text
            AutoCompleteStringCollection MyCollection3 = new AutoCompleteStringCollection();

            var Query3 = (from item in Context.Products
                          where item.Name.Contains(calculateinputname.Text)
                          select new { item.Name, item.Parcode, item.Price, item.Image }).ToList();

            if (Query3.Count != 0)
            {
                foreach (var item in Query3)
                {
                    MyCollection3.Add(item.Name);
                }
                calculateinputname.AutoCompleteMode = AutoCompleteMode.Suggest;
                calculateinputname.AutoCompleteSource = AutoCompleteSource.CustomSource;
                calculateinputname.AutoCompleteCustomSource = MyCollection3;


            }
            else
            {

            }
            #endregion

            DisplayAll();

            queryinputcode.Focus();

        }

        private void queryinputname_TextChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    dataGridView2.Rows.Clear();
            //    if (queryinputname.Text != "")
            //    {
            //        var Query = (from data in Context.Products
            //                     where data.Name.Contains(queryinputname.Text)
            //                     select data).ToList();

            //        if (Query != null)
            //        {
            //            foreach (var item in Query)
            //            {
            //                var RowIndex = dataGridView2.Rows.Add();
            //                dataGridView2.Rows[RowIndex].Cells["SName"].Value = item.Name;
            //                dataGridView2.Rows[RowIndex].Cells["SPrice"].Value = item.Price;
            //                if (item.Image != null)
            //                    dataGridView2.Rows[RowIndex].Cells["SImage"].Value = Image.FromFile(item.Image);
            //            }
            //        }

            //    }
            //    else
            //    {
            //        dataGridView2.Rows.Clear();
            //    }

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("حدثت مشكلة !! حاول مرة تانية ", ex.Message);

            //}
        }

        public void DisplayAll()
        {
            try
            {
                AllProductGridView.Rows.Clear();

                var Query = (from data in Context.Products
                             select data).ToList();

                if (Query.Count != 0)
                {
                    foreach (var item in Query)
                    {
                        var RowIndex = AllProductGridView.Rows.Add();
                        AllProductGridView.Rows[RowIndex].Cells["genname"].Value = item.Name;
                        AllProductGridView.Rows[RowIndex].Cells["genprice"].Value = item.Price;
                        if (item.Image != null)
                            AllProductGridView.Rows[RowIndex].Cells["genimage"].Value = Image.FromFile(item.Image);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("حدثت مشكلة !! حاول مرة تانية ", ex.Message);

            }
        }
        private void queryinputcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)Keys.Return)
                {
                    var value = queryinputcode.Text;
                    var Query = (from item in Context.Products
                                 where item.Parcode == value
                                 select new { item.Name, item.Parcode, item.Price, item.Image }).FirstOrDefault();

                    if (Query != null)
                    {

                        displayPricelbl.Text = Query.Price.ToString() + "  جنية ";
                        displaynamelbl.Text = Query.Name.ToString();
                        if (Query.Image != null)
                            displaypicture.Image = Image.FromFile(Query.Image);
                        maintabcontrol.SelectedTab = displaytab;
                        queryinputcode.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("مش موجود");
                        queryinputcode.Text = "";
                        queryinputcode.Focus();
                    }
                    Query = null;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("حدثت مشكلة !! حاول مرة تانية ", ex.Message);

            }
        }

        private void displaynamelbl_Click(object sender, EventArgs e)
        {
            maintabcontrol.SelectedTab = querytab;
            queryinputcode.Focus();
        }

        private void queryinputname_Click(object sender, EventArgs e)
        {
            queryinputname.Text = "";
        }

        private void queryinputcode_Enter(object sender, EventArgs e)
        {
            queryinputcode.BackColor = Color.Black;
            queryinputcode.ForeColor = Color.White;
        }

        private void queryinputcode_Leave(object sender, EventArgs e)
        {
            queryinputcode.BackColor = Color.White;
            queryinputcode.ForeColor = Color.Black;

            queryinputcode.Text = "";
        }

        private void queryinputname_Enter(object sender, EventArgs e)
        {
            queryinputname.BackColor = Color.Black;
            queryinputname.ForeColor = Color.White;
        }

        private void queryinputname_Leave(object sender, EventArgs e)
        {
            queryinputname.BackColor = Color.White;
            queryinputname.ForeColor = Color.Black;

            //queryinputname.Text = "";
        }

        private void maintabcontrol_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.F1)
            {
                maintabcontrol.SelectedTab = inserttab;
            }

            if (e.KeyData == Keys.F2)
            {
                maintabcontrol.SelectedTab = calculate;
            }

            if (e.KeyData == Keys.F3)
            {
                maintabcontrol.SelectedTab = AllProducttabPage;
            }

            if (e.KeyData == Keys.Down || e.KeyData == Keys.Up || e.KeyData == Keys.Left || e.KeyData == Keys.Right)
            {
                maintabcontrol.SelectedTab = querytab;
                queryinputcode.Focus();
            }
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            insertnametxt.BackColor = Color.Black;
            insertnametxt.ForeColor = Color.White;
        }

        private void insertnametxt_Leave(object sender, EventArgs e)
        {
            insertnametxt.BackColor = Color.White;
            insertnametxt.ForeColor = Color.Black;

        }

        private void insertpricetxt_Enter(object sender, EventArgs e)
        {
            insertpricetxt.BackColor = Color.Black;
            insertpricetxt.ForeColor = Color.White;
        }

        private void insertpricetxt_Leave(object sender, EventArgs e)
        {
            insertpricetxt.BackColor = Color.White;
            insertpricetxt.ForeColor = Color.Black;
        }

        private void insertcodetxt_Enter(object sender, EventArgs e)
        {
            insertcodetxt.BackColor = Color.Black;
            insertcodetxt.ForeColor = Color.White;
        }

        private void insertcodetxt_Leave(object sender, EventArgs e)
        {
            insertcodetxt.BackColor = Color.White;
            insertcodetxt.ForeColor = Color.Black;
        }

        private void insertproductbtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (insertradio.Checked == true)
                {
                    if (insertnametxt.Text.Length <= 0)
                        MessageBox.Show("أكتب الأسم الأول");
                    product.Name = insertnametxt.Text;

                    if (insertpricetxt.Text.Length <= 0)
                        MessageBox.Show("أكتب السعر ");
                    product.Price = Double.Parse((insertpricetxt.Value).ToString());

                    if (insertcodetxt.Text.Length <= 0)
                        MessageBox.Show("أكتب 0 لو مفيش كود  ");

                    var Query = (from item in Context.Products
                                 where item.Parcode == insertcodetxt.Text
                                 select new { item.Parcode, item.Name, item.Price, item.Image }).ToList();
                    if (Query.Count > 0)
                        MessageBox.Show(" الكود دا متسجل بالفعل ");

                    product.Parcode = insertcodetxt.Text;



                    if (insertnametxt.Text.Length > 1 && insertpricetxt.Text.Length > 0 && insertcodetxt.Text != "" && Query.Count <= 0)
                    {
                        Context.Products.Add(product);
                        Context.SaveChanges();
                        insertnametxt.Text = "";
                        insertpricetxt.Text = "";
                        insertcodetxt.Text = "";
                        pictureBox2.Image = null;
                        maintabcontrol.SelectedTab = querytab;
                        MessageBox.Show("تم التسجيل بنجاح ");
                    }
                }
                else if (updateradio.Checked == true)
                {

                    var value = insertcodetxt.Text;
                    var Query = (from item in Context.Products
                                 where item.Parcode == value
                                 select new { item.Name, item.Parcode, item.Price, item.Image }).SingleOrDefault();

                    if (Query != null)
                    {
                        product.Name = insertnametxt.Text;
                        product.Price = Double.Parse((insertpricetxt.Value).ToString());
                        product.Parcode = insertcodetxt.Text;

                        if (product.Image == null)
                            product.Image = Query.Image;



                        if (insertnametxt.Text.Length > 1 && insertpricetxt.Text.Length > 0 && insertcodetxt.Text != "")
                        {
                            //Context.Products.Attach(product);
                            Context.Entry(product).State = EntityState.Modified;
                            Context.SaveChanges();
                            insertnametxt.Text = "";
                            insertpricetxt.Text = "";
                            insertcodetxt.Text = "";
                            pictureBox2.Image = null;
                            maintabcontrol.SelectedTab = querytab;
                            MessageBox.Show("تم التعديل بنجاح ");
                        }
                    }


                }
                else
                {
                    MessageBox.Show("إختر نوع العملية إضافة وللا تعديل من فوق ");
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("حدثت مشكلة !! حاول مرة تانية ", ex.Message);
            }
        }

        private void insertcodetxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)Keys.Return)
                {
                    var value = queryinputcode.Text;

                    var Query = (from item in Context.Products
                                 where item.Parcode == insertcodetxt.Text
                                 select new { item.ID, item.Parcode, item.Name, item.Price, item.Image }).ToList();

                    if (Query.Count > 0)
                    {
                        foreach (var item in Query)
                        {
                            product.ID = item.ID;
                            insertnametxt.Text = item.Name;
                            insertpricetxt.Text = item.Price.ToString();
                            pictureBox2.Image = Image.FromFile(item.Image);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("حدثت مشكلة !! حاول مرة تانية ", ex.Message);

            }
        }

        private void insertimagebtn_Click(object sender, EventArgs e)
        {
            try
            {
                DealWithImage();
            }
            catch (Exception ex)
            {
                MessageBox.Show("حدثت مشكلة !! حاول مرة تانية ", ex.Message);

            }
        }

        public void DealWithImage()
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp;*.png)|*.jpg; *.jpeg; *.gif; *.bmp;*.png";

            if (open.ShowDialog() == DialogResult.OK)
            {
                var imgpath = open.FileName;
                pictureBox2.Image = new Bitmap(open.FileName);

                if (!string.IsNullOrWhiteSpace(imgpath) && File.Exists(imgpath))
                {
                    product.Image = destinationPath + Guid.NewGuid().ToString() + Path.GetExtension(open.FileName);
                    File.Copy(imgpath, product.Image);
                }
            }

        }

        private void calculateinputname_Enter(object sender, EventArgs e)
        {
            try
            {
                calculatedategridview.Rows.Clear();
                if (calculateinputname.Text != "")
                {
                    var Query = (from data in Context.Products
                                 where data.Name.Contains(queryinputname.Text)
                                 select data).FirstOrDefault();

                    if (Query != null)
                    {
                        var RowIndex = calculatedategridview.Rows.Add();
                        calculatedategridview.Rows[RowIndex].Cells["calculatenamegrid"].Value = Query.Name;
                        calculatedategridview.Rows[RowIndex].Cells["calculatepricegrid"].Value = Query.Price;
                        calculatedategridview.Rows[RowIndex].Cells["calculatecountgrid"].Value = 1;
                        calculatedategridview.Rows[RowIndex].Cells["calculatetotalgrid"].Value = (Convert.ToInt32(calculatedategridview.Rows[RowIndex].Cells["calculatecountgrid"].Value.ToString())) * (Convert.ToDouble(calculatedategridview.Rows[RowIndex].Cells["calculatepricegrid"].Value));
                        if (Query.Image != null)
                            dataGridView2.Rows[RowIndex].Cells["SImage"].Value = Image.FromFile(Query.Image);
                    }

                }
                else
                {
                    dataGridView2.Rows.Clear();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("حدثت مشكلة !! حاول مرة تانية ", ex.Message);

            }
        }

        private void calculateinputcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)Keys.Return)
                {
                    var value = queryinputcode.Text;
                    var Query = (from item in Context.Products
                                 where item.Parcode == value
                                 select new { item.Name, item.Parcode, item.Price, item.Image }).FirstOrDefault();

                    if (Query != null)
                    {

                        var RowIndex = calculatedategridview.Rows.Add();
                        calculatedategridview.Rows[RowIndex].Cells["calculatenamegrid"].Value = Query.Name;
                        calculatedategridview.Rows[RowIndex].Cells["calculatepricegrid"].Value = Query.Price;
                        calculatedategridview.Rows[RowIndex].Cells["calculatecountgrid"].Value = 1;
                        calculatedategridview.Rows[RowIndex].Cells["calculatetotalgrid"].Value = (Convert.ToInt32(calculatedategridview.Rows[RowIndex].Cells["calculatecountgrid"].Value.ToString())) * (Convert.ToDouble(calculatedategridview.Rows[RowIndex].Cells["calculatepricegrid"].Value));
                        if (Query.Image != null)
                            dataGridView2.Rows[RowIndex].Cells["SImage"].Value = Image.FromFile(Query.Image);
                    }
                    else
                    {
                        MessageBox.Show("مش موجود");
                        queryinputcode.Text = "";
                        queryinputcode.Focus();
                    }
                    Query = null;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("حدثت مشكلة !! حاول مرة تانية ", ex.Message);

            }
        }

        private void calculateinputname_TextChanged(object sender, EventArgs e)
        {
            #region Fill AutoComplete QName Text
            AutoCompleteStringCollection MyCollection4 = new AutoCompleteStringCollection();

            var Query4 = (from item in Context.Products
                          where item.Name.Contains(calculateinputname.Text)
                          select new { item.Name, item.Parcode, item.Price, item.Image }).ToList();

            if (Query4.Count != 0)
            {
                foreach (var item in Query4)
                {
                    MyCollection4.Add(item.Name);
                }
                calculateinputname.AutoCompleteMode = AutoCompleteMode.Suggest;
                calculateinputname.AutoCompleteSource = AutoCompleteSource.CustomSource;
                calculateinputname.AutoCompleteCustomSource = MyCollection4;


            }
            else
            {

            }
            #endregion
        }

        private void dataGridView2_Click(object sender, EventArgs e)
        {
          

        }

        private void queryinputname_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void searchbtn_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView2.Rows.Clear();
                if (queryinputname.Text != "")
                {
                    var Query = (from data in Context.Products
                                 where data.Name.Contains(queryinputname.Text)
                                 select data).ToList();

                    if (Query != null)
                    {
                        foreach (var item in Query)
                        {
                            var RowIndex = dataGridView2.Rows.Add();
                            dataGridView2.Rows[RowIndex].Cells["SName"].Value = item.Name;
                            dataGridView2.Rows[RowIndex].Cells["SPrice"].Value = item.Price;
                            if (item.Image != null)
                                dataGridView2.Rows[RowIndex].Cells["SImage"].Value = Image.FromFile(item.Image);
                        }
                    }

                }
                else
                {
                    dataGridView2.Rows.Clear();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("حدثت مشكلة !! حاول مرة تانية ", ex.Message);

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            maintabcontrol.SelectedTab = cheese;
        }
    }
}
