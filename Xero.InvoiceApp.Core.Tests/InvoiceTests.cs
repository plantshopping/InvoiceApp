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
            // Given
            var invoiceLines = new List<InvoiceLine>
            {
                new InvoiceLine
                {
                    Id = 1,
                    Cost = 10.21m,
                    Quantity = 4,
                    Description = "Banana"
                }
            };

            var invoice = new Invoice();

            // When
            invoice.AddInvoiceLines(invoiceLines);

            // Then
            Assert.Equal(40.84m, invoice.Total);
        }

        [Fact]
        public void Add_MultipleInvoiceLines_ShouldReturnCorrectTotal()
        {
            // Given
            var invoiceLines = new List<InvoiceLine>
            {
                new InvoiceLine
                {
                    Id = 1,
                    Cost = 10.21m,
                    Quantity = 4,
                    Description = "Banana"
                },
                new InvoiceLine
                {
                    Id = 2,
                    Cost = 5.21m,
                    Quantity = 1,
                    Description = "Orange"
                },
                new InvoiceLine
                {
                    Id = 3,
                    Cost = 5.21m,
                    Quantity = 5,
                    Description = "Pineapple"
                }
            };

            var invoice = new Invoice
            {
                LineItems = invoiceLines
            };

            // When
            invoice.AddInvoiceLines(invoiceLines);

            // Then
            Assert.Equal(72.1m, invoice.Total);
        }

        [Fact]
        public void Remove_InvoiceLines_ShouldReturnCorrectTotal()
        {
            // Given
            var invoiceLines = new List<InvoiceLine>
            {
                new InvoiceLine
                {
                    Id = 1,
                    Cost = 10.21m,
                    Quantity = 4,
                    Description = "Banana"
                },
                new InvoiceLine
                {
                    Id = 2,
                    Cost = 5.21m,
                    Quantity = 1,
                    Description = "Orange"
                },
                new InvoiceLine
                {
                    Id = 3,
                    Cost = 5.21m,
                    Quantity = 5,
                    Description = "Pineapple"
                }
            };

            var invoice = new Invoice
            {
                LineItems = invoiceLines
            };

            // When
            invoice.RemoveInvoiceLines(new List<int> {1});

            // Then
            Assert.Equal(43.96m, invoice.Total);
        }

        [Fact]
        public void Append_InvoiceWithMultipleInvoiceLines_ShouldReturnCorrectTotal()
        {
            // Given
            var invoice1Lines = new List<InvoiceLine>
            {
                new InvoiceLine
                {
                    Id = 1,
                    Cost = 10.33m,
                    Quantity = 4,
                    Description = "Banana"
                }
            };

            var invoice1 = new Invoice
            {
                LineItems = invoice1Lines
            };

            var invoice2Lines = new List<InvoiceLine>
            {
                new InvoiceLine
                {
                    Id = 2,
                    Cost = 5.22m,
                    Quantity = 1,
                    Description = "Orange"
                },
                new InvoiceLine
                {
                    Id = 3,
                    Cost = 6.27m,
                    Quantity = 3,
                    Description = "Blueberries"
                }
            };

            var invoice2 = new Invoice
            {
                LineItems = invoice2Lines
            };

            // When
            invoice1.AppendInvoices(new List<Invoice> {invoice2});

            // Then
            Assert.Equal(65.35m, invoice1.Total);
        }

        [Fact]
        public void DeepClone_Invoice_ShouldHaveTheSameProperties()
        {
            // Given
            var invoiceLines = new List<InvoiceLine>
            {
                new InvoiceLine
                {
                    Id = 1,
                    Cost = 10.33m,
                    Quantity = 4,
                    Description = "Banana"
                }
            };

            var invoice1 = new Invoice
            {
                Number = 1,
                Date = DateTime.Now,
                LineItems = invoiceLines
            };

            // When
            var invoiceClone = invoice1.DeepClone();

            // Then
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
            // Given
            var dateNow = DateTime.Now;

            var invoice1Lines = new List<InvoiceLine>
            {
                new InvoiceLine
                {
                    Id = 1,
                    Cost = 10.33m,
                    Quantity = 4,
                    Description = "Banana"
                }
            };

            var invoice1 = new Invoice
            {
                Number = 1,
                Date = dateNow,
                LineItems = invoice1Lines
            };

            // When
            var invoiceClone = invoice1.DeepClone();
            invoiceClone.Number = 2;
            invoiceClone.Date = new DateTime(2020, 01, 01);
            invoiceClone.LineItems = new List<InvoiceLine>();

            // Then
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
            // Given
            var date = new DateTime(2020, 12, 10);

            var invoice = new Invoice
            {
                Date = date,
                Number = 1000,
                LineItems = new List<InvoiceLine>
                {
                    new InvoiceLine
                    {
                        Id = 1,
                        Cost = 6.99m,
                        Quantity = 1,
                        Description = "Apple"
                    }
                }
            };

            // When
            var invoiceString = invoice.ToString();

            // Then
            Assert.Equal(
                $"Invoice Number: {invoice.Number}, InvoiceDate: 10/12/2020, LineItemCount: {invoice.LineItems.Count}",
                invoiceString);
        }
    }
}