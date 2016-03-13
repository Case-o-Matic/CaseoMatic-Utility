using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace CaseoMatic.Util
{
    /// <summary>
    /// A byte that is used to store bit data.
    /// </summary>
    [StructLayout(LayoutKind.Auto, Pack = 1)]
    public struct VectorByte
    {
        private byte b;

        /// <summary>
        /// The byte where the data is stored in.
        /// </summary>
        public byte Byte
        {
            get { return b; }
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
        public void WriteBool(byte index, bool value)
        {
            Write(index, (byte)(value ? 1 : 0));
        }

        /// <summary>
        /// Writes a byte number into the byte at the specific index with flexible length.
        /// </summary>
        /// <param name="index">The index of the stored value.</param>
        /// <param name="value">The value that should be written into the byte.</param>
        public void WriteByte(byte index, byte value)
        {
            Write(index, value);
        }

        /// <summary>
        /// Reads a bool from the byte at the specific index.
        /// </summary>
        /// <param name="index">The index of the requested value.</param>
        /// <returns>Returns the requested value.</returns>
        public bool ReadBool(byte index)
        {
            return Read(index, 1) != 0;
        }
        /// <summary>
        /// Reads a byte number from the bste at the specific index.
        /// </summary>
        /// <param name="index">The index of the stored value.</param>
        /// <param name="length">The length of the requested value.</param>
        /// <returns>Returns the requested value.</returns>
        public byte ReadByte(byte index, byte length)
        {
            return Read(index, length);
        }

        private void Write(byte index, byte value)
        {
            byte newValue = 0;
            for (byte i = index; i < sizeof(byte); i++)
            {
                var bit = (value & (1 << i)) != 0;
                if (bit)
                    newValue |= (byte)(1 << i);
            }

            b |= (byte)(newValue << index);
        }
        private byte Read(byte index, byte length)
        {
            byte value = 0;
            for (int i = 0; i < sizeof(byte); i++)
            {
                var bit = (b & (1 << i)) != 0;
                if (bit)
                {
                    var orValue = (byte)((bit ? 1 : 0) << i);
                    value |= orValue;
                }
            }

            return value;
        }
    }
}
