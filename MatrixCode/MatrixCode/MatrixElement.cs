namespace MatrixTask
{
    public class MatrixElement
    {
        public MatrixElement() : this(0,0) {}

        public MatrixElement(int line, int column)
        {
            Line = line;
            Column = column;
        }

        public MatrixElement(int line, int column, int value)
        {
            Line = line;
            Column = column;
            Value = value;
        }

        public MatrixElement NextItem { get; set; }
        public int Line { get; private set; }
        public int Column { get; private set; }
        public int Value { get; set; }
    }
}