using MealyToMoore.Automats;
using MealyToMoore.AutomatsUtils;

namespace MealyToMoore;

internal static class Program
{
    public const string MealyToMooreConversionType = "mealy2moore";
    public const string MooreToMealyConversionType = "moore2mealy";

    public static void Main( string[] args )
    {
        if ( args.Length != 3 )
        {
            throw new ArgumentException( "Incorrect arguments count" );
        }

        string conversionType = args[0];
        string sourceFilePath = args[1];
        string destinationFilePath = args[2];

        (IAutomatData inputAutomat, IAutomatData outputAutomat) automats = GetAutomats( conversionType, sourceFilePath );
        
        PrintDataToFile( automats.outputAutomat.GetCsvData(), destinationFilePath );
        
        GraphVisualizer.Visualize( automats.inputAutomat.GetNodes(), "./input_graph.png" );
        GraphVisualizer.Visualize( automats.outputAutomat.GetNodes(), "./output_graph.png" );
    }

    private static (IAutomatData inputAutomat, IAutomatData outputAutomat) GetAutomats( string conversionType, string sourceFilePath )
    {
        IAutomatData inputAutomat;
        IAutomatData outputAutomat;

        AutomatReader automatReader = new AutomatReader();
        switch ( conversionType )
        {
            case MealyToMooreConversionType:
                MealyAutomat mealyAutomat = automatReader.ReadMealyAutomat( sourceFilePath );
                MooreAutomat convertedMoore = mealyAutomat.ConvertToMoore();

                inputAutomat = mealyAutomat;
                outputAutomat = convertedMoore;
                break;
            case MooreToMealyConversionType:
                MooreAutomat mooreAutomat = automatReader.ReadMooreAutomat( sourceFilePath );
                MealyAutomat convertedMealy = mooreAutomat.ConvertToMealy();
                
                inputAutomat = mooreAutomat;
                outputAutomat = convertedMealy;
                break;
            default:
                throw new ArgumentException( "Unavailable conversion type" );
        }

        return ( inputAutomat, outputAutomat );
    }
    
    static void PrintDataToFile( string data, string filePath )
    {
        using StreamWriter writer = new StreamWriter( filePath );
        writer.Write( data );
    }
}