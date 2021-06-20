﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG.Stats
{
    interface IProvideStats
    {
        float ProvideInitialHealth();
        float ProvideExperienceReward();
    }
}
