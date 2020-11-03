
namespace IntegrationApiSynchroniser.Infrastructure.Enums
{
    public enum SignInresult
    {
        Success = 0,
        Locked = 1,
        Expire = 2,
        Failure = 3,
        NoAccess = 4
    }
    public enum StakeholderIDs
    {
        ARD_ID = 1,
        NPA_ID = 2,
        Passport_ID = 3,
        DAB_ID = 4,
        CommercialBankAndOtherBanks_ID = 5,
        ACBRIPSystem_ID = 6,
    }
}