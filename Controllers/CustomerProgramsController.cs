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
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using System.IO.Compression;
using Microsoft.EntityFrameworkCore;
using Gather_Requirement_Project.Models.NotificationClasses;
using Newtonsoft.Json;
using Microsoft.Office.Interop.Word;
using Microsoft.Extensions.Hosting;
using ICSharpCode.SharpZipLib.Zip;
using Microsoft.AspNetCore.Hosting.Server;
using Pluralize.NET.Core;
using Section = Gather_Requirement_Project.Models.Section;

namespace Gather_Requirement_Project.Controllers
{
    [Authorize]

    public class CustomerProgramsController : Controller
    {
        public IConfiguration Configuration { get; }

        Context context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public CustomerProgramsController(Context _context, IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            context = _context;
            Configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }


        public ActionResult Index()
        {
            return View(context.CustomerPrograms.Include(s => s.Section).ToList());
        }

        public ActionResult Details(int id)
        {
            CustomerProgram customprogramslist = context.CustomerPrograms.Include(s => s.Section).FirstOrDefault(s => s.ID == id);

            IEnumerable<Module> ModuleList = context.Modules.ToList();
            ViewBag.screenList = ModuleList;

            return View(customprogramslist);
        }

        public ActionResult Create()
        {
            ViewBag.portalsList = context.Section.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerProgram collection)
        {
            try
            {
                context.CustomerPrograms.Add(collection);
                context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            ViewBag.portalsList = context.Section.ToList();

            CustomerProgram moduleEdit = context.CustomerPrograms.FirstOrDefault(s => s.ID == id);
            return View(moduleEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CustomerProgram collection)
        {
            try
            {
                context.Entry(collection).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            CustomerProgram customerProgram = context.CustomerPrograms.FirstOrDefault(s => s.ID == id);

            context.CustomerPrograms.Remove(customerProgram);
            context.SaveChanges();
            return RedirectToAction("Index");
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






        public IActionResult GenerateJsonFrontEnd(int id)
        {
            CustomerProgram CP = context.CustomerPrograms.FirstOrDefault(s => s.ID == id);
            IEnumerable<Module> mods = context.Modules.Where(s => s.CustomerProgramID == CP.ID);


            ModuleFrontEnd moduleFrontEnd = new ModuleFrontEnd();
            moduleFrontEnd.id = CP.EnglishNameInSingle;
            moduleFrontEnd.title = CP.ArabicNameInSingle;

            children ch1 = new children(); ch1.id = CP.EnglishNameInSingle + " Program"; ch1.title = "البرامج"; ch1.icon = "apps";
            children ch2 = new children(); ch2.id = CP.EnglishNameInSingle + " Config"; ch2.title = "الأعدادات"; ch2.icon = "build";
            children ch3 = new children(); ch3.id = CP.EnglishNameInSingle + " Report"; ch3.title = "التقارير"; ch3.icon = "assignment";

            foreach (var mod in mods)
            {

                subchildren subch = new subchildren();
                subch.id = mod.EnglishNameInSingle;
                subch.title = mod.ArabicNameInSingle;
                subch.url = "/" + mod.EnglishNameInSingle.Replace(" ", "").ToLower() + "";

                ch1.subchildren.Add(subch);



                IEnumerable<Screen> scs = context.Screens.Where(s => s.ModuleID == mod.ID).Include(st => st.ScreenTypes);
                foreach (var sc in scs)
                {

                    if (sc.ScreenTypes.Name == "Setting")
                    {
                        subchildren subch1 = new subchildren();
                        subch1.id = sc.ScreenName;
                        subch1.title = sc.ScreenNameArabic;
                        subch1.url = "/#";

                        ch2.subchildren.Add(subch1);
                    }

                    else if (sc.ScreenTypes.Name == "Report")
                    {
                        subchildren subch2 = new subchildren();
                        subch2.id = sc.ScreenName;
                        subch2.title = sc.ScreenNameArabic;
                        subch2.url = "/#";

                        ch3.subchildren.Add(subch2);
                    }
                }
            }


            moduleFrontEnd.children.Add(ch1);
            moduleFrontEnd.children.Add(ch2);
            moduleFrontEnd.children.Add(ch3);



            HeadFile headFile = new HeadFile();
            headFile.program.Add(moduleFrontEnd);

            string result = JsonConvert.SerializeObject(headFile);
            result = result.Replace("subchildren", "children").Replace("program", CP.EnglishNameInSingle);
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(result));
            return new FileStreamResult(stream, new MediaTypeHeaderValue("text/plain"))
            {
                FileDownloadName = "ModuleFrontEnd.json"
            };
        }

        //private string GenerateSQLtext(IEnumerable<Module> modules, CustomerProgram CustomerProgramObject)
        //{
        //    string fill;
        //    string[] s1 = new string[100];
        //    string[] s2 = new string[100];
        //    int i = 0;

        //    Queries queries = new Queries();
        //    fill = queries.DB(CustomerProgramObject.EnglishNameInSingle);


        //    foreach (var Module in modules)
        //    {
        //        ModuleType screenType = context.ModuleTypes.FirstOrDefault(s => s.ID == Module.screenTypeID);
        //        if (screenType.Name.ToLower() == "create")
        //        {
        //            IEnumerable<ModuleComponent> ModuleComponents = context.ModuleComponent.Where(s => s.ModuleID == Module.ID);
        //            //if(ModuleComponents.Count() != 0)
        //            //{
        //            s1 = new string[ModuleComponents.Count()];
        //            s2 = new string[ModuleComponents.Count()];
        //            i = 0;

        //            foreach (var ModuleComponent in ModuleComponents)
        //            {

        //                FieldType inputType = context.FieldTypes.FirstOrDefault(s => s.ID == ModuleComponent.FieldTypeID);
        //                string name = inputType.Name;
        //                if (name == "button")
        //                    continue;
        //                s1[i] = ModuleComponent.FieldNameEnglish;


        //                switch (name.ToLower())
        //                {
        //                    case "dropdown": name = "int"; break;
        //                    case "number": name = "nvarchar(255)"; break;
        //                    case "text": name = "nvarchar(255)"; break;
        //                    case "email": name = "nvarchar(255)"; break;
        //                    case "radibutton": name = "nvarchar(255)"; break;
        //                    case "textarea": name = "nvarchar(255)"; break;
        //                    case "file": name = "nvarchar(255)"; break;
        //                    case "checkbox": name = "boolean"; break;
        //                    case "date": name = "datetime"; break;
        //                    default: name = ""; break;
        //                }

        //                s2[i] = name;

        //                i++;
        //            }

        //            fill += queries.CreateTable(Module.ModuleName, s1, s2);
        //            //}
        //        }
        //    }
        //    return fill;
        //}

        //public FileStreamResult GenerateSQL(int? id)
        //{
        //    CustomerProgram CustomerProgramObject = context.CustomerPrograms.FirstOrDefault(s => s.ID == id);
        //    IEnumerable<Module> Modules = context.Modules.Where(s => s.CustomerProgramID == CustomerProgramObject.ID);
        //    string ss = "";
        //    if (Modules.Count() != 0)
        //        ss = GenerateSQLtext(Modules, CustomerProgramObject);

        //    var stream = new MemoryStream(Encoding.UTF8.GetBytes(ss));
        //    return new FileStreamResult(stream, new MediaTypeHeaderValue("text/plain"))
        //    {
        //        FileDownloadName = CustomerProgramObject.EnglishNameInSingle + ".txt"
        //    };
        //}

        //public FileStreamResult GenerateSQLAll()
        //{
        //    IEnumerable<CustomerProgram> CustomerProgramObjects = context.CustomerPrograms.ToList();
        //    string str = "";
        //    foreach (var customerProgram in CustomerProgramObjects)
        //    {
        //        IEnumerable<Module> modules = context.Modules.Where(s => s.CustomerProgramID == customerProgram.ID);
        //        if (modules != null)
        //            str += GenerateSQLtext(modules, customerProgram);
        //        str += "\n\n\n";
        //    }

        //    var stream = new MemoryStream(Encoding.UTF8.GetBytes(str));
        //    return new FileStreamResult(stream, new MediaTypeHeaderValue("text/plain"))
        //    {
        //        FileDownloadName = "DBScrpit" + ".txt"
        //    };
        //}

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

        public IActionResult ScriptAllProgram()
        {
            IEnumerable<CustomerProgram> cps = context.CustomerPrograms.ToList();
            StringBuilder report = new StringBuilder("");

            foreach (var cp in cps)
            {
                report.Append(cp.ArabicNameInSingle);
                report.Append("\n");

                IEnumerable<Module> mods = context.Modules.Where(s => s.CustomerProgramID == cp.ID);
                foreach (var mod in mods)
                {
                    report.Append("\t");
                    report.Append(mod.ArabicNameInSingle);
                    report.Append("\n");

                    IEnumerable<Screen> scrs = context.Screens.Where(s => s.ModuleID == mod.ID);
                    foreach (var scr in scrs)
                    {
                        report.Append("\t\t");
                        report.Append(scr.ScreenNameArabic);
                        report.Append("\n");

                        /*
                        IEnumerable<ScreenComponent> scrcoms = context.ScreenComponent.Where(s => s.ScreenComponentID == scr.ID);
                        foreach (var scrcom in scrcoms)
                        {
                            report.Append("\t\t\t");
                            report.Append(scrcom.FieldNameEnglish);
                            report.Append("\n");
                        }
                        */
                    }

                    report.Append("\n");
                }

                report.Append("\n");
            }



            var stream = new MemoryStream(Encoding.UTF8.GetBytes(report.ToString()));
            return new FileStreamResult(stream, new MediaTypeHeaderValue("text/plain"))
            {
                FileDownloadName = "ScriptAllProgram.txt"
            };

        }

        public IActionResult ScriptAllProgram2()
        {
            IEnumerable<Section> portals = context.Section.ToList();
            StringBuilder report = new StringBuilder("");
            foreach (var portal in portals)
            {
                report.Append(portal.ArabicName);
                report.Append("\n");


                IEnumerable<CustomerProgram> cps = context.CustomerPrograms.Where(s => s.SectionID == portal.ID);
                foreach (var cp in cps)
                {
                    report.Append("\t");
                    report.Append(cp.ArabicNameInSingle);
                    report.Append("\n");

                    IEnumerable<Module> mods = context.Modules.Where(s => s.CustomerProgramID == cp.ID);
                    foreach (var mod in mods)
                    {
                        report.Append("\t\t");
                        report.Append(mod.ArabicNameInSingle);
                        report.Append("\n");

                        IEnumerable<Screen> scrs = context.Screens.Where(s => s.ModuleID == mod.ID);
                        foreach (var scr in scrs)
                        {

                            report.Append("\t\t\t");
                            report.Append(scr.ScreenNameArabic);
                            report.Append("\n");
                            /*
                            IEnumerable<ScreenComponent> scrcoms = context.ScreenComponent.Where(s => s.ScreenComponentID == scr.ID);
                            foreach (var scrcom in scrcoms)
                            {
                                report.Append("\t\t\t");
                                report.Append(scrcom.FieldNameEnglish);
                                report.Append("\n");
                            }
                            */
                            IEnumerable<UserStories> userStories = context.UserStories.Where(s => s.ScreenID == scr.ID);
                            foreach (var userStory in userStories)
                            {

                                report.Append("\t\t\t\t");
                                report.Append(userStory.Requirement);
                                report.Append("\n");
                            }

                        }
                        //report.Append("\n");
                    }
                    //report.Append("\n");
                }
            }


            var stream = new MemoryStream(Encoding.UTF8.GetBytes(report.ToString()));
            return new FileStreamResult(stream, new MediaTypeHeaderValue("text/plain"))
            {
                FileDownloadName = "ScriptAllProgram.txt"
            };

        }

        public ActionResult CopyProgram(int id)
        {
            //---------------------------Program---------------------------------------------------------------
            CustomerProgram customerProgram1 = context.CustomerPrograms.Include(a => a.Modules).FirstOrDefault(s => s.ID == id);
            CustomerProgram customerProgram2 = new CustomerProgram();
            customerProgram2.EnglishNameInSingle = customerProgram1.EnglishNameInSingle;
            customerProgram2.EnglishNameInPlural = customerProgram1.EnglishNameInPlural;
            customerProgram2.ArabicNameInSingle = customerProgram1.ArabicNameInSingle;
            customerProgram2.ArabicNameInPlural = customerProgram1.ArabicNameInPlural;
            customerProgram2.URLForService = customerProgram1.URLForService;
            customerProgram2.DevName = customerProgram1.DevName;
            customerProgram2.Section = customerProgram1.Section;
            customerProgram2.SectionID = customerProgram1.SectionID;
            context.Entry<CustomerProgram>(customerProgram2).State = EntityState.Added;
            //CustomerProgram cp = context.CustomerPrograms.Add(customerProgram2);
            //context.SaveChanges();


            //-----------------------------Module---------------------------------------------------------------
            //CustomerProgram cp = context.CustomerPrograms.Last();
            //IEnumerable<Module> Modules = context.Modules.Where(s => s.CustomerProgramID == customerProgram1.ID);
            CustomerProgram cp = customerProgram2;
            IEnumerable<Module> Modules = customerProgram1.Modules;
            foreach (var module in Modules)
            {
                Module mod = new Module();
                mod.EnglishNameInSingle = module.EnglishNameInSingle;
                mod.EnglishNameInPlural = module.EnglishNameInPlural;
                mod.ArabicNameInSingle = module.ArabicNameInSingle;
                mod.ArabicNameInPlural = module.ArabicNameInPlural;
                mod.URLForService = module.URLForService;
                mod.CustomerProgram = cp;
                mod.CustomerProgramID = cp.ID;

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

            }
            context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public ActionResult UserstoryReport(int id)
        {
            IEnumerable<Section> portals = context.Section.ToList();
            StringBuilder report = new StringBuilder("");
            foreach (var portal in portals)
            {
                //report.Append(portal.ArabicName);
                //report.Append("\n");


                IEnumerable<CustomerProgram> cps = context.CustomerPrograms.Where(s => s.SectionID == portal.ID);
                foreach (var cp in cps)
                {
                    //report.Append("\t");
                    //report.Append(cp.ArabicNameInSingle);
                    //report.Append("\n");

                    IEnumerable<Module> mods = context.Modules.Where(s => s.CustomerProgramID == cp.ID);
                    foreach (var mod in mods)
                    {
                        //report.Append("\t\t");
                        //report.Append(mod.ArabicNameInSingle);
                        //report.Append("\n");

                        IEnumerable<Screen> scrs = context.Screens.Where(s => s.ModuleID == mod.ID);
                        foreach (var scr in scrs)
                        {

                            //report.Append("\t\t\t");
                            //report.Append(scr.ScreenNameArabic);
                            //report.Append("\n");


                            /*
                            IEnumerable<ScreenComponent> scrcoms = context.ScreenComponent.Where(s => s.ScreenComponentID == scr.ID);
                            foreach (var scrcom in scrcoms)
                            {
                                report.Append("\t\t\t");
                                report.Append(scrcom.FieldNameEnglish);
                                report.Append("\n");
                            }
                            */
                            /*
                            IEnumerable<UserStories> userStories = context.UserStories.Where(s => s.ScreenID == scr.ID);
                            if(userStories.Count() == 0 && !( report.ToString().Contains(cp.ArabicNameInSingle)))
                            {
                                report.Append(cp.ArabicNameInSingle);
                                report.Append("\n");
                            }
                            */
                            IEnumerable<UserStories> userStories = context.UserStories.Where(s => s.ScreenID == scr.ID);
                            if (userStories.Count() == 0)
                            {
                                report.Append(cp.ArabicNameInSingle);
                                report.Append("\t");
                                report.Append(mod.ArabicNameInSingle);
                                report.Append("\t");
                                report.Append(scr.ScreenNameArabic);
                                report.Append("\n");
                            }
                        }
                    }
                }
            }


            var stream = new MemoryStream(Encoding.UTF8.GetBytes(report.ToString()));
            return new FileStreamResult(stream, new MediaTypeHeaderValue("text/plain"))
            {
                FileDownloadName = "UserstoryReport.txt"
            };

        }

        public async Task<IActionResult> GenerateWord(int id)
        {
            IEnumerable<Section> portals = context.Section.ToList();
            StringBuilder report = new StringBuilder("");

            Microsoft.Office.Interop.Word.Application winword = new Microsoft.Office.Interop.Word.Application();
            winword.ShowAnimation = false;
            winword.Visible = false;
            object missing = System.Reflection.Missing.Value;

            Microsoft.Office.Interop.Word.Document document = winword.Documents.Add(ref missing, ref missing, ref missing, ref missing);
            document.Content.SetRange(0, 0);
            //document.Content.Text = "" + Environment.NewLine;



            //Add paragraph with Heading 2 style
            Microsoft.Office.Interop.Word.Paragraph para1 = document.Content.Paragraphs.Add(ref missing);
            para1.ReadingOrder = WdReadingOrder.wdReadingOrderRtl;
            para1.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
            object styleHeading1 = "Heading 1";
            para1.Range.set_Style(ref styleHeading1);
            para1.Range.Text = "Report";
            para1.Range.InsertParagraphAfter();
            //para1.ReadingOrder = WdReadingOrder.wdReadingOrderRtl;
            //para1.Alignment = WdParagraphAlignment.wdAlignParagraphRight;

            int xx = 0;
            foreach (var portal in portals)
            {
                report.Append(portal.ArabicName);
                report.Append("\n");


                IEnumerable<CustomerProgram> cps = context.CustomerPrograms.Where(s => s.SectionID == portal.ID);
                foreach (var cp in cps)
                {
                    report.Append("\t");
                    report.Append(cp.ArabicNameInSingle);
                    report.Append("\n");

                    IEnumerable<Module> mods = context.Modules.Where(s => s.CustomerProgramID == cp.ID);
                    foreach (var mod in mods)
                    {
                        report.Append("\t\t");
                        report.Append(mod.ArabicNameInSingle);
                        report.Append("\n");

                        IEnumerable<Screen> scrs = context.Screens.Where(s => s.ModuleID == mod.ID);
                        foreach (var scr in scrs)
                        {

                            report.Append("\t\t\t");
                            report.Append(scr.ScreenNameArabic);
                            report.Append("\n");
                            /*
                            IEnumerable<ScreenComponent> scrcoms = context.ScreenComponent.Where(s => s.ScreenComponentID == scr.ID);
                            foreach (var scrcom in scrcoms)
                            {
                                report.Append("\t\t\t");
                                report.Append(scrcom.FieldNameEnglish);
                                report.Append("\n");
                            }
                            */
                            IEnumerable<UserStories> userStories = context.UserStories.Where(s => s.ScreenID == scr.ID);
                            foreach (var userStory in userStories)
                            {

                                report.Append("\t\t\t\t");
                                report.Append(userStory.Requirement);
                                report.Append("\n");
                            }


                            //Add paragraph with Heading 2 style
                            Microsoft.Office.Interop.Word.Paragraph para2 = document.Content.Paragraphs.Add(ref missing);
                            para2.ReadingOrder = WdReadingOrder.wdReadingOrderRtl;
                            //para2.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
                            object styleHeading2 = "Heading 2";
                            para2.Range.set_Style(ref styleHeading2);
                            para2.Range.Font.ColorIndex = WdColorIndex.wdBlack;
                            para2.Range.Text = report.ToString();


                            //para2.ReadingOrder = WdReadingOrder.wdReadingOrderRtl;
                            //para2.Alignment = WdParagraphAlignment.wdAlignParagraphRight;

                            para2.Range.InsertParagraphAfter();


                            if (xx < 5)
                            {


                                IEnumerable<ScreenComponent> ScreenComponents = context.ScreenComponent.Where(s => s.ScreenID == scr.ID);
                                string[] marks = new string[] { "FieldnameE", "FieldnameA", "Fieldtype", "Validation", "Note" };
                                string[] arr = new string[ScreenComponents.Count() * 5];
                                int counter = 0;
                                int row = 1;
                                string val = "";
                                foreach (ScreenComponent ScreenComponent in ScreenComponents)
                                {
                                    val = "";
                                    row++;
                                    arr[counter++] = (ScreenComponent.FieldNameEnglish.ToString()) ?? " ";
                                    arr[counter++] = (ScreenComponent.FielNameArabic) as string ?? " ";

                                    FieldType fieldType = context.FieldTypes.FirstOrDefault(s => s.ID == ScreenComponent.FieldTypeID);
                                    arr[counter++] = (fieldType.Name.ToString()) ?? " ";

                                    if (ScreenComponent.ValidationRequiredMessage != null)
                                        val += "Required, ";
                                    if (ScreenComponent.ValidationMaxMessage != null)
                                        val += "Max(" + ScreenComponent.ValidationMax + "), ";
                                    if (ScreenComponent.ValidationMinMessage != null)
                                        val += "Min(" + ScreenComponent.ValidationMin + ").";

                                    arr[counter++] = val;
                                    arr[counter++] = " ";
                                }


                                Table firstTable = document.Tables.Add(para1.Range, row, 5);
                                firstTable.Borders.Enable = 1;


                                int counter2 = 0;
                                for (int i = 1; i <= row; i++)
                                {
                                    for (int x = 1; x <= 5; x++)
                                    {
                                        if (i == 1)
                                        {
                                            firstTable.Rows[i].Cells[x].Range.Text = marks[x - 1];
                                            firstTable.Rows[i].Cells[x].Range.Font.ColorIndex = WdColorIndex.wdGray25;
                                            firstTable.Rows[i].Cells[x].Range.Font.Bold = 1;
                                            firstTable.Rows[i].Cells[x].Range.Font.Name = "verdana";
                                            firstTable.Rows[i].Cells[x].Range.Font.Size = 10;
                                            firstTable.Rows[i].Cells[x].Shading.BackgroundPatternColor = WdColor.wdColorGreen;
                                            firstTable.Rows[i].Cells[x].VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                                            firstTable.Rows[i].Cells[x].Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                                        }

                                        else
                                        {
                                            if (x == 1)
                                                firstTable.Rows[i].Cells[x].Range.Text = arr[counter2++];
                                            if (x == 2)
                                                firstTable.Rows[i].Cells[x].Range.Text = arr[counter2++];
                                            if (x == 3)
                                                firstTable.Rows[i].Cells[x].Range.Text = arr[counter2++];
                                            if (x == 4)
                                                firstTable.Rows[i].Cells[x].Range.Text = arr[counter2++];
                                            if (x == 5)
                                                firstTable.Rows[i].Cells[x].Range.Text = arr[counter2++];
                                            firstTable.Rows[i].Cells[x].Range.Font.ColorIndex = WdColorIndex.wdBlack;
                                            firstTable.Rows[i].Cells[x].Range.Font.Bold = 1;
                                            firstTable.Rows[i].Cells[x].Range.Font.Name = "verdana";
                                            firstTable.Rows[i].Cells[x].Range.Font.Size = 10;
                                            //firstTable.Rows[i].Cells[x].Shading.BackgroundPatternColor = WdColor.wdColorGreen;
                                            firstTable.Rows[i].Cells[x].VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                                            firstTable.Rows[i].Cells[x].Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                                        }
                                    }
                                }

                                report.Clear();
                                report.Append("\n");
                                para2.Range.Text = report.ToString();
                                string imagePath = scr.ImagePathPhysical;
                                para2.Range.InlineShapes.AddPicture(imagePath, ref missing, ref missing, ref missing);

                                para2.Range.InsertParagraphAfter();


                            }
                            xx++;


                        }
                        //report.Append("\n");
                    }
                    break;
                    //report.Append("\n");
                }
                break;
            }






            object filename = Path.Combine(Configuration.GetSection("Data").GetSection("Path1").Value, "file.docx");
            document.SaveAs2(ref filename);
            document.Close(ref missing, ref missing, ref missing);
            document = null;
            winword.Quit(ref missing, ref missing, ref missing);
            winword = null;

            string filen = "file.docx";
            if (filename == null)
                return Content("filename not present");

            var path = Path.Combine(Configuration.GetSection("Data").GetSection("Path1").Value, filen);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            //return File(memory, GetContentType(path), Path.GetFileName(path));
            return File(memory, "text/plain", Path.GetFileName(path));



        }

        public async Task<IActionResult> GenerateSRS(int id)
        {
            StringBuilder report = new StringBuilder("");

            Microsoft.Office.Interop.Word.Application winword = new Microsoft.Office.Interop.Word.Application();
            winword.ShowAnimation = false;
            winword.Visible = false;
            object missing = System.Reflection.Missing.Value;

            Microsoft.Office.Interop.Word.Document document = winword.Documents.Add(ref missing, ref missing, ref missing, ref missing);
            document.Content.SetRange(0, 0);
            //document.Content.Text = "" + Environment.NewLine;

            document.Content.Font.Size = 8;
            document.Content.Font.Name = "Arial";


            //Add paragraph with Heading 2 style
            Microsoft.Office.Interop.Word.Paragraph para1 = document.Content.Paragraphs.Add(ref missing);
            para1.ReadingOrder = WdReadingOrder.wdReadingOrderRtl;
            para1.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
            object styleHeading1 = "Heading 1";
            para1.Range.set_Style(ref styleHeading1);
            para1.Range.Text = "Report";
            para1.Range.InsertParagraphAfter();
            //para1.ReadingOrder = WdReadingOrder.wdReadingOrderRtl;
            //para1.Alignment = WdParagraphAlignment.wdAlignParagraphRight;


            IEnumerable<CustomerProgram> cps = context.CustomerPrograms.Where(s => s.ID == id);
            foreach (var cp in cps)
            {
                report.Append("\t");
                report.Append(cp.ArabicNameInSingle);
                report.Append("\n");

                IEnumerable<Module> mods = context.Modules.Where(s => s.CustomerProgramID == cp.ID);
                foreach (var mod in mods)
                {
                    report.Append("\t\t");
                    report.Append(mod.ArabicNameInSingle);
                    report.Append("\n");

                    IEnumerable<Screen> scrs = context.Screens.Where(s => s.ModuleID == mod.ID);
                    foreach (var scr in scrs)
                    {

                        report.Append("\t\t\t");
                        report.Append(scr.ScreenNameArabic);
                        report.Append("\n");
                        /*
                        IEnumerable<ScreenComponent> scrcoms = context.ScreenComponent.Where(s => s.ScreenComponentID == scr.ID);
                        foreach (var scrcom in scrcoms)
                        {
                            report.Append("\t\t\t");
                            report.Append(scrcom.FieldNameEnglish);
                            report.Append("\n");
                        }
                        */
                        IEnumerable<UserStories> userStories = context.UserStories.Where(s => s.ScreenID == scr.ID);
                        foreach (var userStory in userStories)
                        {

                            report.Append("\t\t\t\t");
                            report.Append(userStory.Requirement);
                            report.Append("\n");
                        }


                        //Add paragraph with Heading 2 style
                        Microsoft.Office.Interop.Word.Paragraph para2 = document.Content.Paragraphs.Add(ref missing);
                        para2.ReadingOrder = WdReadingOrder.wdReadingOrderRtl;
                        //para2.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
                        object styleHeading2 = "Heading 2";
                        para2.Range.set_Style(ref styleHeading2);
                        para2.Range.Font.ColorIndex = WdColorIndex.wdBlack;
                        para2.Range.Text = report.ToString();
                        para2.Range.InsertParagraphAfter();


                        IEnumerable<ScreenComponent> ScreenComponents = context.ScreenComponent.Where(s => s.ScreenID == scr.ID);
                        string[] marks = new string[] { "FieldnameE", "FieldnameA", "Fieldtype", "Validation", "Note" };
                        string[] arr = new string[ScreenComponents.Count() * 5];
                        int counter = 0;
                        int row = 1;
                        string val = "";
                        foreach (ScreenComponent ScreenComponent in ScreenComponents)
                        {
                            val = "";
                            row++;
                            arr[counter++] = (ScreenComponent.FieldNameEnglish.ToString()) ?? " ";
                            arr[counter++] = (ScreenComponent.FielNameArabic) as string ?? " ";

                            FieldType fieldType = context.FieldTypes.FirstOrDefault(s => s.ID == ScreenComponent.FieldTypeID);
                            arr[counter++] = (fieldType.Name.ToString()) ?? " ";

                            if (ScreenComponent.ValidationRequiredMessage != null)
                                val += "Required, ";
                            if (ScreenComponent.ValidationMaxMessage != null)
                                val += "Max(" + ScreenComponent.ValidationMax + "), ";
                            if (ScreenComponent.ValidationMinMessage != null)
                                val += "Min(" + ScreenComponent.ValidationMin + ").";

                            arr[counter++] = val;
                            arr[counter++] = " ";
                        }


                        Table firstTable = document.Tables.Add(para1.Range, row, 5);
                        firstTable.Borders.Enable = 1;


                        int counter2 = 0;
                        for (int i = 1; i <= row; i++)
                        {
                            for (int x = 1; x <= 5; x++)
                            {
                                if (i == 1)
                                {
                                    firstTable.Rows[i].Cells[x].Range.Text = marks[x - 1];
                                    firstTable.Rows[i].Cells[x].Range.Font.ColorIndex = WdColorIndex.wdGray25;
                                    firstTable.Rows[i].Cells[x].Range.Font.Bold = 1;
                                    firstTable.Rows[i].Cells[x].Range.Font.Name = "verdana";
                                    firstTable.Rows[i].Cells[x].Range.Font.Size = 10;
                                    firstTable.Rows[i].Cells[x].Shading.BackgroundPatternColor = WdColor.wdColorGreen;
                                    firstTable.Rows[i].Cells[x].VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                                    firstTable.Rows[i].Cells[x].Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                                }

                                else
                                {
                                    if (x == 1)
                                        firstTable.Rows[i].Cells[x].Range.Text = arr[counter2++];
                                    if (x == 2)
                                        firstTable.Rows[i].Cells[x].Range.Text = arr[counter2++];
                                    if (x == 3)
                                        firstTable.Rows[i].Cells[x].Range.Text = arr[counter2++];
                                    if (x == 4)
                                        firstTable.Rows[i].Cells[x].Range.Text = arr[counter2++];
                                    if (x == 5)
                                        firstTable.Rows[i].Cells[x].Range.Text = arr[counter2++];
                                    firstTable.Rows[i].Cells[x].Range.Font.ColorIndex = WdColorIndex.wdBlack;
                                    firstTable.Rows[i].Cells[x].Range.Font.Bold = 1;
                                    firstTable.Rows[i].Cells[x].Range.Font.Name = "verdana";
                                    firstTable.Rows[i].Cells[x].Range.Font.Size = 10;
                                    //firstTable.Rows[i].Cells[x].Shading.BackgroundPatternColor = WdColor.wdColorGreen;
                                    firstTable.Rows[i].Cells[x].VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                                    firstTable.Rows[i].Cells[x].Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                                }
                            }
                        }

                        report.Clear();
                        report.Append("\n");
                        para2.Range.Text = report.ToString();
                        string imagePath = scr.ImagePathPhysical;
                        para2.Range.InlineShapes.AddPicture(imagePath, ref missing, ref missing, ref missing);

                        para2.Range.InsertParagraphAfter();


                    }
                }
            }





            object filename = Path.Combine(Configuration.GetSection("Data").GetSection("Path1").Value, "file.docx");
            document.SaveAs2(ref filename);
            document.Close(ref missing, ref missing, ref missing);
            document = null;
            winword.Quit(ref missing, ref missing, ref missing);
            winword = null;

            string filen = "file.docx";
            if (filename == null)
                return Content("filename not present");

            var path = Path.Combine(Configuration.GetSection("Data").GetSection("Path1").Value, filen);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            //return File(memory, GetContentType(path), Path.GetFileName(path));
            return File(memory, "text/plain", Path.GetFileName(path));



        }

        public IActionResult ScriptAllProgram3()
        {
            IEnumerable<Section> portals = context.Section.ToList();
            StringBuilder report = new StringBuilder("");
            foreach (var portal in portals)
            {
                report.Append(portal.ArabicName);
                report.Append("\n");


                IEnumerable<CustomerProgram> cps = context.CustomerPrograms.Where(s => s.SectionID == portal.ID);
                foreach (var cp in cps)
                {
                    report.Append("\t");
                    report.Append(cp.ArabicNameInSingle);
                    report.Append("\n");

                    IEnumerable<Module> mods = context.Modules.Where(s => s.CustomerProgramID == cp.ID);
                    foreach (var mod in mods)
                    {
                        report.Append("\t\t");
                        report.Append(mod.ArabicNameInSingle);
                        report.Append("\n");

                    }
                }
            }


            var stream = new MemoryStream(Encoding.UTF8.GetBytes(report.ToString()));
            return new FileStreamResult(stream, new MediaTypeHeaderValue("text/plain"))
            {
                FileDownloadName = "nav.txt"
            };


        }

        public IActionResult CreateZipFile()
        {
            WriteAllLookup();

            var fileName = string.Format("{0}_ImageFiles.zip", DateTime.Today.Date.ToString("dd-MM-yyyy") + "_1");
            //var tempOutPutPath = _hostingEnvironment.ContentRootPath + Url.Content("\\TempImages\\") + fileName;
            var tempOutPutPath = _hostingEnvironment.ContentRootPath + "\\wwwroot\\" + fileName;

            using (ZipOutputStream s = new ZipOutputStream(System.IO.File.Create(tempOutPutPath)))
            {
                s.SetLevel(9); // 0-9, 9 being the highest compression  

                byte[] buffer = new byte[4096];

                var ImageList = new List<string>();
                DirectoryInfo DI = new DirectoryInfo(_hostingEnvironment.ContentRootPath + "\\wwwroot\\LookupClass\\Models");
                foreach (var i in DI.GetFiles())
                {
                    ImageList.Add(i.ToString());
                }


                for (int i = 0; i < ImageList.Count; i++)
                {
                    ZipEntry entry = new ZipEntry(Path.GetFileName(ImageList[i]));
                    entry.DateTime = DateTime.Now;
                    entry.IsUnicodeText = true;
                    s.PutNextEntry(entry);

                    using (FileStream fs = System.IO.File.OpenRead(ImageList[i]))
                    {
                        int sourceBytes;
                        do
                        {
                            sourceBytes = fs.Read(buffer, 0, buffer.Length);
                            s.Write(buffer, 0, sourceBytes);
                        } while (sourceBytes > 0);
                    }
                }
                s.Finish();
                s.Flush();
                s.Close();

            }

            byte[] finalResult = System.IO.File.ReadAllBytes(tempOutPutPath);
            if (System.IO.File.Exists(tempOutPutPath))
                System.IO.File.Delete(tempOutPutPath);

            if (finalResult == null || !finalResult.Any())
                throw new Exception(String.Format("No Files found with Image"));

            return File(finalResult, "application/zip", fileName);

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
            IEnumerable<ScreenComponent> screenComponents = context.ScreenComponent.Where(s => s.FieldTypeID == 1);
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
        private void WriteAllLookup()
        {
            IEnumerable<ScreenComponent> screenComponents = context.ScreenComponent.Where(s => s.FieldTypeID == 1);
            List<string> SCs = new List<string>();
            foreach (var screenComponent in screenComponents)
            {
                if (!(SCs.Contains(screenComponent.FieldNameEnglish)))
                    SCs.Add(screenComponent.FieldNameEnglish);
            }

            foreach (var SC in SCs)
            {
                WriteInFile("Models", SC, LookupModelFile(SC));
                WriteInFile("Controllers", SC, LookupControlFile(SC));
            }

            WriteInFile("Context", "Context.txt", ContextTXT(SCs));
        }
        public IActionResult CreateZipFile2(int id)
        {
            WriteAllLookup();

            string FolderPath = _hostingEnvironment.ContentRootPath + "\\wwwroot\\FolderData";
            string ZipFilePath = _hostingEnvironment.ContentRootPath + "\\wwwroot\\folder.zip";

            System.IO.Compression.ZipFile.CreateFromDirectory(FolderPath, ZipFilePath);

            byte[] finalResult = System.IO.File.ReadAllBytes(ZipFilePath);
            if (System.IO.File.Exists(ZipFilePath))
                System.IO.File.Delete(ZipFilePath);

            if (finalResult == null || !finalResult.Any())
                throw new Exception(String.Format("No Files found with Image"));

            return File(finalResult, "application/zip", "Lookups.zip");


        }





        public IActionResult CreateZipFile3(int id)
        {
            CustomerProgram pro = context.CustomerPrograms.FirstOrDefault(s => s.ID == id);

            WriteAllClass(id);

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

            return File(finalResult, "application/zip", "ClassesFor_" + pro.EnglishNameInSingle.Replace(" ", "") + ".zip");


        }
        private void WriteAllClass(int id)
        {
            CustomerProgram pro = context.CustomerPrograms.FirstOrDefault(s => s.ID == id);
            IEnumerable<Module> modules = context.Modules.Where(s => s.CustomerProgramID == pro.ID);

            foreach (var module in modules)
            {
                IEnumerable<Screen> Screens = context.Screens.Where(s => s.ModuleID == module.ID && s.screenTypeID == 1);
                foreach (var Screen in Screens)
                {
                    string name = Screen.ScreenName.Replace("create", "").Replace("Create", "").Replace(" ", "");
                    WriteInFile("ModelClass", name, GenerateModelCS(Screen));
                }
            }
        }
        private string GenerateModelCS(Screen ScreenObject)
        {
            IEnumerable<ScreenComponent> ScreenComponents = context.ScreenComponent.Where(s => s.ScreenID == ScreenObject.ID && s.Readonly == false);
            StringBuilder report = new StringBuilder("");
            report.Append(
            @"using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;");

            report.Append("\n");
            report.Append("\n");
            report.Append(@"namespace RecordEmployeeData.Models");
            report.Append("\n");
            report.Append(@"{");
            report.Append("\n");
            report.Append(@"public class " + ScreenObject.ScreenName.Replace("create", "").Replace("Create", "").Replace(" ", ""));
            report.Append("\n");
            report.Append(@" {

[Key]
[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
public int Id { get; set; }
");


            foreach (var item in ScreenComponents)
            {
                report.Append("\n");
                report.Append("public ");

                InputType inputType = context.InputTypes.FirstOrDefault(s => s.ID == item.InputtypeID);
                string name = inputType.Name;
                if (name == "button") continue;

                switch (name.ToLower())
                {
                    case "number": name = "int?"; break;
                    case "text": name = "string"; break;
                    case "bool": name = "bool?"; break;
                    case "email": name = "string"; break;
                    case "date": name = "DateTime?"; break;
                    default: name = ""; break;
                }
                report.Append(name);
                report.Append(" ");
                report.Append(item.FieldNameEnglish);
                report.Append(" { get; set; }");
                report.Append("\n");
            }

            report.Append("\n");
            report.Append(" }");
            report.Append("\n");
            report.Append("}");

            return report.ToString();
        }






        public IActionResult ScriptAllProgram4()
        {
            IEnumerable<CustomerProgram> pros = context.CustomerPrograms.ToList();
            StringBuilder report = new StringBuilder("");
            foreach (var pro in pros)
            {
                IEnumerable<Module> mods = context.Modules.Where(m => m.CustomerProgramID == pro.ID);
                foreach (var mod in mods)
                {
                    report.Append(pro.ArabicNameInPlural);
                    report.Append("\t");

                    report.Append((mod.ArabicNameInSingle));
                    report.Append("\t");

                    //IEnumerable<Screen> scs = context.Screens.Where(m => m.ModuleID == mod.ID).Count();
                    report.Append(context.Screens.Where(m => m.ModuleID == mod.ID).Count());
                    report.Append("\n");
                }
            }


            var stream = new MemoryStream(Encoding.UTF8.GetBytes(report.ToString()));
            return new FileStreamResult(stream, new MediaTypeHeaderValue("text/plain"))
            {
                FileDownloadName = "report.txt"
            };
        }


        public IActionResult ScriptAllProgram5()
        {
            IEnumerable<CustomerProgram> pros = context.CustomerPrograms.ToList();
            StringBuilder report = new StringBuilder("");
            foreach (var pro in pros)
            {
                report.Append(pro.ArabicNameInSingle);
                report.Append("\t");
                report.Append((pro.Date).ToString("dd/MM/yyyy"));
                report.Append("\t");
                report.Append((pro.Date).ToString("dddd"));
                report.Append("\n");

            }


            var stream = new MemoryStream(Encoding.UTF8.GetBytes(report.ToString()));
            return new FileStreamResult(stream, new MediaTypeHeaderValue("text/plain"))
            {
                FileDownloadName = "report.txt"
            };
        }


        /*
        public async Task<IActionResult> GenerateWord(int id)
        {
            IEnumerable<CustomerProgram> cps = context.CustomerPrograms.ToList();
            StringBuilder report = new StringBuilder("");

            foreach (var cp in cps)
            {

                report.Append(cp.ArabicNameInSingle);
                report.Append("\n");

                IEnumerable<Module> mods = context.Modules.Where(s => s.CustomerProgramID == cp.ID);
                foreach (var mod in mods)
                {
                    report.Append("\t");
                    report.Append(mod.ArabicNameInSingle);
                    report.Append("\n");

                    IEnumerable<Screen> scrs = context.Screens.Where(s => s.ModuleID == mod.ID);
                    foreach (var scr in scrs)
                    {
                        report.Append("\t\t");
                        report.Append(scr.ScreenNameArabic);
                        report.Append("\n");
                        /*
                        IEnumerable<ScreenComponent> scrcoms = context.ScreenComponent.Where(s => s.ScreenComponentID == scr.ID);
                        foreach (var scrcom in scrcoms)
                        {
                            report.Append("\t\t\t");
                            report.Append(scrcom.FieldNameEnglish);
                            report.Append("\n");
                        }


                        IEnumerable<UserStories> userStories = context.UserStories.Where(s => s.ScreenID == scr.ID);
                        foreach (var userStory in userStories)
                        {
                            report.Append("\t\t\t");
                            report.Append(userStory.Requirement);
                            report.Append("\n");
                        }

                    }
                    //report.Append("\n");
                }
                //report.Append("\n");
            }


            Microsoft.Office.Interop.Word.Application winword = new Microsoft.Office.Interop.Word.Application();
            winword.ShowAnimation = false;
            winword.Visible = false;
            object missing = System.Reflection.Missing.Value;

            Microsoft.Office.Interop.Word.Document document = winword.Documents.Add(ref missing, ref missing, ref missing, ref missing);
            document.Content.SetRange(0, 0);
            //document.Content.Text = "" + Environment.NewLine;

            //Add paragraph with Heading 2 style
            Microsoft.Office.Interop.Word.Paragraph para1 = document.Content.Paragraphs.Add(ref missing);
            para1.ReadingOrder = WdReadingOrder.wdReadingOrderRtl;
            para1.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
            object styleHeading1 = "Heading 1";
            para1.Range.set_Style(ref styleHeading1);
            para1.Range.Text = "Report";
            para1.Range.InsertParagraphAfter();
            //para1.ReadingOrder = WdReadingOrder.wdReadingOrderRtl;
            //para1.Alignment = WdParagraphAlignment.wdAlignParagraphRight;


            //Add paragraph with Heading 2 style
            Microsoft.Office.Interop.Word.Paragraph para2 = document.Content.Paragraphs.Add(ref missing);
            para2.ReadingOrder = WdReadingOrder.wdReadingOrderRtl;
            //para2.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
            object styleHeading2 = "Heading 2";
            para2.Range.set_Style(ref styleHeading2);
            para2.Range.Font.ColorIndex = WdColorIndex.wdBlack;
            para2.Range.Text = report.ToString();
            para2.Range.InsertParagraphAfter();
            //para2.ReadingOrder = WdReadingOrder.wdReadingOrderRtl;
            //para2.Alignment = WdParagraphAlignment.wdAlignParagraphRight;



            object filename = Path.Combine(Configuration.GetSection("Data").GetSection("Path1").Value, "file.docx");
            document.SaveAs2(ref filename);
            document.Close(ref missing, ref missing, ref missing);
            document = null;
            winword.Quit(ref missing, ref missing, ref missing);
            winword = null;

            string filen = "file.docx";
            if (filename == null)
                return Content("filename not present");

            var path = Path.Combine(Configuration.GetSection("Data").GetSection("Path1").Value, filen);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            //return File(memory, GetContentType(path), Path.GetFileName(path));
            return File(memory, "text/plain", Path.GetFileName(path));
        }
        */
    }
}