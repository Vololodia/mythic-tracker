using System;
using System.Collections.Generic;
using System.Text;

namespace MythicTracker.Application.GameDatabase
{
    public interface ICardDatabase
    {
        Card GetCardOnID(int id);
    }
}
