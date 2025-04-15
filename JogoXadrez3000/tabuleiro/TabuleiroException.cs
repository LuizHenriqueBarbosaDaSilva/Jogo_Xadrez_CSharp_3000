using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace JogoXadrez3000.tabuleiro
{
    public class TabuleiroException : Exception
    {
        public TabuleiroException(string msg) : base(msg) { }
    }
}
