
namespace NetherWars
{

    [System.Flags]
    public enum eColorType
    {
        Black       = 1,
        Blue        = 1 << 1,
        Red         = 1 << 2,
        Green       = 1 << 3,
        White       = 1 << 4,
        Yellow      = 1 << 5,
    }

    [System.Serializable]
    public enum eCardType
    {
        Creature,
        Spell,
        Artifact,
    }

    [System.Serializable]
    public enum eZoneType
    {
        None,
        Hand,
        Battlefield,
        Deck,
        Graveyard,
        Exile,
        ResourcesPool,
    }

}
