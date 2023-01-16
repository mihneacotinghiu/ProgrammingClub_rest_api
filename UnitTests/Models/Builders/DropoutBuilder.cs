using ProgrammingClub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Models.Builders
{
    public class DropoutBuilder : BuilderBase<Dropout>
    {
        public DropoutBuilder()
        {
            ObjectToBuild = new Dropout
            {
                IDDropout = Guid.NewGuid(),
                IDEvent = Guid.NewGuid(),
                DropoutRate = 1
            };
        }
    }
}
