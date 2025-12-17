using FoodOrderingApplicationFigma.Services.Service_Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrderingApplicationFigma.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OtpController : ControllerBase
    {
        private readonly IOtpService _otpService;
        private readonly IEmailService _emailService;
        private readonly ISmsService _smsService;

        public OtpController(IOtpService otpService, IEmailService emailService, ISmsService smsService)
        {
            _otpService = otpService;
            _emailService = emailService;
            _smsService = smsService;
        }

        [HttpPost("send-otp-email")]
        public async Task<ActionResult> SendOtpEmail([FromQuery] string email)
        {
            try
            {
                var otp = _otpService.GenerateOtp();
                var token = _otpService.GenerateOtpToken(otp, email);

                await _emailService.SendEmailAsync(
                    email,
                    "Your OTP Code",
                    $"<h2>Your OTP is: <strong>{otp}</strong></h2><p>Valid for 5 minutes.</p>");

                return Ok(new
                {
                    token = token,
                    message = "OTP sent successfully to your email",
                    otp = otp
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost("send-otp-sms")]
        public async Task<ActionResult> SendOtpSms([FromQuery] string phone)
        {
            try
            {
                var otp = _otpService.GenerateOtp();
                var token = _otpService.GenerateOtpToken(otp, phone);

                await _smsService.SendOtpAsync(phone, otp);

                return Ok(new
                {
                    token = token,
                    message = "OTP sent successfully to your phone",
                    otp = otp
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost("verify-otp")]
        public ActionResult VerifyOtp([FromQuery] string identifier, [FromQuery] string otp, [FromQuery] string token)
        {
            try
            {
                var isValid = _otpService.ValidateOtpToken(token, otp, identifier);

                if (!isValid)
                {
                    return BadRequest(new { message = "Invalid or expired OTP" });
                }

                return Ok(new { message = "OTP verified successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
