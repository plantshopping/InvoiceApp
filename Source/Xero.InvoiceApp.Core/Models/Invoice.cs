﻿using Force.DeepCloner;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InvoiceProject
{
    public class Invoice
    {
        public int InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public List<InvoiceLine> LineItems { get; set; } = new List<InvoiceLine>();
        public decimal Total => LineItems.Sum(l => l.TotalCost);

        public void AddInvoiceLine(InvoiceLine invoiceLine)
        {
            LineItems.Add(invoiceLine);
        }

        public void RemoveInvoiceLine(int id)
        {
            LineItems.RemoveAll(l => l.InvoiceLineId == id);
        }

        /// <summary>
        /// MergeInvoices appends the items from the sourceInvoice to the current invoice
        /// </summary>
        /// <param name="sourceInvoice">Invoice to merge from</param>
        public void MergeInvoices(Invoice sourceInvoice)
        {
            LineItems.AddRange(sourceInvoice.LineItems);
        }

        /// <summary>
        /// Creates a deep clone of the current invoice (all fields and properties)
        /// </summary>
        public Invoice Clone()
        {
            return this.DeepClone();
        }

        /// <summary>
        /// Outputs string containing the following (replace [] with actual values):
        /// Invoice Number: [InvoiceNumber], InvoiceDate: [dd/MM/yyyy], LineItemCount: [Number of items in LineItems]
        /// </summary>
        public override string ToString()
        {
            return string.Format(Strings.InvoiceTemplate, InvoiceNumber.ToString(), InvoiceDate.ToString("dd/MM/yyyy"), LineItems.Count);
        }
    }
}