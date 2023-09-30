namespace MealyToMoore.Automats;

public class MooreAutomat : IAutomatData
{
    public List<string> OutputAlphabet = new List<string>();
    public List<string> States = new List<string>();
    public List<string> InputAlphabet = new List<string>();
    public List<List<string>> TransitionFunctions = new List<List<string>>();
        
    private const string SplitChar = ";";

    public List<Node> GetNodes()
    {
        List<Node> nodes = new List<Node>();
        
        for ( int line = 0; line < TransitionFunctions.Count; line++ )
        {
            string inputChar = InputAlphabet[ line ];

            for ( int column = 0; column < TransitionFunctions[ line ].Count; column++ )
            {
                string state = States[ column ];
                string signal = OutputAlphabet[ column ];
                
                string toState = TransitionFunctions[ line ][ column ];
                
                nodes.Add( new Node
                {
                    From = state,
                    To = toState + "/" + signal,
                    Label = inputChar
                } );
            }
        }
        
        return nodes;
    }

    public string GetCsvData()
    {
        string data = "";

        data += SplitChar;
        data += OutputAlphabet.Aggregate( ( total, part ) => $"{total}{SplitChar}{part}" );
        data += "\n";

        data += SplitChar;
        data += States.Aggregate( ( total, part ) => $"{total}{SplitChar}{part}" );
        data += "\n";

        for ( int i = 0; i < InputAlphabet.Count; i++ )
        {
            data += InputAlphabet[ i ] + SplitChar;
            data += TransitionFunctions[ i ].Aggregate( ( total, part ) => $"{total}{SplitChar}{part}" );
            data += "\n";
        }

        return data;
    }
}