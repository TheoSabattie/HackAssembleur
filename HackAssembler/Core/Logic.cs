using System.Text;

public interface IParser
{
    public Command? ParseLine(string line);
}

public interface ICode
{
    public string ACommand(int value);
    public string CCommand(CDestination? dest, string comp, JumpType? jump);
}

public interface ISymbolTable
{
    int this[string key] { get; set; }
    bool TryGetValue(string key, out int value);
}

public class Logic
{
    private string fileName;
    private IParser parser;
    private ICode code;
    private ISymbolTable symbolTable;

    public Logic(string fileName, IParser parser, ICode code, ISymbolTable symbolTable)
    {
        this.fileName = fileName;
        this.parser = parser;
        this.code = code;
        this.symbolTable = symbolTable;
    }

    public void Run()
    {
        if (!File.Exists(fileName))
        {
            Console.WriteLine($"Error: File '{fileName}' not found.");
            return;
        }

        SymbolTable symbolsTable = new SymbolTable();

        string content = File.ReadAllText(fileName);
        string[] lines = content.Split("\n");

        List<Command> commands = new List<Command>(lines.Length);
        
        int lineCounter = 1;

        foreach (string line in lines)
        {
            try
            {
                Command? command = parser.ParseLine(line);
                ++lineCounter;

                if (command != null)
                {
                    commands.Add(command.Value);
                }
            } catch (Exception ex)
            {
                throw new Exception($"Error parsing line {lineCounter}: {ex.Message}");
            }
        }

        // label pass, we remove them because we don't need them anymore, but we store their index in the symbols dictionary
        for (int i = 0; i < commands.Count; ++i)
        {
            if (commands[i].Type == CommandType.Label)
            {
                symbolsTable[commands[i].Label] = i;
                commands.RemoveAt(i);
                --i;
            }
        }

        Code write = new Code();
        var output = new StringBuilder();
        int varPointer = 16;

        for (int i = 0; i < commands.Count; ++i)
        {
            if (output.Length > 0)
            {
                output.AppendLine();
            }

            if (commands[i].Type == CommandType.A)
            {
                if (!int.TryParse(commands[i].ADest, out int value))
                {
                    if (!symbolsTable.TryGetValue(commands[i].ADest, out value))
                    {
                        value = symbolsTable[commands[i].ADest] = varPointer;
                        ++varPointer;
                    }
                }

                output.Append(write.ACommand(value));
            } else if (commands[i].Type == CommandType.C)
            {
                output.Append(write.CCommand(commands[i].CDest, commands[i].Comp, commands[i].Jump));
            }
        }

        string hackFilePath = Path.ChangeExtension(fileName, ".hack");
        File.WriteAllText(hackFilePath, output.ToString());
    }
}