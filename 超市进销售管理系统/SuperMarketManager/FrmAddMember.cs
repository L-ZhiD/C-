using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IBLL.Manager;
using BLL.Manager;
using Moder;

namespace SuperMarketManager
{
    public partial class FrmAddMember : Form
    {
        public FrmAddMember()
        {
            InitializeComponent();
            txtMemberName.Focus();
        }
        ISuperMarketMemberManager memberManager = new SuperMarketMemberManager();
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 确认注册
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSure_Click(object sender, EventArgs e)
        {
            if (txtMemberName.CheckNullOrEmpty() * txtMemberTel.CheckData(@"^1\d{10}$", "手机号格式 有误！") != 0)
            {
                if (memberManager.GetMemberByIdOrPhone(txtMemberTel.Text.Trim()) != null)
                {
                    MessageBox.Show("该手机号已经被注册！");
                    return;
                }
                SMMembers member = new SMMembers()
                {
                    MemberName = txtMemberName.Text.Trim(),
                    PhoneNumber = txtMemberTel.Text.Trim(),
                    MemberAddress = string.IsNullOrEmpty(txtAddress.Text.Trim()) ? "地址不详" : txtAddress.Text.Trim()
                };
                member = memberManager.AddMember(member);
                if (member != null)
                {
                    if (MessageBox.Show($"注册成功！会员账号是【{member.MemberId}】\r\n是否继续注册？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        txtMemberTel.Text = "";
                        txtMemberName.Text = "";
                        txtAddress.Text = "";
                        txtMemberName.Focus();
                    }
                    else
                    {
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("注册失败请稍后再试！");
                }
            }
        }
    }
}
