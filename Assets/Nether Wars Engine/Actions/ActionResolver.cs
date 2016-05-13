using System.Collections;
using Entitas;
using System.Collections.Generic;

namespace NetherWars
{
    public class ActionResolver 
    {

        const string KEYWARD_DRAW = "draw";
        const string KEYWARD_END = "end";
        const string KEYWARD_VAR = "var";

        public static void ExecuteAction(string actionString)
        {
            Logger.LogAction("Execute action " + actionString);

            string[] fields = actionString.Split(" "[0]);

            Dictionary<string, object> vars = new Dictionary<string, object>();

            Entity player = null;            

            for (int i=0; i< fields.Length; i++)
            {
                if (fields[i].ToLower() == KEYWARD_DRAW)
                {
                    if (player == null)
                    {
                        player = Pools.pool.activePlayerEntity;
                    }

                    int drawCount = 1;
                    if (i + 1 < fields.Length)
                    {
                        int.TryParse(fields[i + 1], out drawCount);
                    }

                    player.AddDraw(drawCount);

                }


                if (fields[i].ToLower() == KEYWARD_END)
                {
                    Pools.pool.ReplaceTurnPhase(NetherWars.TurnPhase.eTurnPhase.End);
                }

                if (fields[i].ToLower() == KEYWARD_VAR)
                {
                    string varname = null;
                    object value = null;
                    if (i + 1 < fields.Length)
                    {
                        varname = fields[i + 1];
                    }

                    if (i + 2 < fields.Length)
                    {
                        string varType = fields[i + 2];

                        if (varType.ToLower() == "currentplayer" || varType.ToLower() == "player")
                        {
                            value = Pools.pool.activePlayerEntity;
                        }

                        else if (varType.ToLower() == "card")
                        {

                        }
                    }

                    if (!string.IsNullOrEmpty(varname) && value != null)
                    {
                        vars.Add(varname, value);

                        Logger.LogMessage("created var: [" + varname + " : " + value + "]");
                    }
                }
            }
        }
    }

}
