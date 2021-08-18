using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AnseremCRUD.Models;
using AnseremCRUD.ViewModels;

namespace AnseremCRUD.Controllers
{
    public class SaleController : Controller
    {
        private SaleDBEntities db;
        public SaleController()
        {
            db = new SaleDBEntities();
        }
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetAllSale()
        {
            var saleRecord = (from sale in db.Sales
                              join contact in db.Contacts on sale.ContactId equals contact.ContactId
                              join counterparty in db.Counterparties on sale.CounterpartyId equals counterparty.CounterpartyId
                              join city in db.Counterparties on counterparty.CityId equals city.CityId
                              select new
                              {
                                  SaleId = sale.SaleId,
                                  SaleName = sale.SaleName,
                                  ContactId = sale.ContactId,
                                  ContactPerson = contact.ContactPerson,
                                  ResponsiblePerson = contact.ResponsiblePerson,
                                  CounterpartyId = sale.CounterpartyId,
                                  CounterpartyName = counterparty.CounterpartyName,
                                  CityId = counterparty.CityId,
                                  CityName = city.Cities.CityName
                              }
                             ).ToList();

            return Json( new {Success = true, data = saleRecord } , JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddUpdateSale(SaleViewModel saleVM)
        {
            string Message = "Данные успешно добавлены.";
            if (!ModelState.IsValid)
            {
                var errorList = (from item in ModelState
                                 where item.Value.Errors.Any()
                                 select item.Value.Errors[0].ErrorMessage).ToList();

                return Json( new {Success = false, Message = "Ошибка валидации модели.", ErrorList = errorList });
            }

            Sales sale = db.Sales.SingleOrDefault(model => model.SaleId == saleVM.SaleId) ?? new Sales();
            sale.SaleId = saleVM.SaleId;
            sale.SaleName = saleVM.SaleName;
            sale.CounterpartyId = saleVM.CounterpartyId;
            sale.ContactId = saleVM.ContactId;
            sale.Counterparties.CityId = saleVM.CityId;

            if (saleVM.SaleId == 0)
            {
                Message = "Данные успешно добавлены.";
                db.Sales.Add(sale);
            }

            db.SaveChanges();
            return Json(new { Success = true, Message = Message }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EditSale(int saleId)
        {
            return Json(db.Sales.SingleOrDefault(model => model.SaleId == saleId), JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteSale(int saleId)
        {
            Sales sale = db.Sales.Single(model => model.SaleId == saleId);
            db.Sales.Remove(sale);
            db.SaveChanges();
            return Json(new { Success = true, Message = "Данные успешно удалены." }, JsonRequestBehavior.AllowGet);
        }
        
    }
}