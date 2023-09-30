using MealyToMoore.Graph;

namespace MealyToMoore.Automats;

public class MealyAutomat : IAutomatData
{
    public List<string> States = new List<string>();
    public List<string> InputAlphabet = new List<string>();
    public List<List<string>> TransitionFunctions = new List<List<string>>();
    
    private const string SplitChar = ";";

    public Graph.Graph GetGraph()
    {
        List<Node> transactions = new List<Node>();
        
        for ( int line = 0; line < TransitionFunctions.Count; line++ )
        {
            string inputChar = InputAlphabet[ line ];

            for ( int column = 0; column < TransitionFunctions[ line ].Count; column++ )
            {
                string[] transactionArgs = TransitionFunctions[ line ][ column ].Split( "/" );
                
                string state = States[ column ];
                
                string toState = transactionArgs[ 0 ];
                string outputSignal = transactionArgs[ 1 ];
                
                transactions.Add( new Node
                {
                    From = state,
                    To = toState,
                    Label = inputChar + "/" + outputSignal
                } );
            }
        }
        
        return new Graph.Graph
        {
            Nodes = States.Select( x => new Node { From = x, To = x } ).ToList(),
            Transactions = transactions
        };
    }

    public string GetCsvData()
    {
        string data = "";

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