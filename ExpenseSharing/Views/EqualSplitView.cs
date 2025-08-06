using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseSharing
{
    public class EqualSplitView : SplitView
    {
        public EqualSplitView(Guid paidTo) : base(paidTo)
        {
        }
    }
}
