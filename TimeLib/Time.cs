using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeLib
{
    public struct Time : IEquatable<Time>, IComparable<Time>
    {
        private struct OperationalValues
        {
            internal OperationalValues(int baseSeconds, int factorSeconds)
            {
                BaseSeconds = baseSeconds;
                FactorSeconds = factorSeconds;
            }
            internal int BaseSeconds { get; set; }
            internal int FactorSeconds { get; set; }
        }

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
            var operationalValues = CalculateSeconds(time1, time2);

            var result = operationalValues.BaseSeconds + operationalValues.FactorSeconds;

            //If result is greater than 24h set 24h to prevent from getting more hours.
            result = result > 86400 ? 86400 : result;
            return CalculateHour(result);
        }

        public static Time operator -(Time time1, Time time2)
        {
            var operationalValues = CalculateSeconds(time1, time2);

            if (operationalValues.BaseSeconds < operationalValues.FactorSeconds)
            {
                throw new ArgumentOutOfRangeException(nameof(operationalValues.FactorSeconds),
                    "Cannot substract bigger time value from base time");
            }

            var result = operationalValues.BaseSeconds - operationalValues.FactorSeconds;
            return CalculateHour(result);
        }

        private static OperationalValues CalculateSeconds(Time time1, Time time2)
        {
            var baseSeconds = time1.Seconds + time1.Minutes * 60 + time1.Hours * 3600;
            var additionalSeconds = time2.Seconds + time2.Minutes * 60 + time2.Hours * 3600;
            return new OperationalValues(baseSeconds, additionalSeconds);
        }

        private static Time CalculateHour(int result)
        {
            var hours = result / 3600;
            var minutes = (result - 3600 * hours) / 60;
            var seconds = result - 60 * minutes - 3600 * hours;
            return FromIntParams(hours, minutes, seconds);
        }

        private static Time FromIntParams(int hours, int minutes, int seconds)
        {
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
