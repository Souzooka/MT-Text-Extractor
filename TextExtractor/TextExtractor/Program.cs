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
            // program launched without file being dragged onto it
            if (args.Length == 0) { return; }

            FileInfo file = new FileInfo(args[0]);
            Directory.SetCurrentDirectory(file.DirectoryName);

            byte[] contents = File.ReadAllBytes(file.Name);
            StringBuilder parsedText = new StringBuilder();

            // pointer for the start of messages in file header
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
                else { parsedText.Append(character.Value); }
            }

            File.WriteAllText($"./{file.Name}.txt", parsedText.ToString());
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
