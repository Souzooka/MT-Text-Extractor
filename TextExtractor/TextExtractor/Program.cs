using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TextExtractor
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] contents = File.ReadAllBytes("epilogue_u16_usa.rMessage");
            StringBuilder parsedText = new StringBuilder();
            int start = (int)contents[4];

            for (int i = start; i <= contents.Length - 6; i += 6)
            {
                // Get contents for 1 instruction in new array
                byte[] data = new byte[6];
                Array.Copy(contents, i, data, 0, 6);

                // Get pointer to struct represented by array
                DRCharInstruction character = ByteArrayToStructure<DRCharInstruction>(data);

                // Parse character instruction data
                if (character.EOL) { parsedText.Append('\n'); }
                else { parsedText.Append((char)character.Value); }
            }

            File.WriteAllText("./epilogue_u16_usa.txt", parsedText.ToString());
        }

        static T ByteArrayToStructure<T>(byte[] bytes) where T : struct
        {
            IntPtr ptr = Marshal.AllocHGlobal(bytes.Length);
            Marshal.Copy(bytes, 0, ptr, bytes.Length);
            T structureObj = (T)Marshal.PtrToStructure(ptr, typeof(T));
            Marshal.FreeHGlobal(ptr);
            return structureObj;
        }
    }
}
