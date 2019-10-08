using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gather_Requirement_Project.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Gather_Requirement_Project.Controllers
{
    public class StatisticsController : Controller
    {


        public IConfiguration Configuration { get; }

        Context context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public StatisticsController(Context _context, IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            context = _context;
            Configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }



        public IActionResult Index()
        {
            IEnumerable<CustomerProgram> cps = context.CustomerPrograms.ToList();
            List<string> Devs = new List<string>();
            List<int> programs = new List<int>();
            List<int> modules = new List<int>();
            List<int> screens = new List<int>();
            List<int> comp = new List<int>();


            foreach (var cp in cps)
            {
                string s = cp.DevName!=null ? cp.DevName.ToLower(): cp.DevName;
                if (!(Devs.Contains(s)))
                    Devs.Add(s);

            }

            int countProgram = 0;
            int countModule = 0;
            int countScreen = 0;
            int countComp = 0;
            int countUserStories = 0;

            foreach (var Dev in Devs)
            {

                IEnumerable<CustomerProgram> progs = context.CustomerPrograms.Where(s => s.DevName.ToLower() == Dev);
                foreach (var prog in progs)
                {
                    countProgram++;
                    IEnumerable<Module> mods = context.Modules.Where(s => s.CustomerProgramID == prog.ID);
                    countModule += mods.Count();
                    foreach (var mod in mods)
                    {
                        
                        IEnumerable<Screen> scrs = context.Screens.Where(s => s.ModuleID == mod.ID);
                        countScreen+= scrs.Count();
                        foreach (var scr in scrs)
                        {
                            countComp += Int32.Parse(context.ScreenComponent.Where(s => s.ScreenID == scr.ID).Count().ToString());
                            /*
                            IEnumerable<UserStories> userStories = context.UserStories.Where(s => s.ScreenID == scr.ID);
                            if (userStories.Count() == 0)
                            {
                                countUserStories++;
                            }*/
                        }
                    }
                }

                programs.Add(countProgram); countProgram = 0;
                modules.Add(countModule); countModule = 0;
                screens.Add(countScreen); countScreen = 0;
                comp.Add(countComp); countComp = 0;
            }



            ViewBag.Length = Devs.Count();
            ViewBag.Devs = Devs;
            ViewBag.programs = programs;
            ViewBag.modules = modules;
            ViewBag.screens = screens;
            ViewBag.comp = comp;
            return View();

        }






















    }
}
