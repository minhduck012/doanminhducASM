using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Waterbill
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void lv_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lv.SelectedItems.Count > 0)
            {
                ListViewItem selected_Item = lv.SelectedItems[0];

                // Get details from the selected item
                string Name = selected_Item.SubItems[0].Text;
                string Lastmonth = selected_Item.SubItems[1].Text;
                string Thismonth = selected_Item.SubItems[2].Text;
                string Customer = selected_Item.SubItems[3].Text;

                // Update text fields with selected item details
                textname.Text = Name;
                textlastmonth.Text = Lastmonth;
                textthismonth.Text = Thismonth;
                cbbcustomer.Text = Customer;

                // Determine customer type based on selected index
                if (cbbcustomer.SelectedIndex == 0)
                {
                    Customer = "Household";
                }
                else if (cbbcustomer.SelectedIndex == 1)
                {
                    Customer = "Administrative agency";
                }
                else if (cbbcustomer.SelectedIndex == 2)
                {
                    Customer = "Production unit";
                }
                else if (cbbcustomer.SelectedIndex == 3)
                {
                    Customer = "Business service";
                }
            }
        }
        private void Form3_Load(object sender, EventArgs e)
        {
            lv.View = View.Details;
            lv.GridLines = true;
            lv.FullRowSelect = true;

            // Add columns to the ListView
            lv.Columns.Add("Name", 90);
            lv.Columns.Add("Last month", 100);
            lv.Columns.Add("This month", 100);
            lv.Columns.Add("TypeOfCustomer", 150);
            lv.Columns.Add("People", 70);
            lv.Columns.Add("Water consumed", 150);
            lv.Columns.Add("Water bill", 120);
            lv.Columns.Add("Environmental Fee", 150);
            lv.Columns.Add("VAT", 160);
            lv.Columns.Add("Total bill", 150);
        }

        private void butadd_Click(object sender, EventArgs e)
        {
            string name = textname.Text;
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Please Enter Your Name!", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textname.Focus();
                return;
            }

            // Validate and parse water usage inputs
            if (!double.TryParse(textlastmonth.Text, out double LMW))
            {
                MessageBox.Show("Please re-enter last month's water number!", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!double.TryParse(textthismonth.Text, out double TMW))
            {
                MessageBox.Show("Please re-enter this month's water number!", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (TMW < LMW)
            {
                MessageBox.Show("Error. This month's water number is larger than last month's water number!", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Determine customer type
            string Customer = cbbcustomer.Text;
            if (cbbcustomer.SelectedIndex == 0)
            {
                Customer = "Household";
            }
            else if (cbbcustomer.SelectedIndex == 1)
            {
                Customer = "Administrative agency";
            }
            else if (cbbcustomer.SelectedIndex == 2)
            {
                Customer = "Production unit";
            }
            else if (cbbcustomer.SelectedIndex == 3)
            {
                Customer = "Business service";
            }

            string People = textpeople.Text;

            double meterWater = TMW - LMW;

            // Define pricing constants
            double HOUSE_HOLD_1 = 5.973;
            double HOUSE_HOLD_2 = 7.052;
            double HOUSE_HOLD_3 = 8.699;
            double HOUSE_HOLD_4 = 15.929;
            double PRICE_AGENCIES = 9.955;
            double PRICE_PRODUCTION = 11.615;
            double PRICE_BUSINESS = 22.068;

            double price = 0;
            // Calculate price based on customer type and water usage
            switch (Customer)
            {
                case "Household":
                    if (meterWater <= 10)
                    {
                        price = meterWater * HOUSE_HOLD_1;
                    }
                    else if (meterWater <= 20)
                    {
                        price = (10 * HOUSE_HOLD_1) + (meterWater - 10) * HOUSE_HOLD_2;
                    }
                    else if (meterWater <= 30)
                    {
                        price = (10 * HOUSE_HOLD_1) + (10 * HOUSE_HOLD_2) + (meterWater - 20) * HOUSE_HOLD_3;
                    }
                    else
                    {
                        price = (10 * HOUSE_HOLD_1) + (10 * HOUSE_HOLD_2) + (10 * HOUSE_HOLD_3) + (meterWater - 30) * HOUSE_HOLD_4;
                    }
                    break;
                case "Administrative agency":
                    price = meterWater * PRICE_AGENCIES;
                    break;
                case "Production unit":
                    price = meterWater * PRICE_PRODUCTION;
                    break;
                case "Business service":
                    price = meterWater * PRICE_BUSINESS;
                    break;
            }

            // Calculate fees and total bill
            double environmentFee = price * 0.1;
            double vat = (price + environmentFee) * 0.1;
            double totalBill = price + environmentFee + vat;

            // Create and add new ListViewItem
            ListViewItem item = new ListViewItem();
            item.Text = name;
            item.SubItems.Add(LMW.ToString());
            item.SubItems.Add(TMW.ToString());
            item.SubItems.Add(Customer);
            item.SubItems.Add(People);
            item.SubItems.Add(meterWater.ToString());
            item.SubItems.Add(price.ToString());
            item.SubItems.Add(environmentFee.ToString());
            item.SubItems.Add(vat.ToString());
            item.SubItems.Add(totalBill.ToString());
            lv.Items.Add(item);
        }

        private void butedit_Click(object sender, EventArgs e)
        {
            if (lv.SelectedItems.Count > 0)
            {
                DialogResult confirm = MessageBox.Show("do you want to edit?", "Notice", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (confirm == DialogResult.OK)
                {
                    ListViewItem item = lv.SelectedItems[0];
                    item.SubItems.Clear();

                    // Retrieve and validate input
                    string name = textname.Text;
                    if (string.IsNullOrEmpty(name))
                    {
                        MessageBox.Show("Please Enter Your Name!", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        textname.Focus();
                        return;
                    }

                    if (!double.TryParse(textlastmonth.Text, out double LMW))
                    {
                        MessageBox.Show("Please re-enter last month's water number!", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    if (!double.TryParse(textthismonth.Text, out double TMW))
                    {
                        MessageBox.Show("Please re-enter this month's water number!", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    if (TMW < LMW)
                    {
                        MessageBox.Show("Error. This month's water number is larger than last month's water number!", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    // Determine customer type
                    string Customer = cbbcustomer.Text;
                    if (cbbcustomer.SelectedIndex == 0)
                    {
                        Customer = "Household";
                    }
                    else if (cbbcustomer.SelectedIndex == 1)
                    {
                        Customer = "Administrative agency";
                    }
                    else if (cbbcustomer.SelectedIndex == 2)
                    {
                        Customer = "Production unit";
                    }
                    else if (cbbcustomer.SelectedIndex == 3)
                    {
                        Customer = "Business service";
                    }

                    string People = textpeople.Text;

                    double meterWater = TMW - LMW;

                    // Define pricing constants
                    double HOUSE_HOLD_1 = 5.973;
                    double HOUSE_HOLD_2 = 7.052;
                    double HOUSE_HOLD_3 = 8.699;
                    double HOUSE_HOLD_4 = 15.929;
                    double PRICE_AGENCIES = 9.955;
                    double PRICE_PRODUCTION = 11.615;
                    double PRICE_BUSINESS = 22.068;

                    double price = 0;
                    // Calculate price based on customer type and water usage
                    switch (Customer)
                    {
                        case "Household":
                            if (meterWater <= 10)
                            {
                                price = meterWater * HOUSE_HOLD_1;
                            }
                            else if (meterWater <= 20)
                            {
                                price = (10 * HOUSE_HOLD_1) + (meterWater - 10) * HOUSE_HOLD_2;
                            }
                            else if (meterWater <= 30)
                            {
                                price = (10 * HOUSE_HOLD_1) + (10 * HOUSE_HOLD_2) + (meterWater - 20) * HOUSE_HOLD_3;
                            }
                            else
                            {
                                price = (10 * HOUSE_HOLD_1) + (10 * HOUSE_HOLD_2) + (10 * HOUSE_HOLD_3) + (meterWater - 30) * HOUSE_HOLD_4;
                            }
                            break;
                        case "Administrative agency":
                            price = meterWater * PRICE_AGENCIES;
                            break;
                        case "Production unit":
                            price = meterWater * PRICE_PRODUCTION;
                            break;
                        case "Business service":
                            price = meterWater * PRICE_BUSINESS;
                            break;
                    }

                    // Calculate fees and total bill
                    double environmentFee = price * 0.1;
                    double vat = (price + environmentFee) * 0.1;
                    double totalBill = price + environmentFee + vat;

                    // Update item with new details
                    item.Text = name;
                    item.SubItems.Add(LMW.ToString());
                    item.SubItems.Add(TMW.ToString());
                    item.SubItems.Add(Customer);
                    item.SubItems.Add(People);
                    item.SubItems.Add(meterWater.ToString());
                    item.SubItems.Add(price.ToString());
                    item.SubItems.Add(environmentFee.ToString());
                    item.SubItems.Add(vat.ToString());
                    item.SubItems.Add(totalBill.ToString());
                }
            }
        }

        private void butdelete_Click(object sender, EventArgs e)
        {
            // Check if there is a selected item in the ListView
            if (lv.SelectedItems.Count > 0)
            {
                // Confirm deletion
                DialogResult confirm = MessageBox.Show("Do you want to delete?", "Notice", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (confirm == DialogResult.OK)
                {
                    // Remove the selected item
                    ListViewItem Item = lv.SelectedItems[0];
                    Item.Remove();
                }
            }
        }

        private void butexit_Click(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show("Do you want to exit?", "Notice", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (confirm == DialogResult.OK)
            {
                // Close the form
                this.Close();
            }
        }

        private void textpeople_TextChanged(object sender, EventArgs e)
        {


        }

        private void cbbcustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbcustomer.Text == "Household")
            {
                textpeople.ReadOnly = false;
            } else
            {
                textpeople.ReadOnly = true;
            }
            textpeople.Text = null;
        }
    }
}
            
        





