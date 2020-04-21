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

namespace SuperMarketManager.ProducFrm
{
    public partial class FrmInventory : Form
    {
        public FrmInventory()
        {
            InitializeComponent();
            dgvProductList.AutoGenerateColumns = false;
            InitializeInventory();
        }
        ISuperMarketProductManager productManager = new SuperMarketProductManager();
        private void InitializeInventory()
        {

        }
    }
}
