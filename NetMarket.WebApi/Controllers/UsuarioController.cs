using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;

using AutoMapper;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using NetMarket.Core.Entities;
using NetMarket.Core.Interfaces;
using NetMarket.Core.Specification;
using NetMarket.WebApi.Dto;
using NetMarket.WebApi.Dto.Login;
using NetMarket.WebApi.Errors;
using NetMarket.WebApi.Extensions;

using StackExchange.Redis;

namespace NetMarket.WebApi.Controllers
{
    public class UsuarioController : BaseApiController
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper mapper;
        private readonly IPasswordHasher<Usuario> _passwordHasher;
        private readonly IGenericSegurancaRepository<Usuario> _genericSegurancaRepository;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsuarioController(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager,
            ITokenService tokenService,
            IMapper mapper,
            IPasswordHasher<Usuario> passwordHasher,
            IGenericSegurancaRepository<Usuario> genericSegurancaRepository,
            RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _tokenService = tokenService;
            this.mapper = mapper;
            _userManager = userManager;
            _passwordHasher = passwordHasher;
            this._genericSegurancaRepository = genericSegurancaRepository;
            this._roleManager = roleManager;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UsuarioDto>> Login(LoginDto loginDto)
        {
            var usuario = await _userManager.FindByEmailAsync(loginDto.Email);

            if (usuario == null)
            {
                return Unauthorized(new CodeErrorResponse(401));
            }

            var resultado = await _signInManager.CheckPasswordSignInAsync(usuario, loginDto.Password, false);

            if (!resultado.Succeeded)
            {
                return Unauthorized(new CodeErrorResponse(401));
            }
            var roles = await _userManager.GetRolesAsync(usuario);
            return new UsuarioDto
            {
                Id = usuario.Id,
                Email = usuario.Email,
                UserName = usuario.UserName,
                Token = _tokenService.CreateToken(usuario, roles),
                Nome = usuario.Nome,
                SobreNome = usuario.SobreNome,
                Admin = roles.Contains("ADMIN"),
                Imagem = usuario.Imagem
            };
        }

        [HttpPost("registrar")]
        public async Task<ActionResult<UsuarioDto>> Registrar(RegistrarDto registrarDto)
        {
            var usuario = new Usuario
            {
                Email = registrarDto.Email,
                UserName = registrarDto.UserName,
                Nome = registrarDto.Nome,
                SobreNome = registrarDto.SobreNome,
            };
            var resultado = await _userManager.CreateAsync(usuario, registrarDto.Password);
            if (!resultado.Succeeded)
            {
                return BadRequest(new CodeErrorResponse(400));
            }
            var roles = await _userManager.GetRolesAsync(usuario);
            return new UsuarioDto
            {
                Id = usuario.Id,
                Email = usuario.Email,
                Nome = usuario.Nome,
                SobreNome = usuario.SobreNome,
                Token = _tokenService.CreateToken(usuario, roles),
                UserName = usuario.UserName,
                Admin = roles.Contains("ADMIN")
            };
        }

        [Authorize(Roles = "ADMIN")]
        [HttpGet("account/{id}")]
        public async Task<ActionResult<UsuarioDto>> GetUsuarioById(string id)
        {
            var usuario = await _userManager.FindByIdAsync(id);
            if (usuario == null)
            {
                return NotFound(new CodeErrorResponse(404, "Usuário não encontrado"));
            }
            var roles = await _userManager.GetRolesAsync(usuario);
            return new UsuarioDto
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                SobreNome = usuario.SobreNome,
                Email = usuario.Email,
                Admin = roles.Contains("ADMIN"),
                Imagem = usuario.Imagem,
                UserName = usuario.UserName,
            };
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UsuarioDto>> GetUsuario()
        {
            //var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            //var usuario = await _userManager.FindByEmailAsync(email);
            var usuario = await _userManager.BuscarUsuarioAsync(HttpContext.User);
            var roles = await _userManager.GetRolesAsync(usuario);
            if (usuario == null)
            {
                return NotFound(new CodeErrorResponse(404));
            }
            return new UsuarioDto {
                Id = usuario.Id,
                Email = usuario.Email,
                Nome = usuario.Nome,
                SobreNome = usuario.SobreNome,
                UserName = usuario.UserName,
                Token = _tokenService.CreateToken(usuario, roles),
                Admin = roles.Contains("ADMIN"),
                Imagem = usuario.Imagem,
        };
        }

        //[Authorize]
        [HttpGet("emailvalido")]
        public async Task<ActionResult<bool>> ValidarEmail([FromQuery] string email)
        {
            var usuario = await _userManager.FindByEmailAsync(email);

            if (usuario == null) return false;
            return true;
        }

        [Authorize]
        [HttpGet("endereco")]
        public async Task<ActionResult<EnderecoDto>> GetEndereco()
        {
            //var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            //var usuario = await _userManager .FindByEmailAsync(email);
            var usuario = await _userManager.BuscarUsuarioComEnderecoAsync(HttpContext.User);
            if (usuario == null || usuario.Endereco == null)
            {
                return BadRequest(new CodeErrorResponse(400));
            }

            return mapper.Map<Endereco, EnderecoDto>(usuario.Endereco);
        }

        [Authorize]
        [HttpPut("endereco")]
        public async Task<ActionResult<EnderecoDto>> PutEndereco(EnderecoDto enderecoDto)
        {
            
            var usuario = await _userManager.BuscarUsuarioComEnderecoAsync(HttpContext.User);
            if (usuario == null || usuario.Endereco == null)
            {
                return BadRequest(new CodeErrorResponse(400));
            }

            usuario.Endereco = mapper.Map<EnderecoDto, Endereco>(enderecoDto);
            var resultado = await _userManager.UpdateAsync(usuario);
            if (resultado.Succeeded) return Ok(mapper.Map<Endereco, EnderecoDto>(usuario.Endereco));

            return BadRequest("Não foi possível atualizar o endereço do usuário.");
        }

        [Authorize]
        [HttpPut("atualizar/{id}")]
        public async Task<ActionResult<UsuarioDto>> Atualizar(string id, RegistrarDto registrarDto)
        {
            var usuario = await _userManager.FindByIdAsync(id);

            if (usuario == null)
            {
                return NotFound(new CodeErrorResponse(404, "O usuário não existe"));
            }
            usuario.Nome = registrarDto.Nome;
            usuario.SobreNome = registrarDto.SobreNome;
            usuario.Imagem = registrarDto.Imagem;
            if (!string.IsNullOrEmpty(registrarDto.Password))
                usuario.PasswordHash = _passwordHasher.HashPassword(usuario, registrarDto.Password);

            var resultado = await _userManager.UpdateAsync(usuario);

            if (resultado.Succeeded)
            {
                var roles = await _userManager.GetRolesAsync(usuario);
                return new UsuarioDto
                {
                    Email = usuario.Email,
                    Nome = usuario.Nome,
                    SobreNome = usuario.SobreNome,
                    UserName = usuario.UserName,
                    Token = _tokenService.CreateToken(usuario, roles),
                    Imagem = usuario.Imagem,
                    Admin = roles.Contains("ADMIN")
                };
            }
            else
                return BadRequest(new CodeErrorResponse(
                    400,
                    "Não pode atualizar o usuário"
                ));
        }

        [Authorize(Roles = "ADMIN")]
        [HttpGet("pagination")]
        public async Task<ActionResult<UsuarioDto>> GetUsuarios([FromQuery] UsuarioSpecificationParams usuarioParams)
        {
            var spec = new UsuarioSpecification(usuarioParams);
            var usuarios = await _genericSegurancaRepository.GetAllWithSpecAsync(spec);

            var specCount = new UsuarioForCountingSpecification(usuarioParams);
            var totalUsuarios = await _genericSegurancaRepository.CountAsync(specCount);

            var rounded = Math.Ceiling((decimal)(totalUsuarios / usuarioParams.PageSize));
            var totalPages = Convert.ToInt32(rounded);

            var data = mapper.Map<IReadOnlyList<Usuario>, IReadOnlyList<UsuarioDto>>(usuarios);

            return Ok(
                new Pagination<UsuarioDto>
                {
                    Count = totalUsuarios,
                    Data = data,
                    PageCount = totalPages,
                    PageIndex = usuarioParams.PageIndex,
                    PageSize = usuarioParams.PageSize,
                }
                );
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPut("role/{idUsuario}")]
        public async Task<ActionResult<UsuarioDto>> UpdateRole(string idUsuario, RoleDto rolParam)
        {
            var role = await _roleManager.FindByNameAsync(rolParam.Nome);
            if (role == null)
                return NotFound(new CodeErrorResponse(404, "O role não existe"));

            var usuario = await _userManager.FindByIdAsync(idUsuario);
            if (usuario == null) {
                return NotFound(new CodeErrorResponse(404, "O usuário não existe!"));
            }

            var usuarioDto = mapper.Map<Usuario, UsuarioDto>(usuario);

            if (rolParam.Status)
            {
                var resultado = await _userManager.AddToRoleAsync(usuario, rolParam.Nome);
                if (resultado.Succeeded)
                {
                    usuarioDto.Admin = true;

                }
                if (resultado.Errors.Any())
                {
                    if (resultado.Errors.Where(x => x.Code == "UserAlreadyInRole").Any())
                    {
                        usuarioDto.Admin = true;
                    }
                }
            }
            else
            {
                var resultado = await _userManager.RemoveFromRoleAsync(usuario, rolParam.Nome);
                if (resultado.Succeeded)
                {
                    usuarioDto.Admin = false;
                }
            }
            var roles = new List<string>();
            
            if (usuarioDto.Admin)
            {
                roles.Add("ADMIN");
            }
            usuarioDto.Token = _tokenService.CreateToken(usuario, roles.Count == 0? null: roles);

            return usuarioDto;
        }
    }
}
