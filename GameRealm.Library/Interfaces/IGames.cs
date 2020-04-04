using System;
using System.Collections.Generic;
using System.Text;

namespace GameRealm.Interface
{
    public interface IGames
    {
        public int GameID { get; set; }
        public string GameName { get; set; }
        public decimal? Cost { get; set; }
    }
}
