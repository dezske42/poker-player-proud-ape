﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nancy.Simple
{
    public interface IPoker
    {
        [Obsolete]
        List<Cards> GetOurCards();

        List<ICards> GetOurFullCards();

        int CurrentBuyIn { get; }

        int Pot { get; }

        int MinimumRaise { get; }

        int HighestStackForActiveOtherPlayers { get; } 

        IList<ICards> CommunityCards { get; } 

        IList<ICards> GetOurCardsWithCommunityCards { get; } 

        bool CommunityMode { get; }

        string GameId { get; }

        string AllCardsJSon { get; }

        int RankId { get; }
    }
}
