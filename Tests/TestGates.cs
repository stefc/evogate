
using System;

using Xunit;
using FakeItEasy;

using stefc.gatelib;
using System.Threading.Tasks;

namespace tests
{
    public class TestGates
    {
        
        private Gates _ = new();

        [Theory]
        [InlineData(0,0,0)]
        [InlineData(0,1,0)]
        [InlineData(1,0,0)]
        [InlineData(1,1,1)]
        public async void TestAnd(int a, int b, int y)
        {
            var onResult = A.Fake<Func<bool,Task>>();

            await _.And(Convert.ToBoolean(a), Convert.ToBoolean(b), onResult);

            A.CallTo( () => onResult.Invoke(Convert.ToBoolean(y))).MustHaveHappened();
        }

        [Theory]
        [InlineData(0,0,0)]
        [InlineData(0,1,1)]
        [InlineData(1,0,1)]
        [InlineData(1,1,1)]
        public async void TestOr(int a, int b, int y)
        {
            var onResult = A.Fake<Func<bool,Task>>();

            await _.Or(Convert.ToBoolean(a), Convert.ToBoolean(b), onResult);

            A.CallTo( () => onResult.Invoke(Convert.ToBoolean(y))).MustHaveHappened();
        }

        [Theory]
        [InlineData(0,0,1)]
        [InlineData(0,1,1)]
        [InlineData(1,0,1)]
        [InlineData(1,1,0)]
        public async void TestNand(int a, int b, int y)
        {
            var onResult = A.Fake<Func<bool,Task>>();

            await _.Nand(Convert.ToBoolean(a), Convert.ToBoolean(b), onResult);

            A.CallTo( () => onResult.Invoke(Convert.ToBoolean(y))).MustHaveHappened();
        }

        [Theory]
        [InlineData(0,0,1)]
        [InlineData(0,1,0)]
        [InlineData(1,0,0)]
        [InlineData(1,1,0)]
        public async void TestNor(int a, int b, int y)
        {
            var onResult = A.Fake<Func<bool,Task>>();

            await _.Nor(Convert.ToBoolean(a), Convert.ToBoolean(b), onResult);

            A.CallTo( () => onResult.Invoke(Convert.ToBoolean(y))).MustHaveHappened();
        }

        [Theory]
        [InlineData(0,0,0)]
        [InlineData(0,1,1)]
        [InlineData(1,0,1)]
        [InlineData(1,1,0)]
        public async void TestXor(int a, int b, int y)
        {
            var onResult = A.Fake<Func<bool,Task>>();

            await _.Xor(Convert.ToBoolean(a), Convert.ToBoolean(b), onResult);

            A.CallTo( () => onResult.Invoke(Convert.ToBoolean(y))).MustHaveHappened();
        }

        [Theory]
        [InlineData(0,0,1)]
        [InlineData(0,1,0)]
        [InlineData(1,0,0)]
        [InlineData(1,1,1)]
        public async void TestXnor(int a, int b, int y)
        {
            var onResult = A.Fake<Func<bool,Task>>();

            await _.Xnor(Convert.ToBoolean(a), Convert.ToBoolean(b), onResult);

            A.CallTo( () => onResult.Invoke(Convert.ToBoolean(y))).MustHaveHappened();
        }
    }
}