using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Jira
{
    internal class TaskActivity
    {
        private readonly Task _task;
        private string comment;
        private DateTime createdAt;
        private long createdBy;
        private bool isActive;
        private TaskStatus status;
    }
}
