using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeLib.Library;

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
            internal int BaseSeconds { get; }
            internal int FactorSeconds { get; }
        }
        /// <summary>
        /// Create time from string.
        /// </summary>
        /// <param name="time">Format HH:mm:ss.</param>
        public Time(string time)
        {
            var splittedTime = time.Split(':');
            if (splittedTime.Length < 3)
                throw new ArgumentException("Argument has to be in proper format HH:mm:ss", nameof(time));
            var hour = Convert.ToByte(splittedTime[0]);;
            var minute = Convert.ToByte(splittedTime[1]);
            var second = Convert.ToByte(splittedTime[2]);
            if (hour > 24)
                throw new ArgumentOutOfRangeException(nameof(hour), "Hours cannot exceed 24");
            if (minute > 59)
                throw new ArgumentOutOfRangeException(nameof(minute), "Minutes cannot exceed 59");
            if (second > 59)
                throw new ArgumentOutOfRangeException(nameof(second), "Seconds cannot exceed 59");
            Hours = hour;
            Minutes = minute;
            Seconds = second;
        }
        public Time(int hour) : this(hour, 0){}

        public Time(int hour, int minute, int second)
        {
            if (hour > 24)
                throw new ArgumentOutOfRangeException(nameof(hour), "Hours cannot exceed 24");
            if (minute > 59)
                throw new ArgumentOutOfRangeException(nameof(minute), "Minutes cannot exceed 59");
            if (second > 59)
                throw new ArgumentOutOfRangeException(nameof(second), "Seconds cannot exceed 59");
            Hours = hour;
            Minutes = minute;
            Seconds = second;
        }

        public Time(int hour, int minute) : this(hour, minute, 0){}


        /// <summary>
        /// Hours.
        /// </summary>
        public int Hours { get; }

        /// <summary>
        /// Minutes.
        /// </summary>
        public int Minutes { get; }

        /// <summary>
        /// Seconds.
        /// </summary>
        public int Seconds { get; }

        /// <summary>
        /// Convert structure into string.
        /// </summary>
        /// <returns>Returns formatted time HH:mm:ss.</returns>
        public override string ToString()
        {
            return $"{Utilities.FormatValue(Hours)}:{Utilities.FormatValue(Minutes)}:{Utilities.FormatValue(Seconds)}";
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
        /// Compare one time instance to another.
        /// </summary>
        /// <param name="other">Structure to compare.</param>
        /// <returns>1 if base time is greater, 0 if base time is equal to other and -1 if time is smaller than other.</returns>
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
        /// <summary>
        /// Check if time1 is equal to time2.
        /// </summary>
        /// <param name="time1"></param>
        /// <param name="time2"></param>
        /// <returns>True if both instances are equal.</returns>
        public static bool operator ==(Time time1, Time time2)
        {
            return Equals(time1, time2);
        } 
        /// <summary>
        /// Check if time1 is not equal to time2.
        /// </summary>
        /// <param name="time1"></param>
        /// <param name="time2"></param>
        /// <returns>True if both instances are not equal.</returns>
        public static bool operator !=(Time time1, Time time2)
        {
            return !Equals(time1, time2);
        }
        /// <summary>
        /// Check if time1 is smaller than time2.
        /// </summary>
        /// <param name="time1"></param>
        /// <param name="time2"></param>
        /// <returns>True if time1 is smaller than time2.</returns>
        public static bool operator <(Time time1, Time time2)
        {
            return time1.CompareTo(time2) == -1;
        }
        /// <summary>
        /// Check if time1 is greater than time2.
        /// </summary>
        /// <param name="time1"></param>
        /// <param name="time2"></param>
        /// <returns>True if time1 is greater than time2.</returns>
        public static bool operator >(Time time1, Time time2)
        {
            return time1.CompareTo(time2) == 1;
        }
        /// <summary>
        /// Check if time1 is is greater or equal to time2.
        /// </summary>
        /// <param name="time1"></param>
        /// <param name="time2"></param>
        /// <returns>True if time1 is greater or equal to time2.</returns>
        public static bool operator >=(Time time1, Time time2)
        {
            return time1.CompareTo(time2) == 1 || time1.Equals(time2);
        }
        /// <summary>
        /// Check if time1 is is smaller or equal to time2.
        /// </summary>
        /// <param name="time1"></param>
        /// <param name="time2"></param>
        /// <returns>True if time1 is smaller or equal to time2.</returns>
        public static bool operator <=(Time time1, Time time2)
        {
            return time1.CompareTo(time2) == -1 || time1.Equals(time2);
        }
        /// <summary>
        /// Add time to time.
        /// </summary>
        /// <param name="time1"></param>
        /// <param name="time2"></param>
        /// <returns>Returns new time instance. When sum exceeds 24:00:00 it returns maximum 24:00:00.</returns>
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
        /// <summary>
        /// Add time period to time.
        /// </summary>
        /// <param name="timePeriod"></param>
        /// <returns>Returns new time instance. When sum exceeds 24:00:00 it returns maximum 24:00:00.</returns>
        public Time Add(TimePeriod timePeriod)
        {
            var operationalValues = CalculateSeconds(this, timePeriod);

            var result = operationalValues.BaseSeconds + operationalValues.FactorSeconds;

            //If result is greater than 24h set 24h to prevent from getting more hours.
            result = result > 86400 ? 86400 : result;
            return CalculateHour(result);
        }
        /// <summary>
        /// Substract specific time period from time.
        /// </summary>
        /// <param name="timePeriod"></param>
        /// <returns>Returns new time instance.</returns>
        public Time Substract(TimePeriod timePeriod)
        {
            var operationalValues = CalculateSeconds(this, timePeriod);

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
        private static OperationalValues CalculateSeconds(Time time1, TimePeriod timePeriod)
        {
            var baseSeconds = time1.Seconds + time1.Minutes * 60 + time1.Hours * 3600;
            var additionalSeconds = timePeriod.Seconds + timePeriod.Minutes * 60 + timePeriod.Hours * 3600;
            return new OperationalValues(baseSeconds, Convert.ToInt32(additionalSeconds));
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
    }
}
