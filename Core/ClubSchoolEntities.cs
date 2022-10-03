using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public partial class ClubSchoolEntities
    {
        private ClubSchoolEntities clubSchoolEntities;

        public ClubSchoolEntities GetContext()
        {
            if (clubSchoolEntities == null)
                clubSchoolEntities = new ClubSchoolEntities();
            return clubSchoolEntities;
        }
    }
}
