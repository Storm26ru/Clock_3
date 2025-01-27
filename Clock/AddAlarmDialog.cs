using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clock
{
	public partial class AddAlarmDialog : Form
	{
		public Alarm Alarm;
		OpenFileDialog openFile;
		public AddAlarmDialog()
		{
			InitializeComponent();
			dateTimePickerDate.Enabled = false;
			this.StartPosition = FormStartPosition.Manual;
			Alarm = new Alarm();
			SetWeekDays();
			openFile = new OpenFileDialog()
			{
				FileName = "Select file",
				Filter = "Sound files (*.mp3;*.wav) | *.mp3;*.wav|All files (*.*)|*.*",
				Title = "Open sound file"
			};
			labelFilename.Height = 32;
		}
		void SetWeekDays()
		{
			bool[] days = Alarm.Week.ToArray();
			for (int i = 0; i < checkedListBoxWeekdays.Items.Count; i++)
				checkedListBoxWeekdays.SetItemChecked(i, days[i]);
		}

		private void checkBoxUseDate_CheckedChanged(object sender, EventArgs e)
		{
			dateTimePickerDate.Enabled = checkBoxUseDate.Checked;
			checkedListBoxWeekdays.Enabled = !checkBoxUseDate.Checked;
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			Alarm.Date = dateTimePickerDate.Enabled ? dateTimePickerDate.Value : DateTime.MinValue;
			Alarm.Time = dateTimePickerTime.Value.TimeOfDay;
			//for (int i = 0; i < checkedListBoxWeekdays.Items.Count; i++)
			//{
			//	Console.Write(checkedListBoxWeekdays.GetItemChecked(i) + "\t");
			//}
			//Console.WriteLine();
			Alarm.Week = new Week
				(
					checkedListBoxWeekdays.
					Items.
					Cast<object>().
					Select((item, index) => checkedListBoxWeekdays.GetItemChecked(index)).ToArray()
				);
			if (labelFilename.Text != "Filename:" && labelFilename.Text != "")
			{
				Alarm.Filename = openFile.FileName;
			}
			else
			{
				MessageBox.Show(this, "Выберите звуковой файл", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
				this.DialogResult = DialogResult.None;
			}

			Alarm.Message = richTextBoxMessage.Text;
		}

		private void buttonChooseFile_Click(object sender, EventArgs e)
		{
			if (openFile.ShowDialog() == DialogResult.OK)
			{
				labelFilename.Text = $"Filename:\n{openFile.FileName}";
			}
		}
	}
}
