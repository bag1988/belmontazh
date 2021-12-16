using belmontazh.Areas.Admin.Models;
using belmontazh.Models;
using belmontazh.Models.Repositor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace belmontazh.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    public class ParseController : Controller
    {
        // GET: Admin/Parse
        public ActionResult Index()
        {
            return View();
        }
        //парсинг дверей
        public ActionResult Parse()
        {
            //обработка фото
            //List<string> fil = Directory.GetFiles(Server.MapPath("~/dveriImgGif/"), "*.gif").ToList();
            //foreach (var s in fil)
            //{
            //    FileInfo f = new FileInfo(s);
            //    System.Drawing.Image img = System.Drawing.Image.FromFile(f.FullName);

            //    img.Save(Server.MapPath("~/dveriImgGif/") +Path.GetFileNameWithoutExtension(s) + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            //    img.Dispose();               
            //}
            //конец обработки фото



            //парсинг url
            WebClient wc = new WebClient();
            StreamReader read = new StreamReader(Server.MapPath("~/dveri.txt"));
            List<string> str = new List<string>();

            while (!read.EndOfStream)
            {
                str.Add(read.ReadLine());
            }
            read.Dispose();
            return View(str);


            //получаем url страниц для парсинга
            //WebClient wc = new WebClient();
            //string encodingStr = wc.Encoding.BodyName;
            //wc.Headers.Add("User-Agent", "Mozilla/5.0 (Windows; U; Windows NT 6.1; en-GB; rv:1.9.2.12) Gecko/20101026 Firefox/3.6.12");
            //wc.Headers.Add("Accept", "*/*");
            //StreamWriter strW;
            //string result = "";
            //string pattern = "(?:<a class=\"imglinko\".*href=\")(.*)(?:\")";
            //Regex regex = new Regex(pattern);
            //strW = new StreamWriter(Server.MapPath("/dveri.txt"), true);
            //for (int i = 0; i <= 232; i = i + 33)
            //{
            //    if (i == 0)
            //        result = Encoding.UTF8.GetString(wc.DownloadData("https://www.dveridoff.by/katalog/vhodnye-dveri/?limitstart=0"));
            //    else
            //        result = Encoding.UTF8.GetString(wc.DownloadData("https://www.dveridoff.by/katalog/vhodnye-dveri/?start=" + i.ToString()));

            //    MatchCollection matches = regex.Matches(result);
            //    foreach (Match mat in matches)
            //    {
            //       strW.Write("https://www.dveridoff.by" + mat.Groups[1].Value);
            //       strW.WriteLine();
            //    }
            //}
            //strW.Dispose();
            //return View();
        }

        [HttpPost]
        public string getParse(string url)
        {
            WebClient wc = new WebClient();
            wc.Headers.Add("User-Agent", "Mozilla/5.0 (Windows; U; Windows NT 6.1; en-GB; rv:1.9.2.12) Gecko/20101026 Firefox/3.6.12");
            wc.Headers.Add("Accept", "*/*");
            string result = "";
            using (Stream data = wc.OpenRead(url))
            {
                return result = Encoding.UTF8.GetString(wc.DownloadData(url));                
            }
        }

        [HttpPost]
        public JsonResult saveParse(List<ParseDveri> project)
        {
            DveriKomnat d = new DveriKomnat();
            DveriKomnatModel dModel = new DveriKomnatModel();
            imageDveriModel imgDveri = new imageDveriModel();
            ProizvoditelModel proiz = new ProizvoditelModel();

            if (project.Count > 0)
            {
                dModel.name = project[0].name;
                dModel.cost = project[0].costDveri / 1.4;
                if (project[0].desc == null)
                    project[0].desc = project[0].smallDescript;
                dModel.description = project[0].desc.Replace("\t", "").Replace("\n", "");
                dModel.nameEn = project[0].name.ToTranslit();
                dModel.kategoriModelid = 2;
                if (project[0].proiz == "")
                    project[0].proiz = "Нет";
                if (project[0].podzakaz != null)
                    dModel.presence = true;
                dModel.enable = true;
                using (var dbContext = new belmontazhBase())
                {
                    var pr = dbContext.Proizvoditel.ToList();
                    if (!pr.Exists(x => x.name == project[0].proiz))
                    {
                        proiz.name = project[0].proiz;
                        proiz.nameEn = project[0].proiz.ToTranslit();
                        dbContext.Proizvoditel.Add(proiz);
                        dbContext.SaveChanges();
                    }
                    dModel.ProizvoditelModelid = pr.FirstOrDefault(x => x.name == project[0].proiz).id;
                }
                int idDveri = 0;
                string content = dModel.description;
                string url = "";
                if (project[0].arrayImg!=null && project[0].arrayImg.Count > 0)
                {
                    WebClient webClient = new WebClient();
                    Directory.CreateDirectory(Server.MapPath("~/tempFile/"));
                    string fName = "";
                    FileInfo f;
                    for (int b = 0; b < project[0].arrayImg.Count; b++)
                    {
                        fName = project[0].name.ToTranslit();
                        string inf = Path.GetExtension(project[0].arrayImg[b].url);
                        using (var client = new WebClient())
                        {
                            client.DownloadFile(project[0].arrayImg[b].url, Server.MapPath("~/tempFile/" + b + inf));
                        }
                        f = new FileInfo(Server.MapPath("~/tempFile/" + b + inf));
                        if (f.Length > 0)
                        {
                            var sName = Directory.EnumerateFiles(Server.MapPath("~/images/"), fName + ".*", SearchOption.AllDirectories);
                            if (sName.Count() > 0)
                            {
                                fName = fName + 0;
                                for (int i = 1; i > 0; i++)
                                {
                                    if ((Directory.EnumerateFiles(Server.MapPath("~/images/"), fName + ".*", SearchOption.AllDirectories).Count() == 0))
                                    {
                                        break;
                                    }
                                    fName = fName.Replace((i - 1).ToString(), i.ToString());
                                }
                            }                            
                            SaveImage save = new SaveImage();
                            url = "/images/" + save.Upload(f, 1000, 600, fName, Server.MapPath("~/images/"));
                            content = content.Replace("&lt;img id='" + project[0].arrayImg[b].id + "'/&gt;", "<img src='" + url + "' alt='" + project[0].name + "' title='" + project[0].name + "'/>");
                            content = content.Replace("<img id=\"" + project[0].arrayImg[b].id + "\"/>", "<img src='" + url + "' alt='" + project[0].name + "' title='" + project[0].name + "'/>");
                            content = content.Replace("<img id=\"" + project[0].arrayImg[b].id + "\">", "<img src='" + url + "' alt='" + project[0].name + "' title='" + project[0].name + "'/>");
                        }
                        else
                        {
                            content = content.Replace("&lt;img id='" + project[0].arrayImg[b].id + "'/&gt;", "");
                            content = content.Replace("<img id=\"" + project[0].arrayImg[b].id + "\"/>", "");
                            content = content.Replace("<img id=\"" + project[0].arrayImg[b].id + "\">", "");
                        }

                    }
                    dModel.description = content;
                }


                d.Save(dModel);

                using (var dbContext = new belmontazhBase())
                {
                    idDveri = dbContext.DveriKomnat.Max(x => x.id);
                }



                /*if (project[0].propertyDveri != null)
                {
                    List<valuePropertyModel> valList = new List<valuePropertyModel>();
                    using (var dbContext = new belmontazhBase())
                    {
                        List<propertyDveriModel> propModelList = new List<propertyDveriModel>();
                        foreach (var prop in project[0].propertyDveri)
                        {
                            if (dbContext.propertyDveri.Where(x => x.name == prop.name).Count() == 0)
                            {
                                propertyDveriModel propModel = new propertyDveriModel();
                                propModel.name = prop.name;
                                propModelList.Add(propModel);

                            }
                        }
                        dbContext.propertyDveri.AddRange(propModelList);
                        dbContext.SaveChanges();
                    }
                    using (var dbContext = new belmontazhBase())
                    {
                        var pr = dbContext.propertyDveri.ToList();
                        foreach (var prop in project[0].propertyDveri)
                        {
                            valuePropertyModel val = new valuePropertyModel();
                            val.propertyDveriModelid = pr.Where(x => x.name == prop.name).First().id;
                            val.valueProp = prop.value;
                            val.DveriKomnatModelid = idDveri;
                            valList.Add(val);
                        }
                    }
                    using (var dbContext = new belmontazhBase())
                    {
                        dbContext.valueProperty.AddRange(valList);
                        dbContext.SaveChanges();
                    }
                }*/

                if (project[0].urlImg.Count > 0)
                {
                    List<imageDveriModel> imgList = new List<imageDveriModel>();
                    WebClient webClient = new WebClient();
                    Directory.CreateDirectory(Server.MapPath("~/tempFile/"));
                    Directory.CreateDirectory(Server.MapPath("~/dveriImg/"));
                    string fName = project[0].name.ToTranslit();
                    for (int b = 0; b < project[0].urlImg.Count; b++)
                    {
                        fName = project[0].name.ToTranslit();
                        string inf = Path.GetExtension(project[0].urlImg[b].url);
                        using (var client = new WebClient())
                        {
                            client.DownloadFile(project[0].urlImg[b].url, Server.MapPath("~/tempFile/" + b + inf));
                        }
                        var sName = Directory.EnumerateFiles(Server.MapPath("~/dveriImg/"), fName + ".*", SearchOption.AllDirectories);
                        if (sName.Count() > 0)
                        {
                            fName = fName + 0;
                            for (int i = 1; i > 0; i++)
                            {
                                if ((Directory.EnumerateFiles(Server.MapPath("~/dveriImg/"), fName + "-" + i.ToString() + ".*", SearchOption.AllDirectories).Count() == 0))
                                {
                                    break;
                                }
                                fName = fName.Replace((i - 1).ToString(), i.ToString());
                            }
                        }

                        FileInfo f = new FileInfo(Server.MapPath("~/tempFile/" + b + inf));

                        SaveImage save = new SaveImage();
                        imageDveriModel img = new imageDveriModel();
                        img.DveriKomnatModelid = idDveri;
                        img.urlImage = "/dveriImg/" + save.Upload(f, 1000, 600, fName, Server.MapPath("~/dveriImg/"));
                        imgList.Add(img);
                    }
                    using (var dbContext = new belmontazhBase())
                    {
                        dbContext.imageDveri.AddRange(imgList);
                        dbContext.SaveChanges();
                    }
                }
            }
            return null;
        }

        //парсинг статей
        public ActionResult ParseKnow()
        {
            //парсинг url
            WebClient wc = new WebClient();
            StreamReader read = new StreamReader(Server.MapPath("~/know.txt"));
            List<string> str = new List<string>();

            while (!read.EndOfStream)
            {
                str.Add(read.ReadLine());
            }
            read.Dispose();
            return View(str);


            //получаем url страниц для парсинга
            //WebClient wc = new WebClient();
            //string encodingStr = wc.Encoding.BodyName;
            //wc.Headers.Add("User-Agent", "Mozilla/5.0 (Windows; U; Windows NT 6.1; en-GB; rv:1.9.2.12) Gecko/20101026 Firefox/3.6.12");
            //wc.Headers.Add("Accept", "*/*");
            ////StreamWriter strW;
            //string result = "";
            //string pattern = @"<td.*\W*.*\W*</td>";
            //Regex regex = new Regex(pattern);

            //for (int i = 1; i <= 1; i++)
            //{
            //    result = Encoding.GetEncoding(1251).GetString(wc.DownloadData("https://www.calc.ru/dveri.html"));

            //    MatchCollection matches = regex.Matches(result);


            //    foreach (Match mat in matches)
            //    {
            //        if (Regex.IsMatch(mat.Value, "[^\"]*.html"))
            //        {
            //            ParseKnowUrl("https://www.calc.ru/" + Regex.Match(mat.Value, "[^\"]*.html").Value);
            //        }
            //    }
            //}

            //return View();
        }

        public void ParseKnowUrl(string url)
        {
            //парсинг url
            //WebClient wc = new WebClient();
            //StreamReader read = new StreamReader(Server.MapPath("~/know.txt"));
            //List<string> str = new List<string>();

            //while (!read.EndOfStream)
            //{
            //    str.Add(read.ReadLine());
            //}
            //read.Dispose();
            //return View(str);


            //получаем url страниц для парсинга
            WebClient wc = new WebClient();
            string encodingStr = wc.Encoding.BodyName;
            wc.Headers.Add("User-Agent", "Mozilla/5.0 (Windows; U; Windows NT 6.1; en-GB; rv:1.9.2.12) Gecko/20101026 Firefox/3.6.12");
            wc.Headers.Add("Accept", "*/*");
            StreamWriter strW;
            string result = "";
            string pattern = @"<td.*\W*.*\W*</td>";
            Regex regex = new Regex(pattern);


            result = Encoding.GetEncoding(1251).GetString(wc.DownloadData(url));
            strW = new StreamWriter(Server.MapPath("/know.txt"), true);
            if (Regex.IsMatch(result, "class=\"articleBody\""))
            {
                strW.Write(url);
                strW.WriteLine();
            }
            else
            {
                MatchCollection matches = regex.Matches(result);                
                foreach (Match mat in matches)
                {
                    if (Regex.IsMatch(mat.Value, "[^\"]*.html"))
                    {
                        strW.Write("https://www.calc.ru/" + Regex.Match(mat.Value, "[^\"]*.html").Value);
                        strW.WriteLine();
                    }
                }
               
            }
            strW.Dispose();
        }

        [HttpPost]
        public JsonResult saveParseKnow(List<knowBase> project)
        {

            KnowBase d = new KnowBase();
            knowBaseModel dModel = new knowBaseModel();
            if (project.Count > 0)
            {
                dModel.name = project[0].name;
                dModel.description = project[0].smallDescription.TrimStart(' ');
                dModel.date = DateTime.Now;
                dModel.nameEn = project[0].name.ToTranslit();
                string content = project[0].Content;
                string url = "";
                List<string> urlImage = new List<string>();
                if (project[0].arrayImg.Count > 0)
                {
                    WebClient webClient = new WebClient();
                    Directory.CreateDirectory(Server.MapPath("~/tempFile/"));
                    Directory.CreateDirectory(Server.MapPath("~/knowImages/"));
                    string fName = "";
                    FileInfo f;
                    for (int b = 0; b < project[0].arrayImg.Count; b++)
                    {
                        fName = project[0].name.ToTranslit();
                        string inf = Path.GetExtension(project[0].arrayImg[b].url);
                        using (var client = new WebClient())
                        {
                            client.DownloadFile("https://www.calc.ru"+project[0].arrayImg[b].url, Server.MapPath("~/tempFile/" + b + inf));
                        }
                        f = new FileInfo(Server.MapPath("~/tempFile/" + b + inf));
                        if (f.Length > 0)
                        {
                            var sName = Directory.EnumerateFiles(Server.MapPath("~/knowImages/"), fName + ".*", SearchOption.AllDirectories);
                            if (sName.Count() > 0)
                            {
                                fName = fName + 0;
                                for (int i = 1; i > 0; i++)
                                {
                                    if ((Directory.EnumerateFiles(Server.MapPath("~/knowImages/"), fName + ".*", SearchOption.AllDirectories).Count() == 0))
                                    {
                                        break;
                                    }
                                    fName = fName.Replace((i - 1).ToString(), i.ToString());                                  
                                    //fName = Regex.Replace(fName, @"\d$", i, RegexOptions.Multiline);
                                }
                            }
                            if (b == 0)
                                dModel.nameEn = fName;


                            SaveImage save = new SaveImage();
                            url = "/knowImages/" + save.Upload(f, 1000, 600, fName, Server.MapPath("~/knowImages/"));
                            urlImage.Add(url);
                            content = content.Replace("&lt;img id='" + project[0].arrayImg[b].id + "'/&gt;", "<img src='" + url + "' alt='" + project[0].name + "' title='" + project[0].name + "'/>");
                            content = content.Replace("<img id=\"" + project[0].arrayImg[b].id + "\"/>", "<img src='" + url + "' alt='" + project[0].name + "' title='" + project[0].name + "'/>");
                            content = content.Replace("<img id=\"" + project[0].arrayImg[b].id + "\">", "<img src='" + url + "' alt='" + project[0].name + "' title='" + project[0].name + "'/>");
                        }
                        else
                        {
                            content = content.Replace("&lt;img id='" + project[0].arrayImg[b].id + "'/&gt;", "");
                            content = content.Replace("<img id=\"" + project[0].arrayImg[b].id + "\"/>", "");
                            content = content.Replace("<img id=\"" + project[0].arrayImg[b].id + "\">", "");
                        } 
                                              
                    }
                    dModel.urlImage = urlImage[0];
                    dModel.content = content;

                    d.Save(dModel);
                }
                
            }
            return null;
        }


    }
    public class ParseDveri
    {
        public string name { get; set; }
        public string proiz { get; set; }
        public double costDveri { get; set; }
        public List<UrlImg> urlImg { get; set; }
        public List<PropertyDveri> propertyDveri { get; set; }
        public string desc { get; set; }
        public string smallDescript { get; set; }
        public List<ArrayImg> arrayImg { get; set; }
        public string podzakaz { get; set; }
    }

    public class UrlImg
    {
        public string url { get; set; }
    }

    public class PropertyDveri
    {
        public string name { get; set; }
        public string value { get; set; }
    }

    public class ArrayImg
    {
        public int id { get; set; }
        public string url { get; set; }
    }

    public class knowBase
    {
        public string name { get; set; }
        public string smallDescription { get; set; }
        public string Content { get; set; }
        public List<ArrayImg> arrayImg { get; set; }
    }
}