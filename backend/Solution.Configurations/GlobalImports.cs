﻿global using FluentValidation.AspNetCore;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.Diagnostics;
global using Microsoft.AspNetCore.Hosting;
global using Microsoft.AspNetCore.Http;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Mvc.Infrastructure;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.DependencyInjection.Extensions;
global using Microsoft.Extensions.Hosting;
global using Microsoft.Extensions.Logging;
global using Microsoft.Extensions.Options;
global using Microsoft.IdentityModel.Tokens;
global using Microsoft.OpenApi.Models;
global using Serilog;
global using Serilog.Events;
global using Serilog.Templates;
global using Serilog.Templates.Themes;
global using Solution.Common.Models;
global using Solution.Core.Interfaces;
global using Solution.Core.Models.Response;
global using Solution.Core.Models.Settings;
global using Solution.Database;
global using Solution.Services;
global using Solution.Validators.Extensions;
global using Solution.Validators.Interceptors;
global using Swashbuckle.AspNetCore.SwaggerUI;
global using System.IdentityModel.Tokens.Jwt;
global using System.Net.Http.Headers;
global using System.Reflection;
global using System.Runtime.ExceptionServices;
global using System.Security.Claims;
global using System.Text;
global using System.Text.Json;
global using System.Text.Json.Serialization;