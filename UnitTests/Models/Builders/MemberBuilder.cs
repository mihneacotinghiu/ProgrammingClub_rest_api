using ProgrammingClub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Models.Builders
{
    public class MemberBuilder: BuilderBase<Member>
    {
        public MemberBuilder()
        {
            ObjectToBuild = new Member
            {
                IdMember = Guid.NewGuid(),
                Name = "Name",
                Description = "Description",
                Title = "Title",
                Position = "Position",
                Resume = "Resume"
            };
        }
    }
}
