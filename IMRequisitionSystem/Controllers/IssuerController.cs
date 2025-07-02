using iTextSharp.text;
using iTextSharp.text.pdf;
using IMRequisitionSystem.Models.Assets;
using IMRequisitionSystem.Repository;
using IMRequisitionSystem.Repository.Approver;
using IMRequisitionSystem.Repository.Issuer;
using IMRequisitionSystem.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static IMRequisitionSystem.Util.Enums;
using System.Drawing.Printing;
using System.IO;
using System.Xml.Linq;

namespace IMRequisitionSystem.Controllers
{
    [CustomAdminAuthorize]
    public class IssuerController : Controller
    {
        private readonly IIMIssuerRepository _imIssuerRepository;
        private readonly IAssetCategoryRepository _assetCategoryRepository;
        // GET: Approver
        public IssuerController(
            IIMIssuerRepository imIssuerRepository,
             IAssetCategoryRepository assetCategoryRepository
            )
        {

            _imIssuerRepository = imIssuerRepository;
            _assetCategoryRepository = assetCategoryRepository;

        }
        public ActionResult RequisitionIssueList(string status, string message, bool isSwal = false)
        {
            try
            {
                if (!string.IsNullOrEmpty(status))
                {
                    TempData[ToastMessageParameter.MessageType.ToString()] = status;
                }
                if (!string.IsNullOrEmpty(message))
                {
                    TempData[ToastMessageParameter.Message.ToString()] = message;
                }
                TempData[ToastMessageParameter.IsSwal.ToString()] = isSwal;
                ViewBag.RequisitionDetailsForIssuerDataDD = _imIssuerRepository.GetRequisitionIssuerList();
                ViewBag.AssetCategoryDD = _assetCategoryRepository.GetAllAssetCategoryForDropDown();
            }
            catch (Exception ex)
            {
                LoggingClass.SaveExceptionLog(ex);
            }
            return View();
        }


        [HttpGet]
        
        public JsonResult CheckAvailability(string deviceTypeId)
        {
            try
            {
                var result = _imIssuerRepository.CheckAvailabilityOfDevice(deviceTypeId);
                return Json(new { success = true, data = result }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        //[HttpGet]

        //public JsonResult GetDeviceTypeID(string deviceTypeId)
        //{
        //    try
        //    {
        //        var result = _imIssuerRepository.GetDeviceTypeID(deviceTypeId);
        //        return Json(new { success = true, data = result }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
        //    }
        //}

        [HttpGet]
        public JsonResult GetDeviceTypeID(string deviceTypeId)
        {
            try
            {
                List<RequisitionRequestModel> filterData = _imIssuerRepository.GetDeviceTypeID(deviceTypeId);
                return Json(filterData, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new List<RequisitionRequestModel>(), JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult RequisitionIssueByIMIssuer(RequisitionRequestModel requisitionRequestModel)
        {
            var spResponse = _imIssuerRepository.IMIssuerUpdate(requisitionRequestModel);

            Session["Requisition_No"] = requisitionRequestModel.Requisition_No;

            var jsonResponseHandler = new JsonResponseHandler(Url);
            return jsonResponseHandler.HandleResponseWithBookingRequestNo(spResponse, "RequisitionIssueList", "Issuer", requisitionRequestModel.Requisition_No);
        }

        public ActionResult IssuedRequisitionList(string status, string message, bool isSwal = false)
        {
            try
            {
                if (!string.IsNullOrEmpty(status))
                {
                    TempData[ToastMessageParameter.MessageType.ToString()] = status;
                }
                if (!string.IsNullOrEmpty(message))
                {
                    TempData[ToastMessageParameter.Message.ToString()] = message;
                }
                TempData[ToastMessageParameter.IsSwal.ToString()] = isSwal;
                ViewBag.RequisitionIMIssuededData = _imIssuerRepository.GetRequisitionIMIssuedList();
            }
            catch (Exception ex)
            {
                LoggingClass.SaveExceptionLog(ex);
            }
            return View();
        }

        public ActionResult RequisitionAllocateList(string status, string message, bool isSwal = false)
        {
            try
            {
                if (!string.IsNullOrEmpty(status))
                {
                    TempData[ToastMessageParameter.MessageType.ToString()] = status;
                }
                if (!string.IsNullOrEmpty(message))
                {
                    TempData[ToastMessageParameter.Message.ToString()] = message;
                }
                TempData[ToastMessageParameter.IsSwal.ToString()] = isSwal;
                ViewBag.RequisitionIMIssuededData = _imIssuerRepository.GetRequisitionIMIssuedList();
            }
            catch (Exception ex)
            {
                LoggingClass.SaveExceptionLog(ex);
            }
            return View();
        }

        public JsonResult RequisitionAllocate(RequisitionRequestModel requisitionRequestModel)
        {
            var spResponse = _imIssuerRepository.IMAllocatorUpdate(requisitionRequestModel);

            Session["Requisition_No"] = requisitionRequestModel.Requisition_No;

            var jsonResponseHandler = new JsonResponseHandler(Url);
            return jsonResponseHandler.HandleResponseWithBookingRequestNo(spResponse, "RequisitionAllocateList", "Issuer", requisitionRequestModel.Requisition_No);
        }
        public ActionResult RequisitionAllocatedList(string status, string message, bool isSwal = false)
        {
            try
            {
                if (!string.IsNullOrEmpty(status))
                {
                    TempData[ToastMessageParameter.MessageType.ToString()] = status;
                }
                if (!string.IsNullOrEmpty(message))
                {
                    TempData[ToastMessageParameter.Message.ToString()] = message;
                }
                TempData[ToastMessageParameter.IsSwal.ToString()] = isSwal;
                ViewBag.RequisitionIMIssuededData = _imIssuerRepository.GetRequisitionAllocatedList();
            }
            catch (Exception ex)
            {
                LoggingClass.SaveExceptionLog(ex);
            }
            return View();
        }
        /*public ActionResult RequisitionData(string status, string message, string ID, bool isSwal = false)
        {
            try
            {
                
                string pdfName = "Asset_Issue_Order_" + ID;
                FileStreamResult result = PrintPDF(ID, pdfName);

            }
            catch (Exception ex)
            {
                LoggingClass.SaveExceptionLog(ex);
            }
            return View();
        }*/



        public ActionResult DetailPageForIssuer(RequisitionRequestModel requisitionRequestModel, string status, string message, string requisition_No)
        {
            try
            {
                if (!string.IsNullOrEmpty(status))
                {
                    TempData[ToastMessageParameter.MessageType.ToString()] = status;
                }
                if (!string.IsNullOrEmpty(message))
                {
                    TempData[ToastMessageParameter.Message.ToString()] = message;
                }
                TempData[ToastMessageParameter.IsSwal.ToString()] = true;
                //ViewBag.CancelRequest = cancelRequest;
                requisition_No = System.Web.HttpContext.Current.Session["requisition_No"] as string;
                //ViewBag.RequisitionDetailsForRequestorDataDD = _requisitionApproveList.GetDetailsPageForUnitIncharge(requisition_No);
                requisitionRequestModel = _imIssuerRepository.GetDetailsDataForIssuer(requisition_No);
                return View(requisitionRequestModel);
            }
            catch (Exception ex)
            {
                LoggingClass.SaveExceptionLog(ex);
            }
            return View();
        }

        public JsonResult RequisitionSortCloseByIssuer(RequisitionRequestModel requisitionRequestModel)
        {
            var spResponse = _imIssuerRepository.IssuerCloseUpdate(requisitionRequestModel);

            Session["Requisition_No"] = requisitionRequestModel.Requisition_No;

            var jsonResponseHandler = new JsonResponseHandler(Url);
            return jsonResponseHandler.HandleResponseWithBookingRequestNo(spResponse, "RequisitionIssueList", "Issuer", requisitionRequestModel.Requisition_No);
        }

        public ActionResult DetailPageForAlocator(RequisitionRequestModel requisitionRequestModel, string status, string message, string requisition_No)
        {
            try
            {
                if (!string.IsNullOrEmpty(status))
                {
                    TempData[ToastMessageParameter.MessageType.ToString()] = status;
                }
                if (!string.IsNullOrEmpty(message))
                {
                    TempData[ToastMessageParameter.Message.ToString()] = message;
                }
                TempData[ToastMessageParameter.IsSwal.ToString()] = true;
                //ViewBag.CancelRequest = cancelRequest;
                requisition_No = System.Web.HttpContext.Current.Session["requisition_No"] as string;
                //ViewBag.RequisitionDetailsForRequestorDataDD = _requisitionApproveList.GetDetailsPageForUnitIncharge(requisition_No);
                requisitionRequestModel = _imIssuerRepository.GetDetailsDataForIssuer(requisition_No);
                return View(requisitionRequestModel);
            }
            catch (Exception ex)
            {
                LoggingClass.SaveExceptionLog(ex);
            }
            return View();
        }


        public ActionResult PrintPDF(string ID)
        {
            var pdfName = "Asset_Issue_Order_" + ID;

            var requisitionRequestModel = new RequisitionRequestModel();
            requisitionRequestModel = _imIssuerRepository.GetDetailsDataForIssuer(ID);
            //var permitData = _epermitMethod.GetAllPermitRelatedData(permitRequestNo);
            //var materialRequetData = _materialMethod.GetMaterialRequest_By_PK_MaterialRequestID(requestID);
            //var materialRequetData = _materialMethod.GetMaterialRequest_By_PK_MaterialRequestID(requestID);

            Document pdfDoc = new Document(PageSize.A4, -30, -30, 10, 10);
            //PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            MemoryStream memoryStream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
            pdfDoc.Open();


            iTextSharp.text.pdf.PdfPTable tableHeader = new iTextSharp.text.pdf.PdfPTable(1);
            iTextSharp.text.pdf.PdfPCell cellL = new iTextSharp.text.pdf.PdfPCell();
            var boldFontHeader = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10);
            var boldFontHeading = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8);
            var boldFontDetails = FontFactory.GetFont(FontFactory.HELVETICA_OBLIQUE, 8);
            var regularFontHeader = FontFactory.GetFont(FontFactory.HELVETICA, 10);
            var regularFontDetails = FontFactory.GetFont(FontFactory.HELVETICA, 8);
            var boldFontDetailsNew = FontFactory.GetFont(FontFactory.HELVETICA, 10);

            tableHeader.DefaultCell.BorderWidth = 0f;
            tableHeader.DefaultCell.BorderWidthLeft = 0f;
            tableHeader.DefaultCell.BorderWidthRight = 0f;

            cellL.BorderWidth = 0f;
            cellL.BorderWidthRight = 0f;
            cellL.BorderWidthLeft = 0f;
            cellL.BorderWidthTop = 0f;
            cellL.BorderWidthBottom = 0f;
            cellL.PaddingRight = 0f;



            #region Header of Permit

            PdfPTable tableH = new PdfPTable(2);
            float[] tblwidthsH = new float[] { 1f, 15f };
            tableH.SetWidths(tblwidthsH);
            tableH.DefaultCell.BorderWidth = 0f;

            Paragraph header1 = new Paragraph();
            header1.Add(new Chunk("Instrumentation Maintenance Department", boldFontHeader));
            header1.Alignment = Element.ALIGN_CENTER;
            cellL.AddElement(header1);

            Paragraph header2 = new Paragraph();
            header2.Add(new Chunk("ASSET ISSUE ORDER", boldFontHeader));
            header2.Alignment = Element.ALIGN_CENTER;
            cellL.AddElement(header2);

            
            iTextSharp.text.Image checkedH = iTextSharp.text.Image.GetInstance(Server.MapPath("~/lib/assets/media/svg/illustrations/nrl_logo.jpg"));
            checkedH.ScaleAbsolute(25f, 25f);
            checkedH.Alignment = Element.ALIGN_CENTER;
            PdfPCell imageCellH = new PdfPCell(checkedH);
            imageCellH.VerticalAlignment = Element.ALIGN_MIDDLE;
            imageCellH.BorderWidth = 0f;

            tableH.AddCell(imageCellH);
            tableH.AddCell(cellL);

            #endregion

            
            #region Body
            iTextSharp.text.pdf.PdfPCell cellL2 = new iTextSharp.text.pdf.PdfPCell();

            cellL2.BorderWidth = 0f;
            cellL2.PaddingRight = 0f;

            Paragraph hhh1 = new Paragraph();
            Chunk ccc1 = new Chunk("REQUISITION ID :" + ID, boldFontHeader);
            hhh1.Add(ccc1);

            cellL2.AddElement(hhh1);

           

           
            iTextSharp.text.pdf.PdfPCell cellL1 = new iTextSharp.text.pdf.PdfPCell();

            cellL1.BorderWidth = 0f;
            cellL1.PaddingRight = 0f;

            Paragraph hh1 = new Paragraph();
            Chunk cc1 = new Chunk("ASSET ID SAP:" + requisitionRequestModel.Asset_Id_SAP, boldFontHeader);
            hh1.Add(cc1);

            cellL1.AddElement(hh1);

            Paragraph hh111 = new Paragraph();
            Chunk cc111 = new Chunk("ASSET SL NO:" + requisitionRequestModel.Asset_Sl_No, boldFontHeader);
            hh111.Add(cc111);

            cellL1.AddElement(hh111);

            Paragraph hh2 = new Paragraph();
            Chunk cc2 = new Chunk("REQUESTED DEVICE TYPE: " + requisitionRequestModel.DeviceName, boldFontHeader);
            hh2.Add(cc2);

            cellL1.AddElement(hh2);

            Paragraph hh3 = new Paragraph();
            Chunk cc3 = new Chunk("REQUISITION STATUS: " + requisitionRequestModel.Requisition_Status, boldFontHeader);
            hh3.Add(cc3);

            cellL1.AddElement(hh3);

            Paragraph hh4 = new Paragraph();
            Chunk cc4 = new Chunk("REQUESTOR LOCATION: " + requisitionRequestModel.Requestor_Department, boldFontHeader);
            hh4.Add(cc4);

            cellL1.AddElement(hh4);

            Paragraph hh5 = new Paragraph();
            Chunk cc5 = new Chunk("USE LOCATION: " + requisitionRequestModel.Use_Location, boldFontHeader);
            hh5.Add(cc5);

            cellL1.AddElement(hh5);

            Paragraph hh6 = new Paragraph();
            Chunk cc6 = new Chunk("USE DEPARTMENT: " + requisitionRequestModel.Use_Department_Name + " ", boldFontHeader);
            hh6.Add(cc6);

            cellL1.AddElement(hh6);

            Paragraph hh7 = new Paragraph();
            Chunk cc7 = new Chunk("REQUISITION FOR: " + requisitionRequestModel.RequisitionFor + " ", boldFontHeader);
            hh7.Add(cc7);

            cellL1.AddElement(hh7);

            Paragraph hh8 = new Paragraph();
            Chunk cc8 = new Chunk("PURPOSE OF REQUISITION: " + requisitionRequestModel.Purpose_Of_Use + " ", boldFontHeader);
            hh8.Add(cc8);

            cellL1.AddElement(hh8);
            
            Paragraph hh9 = new Paragraph();
            Chunk cc9 = new Chunk("REQUISITIONER NAME: " + requisitionRequestModel.Requestor_Name + " ", boldFontHeader);
            hh9.Add(cc9);

            cellL1.AddElement(hh9);

           Paragraph hh10 = new Paragraph();
            Chunk cc10 = new Chunk("REQUISITIONER DATE: " + requisitionRequestModel.Requisition_Date + " ", boldFontHeader);
            hh10.Add(cc10);

            cellL1.AddElement(hh10);
 
            //Paragraph hh11 = new Paragraph();
            //Chunk cc11 = new Chunk("ISSUER NAME: " + requisitionRequestModel.IM_Issuer_Name + " ", boldFontHeader);
            //hh11.Add(cc11);

            //cellL1.AddElement(hh11);

            //Paragraph hh12 = new Paragraph();

            

            //Chunk cc12 = new Chunk("ISSUER NAME: " + requisitionRequestModel.IM_Issued_DateTime + " ", boldFontHeader);
            //hh12.Add(cc12);

            //cellL1.AddElement(hh12);
            #endregion


            #region Table
            PdfPTable table2 = new PdfPTable(3);
            float[] tblwidthstt = new float[] { 4f, 4f, 4f };
            table2.SetWidths(tblwidthstt);
            table2.DefaultCell.BorderWidth = 1f;
            table2.DefaultCell.Padding = 5f;

            Paragraph headerT1 = new Paragraph();
            headerT1.Add(new Chunk("IM APPROVER", boldFontHeader));
            headerT1.Alignment = Element.ALIGN_CENTER;
            cellL.AddElement(headerT1);

            Paragraph headerT2 = new Paragraph();
            headerT2.Add(new Chunk("IM ISSUER", boldFontHeader));
            headerT2.Alignment = Element.ALIGN_CENTER;
            cellL.AddElement(headerT2);
            
            Paragraph headerT3 = new Paragraph();
            headerT3.Add(new Chunk("WORKSHOP", boldFontHeader));
            headerT3.Alignment = Element.ALIGN_CENTER;
            cellL.AddElement(headerT3);


           
            table2.AddCell(headerT1);
            table2.AddCell(headerT2);
            table2.AddCell(headerT3);


            Paragraph headerT4 = new Paragraph();
            headerT4.Add(new Chunk(requisitionRequestModel.IM_Approver_Name +"\n" + "" + requisitionRequestModel.IM_Approve_DateTime + "", boldFontHeader));
            headerT4.Alignment = Element.ALIGN_CENTER;
            cellL.AddElement(headerT4);

            Paragraph headerT5 = new Paragraph();
            headerT5.Add(new Chunk(requisitionRequestModel.IM_Issuer_Name + "\n" + ""+ requisitionRequestModel.IM_Issued_DateTime + "", boldFontHeader));
            headerT5.Alignment = Element.ALIGN_CENTER;
            cellL.AddElement(headerT5);



            Paragraph headerT6 = new Paragraph();
            headerT6.Add(new Chunk(requisitionRequestModel.IM_Allocator_Name + "\n" + "" + requisitionRequestModel.IM_Allocated_DateTime  +"" , boldFontHeader));
            headerT6.Alignment = Element.ALIGN_CENTER;
            cellL.AddElement(headerT6);


            table2.AddCell(headerT4);
            table2.AddCell(headerT5);
            table2.AddCell(headerT6);




            tableHeader.AddCell(tableH);
            tableHeader.AddCell(cellL2);
            tableHeader.AddCell(cellL1);
            tableHeader.AddCell(table2);




            pdfDoc.Add(tableHeader);
            pdfDoc.Close();




            string fileRoute = Server.MapPath("~/Content");
            string fileName = pdfName + ".pdf";
            string filePathTempDocument = Path.Combine(fileRoute, "TempDocument", fileName);
            string filePathFiles = Path.Combine(fileRoute, "Files", fileName);

            /*if (System.IO.File.Exists(filePathFiles))
            {
                System.IO.File.Delete(filePathFiles);
            }
            System.IO.File.WriteAllBytes(filePathFiles, memoryStream.ToArray());*/

            if (System.IO.File.Exists(filePathFiles))
            {
                System.IO.File.Delete(filePathFiles);
            }
            System.IO.File.WriteAllBytes(filePathFiles, memoryStream.ToArray());


            string outputDirectory = (fileRoute + "/Files/");
            string outputFilePath = Path.Combine(outputDirectory, "abc.pdf");

            /*using (Viewer viewer = new Viewer(filePathTempDocument))
            {
                GroupDocs.Viewer.Options.PdfViewOptions options = new GroupDocs.Viewer.Options.PdfViewOptions(outputFilePath);
                viewer.View(options);
            }*/

            var fileStream = new FileStream(filePathFiles,
                FileMode.Open,
                FileAccess.Read);
            var fsResult = new FileStreamResult(fileStream, "application/pdf")
            {
                FileDownloadName = fileName
            };

            return fsResult;


            #endregion
            /*Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;" + "filename=MaterialIssueForm.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Write(pdfDoc);
            Response.End();*/


        }


    }
}