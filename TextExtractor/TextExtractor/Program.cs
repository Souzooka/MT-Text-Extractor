using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TextExtractor
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] contents = File.ReadAllBytes("shop_u16_usa.msg");
            StringBuilder parsedText = new StringBuilder();
            int start = (int)contents[4];

            for (int i = start; i < contents.Length; i += 6)
            {
                MyChar character = new MyChar(new byte[] { contents[i], contents[i + 1], contents[i + 2], contents[i + 3], contents[i + 4], contents[i + 5] }, "X360Beta");
                parsedText.Append(character.Value);
                if (character.EOL) { parsedText.Append('\n'); }
            }

            File.WriteAllText("./shop_u16_usa.txt", parsedText.ToString());
        }
    }
}
