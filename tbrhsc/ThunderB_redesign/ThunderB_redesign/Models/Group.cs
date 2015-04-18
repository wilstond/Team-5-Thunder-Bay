using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//----Helper class to group pages into groups by menu id----------

namespace ThunderB_redesign.Models
{
    public class Group<T, K>
    {
        public K Key;
        public IEnumerable<T> Values;
    }
}