using System.Collections;

namespace NetherWars.Parsing
{
    public enum eTokenType
    {
        VAR,
        PRINT,
        IF,
        THIS,
        CARD,
        PLAYER,
        HEALTH,
        DAMAGE,
        PLAY,
        CAST,
        MANA,
        TARGET,
        DRAW,
        ON,
        DO,
        COLON,
        EQUALS,
        AND,
        OR,
        STRING,
        IDENTIFIER,
        WHITESPACE,
        NEWLINE,
        FLOAT,
        INTEGER,
        APOSTROPHE,
        LPAREN,
        RPAREN,
        ASTERISK,
        SLASH,
        PLUS,
        MINUS

    }

    public class Token 
    {
        public string TokenName { get; set; }

        public string TokenValue { get; set; }

        public int Index { get; set; }

        public int Line { get; set; }

        public eTokenType TokenType;

        public Token(string name, string value)
        {
            TokenName = name;
            TokenValue = value;
            TokenType = (eTokenType)System.Enum.Parse(typeof(eTokenType), name);
        }

        public Token(string name, string value, int index, int line)
        {
            TokenName = name;
            TokenValue = value;
            Index = index;
            Line = line;
            TokenType = (eTokenType)System.Enum.Parse(typeof(eTokenType), name);
        }
    }
}
