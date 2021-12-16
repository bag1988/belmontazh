using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.SessionState;

namespace belmontazh.Models
{
    public class knowBaseModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        
        public string nameEn { get; set; }

        [Required]
        [Display(Name = "Заглавие")]
        [StringLength(100, ErrorMessage = "Значение {0} должно содержать символов не менее: {2}.", MinimumLength = 3)]
        public string name { get; set; }

        [Display(Name = "Основная фотография")]
        public string urlImage { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Краткое описание")]
        [MinLength(3, ErrorMessage = "Значение {0} должно содержать символов не менее: {1}.")]
        public string description { get; set; }

        [Required]
        [AllowHtml]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Текст")]
        [MinLength(3, ErrorMessage = "Значение {0} должно содержать символов не менее: {1}.")]
        public string content { get; set; }

        [Display(Name = "Дата публикации")]
        [DataType(DataType.DateTime)]
        public DateTime date { get; set; }
    }

    public class RoleEditModel
    {
        public IdentityRole Role { get; set; }
        public IEnumerable<ApplicationUser> Members { get; set; }
        public IEnumerable<ApplicationUser> NonMembers { get; set; }
    }

    public class RoleModificationModel
    {
        [Required]
        public string RoleName { get; set; }
        public string[] IdsToAdd { get; set; }
        public string[] IdsToDelete { get; set; }
    }

    public class commentModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        
        [Display(Name = "Дата отзыва")]
        [DataType(DataType.DateTime)]
        public DateTime dateComment { get; set; }

        [Display(Name = "Фотография")]
        public string urlImage { get; set; }

        [Required]
        [Display(Name = "Имя")]
        [StringLength(100, ErrorMessage = "Значение {0} должно содержать символов не менее: {2}.", MinimumLength = 3)]
        public string nameComment { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Отзыв")]
        [MinLength(3, ErrorMessage = "Значение {0} должно содержать символов не менее: {1}.")]
        public string description { get; set; }
    }

    public class projectImg
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public int idProject { get; set; }

        [Display(Name = "Миниатюра")]
        public string smalUrlImage { get; set; }

        [Display(Name = "Фотография")]
        public string urlImage { get; set; }
        
        [Display(Name = "Описание")]
        [DataType(DataType.MultilineText)]
        [MinLength(3, ErrorMessage = "Значение {0} должно содержать символов не менее: {1}.")]
        public string description { get; set; }
    }

    public class projectModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; } 

        [Required]
        [Display(Name = "Наименование проекта")]
        [StringLength(100, ErrorMessage = "Значение {0} должно содержать символов не менее: {2}.", MinimumLength = 6)]
        public string name { get; set; }
        
        [Display(Name = "Основная фотография")]
        public string urlImage { get; set; }
             
        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Описание")]
        [MinLength(3, ErrorMessage = "Значение {0} должно содержать символов не менее: {1}.")]
        public string description { get; set; }
    }

    public class DveriKomnatModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public string nameEn { get; set; }

        [Required]
        [Display(Name = "Наименование")]
        [StringLength(100, ErrorMessage = "Значение {0} должно содержать символов не менее: {2}.", MinimumLength = 3)]
        public string name { get; set; }

        [Display(Name = "Включен")]
        public bool enable { get; set; }

        [Display(Name = "Под заказ")]
        public bool presence { get; set; }

        [AllowHtml]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Описание")]
        public string description { get; set; }

        [Required]
        [Display(Name = "Цена")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:f}")]
        public double cost { get; set; }

        [Display(Name = "Категория")]
        [Range(1, int.MaxValue, ErrorMessage = "Значение {0} должно быть больше {1}")]
        public int? kategoriModelid { get; set; }
        public kategoriModel kategoriModel { get; set; }

        [Display(Name = "Производитель")]
        [Range(1, int.MaxValue, ErrorMessage = "Значение {0} должно быть больше {1}")]
        public int? ProizvoditelModelid { get; set; }
        public ProizvoditelModel ProizvoditelModel { get; set; }

        [Display(Name = "Материала отделки")]
        [Range(1, int.MaxValue, ErrorMessage = "Значение {0} должно быть больше {1}")]
        public int? materialDveriModelid { get; set; }
        public materialDveriModel materialDveriModel { get; set; }
        
        [Display(Name = "Характеристики")]
        public virtual ICollection<valuePropertyModel> valueProperty { get; set; }
        [Display(Name = "Изображения")]
        public virtual ICollection<imageDveriModel> imageDveri { get; set; }

        [Display(Name = "Просмотры")]
        public virtual ICollection<ViewsDveriModel> viewsDveri { get; set; }

        [Display(Name = "Комплектация")]
        public virtual ICollection<MouldingsDveri> MouldingsDveri { get; set; }
    }

    public class ProizvoditelModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        [Display(Name = "Производитель")]
        [StringLength(100, ErrorMessage = "Значение {0} должно содержать символов не менее: {2}.", MinimumLength = 3)]
        public string name { get; set; }

        public string nameEn { get; set; }

        [Display(Name = "Страна")]
        [StringLength(100, ErrorMessage = "Значение {0} должно содержать символов не менее: {2}.", MinimumLength = 3)]
        public string country { get; set; }

        [Display(Name = "Краткое описание")]
        [DataType(DataType.MultilineText)]
        [MinLength(3, ErrorMessage = "Значение {0} должно содержать символов не менее: {1}.")]
        public string description { get; set; }
    }

    public class imageDveriModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public int? DveriKomnatModelid { get; set; }
        public DveriKomnatModel DveriKomnatModel { get; set; }

        [Display(Name = "Фотография")]
        public string urlImage { get; set; }
    }

    public class propertyDveriModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Display(Name = "Наименование свойства")]
        public string name { get; set; }
    }

    public class materialDveriModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Display(Name = "Материала отделки")]
        public string name { get; set; }
        public string nameEn { get; set; }
    }

    public class valuePropertyModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public int? DveriKomnatModelid { get; set; }
        public DveriKomnatModel DveriKomnatModel { get; set; }

        public int? propertyDveriModelid { get; set; }
        public propertyDveriModel propertyDveriModel { get; set; }

        [Display(Name = "Значение свойства")]
        public string valueProp { get; set; }
    }

    public class dveriKomplektModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        [Display(Name = "Наименование")]
        [StringLength(100, ErrorMessage = "Значение {0} должно содержать символов не менее: {2}.", MinimumLength = 3)]
        public string name { get; set; }

        [Required]
        [Display(Name = "Цена за ед.")]
        [DisplayFormat(DataFormatString = "{0:f}")]
        [DataType(DataType.Currency)]
        public double cost { get; set; }

        [Required]
        [Display(Name = "Кол-во по умолчанию")]
        [DisplayFormat(DataFormatString = "{0:f}")]
        public double defaultValue { get; set; }
        
        [Display(Name = "Включен в стоимость")]
        public bool defaultSet { get; set; }
    }

    public class MouldingsDveri
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        
        public int? DveriKomnatModelid { get; set; }
        public DveriKomnatModel DveriKomnatModel { get; set; }
        
        public int? dveriKomplektModelId { get; set; }
        public dveriKomplektModel dveriKomplektModel { get; set; }
    }

    public class dveriIncrementModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        [Display(Name = "Коэффициент увелечение стоимости двери")]
        [DisplayFormat(DataFormatString = "{0:f}")]
        public double costDveri { get; set; }

        [Required]
        [Display(Name = "Коэффициент увелечение стоимости комплектации")]
        [DisplayFormat(DataFormatString = "{0:f}")]
        public double costKomplekt { get; set; }
    }

    public class kategoriModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        [Display(Name = "Категория")]
        [StringLength(100, ErrorMessage = "Значение {0} должно содержать символов не менее: {2}.", MinimumLength = 3)]
        public string name { get; set; }

        public string nameEn { get; set; }
    }

    public class unitsModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        [Display(Name = "Ед.изм.")]
        [StringLength(100, ErrorMessage = "Значение {0} должно содержать символов не менее: {2}.", MinimumLength = 1)]
        public string name { get; set; }
    }

    public class ViewsDveriModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public int DveriKomnatModelid { get; set; }
    }

    public class PriceModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Display(Name = "Категория")]
        [Range(1, int.MaxValue, ErrorMessage = "Значение {0} должно быть больше {1}")]
        public int? kategoriPriceModelid { get; set; }
        public kategoriPriceModel kategoriPriceModel { get; set; }

        [Required]
        [Display(Name = "Наименование")]
        [StringLength(100, ErrorMessage = "Значение {0} должно содержать символов не менее: {2}.", MinimumLength = 3)]
        public string name { get; set; }


        [Display(Name = "Ед.изм.")]
        [Range(1, int.MaxValue, ErrorMessage = "Значение {0} должно быть больше {1}")]
        public int? unitsModelid { get; set; }
        public unitsModel unitsModel { get; set; }
        
        [Required]
        [Display(Name = "Цена за ед.")]
        [DisplayFormat(DataFormatString = "{0:f}")]
        [DataType(DataType.Currency)]
        public double cost { get; set; }
    }

    public class kategoriPriceModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        [Display(Name = "Категория")]
        [StringLength(100, ErrorMessage = "Значение {0} должно содержать символов не менее: {2}.", MinimumLength = 3)]
        public string name { get; set; }

        public string nameEn { get; set; }

        public virtual ICollection<PriceModel> PriceModel { get; set; }
    }

    public class oknaCtekloModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        [Display(Name = "Наименование стеклопакета")]
        [StringLength(100, ErrorMessage = "Значение {0} должно содержать символов не менее: {2}.", MinimumLength = 3)]
        public string name { get; set; }

        [Display(Name = "Ед.изм.")]
        [Range(1, int.MaxValue, ErrorMessage = "Значение {0} должно быть больше {1}")]
        public int? unitsModelid { get; set; }
        public unitsModel unitsModel { get; set; }

        [Required]
        [Display(Name = "Цена за ед.")]
        [DisplayFormat(DataFormatString = "{0:f}")]
        [DataType(DataType.Currency)]
        public double cost { get; set; }
    }

    public class oknaProfModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        [Display(Name = "Наименование профиля")]
        [StringLength(100, ErrorMessage = "Значение {0} должно содержать символов не менее: {2}.", MinimumLength = 3)]
        public string name { get; set; }

        [Display(Name = "Ед.изм.")]
        [Range(1, int.MaxValue, ErrorMessage = "Значение {0} должно быть больше {1}")]
        public int? unitsModelid { get; set; }
        public unitsModel unitsModel { get; set; }

        [Required]
        [Display(Name = "Цена за ед.")]
        [DisplayFormat(DataFormatString = "{0:f}")]
        [DataType(DataType.Currency)]
        public double cost { get; set; }
    }

    public class oknaHardwareModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        [Display(Name = "Наименование фурнитуры")]
        [StringLength(100, ErrorMessage = "Значение {0} должно содержать символов не менее: {2}.", MinimumLength = 3)]
        public string name { get; set; }

        [Display(Name = "Ед.изм.")]
        [Range(1, int.MaxValue, ErrorMessage = "Значение {0} должно быть больше {1}")]
        public int? unitsModelid { get; set; }
        public unitsModel unitsModel { get; set; }

        [Required]
        [Display(Name = "Цена за ед.")]
        [DisplayFormat(DataFormatString = "{0:f}")]
        [DataType(DataType.Currency)]
        public double cost { get; set; }
    }

    public class oknaTypeModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Display(Name = "Фотография")]
        public string urlImage { get; set; }

        [Required]
        [Display(Name = "Наименование типа")]
        [StringLength(100, ErrorMessage = "Значение {0} должно содержать символов не менее: {2}.", MinimumLength = 3)]
        public string name { get; set; }

        [Required]
        [Display(Name = "Кол-во створок")]
        public int col { get; set; }

        [Required]
        [Display(Name = "Кол-во открывающихся створок")]
        public int count { get; set; }

        [Required]
        [Display(Name = "Процентное увеличение")]
        [DisplayFormat(DataFormatString = "{0:f}")]
        public double cost { get; set; }
    }

    public class oknaModel
    {
        [Display(Name = "Тип окна")]
        public virtual ICollection<oknaTypeModel> oknaTypeModel { get; set; }
        [Display(Name = "Стеклопакет")]
        public virtual ICollection<oknaCtekloModel> oknaCtekloModel { get; set; }
        [Display(Name = "Профиль")]
        public virtual ICollection<oknaProfModel> oknaProfModel { get; set; }
        [Display(Name = "Фурнитура")]
        public virtual ICollection<oknaHardwareModel> oknaHardwareModel { get; set; }
    }

    public class oknaForm
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Значение {0} должно быть больше {1}")]
        [Display(Name = "Тип окна")]
        public int oknaTypeModel { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Значение {0} должно быть больше {1}")]
        [Display(Name = "Стеклопакет")]
        public int oknaCtekloModel { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Значение {0} должно быть больше {1}")]
        [Display(Name = "Профиль")]
        public int oknaProfModel { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Значение {0} должно быть больше {1}")]
        [Display(Name = "Фурнитура")]
        public int oknaHardwareModel { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Значение {0} должно быть больше {1}")]
        [Display(Name = "Высота, мм")]
        public int height { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Значение {0} должно быть больше {1}")]
        [Display(Name = "Ширина, мм")]
        public int width { get; set; }

        [Display(Name = "Отлив")]
        public bool otliv { get; set; }

        [Display(Name = "Отделка откосов")]
        public bool otkos { get; set; }

        [Display(Name = "Подоконник")]
        public bool podokonik { get; set; }

        [Display(Name = "Москитная сетка")]
        public bool moskit { get; set; }

        [Display(Name = "Монтаж")]
        public bool montazh { get; set; }

    }

    public class oknaMontazhCostModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Required]
        [Display(Name = "Стоимость отлива за м2")]
        public Double otliv { get; set; }
        [Required]
        [Display(Name = "Стоимость отделки откосов за м.пог")]
        public Double otkos { get; set; }
        [Required]
        [Display(Name = "Стоимость подоконника за м2")]
        public Double podokonik { get; set; }
        [Required]
        [Display(Name = "Стоимость монтажа за м2")]
        public Double montazh { get; set; }
        [Required]
        [Display(Name = "Стоимость москитной сетки за шт")]
        public Double moskit { get; set; }

    }

    public class krovliaModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Display(Name = "Категория")]
        public int? krovliaKategoriesModelid { get; set; }
        public krovliaKategoriesModel krovliaKategoriesModel { get; set; }

        [Required]
        [Display(Name = "Наименование")]
        [StringLength(100, ErrorMessage = "Значение {0} должно содержать символов не менее: {2}.", MinimumLength = 3)]
        public string name { get; set; }
        
        [Display(Name = "Ед.изм.")]
        public int? unitsModelid { get; set; }
        public unitsModel unitsModel { get; set; }

        [Display(Name = "")]
        public virtual ICollection<krovliaTypeValueModel> krovliaTypeValueModel { get; set; }
    }

    public class krovliaKategoriesModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Display(Name = "Наименование категории")]
        public string name { get; set; }

        public virtual ICollection<krovliaModel> krovliaModel { get; set; }
    }

    public class krovliaTypeModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Display(Name = "Наименование свойства")]
        public string name { get; set; }
    }
    
    public class krovliaTypeValueModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public int? krovliaModelid { get; set; }
        public krovliaModel krovliaModel { get; set; }

        public int? krovliaTypeModelid { get; set; }
        public krovliaTypeModel krovliaTypeModel { get; set; }

        [Required]
        [Display(Name = "Цена за ед.")]
        [DisplayFormat(DataFormatString = "{0:f}")]
        [DataType(DataType.Currency)]
        public double cost { get; set; }
    }

    public class krovliaCalculate
    {
        [Required]
        [Display(Name = "Ширина основания A, м.")]
        public double widthA { get; set; }
        [Required]
        [Display(Name = "Ширина основания A1, м.")]
        public double width { get; set; }
        [Display(Name = "Ширина основания A2, м.")]
        public double widthTwo { get; set; }
        [Required]
        [Display(Name = "Высота подъема B, м.")]
        public double height { get; set; }
        [Required]
        [Display(Name = "Длина основания D, м.")]
        public double length { get; set; }
        [Required]
        [Display(Name = "Длина свеса C, м.")]
        public double lengthC { get; set; }
        [Required]
        [Display(Name = "Кровельный материал")]
        public int krovMat { get; set; }
    }

    public class racshetKrovlia
    {
        [Display(Name = "Угол наклона крыши, градусов")]
        [DisplayFormat(DataFormatString = "{0:D}")]
        public double ugol { get; set; }
        [Display(Name = "Угол наклона вальмовых стропил, градусов")]
        [DisplayFormat(DataFormatString = "{0:D}")]
        public double ugolBok { get; set; }
        [DisplayFormat(DataFormatString = "{0:F}")]
        [Display(Name = "Площадь поверхности крыши, м2")]
        public double ploshad { get; set; }
        [DisplayFormat(DataFormatString = "{0:F}")]
        [Display(Name = "Длина стропил, м")]
        public double lenghtStrop { get; set; }
        [DisplayFormat(DataFormatString = "{0:F}")]
        [Display(Name = "Длина вальмовых стропил в самой высокой точке, м")]
        public double lenghtStropBok { get; set; }
        [DisplayFormat(DataFormatString = "{0:F}")]
        [Display(Name = "Длина диагональных (накосных) стропил, м")]
        public double lenghtStropD { get; set; }
        [DisplayFormat(DataFormatString = "{0:D}")]
        [Display(Name = "Количество стропил, шт")]
        public double countStrop { get; set; }
        [DisplayFormat(DataFormatString = "{0:D}")]
        [Display(Name = "Количество вальмовых стропил, шт")]
        public double countStropBok { get; set; }
        [DisplayFormat(DataFormatString = "{0:F}")]
        [Display(Name = "Объем бруса для стропил, м3")]
        public double obemStrop { get; set; }
        [DisplayFormat(DataFormatString = "{0:F}")]
        [Display(Name = "Объем бруса для вальмовых стропил, м3")]
        public double obemStropBok { get; set; }
        [DisplayFormat(DataFormatString = "{0:F}")]
        [Display(Name = "Примерный вес стропил, кг")]
        public double vesStrop { get; set; }
        [DisplayFormat(DataFormatString = "{0:D}")]
        [Display(Name = "Количество рядов обрешетки, рядов")]
        public double radObr { get; set; }
        [DisplayFormat(DataFormatString = "{0:D}")]
        [Display(Name = "Количество досок обрешетки стандартной длиной 6 метров, шт")]
        public double countObr { get; set; }
        [DisplayFormat(DataFormatString = "{0:F}")]
        [Display(Name = "Объем досок обрешетки, м3")]
        public double obemObr { get; set; }
        [DisplayFormat(DataFormatString = "{0:F}")]
        [Display(Name = "Примерный вес досок обрешетки, кг")]
        public double vesObr { get; set; }
        [DisplayFormat(DataFormatString = "{0:F}")]
        [Display(Name = "Стоимость работ с укладкой кровельного материала, BYN")]
        public double cost { get; set; }
    }

    public class PageInfo
    {
        public int PageNumber { get; set; } // номер текущей страницы
        public int PageSize { get; set; } // кол-во объектов на странице
        public int TotalItems { get; set; } // всего объектов
        public int TotalPages  // всего страниц
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / PageSize); }
        }
    }

    public static class PagingHelpers
    {
        public static string deleteGetParam(this HtmlHelper html, string deleteParam)
        {            
            string paramUrl = "";
            if (html.ViewContext.HttpContext.Request.QueryString.Count > 0)
            {

                for (int i = 0; i < html.ViewContext.HttpContext.Request.QueryString.Count; i++)
                {
                    if (html.ViewContext.HttpContext.Request.QueryString.Keys[i] != deleteParam && html.ViewContext.HttpContext.Request.QueryString.Keys[i] != "page")
                    {
                        if (paramUrl == "")
                            paramUrl += html.ViewContext.HttpContext.Request.QueryString.Keys[i] + "=" + html.ViewContext.HttpContext.Request.QueryString[i];
                        else
                            paramUrl += "&" + html.ViewContext.HttpContext.Request.QueryString.Keys[i] + "=" + html.ViewContext.HttpContext.Request.QueryString[i];
                    }
                }
            }
            if (paramUrl == "")
                paramUrl = deleteParam + "=";
            else
                paramUrl += "&" + deleteParam + "=";
            return paramUrl;
        }
        public static MvcHtmlString PageLinks(this HtmlHelper html,
            PageInfo pageInfo, string pageUrl)
        {
            StringBuilder result = new StringBuilder();
            int startPage = 1;
            int maxPage = 7;

            if(pageInfo.PageNumber>3)
            {
                startPage = pageInfo.PageNumber - 3;
                maxPage = startPage + 6;
            }            
            string paramUrl = "";
            if (html.ViewContext.HttpContext.Request.QueryString.Count > 0)
            {
                
                for (int i = 0; i < html.ViewContext.HttpContext.Request.QueryString.Count; i++)
                {
                    if (html.ViewContext.HttpContext.Request.QueryString.Keys[i] != "page")
                    {                        
                        paramUrl +="&"+ html.ViewContext.HttpContext.Request.QueryString.Keys[i] + "=" + html.ViewContext.HttpContext.Request.QueryString[i];                        
                    }
                }
            }
            for (int i = startPage; i <= pageInfo.TotalPages && i <= maxPage; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl + "?page=" + i + paramUrl);
                tag.InnerHtml = i.ToString();
                // если текущая страница, то выделяем ее,
                // например, добавляя класс
                if (i == pageInfo.PageNumber)
                {
                    tag.AddCssClass("selected");
                    tag.AddCssClass("btn-primary");
                }
                tag.AddCssClass("btn btn-default");
                result.Append(tag.ToString());
            }
            return MvcHtmlString.Create(result.ToString());
        }
    }

    public static class CheckBoxHelpers
    {
        public static MvcHtmlString CheckBoxSimple(this HtmlHelper htmlHelper, string name, object htmlAttributes)
        {
            string checkBoxWithHidden = htmlHelper.CheckBox(name, htmlAttributes).ToHtmlString().Trim();
            string pureCheckBox = checkBoxWithHidden.Substring(0, checkBoxWithHidden.IndexOf("<input", 1));
            return new MvcHtmlString(pureCheckBox);
        }
    }

    public class ViewDveriPage
    {
        public List<DveriKomnatModel> dveri { get; set; }
        public int PageCount { get; set; }
    }

    public class leftMenuDveri
    {
        public List<ProizvoditelModel> proiz { get; set; }
        public List<materialDveriModel> material { get; set; }
    }
    public class ContractModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        [Display(Name = "Ваше Имя")]
        [StringLength(100, ErrorMessage = "Значение {0} должно содержать символов не менее: {2}.", MinimumLength = 3)]
        public string name { get; set; }

        [Required]
        [Display(Name = "Номер телефона")]
        [DataType(DataType.PhoneNumber)]
        public string phone { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Адрес")]
        public string adres { get; set; }

        [Display(Name = "Выполнен")]
        public bool done { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Дополнительная информация")]
        public string description { get; set; }

        public virtual ICollection<OrdersModel> Orders { get; set; }
    }

    public class OrdersModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public int? ContractModelid { get; set; }
        public ContractModel ContractModel { get; set; }

        [Display(Name = "Состояние заказа")]
        public string contract { get; set; }

        [Display(Name = "Наименование позиции")]
        public string name { get; set; }

        public int dveriId { get; set; }
        [DisplayFormat(DataFormatString = "{0:F}")]
        public double countDveri { get; set; }

        public int komplektId { get; set; }
        [DisplayFormat(DataFormatString = "{0:F}")]
        public double countKomplekt { get; set; }
        [DisplayFormat(DataFormatString = "{0:F}")]
        [Display(Name = "Цена за ед.")]
        public double cost { get; set; }
    }

}
