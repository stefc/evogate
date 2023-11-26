
using System;

using Xunit;
using FakeItEasy;

using stefc.gatelib;
using System.Threading.Tasks;

namespace tests;

public class TestFullAdder
{
    
    private FullAdder _ = new();

    [Theory]
    [InlineData(0b000_00)]
    [InlineData(0b001_01)]
    [InlineData(0b010_01)]
    [InlineData(0b011_10)]
    [InlineData(0b100_01)]
    [InlineData(0b101_10)]
    [InlineData(0b110_10)]
    [InlineData(0b111_11)]
    public async void TestAdd(byte bits)
    {
        var x = Convert.ToBoolean(bits & 0b100_00);
        var y = Convert.ToBoolean(bits & 0b010_00);
        var cIn = Convert.ToBoolean(bits & 0b001_00);
        var cOut = Convert.ToBoolean(bits & 0b000_10);
        var s = Convert.ToBoolean(bits & 0b000_01);
        var onSum = A.Fake<Func<bool,Task>>();
        var onCarry = A.Fake<Func<bool,Task>>();

        await _.Add(x, y, cIn, onSum, onCarry);

        A.CallTo( () => onCarry.Invoke(cOut)).MustHaveHappened();
        A.CallTo( () => onSum.Invoke(s)).MustHaveHappened();
    }

   
}