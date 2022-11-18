using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public partial class Group
    {
        public ICollection<StudentGroup> NotDeletedStudentGroups => StudentGroups.ToList().FindAll(x => !x.IsDeleted);
    }
}
