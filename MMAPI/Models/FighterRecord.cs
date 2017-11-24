using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMAPI.Models
{
    public class FighterRecord
    {
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int? Draws { get; set; }
        public int? NoContest { get; set; }

        public override string ToString()
        {
            return $"{Wins}-{Losses}{_drawsAndNoContestPattern()}";
        }

        private string _drawsAndNoContestPattern()
        {
            var drawsAndNoContest = string.Empty;
            if ((Draws.HasValue && Draws.Value > 0) || (NoContest.HasValue && NoContest.Value > 0)) drawsAndNoContest += $"-{Draws.Value}";
            if (NoContest.HasValue && NoContest.Value > 0) drawsAndNoContest += $"-{NoContest}";
            return drawsAndNoContest;
        }
    }
}
