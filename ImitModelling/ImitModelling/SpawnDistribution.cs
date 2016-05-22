using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImitModelling
{
	public partial class SpawnDistribution : Form
	{
		public delegate void SpawnDistribChangedHandler(object sender, DistribEventArgs e);
		public event SpawnDistribChangedHandler DistribChanged;
		public SpawnDistribution(int curDistrib, int curEstimate)
		{
			InitializeComponent();
			this.distribUpDown.Value = curDistrib;
			this.distribUpDown.Maximum = curDistrib + curEstimate;
		}

		private void distribUpDown_ValueChanged(object sender, EventArgs e)
		{
			if (DistribChanged != null) {
				this.DistribChanged(this, new DistribEventArgs(((double)distribUpDown.Value) / 100));
			}
		}
	}
}
