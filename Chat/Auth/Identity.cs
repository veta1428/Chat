using System.Security.Claims;

namespace Chat.Auth
{
    public class Identity : ClaimsIdentity
    {
        public Identity() : base("Custom")
        {
        }

        public Identity(ClaimsIdentity claimsIdentity) : base(claimsIdentity)
        {
        }

        public virtual int? Id
        {
            get
            {
                var value = FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (value != null)
                {
                    return int.Parse(value);
                }

                return null;
            }

            set => AddClaim(new Claim(ClaimTypes.NameIdentifier, value.ToString()!));
        }

        public virtual string? FirstName
        {
            get => FindFirst(ClaimTypes.GivenName)?.Value;
            set => AddClaim(new Claim(ClaimTypes.GivenName, value));
        }

        public virtual string? LastName
        {
            get => FindFirst(ClaimTypes.Surname)?.Value;
            set => AddClaim(new Claim(ClaimTypes.Surname, value));
        }

        public virtual string? Email
        {
            get => FindFirst(ClaimTypes.Email)?.Value;
            set => AddClaim(new Claim(ClaimTypes.Email, value));
        }
    }
}
