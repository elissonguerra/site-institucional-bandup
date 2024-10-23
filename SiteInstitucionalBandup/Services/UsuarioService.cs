using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using SiteInstitucionalBandup.Data;
using SiteInstitucionalBandup.ViewModels;
using Microsoft.EntityFrameworkCore;
using SiteInstitucionalBandup.Helpers;
using SiteInstitucionalBandup.Models;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using System.Text.Encodings.Web;


namespace SiteInstitucionalBandup.Services;

public class UsuarioService : IUsuarioService
{
    private readonly AppDbContext _contexto;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUserStore<IdentityUser> _userStore;
    private readonly IUserEmailStore<IdentityUser> _emailStore;
    private readonly IWebHostEnvironment _hostEnvironment;
    private readonly IEmailSender _emailSender;
    private readonly ILogger<UsuarioService> _logger;


    public UsuarioService(
        AppDbContext contexto,
        SignInManager<IdentityUser> signInManager,
        UserManager<IdentityUser> userManager,
        IHttpContextAccessor httpContextAccessor,
        IUserStore<IdentityUser> userStore,
        IWebHostEnvironment hostEnvironment,
        IEmailSender emailSender,
        ILogger<UsuarioService> logger
    )
    {
        _contexto = contexto;
        _signInManager = signInManager;
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
        _userStore = userStore;
        _emailStore = (IUserEmailStore<IdentityUser>)_userStore;
        _hostEnvironment = hostEnvironment;
        _emailSender = emailSender;
        _logger = logger;
    }

    public async Task<bool> ConfirmarEmail(string userId, string code)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return false;
        }
        code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
        var result = await _userManager.ConfirmEmailAsync(user, code);
        return result.Succeeded;
    }

    public async Task<UsuarioVM> GetUsuarioLogado()
    {
        var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null)
        {
            return null;
        }
        var userAccount = await _userManager.FindByIdAsync(userId);
        var usuario = await _contexto.Usuarios.Where(u => u.UsuarioId == userId).SingleOrDefaultAsync();
        var perfis = string.Join(", ", await _userManager.GetRolesAsync(userAccount));
        var admin = await _userManager.IsInRoleAsync(userAccount, "Administrador");
        UsuarioVM usuarioVM = new()
        {
            UsuarioId = userId,
            Nome = usuario.Nome,
            DataNascimento = usuario.DataNascimento,
            Foto = usuario.Foto,
            Email = userAccount.Email,
            UserName = userAccount.UserName,
            Perfil = perfis,
            IsAdmin = admin
        };
        return usuarioVM;
    }

    public async Task<SignInResult> LoginUsuario(LoginVM login)
    {
        string userName = login.Email;
        if (Helper.IsValidEmail(login.Email))
        {
            var user = await _userManager.FindByEmailAsync(login.Email);
            if (user != null)
                userName = user.UserName;
        }

        var result = await _signInManager.PasswordSignInAsync(
            userName, login.Senha, login.Lembrar, lockoutOnFailure: true
        );

        if (result.Succeeded)
            _logger.LogInformation($"Usuário {login.Email} acessou o sistema");
        if (result.IsLockedOut)
            _logger.LogWarning($"Usuário {login.Email} está bloqueado");

        return result;
    }

    public async Task LogoffUsuario()
    {
        _logger.LogInformation($"Usuário {ClaimTypes.Email} fez logoff");
        await _signInManager.SignOutAsync();
    }

    public async Task<List<string>> RegistrarUsuario(RegistroVM registro)
    {
        var user = Activator.CreateInstance<IdentityUser>();

        await _userStore.SetUserNameAsync(user, registro.Email, CancellationToken.None);
        await _emailStore.SetEmailAsync(user, registro.Email, CancellationToken.None);
        var result = await _userManager.CreateAsync(user, registro.Senha);

        if (result.Succeeded)
        {
            _logger.LogInformation($"Novo usuário registrado com o email {user.Email}.");

            var userId = await _userManager.GetUserIdAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var url = $"http://localhost:5143/Account/ConfirmarEmail?userId={userId}&code={code}";

            await _userManager.AddToRoleAsync(user, "Usuário");

            await _emailSender.SendEmailAsync(registro.Email, "SiteInstitucionalBandup - Criação de Conta", GetConfirmEmailHtml(HtmlEncoder.Default.Encode(url)));

            // Cria a conta pessoal do usuário
            Usuario usuario = new()
            {
                UsuarioId = userId,
                DataNascimento = registro.DataNascimento ?? DateTime.Now,
                Nome = registro.Nome
            };
            if (registro.Foto != null)
            {
                string fileName = userId + Path.GetExtension(registro.Foto.FileName);
                string uploads = Path.Combine(_hostEnvironment.WebRootPath, @"img\usuarios");
                string newFile = Path.Combine(uploads, fileName);
                using (var stream = new FileStream(newFile, FileMode.Create))
                {
                    registro.Foto.CopyTo(stream);
                }
                usuario.Foto = @"\img\usuarios\" + fileName;
            }
            _contexto.Add(usuario);
            await _contexto.SaveChangesAsync();

            return null;
        }

        List<string> errors = new();
        foreach (var error in result.Errors)
        {
            errors.Add(TranslateIdentityErrors.TranslateErrorMessage(error.Code));
        }
        return errors;
    }


    private string GetConfirmEmailHtml(string url)
    {
        var email = @"
        <!DOCTYPE html>
        <html>
        <head>

        <meta charset=""utf-8"">
        <meta http-equiv=""x-ua-compatible"" content=""ie=edge"">
        <title>Email Confirmation</title>
        <meta name=""viewport"" content=""width=device-width, initial-scale=1"">
        <style type=""text/css"">
        /**
        * Google webfonts. Recommended to include the .woff version for cross-client compatibility.
        */
        @media screen {
            @font-face {
            font-family: 'Source Sans Pro';
            font-style: normal;
            font-weight: 400;
            src: local('Source Sans Pro Regular'), local('SourceSansPro-Regular'), url(https://fonts.gstatic.com/s/sourcesanspro/v10/ODelI1aHBYDBqgeIAH2zlBM0YzuT7MdOe03otPbuUS0.woff) format('woff');
            }
            @font-face {
            font-family: 'Source Sans Pro';
            font-style: normal;
            font-weight: 700;
            src: local('Source Sans Pro Bold'), local('SourceSansPro-Bold'), url(https://fonts.gstatic.com/s/sourcesanspro/v10/toadOcfmlt9b38dHJxOBGFkQc6VGVFSmCnC_l7QZG60.woff) format('woff');
            }
        }
        /**
        * Avoid browser level font resizing.
        * 1. Windows Mobile
        * 2. iOS / OSX
        */
        body,
        table,
        td,
        a {
            -ms-text-size-adjust: 100%; /* 1 */
            -webkit-text-size-adjust: 100%; /* 2 */
        }
        /**
        * Remove extra space added to tables and cells in Outlook.
        */
        table,
        td {
            mso-table-rspace: 0pt;
            mso-table-lspace: 0pt;
        }
        /**
        * Better fluid images in Internet Explorer.
        */
        img {
            -ms-interpolation-mode: bicubic;
        }
        /**
        * Remove blue links for iOS devices.
        */
        a[x-apple-data-detectors] {
            font-family: inherit !important;
            font-size: inherit !important;
            font-weight: inherit !important;
            line-height: inherit !important;
            color: inherit !important;
            text-decoration: none !important;
        }
        /**
        * Fix centering issues in Android 4.4.
        */
        div[style*=""margin: 16px 0;""] {
            margin: 0 !important;
        }
        body {
            width: 100% !important;
            height: 100% !important;
            padding: 0 !important;
            margin: 0 !important;
        }
        /**
        * Collapse table borders to avoid space between cells.
        */
        table {
            border-collapse: collapse !important;
        }
        a {
            color: #1a82e2;
        }
        img {
            height: auto;
            line-height: 100%;
            text-decoration: none;
            border: 0;
            outline: none;
        }
        </style>

        </head>
        <body style=""background-color: #e9ecef;"">

        <!-- start preheader -->
        <div class=""preheader"" style=""display: none; max-width: 0; max-height: 0; overflow: hidden; font-size: 1px; line-height: 1px; color: #fff; opacity: 0;"">
            Website GCoock - Confirmação de Conta.
        </div>
        <!-- end preheader -->

        <!-- start body -->
        <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"">

            <!-- start logo -->
            <tr>
            <td align=""center"" bgcolor=""#e9ecef"">
                <!--[if (gte mso 9)|(IE)]>
                <table align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"" width=""600"">
                <tr>
                <td align=""center"" valign=""top"" width=""600"">
                <![endif]-->
                <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""max-width: 600px;"">
                <tr>
                    <td align=""center"" valign=""top"" style=""padding: 36px 24px;"">
                    <a href=""localhost:5143"" target=""_blank"" style=""display: inline-block;"">
                        <img src=""https://github.com/3-MTecPi/SiteInstitucionalBandupA/blob/b2cff88fe35a5c5283b04639ca5caa43ee91a6bb/SiteInstitucionalBandup/wwwroot/img/logo.png?raw=true"" alt=""Logo"" border=""0"" width=""100"" style=""display: block; width: 100px; max-width: 400px; min-width: 100px;"">
                    </a>
                    </td>
                </tr>
                </table>
                <!--[if (gte mso 9)|(IE)]>
                </td>
                </tr>
                </table>
                <![endif]-->
            </td>
            </tr>
            <!-- end logo -->

            <!-- start hero -->
            <tr>
            <td align=""center"" bgcolor=""#e9ecef"">
                <!--[if (gte mso 9)|(IE)]>
                <table align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"" width=""600"">
                <tr>
                <td align=""center"" valign=""top"" width=""600"">
                <![endif]-->
                <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""max-width: 600px;"">
                <tr>
                    <td align=""left"" bgcolor=""#ffffff"" style=""padding: 36px 24px 0; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; border-top: 3px solid #d4dadf;"">
                    <h1 style=""margin: 0; font-size: 32px; font-weight: 700; letter-spacing: -1px; line-height: 48px;"">Confirmação de Endereço de Email</h1>
                    </td>
                </tr>
                </table>
                <!--[if (gte mso 9)|(IE)]>
                </td>
                </tr>
                </table>
                <![endif]-->
            </td>
            </tr>
            <!-- end hero -->

            <!-- start copy block -->
            <tr>
            <td align=""center"" bgcolor=""#e9ecef"">
                <!--[if (gte mso 9)|(IE)]>
                <table align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"" width=""600"">
                <tr>
                <td align=""center"" valign=""top"" width=""600"">
                <![endif]-->
                <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""max-width: 600px;"">

                <!-- start copy -->
                <tr>
                    <td align=""left"" bgcolor=""#ffffff"" style=""padding: 24px; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; font-size: 16px; line-height: 24px;"">
                    <p style=""margin: 0;"">Clique no botão abaixo para confirmar a criação de sua conta. Se você não solicitou acesso ao site de receitas SiteInstitucionalBandup, apenas exclua esse email.
                        .</p>
                    </td>
                </tr>
                <!-- end copy -->

                <!-- start button -->
                <tr>
                    <td align=""left"" bgcolor=""#ffffff"">
                    <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"">
                        <tr>
                        <td align=""center"" bgcolor=""#ffffff"" style=""padding: 12px;"">
                            <table border=""0"" cellpadding=""0"" cellspacing=""0"">
                            <tr>
                                <td align=""center"" bgcolor=""red"" style=""border-radius: 6px;"">
                                <a href=""" + url + @""" target=""_blank"" style=""display: inline-block; padding: 16px 36px; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; font-size: 16px; color: #ffffff; text-decoration: none; border-radius: 6px;"">Confirmar Conta</a>
                                </td>
                            </tr>
                            </table>
                        </td>
                        </tr>
                    </table>
                    </td>
                </tr>
                <!-- end button -->

                <!-- start copy -->
                <tr>
                    <td align=""left"" bgcolor=""#ffffff"" style=""padding: 24px; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; font-size: 16px; line-height: 24px;"">
                    <p style=""margin: 0;"">Se isto não funcionar, clique no link a seguir em um navegador:</p>
                    <p style=""margin: 0;""><a href=""" + url + @""" target=""_blank"">https://SiteInstitucionalBandup.com.br/confirmarEmail</a></p>
                    </td>
                </tr>
                <!-- end copy -->

                <!-- start copy -->
                <tr>
                    <td align=""left"" bgcolor=""#ffffff"" style=""padding: 20px; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; font-size: 16px; line-height: 24px; border-bottom: 3px solid #d4dadf"">
                    <p style=""margin: 0;"">Atenciosamente,<br> Equipe SiteInstitucionalBandup</p>
                    </td>
                </tr>
                <!-- end copy -->

                </table>
                <!--[if (gte mso 9)|(IE)]>
                </td>
                </tr>
                </table>
                <![endif]-->
            </td>
            </tr>
            <!-- end copy block -->

            <!-- start footer -->
            <tr>
            <td align=""center"" bgcolor=""#e9ecef"" style=""padding: 24px;"">
                <!--[if (gte mso 9)|(IE)]>
                <table align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"" width=""600"">
                <tr>
                <td align=""center"" valign=""top"" width=""600"">
                <![endif]-->
                <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""max-width: 600px;"">

                <!-- start permission -->
                <tr>
                    <td align=""center"" bgcolor=""#e9ecef"" style=""padding: 12px 24px; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; font-size: 14px; line-height: 20px; color: #666;"">
                    <p style=""margin: 0;"">Você recebeu este email por solicitar acesso ao site SiteInstitucionalBandup. Se você não solicitou acesso, apenas apague este email.</p>
                    </td>
                </tr>
                <!-- end permission -->

                </table>
                <!--[if (gte mso 9)|(IE)]>
                </td>
                </tr>
                </table>
                <![endif]-->
            </td>
            </tr>
            <!-- end footer -->

        </table>
        <!-- end body -->

        </body>
        </html>
        ";
        return email;
    }



}