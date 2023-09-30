using MealyToMoore.Automats;

namespace MealyToMoore.AutomatsUtils;

public static class AutomatConverter
{
    public static MealyAutomat ConvertToMealy( this MooreAutomat mooreAutomat )
    {
        MealyAutomat mealyAutomat = new MealyAutomat
        {
            States = mooreAutomat.States,
            InputAlphabet = mooreAutomat.InputAlphabet
        };

        for ( int i = 0; i < mooreAutomat.TransitionFunctions.Count; i++ )
        {
            mealyAutomat.TransitionFunctions.Add( new List<string>() );
            for ( int j = 0; j < mooreAutomat.TransitionFunctions[ i ].Count; j++ )
            {
                int indexOfState = mooreAutomat.States.IndexOf( mooreAutomat.TransitionFunctions[ i ][ j ] );
                mealyAutomat.TransitionFunctions[ i ].Add( mooreAutomat.TransitionFunctions[ i ][ j ] + "/" + mooreAutomat.OutputAlphabet[ indexOfState ] );
            }
        }

        return mealyAutomat;
    }
    
    
    public static MooreAutomat ConvertToMoore( this MealyAutomat mealyAutomat )
    {
        MooreAutomat mooreAutomat = new MooreAutomat();

        Dictionary<string, string> mooreStatesMap = new Dictionary<string, string>();
        Dictionary<string, string> viceVersaMooreStatesMap = new Dictionary<string, string>();
        foreach ( List<string> transitionFunctions in mealyAutomat.TransitionFunctions )
        {
            foreach ( string transitionFunction in transitionFunctions )
            {
                if ( viceVersaMooreStatesMap.ContainsKey( transitionFunction ) )
                {
                    continue;
                }

                string newState = "S" + mooreAutomat.States.Count;
                mooreAutomat.States.Add( newState );
                mooreStatesMap.Add( newState, transitionFunction );
                viceVersaMooreStatesMap.Add( transitionFunction, newState );
            }
        }

        foreach ( string state in mooreAutomat.States )
        {
            mooreAutomat.OutputAlphabet.Add( mooreStatesMap[ state ].Split( "/" )[ 1 ] );
        }

        mooreAutomat.InputAlphabet = mealyAutomat.InputAlphabet;

        foreach ( string inputLetter in mooreAutomat.InputAlphabet )
        {
            mooreAutomat.TransitionFunctions.Add( new List<string>() );
        }

        foreach ( string state in mooreAutomat.States )
        {
            string prevState = mooreStatesMap[ state ].Split( "/" )[ 0 ];
            int mealyStatesIndex = mealyAutomat.States.IndexOf( prevState );

            for ( int i = 0; i < mooreAutomat.InputAlphabet.Count; i++ )
            {
                mooreAutomat.TransitionFunctions[ i ]
                    .Add( viceVersaMooreStatesMap[ mealyAutomat.TransitionFunctions[ i ][ mealyStatesIndex ] ] );
            }
        }

        return mooreAutomat;
    } 
}