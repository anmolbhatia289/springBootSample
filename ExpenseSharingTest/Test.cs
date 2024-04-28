using ExpenseSharing;
using ExpenseSharing.Managers;
using System.Reflection.Metadata;

namespace ExpenseSharingTest
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void TestCreateExpense()
        {
            var splits = new List<SplitView>();
            var user1 = Guid.NewGuid();
            var user2 = Guid.NewGuid();
            var splitToUser2 = new EqualSplitView(user2);
            var splitToUser1 = new EqualSplitView(user1);
            splits.Add(splitToUser2);
            splits.Add(splitToUser1);
            var expenseView = new ExpenseView("electricy", "May month", ExpenseType.EQUAL, splits, 1000, user1);
            var expenseManager = ExpenseManager.getInstance();
            expenseManager.addExpense(expenseView);
            float balance = expenseManager.getBalance(user1, user2);
            Assert.AreEqual(balance, 500);
        }
    }
}