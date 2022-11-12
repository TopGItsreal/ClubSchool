using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public partial class Teacher
    {
        public override string ToString()
        {
            return $"{LastName} {FirstName[0]}." + (string.IsNullOrEmpty(Patronymic) ? "" : $"{Patronymic[0]}.");
        }
    }
}
