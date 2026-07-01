public class SymbolTable : Dictionary<string, int>, ISymbolTable
{
    public SymbolTable()
    {
        // Predefined symbols
        this["SP"] = 0;
        this["LCL"] = 1;
        this["ARG"] = 2;
        this["THIS"] = 3;
        this["THAT"] = 4;

        for (int i = 0; i < 16; ++i)
        {
            this[$"R{i}"] = i;
        }

        this["SCREEN"] = 16384;
        this["KBD"] = 24576;
    }
}