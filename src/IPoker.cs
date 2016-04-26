using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nancy.Simple
{
    enum Cards
    {
        Ace,
        King,
        // ...
    }

    interface IPoker
    {
        List<Cards> GetOurCards();
    }
}
