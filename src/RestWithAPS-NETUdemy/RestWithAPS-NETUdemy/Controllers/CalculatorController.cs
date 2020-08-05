using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization;

namespace RestWithAPS_NETUdemy.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculatorController : ControllerBase
    {
        [HttpGet("{firstNumber}/{secondNumber}")]
        public IActionResult Sum(string firstNumber, string secondNumber)
        {
            if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
            {
                var sum = ConvertToDecimal(firstNumber) + ConvertToDecimal(secondNumber);
                return Ok(sum.ToString());
            }

            return BadRequest("Invalid input");
        }

        private decimal ConvertToDecimal(string number)
        {
            decimal decimalValue;

            if (Decimal.TryParse(number, out decimalValue)) return decimalValue;

            return 0;
        }

        private bool IsNumeric(string num)
        {
            double number;

            bool isNumber = double.TryParse(num, NumberStyles.Any, NumberFormatInfo.InvariantInfo, out number);

            return isNumber;
        }
    }
}
