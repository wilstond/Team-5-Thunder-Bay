using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThunderB_redesign.Models
{
    public class Group<T, K>
    {
        public K Key;
        public IEnumerable<T> Values;
    }
}