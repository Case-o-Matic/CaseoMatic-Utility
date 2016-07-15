using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Caseomatic.Util
{
    /// <summary>
    /// A byte that is used to store bit data.
    /// </summary>
    public struct VectorByte
    {
        private const byte byteBitSize = 8;
        private byte b;

        /// <summary>
        /// The byte where the data is stored in.
        /// </summary>
        public byte Value
        {
            get { return b; }
        }

        public bool this[int index]
        {
            get
            {
                CheckIndex(ref index);
                return GetBitAtIndex(index, b);
            }
            set
            {
                CheckIndex(ref index);
                SetBitAtIndex(index, value, ref b);
            }
        }

        /// <summary>
        /// Initializes a new VectorByte instance.
        /// </summary>
        /// <param name="b">The starting byte that is used.</param>
        public VectorByte(byte b)
        {
            this.b = b;
        }

        /// <summary>
        /// Writes a bool into the byte at the specific index with the fixed length of 1 bit field.
        /// </summary>
        /// <param name="index">The index of the stored value.</param>
        /// <param name="value">The value that should be written into the byte.</param>
        public void WriteBool(int index, bool value)
        {
            this[index] = value;
        }

        /// <summary>
        /// Writes a byte number into the byte at the specific index with flexible length.
        /// </summary>
        /// <param name="index">The index of the stored value.</param>
        /// <param name="value">The value that should be written into the byte.</param>
        /// <returns>Returns the length of the written value in bits.</returns>
        public byte WriteByte(int index, byte value)
        {
            CheckIndex(ref index);

            byte length = GetLengthInBits(value);
            CheckOverflow(index, length, value);

            for (int i = index; i < index + length; i++)
            {
                var bit = GetBitAtIndex(i - index, value); // i - index = Iteration of "value" starting from 0 (and not "index" which is addressed at "b")
                SetBitAtIndex(i, bit, ref b);
            }

            return length;
        }

        /// <summary>
        /// Reads a bool from the byte at the specific index.
        /// </summary>
        /// <param name="index">The index of the requested value.</param>
        /// <returns>Returns the requested value.</returns>
        public bool ReadBool(int index)
        {
            return this[index];
        }
        /// <summary>
        /// Reads a byte number from the byte at the specific index.
        /// </summary>
        /// <param name="index">The index of the stored value.</param>
        /// <param name="length">The length of the requested value.</param>
        /// <returns>Returns the requested value.</returns>
        public byte ReadByte(int index, byte length)
        {
            CheckIndex(ref index);

            byte value = 0;
            for (int i = index; i < index + length; i++)
            {
                value |= (byte)((GetBitAtIndex(i, b) ? 1 : 0) << i - index);
            }

            return value;
        }

        public override string ToString()
        {
            return FromByteToString(b);
        }

        public static implicit operator byte (VectorByte vector)
        {
            return vector.b;
        }

        private bool GetBitAtIndex(int index, byte value)
        {
            return (value & (1 << index)) != 0;
        }
        private void SetBitAtIndex(int index, bool bit, ref byte value)
        {
            value |= (byte)((bit ? 1 : 0) << index);
        }

        private byte GetLengthInBits(byte value)
        {
            // Example: 01001000
            // From left to right every 0-bit is counted. The first time a 1-bit is encountered the iteration stops.
            // 3 0-bits means = 8 - 3 = 5 bits are used by the byte "value".

            byte sizeInBits = 0;
            for (int i = byteBitSize; i > 0; i--)
            {
                if (GetBitAtIndex(i - 1, value))
                    break;
                else
                    sizeInBits++;
            }

            return (byte)(byteBitSize - sizeInBits);
        }

        private string FromByteToString(byte value) // Set private
        {
            var stringBuilder = new StringBuilder();
            for (int i = byteBitSize; i >= 0; i--)
                stringBuilder.Append(GetBitAtIndex((byte)i, value) ? "1" : "0");

            return stringBuilder.ToString();
        }

        private void CheckIndex(ref int index)
        {
            if (index < 1 || index > byteBitSize)
                throw new IndexOutOfRangeException("The index " + index + " is invalid. Must be between 1 and " + byteBitSize);
            index -= 1;
        }
        private void CheckOverflow(int index, byte length, byte value)
        {
            //var result = (byte)(b & (value << index));
            //Console.WriteLine(result + ", " + FromByteToString(result));
            //if (result < value)
            //	throw new OverflowException("The value " + value + " does not fit into a byte with the given index " + index);
        }
    }
}
