using Microsoft.AspNetCore.Mvc;
using SMS_Project.Models;
using Twilio.AspNet.Common;
using Twilio.AspNet.Core;
using Twilio.Clients;
using Twilio.Rest.Api.V2010.Account;
using Twilio.TwiML;
using Twilio.Types;

namespace SMS_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SmsController : TwilioController
    {
        private readonly ITwilioRestClient _client;

        public SmsController(ITwilioRestClient client)
        {
            _client = client;
        }

        [HttpGet]
        public IActionResult SendSms(SmsMessage model)
        {
            var message = MessageResource.Create(
                to: new PhoneNumber(model.To),
                from: new PhoneNumber(model.From),
                body: model.Message,
                client: _client);

            return Ok("Success");
        }

        [HttpPost]
        public TwiMLResult ReceiveSms([FromForm] SmsRequest incomingMessage)
        {
            var messagingResponse = new MessagingResponse();
            messagingResponse.Message("Thank you. Your message was received.");

            return TwiML(messagingResponse);
        }
    }
}
