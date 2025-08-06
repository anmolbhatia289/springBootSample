using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedLocking
{
    public class LockManager
    {
        private Dictionary<string, Lock> currentLocks;
        public static LockManager instance;
        private LockManager()
        {
            currentLocks = new Dictionary<string, Lock>();
        }

        public static LockManager GetInstance()
        {
            if (instance == null)
            {
                instance = new LockManager();
            }
            return instance;
        }

        public bool AcquireLock(string lockName, Guid nodeId, long lockDuration)
        {
            var lockObj = GetLock(lockName, lockDuration);
            lock (lockObj)
            {
                if (lockObj.IsAcquired)
                {
                    if (DateTime.UtcNow > lockObj.lockTime.AddMilliseconds(lockObj.lockDuration))
                    {
                        ReleaseLockInternal(lockName);
                    }
                    else
                    {
                        return false;
                    }
                }

                lockObj.IsAcquired = true;
                lockObj.ownerNode = nodeId;
                lockObj.lockTime = DateTime.UtcNow;
                var thread = new Thread(() => AcquireLockCallBack(lockName, lockDuration, nodeId));
                thread.Start();
                return true;
            }
        }

        public bool ReleaseLock(string lockName, Guid nodeId)
        {
            Lock lockObj = GetLock(lockName, 0);
            lock (lockObj)
            {
                if (lockObj.ownerNode == nodeId)
                {
                    return ReleaseLockInternal(lockName);
                }
                return false;
            }
        }

        private bool ReleaseLockInternal(string lockName)
        {
            Lock lockObj = GetLock(lockName, 0);
            lock (lockObj)
            {
                lockObj.IsAcquired = false;
                lockObj.lockTime = DateTime.MinValue;
                return true;
            }
        }

        private Lock GetLock(string lockName, long lockDuration)
        {
            if (currentLocks.ContainsKey(lockName))
            {
                return currentLocks[lockName];
            }
            else
            {
                Lock newLock = new Lock(lockName, lockDuration);
                currentLocks.Add(lockName, newLock);
                return newLock;
            }
        }

        private void AcquireLockCallBack(string lockName, long lockDuration, Guid nodeId)
        {
            Thread.Sleep(TimeSpan.FromMilliseconds(lockDuration));
            ReleaseLock(lockName, nodeId);
        }
    }
}
