﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public partial class Club
    {
        public override string ToString() => Name;

        public ICollection<Group> NotDeletedGroups => Groups.ToList().FindAll(x => !x.IsDeleted);
    }
}
