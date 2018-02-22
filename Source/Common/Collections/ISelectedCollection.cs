using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Collections
{
    public interface ISelectedCollection : ICollection
    {
        ISelectedCollectionItem Current { get; set; }
    }
}
