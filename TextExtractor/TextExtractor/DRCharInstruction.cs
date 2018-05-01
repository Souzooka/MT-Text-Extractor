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
        public short Value;
        public short DRValue;
        public byte Advance;
        public byte Flags;

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

