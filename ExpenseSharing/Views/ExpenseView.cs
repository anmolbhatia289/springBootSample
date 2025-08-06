namespace ExpenseSharing
{
    public class ExpenseView
    {
        public string Name { get; set; }
        public string description { get; set; }
        public ExpenseType type { get; set; }
        public List<SplitView> splits { get; set; }
        public float amount { get; set; }
        public Guid paidBy { get; set; }

        public ExpenseView(string name, string description, ExpenseType types, List<SplitView> splits, float amount, Guid paidBy) 
        {
            this.Name = name;
            this.description = description;
            this.type = types;
            this.splits = splits;
            this.amount = amount;
            this.paidBy = paidBy;
        }
    }
}
