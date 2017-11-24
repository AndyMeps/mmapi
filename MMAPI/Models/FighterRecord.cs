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
        public int Draws { get; set; }
        public int NoContest { get; set; }

        #region Object Overrides
        public override bool Equals(object obj)
        {
            var fr = obj as FighterRecord;
            if (fr == null) return false;

            return fr.Wins == Wins
                && fr.Losses == Losses
                && fr.Draws == Draws
                && fr.NoContest == NoContest;
        }

        public override string ToString()
        {
            return $"{Wins}-{Losses}{_drawsAndNoContestPattern()}";
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion

        #region Private Methods
        private string _drawsAndNoContestPattern()
        {
            var drawsAndNoContest = string.Empty;
            if (Draws > 0 || NoContest > 0) drawsAndNoContest += $"-{Draws}";
            if (NoContest > 0) drawsAndNoContest += $" ({NoContest} NC)";
            return drawsAndNoContest;
        }
        #endregion
    }
}
