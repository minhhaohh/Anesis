namespace Anesis.ApiService.Domain.Constants
{
    public class BankKeywordSettings
    {
        public const string CreditCardProcessor = "IPS ACH";
        public const string BankCreditCardReference = "MERCHANT BNKCD    DE,TSYS/TRANSFIRST   BK,TSYS/TRANSFIRST   CR,GLOBAL PAYMENTS   GL";
        public const string AchCreditDescription = "PREAUTHORIZED ACH CREDIT";
        public const string BankDepositReference = "PAINCAREPHYSICIA  ME,OEWELLSFARGO      DE";
        public const string BankDepositDescription1 = "DEPOSIT,CHECK DEPOSIT PACKAGE,UNIVERSAL CREDIT";
    }
}
