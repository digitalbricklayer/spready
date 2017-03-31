using Spready.Nodes;

namespace Spready
{
    public class SpreadsheetDetails
    {
        public string InputFilename { get; private set; }
        public SpreadyNode RootNode { get; private set; }

        public SpreadsheetDetails(string inputFilename, SpreadyNode theRootNode)
        {
            InputFilename = inputFilename;
            RootNode = theRootNode;
        }
    }
}