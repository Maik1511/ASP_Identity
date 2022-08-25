namespace WebApp.Pages.Account;

public class LoginModel : PageModel
{
    [BindProperty] public Credential? Credential { get; set; }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        //Überprüfung der Credentail
        if (Credential.UserName == "admin" && Credential.Password == "admin")
        {
            // Erstellung des Sicherheitscontext
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "admin"),
                new Claim(ClaimTypes.Email, "admin@localhost.de")
            };
            var identity = new ClaimsIdentity(claims, "MyCookieAuth");
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal);

            return RedirectToPage("/Index");
        }

        return Page();
    }
}

public class Credential
{
    [Required]
    [Display(Name = "User Name ")]
    public string? UserName { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Password ")]
    public string? Password { get; init; }
}