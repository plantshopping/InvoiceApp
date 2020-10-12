using Force.DeepCloner;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Xero.InvoiceApp.Core
{
    public class Invoice
    {
        public int Number { get; set; }
        public DateTime Date { get; set; }
        public List<InvoiceLine> LineItems { get; set; } = new List<InvoiceLine>();
        public decimal Total => LineItems.Sum(l => l.TotalCost);

        public void AddLineItems(IEnumerable<InvoiceLine> invoiceLines) => LineItems.AddRange(invoiceLines ?? new List<InvoiceLine>());

        public void RemoveLineItems(IEnumerable<int> ids) => LineItems.RemoveAll(l => ids?.Contains(l.Id) ?? false);

        /// <summary>
        /// AppendInvoices appends the items from the sourceInvoice to the current invoice
        /// </summary>
        /// <param name="sourceInvoices">Invoice to merge from</param>
        public void AppendInvoices(IEnumerable<Invoice> sourceInvoices) =>
            LineItems.AddRange(sourceInvoices == null ? new List<InvoiceLine>() : sourceInvoices.SelectMany(i => i.LineItems));
        
        public Invoice DeepClone() => DeepClonerExtensions.DeepClone(this);
        
        public override string ToString() => string.Format(Strings.InvoiceTemplate, Number.ToString(),
            Date.ToString("dd/MM/yyyy"), LineItems.Count);
    }
}