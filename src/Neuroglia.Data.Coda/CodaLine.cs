namespace Neuroglia.Data.Coda
{

    public abstract class CodaLine
    {

        protected CodaLine(CodaLineType lineType)
        {
            this.LineType = lineType;
        }

        public CodaLineType LineType { get; }

    }

}
