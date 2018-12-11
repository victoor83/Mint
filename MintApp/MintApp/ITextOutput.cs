using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MintApp
{
    public interface ITextOutput
    {
        void Write(string Text);
        void WriteLine(string Text);
    }
}
