using DistributedLocking;

namespace DistributedLockingTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestLockExpiry()
        {
            var lockManager = LockManager.GetInstance();
            var lockName = "TestLock";
            var nodeId = Guid.NewGuid();
            var lockDuration = 1000;
            lockManager.AcquireLock(lockName, nodeId, lockDuration);
            Thread.Sleep(100000);
        }
    }
}