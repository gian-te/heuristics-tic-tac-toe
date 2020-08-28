namespace TicTacToe.Common.Entities
{
    public class Move
    {
        // store the coordinates only
        // and Symbol
        private int _row;
        public int Row
        {
            get
            {
                return _row;
            }
            set
            {
                _row = value;
            }
        }

        private int _col;
        public int Col
        {
            get
            {
                return _col;
            }
            set
            {
                _col = value;
            }
        }
    }
}
