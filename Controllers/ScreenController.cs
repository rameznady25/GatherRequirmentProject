using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gather_Requirement_Project.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Microsoft.Net.Http.Headers;
using System.Text;
using Microsoft.Office.Interop.Word;
using Microsoft.Extensions.Configuration;
using System.Drawing;
using Microsoft.AspNetCore.Authorization;
using Gather_Requirement_Project.Models.JsonClasses;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.Extensions.Hosting;

namespace Gather_Requirement_Project.Controllers
{
    [Authorize]
    public class ScreenController : Controller
    {

        public IConfiguration Configuration { get; }
        Context context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public ScreenController(Context _context, IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            context = _context;
            Configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }


        // GET: Screen
        public ActionResult Index(int? modulId)
        {
            IEnumerable<ScreenType> ScreenTypeList = context.ScreenTypes.ToList();
            ViewBag.ScreenType = ScreenTypeList;

            IQueryable<Screen> screens = context.Screens.Include(s => s.Module);

            if (modulId != null)
                screens = screens.Where(s => s.ModuleID == modulId);

            ViewBag.ModID = modulId;



            return View(screens.ToList());
        }

        // GET: Screen/Details/5
        public ActionResult Details(int id)
        {
            Screen screenlist = context.Screens.FirstOrDefault(s => s.ID == id);

            IEnumerable<ScreenType> ScreenTypeList = context.ScreenTypes.ToList();
            ViewBag.ScreenType = ScreenTypeList;

            IEnumerable<Module> ProgramList = context.Modules.ToList();
            ViewBag.Program = ProgramList;

            return View(screenlist);
        }

        // GET: Screen/Create
        public ActionResult Create(int modulId)
        {
            IEnumerable<ScreenType> screenTypes = context.ScreenTypes.ToList();
            ViewBag.screenTypesList = screenTypes;

            IEnumerable<Module> modules = context.Modules.Where(s => s.ID == modulId).ToList();
            ViewBag.modulesList = modules;

            ViewBag.ModuleID = modulId;

            return View();
        }

        // POST: Screen/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Screen collection)
        {
            try
            {
                if (collection.ImageFile != null)
                {
                    string FileName = Path.GetFileNameWithoutExtension(collection.ImageFile.FileName);
                    string FileExtension = Path.GetExtension(collection.ImageFile.FileName);
                    FileName = FileName.Trim() + "_" + DateTime.Now.ToString("yyyyMMdd") + FileExtension;
                    //collection.ImagePathPhysical = Configuration.GetSection("Data").GetSection("PhysicalPath").Value + "\\" + FileName;
                    collection.ImagePathPhysical = _hostingEnvironment.ContentRootPath + "\\wwwroot\\Images\\" + FileName;

                    collection.ImageName = FileName;
                    if (collection.ImageFile.Length > 0)
                    {
                        using (var stream = new FileStream(collection.ImagePathPhysical, FileMode.Create))
                        {
                            collection.ImageFile.CopyTo(stream);
                        }
                    }
                }
                IEnumerable<ScreenType> screenTypes = context.ScreenTypes.ToList();
                ViewBag.screenTypesList = screenTypes;

                IEnumerable<Module> modules = context.Modules.Where(s => s.ID == collection.ModuleID).ToList();
                ViewBag.modulesList = modules;

                context.Screens.Add(collection);
                context.SaveChanges();

                return RedirectToAction(nameof(Index), new { modulId = collection.ModuleID });
            }
            catch (Exception ex)
            {
                IEnumerable<ScreenType> screenTypes = context.ScreenTypes.ToList();
                ViewBag.screenTypesList = screenTypes;

                IEnumerable<Module> customerPrograms = context.Modules.ToList();
                ViewBag.programList = customerPrograms;
                return View();
            }
        }

        // GET: Screen/Edit/5
        public ActionResult Edit(int id)
        {
            Screen ScreenEdit = context.Screens.FirstOrDefault(s => s.ID == id);

            //Dpartment
            IEnumerable<ScreenType> screenTypes = context.ScreenTypes.ToList();
            ViewBag.screenTypesList = screenTypes;

            IEnumerable<Module> modules = context.Modules.ToList();
            ViewBag.modules = modules;

            return View(ScreenEdit);
        }

        // POST: Screen/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Screen collection)
        {
            try
            {
                Screen screenInDb = context.Screens.FirstOrDefault(s => s.ID == id);

                if (collection.ImageFile != null)
                {
                    string FileName = Path.GetFileNameWithoutExtension(collection.ImageFile.FileName);
                    string FileExtension = Path.GetExtension(collection.ImageFile.FileName);
                    FileName = FileName.Trim() + "_" + DateTime.Now.ToString("yyyyMMdd") + FileExtension;
                    //screenInDb.ImagePathPhysical = Configuration.GetSection("Data").GetSection("PhysicalPath").Value + "\\" + FileName;
                    screenInDb.ImagePathPhysical = _hostingEnvironment.ContentRootPath + "\\wwwroot\\Images\\" + FileName;
                    screenInDb.ImageName = FileName;
                    if (collection.ImageFile.Length > 0)
                    {
                        using (var stream = new FileStream(screenInDb.ImagePathPhysical, FileMode.Create))
                        {
                            collection.ImageFile.CopyTo(stream);
                        }
                    }
                }

                screenInDb.EmpName = collection.EmpName;
                //screenInDb.DevName = collection.DevName;
                //screenInDb.Date = collection.Date;
                screenInDb.ScreenName = collection.ScreenName;
                screenInDb.ScreenNameArabic = collection.ScreenNameArabic;
                screenInDb.screenTypeID = collection.screenTypeID;
                screenInDb.EmpJob = collection.EmpJob;

                context.SaveChanges();
                return RedirectToAction(nameof(Index), new { modulId = collection.ModuleID });
            }
            catch
            {
                //Screen screenInDb = context.Screens.FirstOrDefault(s => s.ID == id);
                IEnumerable<ScreenType> screenTypes = context.ScreenTypes.ToList();
                ViewBag.screenTypesList = screenTypes;

                IEnumerable<Module> customerPrograms = context.Modules.ToList();
                ViewBag.programList = customerPrograms;


                return View();
            }
        }

        // GET: Screen/Delete/5
        public ActionResult Delete(int id)
        {
            Screen screen = context.Screens.FirstOrDefault(s => s.ID == id);

            context.Screens.Remove(screen);
            context.SaveChanges();
            return RedirectToAction(nameof(Index), new { modulId = screen.ModuleID });
        }

        // POST: Screen/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }




        public ActionResult GenerateTXT(int id)
        {
            Screen ScreenObject = context.Screens.FirstOrDefault(s => s.ID == id);
            IEnumerable<ScreenComponent> ScreenComponents = context.ScreenComponent.Where(s => s.ScreenID == ScreenObject.ID);
            /*
            string lines;
            lines = "This is test document " + "\n";
            lines += "As a <" + ScreenObject.EmpJob + "> " + "\n";
            lines += "I want to <" + ScreenObject.ScreenName + "> " + "\n";
            lines += "So that, <" + ScreenObject.ScreenGoal + "> " + "\n";
            lines += "User Story" + "\n";
            lines += " " + "\n";
            lines += "This is test document " + "\n";
            lines += "Acceptance Criteria: " + ScreenObject.Criteria + "\n";
            lines += "Given <" + ScreenObject.PreCondition + "> " + "\n";
            lines += "When <" + ScreenObject.DoSceniro + "> " + "\n";
            lines += "Then <" + ScreenObject.ExpectedResult + "> " + "\n";
            lines += " " + "\n";
            lines += " " + "\n";
            lines += "FieldnameE FieldnameA  Fieldtype   Validation  Note" + "\n";
            

            string[] s1 = new string[20];
            string[] s2 = new string[20];
            int i = 0;
            string val = "";

            foreach (ScreenComponent ScreenComponent in ScreenComponents)
            {
                val = "";

                lines += ScreenComponent.FieldNameEnglish;
                lines += "    ";
                lines += ScreenComponent.FielNameArabic;
                lines += "    ";
                FieldType fieldType = context.FieldTypes.FirstOrDefault(s => s.ID == ScreenComponent.FieldTypeID);
                lines += (fieldType.Name.ToString()) ?? " ";
                lines += "    ";

                if (ScreenComponent.ValidationRequiredMessage != null)
                    val += "Required, ";
                if (ScreenComponent.ValidationMaxMessage != null)
                    val += "Max(" + ScreenComponent.ValidationMax + "), ";
                if (ScreenComponent.ValidationMinMessage != null)
                    val += "Min(" + ScreenComponent.ValidationMin + ").";
                lines += val;
                lines += "    ";

                lines += "Note:";
                lines += "\n";

            }
               

            var stream = new MemoryStream(Encoding.UTF8.GetBytes(lines));
            return new FileStreamResult(stream, new MediaTypeHeaderValue("text/plain"))
            {
                FileDownloadName = ScreenObject.ScreenName + ".txt"
            };
             */
            return View();
        }

        public IActionResult GenerateJson(int id)
        {

            Screen ScreenObject = context.Screens.FirstOrDefault(s => s.ID == id);
            Module customerProgram = context.Modules.FirstOrDefault(s => s.ID == ScreenObject.ModuleID);
            IEnumerable<ScreenComponent> ScreenComponents = context.ScreenComponent.Where(s => s.ScreenID == ScreenObject.ID);

            List<InputObjectViewModel> inputObject = new List<InputObjectViewModel>();
            List<SelectObjectViewModel> selectObject = new List<SelectObjectViewModel>();

            foreach (ScreenComponent ScreenComponent in ScreenComponents)
            {
                if (ScreenComponent.FieldTypeID == 1)
                {
                    IEnumerable<ScreenComponent> ScreenComponentList = context.ScreenComponent.Where(s => s.ScreenComponentID == ScreenComponent.ID);
                    SelectObjectViewModel var = new SelectObjectViewModel();
                    var.Label = ScreenComponent.FielNameArabic;
                    var.Field = ScreenComponent.FieldNameEnglish;                           //Camel case (stylized as camelCase
                    var.ServiceName = ScreenComponent.ServiceNameForDropdown;               //Camel case (stylized as camelCase
                    FieldType fieldType = context.FieldTypes.FirstOrDefault(s => s.ID == ScreenComponent.FieldTypeID);
                    var.Validators = GenerateValidation(ScreenComponent);
                    var.ReadOnly = ScreenComponent.Readonly;
                    var.Dependants = GenerateDependant(ScreenComponentList);
                    ScreenComponent ScreenComp = context.ScreenComponent.FirstOrDefault(s => s.ID == ScreenComponent.ScreenComponentID);
                    var.Dependant = ScreenComp != null ? ScreenComp.FieldNameEnglish : "";
                    var.Visible = ScreenComponent.Visible;


                    selectObject.Add(var);
                }
                else
                {
                    InputObjectViewModel var = new InputObjectViewModel();
                    var.Label = ScreenComponent.FielNameArabic;                 //Camel case (stylized as camelCase
                    var.Field = ScreenComponent.FieldNameEnglish;               //Camel case (stylized as camelCase
                    FieldType fieldType = context.FieldTypes.FirstOrDefault(s => s.ID == ScreenComponent.FieldTypeID);
                    var.Type = fieldType.Name;
                    var.Visible = ScreenComponent.Visible;
                    var.ReadOnly = ScreenComponent.Readonly;
                    var.Default = ScreenComponent.DefaultValue;
                    var.Validators = GenerateValidation(ScreenComponent);
                    ScreenComponent ScreenComp = context.ScreenComponent.FirstOrDefault(s => s.ID == ScreenComponent.ScreenComponentID);
                    var.Dependant = ScreenComp != null ? ScreenComp.FieldNameEnglish : "";

                    inputObject.Add(var);
                }

            }



            JsonFileViewModel jsoncontent = new JsonFileViewModel();
            jsoncontent.InputObjects = inputObject;
            jsoncontent.SelectObjects = selectObject;
            jsoncontent.Url = customerProgram.URLForService;
            jsoncontent.CustomValidators = GenerateCustomValidation(ScreenObject.ID);
            jsoncontent.ComponentName = customerProgram.EnglishNameInSingle;
            jsoncontent.ComponentPlural = customerProgram.EnglishNameInPlural;
            jsoncontent.PageTitle = customerProgram.ArabicNameInSingle;
            jsoncontent.PageTitles = customerProgram.ArabicNameInPlural;


            ScreenType screenType = context.ScreenTypes.FirstOrDefault(s => s.ID == ScreenObject.screenTypeID);

            string result = JsonConvert.SerializeObject(jsoncontent);
            result = result.Replace("null", "\"\"").Replace("\t", "");
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(result));
            return new FileStreamResult(stream, new MediaTypeHeaderValue("text/plain"))
            {
                FileDownloadName = screenType.Name.ToLower() + "model.json"
            };
        }


        public IActionResult GenerateModelCS (int id)
        {

            Screen ScreenObject = context.Screens.FirstOrDefault(s => s.ID == id);
            IEnumerable<ScreenComponent> ScreenComponents = context.ScreenComponent.Where(s => s.ScreenID == ScreenObject.ID && s.Readonly == false);
            string className = ScreenObject.ScreenName.Replace("create", "").Replace("Create", "").Replace(" ", "");
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
            report.Append(@"public class " + className);
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

            var stream = new MemoryStream(Encoding.UTF8.GetBytes(report.ToString()));
            return new FileStreamResult(stream, new MediaTypeHeaderValue("text/plain"))
            {
                FileDownloadName = className + ".cs"
            };
        }


        public IActionResult GenerateModelJson(int id)
        {

            Screen ScreenObject = context.Screens.FirstOrDefault(s => s.ID == id);
            Module customerProgram = context.Modules.FirstOrDefault(s => s.ID == ScreenObject.ModuleID);
            IEnumerable<ScreenComponent> ScreenComponents = context.ScreenComponent.Where(s => s.ScreenID == ScreenObject.ID);

            List<Property> ps = new List<Property>();


            foreach (var item in ScreenComponents)
            {

                Property p = new Property();
                p.Field = item.FieldNameEnglish;

                FieldType inputType = context.FieldTypes.FirstOrDefault(s => s.ID == item.FieldTypeID);
                string name = inputType.Name;

                switch (name.ToLower())
                {
                    case "dropdown": name = "number";  break;
                    case "number": name = "number";  break;
                    case "text": name = "string";  break;
                    case "email": name = "string";  break;
                    case "radibutton": name = "string";  break;
                    case "textarea": name = "string";  break;
                    case "file": name = "string"; break;
                    case "checkbox": name = "boolean"; break;
                    case "date": name = "Date"; break;
                    default: name = ""; break;
                }
                p.Type = name;

                if (name == "button") break;
                ps.Add(p);
            }

            Model m = new Model();
            m.Name = customerProgram.EnglishNameInSingle;
            m.Properties = ps;

            string result = JsonConvert.SerializeObject(m);
            result = result.Replace("null", "\"\"").Replace("\t", "").Replace("\\", "");
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(result));
            return new FileStreamResult(stream, new MediaTypeHeaderValue("text/plain"))
            {
                FileDownloadName = "model.json"
            };
        }
/*
        public async Task<IActionResult> GenerateWord(int id)
        {
            Screen ScreenObject = context.Screens.FirstOrDefault(s => s.ID == id);
            IEnumerable<ScreenComponent> ScreenComponents = context.ScreenComponent.Where(s => s.ScreenID == ScreenObject.ID);
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


            string lines;
            lines = "As a <" + ScreenObject.EmpJob + "> " + "\n";
            lines += "I want to <" + ScreenObject.ScreenName + "> " + "\n";
            lines += "So that, <" + ScreenObject.ScreenGoal + "> " + "\n";
            lines += "User Story" + "\n";
            lines += " " + "\n";
            lines += "This is test document " + "\n";
            lines += "Acceptance Criteria: " + ScreenObject.Criteria + "\n";
            lines += "Given <" + ScreenObject.PreCondition + "> " + "\n";
            lines += "When <" + ScreenObject.DoSceniro + "> " + "\n";
            lines += "Then <" + ScreenObject.ExpectedResult + "> ";
            lines += " " + "\n";


            Microsoft.Office.Interop.Word.Application winword = new Microsoft.Office.Interop.Word.Application();
            winword.ShowAnimation = false;
            winword.Visible = false;
            object missing = System.Reflection.Missing.Value;

            Microsoft.Office.Interop.Word.Document document = winword.Documents.Add(ref missing, ref missing, ref missing, ref missing);
            document.Content.SetRange(0, 0);
            //document.Content.Text = "" + Environment.NewLine;


            //Add paragraph with Heading 2 style
            Microsoft.Office.Interop.Word.Paragraph para1 = document.Content.Paragraphs.Add(ref missing);
            object styleHeading1 = "Heading 1";
            para1.Range.set_Style(ref styleHeading1);
            para1.Range.Text = "User Story";
            para1.Range.InsertParagraphAfter();

            //Add paragraph with Heading 2 style
            Microsoft.Office.Interop.Word.Paragraph para2 = document.Content.Paragraphs.Add(ref missing);
            object styleHeading2 = "Heading 2";
            para2.Range.set_Style(ref styleHeading2);
            para2.Range.Font.ColorIndex = WdColorIndex.wdBlack;
            para2.Range.Text = lines;
            para2.Range.InsertParagraphAfter();

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



            string imagePath = ScreenObject.ImagePathPhysical;
            if (imagePath != null)
            {
                InlineShape autoScaledInlineShape = document.Content.InlineShapes.AddPicture(imagePath);
                float scaledWidth = autoScaledInlineShape.Width;
                float scaledHeight = autoScaledInlineShape.Height;
                autoScaledInlineShape.Delete();

                Shape newShape = document.Shapes.AddShape(1, 0, 0, scaledWidth, scaledHeight);
                newShape.Fill.UserPicture(imagePath);

                InlineShape finalInlineShape = newShape.ConvertToInlineShape();
                finalInlineShape.Line.Visible = Microsoft.Office.Core.MsoTriState.msoFalse;
            }



            object filename = Path.Combine(Configuration.GetSection("Data").GetSection("Path1").Value, ScreenObject.ScreenName + ".docx");
            document.SaveAs2(ref filename);
            document.Close(ref missing, ref missing, ref missing);
            document = null;
            winword.Quit(ref missing, ref missing, ref missing);
            winword = null;

            string filen = ScreenObject.ScreenName + ".docx";
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

        private List<ValidationViewModel> GenerateValidation(ScreenComponent screenComponent)
        {
            List<ValidationViewModel> vv = new List<ValidationViewModel>();

            if (screenComponent.ValidationMax != null && screenComponent.FieldType.Name.ToLower() == "number")
            {
                ValidationViewModel v = new ValidationViewModel();
                v.Validation = "max(" + screenComponent.ValidationMax + ")";
                v.Message = screenComponent.ValidationMaxMessage;
                vv.Add(v);
            }

            if (screenComponent.ValidationMin != null && screenComponent.FieldType.Name.ToLower() == "number")
            {
                ValidationViewModel v = new ValidationViewModel();
                v.Validation = "min(" + screenComponent.ValidationMin + ")";
                v.Message = screenComponent.ValidationMinMessage;
                vv.Add(v);
            }

            if (screenComponent.ValidationMax != null && screenComponent.FieldType.Name.ToLower() == "text")
            {
                ValidationViewModel v = new ValidationViewModel();
                v.Validation = "maxLength(" + screenComponent.ValidationMax + ")";
                v.Message = screenComponent.ValidationMaxMessage;
                vv.Add(v);
            }

            if (screenComponent.ValidationMin != null && screenComponent.FieldType.Name.ToLower() == "text")
            {
                ValidationViewModel v = new ValidationViewModel();
                v.Validation = "minLength(" + screenComponent.ValidationMin + ")";
                v.Message = screenComponent.ValidationMinMessage;
                vv.Add(v);
            }

            return vv;
        }
        private List<DependantViewModel> GenerateDependant(IEnumerable<ScreenComponent> ScreenComponentList)
        {
            List<DependantViewModel> dd = new List<DependantViewModel>();
            List<string> vals = new List<string>();
            string[] values;

            foreach (var item in ScreenComponentList)
            {
                if (item.Values != null)
                {
                    values = item.Values.Split("-");
                    foreach (var it in values)
                        if (!vals.Contains(it))
                            vals.Add(it);
                }

            }

            foreach (var val in vals)
            {
                DependantViewModel d = new DependantViewModel();
                d.Value = val;
                d.ControlName = new List<string>();
                foreach (var ScreenComponent in ScreenComponentList)
                {
                    if (ScreenComponent.Values != null)
                    {
                        string[] values1 = ScreenComponent.Values.Split("-");
                        if (values1.Contains(val))
                        {
                            d.ControlName.Add(ScreenComponent.FieldNameEnglish);
                        }
                    }

                }
                dd.Add(d);
            }
            return dd;
        }
        private List<ValidationViewModel> GenerateCustomValidation(int id)
        {
            List<ValidationViewModel> vv = new List<ValidationViewModel>();

            IEnumerable<ScreenComponent> ScreenComponents1 = context.ScreenComponent.Where(s => s.ValidationEqualID != null && s.ScreenID == id);
            foreach (var item in ScreenComponents1)
            {
                ScreenComponent var = context.ScreenComponent.FirstOrDefault(s => s.ID == item.ValidationEqualID );
                ValidationViewModel v = new ValidationViewModel();
                v.Validation = "Equal(\"" + item.FieldNameEnglish + "\",\"" + var.FieldNameEnglish + "\")";
                v.Message = item.ValidationEqualMessage;
                vv.Add(v);
            }

            IEnumerable<ScreenComponent> ScreenComponents2 = context.ScreenComponent.Where(s => s.ValidationLessthanID != null && s.ScreenID == id);
            foreach (var item in ScreenComponents2)
            {
                ScreenComponent var = context.ScreenComponent.FirstOrDefault(s => s.ID == item.ValidationLessthanID);
                ValidationViewModel v = new ValidationViewModel();
                v.Validation = "Less(\"" + item.FieldNameEnglish + "\",\"" + var.FieldNameEnglish + "\")";
                v.Message = item.ValidationLessthanMessage;
                vv.Add(v);
            }

            IEnumerable<ScreenComponent> ScreenComponents3 = context.ScreenComponent.Where(s => s.ValidationGreaterthanID != null && s.ScreenID == id );
            foreach (var item in ScreenComponents3)
            {
                ScreenComponent var = context.ScreenComponent.FirstOrDefault(s => s.ID == item.ValidationGreaterthanID);
                ValidationViewModel v = new ValidationViewModel();
                v.Validation = "Greater(\"" + item.FieldNameEnglish + "\",\"" + var.FieldNameEnglish + "\")";
                v.Message = item.ValidationGreaterthanMessage;
                vv.Add(v);
            }
            return vv;
        }
        public ActionResult CopyScreen(int id)
        {
            Screen ScreenObject2 = context.Screens.FirstOrDefault(s => s.ID == id);
            IEnumerable<ScreenComponent> ScreenComponents = context.ScreenComponent.Where(s => s.ScreenID == ScreenObject2.ID);

            Screen ScreenObject1 = new Screen();
            ScreenObject1.Module = ScreenObject2.Module;
            ScreenObject1.ModuleID = ScreenObject2.ModuleID;
            //ScreenObject1.ScreenComponents = ScreenObject2.ScreenComponents;

            ScreenObject1.ScreenName = ScreenObject2.ScreenName;
            ScreenObject1.ScreenNameArabic = ScreenObject2.ScreenNameArabic;

            ScreenObject1.ScreenTypes = ScreenObject2.ScreenTypes;
            ScreenObject1.screenTypeID = ScreenObject2.screenTypeID;

           // ScreenObject1.DevName = ScreenObject2.DevName;
            ScreenObject1.EmpName = ScreenObject2.EmpName;
            ScreenObject1.EmpJob = ScreenObject2.EmpJob;
            //ScreenObject1.Date = ScreenObject2.Date;
            ScreenObject1.ImageFile = ScreenObject2.ImageFile;
            ScreenObject1.ImagePathPhysical = ScreenObject2.ImagePathPhysical;
            ScreenObject1.ImageName = ScreenObject2.ImageName;
            ScreenObject1.UserStories = ScreenObject2.UserStories;

            context.Screens.Add(ScreenObject1);
            context.SaveChanges();


            Screen Scr = context.Screens.Last();
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

                context.ScreenComponent.Add(screenComponent);

            }
            context.SaveChanges();

            return RedirectToAction(nameof(Index), new { modulId = ScreenObject1.ModuleID });
        }

    }
}