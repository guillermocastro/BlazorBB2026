using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;

namespace BlazorBB2026.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CookieController : ControllerBase
    {
        [HttpGet("/cookie/essential")]
        public async Task<ActionResult> EssentialCookie()
        {
            CookieOptions options = new CookieOptions()
            {
                IsEssential = true
            };
            Response.Cookies.Append("essentialCookie", $"{Guid.NewGuid()}", options);
            return Redirect("/");
        }
        [HttpGet("/cookie/nonessential")]
        public async Task<ActionResult> NonEssentialCookie()
        {
            CookieOptions options = new CookieOptions()
            {
                IsEssential = true
            };
            Response.Cookies.Append("nonessentialCookie", $"{Guid.NewGuid()}", options);
            return Redirect("/");
        }
        [HttpGet("/cookie/consent")]
        public async Task<ActionResult> SetConsent()
        {
            ITrackingConsentFeature consent = HttpContext.Features.Get<ITrackingConsentFeature>();
            if (!consent.CanTrack)
            {
                consent.GrantConsent();
            }
            return Redirect("/");
        }
    }
}
