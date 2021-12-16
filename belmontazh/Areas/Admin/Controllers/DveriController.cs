using belmontazh.Models.Repositor;
using belmontazh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using belmontazh.Areas.Admin.Models;
using System.Threading.Tasks;

namespace belmontazh.Areas.Admin.Controllers
{    
    [Authorize(Roles = "admin")]
    public class DveriController : Controller
    {
        private static string sortDveri;
        private static int? katId;
        private static string proiz;
        private static string material;
        // GET: Admin/Dveri
        public ActionResult Index(int page=1, string name="", string sort = "", int? typeDveri=null, string typeProiz="", string typeMaterial="")
        {
            int pageSize = 32;

            if (typeDveri == 0)
                katId = typeDveri = null;
            if (typeProiz == "all")
                proiz = typeProiz = "";
            if (typeMaterial == "all")
                material = typeMaterial = "";

            if (sort != "")
                sortDveri = sort;
            if (typeDveri!=null&&typeDveri != katId)
            {
                proiz = "";
                material = "";
                katId = typeDveri;
            }
            switch(katId)
            {
                case 2:
                    using (var dbContext = new belmontazhBase())
                    {
                        ViewBag.ProizList = dbContext.DveriKomnat.Include("ProizvoditelModel").Where(x => x.kategoriModelid == 2).GroupBy(x => x.ProizvoditelModel).Select(x => x.Key).ToList();
                    }
                    break;
                case 1:
                    using (var dbContext = new belmontazhBase())
                    {
                        ViewBag.MaterialList = dbContext.DveriKomnat.Include("materialDveriModel").Where(x => x.kategoriModelid == 1).GroupBy(x => x.materialDveriModel).Select(x => x.Key).ToList();
                    }
                    break;
            }
            if (typeProiz != "")
            {
                proiz = typeProiz;
                material = "";
            }
            if (typeMaterial != "")
            {
                material = typeMaterial;
                proiz = "";
            }
            
            ViewBag.Sort = sortDveri;
            ViewBag.typeDveri = katId;
            ViewBag.typeProiz = proiz;
            ViewBag.typeMaterial = material;
            ViewBag.typeDveri = katId;

            var p = new DveriKomnat();
            var dveri = p.Get(page, pageSize, katId, material, proiz, sortDveri, name);
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = dveri.PageCount };

            ViewBag.PageInfo = pageInfo;
            
            return View(dveri.dveri.ToList());
        }
        
        public ActionResult Create()
        {
            DveriKomnatModel project = new DveriKomnatModel();
            using (var dbContext = new belmontazhBase())
            {
                ViewBag.Proizvoditel = new SelectList(dbContext.Proizvoditel.ToList(), "id", "name");
                ViewBag.Material = new SelectList(dbContext.materialDveri.ToList(), "id", "name");
                ViewBag.Property = new SelectList(dbContext.propertyDveri.ToList(), "id", "name");
                ViewBag.Kategori = new SelectList(dbContext.Kategori.ToList(), "id", "name");
                ViewBag.Komplekt = dbContext.dveriKomplekt.ToList();
                
            }
            return View(project);
        }

        
        [HttpPost]
        public ActionResult Create(DveriKomnatModel project, HttpPostedFileBase[] image)
        {
            bool nameFind = false;  
            if (ModelState.IsValid)
            {                
                var p = new DveriKomnat();
                project.nameEn = project.name.ToTranslit();
                using (var dbContext = new belmontazhBase())
                {
                    if(dbContext.DveriKomnat.Where(x => x.nameEn == project.nameEn).Count() >0)
                    {
                        ModelState.AddModelError("Наименование", "Данное наименование уже существует в базе! Измените наименование!");
                        nameFind = true;
                    }
                }
                if (!nameFind)
                {
                    project.MouldingsDveri = project.MouldingsDveri.Where(x => x.dveriKomplektModelId != null).ToList();
                    project.valueProperty = project.valueProperty.Where(x => x.valueProp != null && x.valueProp != "").ToList();

                    p.Save(project);

                    int idDveri = 0;

                    using (var dbContext = new belmontazhBase())
                    {
                        idDveri = dbContext.DveriKomnat.Max(x => x.id);
                    }
                    List<imageDveriModel> imgList = new List<imageDveriModel>();

                    string fName = "";
                    if (image.Length > 0 && image[0] != null)
                    {
                        Directory.CreateDirectory(Server.MapPath("~/dveriImg/"));
                        for (int b = 0; b < image.Length; b++)
                        {
                            FileInfo fInfo = new FileInfo(image[b].FileName);
                            fName = project.name.ToTranslit();
                            var sName = Directory.EnumerateFiles(Server.MapPath("~/dveriImg/"), fName + ".*", SearchOption.AllDirectories);
                            if (sName.Count() > 0)
                            {
                                fName = fName + 0;
                                for (int i = 1; i > 0; i++)
                                {
                                    if ((Directory.EnumerateFiles(Server.MapPath("~/dveriImg/"), fName + ".*", SearchOption.AllDirectories).Count() == 0))
                                    {
                                        break;
                                    }
                                    fName = fName.Replace((i - 1).ToString(), i.ToString());
                                }
                            }
                            SaveImage save = new SaveImage();
                            imageDveriModel img = new imageDveriModel();
                            img.DveriKomnatModelid = idDveri;
                            img.urlImage = "/dveriImg/" + save.Upload(image[b], 1000, 600, fName, Server.MapPath("~/dveriImg/"));
                            imgList.Add(img);
                        }
                        using (var dbContext = new belmontazhBase())
                        {
                            dbContext.imageDveri.AddRange(imgList);
                            dbContext.SaveChanges();
                        }
                    }
                    return RedirectToAction("Index", "Dveri");
                }
            }

            using (var dbContext = new belmontazhBase())
            {
                ViewBag.Proizvoditel = new SelectList(dbContext.Proizvoditel.ToList(), "id", "name");
                ViewBag.Material = new SelectList(dbContext.materialDveri.ToList(), "id", "name");
                ViewBag.Property = new SelectList(dbContext.propertyDveri.ToList(), "id", "name");
                ViewBag.Kategori = new SelectList(dbContext.Kategori.ToList(), "id", "name");
                ViewBag.Komplekt = dbContext.dveriKomplekt.ToList();
            }
            return View(project);
        }

       
        public ActionResult Edit(int id)
        {
            var p = new DveriKomnat();
            using (var dbContext = new belmontazhBase())
            {
                ViewBag.Proizvoditel = new SelectList(dbContext.Proizvoditel.ToList(), "id", "name");
                ViewBag.Material = new SelectList(dbContext.materialDveri.ToList(), "id", "name");
                ViewBag.Property = new SelectList(dbContext.propertyDveri.ToList(), "id", "name");
                ViewBag.Kategori = new SelectList(dbContext.Kategori.ToList(), "id", "name");
                ViewBag.Komplekt = ViewBag.Komplekt = dbContext.dveriKomplekt.ToList();

            }
            return View(p.Get(id));
        }

        
        [HttpPost]
        public ActionResult Edit(DveriKomnatModel project, HttpPostedFileBase[] image)
        {
            bool nameFind = false;            
            if (ModelState.IsValid)
            {
                project.MouldingsDveri = project.MouldingsDveri.Where(x => x.dveriKomplektModelId != null).ToList();
                project.valueProperty = project.valueProperty.Where(x => x.valueProp != null && x.valueProp != "").ToList();
                project.nameEn = project.name.ToTranslit();
                using (var dbContext = new belmontazhBase())
                {
                    if (dbContext.DveriKomnat.Where(x=>x.id!=project.id).Where(x => x.nameEn == project.nameEn).Count() > 0)
                    {
                        ModelState.AddModelError("Наименование", "Данное наименование уже существует в базе! Измените наименование!");
                        nameFind = true;
                    }
                }
                if (!nameFind)
                {
                    

                    List<imageDveriModel> imgList = new List<imageDveriModel>();
                    if (project.imageDveri != null)
                        imgList = project.imageDveri.Where(x => x.urlImage != null).ToList();
                    string fName = "";
                    if (image.Length > 0 && image[0] != null)
                    {
                        Directory.CreateDirectory(Server.MapPath("~/dveriImg/"));
                        for (int b = 0; b < image.Length; b++)
                        {
                            FileInfo fInfo = new FileInfo(image[b].FileName);
                            fName = project.name.ToTranslit();
                            var sName = Directory.EnumerateFiles(Server.MapPath("~/dveriImg/"), fName + ".*", SearchOption.AllDirectories);
                            if (sName.Count() > 0)
                            {
                                for (int i = 1; i > 0; i++)
                                {
                                    if ((Directory.EnumerateFiles(Server.MapPath("~/dveriImg/"), fName + "-" + i.ToString() + ".*", SearchOption.AllDirectories).Count() == 0))
                                    {
                                        fName += "-" + i.ToString();
                                        break;
                                    }
                                }
                            }
                            SaveImage save = new SaveImage();
                            imageDveriModel img = new imageDveriModel();
                            img.DveriKomnatModelid = project.id;
                            img.urlImage = "/dveriImg/" + save.Upload(image[b], 1000, 600, fName, Server.MapPath("~/dveriImg/"));
                            imgList.Add(img);
                        }
                    }
                    project.imageDveri = imgList;
                    var p = new DveriKomnat();
                    p.Eddit(project);
                    return RedirectToAction("Index", "Dveri");
                }
            }
            using (var dbContext = new belmontazhBase())
            {
                ViewBag.Proizvoditel = new SelectList(dbContext.Proizvoditel.ToList(), "id", "name");
                ViewBag.Material = new SelectList(dbContext.materialDveri.ToList(), "id", "name");
                ViewBag.Property = new SelectList(dbContext.propertyDveri.ToList(), "id", "name");
                ViewBag.Kategori = new SelectList(dbContext.Kategori.ToList(), "id", "name");
                ViewBag.Komplekt = dbContext.dveriKomplekt.ToList();
            }
            return View(project);
        }

        
        public ActionResult Delete(int id)
        {
            var p = new DveriKomnat();
            return View(p.Get(id));
        }

        [HttpPost]
        public ActionResult Delete(int id, DveriKomnatModel project)
        {
            var p = new DveriKomnat();
            p.Delete(id);
            return RedirectToAction("Index");
        }

        // GET: Admin/Dveri/Create
        public ActionResult CreateProperty()
        {
            using (var dbContext = new belmontazhBase())
            {
                ViewBag.List = dbContext.propertyDveri.ToList();
                return View();
            }
        }

       
        public ActionResult DeleteProperty(int id)
        {
            var p = new DveriKomnat();
            p.DeleteProperty(id);
            return RedirectToAction("CreateProperty");
        }

        [HttpPost]
        public ActionResult CreateProperty(propertyDveriModel project)
        {
            if (ModelState.IsValid)
            {
                if (project.id != 0)
                {
                    using (var dbContext = new belmontazhBase())
                    {
                        var db = dbContext.propertyDveri.First(p => p.id == project.id);
                        db.name = project.name;
                        dbContext.SaveChanges();
                    }
                }
                else
                {
                    using (var dbContext = new belmontazhBase())
                    {
                        dbContext.propertyDveri.Add(project);
                        dbContext.SaveChanges();
                    }
                }                
                return RedirectToAction("CreateProperty", "Dveri");
            }            
            using (var dbContext = new belmontazhBase())
            {
                ViewBag.List = dbContext.propertyDveri.ToList();
            }           
            return View(project);
        }

        public ActionResult CreateProizvoditel()
        {
            using (var dbContext = new belmontazhBase())
            {
                ViewBag.List = dbContext.Proizvoditel.ToList();
                return View();
            }
        }

      
        public ActionResult DeleteProizvoditel(int id)
        {
            var p = new DveriKomnat();
            p.DeleteProizvoditel(id);
            return RedirectToAction("CreateProizvoditel");
        }

        [HttpPost]
        public ActionResult CreateProizvoditel(ProizvoditelModel project)
        {
            if (ModelState.IsValid)
            {
                if (project.id != 0)
                {
                    using (var dbContext = new belmontazhBase())
                    {
                        var db = dbContext.Proizvoditel.First(p => p.id == project.id);
                        db.name = project.name;
                        db.nameEn = project.name.ToTranslit();
                        db.description = project.description;
                        db.country = project.country;
                        dbContext.SaveChanges();
                    }
                }
                else
                {
                    using (var dbContext = new belmontazhBase())
                    {
                        dbContext.Proizvoditel.Add(project);
                        dbContext.SaveChanges();
                    }
                }                
                return RedirectToAction("CreateProizvoditel", "Dveri");
            }           
            using (var dbContext = new belmontazhBase())
            {
                ViewBag.List = dbContext.Proizvoditel.ToList();
            }
            return View(project);
        }

        public ActionResult CreateMaterial()
        {
            using (var dbContext = new belmontazhBase())
            {
                ViewBag.List = dbContext.materialDveri.ToList();
                return View();
            }
        }
        
        public ActionResult DeleteMaterial(int id)
        {
            var p = new DveriKomnat();
            p.DeleteMaterial(id);
            return RedirectToAction("CreateMaterial");
        }
        [HttpPost]
        public ActionResult CreateMaterial(materialDveriModel project)
        {
            if (ModelState.IsValid)
            {
                if (project.id != 0)
                {
                    using (var dbContext = new belmontazhBase())
                    {
                        var db = dbContext.materialDveri.First(p => p.id == project.id);
                        db.name = project.name;
                        db.nameEn = project.name.ToTranslit();
                        dbContext.SaveChanges();
                    }
                }
                else
                {
                    using (var dbContext = new belmontazhBase())
                    {
                        project.nameEn = project.name.ToTranslit();
                        dbContext.materialDveri.Add(project);
                        dbContext.SaveChanges();
                    }
                }                
                return RedirectToAction("CreateMaterial", "Dveri");
            }            
            using (var dbContext = new belmontazhBase())
            {
                ViewBag.List = dbContext.materialDveri.ToList();
            }
            return View(project);
        }

        public ActionResult CreateKategori()
        {
            using (var dbContext = new belmontazhBase())
            {
                ViewBag.List = dbContext.Kategori.ToList();
                return View();
            }
        }
       
        public ActionResult DeleteKategori(int id)
        {
            var p = new DveriKomnat();
            p.DeleteKategori(id);
            return RedirectToAction("CreateKategori");
        }
        [HttpPost]
        public ActionResult CreateKategori(kategoriModel project)
        {
            if (ModelState.IsValid)
            {
                if (project.id != 0)
                {
                    using (var dbContext = new belmontazhBase())
                    {
                        var db = dbContext.Kategori.First(p => p.id == project.id);
                        db.name = project.name;
                        db.nameEn = project.name.ToTranslit();
                        dbContext.SaveChanges();
                    }
                }
                else
                {
                    using (var dbContext = new belmontazhBase())
                    {
                        project.nameEn = project.name.ToTranslit();
                        dbContext.Kategori.Add(project);
                        dbContext.SaveChanges();
                    }
                }
                return RedirectToAction("CreateKategori", "Dveri");
            }
            
            using (var dbContext = new belmontazhBase())
            {
                ViewBag.List = dbContext.Kategori.ToList();
            }
            return View(project);
        }

        [ChildActionOnly]
        public ActionResult valuePropertyResult(List<valuePropertyModel> valueProperty, List<valuePropertyModel> modelProperty)
        {
           if (valueProperty == null) valueProperty = modelProperty;
           List<valuePropertyModel> valProp = new List<valuePropertyModel>();
            
            List<propertyDveriModel> property = new List<propertyDveriModel>();
            using (var dbContext = new belmontazhBase())
            {
                property = dbContext.propertyDveri.ToList();
            }

            foreach(propertyDveriModel p in property)
            {
                valuePropertyModel v = new valuePropertyModel();
                v.propertyDveriModelid = p.id;
                v.propertyDveriModel = new propertyDveriModel();
                v.propertyDveriModel.name = p.name;
                if(valueProperty != null && valueProperty.Exists(x=>x.propertyDveriModelid==p.id))
                {
                    v.valueProp = valueProperty.First(x => x.propertyDveriModelid == p.id).valueProp;
                }
                valProp.Add(v);
            }

            return PartialView("valuePropertyResult", valProp);
        }

        public ActionResult Increment()
        {
            var p = new DveriKomnat();
            return View(p.GetIncrement());
        }
        [HttpPost]
        public ActionResult Increment(dveriIncrementModel project)
        {
            if (ModelState.IsValid)
            {
                var p = new DveriKomnat();
                p.EdditIncrement(project);
                ViewBag.Status = "Изменения успешно сохранены!";
            }            
            return View(project);
        }

        public ActionResult CreateKomplekt()
        {
            var p = new DveriKomnat();
            ViewBag.List = p.GetKomplekt().ToList();
            return View();
        }
       
        public ActionResult DeleteKomplekt(int id)
        {
            var p = new DveriKomnat();
            p.DeleteKomplekt(id);
            return RedirectToAction("CreateKomplekt");
        }
        [HttpPost]
        public ActionResult CreateKomplekt(dveriKomplektModel project)
        {
            var p = new DveriKomnat();
            if (ModelState.IsValid)
            {
                if (project.id != 0)
                {
                    p.EdditKomplekt(project);
                }
                else
                    p.SaveKomplekt(project);                
                return RedirectToAction("CreateKomplekt", "Dveri");
            }            
            ViewBag.List = p.GetKomplekt().ToList();
            return View(project);
        }
    }
}
