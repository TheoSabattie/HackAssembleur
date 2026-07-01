public static class CompType
{
    public const string Zero = "0"; 
    public const string One = "1";
    public const string NegOne = "-1";
    public const string D = "D";
    public const string A = "A";
    public const string NotD = "!D";
    public const string NotA = "!A";
    public const string NegD = "-D";
    public const string NegA = "-A";
    public const string DPlusOne = "D+1";
    public const string APlusOne = "A+1";
    public const string DMinusOne = "D-1";
    public const string AMinusOne = "A-1";
    public const string DPlusA = "D+A";
    public const string DMinusA = "D-A";
    public const string AMinusD = "A-D";
    public const string DAndA = "D&A";
    public const string DOrA = "D|A";
    public const string M = "M";
    public const string NotM = "!M";
    public const string NegM = "-M";
    public const string MPlusOne = "M+1";
    public const string MMinusOne = "M-1";
    public const string DPlusM = "D+M";
    public const string DMinusM = "D-M";
    public const string MMinusD = "M-D";
    public const string DAndM = "D&M";
    public const string DOrM = "D|M";

    private static readonly List<string> _all = new List<string>
    {
        Zero, One, NegOne, D, A, NotD, NotA, NegD, NegA,
        DPlusOne, APlusOne, DMinusOne, AMinusOne,
        DPlusA, DMinusA, AMinusD, DAndA, DOrA,
        M, NotM, NegM, MPlusOne, MMinusOne,
        DPlusM, DMinusM, MMinusD, DAndM, DOrM
    };

    public static IReadOnlyList<string> All => _all;

    public static bool IsValid(string comp)
    {
        return _all.Contains(comp);
    }
}