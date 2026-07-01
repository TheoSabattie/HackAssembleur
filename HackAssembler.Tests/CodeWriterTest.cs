namespace HackAssembleur.Tests;

public class CodeWriterTest
{
    [Fact]
    public void ACommand()
    {
        var codeWriter = new Code();
        string result = codeWriter.ACommand(2);
        Assert.Equal("0000000000000010", result);
    }

    [Fact]
    public void CCommand_DestComp()
    {
        var codeWriter = new Code();
        string result = codeWriter.CCommand(CDestination.D, CompType.DPlusA, null);
        Assert.Equal("1110000010010000", result);

        string result2 = codeWriter.CCommand(CDestination.M, CompType.D, null);
        Assert.Equal("1110001100001000", result2);
    }
}
