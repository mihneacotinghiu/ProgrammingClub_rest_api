using ProgrammingClub.Models;
using ProgrammingClub.Models.CreateModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Models.Builders
{
    public class CreateDropoutBuilder : BuilderBase<CreateDropout>
    {
        public CreateDropoutBuilder()
        {
            ObjectToBuild = new CreateDropout
            {
                IDDropout = Guid.NewGuid(),
                IDEvent = Guid.NewGuid(),
                DropoutRate = 1
            };
        }
    }
}
