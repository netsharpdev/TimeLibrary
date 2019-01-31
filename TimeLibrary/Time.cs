using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeLibrary
{
    public struct Time : IEquatable<Time>, IComparable<Time>
    {
        private byte _hours;
        private byte _minutes;
        private byte _seconds;
        public Time(byte hour) : this(hour, 0){}

        public Time(byte hour, byte minute) : this(hour, minute, 0){}

        public Time(byte hour, byte minute, byte second)
        {
            _hours = hour;
            _minutes = minute;
            _seconds = second;
        }

        public byte Hours => _hours;

        public byte Minutes => _minutes;

        public byte Seconds => _seconds;

        /// <summary>
        /// Convert structure into string.
        /// </summary>
        /// <returns>Returns formatted time HH:mm:ss.</returns>
        public override string ToString()
        {
            return $"{FormatValue(_hours)}:{FormatValue(_minutes)}:{FormatValue(_seconds)}";
        }

        /// <summary>
        /// Compare time to other time structure.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Time other)
        {
            return _hours == other._hours && _minutes == other._minutes && _seconds == other._seconds;
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
        /// Generate hashcode of structure.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = _hours.GetHashCode();
                hashCode = (hashCode * 397) ^ _minutes.GetHashCode();
                hashCode = (hashCode * 397) ^ _seconds.GetHashCode();
                return hashCode;
            }
        }
        /// <summary>
        /// Compare one time instance to another
        /// </summary>
        /// <param name="other">Structure to compare</param>
        /// <returns>1 if base time is greater, 0 if base time is equal to other and -1 if time is smaller than other</returns>
        public int CompareTo(Time other)
        {
            if (_hours > other._hours)
            {
                return 1;
            }

            if (_hours < other._hours)
            {
                return -1;
            }

            if (_minutes > other._minutes)
            {
                return 1;
            }

            if (_minutes < other._minutes)
            {
                return -1;
            }

            if (_seconds > other._seconds)
            {
                return 1;
            }

            return _seconds == other._seconds ? 0 : -1;
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
