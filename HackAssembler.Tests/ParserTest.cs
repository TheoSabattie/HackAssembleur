namespace HackAssembleur.Tests;

public class ParserTest
{
    [Fact]
    public void EmptyLineOrComment_ShouldReturnNull()
    {
        var parser = new Parser();

        Command? command1 = parser.ParseLine("");
        Assert.Null(command1);

        Command? command2 = parser.ParseLine("   ");
        Assert.Null(command2);

        Command? command3 = parser.ParseLine("// This is a comment");
        Assert.Null(command3);
    }

    [Fact]
    public void ACommand()
    {
        var parser = new Parser();
        Command? command = parser.ParseLine("@2");
        Assert.NotNull(command);
        Assert.Equal(CommandType.A, command.Value.Type);
        Assert.Equal("2", command.Value.ADest);
    }

    [Fact]
    public void CCommand_DestComp()
    {
        var parser = new Parser();

        foreach (string comp in CompType.All)
        {
            foreach (var dest in Enum.GetValues<CDestination>())
            {
                Command? command = parser.ParseLine($"{dest}={comp}");
                Assert.NotNull(command);
                Assert.Equal(CommandType.C, command.Value.Type);
                Assert.Equal(dest, command.Value.CDest);
                Assert.Equal(comp, command.Value.Comp);
                Assert.Null(command.Value.Jump);
            }
        }
    }

    [Fact]
    public void CCommand_JUMP()
    {
        var parser = new Parser();

        foreach (string comp in CompType.All)
        {
            foreach (var jump in Enum.GetValues<JumpType>())
            {
                Command? command = parser.ParseLine($"{comp};{jump}");
                Assert.NotNull(command);
                Assert.Equal(CommandType.C, command.Value.Type);
                Assert.Equal(jump, command.Value.Jump);
                Assert.Equal(comp, command.Value.Comp);
                Assert.Null(command.Value.CDest);
            }
        }
    }
}
