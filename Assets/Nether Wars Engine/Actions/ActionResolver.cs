using System.Collections;
using Entitas;

namespace NetherWars
{
    public class ActionResolver 
    {

        public static void ExecuteAction(string actionString)
        {
            string[] fields = actionString.Split(" "[0]);

            Entity player = null;            

            for (int i=0; i< fields.Length; i++)
            {
                if (fields[i] == "draw")
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


                if (fields[i] == "end")
                {
                    Pools.pool.ReplaceTurnPhase(NetherWars.TurnPhase.eTurnPhase.End);
                }
            }
        }
    }

}
