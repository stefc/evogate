using System;
using System.Collections.Generic;
using System.Collections;

namespace stefc.evolib.contract
{
	public interface IEvolution
	{
		/// <summary>
		/// Genetic selection for a given population based on a fitness function.
		/// </summary>
		/// <param name='population'>Current Population</param>
		/// <param name='fitness'>Fitness function</param>
		IList<BitArray> Selection(IList<BitArray> population, Func<BitArray,double> fitness);
		
		/// <summary>
		/// Genetic Mutation operator on a population.
		/// </summary>
		/// <param name='population'>Population</param>
		/// <param name='probability'>Probability</param>
		IList<BitArray> Mutation(IList<BitArray> population, double probability);
		
		/// <summary>
		/// Genetic Crossover operator on a population.
		/// </summary>
		/// <param name='population'>Population</param>
		/// <param name='crossOverIndex'>List of indexes where crossover is possible</param>
		IList<BitArray> Crossover(IList<BitArray> population, IList<int> crossOverIndex);
	}
}

