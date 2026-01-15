using Microsoft.AspNetCore.Identity;
using UfsConnectBook.Models.Entities;

namespace UfsConnectBook.Data
{
    public class SeedDataIdentity
    {
        private const string FacilityAdminUser = "Admin";
        private const string FacilityAdminPassword = "&0137Deon.XliA";
        private const string FacilittyAdminEmail = "lxnyambe@ufs.ac.za";
        private const string FacilityAdminRole = "Admin";

        private const string FacilityManagerUser = "Manager";
        private const string FacilityManagerPassword = "&0137Deon.XliM";
        private const string FacilittyManagerEmail = "pngwamba@ufs.ac.za";
        private const string FacilityManagerRole = "Manager";

        private const string UserInCahrgeUser = "Incharge";
        private const string UserInChargePassword = "&0137Deon.XliU";
        private const string UserInChargeEmail = "pdeon@ufs.ac.za";
        private const string UserInChargeRole = "Incharge";

        private const string UserInCahrgeUserG = "InchargeG";
        private const string UserInChargePasswordG = "&0137Deon.XliUG";
        private const string UserInChargeEmailG = "pdeonG@ufs.ac.za";
        private const string UserInChargeRoleG = "InchargeG";

        private const string UserInCahrgeUserP = "InchargeP";
        private const string UserInChargePasswordP = "&0137Deon.XliUP";
        private const string UserInChargeEmailP = "pdeonP@ufs.ac.za";
        private const string UserInChargeRoleP = "InchargeP";

        private const string UserInCahrgeUserL = "InchargeL";
        private const string UserInChargePasswordL = "&0137Deon.XliUL";
        private const string UserInChargeEmailL = "pdeonL@ufs.ac.za";
        private const string UserInChargeRoleL = "InchargeL";

        private const string UserInCahrgeUserS = "InchargeS";
        private const string UserInChargePasswordS = "&0137Deon.XliUS";
        private const string UserInChargeEmailS = "pdeonS@ufs.ac.za";
        private const string UserInChargeRoleS = "InchargeS";

        public static async void EnsurePopulated(IApplicationBuilder app)
        {
            AppDbContext context = app.ApplicationServices
                .CreateScope().ServiceProvider

                .GetRequiredService<AppDbContext>();
            UserManager<AppUser> userManager = app.ApplicationServices
                .CreateScope().ServiceProvider
                .GetRequiredService<UserManager<AppUser>>();

            RoleManager<IdentityRole> roleManager = app.ApplicationServices
                .CreateScope().ServiceProvider
                .GetRequiredService<RoleManager<IdentityRole>>();

            //Seeding Roles
            var role = "Admin";
            if (!roleManager.RoleExistsAsync(role).Result)
                await roleManager.CreateAsync(new IdentityRole(role));
            role = "Incharge";
            if (!roleManager.RoleExistsAsync(role).Result)
                await roleManager.CreateAsync(new IdentityRole(role));
            role = "Manager";
            if (!roleManager.RoleExistsAsync(role).Result)
                await roleManager.CreateAsync(new IdentityRole(role));
            role = "AppUser";
            if (!roleManager.RoleExistsAsync(role).Result)
                await roleManager.CreateAsync(new IdentityRole(role));
            role = "InchargeG";
            if (!roleManager.RoleExistsAsync(role).Result)
                await roleManager.CreateAsync(new IdentityRole(role));
            role = "InchargeP";
            if (!roleManager.RoleExistsAsync(role).Result)
                await roleManager.CreateAsync(new IdentityRole(role));
            role = "InchargeL";
            if (!roleManager.RoleExistsAsync(role).Result)
                await roleManager.CreateAsync(new IdentityRole(role));
            role = "InchargeS";
            if (!roleManager.RoleExistsAsync(role).Result)
                await roleManager.CreateAsync(new IdentityRole(role));

            //Seeding Default users


            ////
            if (await userManager.FindByEmailAsync(UserInChargeEmailG) == null)
            {
                if (await roleManager.FindByNameAsync(UserInChargeRoleG) == null)
                {
                    await roleManager.CreateAsync(new IdentityRole(UserInChargeRoleG));
                }

                var user = new AppUser
                {
                    UserName = UserInCahrgeUserG,
                    Email = UserInChargeEmailG,
                    Identity = Guid.NewGuid().ToString(),
                    Name = "Sifundo",
                    Surname = "Dube"
                };

                IdentityResult result = await userManager.CreateAsync(user, UserInChargePasswordG);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, UserInChargeRoleG);
                }

            }
            ////
            if (await userManager.FindByEmailAsync(UserInChargeEmailL) == null)
            {
                if (await roleManager.FindByNameAsync(UserInChargeRoleL) == null)
                {
                    await roleManager.CreateAsync(new IdentityRole(UserInChargeRoleL));
                }

                var user = new AppUser
                {
                    UserName = UserInCahrgeUserL,
                    Email = UserInChargeEmailL,
                    Identity = Guid.NewGuid().ToString(),
                    Name = "Ncedile",
                    Surname = "Singwane"
                };

                IdentityResult result = await userManager.CreateAsync(user, UserInChargePasswordL);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, UserInChargeRoleL);
                }

            }
            ////
            if (await userManager.FindByEmailAsync(UserInChargeEmailP) == null)
            {
                if (await roleManager.FindByNameAsync(UserInChargeRoleP) == null)
                {
                    await roleManager.CreateAsync(new IdentityRole(UserInChargeRoleP));
                }

                var user = new AppUser
                {
                    UserName = UserInCahrgeUserP,
                    Email = UserInChargeEmailP,
                    Identity = Guid.NewGuid().ToString(),
                    Name = "Mbali",
                    Surname = "Twala"
                };

                IdentityResult result = await userManager.CreateAsync(user, UserInChargePasswordP);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, UserInChargeRoleP);
                }
            }
            ////
            if (await userManager.FindByEmailAsync(UserInChargeEmailS) == null)
            {
                if (await roleManager.FindByNameAsync(UserInChargeRoleS) == null)
                {
                    await roleManager.CreateAsync(new IdentityRole(UserInChargeRoleS));
                }

                var user = new AppUser
                {
                    UserName = UserInCahrgeUserS,
                    Email = UserInChargeEmailS,
                    Identity = Guid.NewGuid().ToString(),
                    Name = "Siphosethu",
                    Surname = "Nyambe"
                };

                IdentityResult result = await userManager.CreateAsync(user, UserInChargePasswordS);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, UserInChargeRoleS);
                }
            }
            ////


            if (await userManager.FindByEmailAsync(FacilittyAdminEmail) == null)
            {
                if (await roleManager.FindByNameAsync(FacilityAdminRole) == null)
                {
                    await roleManager.CreateAsync(new IdentityRole(FacilityAdminRole));
                }

                var user = new AppUser
                {
                    UserName = FacilityAdminUser,
                    Email = FacilittyAdminEmail,
                    Identity = Guid.NewGuid().ToString(),
                    Name = "Lindele",
                    Surname = "Nyambe"
                };

                IdentityResult result = await userManager.CreateAsync(user, FacilityAdminPassword);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, FacilityAdminRole);
                }

            }
            ////
            if (await userManager.FindByEmailAsync(UserInChargeEmailS) == null)
            {
                if (await roleManager.FindByNameAsync(UserInChargeRoleS) == null)
                {
                    await roleManager.CreateAsync(new IdentityRole(UserInChargeRoleS));
                }

                var user = new AppUser
                {
                    UserName = UserInCahrgeUserS,
                    Email = UserInChargeEmailS,
                    Identity = Guid.NewGuid().ToString(),
                    Name = "Mbali",
                    Surname = "Twala"
                };

                IdentityResult result = await userManager.CreateAsync(user, UserInChargePasswordS);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, UserInChargeRoleS);
                }
            }
            ////

            if (await userManager.FindByEmailAsync(UserInChargeEmail) == null)
            {
                if (await roleManager.FindByNameAsync(UserInChargeRole) == null)
                {
                    await roleManager.CreateAsync(new IdentityRole(UserInChargeRole));
                }

                var user = new AppUser
                {
                    UserName = UserInCahrgeUser,
                    Email = UserInChargeEmail,
                    Identity = Guid.NewGuid().ToString(),
                    Name = "Philasande",
                    Surname = "Nyambe",

                };

                IdentityResult result = await userManager.CreateAsync(user, UserInChargePassword);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, UserInChargeRole);
                }

            }
            if (await userManager.FindByEmailAsync(FacilittyManagerEmail) == null)
            {
                if (await roleManager.FindByNameAsync(FacilityManagerRole) == null)
                {
                    await roleManager.CreateAsync(new IdentityRole(FacilityManagerRole));
                }

                var user = new AppUser
                {
                    UserName = FacilityManagerRole,
                    Email = FacilittyManagerEmail,
                    Identity = Guid.NewGuid().ToString(),
                    Name = "Phumzile",
                    Surname = "Ngwamba"

                };

                IdentityResult result = await userManager.CreateAsync(user, FacilityManagerPassword);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, FacilityManagerRole);
                }

            }

            //Seeding Default Data
            if (!context.Category.Any())
            {
                context.Category.AddRange(
                    new Catagory { Name = "Gym", Price = 150M, Description = "Mandela Building Gyming Facility" },
                    new Catagory { Name = "Laundry", Price = 100M, Description = "UFS Laundry Services" },
                    new Catagory { Name = "Parking", Price = 50M, Description = "UFS Parking Services" },
                    new Catagory { Name = "Study", Price = 0M, Description = "UFS Studying Facilities" });
                context.SaveChanges();
            }
            if (!context.Facilities.Any())
            {
                context.Facilities.AddRange(new Facility[] {
                    new Facility { CategoryId = 1, Name = "Mandela Hall Gym" },
                    new Facility { CategoryId = 1, Name = "Steve Biko Gyming Hall"},
                    new Facility { CategoryId = 2, Name = "New Res Community Laungry"},
                    new Facility { CategoryId = 2, Name = "Res Block E Laungry"},
                    new Facility { CategoryId = 3, Name = "Parking Lot A - Reserved"},
                    new Facility { CategoryId = 3, Name = "Student Parking"},
                    new Facility { CategoryId = 4, Name = "Overnigt Study Hall"},
                    new Facility { CategoryId = 4, Name = "Computer Science overnight Lab"},
                    new Facility { CategoryId = 4, Name = "New Lab"},
                    new Facility { CategoryId = 4, Name = "Media Lab (Old Lab)"}

                });
                context.SaveChanges();


            }

        }
    }
}
