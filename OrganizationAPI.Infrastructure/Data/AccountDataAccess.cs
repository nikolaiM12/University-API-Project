using AutoMapper;
using OrganizationAPI.Domain.Abstractions.Constants;
using OrganizationAPI.Domain.Abstractions.Database;
using OrganizationAPI.Domain.Abstractions.DTOs.Account;
using OrganizationAPI.Domain.Abstractions.DTOs.Authentication;
using OrganizationAPI.Domain.Abstractions.Enums;
using OrganizationAPI.Domain.Abstractions.Repositories;
using OrganizationAPI.Domain.Abstractions.Services;
using System.Data.SqlClient;

namespace OrganizationAPI.Infrastructure.Data
{
    public abstract class AccountDataAccess : IRepository<AccountDto, Guid>
    {
        private readonly IDbConnector connection;
        private readonly IPasswordService passwordService;
        private readonly IMapper mapper;
        public AccountDataAccess(IDbConnector connection, IPasswordService passwordService, IMapper mapper)
        {
            this.connection = connection;
            this.passwordService = passwordService;
            this.mapper = mapper;
        }

        public async Task<List<AccountDto>> GetAll()
        {
            try
            {
                List<AccountDto> accounts = new List<AccountDto>();

                using (SqlCommand cmd = new SqlCommand(GetAllQueriesConstants.GetAllAccountsQuery, connection.GetConnection()))
                {
                    await connection.OpenConnection();

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            AccountDto account = new AccountDto();
                            account.Id = reader.GetGuid(reader.GetOrdinal("Id"));
                            account.Email = reader.GetString(reader.GetOrdinal("Email"));
                            account.PasswordHash = reader.GetString(reader.GetOrdinal("PasswordHash"));
                            account.PasswordSalt = reader.GetString(reader.GetOrdinal("PasswordSalt"));
                            account.Role = (Role)reader.GetInt32(reader.GetOrdinal("Role"));
                            account.IsDeleted = reader.GetBoolean(reader.GetOrdinal("IsDeleted"));

                            accounts.Add(account);
                        }
                    }

                    await connection.CloseConnection();
                }

                return accounts;
            }
            catch (SqlException e)
            {
                throw new InvalidOperationException($"Error: {e.Message}. SQL Server error: {e.InnerException?.Message ?? "N/A"}");
            }
        }

        public async Task<AccountDto> GetAccountByEmail(string email)
        {
            try
            {
                AccountDto account = new AccountDto();

                using (SqlCommand cmd = new SqlCommand(GetByQueriesConstants.GetAccountsByEmail, connection.GetConnection()))
                {
                    await connection.OpenConnection();
                    cmd.Parameters.AddWithValue(@"Email", email);

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            account = new AccountDto();
                            account.Id = reader.GetGuid(reader.GetOrdinal("Id"));
                            account.Email = reader.GetString(reader.GetOrdinal("Email"));
                            account.PasswordHash = reader.GetString(reader.GetOrdinal("PasswordHash"));
                            account.PasswordSalt = reader.GetString(reader.GetOrdinal("PasswordSalt"));
                            account.Role = (Role)reader.GetInt32(reader.GetOrdinal("Role"));
                        }
                    }

                    await connection.CloseConnection();
                }

                return account;
            }
            catch (SqlException e)
            {
                throw new InvalidOperationException($"Error: {e.Message}. SQL Server error: {e.InnerException?.Message ?? "N/A"}");
            }
        }

        public async Task<AccountDto> GetById(Guid id)
        {
            try
            {
                AccountDto account = new AccountDto();

                using (SqlCommand cmd = new SqlCommand(GetByQueriesConstants.GetAccountById, connection.GetConnection()))
                {
                    await connection.OpenConnection();
                    cmd.Parameters.AddWithValue(@"AccountId", id);

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            account = new AccountDto();
                            account.Id = reader.GetGuid(reader.GetOrdinal("Id"));
                            account.Email = reader.GetString(reader.GetOrdinal("Email"));
                            account.PasswordHash = reader.GetString(reader.GetOrdinal("PasswordHash"));
                            account.PasswordSalt = reader.GetString(reader.GetOrdinal("PasswordSalt"));
                            account.Role = (Role)reader.GetInt32(reader.GetOrdinal("Role"));
                        }
                    }

                    await connection.CloseConnection();
                }

                return account;
            }
            catch (SqlException e)
            {
                throw new InvalidOperationException($"Error: {e.Message}. SQL Server error: {e.InnerException?.Message ?? "N/A"}");
            }
        }

        public async Task Update(AccountDto account)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand(UpdateQueriesConstants.UpdateAccount, connection.GetConnection()))
                {
                    await connection.OpenConnection();

                    cmd.Parameters.AddWithValue("@AccountId", account.Id);
                    cmd.Parameters.AddWithValue("@Email", account.Email);
                    cmd.Parameters.AddWithValue("@PasswordHash", account.PasswordHash);
                    cmd.Parameters.AddWithValue("@PasswordSalt", account.PasswordSalt);
                    cmd.Parameters.AddWithValue("@Role", account.Role);
                    cmd.Parameters.AddWithValue("@IsDeleted", account.IsDeleted);

                    await cmd.ExecuteNonQueryAsync();

                    await connection.CloseConnection();
                }
            }
            catch (SqlException e)
            {
                throw new InvalidOperationException($"Error: {e.Message}. SQL Server error: {e.InnerException?.Message ?? "N/A"}");
            }
        }

        public async Task Add(List<AccountDto> accounts)
        {
            foreach (var account in accounts)
            {
                try
                {
                    RegisterDto register = mapper.Map<RegisterDto>(account);

                    string salt;
                    string hashedPassword = passwordService.HashPassword(register.Password, out salt);

                    AccountDto accountDto = new AccountDto();
                    accountDto.Id = Guid.NewGuid();
                    accountDto.Email = register.Email;
                    accountDto.PasswordHash = hashedPassword;
                    accountDto.PasswordSalt = salt;
                    accountDto.Role = register.Role;

                    using (SqlCommand cmd = new SqlCommand(AddQueriesConstants.InsertIntoAccount, connection.GetConnection()))
                    {
                        await connection.OpenConnection();

                        cmd.Parameters.AddWithValue("@Id", accountDto.Id);
                        cmd.Parameters.AddWithValue("@Email", accountDto.Email);
                        cmd.Parameters.AddWithValue("@PasswordHash", accountDto.PasswordHash);
                        cmd.Parameters.AddWithValue("@PasswordSalt", accountDto.PasswordSalt);
                        cmd.Parameters.AddWithValue("@Role", accountDto.Role);

                        await cmd.ExecuteNonQueryAsync();

                        await connection.CloseConnection();
                    }
                }
                catch (SqlException e)
                {
                    throw new InvalidOperationException($"Error: {e.Message}. SQL Server error: {e.InnerException?.Message ?? "N/A"}");
                }
            }
        }

        public async Task Delete(Guid id)
        {
            Role role = new Role();
            if (role == Role.Admin)
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand(SoftDeleteQueriesConstants.SoftDeleteAccountAndInsertIntoHistorical, connection.GetConnection()))
                    {
                        await connection.OpenConnection();

                        cmd.Parameters.AddWithValue("@Id", id);
                        cmd.Parameters.AddWithValue("@DeletedByParam", $"{role}");

                        await cmd.ExecuteNonQueryAsync();

                        await connection.CloseConnection();
                    }
                }
                catch (SqlException e)
                {
                    throw new InvalidOperationException($"Error: {e.Message}. SQL Server error: {e.InnerException?.Message ?? "N/A"}");
                }
            }
            else
            {
                throw new UnauthorizedAccessException("Only Admin users can perform soft delete operations.");
            }
        }
    }
}
