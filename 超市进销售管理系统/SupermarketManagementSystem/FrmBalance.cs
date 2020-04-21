using BLL.Cashier;
using IBLL.Cashier;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common;
using Moder;

namespace SupermarketManagementSystem
{
    public partial class FrmBalance : Form
    {
        ISuperMarketMemberManager memberManager = new SuperMarketMemberManager();
        public FrmBalance(string totalMoney)
        {
            InitializeComponent();
            txtPay.Text = totalMoney;
            this.StartPosition = FormStartPosition.CenterScreen;
            txtVip.GotFocus += TxtVip_GotFocus;
            txtVip.LostFocus += TxtVip_LostFocus;
            txtAmount.GotFocus += TxtVip_GotFocus;
            txtAmount.LostFocus += TxtVip_LostFocus;
        }

        private void TxtVip_LostFocus(object sender, EventArgs e)
        {
            textbox txt = sender as textbox;
            txt.BackColor = Color.White;
            if (txtAmount.Text.Contains(".") && txtAmount.Text.IndexOf(".") == txtAmount.Text.Length)
            {
                txtAmount.Text += "00";
            }
            else if (!txtAmount.Text.Contains("."))
            {
                txtAmount.Text += ".00";
            }
            txtAmount.Text = Convert.ToDecimal(txtAmount.Text).ToString("F2");
        }

        private void TxtVip_GotFocus(object sender, EventArgs e)
        {
            textbox txt = sender as textbox;
            txt.BackColor = Color.Cyan;
            txt.SelectAll();
        }

        private void txtVip_KeyDown(object sender, KeyEventArgs e)
        {
            txtAmount.Text = txtAmount.Text.Replace("\r\n", "");
            txtVip.Text = txtVip.Text.Replace("\r\n", "");
            if (e.KeyCode == Keys.F1)
            {
                radMoney.Checked = true;
            }
            else if (e.KeyCode == Keys.F2)
            {
                radCard.Checked = true;
            }
            else if (e.KeyCode == Keys.F3)
            {
                radQRCode.Checked = true;
            }
            else if (e.KeyCode == Keys.Enter)//回车键：表示正常结算
            {
                if (txtAmount.CheckData(@"^(([1-9]\d*)|(\d*.\d{0,2}))$", "输入金额有误") != 0)
                {
                    if (txtAmount.Text.Contains(".") && txtAmount.Text.IndexOf(".") == txtAmount.Text.Length)
                    {
                        txtAmount.Text += "00";
                    }
                    else if (!txtAmount.Text.Contains("."))
                    {
                        txtAmount.Text += ".00";
                    }
                    txtAmount.Text = Convert.ToDecimal(txtAmount.Text).ToString("F2");
                    if (txtVip.Text.Length == 0)//判断不是会员
                    {
                        this.Tag = txtAmount.Text.Trim();
                    }
                    else//有会员卡号
                    {
                        if (txtVip.CheckData(@"^[1-9]\d*$", "会员卡号有误") != 0)
                        {
                            //进一步判断会员是否正常、、自己完成
                            SMMembers members = memberManager.GetMembersById(int.Parse(txtVip.Text.Trim()));
                            if (members != null)
                            {
                                this.Tag = $"{txtAmount.Text.Trim()}&{txtVip.Text.Trim()}";
                            }
                            else
                            {
                                this.Tag = txtAmount.Text.Trim();
                            }
                        }
                        else
                        {
                            this.Tag = txtAmount.Text.Trim();
                        }
                    }
                    //证明客户付钱够了
                    if (Convert.ToDecimal(txtPay.Text) <= Convert.ToDecimal(txtAmount.Text))
                    {
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("客户实际付款金额不足！", "注意");
                    }
                }
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.Tag = "Esc";
                this.Close();
            }
            else if (e.KeyCode == Keys.Tab)
            {

                textbox txt = sender as textbox;
                if (txt.Tag.ToString() == "vip")
                {
                    txtAmount.Focus();
                }
                else if (txt.Tag.ToString() == "pay")
                {
                    txtVip.Focus();
                }
            }
        }
    }
}
