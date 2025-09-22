using System.ComponentModel.DataAnnotations;

namespace FuelDispensingAPI.Domain
{
    public class DispensingRecordDto
    {

        public int Id { get; set; }
        public  int DispenserNo { get; set; }
        public decimal QuantityFilled { get; set; }

        //[Required(ErrorMessage = "VehicleNumber is required.")]
        public  string VehicleNumber { get; set; }
        public  int PaymentMode { get; set; }

        public IFormFile PaymentProof { get; set; }
        public string PaymentProofPath { get; set; } = String.Empty;

        public DateTime CreatedDate { get; set; }

    }
    public class DispensingRecord
    {
        public int Id { get; set; }
        public string DispenserNo { get; set; }
        public decimal QuantityFilled { get; set; }
        public string VehicleNumber { get; set; }
        public string PaymentMode { get; set; }
        public string paymentProofPath { get; set; } = String.Empty;
        public DateTime CreatedDate { get; set; }

    }

    public class DispensingFilterDto
    {
        public int? DispenserNo { get; set; }
        public int? PaymentMode { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
