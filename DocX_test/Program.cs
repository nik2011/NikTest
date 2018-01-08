using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Novacode;
using System.Drawing;

namespace DocX_test
{
    class Program
    {
        static void Main(string[] args)
        {
            using (DocX document = DocX.Create(@"D:\NikTest\DocX_test\doc\test.docx"))
            {
                //首先创建1个格式对象
                Formatting formatting = new Formatting();
                formatting.Bold = true;
                formatting.FontColor = Color.Red;
                formatting.Size = 30;
                //控制段落插入的位置
                int index = document.Text.Length / 2;
                //将文本插入到指定位置，并控制格式
                document.InsertParagraph(index, "New text", false, formatting);
                document.Save();//保存文档
            }

        }
    }
}
