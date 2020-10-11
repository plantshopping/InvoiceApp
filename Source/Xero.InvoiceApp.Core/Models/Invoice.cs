using Force.DeepCloner;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InvoiceProject
{
    public class Invoice
    {
        public int Number { get; set; }
        public DateTime Date { get; set; }
        public List<InvoiceLine> LineItems { get; set; } = new List<InvoiceLine>();
        public decimal Total => LineItems.Sum(l => l.TotalCost);

        public void AddInvoiceLines(IEnumerable<InvoiceLine> invoiceLines) => LineItems.AddRange(invoiceLines);

        public void RemoveInvoiceLines(IEnumerable<int> ids) => LineItems.RemoveAll(l => ids.Contains(l.Id));

        /// <summary>
        /// AppendInvoices appends the items from the sourceInvoice to the current invoice
        /// </summary>
        /// <param name="sourceInvoices">Invoice to merge from</param>
        public void AppendInvoices(IEnumerable<Invoice> sourceInvoices) =>
            LineItems.AddRange(sourceInvoices.SelectMany(i => i.LineItems));

        /// <summary>
        /// Creates a deep clone of the current invoice (all fields and properties)
        /// </summary>
        public Invoice DeepClone() => DeepClonerExtensions.DeepClone(this);

        /// <summary>
        /// Outputs string containing the following (replace [] with actual values):
        /// Invoice Number: [InvoiceNumber], InvoiceDate: [dd/MM/yyyy], LineItemCount: [Number of items in LineItems]
        /// </summary>
        public override string ToString() => string.Format(Strings.InvoiceTemplate, Number.ToString(),
            Date.ToString("dd/MM/yyyy"), LineItems.Count);
    }
}