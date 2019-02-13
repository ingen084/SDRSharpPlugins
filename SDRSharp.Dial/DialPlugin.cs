using SDRSharp.Common;
using System.IO.Ports;
using System.Threading.Tasks;

namespace SDRSharp.Dial
{
	public class DialPlugin : ISharpPlugin
	{
		public System.Windows.Forms.UserControl Gui => Panel;
		private DialPanel Panel { get; set; }
		private SerialPort Port { get; set; }

		public string DisplayName => "Dial";

		private ISharpControl ControlInterface { get; set; }

		private void SendLedState(byte state)
		{
			if (!(Port?.IsOpen ?? false))
				return;
			state &= 0b00001111;
			Port.Write(new byte[] { (byte)(state | (~state << 4)) }, 0, 1);
		}

		public void Initialize(ISharpControl control)
		{
			ControlInterface = control;
			Panel = new DialPanel();

			bool preLighting = false;
			Panel.LedStateToggled += s => SendLedState(s || preLighting ? (byte)0b00001111 : (byte)0b00000000);
			Panel.ButtonClicked += n =>
			{
				if (Port?.IsOpen ?? false)
				{
					SendLedState(0b00000000);
					Port.Close();
					Panel.ChangeConnectedState(false);
					return;
				}

				Port = new SerialPort(n)
				{
					BaudRate = 4800,
					Parity = Parity.None,
					StopBits = StopBits.One,
					DataBits = 8,
				};
				try
				{
					Port.Open();
				}
				catch
				{
					Panel.ChangeConnectedState(false);
				}
				Panel.ChangeConnectedState(true);
				Task.Run(async () =>
				{
					await Task.Delay(100);
					SendLedState(0b00001111);
					await Task.Delay(1000);
					if (!Panel.IsLedChecked)
						SendLedState(0b00000000);
				});


				Port.DataReceived += (s, e) =>
				{
					if (Port.BytesToRead < 1)
						return;

					var bytes = new byte[Port.BytesToRead];
					Port.Read(bytes, 0, bytes.Length);

					if (((byte)(bytes[0] >> 4) & (byte)(bytes[0] & 0b00001111)) != 0) // validation failed
						return;

					var number = bytes[0] & 0b00000011;
					var amount = 0;
					switch (number)
					{
						case 0:
							amount = Panel.Rotator1Amount;
							break;
						case 1:
							amount = Panel.Rotator2Amount;
							break;
						case 2:
							amount = Panel.Rotator3Amount;
							break;
						case 3:
							amount = Panel.Rotator4Amount;
							break;
					}
					if ((bytes[0] & 0b00000100) == 0)
						amount = -amount;

					control.SetFrequency(ControlInterface.Frequency + amount, true);
				};
			};
		}

		public void Close()
		{
			if (Port?.IsOpen ?? false)
			{
				SendLedState(0b00000000);
				Port.Close();
			}
		}
	}
}
