using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clock
{
	public class Week
	{
		static readonly string[] DAY_NAMES = new string[] { "Пн", "Вт", "Ср", "Чт", "Пт", "Сб", "Вс" };
		byte days;
		public Week(byte days=127)
		{
			this.days = days;
		}
		public Week(bool[] days)
		{
			for (byte i = 0; i < days.Length; i++)
			{
				if(days[i])this.days |= (byte)(1 << i);
			}
		}
		public Week(Week other)
		{
			this.days = other.days;
		}
		public bool[] ToArray()
		{
			bool[] selectedDays = new bool[7];
			for (byte i = 0; i < selectedDays.Length; i++)
				selectedDays[i] = (days & (byte)(1 << i)) != 0;
			return selectedDays;
		}
		public string DayOfWeek ()
        {
			string dayofweek = "";
			for (byte i = 0; i < 7; i++)
			{
				if ((days & (1 << i)) != 0)
				{
					if (i == 6) dayofweek += $"{(DayOfWeek)(i - 6)}";
					else dayofweek += $"{(DayOfWeek)(i + 1)} ";
				}
			}
			return dayofweek;
        }
		public override string ToString()
		{
			if (days == 127) return "Каждый день";
			if (days == 0) return "Ни одного дня";
			if (days == 31) return "Только в рабочие дни";
			if (days == 96) return "Только в выходные";
			string dayNames = "";
			for (byte i = 0; i < DAY_NAMES.Length; i++)
				if ((days & (byte)(1 << i)) != 0) dayNames += $"{DAY_NAMES[i]},";
			return dayNames;
		}
	}
}
