using KKHondaBackend.Data;
using KKHondaBackend.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using KKHondaBackend.Entities;
using System;
using KKHondaBackend.Services;
using System.Data;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Net.Http;
using System.Web.Script.Serialization;

namespace KKHondaBackend.Controllers.mcs
{



    [Route("api/pss")]
    public class pssController : Controller
    {
        private readonly dbwebContext ctx;
        private readonly ISysParameterService iSysParamService;
        private readonly ICustomerServices iCustomer;
        private readonly IHostingEnvironment _hostingEnvironment;

        public pssController(dbwebContext _ctx,
            ISysParameterService isysParamService,
            ICustomerServices _iCustomer,
            IHostingEnvironment hostingEnvironment
            )
        {
            ctx = _ctx;
            iSysParamService = isysParamService;
            iCustomer = _iCustomer;
            _hostingEnvironment = hostingEnvironment;
        }


        [HttpGet("receive_list")]
        public IActionResult receive_list()
        {

            var list = (from h in ctx.ReceiveH

                        join _dealer in ctx.MDealer on h.dealer_code equals _dealer.dealer_code into dealer1
                        from dealer in dealer1.DefaultIfEmpty()

                        join _status in ctx.Information
                        on (from inf in ctx.Information
                            where inf.code_type == "REC_STATUS" && inf.code_id == h.receive_status
                            select inf.id).FirstOrDefault()
                        equals _status.id
                        into status1
                        from status in status1.DefaultIfEmpty()

                        join _type in ctx.Information
                        on (from inf in ctx.Information
                            where inf.code_type == "REC_TYPE" && inf.code_id == h.receive_type
                            select inf.id).FirstOrDefault()
                        equals _type.id
                        into type1
                        from type in type1.DefaultIfEmpty()

                        join _rec in ctx.User on h.receive_id equals _rec.Id into rec1
                        from rec in rec1.DefaultIfEmpty()
                        join _cr in ctx.User on h.create_id equals _cr.Id into cr1
                        from cre in cr1.DefaultIfEmpty()
                        join _up in ctx.User on h.update_id equals _up.Id into up1
                        from upd in up1.DefaultIfEmpty()
                        where h.receive_no.StartsWith("RC")
                        select new ReceiveHRes
                        {
                            id = h.id,
                            receive_no = h.receive_no,
                            receive_id = h.receive_id,
                            receive_name = rec.FullName,
                            receive_date = h.receive_date,
                            receive_status = h.receive_status,
                            receive_status_name = status.code_value,
                            receive_type = h.receive_type,
                            receive_type_name = type.code_value,
                            dealer_code = h.dealer_code,
                            dealer_name = dealer.dealer_name_th,
                            purchase_no = h.purchase_no,
                            remark = h.remark,
                            create_id = h.create_id,
                            create_name = cre.FullName,
                            create_date = h.create_date,
                            update_id = h.update_id,
                            update_name = upd.FullName,
                            update_date = h.update_date,
                        }).OrderByDescending(x => x.id).AsNoTracking();

            return Ok(list);
        }

        [HttpGet("receive_detail")]
        public IActionResult receive_detail(string receive_no)
        {

            var detail_list = (from d in ctx.ReceiveD

                               join _product in ctx.Product on d.item_id equals _product.ItemId into product1
                               from product in product1.DefaultIfEmpty()

                                   //join _cat in ctx.ProductCategory on d.cat_id equals _cat.CatId into cat1
                                   //from cat in cat1.DefaultIfEmpty()

                                   //join _brand in ctx.ProductBrand on d.brand_id equals _brand.BrandId into brand1
                                   //from brand in brand1.DefaultIfEmpty()

                               join _model in ctx.ProductModel on d.model_id equals _model.ModelId into model1
                               from model in model1.DefaultIfEmpty()

                               //join _type in ctx.ProductType on d.type_id equals _type.TypeId into type1
                               //from type in type1.DefaultIfEmpty()

                               //join _color in ctx.ProductColor on d.color_id equals _color.ColorId into color1
                               //from color in color1.DefaultIfEmpty()

                               join _branch in ctx.Branch on d.branch_id equals _branch.BranchId into branch1
                               from branch in branch1.DefaultIfEmpty()

                               join _wh in ctx.WarehouseLocation on d.whl_id equals _wh.WhlId into wh1
                               from wh in wh1.DefaultIfEmpty()

                               join _cr in ctx.User on d.create_id equals _cr.Id into cr1
                               from cre in cr1.DefaultIfEmpty()

                               join _up in ctx.User on d.update_id equals _up.Id into up1
                               from upd in up1.DefaultIfEmpty()

                               where d.receive_no == receive_no

                               select new ReceiveDRes
                               {
                                   id = d.id,
                                   receive_no = d.receive_no,
                                   dealer_no = d.dealer_no,

                                   //cat_id = d.cat_id,
                                   //cat_code = cat.CatName,
                                   //brand_id = d.brand_id,
                                   //brand_code = brand.BrandCode,
                                   model_id = d.model_id,
                                   model_code = model.ModelCode,
                                   //type_id = d.type_id,
                                   //type_code = model.ModelType,
                                   //color_id = d.color_id,
                                   //color_code = color.ColorCode,

                                   //frame_no = d.frame_no,
                                   //engine_no = d.engine_no,
                                   delivery_no = d.delivery_no,
                                   delivery_date = d.delivery_date,
                                   invoice_no = d.invoice_no,
                                   tax_invoice_no = d.tax_invoice_no,

                                   create_id = d.create_id,
                                   create_name = cre.FullName,
                                   create_date = d.create_date,
                                   update_id = d.update_id,
                                   update_name = upd.FullName,
                                   update_date = d.update_date,

                                   license_no = d.license_no,
                                   branch_id = d.branch_id,
                                   branch_code = branch.BranchName,
                                   line_remark = d.line_remark,
                                   line_status = d.line_status,
                                   //line_status_name = ReceiveLineStatus.listLineStatus.FirstOrDefault(o => o.Id == d.line_status).Desc,
                                   cost_inc_vat = d.cost_inc_vat,
                                   vat_flag = d.vat_flag,
                                   vat_rate = d.vat_rate,
                                   cost_vat = d.cost_vat,
                                   cost_exc_vat = d.cost_exc_vat,
                                   cost_other_exc_vat = d.cost_other_exc_vat,
                                   cost_repair_exc_vat = d.cost_repair_exc_vat,
                                   whl_id = d.whl_id,
                                   whl_code = wh.WhlCode + " " + wh.WhlName,
                                   log_id = d.log_id,
                                   item_id = d.item_id,
                                   province_code = d.province_code,
                                   part_code = product.PartCode,
                                   part_name = product.PartName,
                                   receive_qty = d.receive_qty
                               }).OrderByDescending(x => x.id).AsNoTracking();


            var list = (from h in ctx.ReceiveH
                        join _rec in ctx.User on h.receive_id equals _rec.Id into rec1
                        from rec in rec1.DefaultIfEmpty()
                        join _cr in ctx.User on h.create_id equals _cr.Id into cr1
                        from cre in cr1.DefaultIfEmpty()
                        join _up in ctx.User on h.update_id equals _up.Id into up1
                        from upd in up1.DefaultIfEmpty()
                        where h.receive_no == receive_no
                        select new ReceiveDetailRes
                        {
                            id = h.id,
                            receive_no = h.receive_no,
                            receive_id = h.receive_id,
                            receive_name = rec.FullName,
                            receive_date = h.receive_date,
                            receive_status = h.receive_status,
                            //receive_status_name = ReceiveStatus.listStatus.FirstOrDefault(o => o.Id == h.receive_status).Desc,
                            receive_type = h.receive_type,
                            //receive_type_name = ReceiveType.listType.FirstOrDefault(o => o.Id == h.receive_type).Desc,
                            dealer_code = h.dealer_code,
                            //dealer_name = DealerName.listDealer.FirstOrDefault(o => o.Id == h.dealer_id).Desc,
                            purchase_no = h.purchase_no,
                            remark = h.remark,
                            create_id = h.create_id,
                            create_name = cre.FullName,
                            create_date = h.create_date,
                            update_id = h.update_id,
                            update_name = upd.FullName,
                            update_date = h.update_date,
                            transfer_code = h.transfer_code,
                            delivery_code = h.delivery_code,
                            delivery_date = h.delivery_date,
                            detail = detail_list.ToList()

                        }).OrderByDescending(x => x.id).AsNoTracking();

            return Ok(list.SingleOrDefault());
        }

        [HttpGet("get_autocomplete")]
        public IActionResult get_autocomplete(string code_type)
        {
            var branch = (from inf in ctx.Information
                          where inf.code_type == code_type
                          select new
                          {
                              id = inf.code_id,
                              txt = inf.code_value
                          }).ToList();
            return Ok(branch);
        }

        [HttpGet("get_cat")]
        public IActionResult get_cat()
        {
            var data = (from inf in ctx.ProductCategory
                        select new
                        {
                            value = inf.CatId,
                            text = inf.CatName
                        }).ToList();
            return Ok(data);
        }

        [HttpGet("get_brand")]
        public IActionResult get_brand()
        {
            var data = (from inf in ctx.ProductBrand
                        select new
                        {
                            value = inf.BrandId,
                            text = inf.BrandName
                        }).ToList();
            return Ok(data);
        }

        [HttpGet("get_model")]
        public IActionResult get_model()
        {
            var data = (from inf in ctx.ProductModel
                        select new
                        {
                            value = inf.ModelId,
                            text = inf.ModelCode
                        }).ToList();
            return Ok(data);
        }

        [HttpGet("get_type")]
        public IActionResult get_type()
        {
            var data = (from inf in ctx.ProductType
                        select new
                        {
                            value = inf.TypeId,
                            text = inf.TypeCode
                        }).ToList();
            return Ok(data);
        }

        [HttpGet("get_color")]
        public IActionResult get_color()
        {
            var data = (from inf in ctx.ProductColor
                        select new
                        {
                            value = inf.ColorId,
                            text = inf.ColorCode
                        }).ToList();
            return Ok(data);
        }

        [HttpGet("get_province")]
        public IActionResult get_province()
        {
            var data = (from inf in ctx.MProvince
                        select new
                        {
                            value = inf.ProvinceCode,
                            text = inf.ProvinceNameTh
                        }).ToList();
            return Ok(data);
        }

        [HttpGet("get_dealer")]
        public IActionResult get_dealer()
        {
            var data = (from inf in ctx.MDealer
                        select new
                        {
                            value = inf.dealer_code,
                            text = inf.dealer_name_th
                        }).ToList();
            return Ok(data);
        }

        [HttpGet("get_wh")]
        public IActionResult get_wh(int branch_id)
        {
            var data = (from inf in ctx.WarehouseLocation
                        where inf.BranchId == branch_id && inf.WhlType == 1 && inf.WhlStatus == 1
                        select new
                        {
                            value = inf.WhlId,
                            text = inf.WhlCode + " " + inf.WhlName
                        }).ToList();
            return Ok(data);
        }

        [HttpGet("get_wh_all")]
        public IActionResult get_wh_all()
        {
            var data = (from inf in ctx.WarehouseLocation
                        select new
                        {
                            value = inf.WhlId,
                            text = inf.WhlCode + " " + inf.WhlName
                        }).ToList();
            return Ok(data);
        }

        public static decimal ToDecimalFormatted(string s)
        {
            try
            {
                return decimal.Parse(s);
            }
            catch
            {
                var numberWithoutMoneyFormatting = System.Text.RegularExpressions.Regex.Replace(s, @"[^\d.-]", "");
                return decimal.Parse(numberWithoutMoneyFormatting);
            }
        }

        [HttpPost("upload_dcs")]
        public IActionResult upload_dcs()
        {
            using (var transaction = ctx.Database.BeginTransaction())
            {
                try
                {
                    string create_by = Request.Form["id"][0];
                    Microsoft.AspNetCore.Http.IFormFile file = Request.Form.Files[0];
                    string folderName = "Upload";
                    string webRootPath = _hostingEnvironment.ContentRootPath; //_hostingEnvironment.WebRootPath;
                    string newPath = System.IO.Path.Combine(webRootPath, folderName);
                    DataTable dt = new DataTable();
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    if (!System.IO.Directory.Exists(newPath))
                    {
                        System.IO.Directory.CreateDirectory(newPath);
                    }
                    if (file.Length > 0)
                    {
                        string sFileExtension = System.IO.Path.GetExtension(file.FileName).ToLower();
                        NPOI.SS.UserModel.ISheet sheet;
                        string fullPath = System.IO.Path.Combine(newPath, file.FileName);
                        using (var stream = new System.IO.FileStream(fullPath, System.IO.FileMode.Create))
                        {
                            file.CopyTo(stream);
                            stream.Position = 0;
                            if (sFileExtension == ".xls")
                            {
                                NPOI.HSSF.UserModel.HSSFWorkbook hssfwb = new NPOI.HSSF.UserModel.HSSFWorkbook(stream); //This will read the Excel 97-2000 formats  
                                sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook  
                            }
                            else
                            {
                                NPOI.XSSF.UserModel.XSSFWorkbook hssfwb = new NPOI.XSSF.UserModel.XSSFWorkbook(stream); //This will read 2007 Excel format  
                                sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook   
                            }
                            dt = Doc2DT(sheet);
                        }

                        foreach (DataRow row in dt.Rows)
                        {

                            //select* from _transfer_log where log_item_type = '1' and engine_no = '".$rowData[0][6]."'

                            var model_id = (from p in ctx.ProductModel where p.ModelCode == row[15].ToString().Trim() select p.ModelId).FirstOrDefault();

                            var item_id = (from p in ctx.Product where p.ItemType == 2 && p.PartCode == row[13].ToString().Trim() && p.ModelId == model_id select p.ItemId).FirstOrDefault();

                            if (model_id == 0)
                            {
                                throw new Exception(row[15].ToString().Trim() + " (Model Code ไม่ถูกต้อง)");
                            }

                            if (item_id == 0)
                            {
                                var part_code = row[13].ToString().Trim();
                                var part_name = row[14].ToString().Trim();
                                var sell_price = ToDecimalFormatted(row[16].ToString().Trim());
                                var sell_vat_price = sell_price * (decimal)0.07;
                                var sell_net = sell_price * (decimal)1.07;

                                var cost_price = ToDecimalFormatted(row[18].ToString().Trim());
                                var cost_vat_price = cost_price * (decimal)0.07;
                                var cost_net = cost_price * (decimal)1.07;

                                Product product = new Product();
                                product.ItemType = 1;
                                product.ModelId = model_id;
                                product.PartCode = part_code;
                                product.PartName = part_name;
                                product.CostPrice = cost_price;
                                product.CostVat = 7;
                                product.CostVatPrice = cost_net;
                                product.CostNet = cost_vat_price;
                                product.SellPrice = sell_price;
                                product.SellVat = 7;
                                product.SellVatPrice = sell_vat_price;
                                product.SellNet = sell_net;
                                product.ItemStatus = 1;
                                product.CreateBy = int.Parse(create_by);
                                product.CreateDate = DateTime.Now;
                                ctx.Entry(product).State = EntityState.Added;
                                ctx.SaveChanges();
                                item_id = product.ItemId;
                                //throw new Exception(row[13].ToString().Trim() + " (Part Code ไม่ถูกต้อง)");
                            }

                            var chk_item_model = (from p in ctx.Product where p.ItemId == item_id && p.ModelId == model_id select p.ItemId).FirstOrDefault();

                            var chk_dup = (from p in ctx.TransferLog where p.LogItemType == 2 && p.PartCode == row[13].ToString().Trim() && p.LogRef == row[12].ToString().Trim() select p.LogId).FirstOrDefault();

                            var branch_id = 1;

                            if (chk_item_model == 0)
                            {
                                throw new Exception("(Part Code " + row[13].ToString().Trim() + " ยังไม่ได้ผูกกับ Model Code " + row[15].ToString().Trim() + ")");
                            }

                            if (chk_dup != 0)
                            {
                                throw new Exception("(เลขที่ใบสั่งซื้อ " + row[12].ToString().Trim() + " มี Part code. " + row[13].ToString().Trim() + " ในระบบแล้ว)");
                            }

                            TransferLog log = new TransferLog();
                            //log.LogId = null;
                            log.LogType = 3;

                            log.TranferNo = row[7].ToString().Replace(" ", "").Trim();
                            log.LogRef = row[12].ToString().Replace(" ", "").Trim();
                            log.SenderId = null;
                            log.ReceiverId = branch_id;
                            log.LogItemType = 2;
                            log.LogSecondhand = 0;
                            log.ItemId = item_id;
                            log.ModelId = model_id;
                            log.ColorId = null;
                            log.EngineNo = null;
                            log.FrameNo = null;
                            log.PartCode = row[13].ToString().Replace(" ", "").Trim();
                            log.Qty = int.Parse(row[17].ToString().Replace(" ", "").Trim());
                            log.LogStatus = 1;

                            log.TaxNo = row[3].ToString().Replace(" ", "").Trim();
                            log.InvAmt = ToDecimalFormatted(row[18].ToString().Trim());
                            log.VatAmt = 0;
                            log.DeliveryDate = null;
                            log.dealer_code = row[0].ToString();
                            log.CreateBy = int.Parse(create_by);
                            log.CreateDate = DateTime.Now;
                            log.UpdateBy = null;
                            log.UpdateDate = null;
                            ctx.Entry(log).State = EntityState.Added;
                            ctx.SaveChanges();
                        }

                    }
                    transaction.Commit();

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return StatusCode(500, ex.Message);
                }
            }
            return Ok();
        }

        public static string ToStringOrEmpty(Object value)
        {
            return value == null ? "" : value.ToString();
        }


        public partial class search_transfer
        {
            public int? log_id { get; set; }
            public string engine_no { get; set; }
            public string frame_no { get; set; }
            public string tax_no { get; set; }
            public decimal? inv_amt { get; set; }
            public decimal? vat_amt { get; set; }

            public int? item_id { get; set; }
            public int? cat_id { get; set; }
            public int? brand_id { get; set; }
            public int? model_id { get; set; }
            public int? type_id { get; set; }
            public int? color_id { get; set; }

            public string cat_code { get; set; }
            public string brand_code { get; set; }
            public string model_code { get; set; }
            public string model_type { get; set; }
            public string color_code { get; set; }


            public string dealer_code { get; set; }
            public string delivery_code { get; set; }
            public DateTime? delivery_date { get; set; }
            public string invoice_no { get; set; }

            public string part_code { get; set; }
            public string part_name { get; set; }
            public int? qty { get; set; }
            public int? b_qty { get; set; }
        }
        [HttpGet("transfer_log_list")]
        public IActionResult transfer_log_list(string log_ref, string part_code, string tax_no)
        {
            try
            {
                string f_log_ref = ToStringOrEmpty(log_ref);
                string f_part_code = ToStringOrEmpty(part_code);
                string f_tax_no = ToStringOrEmpty(tax_no);

                var list = (from db in ctx.TransferLog

                            join _product in ctx.Product on db.ItemId equals _product.ItemId into product1
                            from product in product1.DefaultIfEmpty()

                            join _model in ctx.ProductModel on product.ModelId equals _model.ModelId into model1
                            from model in model1.DefaultIfEmpty()

                            where db.LogStatus.Equals(1)
                            && db.LogItemType.Equals(2)
                            && db.LogRef.Contains(f_log_ref)
                            && db.PartCode.Contains(f_part_code)
                            && db.TaxNo.Contains(f_tax_no)
                            orderby db.CreateDate descending
                            select new search_transfer
                            {
                                log_id = db.LogId,
                                part_code = db.PartCode,
                                part_name = product.PartName,
                                tax_no = db.TaxNo,
                                inv_amt = db.InvAmt,
                                vat_amt = db.VatAmt,
                                item_id = product.ItemId,
                                model_id = product.ModelId,
                                model_code = model.ModelCode,
                                model_type = model.ModelType,
                                dealer_code = db.dealer_code,
                                delivery_code = db.TranferNo,
                                qty = db.Qty,
                                b_qty = db.BQty,
                                invoice_no = db.LogRef
                            }).ToList();

                return Ok(list);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        public partial class ReceiveHeadFormBody
        {
            public int? id { get; set; }
            public string receive_no { get; set; }
            public int? receive_id { get; set; }
            public DateTime? receive_date { get; set; }
            public int? receive_status { get; set; }
            public int? receive_type { get; set; }
            public string dealer_code { get; set; }
            public string transfer_code { get; set; }
            public string purchase_no { get; set; }
            public string remark { get; set; }
            public int? create_id { get; set; }
            public DateTime? create_date { get; set; }
            public int? update_id { get; set; }
            public DateTime? update_date { get; set; }
            public string delivery_code { get; set; }
            public DateTime? delivery_date { get; set; }


            public List<ReceiveDetailFormBody> detail { get; set; }
        }
        public partial class ReceiveDetailFormBody
        {
            public int? cat_id { get; set; }
            public int? brand_id { get; set; }
            public int? model_id { get; set; }
            public int? type_id { get; set; }
            public int? color_id { get; set; }
            public string engine_no { get; set; }
            public string frame_no { get; set; }
            public string invoice_no { get; set; }
            public decimal? cost_exc_vat { get; set; }
            public decimal? cost_other_exc_vat { get; set; }
            public decimal? cost_repair_exc_vat { get; set; }
            public string province_code { get; set; }
            public int? id { get; set; }
            public string receive_no { get; set; }
            public string dealer_code { get; set; }
            public string delivery_code { get; set; }
            public DateTime? delivery_date { get; set; }
            public string tax_invoice_no { get; set; }
            public string license_no { get; set; }
            public int? branch_id { get; set; }
            public string line_remark { get; set; }
            public int? line_status { get; set; }
            public decimal? cost_inc_vat { get; set; }
            public string vat_flag { get; set; }
            public decimal? vat_rate { get; set; }
            public decimal? cost_vat { get; set; }
            public int? whl_id { get; set; }
            public int? item_id { get; set; }
            public string part_code { get; set; }
            public int r_qty { get; set; }
            
        }
        [HttpPost("create_receive")]
        public IActionResult create_receive([FromBody] ReceiveHeadFormBody value)
        {
            using (var transaction = ctx.Database.BeginTransaction())
            {
                try
                {

                    var h = value;
                    var receive_no = GenerateReceiveNo();

                    ReceiveH head = new ReceiveH();
                    head.receive_no = receive_no;
                    head.receive_id = h.receive_id;
                    head.receive_date = h.receive_date;
                    head.receive_status = h.receive_status;
                    head.receive_type = h.receive_type;
                    head.dealer_code = h.dealer_code;
                    head.delivery_code = h.delivery_code;
                    head.delivery_date = h.delivery_date;
                    head.purchase_no = h.purchase_no;
                    head.remark = h.remark;
                    head.create_id = h.create_id;
                    head.create_date = DateTime.Now;
                    ctx.Entry(head).State = EntityState.Added;
                    ctx.SaveChanges();

                    var d = h.detail;
                    d.ForEach(item =>
                    {
                        int receive_qty = item.r_qty;
                        if (receive_qty <= 0)
                        {
                            throw new Exception(item.invoice_no + " " + item.part_code + " จำนวนรับต้องมากกว่า 0 เท่านั้น");
                        }

                        var log_status = (from p in ctx.TransferLog where p.LogId == item.id select p.LogStatus).FirstOrDefault();
                        var wh = (from p in ctx.WarehouseLocation where p.WhlId == item.whl_id && p.BranchId == item.branch_id && p.WhlStatus == 1 && p.WhlType == 1 select p.WhlCode).Count();

                        if (log_status != 1)
                        {
                            throw new Exception(item.invoice_no + " " + item.part_code + " รายการนี้ได้ทำการรับเข้าคลังเรียบร้อยแล้ว");
                        }
                        if (wh == 0)
                        {
                            var wh_code = (from p in ctx.WarehouseLocation where p.WhlId == item.whl_id select p.WhlCode).FirstOrDefault();
                            var wh_name = (from p in ctx.WarehouseLocation where p.WhlId == item.whl_id select p.WhlName).FirstOrDefault();
                            throw new Exception(wh_code + " " + wh_name + " ไม่สามารถเก็บสินค้าชนิดนี้ได้");
                        }

                        ReceiveD detail = new ReceiveD();

                        detail.receive_no = receive_no;
                        detail.dealer_no = item.dealer_code;

                        //detail.cat_id = item.cat_id;
                        //detail.brand_id = item.brand_id;
                        detail.model_id = item.model_id;
                        //detail.type_id = item.type_id;
                        //detail.color_id = item.color_id;
                        //detail.frame_no = item.frame_no;
                        //detail.engine_no = item.engine_no;
                        detail.delivery_no = item.delivery_code;
                        //detail.delivery_date = item.delivery_date;
                        detail.invoice_no = item.invoice_no;
                        detail.tax_invoice_no = item.tax_invoice_no;
                        detail.create_id = h.create_id;
                        detail.create_date = DateTime.Now;
                        //detail.license_no = item.license_no;
                        detail.branch_id = item.branch_id;
                        //detail.line_remark = item.line_remark;
                        //detail.line_status = item.line_status;
                        detail.cost_inc_vat = item.cost_inc_vat;
                        detail.vat_flag = item.vat_flag;
                        detail.vat_rate = item.vat_rate;
                        detail.cost_vat = item.cost_vat;
                        detail.cost_exc_vat = item.cost_exc_vat;
                        detail.cost_other_exc_vat = item.cost_other_exc_vat;
                        detail.cost_repair_exc_vat = item.cost_repair_exc_vat;

                        detail.whl_id = item.whl_id;
                        detail.log_id = item.id;
                        detail.item_id = item.item_id;
                        detail.receive_qty = receive_qty;

                        ctx.Entry(detail).State = EntityState.Added;
                        ctx.SaveChanges();


                        var stock = (from p in ctx.StockReceive where p.ItemId == item.item_id && p.BranchId == item.branch_id && p.WhlId == item.whl_id && p.LogId == item.id select p.ReceiveId).Count();
                        
                        if (stock == 0)
                        {

                            StockReceive rec = new StockReceive();
                            rec.WhlId = item.whl_id;
                            rec.BranchId = item.branch_id;
                            rec.ItemId = item.item_id;
                            rec.ReceiveQty = receive_qty;
                            rec.ReceiveBy = h.create_id;
                            rec.ReceiveDate = DateTime.Now;
                            rec.LogId = item.id;
                            rec.BalanceQty = receive_qty;
                            rec.stock_onhand = receive_qty;
                            rec.stock_variable = receive_qty;
                            ctx.Entry(rec).State = EntityState.Added;
                            ctx.SaveChanges();

                            var log = ctx.TransferLog.FirstOrDefault(x => x.LogId == item.id);
                            if ((log.BQty + receive_qty) < log.Qty )
                            {
                                log.BQty = log.BQty + receive_qty;
                                log.LogStatus = 1;
                            }
                            else
                            {
                                log.BQty = log.BQty + receive_qty;
                                log.LogStatus = 2;
                            }
                            log.InvAmt = item.cost_exc_vat;
                            log.VatAmt = item.cost_vat;
                            ctx.Entry(log).State = EntityState.Modified;
                            ctx.SaveChanges();

                        }
                        else
                        {
                            var rec = ctx.StockReceive.FirstOrDefault(x => x.ItemId == item.item_id && x.BranchId == item.branch_id && x.WhlId == item.whl_id && x.LogId == item.id);
                            rec.ReceiveQty = rec.ReceiveQty + receive_qty;
                            rec.BalanceQty = rec.BalanceQty + receive_qty;
                            rec.stock_onhand = rec.stock_onhand + receive_qty;
                            rec.stock_variable = rec.stock_variable + receive_qty;
                            ctx.Entry(rec).State = EntityState.Modified;
                            ctx.SaveChanges();

                            var log = ctx.TransferLog.FirstOrDefault(x => x.LogId == item.id);
                            if ((log.BQty + receive_qty) < log.Qty)
                            {
                                log.BQty = log.BQty + receive_qty;
                                log.LogStatus = 1;
                            }
                            else
                            {
                                log.BQty = log.BQty + receive_qty;
                                log.LogStatus = 2;
                            }
                            log.InvAmt = item.cost_exc_vat;
                            log.VatAmt = item.cost_vat;
                            ctx.Entry(log).State = EntityState.Modified;
                            ctx.SaveChanges();
                        }
                    });

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return StatusCode(500, ex.Message);
                }
            }
            return NoContent();
        }


        public DataTable Doc2DT(NPOI.SS.UserModel.ISheet sheet)
        {
            DataTable data = new DataTable();
            int startRow = 0;
            try
            {
                if (sheet != null)
                {
                    NPOI.SS.UserModel.IRow firstRow = sheet.GetRow(0);
                    int cellCount = firstRow.LastCellNum;

                    for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                    {
                        NPOI.SS.UserModel.ICell cell = firstRow.GetCell(i);
                        if (cell != null)
                        {
                            string cellValue = cell.StringCellValue;
                            if (cellValue != null)
                            {
                                DataColumn column = new DataColumn(cellValue);
                                data.Columns.Add(column);
                            }
                        }
                    }
                    startRow = sheet.FirstRowNum + 1;


                    int rowCount = sheet.LastRowNum;
                    for (int i = startRow; i <= rowCount; ++i)
                    {
                        NPOI.SS.UserModel.IRow row = sheet.GetRow(i);
                        if (row == null) continue;

                        DataRow dataRow = data.NewRow();
                        for (int j = row.FirstCellNum; j < cellCount; ++j)
                        {
                            if (row.GetCell(j) != null)
                                dataRow[j] = row.GetCell(j).ToString();
                        }
                        data.Rows.Add(dataRow);
                    }
                }
                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return null;
            }
        }

        public string GenerateReceiveNo()
        {
            // ctx.CreditContractPayment
            var receiptNo = (from db in ctx.ReceiveH
                             orderby db.receive_no descending
                             where db.receive_no.StartsWith("RC")
                             select db.receive_no).FirstOrDefault();

            return SetRunningCode("RC", 1, receiptNo);
        }

        private string SetRunningCode(string prefix, int branchId, string runningNumber)
        {
            string year = (DateTime.Now.Year + 543).ToString().Substring(2, 2);
            string month = (DateTime.Now.Month).ToString("00");
            string r = $"{prefix}{branchId.ToString("00")}{year}{month}";

            if (runningNumber == null) return $"{r}/0001";

            string preStr = runningNumber.Split("/")[0];
            string endStr = runningNumber.Split("/")[1];

            string preMonth = preStr.Substring(preStr.Length - 2);
            int runNumber = (preMonth == month) ? int.Parse(endStr) + 1 : 1;
            return $"{r}/{runNumber.ToString("0000")}";
        }
    }
}