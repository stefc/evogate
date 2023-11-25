
using System;

using Xunit;
using FakeItEasy;

using stefc.gatelib;
using System.Threading.Tasks;

namespace tests
{
    public class TestFullAdder
    {
        
        private FullAdder _ = new FullAdder();

        [Theory]
        [InlineData(false,	false,	false,	false,	false)]
        [InlineData(false,	false,	true,	false,	true)]
        [InlineData(false,	true,	false,	false,	true)]
        [InlineData(false,	true,	true,	true,	false)]
        [InlineData(true,	false,	false,	false,	true)]
        [InlineData(true,	false,	true,	true,	false)]
        [InlineData(true,	true,	false,	true,	false)]
        [InlineData(true, 	true, 	true, 	true,	true)]
        public async void TestAdd(bool x, bool y, bool cIn, bool cOut, bool s)
        {
            var onSum = A.Fake<Func<bool,Task>>();
            var onCarry = A.Fake<Func<bool,Task>>();

            await _.Add(x, y, cIn, onSum, onCarry);

            A.CallTo( () => onCarry.Invoke(cOut)).MustHaveHappened();
            A.CallTo( () => onSum.Invoke(s)).MustHaveHappened();
        }

       
    }
}