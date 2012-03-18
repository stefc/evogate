using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

using stefc.evolib.contract;

namespace stefc.evolib
{
	public class Evolution : IEvolution
	{
		private readonly Random rnd; 
		private readonly Func<BitArray, double> fitness;
		private readonly double mutatonProbability;
		private readonly IList<int> crossOverIndex;
		
		private double topFitness = 0.0;
		
		public Evolution (Func<BitArray, double> fitness, double mutatonProbability, IList<int> crossOverIndex)
		{
			rnd = new Random();
			this.fitness=fitness;
			this.mutatonProbability=mutatonProbability;
			this.crossOverIndex=crossOverIndex;
		}

		#region IEvolution implementation
		public IList<BitArray> Selection (IList<BitArray> population, Func<BitArray, double> fitness)
		{
			int populationCount = population.Count();
			
			// calc the fitness of each chromosome 
			var overallFitness = 
				from c in population
				select fitness(c);
			
			// calc the relative probability
			double totalFitness = overallFitness.Sum();
			
			this.topFitness = overallFitness.Max();
			
			IEnumerable<double> probabilities;
			if(totalFitness == 0.0)
			{	
				probabilities = 
					Enumerable.Repeat(1.0/populationCount, populationCount);
			}
			else
			{
				probabilities = 	
					from f in overallFitness
					select f / totalFitness;
			}
			
			Console.WriteLine (this.topFitness);
		
			// create the interval with probability of each chromosome
			IList<KeyValuePair<double,BitArray>> interval = new List<KeyValuePair<double,BitArray>>(populationCount);
			using (IEnumerator<double> prob = probabilities.GetEnumerator())
				using(IEnumerator<BitArray> popu = population.GetEnumerator()) 
	        	{ 
					double marker = 0.0;
	            	while (prob.MoveNext() && popu.MoveNext()) 
					{ 
						marker += prob.Current;
						interval.Add(new KeyValuePair<double,BitArray>(marker, popu.Current));
	            	} 
	        	} 
			
			// fill the resulting new population
			IList<BitArray> newPopulation = new List<BitArray>(populationCount);
			while(newPopulation.Count < populationCount)
			{
				double z = rnd.NextDouble();
				var query = from t in interval where t.Key < z select t;
				BitArray c;
				if (query.Any()) 
					c = query.Last().Value;
				else
					c = interval.First().Value;
				
				if(c != null)
					newPopulation.Add(new BitArray(c));				
			}
			
			return newPopulation;
		}
		
		public IList<BitArray> Mutation (IList<BitArray> population, double probability)
		{
			int populationCount = population.Count();
			// fill the resulting new population
			IList<BitArray> newPopulation = new List<BitArray>(populationCount);
			
			foreach(BitArray chromosome in population)
			{
				BitArray newChromosome = new BitArray(chromosome);
				double z = rnd.NextDouble();
				if(z < probability)
				{
					int index = rnd.Next(chromosome.Length);
					newChromosome[index]=!newChromosome[index];
				}
				newPopulation.Add(newChromosome);
			}
			
			return newPopulation;
		}

		public IList<BitArray> Crossover (IList<BitArray> population, IList<int> crossOverIndex)
		{
			int populationCount = population.Count();
			
			// fill the resulting new population
			IList<BitArray> newPopulation = new List<BitArray>(populationCount);
			while(newPopulation.Count < populationCount)
			{
				BitArray father = population[rnd.Next(population.Count())];
				
				int geneCount = father.Length;
				
				int index = rnd.Next(geneCount);
				
				if(crossOverIndex.Any( x => x == index))
				{
					population.Remove(father);
				
					BitArray mother = population[rnd.Next(population.Count())];
					population.Remove(mother);
					
					BitArray son = new BitArray(geneCount);
					BitArray daugther = new BitArray(geneCount);
				
					for(int i=0; i<geneCount; i++)
					{
						if(i < index)
						{
							son[i]=father[i];
							daugther[i]=mother[i];
						}
						else
						{
							son[i]=mother[i];
							daugther[i]=father[i];
						}
					}
					
					newPopulation.Add(son);
					newPopulation.Add(daugther);
				}
			}
			
			
			return newPopulation;
		}

		public IList<BitArray> Run (IList<BitArray> population, Func<int, double, bool> terminationCondition)
		{
			int generation = 1;
			while(!terminationCondition(generation, this.topFitness))
			{
				population = Selection(population, this.fitness);
				population = Mutation(population, this.mutatonProbability);
				population = Crossover(population, this.crossOverIndex);
				
				generation++;
			}
			
			return 	
				(	from c in population
					orderby fitness(c) descending
					select c 
				).ToList();
		}
		#endregion
	}
}

