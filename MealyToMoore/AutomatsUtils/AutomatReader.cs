using MealyToMoore.Automats;

namespace MealyToMoore.AutomatsUtils;

public class AutomatReader
{
    private const string SplitChar = ";";
    
    public MealyAutomat ReadMealyAutomat( string filePath )
    {
        List<string> rawStrings = GetRawStrings( filePath );
        
        MealyAutomat mealyAutomat = new MealyAutomat();
        
        if ( !rawStrings.Any() )
        {
            return mealyAutomat;
        }
        
        string[] dirtyStates = rawStrings[ 0 ].Split( SplitChar );
        mealyAutomat.States = new ArraySegment<string>( dirtyStates, 1, dirtyStates.Length - 1 ).ToList();
        
        for ( int i = 1; i < rawStrings.Count; i++ )
        {
            string[] values = rawStrings[ i ].Split( SplitChar );
            
            mealyAutomat.InputAlphabet.Add( values[ 0 ] );
            mealyAutomat.TransitionFunctions.Add( new ArraySegment<string>( values, 1, values.Length - 1 ).ToList() );
        }

        return mealyAutomat;
    }
    
    public MooreAutomat ReadMooreAutomat( string filePath )
    {
        List<string> rawStrings = GetRawStrings( filePath );
        
        MooreAutomat mooreAutomat = new MooreAutomat();
        
        if ( !rawStrings.Any() )
        {
            return mooreAutomat;
        }

        string[] dirtyOutputAlphabet = rawStrings[ 0 ].Split( SplitChar );
        mooreAutomat.OutputAlphabet = new ArraySegment<string>( dirtyOutputAlphabet, 1, dirtyOutputAlphabet.Length - 1 ).ToList();
        
        string[] dirtyStates = rawStrings[ 1 ].Split( SplitChar );
        mooreAutomat.States = new ArraySegment<string>( dirtyStates, 1, dirtyStates.Length - 1 ).ToList();

        for ( int i = 2; i < rawStrings.Count; i++ )
        {
            string[] values = rawStrings[ i ].Split( SplitChar );
            mooreAutomat.InputAlphabet.Add( values[ 0 ] );
            mooreAutomat.TransitionFunctions.Add( new ArraySegment<string>( values, 1, values.Length - 1 ).ToList() );
        }

        return mooreAutomat;
    }
    
    private List<string> GetRawStrings( string filePath )
    {
        List<string> info = new List<string>();

        using StreamReader reader = new StreamReader( filePath );
        while ( !reader.EndOfStream )
        {
            info.Add( reader.ReadLine()! );
        }

        return info;
    }
}