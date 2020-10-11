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
        public void Add_SingleInvoiceLine_ShouldReturnCorrectTotal()
        {
            var invoice = new Invoice();

            invoice.AddInvoiceLine(new InvoiceLine()
            {
                Id = 1,
                Cost = 10.21m,
                Quantity = 4,
                Description = "Banana"
            });

            Assert.Equal(40.84m, invoice.Total);
        }

        [Fact]
        public void Add_MultipleInvoiceLines_ShouldReturnCorrectTotal()
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
        public void Remov_InvoiceLines_ShouldReturnCorrectTotal()
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
        public void Append_InvoiceWithMultipleInvoiceLines_ShouldReturnCorrectTotal()
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

            invoice1.AppendInvoices(new List<Invoice> { invoice2 });

            Assert.Equal(65.35m, invoice1.Total);
        }

        [Fact]
        public void DeepClone_Invoice_ShouldHaveTheSameProperties()
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

            var invoiceClone = invoice1.DeepClone();

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
        public void DeepClone_InvoiceWithChangeInProperties_ShouldNotChangeOriginalProperties()
        {
            var dateNow = DateTime.Now;

            var invoice1 = new Invoice
            {
                Number = 1,
                Date = dateNow
            };
            invoice1.AddInvoiceLine(new InvoiceLine()
            {
                Id = 1,
                Cost = 10.33m,
                Quantity = 4,
                Description = "Banana"
            });

            var invoiceClone = invoice1.DeepClone();
            invoiceClone.Number = 2;
            invoiceClone.Date = new DateTime(2020, 01, 01);
            invoiceClone.LineItems = new List<InvoiceLine>();

            Assert.Equal(2, invoiceClone.Number);
            Assert.Equal(new DateTime(2020, 01, 01), invoiceClone.Date);
            Assert.Equal(0, invoiceClone.Total);
            Assert.Empty(invoiceClone.LineItems);

            var invoiceFirstLineItem = invoice1.LineItems.First();

            Assert.Equal(1, invoice1.Number);
            Assert.Equal(dateNow, invoice1.Date);
            Assert.Single(invoice1.LineItems);
            Assert.Equal(1, invoiceFirstLineItem.Id);
            Assert.Equal(10.33m, invoiceFirstLineItem.Cost);
            Assert.Equal(4, invoiceFirstLineItem.Quantity);
            Assert.Equal("Banana", invoiceFirstLineItem.Description);
        }

        [Fact]
        public void ToString_Invoice_ShouldReturnCorrectString()
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
