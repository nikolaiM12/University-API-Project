using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml.FormulaParsing.Excel.Operators;
using OrganizationAPI.API.Configurations;
using OrganizationAPI.Domain.Abstractions.DTOs.Account;
using OrganizationAPI.Domain.Abstractions.DTOs.Authentication;
using OrganizationAPI.Domain.Abstractions.Enums;
using OrganizationAPI.Domain.Abstractions.Services;
using System.Security.Principal;

namespace OrganizationAPI.API.Controllers
{
    [Route("accounts")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;
        private readonly IMapper mapper;
        public AccountController(IAccountService accountService, IMapper mapper)
        {
            this.accountService = accountService;
            this.mapper = mapper;
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<AccountDto>>> GetAll()
        {
            try
            {
                var accounts = await accountService.GetAll();
                return Ok(accounts);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error: {e.Message}");
            }
        }

        [HttpGet("id")]
        public async Task<ActionResult<AccountDto>> GetById(Guid id)
        {
            try
            {
                var account = await accountService.GetById(id);
                return Ok(account);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error: {e.Message}");
            }
        }

        [HttpPost("create")]
        public async Task<ActionResult<AccountDto>> Add(RegisterDto register)
        {
            try
            {
                AccountDto account = mapper.Map<AccountDto>(register);
                await accountService.Add(new List<AccountDto>
                {
                    account
                });

                return Ok(register);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error: {e.Message}");
            }
        }

        [HttpPut("update")]
        public async Task<ActionResult<AccountDto>> Update(AccountDto account)
        {
            try
            {
                await accountService.Update(account);
                return Ok(account);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error: {e.Message}");
            }
        }

        [HttpGet("accountByEmail")]
        //[AuthorizeRoles(Role.Admin)]
        public async Task<ActionResult<AccountDto>> GetAccountByEmail(string email)
        {
            try
            {
                var account = await accountService.GetAccountByEmail(email);

                if (account != null)
                {
                    return Ok(account);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error: {e.Message}");
            }
        }

        [HttpDelete("{id}")]
        [AuthorizeRoles(Role.Admin)]
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                await accountService.Delete(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error: {e.Message}");
            }
        }
    }
}
