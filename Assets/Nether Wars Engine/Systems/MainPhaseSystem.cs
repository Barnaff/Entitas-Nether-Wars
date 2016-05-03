using System;
using System.Collections.Generic;
using Entitas;

namespace NetherWars
{
    public class MainPhaseSystem : ISetPool, IExecuteSystem
    {
        Pool _pool;

        public void SetPool(Pool pool)
        {
            _pool = pool;
        }

        public void Execute()
        {
            // do stuff in main phase
        }
    }
}
