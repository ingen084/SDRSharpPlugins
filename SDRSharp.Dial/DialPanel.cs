using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Windows.Forms;

namespace SDRSharp.Dial
{
	[Category("SDRSharp")]
	[Description("Dial Panel")]
	public partial class DialPanel : UserControl
	{
		public Dictionary<string, int> Units = new Dictionary<string, int>
		{
			{  "Hz", 1 },
			{ "kHz", 1_000 },
			{ "MHz", 1_000_000 },
			{ "GHz", 1_000_000_000 },
		};

		public DialPanel()
		{
			InitializeComponent();

			comboBox1.Items.Clear();
			comboBox1.Items.AddRange(SerialPort.GetPortNames());
			comboBox1.SelectedIndex = 0;

			button1.Click += (s, e) => ButtonClicked?.Invoke(comboBox1.SelectedItem as string);
			checkBox1.Click += (s, e) => LedStateToggled?.Invoke(checkBox1.Checked);

			comboBox2.Items.Clear();
			comboBox2.Items.AddRange(Units.Select(p => p.Key).ToArray());
			comboBox2.SelectedIndex = 2;
			numericUpDown1.Value = 100;
			comboBox3.Items.Clear();
			comboBox3.Items.AddRange(Units.Select(p => p.Key).ToArray());
			comboBox3.SelectedIndex = 2;
			numericUpDown2.Value = 10;
			comboBox4.Items.Clear();
			comboBox4.Items.AddRange(Units.Select(p => p.Key).ToArray());
			comboBox4.SelectedIndex = 2;
			numericUpDown3.Value = 1;
			comboBox5.Items.Clear();
			comboBox5.Items.AddRange(Units.Select(p => p.Key).ToArray());
			comboBox5.SelectedIndex = 1;
			numericUpDown4.Value = 100;
		}

		public event Action<string> ButtonClicked;
		public event Action<bool> LedStateToggled;

		public bool IsLedChecked => (bool)Invoke(new Func<bool>(() => checkBox1.Checked));
		public int Rotator1Amount => (int)Invoke(new Func<int>(() => (int)numericUpDown1.Value * Units[comboBox2.SelectedItem as string]));
		public int Rotator2Amount => (int)Invoke(new Func<int>(() => (int)numericUpDown2.Value * Units[comboBox3.SelectedItem as string]));
		public int Rotator3Amount => (int)Invoke(new Func<int>(() => (int)numericUpDown3.Value * Units[comboBox4.SelectedItem as string]));
		public int Rotator4Amount => (int)Invoke(new Func<int>(() => (int)numericUpDown4.Value * Units[comboBox5.SelectedItem as string]));

		public void ChangeConnectedState(bool state)
		{
			if (InvokeRequired)
			{
				Invoke(new Action(() => ChangeConnectedState(state)));
				return;
			}
			label1.Text = "接続状態:" + (state ? "接続中" : "未接続");
			button1.Text = state ? "切断" : "接続";
			comboBox1.Enabled = !state;
		}

	}
}
