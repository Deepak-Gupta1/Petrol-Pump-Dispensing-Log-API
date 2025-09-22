using FuelDispensingAPI.Domain;

namespace FuelDispensingAPI.Infrastructure.Repositories
{
    public interface IDispensingRepository
    {
        Task<int> AddRecordAsync(DispensingRecordDto record);
        Task<IEnumerable<DispensingRecord>> GetRecordsAsync(DispensingFilterDto filter);
        Task<string> GetPaymentProofByIdAsync(int DispenserId);
    }

}
