using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dependous;
using FullStackDevExercise.DataAccess;

namespace FullStackDevExercise.Contracts
{
  public interface IIdentityValueGenerator : ISingletonDependency
  {
    public Task<long> WithContext(DolittleContext context, Func<DolittleContext, long> accessor);
  }
}