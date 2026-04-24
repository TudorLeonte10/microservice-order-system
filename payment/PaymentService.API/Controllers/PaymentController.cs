using Microsoft.AspNetCore.Mvc;
using PaymentService.Application.Abstractions;
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
        private readonly IStripeWebhookService _stripeWebhookService;
        private readonly ILogger<PaymentController> _logger;
        public PaymentController(
            IPaymentService paymentService,
            IGetPaymentByIdService getPaymentService,
            IStripeWebhookService stripeWebhookService,
            ILogger<PaymentController> logger)
        {
            _paymentService = paymentService;
            _getPaymentService = getPaymentService;
            _stripeWebhookService = stripeWebhookService;
            _logger = logger;
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

        [HttpPost("webhook")]
        public async Task<IActionResult> StripeWebhook()
        {
            try
            {
                var payload = await new StreamReader(Request.Body).ReadToEndAsync();
                var signature = Request.Headers["Stripe-Signature"];

                await _stripeWebhookService.HandleWebhookAsync(payload, signature);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing Stripe webhook");
                return StatusCode(500, ex.Message);
            }
        }   
    }
}
