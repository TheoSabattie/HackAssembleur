using System.Text;

public class Code : ICode
{
    public Code()
    {
        
    }

    public string ACommand(int value)
    {
        if (value >= 0b1000000000000000)
        {
            throw new Exception($"A-command value {value} exceeds the 15-bit limit. Action: ensure the value is between 0 and 32767.");
        }

        return Convert.ToString(value, 2).PadLeft(16, '0');
    }

    public string CCommand(CDestination? dest, string comp, JumpType? jump)
    {
        var tmp = new StringBuilder(16);
        tmp.Append(111);

        if (comp != null)
        {
            switch (comp)
            {
                case CompType.Zero: tmp.Append("0101010"); break;
                case CompType.One: tmp.Append("0111111"); break;
                case CompType.NegOne: tmp.Append("0111010"); break;
                case CompType.D: tmp.Append("0001100"); break;
                case CompType.A: tmp.Append("0110000"); break;
                case CompType.NotD: tmp.Append("0001101"); break;
                case CompType.NotA: tmp.Append("0110001"); break;
                case CompType.NegD: tmp.Append("0001111"); break;
                case CompType.NegA: tmp.Append("0110011"); break;
                case CompType.DPlusOne: tmp.Append("0011111"); break;
                case CompType.APlusOne: tmp.Append("0110111"); break;
                case CompType.DMinusOne: tmp.Append("0001110"); break;
                case CompType.AMinusOne: tmp.Append("0110010"); break;
                case CompType.DPlusA: tmp.Append("0000010"); break;
                case CompType.DMinusA: tmp.Append("0010011"); break;
                case CompType.AMinusD: tmp.Append("0000111"); break;
                case CompType.DAndA: tmp.Append("0000000"); break;
                case CompType.DOrA: tmp.Append("0010101"); break;

                // M variants
                case CompType.M: tmp.Append("1110000"); break;
                case CompType.NotM: tmp.Append("1110001"); break;
                case CompType.NegM: tmp.Append("1110011"); break;
                case CompType.MPlusOne: tmp.Append("1110111"); break;
                case CompType.MMinusOne: tmp.Append("1110010"); break;
                case CompType.DPlusM: tmp.Append("1000010"); break;
                case CompType.DMinusM: tmp.Append("1010011"); break;
                case CompType.MMinusD: tmp.Append("1000111"); break;
                case CompType.DAndM: tmp.Append("1000000"); break;
                case CompType.DOrM: tmp.Append("1010101"); break;

                default:
                    throw new Exception($"Invalid computation value '{comp}'. Action: ensure the computation is one of the valid Hack assembly language computations.");
            }
        } else
        {
            tmp.Append("0000000");
        }

        if (dest.HasValue)
        {
            switch (dest.Value)
            {
                case CDestination.M: tmp.Append("001"); break;
                case CDestination.D: tmp.Append("010"); break;
                case CDestination.MD: tmp.Append("011"); break;
                case CDestination.A: tmp.Append("100"); break;
                case CDestination.AM: tmp.Append("101"); break;
                case CDestination.AD: tmp.Append("110"); break;
                case CDestination.AMD: tmp.Append("111"); break;
            }
        }
        else
        {
            tmp.Append("000");
        }

        if (jump.HasValue)
        {
            switch (jump.Value)
            {
                case JumpType.JGT: tmp.Append("001"); break;
                case JumpType.JEQ: tmp.Append("010"); break;
                case JumpType.JGE: tmp.Append("011"); break;
                case JumpType.JLT: tmp.Append("100"); break;
                case JumpType.JNE: tmp.Append("101"); break;
                case JumpType.JLE: tmp.Append("110"); break;
                case JumpType.JMP: tmp.Append("111"); break;
            }
        }
        else
        {
            tmp.Append("000");
        }

        return tmp.ToString();
    }
}