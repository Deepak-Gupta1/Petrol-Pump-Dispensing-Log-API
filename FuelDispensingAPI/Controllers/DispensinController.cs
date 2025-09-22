using System.Text.RegularExpressions;
using FuelDispensingAPI.Domain;
using FuelDispensingAPI.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class DispensingController : ControllerBase
{
    private readonly IDispensingRepository _repository;

    public DispensingController(IDispensingRepository repository)
    {
        _repository = repository;
    }

    [HttpPost("SaveRecord")]
    public async Task<IActionResult> SaveRecord(DispensingRecordDto record)
    {

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (record.PaymentProof != null)
        {
            // Define the folder path
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "PaymentProofs");

            // Ensure the directory exists
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // Generate a unique file name
            var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(record.PaymentProof.FileName)}";
            var filePath = Path.Combine(folderPath, fileName);

            // Save the file
            using var stream = new FileStream(filePath, FileMode.Create);
            await record.PaymentProof.CopyToAsync(stream);

            // Store the relative path or file name
            record.PaymentProofPath = fileName;
        }

            record.CreatedDate = DateTime.Now;
        return await _repository.AddRecordAsync(record) > 0
       ? Ok(new ResponseModel<bool>(true, true, "Record added successfully."))
       : Ok(new ResponseModel<bool>(false, false, "Failed to add record."));

    }

    [HttpGet("GetDispensingRecords")]
    public async Task<IActionResult> GetRecords([FromQuery] DispensingFilterDto filter)
    {
        var records = await _repository.GetRecordsAsync(filter);
        if (!records.Any()) {
            return NotFound(new ResponseModel<bool>(false,false,"No Dispensing Records found !"));
        }
        return Ok(new ResponseModel<IEnumerable<DispensingRecord>>(true, records, "Successfully Fetch records"));
    }

    [AllowAnonymous]
    [HttpGet("GetPaymentProofPdf/{DispenserId}")]
    public async Task<IActionResult> GetPaymentProofPdf(int DispenserId)
    {
        var paymentProofFileName = await _repository.GetPaymentProofByIdAsync(DispenserId);

        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "PaymentProofs", paymentProofFileName);

        if (!System.IO.File.Exists(filePath))
            return NotFound("File not found");

        var bytes = System.IO.File.ReadAllBytes(filePath);
        Response.Headers.Add("Content-Disposition", "inline; filename=" + filePath);
        return File(bytes, "application/pdf");
    }
}
