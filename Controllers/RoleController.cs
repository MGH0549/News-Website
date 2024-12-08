using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using NewsWebsite.Models;
using System.Data.Entity;
using System.Web.Providers.Entities;

namespace NewsWebsite.Controllers
{
     [Authorize(Roles ="Manager")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;
        public RoleController(RoleManager<IdentityRole> roleManager, IMapper mapper, UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _mapper = mapper;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var roles = _roleManager.Roles.ToList();
            var result=_mapper.Map<List<RoleViewModel>>(roles);
            return View(result.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel model)
        {
            if (ModelState.IsValid) { 
            
                var role=new IdentityRole { Name = model.Name };
             var result=await _roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                return View(model);
            }
            return View(model);
        }

        public async Task<IActionResult> Update(string id)
        {
            var role=await _roleManager.FindByIdAsync(id);
            if (role == null)return NotFound();
            var result =_mapper.Map<RoleViewModel>(role);
            return View(result  );
        }

        [HttpPost]
        public async Task<IActionResult> Update(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {

                
                var role = await _roleManager.FindByIdAsync(model.Id.ToString());
                if (role == null) return NotFound();
                var newrole= _mapper.Map(model, role);
                if (await _roleManager.RoleExistsAsync(newrole.Name!))
                {
                    return BadRequest("this name role is already exist.");
                }
                var result=await _roleManager.UpdateAsync(newrole);

                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                return View(model);
            }
            return View(model);
        }
        public async Task<IActionResult> Delete(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null) return NotFound();
            var result = _mapper.Map<RoleViewModel>(role);
            return View(result);
        }

        [HttpPost,ActionName("Delete")]
        public async Task<IActionResult> Deleteconfirme(string id)
        {

                var role = await _roleManager.FindByIdAsync(id);
                if (role == null) return NotFound();
              
                var result = await _roleManager.DeleteAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                return View();
           
        }


        [HttpGet]
        public async Task<IActionResult> ManageUserinRole(string RoleId)
        {

            var role = await _roleManager.FindByIdAsync(RoleId);
            if (role == null) return NotFound();

            var Users =  _userManager.Users.ToList();

            var ManageUsersList=new List<ManageUserinRole>();
            foreach (var user in Users) {
                var ManagedUser = _mapper.Map<ManageUserinRole>(user);
                var IsUserinRole = await _userManager.IsInRoleAsync(user,role.Name!);
                ManagedUser.IsSelected = IsUserinRole;
                ManageUsersList.Add(ManagedUser);


            }
            ViewBag.RoleId = RoleId;
            return View(ManageUsersList);

        }


        [HttpPost]
        public async Task<IActionResult> ManageUserinRole(List<ManageUserinRole> manages,string roleid)
        {

            var role = await _roleManager.FindByIdAsync(roleid);
            if (role == null) return NotFound();

            
            foreach (var model in manages)
            {
                var User =await  _userManager.FindByIdAsync(model.UserId);
                if (User == null) return NotFound();
                if(model.IsSelected&&!(await _userManager.IsInRoleAsync(User, role.Name!)))
                {
                    await _userManager.AddToRoleAsync(User, role.Name!);
                }
                else if (!model.IsSelected && (await _userManager.IsInRoleAsync(User, role.Name!)))
                {
                    await _userManager.RemoveFromRoleAsync(User, role.Name!);
                }
                else
                {
                    continue;
                }
                


            }
            return RedirectToAction(nameof(Index));

        }
    }
}

