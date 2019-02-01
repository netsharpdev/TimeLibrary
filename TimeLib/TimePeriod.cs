using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeLib.Library;

namespace TimeLib
{
    public struct TimePeriod : IEquatable<TimePeriod>, IComparable<TimePeriod>
    {
        private struct OperationalValues
        {
            internal OperationalValues(long baseSeconds, long factorSeconds)
            {
                BaseSeconds = baseSeconds;
                FactorSeconds = factorSeconds;
            }
            internal long BaseSeconds { get; set; }
            internal long FactorSeconds { get; set; }
        }
        /// <summary>
        /// Create time period from string.
        /// </summary>
        /// <param name="period">Format H:mm:ss</param>
        public TimePeriod(string period)
        {
            var splittedPeriod = period.Split(':');
            if (splittedPeriod.Length < 3)
                throw new ArgumentException("Argument has to be in proper format H:mm:ss", nameof(period));
            var hour = Convert.ToByte(splittedPeriod[0]); ;
            var minute = Convert.ToByte(splittedPeriod[1]);
            var second = Convert.ToByte(splittedPeriod[2]);
            if (hour < 0)
                throw new ArgumentOutOfRangeException(nameof(hour), "Minutes cannot be smaller than 0");
            if (minute > 59 || minute < 0)
                throw new ArgumentOutOfRangeException(nameof(minute), "Minutes cannot exceed 59 and be smaller than 0");
            if (second > 59 || second < 0)
                throw new ArgumentOutOfRangeException(nameof(second), "Seconds cannot exceed 59 and be smaller than 0");
            Hours = hour;
            Minutes = minute;
            Seconds = second;
        }

        public TimePeriod(long hour) : this(hour, 0) { }

        public TimePeriod(long hour, long minute) : this(hour, minute, 0) { }

        public TimePeriod(long hour, long minute, long second)
        {
            if (hour < 0)
                throw new ArgumentOutOfRangeException(nameof(hour), "Minutes cannot be smaller than 0");
            if (minute > 59 || minute < 0)
                throw new ArgumentOutOfRangeException(nameof(minute), "Minutes cannot exceed 59 and be smaller than 0");
            if (second > 59 || second < 0)
                throw new ArgumentOutOfRangeException(nameof(second), "Seconds cannot exceed 59 and be smaller than 0");
            Hours = hour;
            Minutes = minute;
            Seconds = second;
        }

        /// <summary>
        /// Hours
        /// </summary>
        public long Hours { get; }

        /// <summary>
        /// Minutes
        /// </summary>
        public long Minutes { get; }

        /// <summary>
        /// Seconds
        /// </summary>
        public long Seconds { get; }

        /// <summary>
        /// Convert structure into string.
        /// </summary>
        /// <returns>Returns formatted TimePeriod HH:mm:ss.</returns>
        public override string ToString()
        {
            return $"{Hours}:{Utilities.FormatValue(Minutes)}:{Utilities.FormatValue(Seconds)}";
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
            var operationalValues = CalculateSeconds(timePeriod1, timePeriod2);

            var result = operationalValues.BaseSeconds + operationalValues.FactorSeconds;
            return CalculateHour(result);

        }
        public static TimePeriod operator -(TimePeriod timePeriod1, TimePeriod timePeriod2)
        {
            var operationalValues = CalculateSeconds(timePeriod1, timePeriod2);

            if (operationalValues.BaseSeconds < operationalValues.FactorSeconds)
            {
                throw new ArgumentOutOfRangeException(nameof(operationalValues.FactorSeconds),
                    "Cannot substract greater timeperiod value from base timeperiod");
            }

            var result = operationalValues.BaseSeconds - operationalValues.FactorSeconds;
            return CalculateHour(result);
        }
        private static OperationalValues CalculateSeconds(TimePeriod time1, TimePeriod time2)
        {
            var baseSeconds = time1.Seconds + time1.Minutes * 60 + time1.Hours * 3600;
            var additionalSeconds = time2.Seconds + time2.Minutes * 60 + time2.Hours * 3600;
            return new OperationalValues(baseSeconds, additionalSeconds);
        }

        private static TimePeriod CalculateHour(long result)
        {
            var hours = result / 3600;
            var minutes = (result - 3600 * hours) / 60;
            var seconds = result - 60 * minutes - 3600 * hours;
            return new TimePeriod(hours, minutes, seconds);
        }
    }
}
