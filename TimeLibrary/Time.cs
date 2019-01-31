using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeLibrary
{
    public struct Time : IEquatable<Time>, IComparable<Time>
    {
        public Time(byte hour) : this(hour, 0){}

        public Time(byte hour, byte minute) : this(hour, minute, 0){}

        public Time(byte hour, byte minute, byte second)
        {
            if(hour > 24)
                throw new ArgumentOutOfRangeException(nameof(hour), "Hours cannot exceed 24");
            if (minute > 59)
                throw new ArgumentOutOfRangeException(nameof(minute), "Minutes cannot exceed 59");
            if (second > 59)
                throw new ArgumentOutOfRangeException(nameof(second), "Seconds cannot exceed 59");
            Hours = hour;
            Minutes = minute;
            Seconds = second;
        }

        /// <summary>
        /// Hours of point in time
        /// </summary>
        public byte Hours { get; }

        /// <summary>
        /// Minutes of point in time
        /// </summary>
        public byte Minutes { get; }

        /// <summary>
        /// Seconds of point in time
        /// </summary>
        public byte Seconds { get; }

        /// <summary>
        /// Convert structure into string.
        /// </summary>
        /// <returns>Returns formatted time HH:mm:ss.</returns>
        public override string ToString()
        {
            return $"{FormatValue(Hours)}:{FormatValue(Minutes)}:{FormatValue(Seconds)}";
        }

        /// <summary>
        /// Compare time to other time structure.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Time other)
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
            return obj is Time && Equals((Time) obj);
        }
  
        /// <summary>
        /// Compare one time instance to another
        /// </summary>
        /// <param name="other">Structure to compare</param>
        /// <returns>1 if base time is greater, 0 if base time is equal to other and -1 if time is smaller than other</returns>
        public int CompareTo(Time other)
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

        public static bool operator ==(Time time1, Time time2)
        {
            return Equals(time1, time2);
        }
        public static bool operator !=(Time time1, Time time2)
        {
            return !Equals(time1, time2);
        }
        public static bool operator <(Time time1, Time time2)
        {
            return time1.CompareTo(time2) == -1;
        }
        public static bool operator >(Time time1, Time time2)
        {
            return time1.CompareTo(time2) == 1;
        }
        public static bool operator >=(Time time1, Time time2)
        {
            return time1.CompareTo(time2) == 1 || time1.Equals(time2);
        }
        public static bool operator <=(Time time1, Time time2)
        {
            return time1.CompareTo(time2) == -1 || time1.Equals(time2);
        }

        public static Time operator +(Time time1, Time time2)
        {
            var minutes = time1.Minutes + time2.Minutes;
            var hours = time1.Hours + time2.Hours;
            var seconds = time1.Seconds + time2.Seconds;
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
            var hDivision = hours / 24;
            if (hDivision > 0)
            {
                hours = 24;
                minutes = 0;
                seconds = 0;
            }
            return new Time(Convert.ToByte(hours), Convert.ToByte(minutes), Convert.ToByte(seconds));

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
