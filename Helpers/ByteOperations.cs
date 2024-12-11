using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Helpers
{
    public static class ByteOperations
    {
        public static byte RotateRight(byte value)
        {
            const byte mask = 0b1111; // Mask to ensure we work with only 4 bits
            byte rotated = (byte)(((value & mask) >> 1) | ((value & 1) << 3));
            return (byte)(rotated & mask); // Ensure the result is still within 4 bits
        }

        // Check if a specific bit is set (0-based index)
        public static bool IsBitSet(byte value, int bitIndex)
        {
            return (value & (1 << bitIndex)) != 0;
        }

        // Set a specific bit to 1
        public static byte SetBit(byte value, int bitIndex)
        {
            return (byte)(value | (1 << bitIndex));
        }

        // Clear a specific bit (set to 0)
        public static byte ClearBit(byte value, int bitIndex)
        {
            return (byte)(value & ~(1 << bitIndex));
        }

        public static byte And4Bit(byte value1, byte value2)
        {
            const byte mask = 0b1111; // Mask to ensure we work with only 4 bits
            return (byte)((value1 & mask) & (value2 & mask));
        }
    }
}
