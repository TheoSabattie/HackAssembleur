public class Parser : IParser
{
    public Parser()
    {
    }

    public Command? ParseLine(string line)
    {
        string trimmedLine = line.Trim();

        // Ignore empty lines and comments
        if (string.IsNullOrEmpty(trimmedLine) || trimmedLine.StartsWith("//"))
        {
            return null;
        }

        // Remove inline comments
        int commentIndex = trimmedLine.IndexOf("//");

        if (commentIndex != -1)
        {
            trimmedLine = trimmedLine.Substring(0, commentIndex).Trim();
        }

        if (trimmedLine.StartsWith("@"))
        {
            return new Command
            {
                Type = CommandType.A,
                ADest = trimmedLine.Substring(1)
            };
        }
        else if (trimmedLine.StartsWith("("))
        {
            if (!trimmedLine.EndsWith(")"))
            {
                throw new Exception($"Invalid label format: {trimmedLine}. Action: ensure label starts with '(' and ends with ')'");
            }

            return new Command
            {
                Type = CommandType.Label,
                Label = trimmedLine.Substring(1, trimmedLine.Length - 2)
            };
        }
        else
        {
            int jumpIndex = trimmedLine.IndexOf(";") + 1;
            JumpType? jump = null;
            int destCompMaxIndex = trimmedLine.Length;

            if (jumpIndex != 0)
            {
                string tmp = trimmedLine.Substring(jumpIndex);
                jump = ParseJumpType(tmp);
                destCompMaxIndex = jumpIndex - 1;
            }

            (CDestination? dest, string comp) = ParseDestComp(trimmedLine.Substring(0, destCompMaxIndex));

            return new Command
            {
                Type = CommandType.C,
                CDest = dest,
                Comp = comp,
                Jump = jump
            };
        }
    }

    private static (CDestination? dest, string comp) ParseDestComp(string destComp)
    {
        int equalsIndex = destComp.IndexOf("=");

        if (equalsIndex == -1)
        {
            return (null, destComp);
        }

        string destStr = destComp.Substring(0, equalsIndex);

        if (!Enum.TryParse(destStr, out CDestination dest))
        {
            throw new Exception($"Invalid destination: {destStr}. Action: ensure the destination is one of the following: {string.Join(", ", Enum.GetNames<CDestination>())}");
        }

        string comp = destComp.Substring(equalsIndex + 1);

        if (!CompType.IsValid(comp))
        {
            throw new Exception($"Invalid computation: {comp}. Action: ensure the computation is one of the following: {string.Join(", ", CompType.All)}");
        }

        return (dest, comp);
    }

    private static JumpType? ParseJumpType(string tmp)
    {
        if (Enum.TryParse(tmp, out JumpType jumpType))
        {
            return jumpType;
        }

        if (tmp == "null" || string.IsNullOrEmpty(tmp))
        {
            return null;
        }

        throw new Exception($"Invalid jump type: {tmp}");
    }
}
