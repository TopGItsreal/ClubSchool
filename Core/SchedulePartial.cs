using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public partial class Schedule
    {
        public string Attendance => Journals.Count() == 0 ? "Занятия не было" : $"{(Journals.Count(x => x.IsVisited) * 100) / Journals.Count()}%"; 
    }
}
