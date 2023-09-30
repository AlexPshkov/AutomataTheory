using System.Diagnostics;

namespace MealyToMoore;

public static class GraphVisualizer
{
    public static void Visualize( IEnumerable<Node> nodes, string outputFile )
    {
        if ( !nodes.Any() )
        {
            return;
        }
        
        string nodesString = nodes.Aggregate( "", ( current, node ) => current + $"{node.From} -> {node.To} [label = \"{node.Label}\"];" );

        string dotGraph = $@"
            digraph finite_state_machine {{
                rankdir=LR;
                node [shape = circle];

                {nodesString}
               
                node [shape = doublecircle]; 
                {nodes.First().From}; 
                {nodes.Last().To}; 
            }}
        ";

        // Создаем временный файл для сохранения описания графа
        string dotFilePath = "finite_state_machine.dot";
        File.WriteAllText(dotFilePath, dotGraph);
        
        string dotExePath = "./Graphviz/bin/dot.exe";

        // Создаем процесс для выполнения команды dot и отображения графа
        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = dotExePath,
            Arguments = $"-Tpng {dotFilePath} -o {outputFile}",
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        Process process = new Process { StartInfo = startInfo };
        process.Start();
        process.WaitForExit();

        // Удаляем временный файл с описанием графа
        File.Delete(dotFilePath);

        Console.WriteLine($"Граф автомата Мили создан и сохранен в {outputFile}");
    }

    public static void VisualizeMoore( IEnumerable<Node> nodes, string outputFile )
    {
        if ( !nodes.Any() )
        {
            return;
        }
        
        string nodesString = nodes.Aggregate( "", ( current, node ) => current + $"{node.From} -> {node.To} [label = \"{node.Label}\"];" );

        string dotGraph = $@"
            digraph finite_state_machine {{
                rankdir=LR;
                node [shape = circle];

                // Определяем состояния автомата и переходы между ними
                S0 [label = """"S0/0""""]; 
                S1 [label = """"S1/1""""]; 
                S2 [label = """"S2/0""""]; 

                S0 -> S1 [label = """"0""""]; 
                S0 -> S0 [label = """"1""""]; 
                S1 -> S2 [label = """"0""""]; 
                S1 -> S0 [label = """"1""""]; 
                S2 -> S1 [label = """"0""""]; 
                S2 -> S2 [label = """"1""""]; 
                
                // Определяем выходы для каждого состояния
                S0 [shape=doublecircle, label = """"S0/OUT=0""""]; 
                S1 [shape=doublecircle, label = """"S1/OUT=1""""]; 
                S2 [shape=doublecircle, label = """"S2/OUT=0""""]; 
            }}
        ";

        // Создаем временный файл для сохранения описания графа
        string dotFilePath = "finite_state_machine.dot";
        File.WriteAllText(dotFilePath, dotGraph);
        
        string dotExePath = "./Graphviz/bin/dot.exe";

        // Создаем процесс для выполнения команды dot и отображения графа
        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = dotExePath,
            Arguments = $"-Tpng {dotFilePath} -o {outputFile}",
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        Process process = new Process { StartInfo = startInfo };
        process.Start();
        process.WaitForExit();

        // Удаляем временный файл с описанием графа
        File.Delete(dotFilePath);

        Console.WriteLine($"Граф автомата Мура создан и сохранен в {outputFile}");
    }
}