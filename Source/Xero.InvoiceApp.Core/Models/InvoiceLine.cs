namespace InvoiceProject
{
    public class InvoiceLine
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal Cost { get; set; }
        public decimal TotalCost => Quantity * Cost;
    }
}