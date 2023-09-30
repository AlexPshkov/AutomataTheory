using System.Diagnostics;

namespace MealyToMoore;

public static class GraphVisualizer
{
    public static void VisualizeMealy( Graph.Graph graph, string outputFile )
    {
        if ( !graph.Nodes.Any() )
        {
            return;
        }
        
        string nodeTransactions = graph.Transactions.Aggregate( "", ( current, node ) => current + $"{node.From} -> {node.To} [label = \"{node.Label}\"];" );

        string dotGraph = $@"
            digraph finite_state_machine {{
                rankdir=LR;
                node [shape = circle];

                {nodeTransactions}
               
                node [shape = doublecircle]; 
                {graph.Nodes.First().From}; 
                {graph.Nodes.Last().To}; 
            }}
        ";
        
        string dotFilePath = "mealy_finite_state_machine.dot";
        File.WriteAllText(dotFilePath, dotGraph);
        
        string dotExePath = "./Graphviz/bin/dot.exe";
        
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
        
        File.Delete(dotFilePath);
    }

    public static void VisualizeMoore( Graph.Graph graph, string outputFile )
    {
        if ( !graph.Nodes.Any() )
        {
            return;
        }
        
        string nodes = graph.Nodes.Aggregate( "", ( current, node ) => current + $"{node.From} [label = \"{node.Label}\"];" );
        string nodeTransactions = graph.Transactions.Aggregate( "", ( current, node ) => current + $"{node.From} -> {node.To} [label = \"{node.Label}\"];" );

        string dotGraph = $@"
            digraph moore_state_machine {{
                rankdir=LR;
                node [shape = circle];

                {nodes} 

                {nodeTransactions}
              
            }}
        ";
        
        string dotFilePath = "moore_finite_state_machine.dot";
        File.WriteAllText(dotFilePath, dotGraph);
        
        string dotExePath = "./Graphviz/bin/dot.exe";
        
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
        
        File.Delete(dotFilePath);
    }
}