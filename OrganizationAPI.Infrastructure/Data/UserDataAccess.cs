using AutoMapper;
using OrganizationAPI.Domain.Abstractions.Constants;
using OrganizationAPI.Domain.Abstractions.Database;
using OrganizationAPI.Domain.Abstractions.DTOs.Authentication;
using OrganizationAPI.Domain.Abstractions.DTOs.User;
using OrganizationAPI.Domain.Abstractions.Repositories;
using System.Data.SqlClient;

namespace OrganizationAPI.Infrastructure.Data
{
    public abstract class UserDataAccess : IRepository<UserDto, Guid>
    {
        private readonly IDbConnector connection;
        private readonly IMapper mapper;
        public UserDataAccess(IDbConnector connection, IMapper mapper)
        {
            this.connection = connection;
            this.mapper = mapper;
        }

        public async Task<List<UserDto>> GetAll()
        {
            try
            {
                List<UserDto> users = new List<UserDto>();

                using (SqlCommand cmd = new SqlCommand(GetAllQueriesConstants.GetAllUsersQuery, connection.GetConnection()))
                {
                    await connection.OpenConnection();

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            UserDto user = new UserDto();
                            user.Id = reader.GetGuid(reader.GetOrdinal("Id"));
                            user.FirstName = reader.GetString(reader.GetOrdinal("FirstName"));
                            user.LastName = reader.GetString(reader.GetOrdinal("LastName"));
                            user.Age = reader.GetInt32(reader.GetOrdinal("Age"));
                            user.PhoneNumber = reader.GetString(reader.GetOrdinal("PhoneNumber"));
                            user.Country = reader.GetString(reader.GetOrdinal("Country"));
                            user.AccountId = reader.GetGuid(reader.GetOrdinal("AccountId"));
                            user.IsDeleted = reader.GetBoolean(reader.GetOrdinal("IsDeleted"));

                            users.Add(user);
                        }
                    }

                    await connection.CloseConnection();
                }

                return users;
            }
            catch (SqlException e)
            {
                throw new InvalidOperationException($"Error: {e.Message}, InnerException: {e.InnerException}");
            }
        }
        public async Task Add(List<UserDto> users)
        {
            foreach (var user in users)
            {
                try
                {
                    RegisterDto register = mapper.Map<RegisterDto>(user);

                    UserDto userDto = new UserDto();
                    userDto.Id = Guid.NewGuid();
                    userDto.FirstName = register.FirstName;
                    userDto.LastName = register.LastName;
                    userDto.Age = register.Age;
                    userDto.PhoneNumber = register.PhoneNumber;
                    userDto.Country = register.Country;
                    userDto.AccountId = user.AccountId;

                    using (SqlCommand cmd = new SqlCommand(AddQueriesConstants.InsertIntoUser, connection.GetConnection()))
                    {
                        await connection.OpenConnection();

                        cmd.Parameters.AddWithValue("@Id", userDto.Id);
                        cmd.Parameters.AddWithValue("@FirstName", userDto.FirstName);
                        cmd.Parameters.AddWithValue("@LastName", userDto.LastName);
                        cmd.Parameters.AddWithValue("@Age", userDto.Age);
                        cmd.Parameters.AddWithValue("@PhoneNumber", userDto.PhoneNumber);
                        cmd.Parameters.AddWithValue("@Country", userDto.Country);
                        cmd.Parameters.AddWithValue("@AccountId", userDto.AccountId);

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

        public async Task<UserDto> GetById(Guid id)
        {
            try
            {
                UserDto user = new UserDto();

                using (SqlCommand cmd = new SqlCommand(GetByQueriesConstants.GetUserById, connection.GetConnection()))
                {
                    await connection.OpenConnection();

                    cmd.Parameters.AddWithValue(@"UserId", id);

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            user = new UserDto();
                            user.Id = reader.GetGuid(reader.GetOrdinal("Id"));
                            user.FirstName = reader.GetString(reader.GetOrdinal("FirstName"));
                            user.LastName = reader.GetString(reader.GetOrdinal("LastName"));
                            user.Age = reader.GetInt32(reader.GetOrdinal("Age"));
                            user.PhoneNumber = reader.GetString(reader.GetOrdinal("PhoneNumber"));
                            user.Country = reader.GetString(reader.GetOrdinal("Country"));
                            user.AccountId = reader.GetGuid(reader.GetOrdinal("AccountId"));
                        }
                    }

                    await connection.CloseConnection();
                }

                return user;
            }
            catch (SqlException e)
            {
                throw new InvalidOperationException($"Error: {e.Message}, InnerException: {e.InnerException}");
            }
        }

        public async Task Update(UserDto user)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand(UpdateQueriesConstants.UpdateUser, connection.GetConnection()))
                {
                    await connection.OpenConnection();

                    cmd.Parameters.AddWithValue("@UserId", user.Id);
                    cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", user.LastName);
                    cmd.Parameters.AddWithValue("@Age", user.Age);
                    cmd.Parameters.AddWithValue("@PhoneNumber", user.PhoneNumber);
                    cmd.Parameters.AddWithValue("@Country", user.Country);
                    cmd.Parameters.AddWithValue("@AccountId", user.AccountId);
                    cmd.Parameters.AddWithValue("@IsDeleted", user.IsDeleted);

                    await cmd.ExecuteNonQueryAsync();

                    await connection.CloseConnection();
                }
            }
            catch (SqlException e)
            {
                throw new InvalidOperationException($"Error: {e.Message}. SQL Server error: {e.InnerException?.Message ?? "N/A"}");
            }
        }
        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
