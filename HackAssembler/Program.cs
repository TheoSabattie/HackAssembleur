if (args.Length < 1)
{
    Console.WriteLine("Usage: HackAssembleur <fichier-input>");
    return;
}

var logic = new Logic(fileName:args[0], new Parser(), new Code(), new SymbolTable());
logic.Run();