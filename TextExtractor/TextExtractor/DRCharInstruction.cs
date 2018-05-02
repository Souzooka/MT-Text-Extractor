using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TextExtractor
{
#pragma warning disable 0649
    [StructLayout(LayoutKind.Sequential)]
    struct DRCharInstruction
    {
        public char Value;    // The ASCII value of the character
        public short DRValue; // The sector of the fontsheet to use
        public byte Advance;  // The width of the character displayed (how much further to write next character)
        public byte Flags;    // Special flags used in instruction, (Flags & 4) indicates a terminating character.

        public bool EOL
        {
            get
            {
                return (Flags & 4) != 0;
            }
        }
    }
#pragma warning restore 0649
}

