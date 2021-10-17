using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Services;
using entity_fr.Mail;
using entity_fr.models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace entity_fr
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            //Đk dịch vụ email 
            services.AddOptions();
            var mailSettings = Configuration.GetSection("MailSettings");
            services.Configure<MailSettings>(mailSettings);

            services.AddSingleton<IEmailSender, SendMailService>();
            services.AddRazorPages();
            services.AddDbContext<MyBlogContext>(options =>
            {
                // Lay du lieu tu trong file appsettings.json
                string connectionString = Configuration.GetConnectionString("MyBlogContext");
                options.UseSqlServer(connectionString);
            });
            // Đk Identity 

            services.AddIdentity<AppUser, IdentityRole>()
            .AddEntityFrameworkStores<MyBlogContext>()
            .AddDefaultTokenProviders();
            //  services.AddDefaultIdentity<AppUser>()
            // .AddEntityFrameworkStores<MyBlogContext>()
            // .AddDefaultTokenProviders();

            // Truy cập IdentityOptions
            services.Configure<IdentityOptions>(options =>
            {
                // Thiết lập về Password
                options.Password.RequireDigit = false; // Không bắt phải có số
                options.Password.RequireLowercase = false; // Không bắt phải có chữ thường
                options.Password.RequireNonAlphanumeric = false; // Không bắt ký tự đặc biệt
                options.Password.RequireUppercase = false; // Không bắt buộc chữ in
                options.Password.RequiredLength = 3; // Số ký tự tối thiểu của password
                options.Password.RequiredUniqueChars = 1; // Số ký tự riêng biệt

                // Cấu hình Lockout - khóa user
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // Khóa 5 phút
                options.Lockout.MaxFailedAccessAttempts = 5; // Thất bại 5 lầ thì khóa
                options.Lockout.AllowedForNewUsers = true;

                // Cấu hình về User.
                options.User.AllowedUserNameCharacters = // các ký tự đặt tên user
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;  // Email là duy nhất

                // Cấu hình đăng nhập.
                options.SignIn.RequireConfirmedEmail = true;            // Cấu hình xác thực địa chỉ email (email phải tồn tại)
                options.SignIn.RequireConfirmedPhoneNumber = false;     // Xác thực số điện thoại
                options.SignIn.RequireConfirmedAccount = true;

            });
            services.ConfigureApplicationCookie(option =>
            {
                option.LoginPath = "/login";
                option.LogoutPath = "/logout";
                option.AccessDeniedPath = "/accessdenied.html";
            });
            services.AddAuthentication()
                    .AddGoogle(option =>
                   {
                       var gconfig = Configuration.GetSection("Authentication:Google");
                       option.ClientId = gconfig["ClientId"];
                       option.ClientSecret = gconfig["ClientSecret"];
                       //https://localhost:5001/signin-google => địa chỉ mặc định
                       option.CallbackPath = "/dang-nhap-tu-google";
                   }).AddFacebook(facebookOptions =>
                   {
                       // Đọc cấu hình
                       IConfigurationSection facebookAuthNSection = Configuration.GetSection("Authentication:Facebook");
                       facebookOptions.AppId = facebookAuthNSection["AppId"];
                       facebookOptions.AppSecret = facebookAuthNSection["AppSecret"];
                       // Thiết lập đường dẫn Facebook chuyển hướng đến
                       facebookOptions.CallbackPath = "/dang-nhap-tu-facebook";
                   })
                    // .AddMicrosoftAccount()
                    // .AddFacebook()
                    // .AddTwitter()
                    ;
            services.AddSingleton<IdentityErrorDescriber, AppIdentityErrorDescriber>();
            services.Configure<SecurityStampValidatorOptions>(options =>
            {
            // Trên 30 giây truy cập lại sẽ nạp lại thông tin User (Role)
            // SecurityStamp trong bảng User đổi -> nạp lại thông tinn Security
            options.ValidationInterval = TimeSpan.FromSeconds(30);});
            services.AddAuthorization(option =>{
                option.AddPolicy("AllowEditRole",policyBuilder=>
                {
                    //Điều kiện của policyBuilder
                    policyBuilder.RequireAuthenticatedUser();
                    // policyBuilder.RequireRole("Admin"); 
                    // policyBuilder.RequireRole("Editor");

                    policyBuilder.RequireClaim("allowEdit","role");

                    // policyBuilder.RequireClaim("TenClaim",new string[]{"giatri1","giatri2"})
                    // IdentityRoleClaim<string> claim1; 
                    // IdentityUserClaim<string> claim2;
                    // Claim claim;  
                });
            }); 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });

        }
    }
}

/*
    CREATE , READ, UPDATE , DELETE (CRUD)

    dotnet aspnet-codegenerator razorpage -m entity_fr.models.Article -dc entity_fr.models.MyBlogContext -outDir Pages/Blog -udl --referenceScriptLibraries

    Identity : (Bài giảng 97)
    - Authentication : Xác định danh tính -> Login , logout 
    -> Phục hồi thông tin đã đăng nhập , đã xác thực 
    - Authorization : Xác thực quyền truy cập (các chức năng) 
      *  Role-based authorization - xác thực quyền theo vai trò
        -Role(vai trò): (Admin, editor, manager,member..)
        /Areas/Admin/Pages/Role
        Index
        Create  
        Edit                        
        Delete 
         => dotnet new page -n Index -o Areas/Admin/Pages/Role -na App.Admin.Role
        [Authorize]  - Controller , Action , PageModel -> đăng nhập
      *  Policy-based authorization : xác thực quyền theo chính sách
        - Policy : tạo ra các policy mà khi vào cần đảm bảo đáp ứng các chính sách đặt ra
        Vd: [Authorize(Policy="AllowEditRole")]
      *  Claims-based authorization : xác thực theo đặc tính , tính chất của user
        - Claims : đựac tính ,tính chất của một đối tượng (user)

        - Bằng lái xe B2(Role) -> đực lái xe 4 chỗ
          + Ngày sinh ->claim
          + Nơi sinh -> claim
        - Mua rượu ( > 18 t) -> kiểm tra ngày sinh -> Claims-based authoziration 

    - Quản lý user : Sign up , Sign in , Role
    /Identity/Account/Login 
    /Identity/Account/Manage

    dotnet aspnet-codegenerator identity -dc entity_fr.models.MyBlogContext

    CallbackPath : 
    https://localhost:5001/dang-nhap-tu-google
*/