using belmontazh.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace belmontazh.Models.Repositor
{
    public class Comment
    {
        public commentModel Get(int id)
        {
            using (var dbContext = new belmontazhBase())
            {
                var comment = dbContext.comment.First(p => p.id == id);
                return comment;
            }
        }

        public List<commentModel> Get(int pageId=1, int countViews=10, string name = "")
        {
            using (var dbContext = new belmontazhBase())
            {
                List<commentModel> comment = dbContext.comment.Where(x => x.nameComment.Contains(name) || x.description.Contains(name)).OrderBy(x => x.id).Skip(countViews * (pageId - 1)).Take(countViews).ToList();
                return comment;
            }
        }
               
        public int GetCount(string name = "")
        {
            using (var dbContext = new belmontazhBase())
            {
                int project = dbContext.comment.Where(x => x.nameComment.Contains(name) || x.description.Contains(name)).Count();
                return project;
            }
        }

        public void Eddit(commentModel comment)
        {
            using (var dbContext = new belmontazhBase())
            {                
                var dbPerson = dbContext.comment.First(p => p.id == comment.id);
                dbPerson.description = comment.description;
                dbPerson.nameComment = comment.nameComment;
                dbPerson.urlImage = comment.urlImage;
                dbContext.SaveChanges();
                return;
            }
        }

        public void Save(commentModel comment)
        {
            using (var dbContext = new belmontazhBase())
            {
                dbContext.comment.Add(comment);
                dbContext.SaveChanges();
                return;
            }
        }

        public void Delete(int id)
        {
            using (var dbContext = new belmontazhBase())
            {
                var db = dbContext.comment.First(p => p.id == id);
                dbContext.comment.Remove(db);
                dbContext.SaveChanges();
                return;
            }
        }
    }

    public class Project
    {
        public projectModel Get(int id)
        {
            using (var dbContext = new belmontazhBase())
            {
                var project = dbContext.project.First(p => p.id == id);
                return project;
            }
        }

        public List<projectModel> Get(int pageId = 1, int countViews = 10, string name="")
        {
            using (var dbContext = new belmontazhBase())
            {
                List<projectModel> project = dbContext.project.Where(x => x.name.Contains(name) || x.description.Contains(name)).OrderBy(x => x.id).Skip(countViews * (pageId - 1)).Take(countViews).ToList();
                return project;
            }
        }
        
        public int GetCount(string name = "")
        {
            using (var dbContext = new belmontazhBase())
            {
                int project = dbContext.project.Where(x => x.name.Contains(name) || x.description.Contains(name)).Count();
                return project;
            }
        }

        public void Eddit(projectModel project)
        {
            using (var dbContext = new belmontazhBase())
            {
                var db = dbContext.project.First(p => p.id == project.id);
                db.description = project.description;
                db.name = project.name;
                db.urlImage = project.urlImage;
                dbContext.SaveChanges();
                return;
            }
        }

        public void Save(projectModel project)
        {
            using (var dbContext = new belmontazhBase())
            {
                dbContext.project.Add(project);
                dbContext.SaveChanges();
                return;
            }
        }

        public void SaveImg(projectImg project)
        {
            using (var dbContext = new belmontazhBase())
            {
                dbContext.projectImage.Add(project);
                dbContext.SaveChanges();
                return;
            }
        }

        public void Delete(int id)
        {
            using (var dbContext = new belmontazhBase())
            {
                var db = dbContext.project.First(p => p.id == id);
                System.IO.File.Delete(HostingEnvironment.MapPath("~" + db.urlImage));
                dbContext.project.Remove(db);
                dbContext.SaveChanges();
                var image = dbContext.projectImage.Where(x => x.idProject == id).ToList();
                foreach (var img in image)
                {
                    DeleteImage(img.id);
                }
                return;
            }
        }

        public List<projectImg> GetImg(int idProject, int pageId = 1, int countViews = 10)
        {
            using (var dbContext = new belmontazhBase())
            {
                List<projectImg> projectImg = dbContext.projectImage.Where(x => x.idProject == idProject).OrderBy(x => x.id).Skip(countViews * (pageId - 1)).Take(countViews).ToList();
                return projectImg;
            }
        }

        public void DeleteImage(int id)
        {
            using (var dbContext = new belmontazhBase())
            {
                var db = dbContext.projectImage.First(p => p.id == id);
                System.IO.File.Delete(HostingEnvironment.MapPath("~" + db.urlImage));
                System.IO.File.Delete(HostingEnvironment.MapPath("~" + db.smalUrlImage));
                dbContext.projectImage.Remove(db);
                dbContext.SaveChanges();
                return;
            }
        }
    }

    public class KnowBase
    {
        public knowBaseModel Get(int id)
        {
            using (var dbContext = new belmontazhBase())
            {
                var project = dbContext.knowBase.First(p => p.id == id);
                return project;
            }
        }
        public knowBaseModel Get(string nameEn)
        {
            knowBaseModel project = new knowBaseModel();
            using (var dbContext = new belmontazhBase())
            {
                project = dbContext.knowBase.FirstOrDefault(p => p.nameEn == nameEn);
                return project;
            }
        }
        public List<knowBaseModel> Get(int pageId = 1, int countViews = 30, string name="")
        {
            using (var dbContext = new belmontazhBase())
            {
                List<knowBaseModel> project = dbContext.knowBase.Where(x => x.name.Contains(name) || x.content.Contains(name)).OrderBy(x => x.id).Skip(countViews * (pageId - 1)).Take(countViews).ToList();
                return project;
            }
        }

        public int GetCount(string name = "")
        {
            using (var dbContext = new belmontazhBase())
            {
                int project = dbContext.knowBase.Where(x => x.name.Contains(name) || x.content.Contains(name)).Count();
                return project;
            }
        }

        public void Eddit(knowBaseModel project)
        {
            using (var dbContext = new belmontazhBase())
            {
                var db = dbContext.knowBase.First(p => p.id == project.id);
                db.description = project.description;
                db.nameEn = project.nameEn;
                db.name = project.name;
                db.urlImage = project.urlImage;
                db.content = project.content;
                dbContext.SaveChanges();
                return;
            }
        }

        public void Save(knowBaseModel project)
        {
            using (var dbContext = new belmontazhBase())
            {
                dbContext.knowBase.Add(project);
                dbContext.SaveChanges();
                return;
            }
        }

        public void Delete(int id)
        {
            using (var dbContext = new belmontazhBase())
            {
                var db = dbContext.knowBase.First(p => p.id == id);
                System.IO.File.Delete(HostingEnvironment.MapPath("~" + db.urlImage));
                dbContext.knowBase.Remove(db);
                dbContext.SaveChanges();
                return;
            }
        }
    }

    public class DveriKomnat
    {
        public DveriKomnatModel Get(int id)
        {
            DveriKomnatModel project = new DveriKomnatModel();
            using (var dbContext = new belmontazhBase())
            {
                project = dbContext.DveriKomnat.Include("MouldingsDveri").Include("imageDveri").Include("kategoriModel").Include("ProizvoditelModel").Include("valueProperty").First(p => p.id == id);
                return project;
            }
        }

        public DveriKomnatModel Get(string nameEn)
        {
            DveriKomnatModel project = new DveriKomnatModel();
            using (var dbContext = new belmontazhBase())
            {
                project = dbContext.DveriKomnat.Include("MouldingsDveri").Include("imageDveri").Include("kategoriModel").Include("ProizvoditelModel").Include("valueProperty").First(p => p.nameEn == nameEn);
                return project;
            }
        }
        
        public ViewDveriPage Get(int pageId = 1, int countViews = 30, int? katId = null, string material = "", string proiz = "", string sort = "", string name = "")
        {
            DveriKomnatModel p = new DveriKomnatModel();
            ViewDveriPage project = new ViewDveriPage();
            ViewDveriPage project1 = new ViewDveriPage();
            using (var dbContext = new belmontazhBase())
            {
                if (material != null && material != "")
                    project.dveri = dbContext.DveriKomnat.Include("materialDveriModel").Include("imageDveri").Include("kategoriModel").Include("ProizvoditelModel").Include("imageDveri").Include("viewsDveri").Where(x => x.materialDveriModel.nameEn.Contains(material)).ToList();
                else
                {
                    if (proiz != null && proiz != "")
                        project.dveri = dbContext.DveriKomnat.Include("materialDveriModel").Include("imageDveri").Include("kategoriModel").Include("ProizvoditelModel").Include("imageDveri").Include("viewsDveri").Where(x => x.ProizvoditelModel.nameEn.Contains(proiz)).ToList();
                    else
                        project.dveri = dbContext.DveriKomnat.Include("materialDveriModel").Include("imageDveri").Include("kategoriModel").Include("ProizvoditelModel").Include("imageDveri").Include("viewsDveri").ToList();
                }
            }
            if (name != "")
                project.dveri = project.dveri.Where(x => x.name.Contains(name)).ToList();            
            if (katId != null)
                project.dveri = project.dveri.Where(x => x.kategoriModelid == katId).ToList();
            
            project.PageCount = project.dveri.Count;

            switch (sort)
            {
                case "name":
                    project.dveri = project.dveri.OrderBy(x => x.name).Skip(countViews * (pageId - 1)).Take(countViews).ToList();
                    break;
                case "popular":
                    project.dveri = project.dveri.OrderByDescending(x => x.viewsDveri.Count()).Skip(countViews * (pageId - 1)).Take(countViews).ToList();
                    break;
                default:
                    project.dveri = project.dveri.OrderBy(x => x.cost).Skip(countViews * (pageId - 1)).Take(countViews).ToList();
                    break;

            }
            return project;
        }


        public ViewDveriPage GetDveri(int pageId = 1, int countViews = 30, string material="", int katId=1, string sort= "", string name = "")
        {            
            DveriKomnatModel p = new DveriKomnatModel();
            ViewDveriPage project = new ViewDveriPage();
            using (var dbContext = new belmontazhBase())
            {
                switch (sort)
                {                    
                    case "name":
                        project.dveri = dbContext.DveriKomnat.Include("materialDveriModel").Include("imageDveri").Include("kategoriModel").Include("ProizvoditelModel").Include("valueProperty").Where(x => x.enable).Where(x => x.materialDveriModel.nameEn.Contains(material)).Where(x => x.kategoriModelid == katId).Where(x => x.name.Contains(name)).OrderBy(x => x.name).Skip(countViews * (pageId - 1)).Take(countViews).ToList();
                        break;                    
                    case "cost":
                        project.dveri = dbContext.DveriKomnat.Include("materialDveriModel").Include("imageDveri").Include("kategoriModel").Include("ProizvoditelModel").Include("valueProperty").Where(x => x.enable).Where(x => x.materialDveriModel.nameEn.Contains(material)).Where(x => x.kategoriModelid == katId).Where(x => x.name.Contains(name)).OrderBy(x => x.cost).Skip(countViews * (pageId - 1)).Take(countViews).ToList();
                        break;
                    default:
                        project.dveri = dbContext.DveriKomnat.Include("materialDveriModel").Include("imageDveri").Include("kategoriModel").Include("ProizvoditelModel").Include("valueProperty").Include("viewsDveri").Where(x => x.enable).Where(x => x.materialDveriModel.nameEn.Contains(material)).Where(x => x.kategoriModelid == katId).Where(x => x.name.Contains(name)).OrderByDescending(x => x.viewsDveri.Count()).Skip(countViews * (pageId - 1)).Take(countViews).ToList();
                        break;

                }
                
                project.PageCount = dbContext.DveriKomnat.Include("materialDveriModel").Include("kategoriModel").Include("ProizvoditelModel").Where(x => x.enable).Where(x => x.materialDveriModel.nameEn.Contains(material)).Where(x => x.kategoriModelid == katId).Where(x => x.name.Contains(name)).ToList().Count;
                return project;
            }
        }

        public ViewDveriPage GetDveriV(int pageId = 1, int countViews = 30, string proiz = "", int katId=2, string sort = "", string name = "")
        {
            ViewDveriPage project = new ViewDveriPage();

            using (var dbContext = new belmontazhBase())
            {
                switch (sort)
                {                   
                    case "name":
                        project.dveri = dbContext.DveriKomnat.Include("ProizvoditelModel").Include("imageDveri").Include("kategoriModel").Where(x => x.enable).Where(x => x.ProizvoditelModel.nameEn.Contains(proiz)).Where(x => x.kategoriModelid == katId).Where(x => x.name.Contains(name)).OrderBy(x => x.name).Skip(countViews * (pageId - 1)).Take(countViews).ToList();
                        break;                    
                    case "cost":
                        project.dveri = dbContext.DveriKomnat.Include("ProizvoditelModel").Include("imageDveri").Include("kategoriModel").Where(x => x.enable).Where(x => x.ProizvoditelModel.nameEn.Contains(proiz)).Where(x => x.kategoriModelid == katId).Where(x => x.name.Contains(name)).OrderBy(x => x.cost).Skip(countViews * (pageId - 1)).Take(countViews).ToList();
                        break;
                    default:
                        project.dveri = dbContext.DveriKomnat.Include("ProizvoditelModel").Include("imageDveri").Include("kategoriModel").Include("viewsDveri").Where(x => x.enable).Where(x => x.ProizvoditelModel.nameEn.Contains(proiz)).Where(x => x.kategoriModelid == katId).Where(x => x.name.Contains(name)).OrderByDescending(x => x.viewsDveri.Count()).Skip(countViews * (pageId - 1)).Take(countViews).ToList();
                        break;

                }

                project.PageCount = dbContext.DveriKomnat.Include("kategoriModel").Include("ProizvoditelModel").Where(x => x.enable).Where(x => x.ProizvoditelModel.nameEn.Contains(proiz)).Where(x => x.kategoriModelid == katId).Where(x => x.name.Contains(name)).ToList().Count;
                return project;
            }
               
        }

        public int GetCount(string name = "")
        {
            using (var dbContext = new belmontazhBase())
            {
                int project = dbContext.DveriKomnat.Where(x => x.name.Contains(name)).Count();
                return project;
            }
        }

        public void Eddit(DveriKomnatModel project)
        {
            using (var dbContext = new belmontazhBase())
            {
                var db = dbContext.DveriKomnat.First(p => p.id == project.id);

                dbContext.imageDveri.RemoveRange(db.imageDveri);
                dbContext.valueProperty.RemoveRange(db.valueProperty);
                dbContext.Mouldings.RemoveRange(db.MouldingsDveri);

                db.description = project.description;
                db.cost = project.cost;
                db.name = project.name;
                db.materialDveriModelid = project.materialDveriModelid;
                db.enable = project.enable;
                db.imageDveri = project.imageDveri;
                db.valueProperty = project.valueProperty;
                db.kategoriModelid = project.kategoriModelid;
                db.ProizvoditelModelid = project.ProizvoditelModelid;
                db.MouldingsDveri = project.MouldingsDveri;
                db.presence = project.presence;
                dbContext.SaveChanges();
                return;
            }
        }

        public void Save(DveriKomnatModel project)
        {            
            using (var dbContext = new belmontazhBase())
            {                
                dbContext.DveriKomnat.Add(project);
                dbContext.SaveChanges();
                return;
            }
        }

        public void Delete(int id)
        {
            using (var dbContext = new belmontazhBase())
            {
                var db = dbContext.DveriKomnat.First(p => p.id == id);

                var project = dbContext.imageDveri.Where(p => p.id == id).ToList();

                foreach(var img in project)
                {
                    System.IO.File.Delete(HostingEnvironment.MapPath("~" + img.urlImage));
                }

                dbContext.DveriKomnat.Remove(db);
                dbContext.SaveChanges();
                return;
            }
        }

        public void SaveKomplekt(dveriKomplektModel project)
        {
            using (var dbContext = new belmontazhBase())
            {
                dbContext.dveriKomplekt.Add(project);
                dbContext.SaveChanges();
                return;
            }
        }

        public void EdditKomplekt(dveriKomplektModel project)
        {
            using (var dbContext = new belmontazhBase())
            {
                var db = dbContext.dveriKomplekt.First(p => p.id == project.id);
                db.name = project.name;
                db.cost = project.cost;
                db.defaultSet = project.defaultSet;
                db.defaultValue = project.defaultValue;
                dbContext.SaveChanges();
                return;
            }
        }

        public dveriKomplektModel GetKomplekt(int id)
        {
            using (var dbContext = new belmontazhBase())
            {
                var project = dbContext.dveriKomplekt.First(p => p.id == id);
                return project;
            }
        }

        public List<dveriKomplektModel> GetKomplekt()
        {
            using (var dbContext = new belmontazhBase())
            {
                List<dveriKomplektModel> project = dbContext.dveriKomplekt.OrderBy(x => x.name).ToList();
                return project;
            }
        }

        public List<dveriKomplektModel> GetKomplektDveri(int idDveri)
        {
            using (var dbContext = new belmontazhBase())
            {
                List<dveriKomplektModel> project = dbContext.Mouldings.Include("dveriKomplektModel").Where(x => x.DveriKomnatModelid == idDveri).Where(x=>x.dveriKomplektModel.defaultSet==true).Select(x => x.dveriKomplektModel).ToList();
                return project;
            }
        }

        public void DeleteKomplekt(int id)
        {
            using (var dbContext = new belmontazhBase())
            {
                var db = dbContext.dveriKomplekt.First(p => p.id == id);
                dbContext.dveriKomplekt.Remove(db);
                dbContext.SaveChanges();
                return;
            }
        }

        public void SaveIncrement(dveriIncrementModel project)
        {
            using (var dbContext = new belmontazhBase())
            {
                dbContext.dveriIncrement.Add(project);
                dbContext.SaveChanges();
                return;
            }
        }

        public void EdditIncrement(dveriIncrementModel project)
        {
            using (var dbContext = new belmontazhBase())
            {
                var db = dbContext.dveriIncrement.First(p => p.id == project.id);
                db.costDveri = project.costDveri;
                db.costKomplekt = project.costKomplekt;
                dbContext.SaveChanges();
                return;
            }
        }

        public dveriIncrementModel GetIncrement()
        {
            using (var dbContext = new belmontazhBase())
            {
                if (dbContext.dveriIncrement.Count() == 0)
                {
                    dveriIncrementModel d = new dveriIncrementModel();
                    d.costDveri = 1.4;
                    d.costKomplekt = 1.4;
                    SaveIncrement(d);
                }
                var project = dbContext.dveriIncrement.FirstOrDefault();
                return project;
            }
        }

        public void DeleteProizvoditel(int id)
        {
            using (var dbContext = new belmontazhBase())
            {
                var db = dbContext.Proizvoditel.First(p => p.id == id);
                dbContext.Proizvoditel.Remove(db);
                dbContext.SaveChanges();
                return;
            }
        }

        public void DeleteProperty(int id)
        {
            using (var dbContext = new belmontazhBase())
            {
                var db = dbContext.propertyDveri.First(p => p.id == id);
                dbContext.propertyDveri.Remove(db);
                dbContext.SaveChanges();
                return;
            }
        }

        public void DeleteMaterial(int id)
        {
            using (var dbContext = new belmontazhBase())
            {
                var db = dbContext.materialDveri.First(p => p.id == id);
                dbContext.materialDveri.Remove(db);
                dbContext.SaveChanges();
                return;
            }
        }

        public void DeleteKategori(int id)
        {
            using (var dbContext = new belmontazhBase())
            {
                var db = dbContext.Kategori.First(p => p.id == id);
                dbContext.Kategori.Remove(db);
                dbContext.SaveChanges();
                return;
            }
        }

        public void SaveView(int idDveri)
        {
            using (var dbContext = new belmontazhBase())
            {
                dbContext.ViewsDveri.Add(new ViewsDveriModel { DveriKomnatModelid = idDveri });
                dbContext.SaveChanges();
                return;
            }
        }

        public void SaveMouldings(MouldingsDveri project)
        {
            using (var dbContext = new belmontazhBase())
            {
                dbContext.Mouldings.Add(project);
                dbContext.SaveChanges();
                return;
            }
        }

        public void EdditMouldings(MouldingsDveri project)
        {
            using (var dbContext = new belmontazhBase())
            {
                var db = dbContext.Mouldings.First(p => p.id == project.id);
                db.DveriKomnatModelid = project.DveriKomnatModelid;
                db.dveriKomplektModelId = project.dveriKomplektModelId;
                dbContext.SaveChanges();
                return;
            }
        }

        public List<MouldingsDveri> GetMouldings(int idDveri)
        {
            using (var dbContext = new belmontazhBase())
            {
                var project = dbContext.Mouldings.Where(x => x.DveriKomnatModelid == idDveri).ToList();
                return project;
            }
        }
    }

    public class Price
    {
        public PriceModel Get(int id)
        {
            using (var dbContext = new belmontazhBase())
            {
                var project = dbContext.Price.Include("kategoriPriceModel").Include("unitsModel").First(p => p.id == id);
                return project;
            }
        }

        public List<PriceModel> Get(int pageId = 1, int countViews = 3000)
        {
            using (var dbContext = new belmontazhBase())
            {
                List<PriceModel> project = dbContext.Price.Include("kategoriPriceModel").Include("unitsModel").OrderBy(x => x.id).Skip(countViews * (pageId - 1)).Take(countViews).ToList();

                return project;
            }
        }

        public List<kategoriPriceModel> GetByKategories(int pageId = 1, int countViews = 3000)
        {
            List<kategoriPriceModel> project = new List<kategoriPriceModel>();
            using (var dbContext = new belmontazhBase())
            {
                dbContext.Configuration.LazyLoadingEnabled = false;
                project = dbContext.kategoriPrice.ToList();

                foreach (var p in project)
                {
                    p.PriceModel = dbContext.Price.Include("unitsModel").OrderBy(x => x.id).Where(m => m.kategoriPriceModelid == p.id).ToList();
                }
            }
            return project.ToList();
        }

        public int GetCount()
        {
            using (var dbContext = new belmontazhBase())
            {
                int project = dbContext.Price.Count();
                return project;
            }
        }

        public void Eddit(PriceModel project)
        {
            using (var dbContext = new belmontazhBase())
            {
                var db = dbContext.Price.First(p => p.id == project.id);
                db.cost = project.cost;
                db.name = project.name;
                db.unitsModelid = project.unitsModelid;
                db.kategoriPriceModelid = project.kategoriPriceModelid;
                dbContext.SaveChanges();
                return;
            }
        }

        public void Save(PriceModel project)
        {
            using (var dbContext = new belmontazhBase())
            {
                dbContext.Price.Add(project);
                dbContext.SaveChanges();
                return;
            }
        }

        public void Delete(int id)
        {
            using (var dbContext = new belmontazhBase())
            {
                var db = dbContext.Price.First(p => p.id == id);
                dbContext.Price.Remove(db);
                dbContext.SaveChanges();
                return;
            }
        }

        public void DeleteKategori(int id)
        {
            using (var dbContext = new belmontazhBase())
            {
                var db = dbContext.kategoriPrice.First(p => p.id == id);
                dbContext.kategoriPrice.Remove(db);
                dbContext.SaveChanges();
                return;
            }
        }

        public void SaveKategori(kategoriPriceModel project)
        {
            using (var dbContext = new belmontazhBase())
            {
                dbContext.kategoriPrice.Add(project);
                dbContext.SaveChanges();
                return;
            }
        }

        public void EdditKategori(kategoriPriceModel project)
        {
            using (var dbContext = new belmontazhBase())
            {
                var db = dbContext.kategoriPrice.First(p => p.id == project.id);
                db.name = project.name;
                db.nameEn = project.nameEn;
                dbContext.SaveChanges();
                return;
            }
        }

        public kategoriPriceModel GetKategori(int id)
        {
            using (var dbContext = new belmontazhBase())
            {
                var project = dbContext.kategoriPrice.First(p => p.id == id);
                return project;
            }
        }

        public List<kategoriPriceModel> GetKategories()
        {
            using (var dbContext = new belmontazhBase())
            {
                List<kategoriPriceModel> project = dbContext.kategoriPrice.OrderBy(x => x.name).ToList();
                return project;
            }
        }
    }

    public class Units
    {
        public void Save(unitsModel project)
        {
            using (var dbContext = new belmontazhBase())
            {
                dbContext.units.Add(project);
                dbContext.SaveChanges();
                return;
            }
        }

        public void Eddit(unitsModel project)
        {
            using (var dbContext = new belmontazhBase())
            {
                var db = dbContext.units.First(p => p.id == project.id);
                db.name = project.name;
                dbContext.SaveChanges();
                return;
            }
        }
        public void Delete(int id)
        {
            using (var dbContext = new belmontazhBase())
            {
                var db = dbContext.units.First(p => p.id == id);
                dbContext.units.Remove(db);
                dbContext.SaveChanges();
                return;
            }
        }
        public unitsModel Get(int id)
        {
            using (var dbContext = new belmontazhBase())
            {
                var project = dbContext.units.First(p => p.id == id);
                return project;
            }
        }

        public List<unitsModel> Get()
        {
            using (var dbContext = new belmontazhBase())
            {
                List<unitsModel> project = dbContext.units.OrderBy(x => x.name).ToList();
                return project;
            }
        }
    }

    public class Okno
    {
        public oknaModel Get()
        {
            using (var dbContext = new belmontazhBase())
            {
                oknaModel project = new oknaModel();
                project.oknaCtekloModel = dbContext.oknaCteklo.ToList(); 
                project.oknaHardwareModel = dbContext.oknaHardware.ToList();
                project.oknaProfModel = dbContext.oknaProf.ToList();
                project.oknaTypeModel = dbContext.oknaType.ToList();
                return project;
            }
        }

        public void SaveCteklo(oknaCtekloModel project)
        {
            using (var dbContext = new belmontazhBase())
            {
                dbContext.oknaCteklo.Add(project);
                dbContext.SaveChanges();
                return;
            }
        }

        public void EdditCteklo(oknaCtekloModel project)
        {
            using (var dbContext = new belmontazhBase())
            {
                var db = dbContext.oknaCteklo.First(p => p.id == project.id);
                db.name = project.name;
                db.cost = project.cost;
                db.unitsModelid = project.unitsModelid;
                dbContext.SaveChanges();
                return;
            }
        }

        public oknaCtekloModel GetCteklo(int id)
        {
            using (var dbContext = new belmontazhBase())
            {
                var project = dbContext.oknaCteklo.Include("unitsModel").First(p => p.id == id);
                return project;
            }
        }

        public List<oknaCtekloModel> GetCteklo()
        {
            using (var dbContext = new belmontazhBase())
            {
                List<oknaCtekloModel> project = dbContext.oknaCteklo.Include("unitsModel").OrderBy(x => x.name).ToList();
                return project;
            }
        }

        public void DeleteCteklo(int id)
        {
            using (var dbContext = new belmontazhBase())
            {
                var db = dbContext.oknaCteklo.First(p => p.id == id);
                dbContext.oknaCteklo.Remove(db);
                dbContext.SaveChanges();
                return;
            }
        }

        public void SaveProf(oknaProfModel project)
        {
            using (var dbContext = new belmontazhBase())
            {
                dbContext.oknaProf.Add(project);
                dbContext.SaveChanges();
                return;
            }
        }

        public void EdditProf(oknaProfModel project)
        {
            using (var dbContext = new belmontazhBase())
            {
                var db = dbContext.oknaProf.First(p => p.id == project.id);
                db.name = project.name;
                db.cost = project.cost;
                db.unitsModelid = project.unitsModelid;
                dbContext.SaveChanges();
                return;
            }
        }

        public oknaProfModel GetProf(int id)
        {
            using (var dbContext = new belmontazhBase())
            {
                var project = dbContext.oknaProf.Include("unitsModel").First(p => p.id == id);
                return project;
            }
        }

        public List<oknaProfModel> GetProf()
        {
            using (var dbContext = new belmontazhBase())
            {
                List<oknaProfModel> project = dbContext.oknaProf.Include("unitsModel").OrderBy(x => x.name).ToList();
                return project;
            }
        }

        public void DeleteProf(int id)
        {
            using (var dbContext = new belmontazhBase())
            {
                var db = dbContext.oknaProf.First(p => p.id == id);
                dbContext.oknaProf.Remove(db);
                dbContext.SaveChanges();
                return;
            }
        }

        public void SaveHardware(oknaHardwareModel project)
        {
            using (var dbContext = new belmontazhBase())
            {
                dbContext.oknaHardware.Add(project);
                dbContext.SaveChanges();
                return;
            }
        }

        public void EdditHardware(oknaHardwareModel project)
        {
            using (var dbContext = new belmontazhBase())
            {
                var db = dbContext.oknaHardware.First(p => p.id == project.id);
                db.name = project.name;
                db.cost = project.cost;
                db.unitsModelid = project.unitsModelid;
                dbContext.SaveChanges();
                return;
            }
        }

        public oknaHardwareModel GetHardware(int id)
        {
            using (var dbContext = new belmontazhBase())
            {
                var project = dbContext.oknaHardware.Include("unitsModel").First(p => p.id == id);
                return project;
            }
        }

        public List<oknaHardwareModel> GetHardware()
        {
            using (var dbContext = new belmontazhBase())
            {
                List<oknaHardwareModel> project = dbContext.oknaHardware.Include("unitsModel").OrderBy(x => x.name).ToList();
                return project;
            }
        }

        public void DeleteHardware(int id)
        {
            using (var dbContext = new belmontazhBase())
            {
                var db = dbContext.oknaHardware.First(p => p.id == id);
                dbContext.oknaHardware.Remove(db);
                dbContext.SaveChanges();
                return;
            }
        }

        public void SaveType(oknaTypeModel project)
        {
            using (var dbContext = new belmontazhBase())
            {
                dbContext.oknaType.Add(project);
                dbContext.SaveChanges();
                return;
            }
        }

        public void EdditType(oknaTypeModel project)
        {
            using (var dbContext = new belmontazhBase())
            {
                var db = dbContext.oknaType.First(p => p.id == project.id);
                db.name = project.name;
                db.count = project.count;
                db.col = project.col;
                db.cost = project.cost;
                db.urlImage = project.urlImage;
                dbContext.SaveChanges();
                return;
            }
        }

        public oknaTypeModel GetTypes(int id)
        {
            using (var dbContext = new belmontazhBase())
            {
                var project = dbContext.oknaType.First(p => p.id == id);
                return project;
            }
        }

        public List<oknaTypeModel> GetTypes()
        {
            using (var dbContext = new belmontazhBase())
            {
                List<oknaTypeModel> project = dbContext.oknaType.OrderBy(x => x.name).ToList();
                return project;
            }
        }

        public void DeleteTypes(int id)
        {
            using (var dbContext = new belmontazhBase())
            {
                var db = dbContext.oknaType.First(p => p.id == id);
                dbContext.oknaType.Remove(db);
                dbContext.SaveChanges();
                return;
            }
        }

        public void SaveMontazh(oknaMontazhCostModel project)
        {
            using (var dbContext = new belmontazhBase())
            {
                dbContext.oknaMontazhCost.Add(project);
                dbContext.SaveChanges();
                return;
            }
        }

        public void EdditMontazh(oknaMontazhCostModel project)
        {
            using (var dbContext = new belmontazhBase())
            {
                var db = dbContext.oknaMontazhCost.First();
                db.montazh = project.montazh;
                db.moskit = project.moskit;
                db.otkos = project.otkos;
                db.otliv = project.otliv;
                db.podokonik = project.podokonik;
                dbContext.SaveChanges();
                return;
            }
        }

        public oknaMontazhCostModel GetMontazh()
        {
            using (var dbContext = new belmontazhBase())
            {
                if (dbContext.oknaMontazhCost.Count() == 0)
                {
                    oknaMontazhCostModel cost = new oknaMontazhCostModel();
                    cost.montazh = 0;
                    cost.moskit = 0;
                    cost.otkos = 0;
                    cost.otliv = 0;
                    cost.podokonik = 0;
                    SaveMontazh(cost);
                }
                var project = dbContext.oknaMontazhCost.First();
                return project;
            }
        }
    }

    public class Krovlia
    {
        public krovliaModel Get(int id)
        {
            using (var dbContext = new belmontazhBase())
            {
                var project = dbContext.krovlia.Include("krovliaTypeValueModel").Include("krovliaKategoriesModel").Include("unitsModel").First(p => p.id == id);
                return project;
            }
        }

        public List<krovliaModel> Get(int pageId = 1, int countViews = 3000)
        {
            using (var dbContext = new belmontazhBase())
            {
                List<krovliaModel> project = dbContext.krovlia.Include("krovliaTypeValueModel").Include("krovliaKategoriesModel").Include("unitsModel").OrderBy(x => x.id).Skip(countViews * (pageId - 1)).Take(countViews).ToList();

                return project;
            }
        }

        public List<krovliaModel> GetMaterial()
        {
            using (var dbContext = new belmontazhBase())
            {
                List<krovliaModel> project = dbContext.krovlia.Where(x => x.krovliaKategoriesModelid == 1).ToList();
                return project;
            }
        }

        public double GetCost(int idMaterial, int idKrovlia)
        {
            using (var dbContext = new belmontazhBase())
            {
                double project = dbContext.krovliaTypeValue.Where(w => w.krovliaModelid == idMaterial && w.krovliaTypeModelid == idKrovlia).Select(x => x.cost).First();
                return project;
            }
        }

        public List<krovliaKategoriesModel> GetByKategories(int pageId = 1, int countViews = 3000)
        {
            List<krovliaKategoriesModel> project = new List<krovliaKategoriesModel>();
            using (var dbContext = new belmontazhBase())
            {
                dbContext.Configuration.LazyLoadingEnabled = false;
                project = dbContext.krovliaKategories.ToList();

                foreach (var p in project)
                {
                    p.krovliaModel = dbContext.krovlia.Include("krovliaTypeValueModel").Include("unitsModel").OrderBy(x => x.id).Where(m => m.krovliaKategoriesModelid == p.id).ToList();
                    
                }
            }
            return project.ToList();
        }

        public int GetCount()
        {
            using (var dbContext = new belmontazhBase())
            {
                int project = dbContext.krovlia.Count();
                return project;
            }
        }

        public void Eddit(krovliaModel project)
        {
            using (var dbContext = new belmontazhBase())
            {
                var db = dbContext.krovlia.First(p => p.id == project.id);

                dbContext.krovliaTypeValue.RemoveRange(db.krovliaTypeValueModel);

                db.name = project.name;
                db.unitsModelid = project.unitsModelid;
                db.krovliaTypeValueModel = project.krovliaTypeValueModel;
                db.krovliaKategoriesModelid = project.krovliaKategoriesModelid;
                dbContext.SaveChanges();
                return;
            }
        }

        public void Save(krovliaModel project)
        {
            using (var dbContext = new belmontazhBase())
            {
                dbContext.krovlia.Add(project);
                dbContext.SaveChanges();
                return;
            }
        }

        public void Delete(int id)
        {
            using (var dbContext = new belmontazhBase())
            {
                var db = dbContext.krovlia.First(p => p.id == id);
                dbContext.krovlia.Remove(db);
                dbContext.SaveChanges();
                return;
            }
        }

        public void DeleteKategori(int id)
        {
            using (var dbContext = new belmontazhBase())
            {
                var db = dbContext.krovliaKategories.First(p => p.id == id);
                dbContext.krovliaKategories.Remove(db);
                dbContext.SaveChanges();
                return;
            }
        }

        public void SaveKategori(krovliaKategoriesModel project)
        {
            using (var dbContext = new belmontazhBase())
            {
                dbContext.krovliaKategories.Add(project);
                dbContext.SaveChanges();
                return;
            }
        }

        public void EdditKategori(krovliaKategoriesModel project)
        {
            using (var dbContext = new belmontazhBase())
            {
                var db = dbContext.krovliaKategories.First(p => p.id == project.id);
                db.name = project.name;
                dbContext.SaveChanges();
                return;
            }
        }

        public krovliaKategoriesModel GetKategoris(int id)
        {
            using (var dbContext = new belmontazhBase())
            {
                var project = dbContext.krovliaKategories.First(p => p.id == id);
                return project;
            }
        }

        public List<krovliaKategoriesModel> GetKategories()
        {
            using (var dbContext = new belmontazhBase())
            {
                List<krovliaKategoriesModel> project = dbContext.krovliaKategories.ToList();
                return project;
            }
        }

        public void DeleteTypes(int id)
        {
            using (var dbContext = new belmontazhBase())
            {
                var db = dbContext.krovliaType.First(p => p.id == id);
                dbContext.krovliaType.Remove(db);
                dbContext.SaveChanges();
                return;
            }
        }

        public void SaveTypes(krovliaTypeModel project)
        {
            using (var dbContext = new belmontazhBase())
            {
                dbContext.krovliaType.Add(project);
                dbContext.SaveChanges();
                return;
            }
        }

        public void EdditTypes(krovliaTypeModel project)
        {
            using (var dbContext = new belmontazhBase())
            {
                var db = dbContext.krovliaType.First(p => p.id == project.id);
                db.name = project.name;
                dbContext.SaveChanges();
                return;
            }
        }

        public krovliaTypeModel GetTypes(int id)
        {
            using (var dbContext = new belmontazhBase())
            {
                var project = dbContext.krovliaType.First(p => p.id == id);
                return project;
            }
        }

        public List<krovliaTypeModel> GetTypes()
        {
            using (var dbContext = new belmontazhBase())
            {
                List<krovliaTypeModel> project = dbContext.krovliaType.ToList();
                return project;
            }
        }
    }

    public class Orders
    {
        public ContractModel GetContract(int id)
        {
            ContractModel project = new ContractModel();
            using (var dbContext = new belmontazhBase())
            {
                project = dbContext.Contract.Include("Orders").First(p => p.id == id);
                return project;
            }
        }
        public List<ContractModel> GetContract(int pageId = 1, int countViews = 30, bool done = false)
        {
            using (var dbContext = new belmontazhBase())
            {
                List<ContractModel> project = dbContext.Contract.Include("Orders").Where(x => x.done == done).OrderBy(x => x.id).Skip(countViews * (pageId - 1)).Take(countViews).ToList();

                return project;
            }
        }
        public OrdersModel GetOrders(int id)
        {
            OrdersModel project = new OrdersModel();
            using (var dbContext = new belmontazhBase())
            {
                project = dbContext.Orders.First(p => p.id == id);
                return project;
            }
        }

        public int GetCount()
        {
            using (var dbContext = new belmontazhBase())
            {
                int project = dbContext.Contract.Count();
                return project;
            }
        }

        public void Eddit(ContractModel project)
        {
            using (var dbContext = new belmontazhBase())
            {
                var db = dbContext.Contract.First(p => p.id == project.id);
                
                dbContext.Orders.RemoveRange(db.Orders);
                db.name = project.name;
                db.phone = project.phone;
                db.adres = project.adres;
                db.Orders = project.Orders;
                db.description = project.description;
                dbContext.SaveChanges();
                return;
            }
        }

        public void Save(ContractModel project)
        {
            using (var dbContext = new belmontazhBase())
            {
                dbContext.Contract.Add(project);
                dbContext.SaveChanges();
                return;
            }
        }

        public void Done(int id)
        {
            using (var dbContext = new belmontazhBase())
            {
                var db = dbContext.Contract.First(p => p.id == id);
                db.done = true;
                dbContext.SaveChanges();
                return;
            }
        }

        public void EdditOrders(OrdersModel project)
        {
            using (var dbContext = new belmontazhBase())
            {
                var db = dbContext.Orders.First(p => p.id == project.id);
                db.ContractModelid = project.ContractModelid;
                db.contract = project.contract;
                db.dveriId = project.dveriId;
                db.name = project.name;
                db.countDveri = project.countDveri;
                db.komplektId = project.komplektId;
                db.countKomplekt = project.countKomplekt;                
                dbContext.SaveChanges();
                return;
            }
        }

        public void SaveOrders(OrdersModel project)
        {
            using (var dbContext = new belmontazhBase())
            {
                dbContext.Orders.Add(project);
                dbContext.SaveChanges();
                return;
            }
        }

        public void DeleteOrders(int id)
        {
            using (var dbContext = new belmontazhBase())
            {
                var db = dbContext.Orders.First(p => p.id == id);
                dbContext.Orders.Remove(db);
                dbContext.SaveChanges();
                return;
            }
        }
    }

    public class Email
    {
        public bool send_email(string str_messege)
        {
            bool str_error = false;
            try
            {
                string smtpServer = "smtp-mail.outlook.com";
                int smtpPort = 587;
                string emailAdress = "bag@live.ru";
                string password = "artSem23021988";
                //создание письма
                SmtpClient Smtp = new SmtpClient(smtpServer, smtpPort);
                Smtp.EnableSsl =  true;
                Smtp.Credentials = new NetworkCredential(emailAdress, password);
                //Формирование письма
                MailMessage Message = new MailMessage();
                Message.IsBodyHtml = true;
                Message.From = new MailAddress(emailAdress);
                Message.To.Add(new MailAddress(emailAdress));
                Message.Subject = "Новый заказ";
                Message.Body = str_messege;

                Smtp.Send(Message);//отправка
                Message.Dispose();
                str_error = true;
            }
            catch
            {
                str_error = false;
            }
            return str_error;
        }
    }
}