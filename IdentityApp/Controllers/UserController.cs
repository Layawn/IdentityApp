using IdentityApp.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IdentityApp.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public UserController(ApplicationDbContext context, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            return View(users);
        }
        
        public async Task<IActionResult> Block(List<string> TheModelFieldName)
        {
            if (TheModelFieldName != null)
            {
                return PartialView("Block", TheModelFieldName);
            }
            return View();
        }

        [HttpPost, ActionName("Block")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BlockUser(List<string> TheModelFieldName)
        {
            for (int i = 0; i < TheModelFieldName.Count; i++)
            {
                var userToUpdate = _context.Users.FirstOrDefault(s => s.Id == TheModelFieldName[i]);
                userToUpdate.statusBlock = true;
                userToUpdate.LockoutEnd = DateTimeOffset.MaxValue;
                if (await TryUpdateModelAsync<ApplicationUser>(userToUpdate, "", s => s.statusBlock))
                {
                    try
                    {
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateException)
                    {
                        ModelState.AddModelError("", "Unable to save changes. " +
                            "Try again, and if the problem persists, " +
                            "see your system administrator.");
                    }
                }
            }
            return RedirectToAction(nameof(GetUsers));
        }
        public async Task<IActionResult> Unblock(List<string> TheModelFieldName)
        {
            if (TheModelFieldName != null)
            {
                return PartialView("Unblock", TheModelFieldName);
            }
            return View();
        }

        [HttpPost, ActionName("Unblock")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UnblockUser(List<string> TheModelFieldName)
        {
            for (int i = 0; i < TheModelFieldName.Count; i++)
            {
                var userToUpdate = _context.Users.FirstOrDefault(s => s.Id == TheModelFieldName[i]);
                userToUpdate.statusBlock = false;
                userToUpdate.LockoutEnd = null;
                if (await TryUpdateModelAsync<ApplicationUser>(userToUpdate, "", s => s.statusBlock))
                {
                    try
                    {
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateException)
                    {
                        ModelState.AddModelError("", "Unable to save changes. " +
                            "Try again, and if the problem persists, " +
                            "see your system administrator.");
                    }
                }
            }
            return RedirectToAction(nameof(GetUsers));
        }
        public async Task<IActionResult> Delete(List<string> TheModelFieldName)
        {
            if (TheModelFieldName != null)
            {
                return PartialView("Delete", TheModelFieldName);
            }
            return View();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUser(List<string> TheModelFieldName)
        {
            for (int i = 0; i < TheModelFieldName.Count; i++)
            {
                var user = await _context.Users.FindAsync(TheModelFieldName[i]);
                if (user == null)
                {
                    return RedirectToAction(nameof(GetUsers));
                }
                try
                {
                    _context.Users.Remove(user);
                    await _context.SaveChangesAsync();
                    
                }
                catch (DbUpdateException)
                {
                    return RedirectToAction(nameof(DeleteUser), new { id = TheModelFieldName[i], saveChangesError = true });
                }
            }
            return RedirectToAction(nameof(GetUsers));
        }
    }
}
