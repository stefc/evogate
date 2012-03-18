using System;
using System.Collections;
using NUnit.Framework;
using stefc.evolib;
using stefc.gatelib;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
	[TestFixture]
	public class TestEvolution
	{
		private BitArray CreateInput(bool A, bool B, bool C)
		{
			return new BitArray(new bool[]{A,B,C});
		}
		
		private BitArray CreateInput(bool A, bool B)
		{
			return new BitArray(new bool[]{A,B});
		}
		
		[Test]
		public void TestXorEvolution()
		{
			Func<BitArray,double> fitness = (chromosome) =>
			{
				Wiring wiring = new Wiring(2,1,4, chromosome);
				
				FPGA xorGate = new FPGA(wiring);
				
				double total = 0.0;
				if(!xorGate.Output(CreateInput(false,false))[0]) total += 1.0;
				if(!xorGate.Output(CreateInput(true,true))[0])	total += 1.0;
				if(xorGate.Output(CreateInput(true,false))[0]) total += 1.0;
				if(xorGate.Output(CreateInput(false,true))[0]) total += 1.0;
				if(total == 0.0) return 0.0;
				
				return 4.0 / total;
			};
			
			Random rnd = new Random();
			
			Func<BitArray> init = () => 
			{
				BitArray result = new BitArray(Wiring.CalcLength(2,1,4));
				for(int i=0; i<result.Length; i++)
				{
					if(rnd.NextDouble()<0.5)
						result[i]=true;
				}
				return result;
			};
			
			Evolution evo = new Evolution(fitness,0.001, new int[]{
				0,2,4,6,8,10,12,14,
				16,19,22,25,
				28,30,32, 34,36, 38});
			
			int n = 100; 
			IList<BitArray> population = new List<BitArray>(n);
			for(int i=0; i<n; i++)
				population.Add(init());
			
			population = evo.Run(population, (generation,error) => { 
				return 
					(generation > 1000); // || (error > 0.99);
			});
			
			foreach(BitArray wiring in population.Take(10))
			{	
				Wiring temp = new Wiring(2,1,4,wiring);
				Console.WriteLine (temp.ToString());
				Console.WriteLine (fitness(wiring));
			}
		}
	}
}