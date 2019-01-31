using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeLib
{
    public struct TimePeriod : IEquatable<TimePeriod>, IComparable<TimePeriod>
    {
        public TimePeriod(byte hour) : this(hour, 0) { }

        public TimePeriod(byte hour, byte minute) : this(hour, minute, 0) { }

        public TimePeriod(byte hour, byte minute, byte second)
        {
            Hours = hour;
            Minutes = minute;
            Seconds = second;
        }

        /// <summary>
        /// Hours of point in TimePeriod
        /// </summary>
        public byte Hours { get; }

        /// <summary>
        /// Minutes of point in TimePeriod
        /// </summary>
        public byte Minutes { get; }

        /// <summary>
        /// Seconds of point in TimePeriod
        /// </summary>
        public byte Seconds { get; }

        /// <summary>
        /// Convert structure into string.
        /// </summary>
        /// <returns>Returns formatted TimePeriod HH:mm:ss.</returns>
        public override string ToString()
        {
            return $"{FormatValue(Hours)}:{FormatValue(Minutes)}:{FormatValue(Seconds)}";
        }

        /// <summary>
        /// Compare TimePeriod to other TimePeriod structure.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(TimePeriod other)
        {
            return Hours == other.Hours && Minutes == other.Minutes && Seconds == other.Seconds;
        }
        /// <summary>
        /// Compare structure to other object.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is TimePeriod && Equals((TimePeriod)obj);
        }

        /// <summary>
        /// Compare one TimePeriod instance to another
        /// </summary>
        /// <param name="other">Structure to compare</param>
        /// <returns>1 if base TimePeriod is greater, 0 if base TimePeriod is equal to other and -1 if TimePeriod is smaller than other</returns>
        public int CompareTo(TimePeriod other)
        {
            if (Hours > other.Hours)
            {
                return 1;
            }

            if (Hours < other.Hours)
            {
                return -1;
            }

            if (Minutes > other.Minutes)
            {
                return 1;
            }

            if (Minutes < other.Minutes)
            {
                return -1;
            }

            if (Seconds > other.Seconds)
            {
                return 1;
            }

            return Seconds == other.Seconds ? 0 : -1;
        }

        public static bool operator ==(TimePeriod timePeriod1, TimePeriod timePeriod2)
        {
            return Equals(timePeriod1, timePeriod2);
        }
        public static bool operator !=(TimePeriod timePeriod1, TimePeriod timePeriod2)
        {
            return !Equals(timePeriod1, timePeriod2);
        }
        public static bool operator <(TimePeriod timePeriod1, TimePeriod timePeriod2)
        {
            return timePeriod1.CompareTo(timePeriod2) == -1;
        }
        public static bool operator >(TimePeriod timePeriod1, TimePeriod timePeriod2)
        {
            return timePeriod1.CompareTo(timePeriod2) == 1;
        }
        public static bool operator >=(TimePeriod timePeriod1, TimePeriod timePeriod2)
        {
            return timePeriod1.CompareTo(timePeriod2) == 1 || timePeriod1.Equals(timePeriod2);
        }
        public static bool operator <=(TimePeriod timePeriod1, TimePeriod timePeriod2)
        {
            return timePeriod1.CompareTo(timePeriod2) == -1 || timePeriod1.Equals(timePeriod2);
        }

        public static TimePeriod operator +(TimePeriod timePeriod1, TimePeriod timePeriod2)
        {
            var minutes = timePeriod1.Minutes + timePeriod2.Minutes;
            var hours = timePeriod1.Hours + timePeriod2.Hours;
            var seconds = timePeriod1.Seconds + timePeriod2.Seconds;
            var secModulo = seconds % 60;
            var secDivision = seconds / 60;
            if (secDivision > 0)
            {
                minutes += secDivision;
                seconds = secModulo;
            }
            var minModulo = minutes % 60;
            var minDivision = minutes / 60;
            if (minDivision > 0)
            {
                hours += minDivision;
                minutes = minModulo;
            }
            return new TimePeriod(Convert.ToByte(hours), Convert.ToByte(minutes), Convert.ToByte(seconds));

        }
        private string FormatValue(byte val)
        {
            if (val < 10)
            {
                return $"0{val}";
            }

            return $"{val}";
        }
    }
}
