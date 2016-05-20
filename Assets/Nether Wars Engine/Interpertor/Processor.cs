using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace NetherWars.Parsing
{
    public class Processor 
    {
        private readonly Dictionary<string, string> _tokens;
        private string _inputString;
        private int _index;

        public string InputString
        {
            set
            {
                _inputString = value;
            }
        }

        public Processor()
        {
            _tokens = new Dictionary<string, string>();
            _index = 0;
            _inputString = string.Empty;
        }

        public void ResetProcessor()
        {
            _inputString = string.Empty;
            _index = 0;
        }


        public string AddRegExToken(string regEx, string identifier)
        {
            if (_tokens.ContainsKey(identifier.Trim()))
                return "";

            identifier = identifier.Trim();
            if (CheckValidity(regEx) == false)
            {
                return GetInvalidMessage(regEx);
            }
            _tokens.Add(identifier, regEx);

            return "";
        }

        private string GetInvalidMessage(string regEx)
        {
            try
            {
                Regex r = new Regex(regEx);

                r.Match("");
            }
            catch (System.Exception e)
            {
                return e.Message;
            }

            return "";
        }

        private bool CheckValidity(string regEx)
        {
            try
            {
                Regex r = new Regex(regEx);

                r.Match("");
            }
            catch (System.Exception)
            {
                return false;
            }

            return true;
        }


        public Token GetToken()
        {
            if (_index >= _inputString.Length)
                return null;

            foreach (KeyValuePair<string, string> pair in _tokens)
            {
                Regex r = new Regex(pair.Value);
                Match m = r.Match(_inputString, _index);

                if (m.Success && m.Index == _index)
                {
                    if (m.Length == 0)
                        continue;
                    _index += m.Length;
                    return new Token(pair.Key, m.Value);
                }
            }
            _index++;
            return new Token("UNDEFINED", "");
        }

    }
}