using DataIntensive.Api.Application.Payments.CreatePayment;
using DataIntensive.Api.Application.Payments.PayInvoice;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DataIntensive.Api.Controllers
{
    [Route("api/payments")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PaymentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> PayInvoice([FromBody] PayInvoiceDto payload, CancellationToken cancellationToken)
        {
            var cmd = new PayInvoiceCommand(payload.UserId,
                payload.CardNumber,
                payload.CardCvv,
                payload.CardExpirationDate,
                payload.CardOwner,
                payload.CardOwnerDocument,
                payload.PaymentValue,
                payload.Items);

            await _mediator.Send(cmd, cancellationToken);

            return Ok();
        }
    }
}