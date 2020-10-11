using InvoiceProject;
using Xunit;

namespace Xero.InvoiceApp.Core.Tests
{
    public class InvoiceLineTests
    {
        private InvoiceLine invoiceLine;

        private readonly int invoiceLineId = 1;
        private readonly decimal invoiceLineCost = 6.99m;
        private readonly string invoiceLinedescription = "Apple";

        public InvoiceLineTests()
        {
            invoiceLine = new InvoiceLine()
            {
                InvoiceLineId = invoiceLineId,
                Cost = invoiceLineCost,
                Description = invoiceLinedescription
            };
        }

        [Fact]
        public void GivenInvoiceLine_WithSingleQuantity_ShouldReturnCorrectTotal()
        {
            invoiceLine.Quantity = 1;

            Assert.Equal(6.99m, invoiceLine.TotalCost);
        }

        [Fact]
        public void GivenInvoiceLine_WithMultipleQuantity_ShouldReturnCorrectTotal()
        {
            invoiceLine.Quantity = 3;

            Assert.Equal(20.97m, invoiceLine.TotalCost);
        }
    }
}
