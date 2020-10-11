using InvoiceProject;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Xero.InvoiceApp.Core.Tests
{
    public class InvoiceTests
    {
        [Fact]
        public void GivenInvoice_WhenAddingMultipleInvoiceLines_ShouldReturnCorrectTotal()
        {
            var invoice = new Invoice();

            invoice.AddInvoiceLine(new InvoiceLine()
            {
                Id = 1,
                Cost = 10.21m,
                Quantity = 4,
                Description = "Banana"
            });
            invoice.AddInvoiceLine(new InvoiceLine()
            {
                Id = 2,
                Cost = 5.21m,
                Quantity = 1,
                Description = "Orange"
            });
            invoice.AddInvoiceLine(new InvoiceLine()
            {
                Id = 3,
                Cost = 5.21m,
                Quantity = 5,
                Description = "Pineapple"
            });

            Assert.Equal(72.1m, invoice.Total);
        }

        [Fact]
        public void GivenInvoice_WhenRemovingInvoiceLines_ShouldReturnCorrectTotal()
        {
            var invoice = new Invoice();

            invoice.AddInvoiceLine(new InvoiceLine()
            {
                Id = 1,
                Cost = 5.21m,
                Quantity = 1,
                Description = "Orange"
            });
            invoice.AddInvoiceLine(new InvoiceLine()
            {
                Id = 2,
                Cost = 10.99m,
                Quantity = 4,
                Description = "Banana"
            });

            invoice.RemoveInvoiceLine(1);

            Assert.Equal(43.96m, invoice.Total);
        }

        [Fact]
        public void GivenInvoice_WhenAppendingInvoice_ShouldReturnCorrectTotal()
        {
            var invoice1 = new Invoice();

            invoice1.AddInvoiceLine(new InvoiceLine()
            {
                Id = 1,
                Cost = 10.33m,
                Quantity = 4,
                Description = "Banana"
            });

            var invoice2 = new Invoice();

            invoice2.AddInvoiceLine(new InvoiceLine()
            {
                Id = 2,
                Cost = 5.22m,
                Quantity = 1,
                Description = "Orange"
            });
            invoice2.AddInvoiceLine(new InvoiceLine()
            {
                Id = 3,
                Cost = 6.27m,
                Quantity = 3,
                Description = "Blueberries"
            });

            invoice1.AppendInvoice(invoice2);

            Assert.Equal(65.35m, invoice1.Total);
        }
    
        [Fact]
        public void GivenInvoice_WhenDeepClone_ShouldHaveTheSameProperties()
        {
            var invoice1 = new Invoice
            {
                Number = 1,
                Date = DateTime.Now
            };
            invoice1.AddInvoiceLine(new InvoiceLine()
            {
                Id = 1,
                Cost = 10.33m,
                Quantity = 4,
                Description = "Banana"
            });

            var invoiceClone = invoice1.Clone();

            Assert.Equal(invoiceClone.Number, invoice1.Number);
            Assert.Equal(invoiceClone.Date, invoice1.Date);
            Assert.Equal(invoiceClone.Total, invoice1.Total);
            Assert.Equal(invoiceClone.LineItems.Count, invoice1.LineItems.Count);

            var invoiceFirstLineItem = invoice1.LineItems.First();
            var invoiceCloneFirstLineItem = invoiceClone.LineItems.First();

            Assert.Equal(invoiceFirstLineItem.Id, invoiceCloneFirstLineItem.Id);
            Assert.Equal(invoiceFirstLineItem.Cost, invoiceCloneFirstLineItem.Cost);
            Assert.Equal(invoiceFirstLineItem.Quantity, invoiceCloneFirstLineItem.Quantity);
            Assert.Equal(invoiceFirstLineItem.Description, invoiceCloneFirstLineItem.Description);
        }

        [Fact]
        public void GivenInvoice_WhenFormattingToString_ShouldReturnCorrectString()
        {
            var date = new DateTime(2020, 12, 10);

            var invoice = new Invoice()
            {
                Date = date,
                Number = 1000,
                LineItems = new List<InvoiceLine>()
                {
                    new InvoiceLine()
                    {
                        Id = 1,
                        Cost = 6.99m,
                        Quantity = 1,
                        Description = "Apple"
                    }
                }
            };

            var invoiceString = invoice.ToString();

            Assert.Equal($"Invoice Number: {invoice.Number}, InvoiceDate: 10/12/2020, LineItemCount: {invoice.LineItems.Count}", invoiceString);
        }
    }
}
