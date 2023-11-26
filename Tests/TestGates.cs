
using System;

using Xunit;
using FakeItEasy;

using stefc.gatelib;
using System.Threading.Tasks;

namespace tests;

public class TestGates
{
    
    private Gates _ = new();

    [Theory]
    [InlineData(0b000)] 
    [InlineData(0b010)] 
    [InlineData(0b100)] 
    [InlineData(0b111)] 
    public async void TestAnd(int bitPattern)
    {
        var a = Convert.ToBoolean((bitPattern & 0b100) >> 2);
        var b = Convert.ToBoolean((bitPattern & 0b010) >> 1);
        var y = Convert.ToBoolean(bitPattern & 0b001);

        var onResult = A.Fake<Func<bool,Task>>();

        await _.And(a, b, onResult);

        A.CallTo( () => onResult.Invoke(y)).MustHaveHappened();
    }

    [Theory]
    [InlineData(0b000)] 
    [InlineData(0b011)] 
    [InlineData(0b101)] 
    [InlineData(0b111)] 
    public async void TestOr(int bitPattern)
    {
        var a = Convert.ToBoolean((bitPattern & 0b100) >> 2);
        var b = Convert.ToBoolean((bitPattern & 0b010) >> 1);
        var y = Convert.ToBoolean(bitPattern & 0b001);

        var onResult = A.Fake<Func<bool,Task>>();

        await _.Or(a, b, onResult);

        A.CallTo( () => onResult.Invoke(y)).MustHaveHappened();
    }

    [Theory]
    [InlineData(0b001)] 
    [InlineData(0b011)] 
    [InlineData(0b101)] 
    [InlineData(0b110)] 
    public async void TestNand(int bitPattern)
    {
        var a = Convert.ToBoolean((bitPattern & 0b100) >> 2);
        var b = Convert.ToBoolean((bitPattern & 0b010) >> 1);
        var y = Convert.ToBoolean(bitPattern & 0b001);

        var onResult = A.Fake<Func<bool,Task>>();

        await _.Nand(a, b, onResult);

        A.CallTo( () => onResult.Invoke(y)).MustHaveHappened();
    }

    [Theory]
    [InlineData(0b001)] 
    [InlineData(0b010)] 
    [InlineData(0b100)] 
    [InlineData(0b110)] 
    public async void TestNor(int bitPattern)
    {
        var a = Convert.ToBoolean((bitPattern & 0b100) >> 2);
        var b = Convert.ToBoolean((bitPattern & 0b010) >> 1);
        var y = Convert.ToBoolean(bitPattern & 0b001);

        var onResult = A.Fake<Func<bool,Task>>();

        await _.Nor(a, b, onResult);

        A.CallTo( () => onResult.Invoke(y)).MustHaveHappened();
    }

    [Theory]
    [InlineData(0b000)] 
    [InlineData(0b011)] 
    [InlineData(0b101)] 
    [InlineData(0b110)] 
    public async void TestXor(int bitPattern)
    {
        var a = Convert.ToBoolean((bitPattern & 0b100) >> 2);
        var b = Convert.ToBoolean((bitPattern & 0b010) >> 1);
        var y = Convert.ToBoolean(bitPattern & 0b001);

        var onResult = A.Fake<Func<bool,Task>>();

        await _.Xor(a, b, onResult);

        A.CallTo( () => onResult.Invoke(y)).MustHaveHappened();
    }

    [Theory]
    [InlineData(0b001)] 
    [InlineData(0b010)] 
    [InlineData(0b100)] 
    [InlineData(0b111)] 
    public async void TestXnor(int bitPattern)
    {
        var a = Convert.ToBoolean((bitPattern & 0b100) >> 2);
        var b = Convert.ToBoolean((bitPattern & 0b010) >> 1);
        var y = Convert.ToBoolean(bitPattern & 0b001);

        var onResult = A.Fake<Func<bool,Task>>();

        await _.Xnor(a, b, onResult);

        A.CallTo( () => onResult.Invoke(y)).MustHaveHappened();
    }
}