using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Gather_Requirement_Project.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.Net.Http.Headers;
using System.Text;
using Gather_Requirement_Project.Models.SQLScript;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Pluralize.NET.Core;



namespace Gather_Requirement_Project.Controllers
{
    [Authorize]

    public class ModulesController : Controller
    {
        public IConfiguration Configuration { get; }

        Context context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public ModulesController(Context _context, IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            context = _context;
            Configuration = configuration;
            _hostingEnvironment = hostingEnvironment;

        }


        public ActionResult Index(int? programId)
        {
            IQueryable<Module> module = context.Modules.Include(s => s.CustomerProgram);

            if (programId != null)
                module = module.Where(s => s.CustomerProgramID == programId);

            ViewBag.ProID = (programId);

            return View(module.ToList());
        }

        public ActionResult Details(int id)
        {
            Module moduleslist = context.Modules.FirstOrDefault(s => s.ID == id);

            IEnumerable<Screen> ScreenList = context.Screens.ToList();
            ViewBag.screenList = ScreenList;

            return View(moduleslist);
        }

        public ActionResult Create(int? programId)
        {
            IEnumerable<CustomerProgram> customerPrograms = context.CustomerPrograms.ToList();
            ViewBag.programList = customerPrograms;

            ViewBag.ProID = programId;


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Module collection)
        {
            try
            {
                context.Modules.Add(collection);
                context.SaveChanges();

                return RedirectToAction(nameof(Index), new { programId = collection.CustomerProgramID });
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            IEnumerable<CustomerProgram> customerPrograms = context.CustomerPrograms.ToList();
            ViewBag.programList = customerPrograms;

            Module moduleEdit = context.Modules.FirstOrDefault(s => s.ID == id);
            return View(moduleEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Module collection)
        {
            try
            {
                context.Entry(collection).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction(nameof(Index), new { programId = collection.CustomerProgramID });
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            Module module = context.Modules.FirstOrDefault(s => s.ID == id);

            context.Modules.Remove(module);
            context.SaveChanges();
            return RedirectToAction(nameof(Index), new { programId = module.CustomerProgramID });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }







        private string GenerateSQLtext(IEnumerable<Screen> Screens, Module ModuleObject)
        {
            string fill;
            string[] s1 = new string[100];
            string[] s2 = new string[100];
            int i = 0;

            Queries queries = new Queries();
            fill = queries.DB(ModuleObject.EnglishNameInSingle);


            foreach (var Screen in Screens)
            {
                ScreenType screenType = context.ScreenTypes.FirstOrDefault(s => s.ID == Screen.screenTypeID);
                if (screenType.Name.ToLower() == "create")
                {
                    IEnumerable<ScreenComponent> ScreenComponents = context.ScreenComponent.Where(s => s.ScreenID == Screen.ID);
                    //if(ScreenComponents.Count() != 0)
                    //{
                    s1 = new string[ScreenComponents.Count()];
                    s2 = new string[ScreenComponents.Count()];
                    i = 0;

                    foreach (var ScreenComponent in ScreenComponents)
                    {

                        FieldType inputType = context.FieldTypes.FirstOrDefault(s => s.ID == ScreenComponent.FieldTypeID);
                        string name = inputType.Name;
                        if (name == "button")
                            continue;
                        s1[i] = ScreenComponent.FieldNameEnglish;


                        switch (name.ToLower())
                        {
                            case "dropdown": name = "int"; break;
                            case "number": name = "nvarchar(255)"; break;
                            case "text": name = "nvarchar(255)"; break;
                            case "email": name = "nvarchar(255)"; break;
                            case "radibutton": name = "nvarchar(255)"; break;
                            case "textarea": name = "nvarchar(255)"; break;
                            case "file": name = "nvarchar(255)"; break;
                            case "checkbox": name = "boolean"; break;
                            case "date": name = "datetime"; break;
                            default: name = ""; break;
                        }

                        s2[i] = name;

                        i++;
                    }

                    fill += queries.CreateTable(Screen.ScreenName, s1, s2);
                    //}
                }
            }
            return fill;
        }
        public FileStreamResult GenerateSQL(int? id)
        {
            Module ModuleObject = context.Modules.FirstOrDefault(s => s.ID == id);
            IEnumerable<Screen> Screens = context.Screens.Where(s => s.ModuleID == ModuleObject.ID);
            string ss = "";
            if (Screens.Count() != 0)
                ss = GenerateSQLtext(Screens, ModuleObject);

            var stream = new MemoryStream(Encoding.UTF8.GetBytes(ss));
            return new FileStreamResult(stream, new MediaTypeHeaderValue("text/plain"))
            {
                FileDownloadName = ModuleObject.EnglishNameInSingle + ".txt"
            };
        }
        public FileStreamResult GenerateSQLAll()
        {
            IEnumerable<Module> ModuleObjects = context.Modules.ToList();
            string str = "";
            foreach (var module in ModuleObjects)
            {
                IEnumerable<Screen> Screens = context.Screens.Where(s => s.ModuleID == module.ID);
                if (Screens != null)
                    str += GenerateSQLtext(Screens, module);
                str += "\n\n\n";
            }

            var stream = new MemoryStream(Encoding.UTF8.GetBytes(str));
            return new FileStreamResult(stream, new MediaTypeHeaderValue("text/plain"))
            {
                FileDownloadName = "DBScrpit" + ".txt"
            };
        }
        public async Task<IActionResult> Backup(string DBname = "")
        {
            string DBname1 = Configuration.GetSection("Data").GetSection("DBName").Value; ;
            string queryString1 = "BACKUP DATABASE [" + DBname1 + "] TO DISK = '" + Configuration.GetSection("Data").GetSection("Path1").Value + "\\" + DBname1 + ".bak';";
            string connectionString = Configuration.GetConnectionString("SqlConnection");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString1, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
            }

            string filename = DBname1 + ".bak";
            if (filename == null)
                return Content("filename not present");

            var path = Path.Combine(Configuration.GetSection("Data").GetSection("Path1").Value, filename);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, "text/plain", Path.GetFileName(path));

        }


        // generate Lookup
        private string LowercaseFirst(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            char[] a = s.ToCharArray();
            a[0] = char.ToLower(a[0]);
            return new string(a);
        }
        private string LookupModelFile(string name)
        {
            string file = @"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LookUpTable.Models
{
    public class Pattern1
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
";
            return file.Replace("Pattern1", name);
        }
        private string LookupControlFile(string name)
        {
            string file = @"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LookUpTable.DAL;
using LookUpTable.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LookUpTable.Controllers
{
    [Route(""api /[controller]"")]
     [ApiController]
    public class Pattern1Controller : ControllerBase
        {
            private readonly LookupDbContext _context;
            public Pattern1Controller(LookupDbContext context)
            {
                _context = context;
            }
            [HttpGet]
            public async Task<IEnumerable<Pattern1>> GetAll()
            {
                return await _context.Pattern2.ToListAsync();
            }
            [HttpGet(""{Id}"")]
            public async Task<Pattern1> GetById([FromRoute] int Id)
            {
                return await _context.Pattern2.FirstOrDefaultAsync(a => a.Id == Id);
            }
            [HttpPost]
            public async Task<IActionResult> Create([FromBody] Pattern1 Pattern3)
            {
                await _context.AddAsync(Pattern3);
                var res = await _context.SaveChangesAsync();
                if (res > 0)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            [HttpPut]
            public async Task<IActionResult> Update([FromBody] Pattern1 Pattern3)
            {
                _context.Entry(Pattern3).State = EntityState.Modified;
                var res = await _context.SaveChangesAsync();
                if (res > 0)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
        }
    }
";
            return file.Replace("Pattern1", name).Replace("Pattern2", new Pluralizer().Pluralize(name)).Replace("Pattern3", LowercaseFirst(name));
        }
        public string ContextTXT(List<string> SCs)
        {
            //public DbSet<CentralAdministration> CentralAdministrations { get; set; }

            StringBuilder report = new StringBuilder("");


            foreach (var SC in SCs)
            {
                report.Append("public DbSet<");
                report.Append(SC);
                report.Append("> ");
                var plural = new Pluralizer().Pluralize(SC);
                report.Append(plural);
                //report.Append(SC + "s");
                report.Append(" { get; set; }");
                report.Append("\n");

            }

            return report.ToString();
        }
        public string FileArabic(List<string> SCs, List<string> SCsArabic)
        {
            StringBuilder report = new StringBuilder("");
            int i = 0;
            foreach (var SCArabic in SCsArabic)
            {
                report.Append(SCs.ElementAt(i));
                report.Append("\t");
                report.Append(SCsArabic.ElementAt(i));
                report.Append("\n");
                i++;
            }

            return report.ToString();
        }
        private void WriteInFile(string subPath, string fileName, string text)
        {
            string realPath = _hostingEnvironment.ContentRootPath + "\\wwwroot\\FolderData";
            realPath = Path.Combine(realPath, subPath);

            fileName = fileName + ".cs";
            fileName = fileName.Replace("\\", "_").Replace("/", "_");

            if (!(Directory.Exists(realPath)))
                Directory.CreateDirectory(realPath);

            if (fileName != "")
            {
                realPath = Path.Combine(realPath, fileName);
                System.IO.File.WriteAllText(realPath, text);
            }

        }
        private void WriteAllLookup(int id)
        {
            Module module = context.Modules.FirstOrDefault(s => s.ID == id);
            IEnumerable<Screen> Screens = context.Screens.Where(s => s.ModuleID == module.ID);
            List<string> SCs = new List<string>();
            List<string> SCsArabic = new List<string>();

            foreach (var sre in Screens)
            {
                IEnumerable<ScreenComponent> ss = context.ScreenComponent.Where(s => s.ScreenID == sre.ID && s.FieldTypeID == 1);
                foreach (var s in ss)
                {
                    if (!(SCs.Contains(s.FieldNameEnglish)))
                    {
                        SCs.Add(s.FieldNameEnglish);
                        SCsArabic.Add(s.FielNameArabic);
                    }
                }
            }

            foreach (var SC in SCs)
            {
                WriteInFile("Models", SC, LookupModelFile(SC));
                WriteInFile("Controllers", SC, LookupControlFile(SC));
            }
            WriteInFile("Context", "Context.txt", ContextTXT(SCs));
            WriteInFile("LookupArabic", "LookupArabic.txt", FileArabic(SCs, SCsArabic));



        }
        public IActionResult CreateZipFile2(int id)
        {
            WriteAllLookup(id);
            Module module = context.Modules.FirstOrDefault(s => s.ID == id);

            string FolderPath = _hostingEnvironment.ContentRootPath + "\\wwwroot\\FolderData";
            string ZipFilePath = _hostingEnvironment.ContentRootPath + "\\wwwroot\\folder.zip";

            System.IO.Compression.ZipFile.CreateFromDirectory(FolderPath, ZipFilePath);

            byte[] finalResult = System.IO.File.ReadAllBytes(ZipFilePath);
            if (System.IO.File.Exists(ZipFilePath))
                System.IO.File.Delete(ZipFilePath);

            if (System.IO.Directory.Exists(FolderPath))
                System.IO.Directory.Delete(FolderPath, true);

            if (finalResult == null || !finalResult.Any())
                throw new Exception(String.Format("No Files found with Image"));

            return File(finalResult, "application/zip", "LookupsFor_" + module.EnglishNameInSingle.Replace(" ", "") + ".zip");


        }




        public ActionResult CopyModule(int id)
        {
            //-----------------------------Module---------------------------------------------------------------
            //CustomerProgram cp = context.CustomerPrograms.Last();
            Module module = context.Modules.FirstOrDefault(s => s.ID == id);
            Module mod = new Module();
            mod.EnglishNameInSingle = module.EnglishNameInSingle;
            mod.EnglishNameInPlural = module.EnglishNameInPlural;
            mod.ArabicNameInSingle = module.ArabicNameInSingle;
            mod.ArabicNameInPlural = module.ArabicNameInPlural;
            mod.URLForService = module.URLForService;
            mod.CustomerProgram = module.CustomerProgram;
            mod.CustomerProgramID = module.CustomerProgramID;

            context.Entry<Module>(mod).State = EntityState.Added;

            //---------------------------Screen---------------------------------------------------------------
            Module m = mod;
            IEnumerable<Screen> Screens = context.Screens.Where(s => s.ModuleID == module.ID);
            foreach (var screen in Screens)
            {
                Screen scr = new Screen();
                //scr.Date = screen.Date;
                //scr.DevName = screen.DevName;
                scr.EmpJob = screen.EmpJob;
                scr.EmpName = screen.EmpName;
                //scr.ScreenComponents = screen.ScreenComponents;
                scr.ScreenName = screen.ScreenName;
                scr.ScreenNameArabic = screen.ScreenNameArabic;
                scr.screenTypeID = screen.screenTypeID;
                scr.ScreenTypes = screen.ScreenTypes;
                scr.UserStories = screen.UserStories;
                //scr.ImageFile = screen.ImageFile;
                //scr.ImagePathPhysical = screen.ImagePathPhysical;
                //scr.ImageName = screen.ImageName;
                scr.Module = m;
                scr.ModuleID = m.ID;

                context.Entry<Screen>(scr).State = EntityState.Added;


                //---------------------------UserStories---------------------------------------------------------------
                Screen Scr = scr;
                IEnumerable<UserStories> userStories = context.UserStories.Where(s => s.ScreenID == screen.ID);
                foreach (var item in userStories)
                {
                    UserStories us = new UserStories();
                    us.Requirement = item.Requirement;
                    us.UserStory = item.UserStory;
                    us.Screen = Scr;
                    us.ScreenID = Scr.ID;

                    context.Entry<UserStories>(us).State = EntityState.Added;
                }



                //---------------------------ScreenComponent---------------------------------------------------------------
                IEnumerable<ScreenComponent> ScreenComponents = context.ScreenComponent.Where(s => s.ScreenID == screen.ID);
                foreach (var item in ScreenComponents)
                {
                    ScreenComponent screenComponent = new ScreenComponent();
                    screenComponent.DefaultValue = item.DefaultValue;
                    screenComponent.FieldNameEnglish = item.FieldNameEnglish;
                    screenComponent.FieldType = item.FieldType;
                    screenComponent.FieldTypeID = item.FieldTypeID;
                    screenComponent.FielNameArabic = item.FielNameArabic;
                    screenComponent.InputType = item.InputType;
                    screenComponent.InputtypeID = item.InputtypeID;
                    screenComponent.Readonly = item.Readonly;
                    screenComponent.ScreenComponentID = item.ScreenComponentID;
                    screenComponent.ServiceNameForDropdown = item.ServiceNameForDropdown;
                    screenComponent.ValidationEqualID = item.ValidationEqualID;
                    screenComponent.ValidationEqualMessage = item.ValidationEqualMessage;
                    screenComponent.ValidationGreaterthanID = item.ValidationGreaterthanID;
                    screenComponent.ValidationGreaterthanMessage = item.ValidationGreaterthanMessage;
                    screenComponent.ValidationLessthanID = item.ValidationLessthanID;
                    screenComponent.ValidationLessthanMessage = item.ValidationLessthanMessage;
                    screenComponent.ValidationMax = item.ValidationMax;
                    screenComponent.ValidationMaxMessage = item.ValidationMaxMessage;
                    screenComponent.ValidationMin = item.ValidationMin;
                    screenComponent.ValidationMinMessage = item.ValidationMinMessage;
                    screenComponent.ValidationRequired = item.ValidationRequired;
                    screenComponent.ValidationRequiredMessage = item.ValidationRequiredMessage;
                    screenComponent.Values = item.Values;
                    screenComponent.Visible = item.Visible;
                    screenComponent.Screen = Scr;
                    screenComponent.ScreenID = Scr.ID;

                    context.Entry<ScreenComponent>(screenComponent).State = EntityState.Added;
                }
            }
            context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }




    }
}
