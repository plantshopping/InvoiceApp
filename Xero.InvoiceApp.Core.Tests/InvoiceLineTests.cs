using InvoiceProject;
using Xunit;

namespace Xero.InvoiceApp.Core.Tests
{
    public class InvoiceLineTests
    {
        private readonly InvoiceLine invoiceLine;

        private readonly int invoiceLineId = 1;
        private readonly decimal invoiceLineCost = 6.99m;
        private readonly string invoiceLineDescription = "Apple";

        public InvoiceLineTests()
        {
            invoiceLine = new InvoiceLine()
            {
                Id = invoiceLineId,
                Cost = invoiceLineCost,
                Description = invoiceLineDescription
            };
        }

        [Fact]
        public void InvoiceLine_SingleQuantity_ShouldReturnCorrectTotal()
        {
            invoiceLine.Quantity = 1;

            AssertInvoiceLine(invoiceLine, 6.99m);
        }

        [Fact]
        public void InvoiceLine_MultipleQuantity_ShouldReturnCorrectTotal()
        {
            invoiceLine.Quantity = 3;

            AssertInvoiceLine(invoiceLine, 20.97m);
        }

        void AssertInvoiceLine(InvoiceLine invoiceLine, decimal expectedInvoiceLineTotalCost)
        {
            Assert.Equal(invoiceLineId, invoiceLine.Id);
            Assert.Equal(invoiceLineCost, invoiceLine.Cost);
            Assert.Equal(invoiceLineDescription, invoiceLine.Description);
            Assert.Equal(expectedInvoiceLineTotalCost, invoiceLine.TotalCost);
        }
    }
}
