using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using kiemketaisan.Models;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace kiemketaisan.Controllers
{
    public class TaiSansController : Controller
    {
        private Data db = new Data();

        // GET: TaiSans
        public ActionResult Index()
        {
            return View(db.TaiSans.ToList());
        }

        // GET: TaiSans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiSan taiSan = db.TaiSans.Find(id);
            if (taiSan == null)
            {
                return HttpNotFound();
            }
            return View(taiSan);
        }

        // GET: TaiSans/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TaiSans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TenTS,NoiSuDung,NamDVSD,SoKiemKeTT,SoTheoKeToan,NguyenNhan,Gia,TinhTrang,GhiChu")] TaiSan taiSan)
        {
            if (ModelState.IsValid)
            {
                db.TaiSans.Add(taiSan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(taiSan);
        }

        // GET: TaiSans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiSan taiSan = db.TaiSans.Find(id);
            if (taiSan == null)
            {
                return HttpNotFound();
            }
            return View(taiSan);
        }

        // POST: TaiSans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TenTS,NoiSuDung,NamDVSD,SoKiemKeTT,SoTheoKeToan,NguyenNhan,Gia,TinhTrang,GhiChu")] TaiSan taiSan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(taiSan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(taiSan);
        }

        // GET: TaiSans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiSan taiSan = db.TaiSans.Find(id);
            if (taiSan == null)
            {
                return HttpNotFound();
            }
            db.TaiSans.Remove(taiSan);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public Task<ActionResult> ExportExcel()
        {

            var package = new ExcelPackage();

                var listData = db.TaiSans.ToList();
                var maxCol = 13;

                var ws = package.Workbook.Worksheets.Add("Sheet1");
                ws.Column(2).Width = 25;
                ws.Column(3).Width = 25;
                ws.Column(4).Width = 25;
                ws.Column(5).Width = 20;
                ws.Column(6).Width = 20;
                ws.Column(7).Width = 10;
                ws.Column(8).Width = 15;
                ws.Column(9).Width = 23;
                ws.Column(10).Width = 16;
                ws.Column(11).Width = 16;
                ws.Column(12).Width = 16;
                ws.Column(13).Width = 20;
                ws.Cells[1, 1, 1, 3].Merge = true;
                ws.Row(1).Height = 23;
            for(int k = 7; k < 13; k++)
            {
                ws.Row(k).Height = 1;
            }
            ws.Row(13).Height = 30;
            ws.Row(14).Height = 30;

            ws.Cells[1, 1, 1, 3].Value = ("TRƯỜNG ĐẠI HỌC").ToUpper();

                ws.Cells[2, 1, 2, 3].Merge = true;
                ws.Row(2).Height = 23;
                ws.Cells[2, 1, 2, 3].Value = ("KỸ THUẬT - CÔNG NGHỆ CẦN THƠ").ToUpper();

                ws.Cells[3, 1, 3, 3].Merge = true;
                ws.Row(3).Height = 23;
                ws.Cells[3, 1, 3, 3].Style.Font.Bold = true;
                ws.Cells[3, 1, 3, 3].Value = ("ĐƠN VỊ: ………………………").ToUpper();

                ws.Cells[1, 9, 1, 13].Merge = true;
                ws.Cells[1, 9, 1, 13].Style.Font.Bold = true;
                ws.Row(1).Height = 23;
                ws.Cells[1, 9, 1, 13].Value = ("Mẫu số: C53-HD ").ToUpper();

                ws.Cells[2, 9, 2, 13].Merge = true;
                ws.Cells[2, 9, 2, 13].Style.Font.Bold = true;
                ws.Row(2).Height = 23;
                ws.Cells[2, 9, 2, 13].Value = ("(Ban hành theo TT số 107/2017/TT-BTC");

                ws.Cells[3, 9, 3, 13].Merge = true;
                ws.Cells[3, 9, 3, 13].Style.Font.Bold = true;
                ws.Row(3).Height = 23;
                ws.Cells[3, 9, 3, 13].Value = ("(Ngày 10/10/2017 của Bộ Tài chính)");

                ws.Cells[5, 1, 5, 13].Merge = true;
                ws.Cells[5, 1, 5, 13].Style.Font.Bold = true;
                ws.Cells[5, 1, 5, 13].Style.Font.Size = 16;

                ws.Row(5).Height = 23;
                ws.Cells[5, 1, 5, 13].Value = ("BIÊN BẢN KIỂM KÊ TÀI SẢN CỐ ĐỊNH, CCDC NĂM "+DateTime.Now.Year.ToString()).ToUpper();

                ws.Cells[6, 1, 6, 13].Merge = true;
                ws.Cells[6, 1, 6, 13].Style.Font.Italic = true;
                ws.Cells[6, 1, 6, 13].Style.Font.Size = 14;
                ws.Row(6).Height = 23;
                ws.Cells[6, 1, 6, 13].Value = ("Thời gian kiểm kê:… giờ ... ngày ... tháng 01 năm " + DateTime.Now.Year.ToString());

                ws.Cells[1, 1, 6, maxCol].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                ws.Cells[1, 1, 6, maxCol].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                //iRow = 2;
                //ws.Cells[iRow, 1, iRow, maxCol].Style.Font.Bold = true;
                //ws.Cells[iRow, 1, iRow, maxCol].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                //ws.Cells[iRow, 1, iRow, maxCol].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                //icol = 1;
                //ws.Column(icol).Width = 5;
                ws.Row(15).Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                ws.Row(15).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Row(16).Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                ws.Row(16).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Row(15).Style.Font.Bold = true;
                ws.Row(16).Style.Font.Bold = true;

                ws.Cells[15, 1, 16, 1].Merge = true;
                ws.Cells[15, 1,16,1].Value = "STT";

                ws.Cells[15, 2, 16, 2].Merge = true;
                ws.Cells[15, 2, 16, 2].Value = "Tên tài sản cố định";

                ws.Cells[15, 3, 16, 3].Merge = true;
                ws.Cells[15, 3, 16, 3].Value = "Nơi sử dụng";

                ws.Cells[15, 4, 16, 4].Merge = true;
                ws.Cells[15, 4, 16, 4].Value = "Năm đưa \n vào sử dụng";

                ws.Cells[15, 5, 16, 5].Merge = true;
                ws.Cells[15, 5, 16, 5].Value = "Số thực \n tế kiểm kê";

                ws.Cells[15, 6, 16, 6].Merge = true;
                ws.Cells[15, 6, 16, 6].Value = "Số theo \n sổ kế toán";

                ws.Cells[15, 7, 15, 8].Merge = true;
                ws.Cells[15, 7, 16, 8].Value = "Chênh lệch";

                ws.Cells[16,7,16,7].Merge = true;
                ws.Cells[16, 7, 16, 7].Value = "Số lượng";

                ws.Cells[16, 8, 16, 8].Merge = true;
                ws.Cells[16, 8, 16, 8].Value = "Nguyên nhân";

                ws.Cells[15, 9, 16, 9].Merge = true;
                ws.Cells[15, 9, 16, 9].Value = "Nguyên giá \n (ĐVT: 1.000đ)";

                ws.Cells[15, 10, 15, 12].Merge = true;
                ws.Cells[15, 10, 15, 12].Value = "Tình trạng thiết bị";

                ws.Cells[16, 10].Value = "Đang hoạt động";
                ws.Cells[16, 11].Value = "Đang hư hỏng";
                ws.Cells[16, 12].Value = "Chưa sử dụng";

                ws.Cells[15, 13, 16, 13].Merge = true;
                ws.Cells[15, 13, 16, 13].Value = "Ghi chú";
                var iRow = 16;
                var icol = 0;
                var i = 0;
                foreach (var item in listData)
                {
                    i++;
                    iRow++;
                    icol = 1;
                    ws.Cells[iRow, icol++].Value = (i).ToString();
                    ws.Cells[iRow, icol++].Value = item.TenTS;
                    ws.Cells[iRow, icol++].Value = item.NoiSuDung;
                    ws.Cells[iRow, icol++].Value = item.NamDVSD;
                    ws.Cells[iRow, icol++].Value = item.SoKiemKeTT;
                    ws.Cells[iRow, icol++].Value = item.SoTheoKeToan;
                    ws.Cells[iRow, icol++].Value = item.SoTheoKeToan-item.SoKiemKeTT;
                    ws.Cells[iRow, icol++].Value = item.NguyenNhan;
                    ws.Cells[iRow, icol++].Value = item.Gia;
                    ws.Cells[iRow, icol++].Value =item.TinhTrang==1?"x":"";
                    ws.Cells[iRow, icol++].Value =item.TinhTrang==2?"x":"";
                    ws.Cells[iRow, icol++].Value =item.TinhTrang==3?"x":"";
                    ws.Cells[iRow, icol++].Value = item.GhiChu;
                }

            // căn giữa

            // khung viền
            ws.Cells[15, 1, iRow, maxCol].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            ws.Cells[15, 1, iRow, maxCol].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            ws.Cells[15, 1, iRow, maxCol].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            ws.Cells[15, 1, iRow, maxCol].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            ws.Cells[15, 1, iRow, maxCol].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            ws.Cells[15, 1, iRow, maxCol].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            ws.Cells[15, 1, iRow, maxCol].Style.WrapText = true;

            iRow ++;
            ws.Cells[iRow, 8, iRow, 12].Merge = true;
            ws.Cells[iRow, 8, iRow, 12].Style.Font.Italic = true;
            ws.Row(iRow).Height = 23;
            ws.Cells[iRow, 8, iRow, 12].Value = ("Cần Thơ, ngày "+DateTime.Now.Day.ToString()+" tháng "+DateTime.Now.Month.ToString()+" năm "+DateTime.Now.Year.ToString());
            ws.Cells[iRow , 8, iRow , 12].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            ws.Cells[iRow, 8, iRow , 12].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            iRow++;
            ws.Cells[iRow, 3].Style.Font.Bold = true;
            ws.Row(iRow).Height = 23;
            ws.Cells[iRow, 3].Value = ("NGƯỜI LẬP BIỂU");
            ws.Cells[iRow, 3].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            ws.Cells[iRow, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            ws.Cells[iRow, 9, iRow , 11].Merge = true;
            ws.Cells[iRow , 9, iRow , 11].Style.Font.Bold = true;
            ws.Row(iRow ).Height = 23;
            ws.Cells[iRow, 9,iRow,11].Value = ("TRƯỞNG ĐƠN VỊ");
            ws.Cells[iRow, 9, iRow, 11].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            ws.Cells[iRow, 9, iRow, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            System.Web.HttpContext.Current.Response.Clear();
                System.Web.HttpContext.Current.Response.AddHeader("content-disposition", "attachment;  filename=" + string.Format("KiemKeTaiSan_{0}.xlsx", DateTime.Now.ToString("yyyyMMdd_HHmmss")));
                System.Web.HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                System.Web.HttpContext.Current.Response.BinaryWrite(package.GetAsByteArray());
                System.Web.HttpContext.Current.Response.End();
            
            return null;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
