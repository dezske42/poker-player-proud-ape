using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nancy.Simple
{
    enum Cards
    {
        Unknown,
        Ace,
        King,
        Jack,
        Queen,
        N2,
        N3,
        N4,
        N5,
        N6,
        N7,
        N8,
        N9,
        N10
    }

    interface IPoker
    {
        List<Cards> GetOurCards();
    }
}
