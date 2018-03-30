using System;
using Xunit;
using FakeItEasy;
using stefc.gatelib;
using System.Linq;

namespace tests {
    public class TestGraph
	{
		[Fact]
		public void WebGraph()
		{
            Graph<string> web = new Graph<string>()
            .AddNode("Privacy.htm")
            .AddNode("People.aspx")
            .AddNode("About.htm")
            .AddNode("Index.htm")
            .AddNode("Products.aspx")
            .AddNode("Contact.aspx");

            Assert.Equal(6, web.Count);

            web = web
            .AddDirectedEdge("People.aspx", "Privacy.htm")   // People -> Privacy

            .AddDirectedEdge("Privacy.htm", "Index.htm")     // Privacy -> Index
            .AddDirectedEdge("Privacy.htm", "About.htm")     // Privacy -> About

            .AddDirectedEdge("About.htm", "Privacy.htm")     // About -> Privacy
            .AddDirectedEdge("About.htm", "People.aspx")     // About -> People
            .AddDirectedEdge("About.htm", "Contact.aspx")    // About -> Contact

            .AddDirectedEdge("Index.htm", "About.htm")       // Index -> About
            .AddDirectedEdge("Index.htm", "Contact.aspx")    // Index -> Contacts
            .AddDirectedEdge("Index.htm", "Products.aspx")   // Index -> Products

            .AddDirectedEdge("Products.aspx", "Index.htm")   // Products -> Index
            .AddDirectedEdge("Products.aspx", "People.aspx");// Products -> People

            Assert.Equal(6, web.Count);

            var aboutNode = web.Nodes.First( n => n.Value == "About.htm");
            Assert.Equal(new []{"Privacy.htm","People.aspx","Contact.aspx"}.OrderBy( x => x), aboutNode.Neighbors.OrderBy( x => x));
        }
    }
}