using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithAPS_NETUdemy.Data.Converter
{
   public interface IParser<O, D>
    {
        O Parse(D origin);
        List<D> ParseList(List<O> origin);
    }
}
