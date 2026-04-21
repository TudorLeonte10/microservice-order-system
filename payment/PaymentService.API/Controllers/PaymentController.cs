using Microsoft.AspNetCore.Mvc;
using PaymentService.Application.Payments.Dtos;
using PaymentService.Application.Payments.Services;

namespace PaymentService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly IGetPaymentByIdService _getPaymentService;
        public PaymentController(IPaymentService paymentService, IGetPaymentByIdService getPaymentService)
        {
            _paymentService = paymentService;
            _getPaymentService = getPaymentService;
        }

        [HttpGet("{paymentId}")]
        public async Task<IActionResult> GetPaymentById(Guid paymentId)
        {
            var payment = await _getPaymentService.GetPaymentByIdAsync(paymentId);
            return Ok(payment);
        }

        [HttpPost("pay")]
        public async Task<IActionResult> Pay([FromBody] PayRequest request)
        {
            var result = await _paymentService.ExecutePayment(request);

            if (result.Status == PaymentService.Domain.Enums.PaymentStatus.Failed)
                return StatusCode(StatusCodes.Status402PaymentRequired, result);

            return Ok(result);
        }
    }
}
