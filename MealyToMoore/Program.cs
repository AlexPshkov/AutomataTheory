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

        (IAutomatData inputAutomat, IAutomatData outputAutomat) automats = Process( conversionType, sourceFilePath );
        
        PrintDataToFile( automats.outputAutomat.GetCsvData(), destinationFilePath );
    }

    private static (IAutomatData inputAutomat, IAutomatData outputAutomat) Process( string conversionType, string sourceFilePath )
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

                GraphVisualizer.VisualizeMealy( mealyAutomat.GetGraph(), "./input_graph.png" );
                GraphVisualizer.VisualizeMoore( convertedMoore.GetGraph(), "./output_graph.png" );
                break;
            case MooreToMealyConversionType:
                MooreAutomat mooreAutomat = automatReader.ReadMooreAutomat( sourceFilePath );
                MealyAutomat convertedMealy = mooreAutomat.ConvertToMealy();
                
                inputAutomat = mooreAutomat;
                outputAutomat = convertedMealy;
                
                GraphVisualizer.VisualizeMoore( mooreAutomat.GetGraph(), "./input_graph.png" );
                GraphVisualizer.VisualizeMealy( convertedMealy.GetGraph(), "./output_graph.png" );
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