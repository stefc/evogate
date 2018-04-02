using System;
using Xunit;
using FakeItEasy;
using stefc.gatelib;
using System.IO;
using System.Text;
using System.Linq;

namespace tests
{
	public class TestICFactory
	{
		[Fact]
		public void TestReadJson()
		{
            string path = Directory.GetCurrentDirectory();

            string jsonDoc = File.ReadAllText(Path.Combine(path, "Fulladder.json"), Encoding.UTF8);

            IC ic = ICFactory.CreateFromJson(jsonDoc);

			Assert.Equal("Full-Adder", ic.Name);
			Assert.Equal(3, ic.Input);
			Assert.Equal(2, ic.Output);
			Assert.Equal(14, ic.Gates);

            Assert.NotNull(ic.Wiring);
            Assert.Equal(19, ic.Wiring.Nodes.Count);

            var gateA = ic.Wiring.Nodes.Find( node => node.Value.Id == ":a");

            var edges = gateA.Neighbors.Select( gate => gate.Id).OrderBy( id => id);
            

            Assert.Equal(new[] { "h1","h2","h2","i3" }.OrderBy( id => id), edges);
		}
		
	}
}

