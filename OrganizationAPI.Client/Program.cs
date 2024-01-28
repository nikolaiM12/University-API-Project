using Newtonsoft.Json;
using OrganizationAPI.Client.Domain.Abstractions.Services;
using OrganizationAPI.Domain.Abstractions.Constants;
using OrganizationAPI.Domain.Abstractions.DTOs.Account;
using OrganizationAPI.Domain.Abstractions.DTOs.Authentication;
using OrganizationAPI.Domain.Abstractions.Enums;
using OrganizationAPI.Domain.Services;
using System.Text;

namespace OrganizationAPI.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                ICsvParsingService csvParsingService = new CsvParsingService();
                IFileSystemService fileSystemService = new FileSystemService();

                Console.WriteLine("Available operations:");
                Console.WriteLine("1. Parse CSV file");
                Console.WriteLine("2. Create Account");
                Console.WriteLine("3. Register");
                Console.WriteLine("4. Login");
                Console.WriteLine("5. Exit");

                while(true)
                {
                    Console.WriteLine("Enter a number");
                    string operationChoice = Console.ReadLine();

                    switch (operationChoice)
                    {
                        case "1":
                            await ProcessCsvParsing(csvParsingService);
                            break;
                        case "2":
                            Console.Write("First name: ");
                            string firstName = Console.ReadLine();
                            Console.Write("Last name: ");
                            string lastName = Console.ReadLine();
                            Console.Write("Age: ");
                            if (int.TryParse(Console.ReadLine(), out int ageAdd))
                            {
                                Console.Write("Phone number: ");
                                string phoneNumber = Console.ReadLine();
                                Console.Write("Country: ");
                                string country = Console.ReadLine();
                                Console.Write("Email: ");
                                string emailAdd = Console.ReadLine();
                                Console.Write("Password: ");
                                string passwordReg = Console.ReadLine();
                                Console.Write("Role: ");
                                string roleInput = Console.ReadLine();

                                if (Enum.TryParse(typeof(Role), roleInput, out object roleObj))
                                {
                                    if (roleObj is Role role)
                                    {
                                        var add = await AddAccount(
                                            new RegisterDto
                                            {
                                                FirstName = firstName,
                                                LastName = lastName,
                                                Age = ageAdd,
                                                PhoneNumber = phoneNumber,
                                                Country = country,
                                                Email = emailAdd,
                                                Password = passwordReg,
                                                Role = role
                                            },
                                            AllowedIPConstants.allowedIP
                                        );

                                        if (add != null)
                                        {
                                            Console.WriteLine("Account created successfully");
                                            Console.WriteLine("Account:");
                                            Console.WriteLine(JsonConvert.SerializeObject(add));
                                        }
                                        else
                                        {
                                            Console.WriteLine("Failed to create account");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid role.");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Invalid role input.");
                                }
                            }
                            break;
                        case "3":
                            Console.Write("First name: ");
                            string firstNameReg = Console.ReadLine();
                            Console.Write("Last name: ");
                            string lastNameReg = Console.ReadLine();
                            Console.Write("Age: ");
                            if (int.TryParse(Console.ReadLine(), out int ageReg))
                            {
                                Console.Write("Phone number: ");
                                string phoneNumber = Console.ReadLine();
                                Console.Write("Country: ");
                                string country = Console.ReadLine();
                                Console.Write("Email: ");
                                string emailReg = Console.ReadLine();
                                Console.Write("Password: ");
                                string passwordReg = Console.ReadLine();
                                Console.Write("Role: ");
                                string roleInput = Console.ReadLine();

                                if (Enum.TryParse(typeof(Role), roleInput, out object roleObj) && roleObj is Role role)
                                {
                                    var reg = await Register(
                                        new RegisterDto
                                        {
                                            FirstName = firstNameReg,
                                            LastName = lastNameReg,
                                            Age = ageReg,
                                            PhoneNumber = phoneNumber,
                                            Country = country,
                                            Email = emailReg,
                                            Password = passwordReg,
                                            Role = role
                                        },
                                        AllowedIPConstants.allowedIP
                                    );

                                    if (reg != null)
                                    {
                                        Console.WriteLine("Registration successful");
                                        Console.WriteLine("Registration:");
                                        Console.WriteLine(reg);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Registration failed");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Invalid role input.");
                                }
                            }
                            break;
                        case "4":
                            Console.Write("Email: ");
                            string email = Console.ReadLine();
                            Console.Write("Password: ");
                            string password = Console.ReadLine();
                            var token = await Login(
                                new LoginDto
                                {
                                    Email = email,
                                    Password = password
                                },
                                AllowedIPConstants.allowedIP
                            );

                            if (token != null)
                            {
                                Console.WriteLine("Logged in successfully!");
                                Console.WriteLine($"Token: {token}");
                            }
                            else
                            {
                                Console.WriteLine("Login failed!");
                            }
                            break;
                        case "5":
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Invalid operation choice.");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in main process: {ex.Message}");
            }
        }

        static async Task ProcessCsvParsing(ICsvParsingService csvParsingService)
        {
            try
            {
                await csvParsingService.ParseCsvFile();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while parsing CSV files: {ex.Message}");
            }
        }

        static async Task<string> Register(RegisterDto register, string ipAddress)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var apiUrl = "https://localhost:7117/auth/register";

                    client.DefaultRequestHeaders.Add("X-Forwarded-For", ipAddress);
                    var json = JsonConvert.SerializeObject(register);
                    var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(apiUrl, stringContent);
                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        return responseContent;
                    }
                    else
                    {
                        throw new Exception($"Error: {response.StatusCode}");
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Error: {e.Message}");
            }
        }

        static async Task<string> Login(LoginDto login, string ipAddress)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var apiUrl = "https://localhost:7117/auth/login";

                    client.DefaultRequestHeaders.Add("X-Forwarded-For", ipAddress);
                    var json = JsonConvert.SerializeObject(login);
                    var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(apiUrl, stringContent);
                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        return responseContent;
                    }
                    else
                    {
                        throw new Exception($"Error: {response.StatusCode}");
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Error: {e.Message}");
            }
        }

        static async Task<string> AddAccount(RegisterDto register, string ipAddress)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var apiUrl = "https://localhost:7117/accounts/create";
                    client.DefaultRequestHeaders.Add("X-Forwarded-For", ipAddress);
                    var json = JsonConvert.SerializeObject(register);
                    var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(apiUrl, stringContent);
                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        return responseContent;
                    }
                    else
                    {
                        throw new Exception($"Error: {response.StatusCode}");
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Error: {e.Message}");
            }
        }
    }
}
