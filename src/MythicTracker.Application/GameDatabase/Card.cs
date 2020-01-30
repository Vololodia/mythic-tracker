using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Bson;

namespace MythicTracker.Application.GameDatabase
{
    public class Card
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Set { get; set; }

        public int Artid { get; set; }

        public string Type { get; set; }

        public string[] Cost { get; set; }

        public int Cmc { get; set; }

        public string Rarity { get; set; }

        public int Cid { get; set; }

        public int Frame { get; set; }

        public int Artist { get; set; }

        public int Dfc { get; set; }

        public bool Collectible { get; set; }

        public bool Booster { get; set; }

        public bool DfcId { get; set; }

        public int Rank { get; set; }

        public int Rank_Values { get; set; }

        public int Rank_Controversy { get; set; }

        public bool Reprints { get; set; }
    }
}
