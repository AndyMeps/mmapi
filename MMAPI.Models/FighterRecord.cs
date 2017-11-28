namespace MMAPI.Models
{
    public class FighterRecord
    {
        public uint Wins { get; set; }
        public uint Losses { get; set; }
        public uint Draws { get; set; }
        public uint NoContests { get; set; }

        #region Object Overrides
        public override bool Equals(object obj)
        {
            return obj is FighterRecord && this == (FighterRecord)obj;            
        }

        public override int GetHashCode()
        {
            var hashCode = 2074115206;
            hashCode = hashCode * -1521134295 + Wins.GetHashCode();
            hashCode = hashCode * -1521134295 + Losses.GetHashCode();
            hashCode = hashCode * -1521134295 + Draws.GetHashCode();
            hashCode = hashCode * -1521134295 + NoContests.GetHashCode();
            return hashCode;
        }

        public override string ToString()
        {
            return $"{Wins}-{Losses}{_drawsAndNoContestPattern()}";
        }
        #endregion

        #region Operator Overrides
        public static bool operator ==(FighterRecord record1, FighterRecord record2)
        {
            if (!ReferenceEquals(record1, null) && !ReferenceEquals(record2, null))
            {
                return record1.Wins == record2.Wins
                    && record1.Losses == record2.Losses
                    && record1.Draws == record2.Draws
                    && record1.NoContests == record2.NoContests;
            }

            return false;            
        }

        public static bool operator !=(FighterRecord record1, FighterRecord record2)
        {
            return !(record1 == record2);
        }
        #endregion

        #region Private Methods
        private string _drawsAndNoContestPattern()
        {
            var drawsAndNoContest = string.Empty;
            if (Draws > 0 || NoContests > 0) drawsAndNoContest += $"-{Draws}";
            if (NoContests > 0) drawsAndNoContest += $" ({NoContests} NC)";
            return drawsAndNoContest;
        }
        #endregion
    }
}
