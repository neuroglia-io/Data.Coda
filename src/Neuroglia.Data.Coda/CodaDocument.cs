using System.Collections.Generic;
using System.Linq;

namespace Neuroglia.Data.Coda
{

    public class CodaDocument
    {

        public CodaDocument(IEnumerable<CodaLine> lines, IEnumerable<CodaStatement> statements)
        {
            this.Lines = lines.ToList().AsReadOnly();
            this.Statements = statements.ToList().AsReadOnly();
        }

        public IReadOnlyCollection<CodaLine> Lines { get; }

        public IReadOnlyCollection<CodaStatement> Statements { get; }

    }

}
