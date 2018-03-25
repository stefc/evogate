
using System;
using System.Collections;
using System.Linq;

using Xunit;
using FakeItEasy;

using stefc.gatelib;

namespace tests
{
    public class TestGates
    {
        
        private Gates _ = new Gates();

        [Theory]
        [InlineData(0,0,0)]
        [InlineData(0,1,0)]
        [InlineData(1,0,0)]
        [InlineData(1,1,1)]
        public void TestAnd(int a, int b, int y)
        {
            var onResult = A.Fake<Action<bool>>();

            _.And(Convert.ToBoolean(a), Convert.ToBoolean(b), onResult);

            A.CallTo( () => onResult.Invoke(Convert.ToBoolean(y))).MustHaveHappened();
        }

        [Theory]
        [InlineData(0,0,0)]
        [InlineData(0,1,1)]
        [InlineData(1,0,1)]
        [InlineData(1,1,1)]
        public void TestOr(int a, int b, int y)
        {
            var onResult = A.Fake<Action<bool>>();

            _.Or(Convert.ToBoolean(a), Convert.ToBoolean(b), onResult);

            A.CallTo( () => onResult.Invoke(Convert.ToBoolean(y))).MustHaveHappened();
        }

        [Theory]
        [InlineData(0,0,1)]
        [InlineData(0,1,1)]
        [InlineData(1,0,1)]
        [InlineData(1,1,0)]
        public void TestNand(int a, int b, int y)
        {
            var onResult = A.Fake<Action<bool>>();

            _.Nand(Convert.ToBoolean(a), Convert.ToBoolean(b), onResult);

            A.CallTo( () => onResult.Invoke(Convert.ToBoolean(y))).MustHaveHappened();
        }

        [Theory]
        [InlineData(0,0,1)]
        [InlineData(0,1,0)]
        [InlineData(1,0,0)]
        [InlineData(1,1,0)]
        public void TestNor(int a, int b, int y)
        {
            var onResult = A.Fake<Action<bool>>();

            _.Nor(Convert.ToBoolean(a), Convert.ToBoolean(b), onResult);

            A.CallTo( () => onResult.Invoke(Convert.ToBoolean(y))).MustHaveHappened();
        }

        [Theory]
        [InlineData(0,0,0)]
        [InlineData(0,1,1)]
        [InlineData(1,0,1)]
        [InlineData(1,1,0)]
        public void TestXor(int a, int b, int y)
        {
            var onResult = A.Fake<Action<bool>>();

            _.Xor(Convert.ToBoolean(a), Convert.ToBoolean(b), onResult);

            A.CallTo( () => onResult.Invoke(Convert.ToBoolean(y))).MustHaveHappened();
        }

        [Theory]
        [InlineData(0,0,1)]
        [InlineData(0,1,0)]
        [InlineData(1,0,0)]
        [InlineData(1,1,1)]
        public void TestXnor(int a, int b, int y)
        {
            var onResult = A.Fake<Action<bool>>();

            _.Xnor(Convert.ToBoolean(a), Convert.ToBoolean(b), onResult);

            A.CallTo( () => onResult.Invoke(Convert.ToBoolean(y))).MustHaveHappened();
        }
    }
}