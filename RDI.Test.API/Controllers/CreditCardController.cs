using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RDI.Test.API.Models;

namespace RDI.Test.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CreditCardController : ControllerBase
    {
        private TestAPIContext _dbContext;

        public CreditCardController()
        {
            _dbContext = new TestAPIContext();
        }

        [HttpPost("Add")]
        public IActionResult Add([FromBody] CreditCardInfo cardInfo)
        {
            try
            {
                if (cardInfo.CardNumber > 9999999999999999)
                    return BadRequest("Card number should have maximum of 16 characters");

                if (cardInfo.CVV > 99999)
                    return BadRequest("CVV should have maximum of 5 characters");

                IssuedToken issuedToken = new IssuedToken() { RegistrationDate = DateTime.UtcNow };
                issuedToken.Token = string.Format("{0}{1:yyyy}{1:MM}{1:dd}{1:HH}{1:mm}", cardInfo.CardNumber, issuedToken.RegistrationDate);
                issuedToken.Token = Transformations.Rotate(Transformations.AbsoluteDifference(issuedToken.Token, 5), cardInfo.CVV);

                _dbContext.IssuedTokens.Add(issuedToken);
                _dbContext.SaveChanges();

                return new JsonResult(new { registration_date = issuedToken.RegistrationDate, token = issuedToken.Token });
            }
            catch (Exception ex)
            {
                /* log ex message */
                return BadRequest("Error processing credit card.");
            }
        }

        [HttpPost("ValidateToken")]
        public IActionResult ValidateToken([FromBody] TokenValidationInfo tokenInfo)
        {
            bool validated = true;
            
            try
            {
                if (string.IsNullOrWhiteSpace(tokenInfo.Token))
                    return BadRequest("Token is empty.");

                if (tokenInfo.CVV > 99999)
                    return BadRequest("CVV should have maximum of 5 characters");

                if (DateTime.UtcNow.Subtract(tokenInfo.RegistrationDate).TotalMinutes > 15)
                    return new JsonResult(new { validated = false });

                IssuedToken issuedToken = _dbContext.IssuedTokens.Where(w => w.RegistrationDate == tokenInfo.RegistrationDate && w.Token == tokenInfo.Token).FirstOrDefault();

                if (issuedToken == null)
                    return new JsonResult(new { validated = false });

                long cardNumber = issuedToken.CardNumber;

                return new JsonResult(new { validated = true });
            }
            catch (Exception ex)
            {
                /* log ex message */
                return BadRequest("Error validating token.");
            }
        }

    }

    public class CreditCardInfo
    {
        public long CardNumber { get; set; }
        public int CVV { get; set; }
    }

    public class TokenValidationInfo
    {
        public string Token { get; set; }
        public DateTime RegistrationDate { get; set; }
        public int CVV { get; set; }
    }
}