﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodInterpreter.Cypher
{
    public class Token
    {
        public Token(TType token_type, string code, object value, int line, int column)
        {
            Token_Type = token_type;
            Code = code;
            Value = value;
            Line = line;
            Column = column;
        }

        public TType Token_Type { get; set; }
        public string Code { get; }
        public object Value { get; set; }
        public int Line { get; }
        public int Column { get; }

        public override string ToString()
        {
            return $"Token({Token_Type}, {Code}, {Value})";
        }
    }
}
