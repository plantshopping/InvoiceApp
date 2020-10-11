using InvoiceProject;
using Xunit;

namespace Xero.InvoiceApp.Core.Tests
{
    public class InvoiceLineTests
    {
        private readonly InvoiceLine _invoiceLine;

        private const int InvoiceLineId = 1;
        private const decimal InvoiceLineCost = 6.99m;
        private const string InvoiceLineDescription = "Apple";

        public InvoiceLineTests()
        {
            _invoiceLine = new InvoiceLine
            {
                Id = InvoiceLineId,
                Cost = InvoiceLineCost,
                Description = InvoiceLineDescription
            };
        }

        [Fact]
        public void InvoiceLine_SingleQuantity_ShouldReturnCorrectTotal()
        {
            _invoiceLine.Quantity = 1;

            AssertInvoiceLine(_invoiceLine, 6.99m);
        }

        [Fact]
        public void InvoiceLine_MultipleQuantity_ShouldReturnCorrectTotal()
        {
            _invoiceLine.Quantity = 3;

            AssertInvoiceLine(_invoiceLine, 20.97m);
        }
        
        void AssertInvoiceLine(InvoiceLine invoiceLine, decimal expectedInvoiceLineTotalCost)
        {
            Assert.Equal(InvoiceLineId, invoiceLine.Id);
            Assert.Equal(InvoiceLineCost, invoiceLine.Cost);
            Assert.Equal(InvoiceLineDescription, invoiceLine.Description);
            Assert.Equal(expectedInvoiceLineTotalCost, invoiceLine.TotalCost);
        }
    }
}