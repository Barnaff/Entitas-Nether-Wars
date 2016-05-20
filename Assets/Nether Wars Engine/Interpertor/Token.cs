using UnityEngine;
using System.Collections;


namespace NetherWars.Parsing
{
    public class Token 
    {
        public string TokenName { get; set; }

        public string TokenValue { get; set; }

        public int TokenIndex { get; set; }

        public int TokenLine { get; set; }


        public Token(string name, string value)
        {
            TokenName = name;
            TokenValue = value;
        }

        public Token(string name, string value, int index, int line)
        {
            TokenName = name;
            TokenValue = value;
            TokenIndex = index;
            TokenLine = line;
        }
    }
}
