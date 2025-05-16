namespace Anesis.Shared.Constants
{
    public enum InvoiceStatus
    {
        Pending = 0,
        //Confirmed = 10,
        Dispute = 20,
        //Returned = 30,
        Approved = 50,
        //PartialPaid = 60,
        Paid = 70,
        //Deleted = 100,
        Voided = 110
    }

    public enum InvoiceCategory
    {
        VendorInvoices = 1
    }


    public enum PaymentMethod
    {
        ACH = 1,
        CreditCard = 2,
        Check = 4,
        //Cash = 8,
        //Deposit = 16,
        DirectDeposit = 32,
        TBD = 64,
    }

    public enum InvoiceFileType
    {
        None,
        Paper,
        UploadInvoice
    }
}
