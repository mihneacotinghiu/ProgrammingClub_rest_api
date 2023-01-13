using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Models
{
    public abstract class BuilderBase<T>
    {
        protected T ObjectToBuild;

        public T Build()
        {
            return ObjectToBuild;
        }

        public BuilderBase<T> With(Action<T> setter)
        {
            setter(ObjectToBuild);

            return this;
        }
    }
}
