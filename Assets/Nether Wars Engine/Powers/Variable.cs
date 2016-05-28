namespace NetherWars.Powers
{
    public enum eVaribaleReturnType
    {
        Number,
        Target,
        Player,
        Card,
        Value,
    }

    public enum eVaribalType
    {
        Number,
        String,
        Pointer,
    }

    public enum eVaribalOperation
    {
        None,
        GetController,
        GetPower,
        GetHealth,

    }

    public class Variable
    {

        public string Name;

        public eVaribaleReturnType ReturnType;

        public string PointerTarget;

        public int Value;

        public eVaribalType Type;

        public eVaribalOperation Operation;

        public Variable(string name, eVaribaleReturnType type)
        {
            Name = name;
            ReturnType = type;
        }
    }
}
