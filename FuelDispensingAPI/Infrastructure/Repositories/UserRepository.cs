using System.Data;
using Dapper;
using FuelDispensingAPI.Domain;
using Web.API.Test.Infrastructure;

namespace FuelDispensingAPI.Infrastructure.Repositories
{
    public class DispensingRepository : IDispensingRepository
    {
        private readonly DapperContext _context;

        public DispensingRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<int> AddRecordAsync(DispensingRecordDto record)
        {
            int rowEffected = 0;
            var query = @"INSERT INTO tblDispensingRecoard 
                      (DispenserNo, QuantityFilled, VehicleNumber, PaymentMode, PaymentProofPath, CreatedDate)
                      VALUES (@DispenserNo, @QuantityFilled, @VehicleNumber, @PaymentMode, @PaymentProofPath, @CreatedDate)";
            using var connection = _context.CreateConnection();
            rowEffected= await connection.ExecuteAsync(query, record);
            return rowEffected;
        }

        public async Task<IEnumerable<DispensingRecord>> GetRecordsAsync(DispensingFilterDto filter)
        {
            using var connection = _context.CreateConnection();

            return await connection.QueryAsync<DispensingRecord>(
                "GetDispensingRecords",
                new
                {
                    DispenserNo = (filter.DispenserNo <= 0) ? null : filter.DispenserNo,
                    PaymentMode = (filter.PaymentMode <= 0) ? null : filter.PaymentMode,
                    StartDate = (filter.StartDate ==null) ? null : filter.StartDate,
                    EndDate = (filter.EndDate == null) ? null : filter.EndDate
                },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<string> GetPaymentProofByIdAsync(int DispenserId)
        {
            using var connection = _context.CreateConnection();

            var query = "SELECT paymentProofPath FROM tblDispensingRecoard WHERE Id = @Id";

            return await connection.QueryFirstOrDefaultAsync<string>(query, new { Id = DispenserId });
        }


    }

}
