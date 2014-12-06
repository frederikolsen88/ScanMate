ScanMate
========

A simple .NET program for either splitting a PDF into a series of one-page documents, or reversing its page order.

ScanMate came about to compensate for the drawbacks of a network scanner my company acquired recently. While fast and robust, the scanner in question features an amazingly slow UI. Coupled with the fact that the machine cannot scan a series of one-page documents in a row to separate PDF files, this makes can make scanning invoices and the like very annoying. 

Furthermore, the scanner is fed documents in reverse, meaning that scanning multi-page documents is also ridiculously unwieldy.

ScanMate attempts to circumvent these problems by allowing the user to drag and drop a single PDF document into the application. Using the iTextSharp library, the PDF is then either split up into a series of single-page documents, or a copy with reversed page order is written. The user may also opt to delete the source file.

I am releasing this first skeletal version of ScanMate under the GNU GPL license, as I can easily imagine that other office environments with entry-level network scanners could use something like this to make the daily work a bit more manageable.
