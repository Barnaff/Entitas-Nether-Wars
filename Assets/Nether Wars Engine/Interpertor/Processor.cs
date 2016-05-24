using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetherWars.Parsing
{
    class Processor
    {

        private eTokenType _currentTokenType;

        public bool Run(string inputString, Tokenizer tokenizer)
        {
            Token token = tokenizer.GetToken();
            string output = "";

            while (token != null)
            {
                output += "{" + token.TokenName + "}";

                switch (token.TokenType)
                {
                    case eTokenType.VAR:
                        {
                            break;
                        }
                }

                token = tokenizer.GetToken();
            }

            UnityEngine.Debug.Log(output);

            tokenizer.ResetProcessor();

            return true;
        }

        private object Run(Node node)
        {
            return null;
        }

    }
}
