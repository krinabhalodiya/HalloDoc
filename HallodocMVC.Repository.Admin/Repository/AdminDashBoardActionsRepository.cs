using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalloDoc.Entity.Models;
using HalloDoc.Entity.DataContext;
using HallodocMVC.Repository.Admin.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Net.NetworkInformation;
using System.Net;
using HalloDoc.Entity.DataModels;
using Microsoft.AspNetCore.Http;
using static HalloDoc.Entity.Models.ViewDocuments;
using System.Runtime.Intrinsics.Arm;
using static HalloDoc.Entity.Models.Constant;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HallodocMVC.Repository.Admin.Repository
{
    public class AdminDashBoardActionsRepository : IAdminDashBoardActionsRepository
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly EmailConfiguration _emailConfig;
        private readonly HalloDocContext _context;
        public AdminDashBoardActionsRepository(HalloDocContext context, IHttpContextAccessor httpContextAccessor, EmailConfiguration emailConfig)
        {
            _context = context;
            this.httpContextAccessor = httpContextAccessor;
            _emailConfig = emailConfig;
        }
        #region Editcase
        public bool EditCase(ViewCaseData model)
        {
            try{
                int monthnum = model.Dob.Month;
                string monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(monthnum);
                int date = model.Dob.Day;
                int year = model.Dob.Year;
                Requestclient client = _context.Requestclients.FirstOrDefault(E => E.Requestid == model.RequestID);
                if (client != null)
                {
                    client.Firstname = model.FirstName;
                    client.Lastname = model.LastName;
                    client.Phonenumber = model.PhoneNumber;
                    client.Intdate = date;
                    client.Intyear = year;
                    client.Strmonth = monthName;
                    client.Notes = model.PatientNotes;
                    client.Phonenumber = model.PhoneNumber;
                    //client.RegionId = model.RegionId;
                    List<string> location = model.Address.Split(',').ToList();
                    client.Street = location[0];
                    client.City = location[1];
                    client.State = location[2];
                    client.Address = model.Room;
                    _context.Requestclients.Update(client);
                    _context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

        #region GetRequestForViewCase
        public ViewCaseData GetRequestForViewCase(int id)
        {
            var n = _context.Requests.FirstOrDefault(E => E.Requestid == id);
            var l = _context.Requestclients.FirstOrDefault(E => E.Requestid == id);
            var region = _context.Regions.FirstOrDefault(E => E.Regionid == l.Regionid);
            ViewCaseData requestforviewcase = new ViewCaseData
            {
                RequestID = id,
                Region = region.Name,
                FirstName = l.Firstname,
                LastName = l.Lastname,
                PhoneNumber = l.Phonenumber,
                PatientNotes = l.Notes,
                Email = l.Email,
                RequestTypeID = n.Requesttypeid,
                Address = l.Street + "," + l.City + "," + l.State,
                Room = l.Address,
                ConfirmationNumber = n.Confirmationnumber,
                Dob = new DateTime((int)l.Intyear, DateTime.ParseExact(l.Strmonth, "MMMM", new CultureInfo("en-US")).Month, (int)l.Intdate)
            };
            return requestforviewcase;
        }
        #endregion

        #region AssignProvider
        public async Task<bool> AssignProvider(int RequestId, int ProviderId, string notes)
        {
            var request = await _context.Requests.FirstOrDefaultAsync(req => req.Requestid == RequestId);
            request.Physicianid = ProviderId;
            request.Status = 1;
            _context.Requests.Update(request);
            _context.SaveChanges();
            Requeststatuslog rsl = new Requeststatuslog();
            rsl.Requestid = RequestId;
            rsl.Physicianid = ProviderId;
            rsl.Notes = notes;
            rsl.Createddate = DateTime.Now;
            rsl.Status = 1;
            _context.Requeststatuslogs.Update(rsl);
            _context.SaveChanges();
            return true;
        }
        #endregion

        #region Cancelcase
        public bool CancelCase(int RequestID, string Note, string CaseTag)
        {
            try
            {
                var requestData = _context.Requests.FirstOrDefault(e => e.Requestid == RequestID);
                if (requestData != null)
                {
                    requestData.Casetag = CaseTag;
                    requestData.Status = 3;
                    _context.Requests.Update(requestData);
                    _context.SaveChanges();
                    Requeststatuslog rsl = new Requeststatuslog
                    {
                        Requestid = RequestID,
                        Notes = Note,
                        Status = 3,
                        Createddate = DateTime.Now
                    };
                    _context.Requeststatuslogs.Add(rsl);
                    _context.SaveChanges();
                    return true;
                }
                else { return false; }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

        #region BlockCase
        public bool BlockCase(int RequestID, string Note)
        {
            try
            {
                var requestData = _context.Requests.FirstOrDefault(e => e.Requestid == RequestID);
                if (requestData != null)
                {
                    requestData.Status = 11;
                    _context.Requests.Update(requestData);
                    _context.SaveChanges();
                    Blockrequest blc = new Blockrequest
                    {
                        Requestid = requestData.Requestid.ToString(),
                        Phonenumber = requestData.Phonenumber,
                        Email = requestData.Email,
                        Reason = Note,
                        Createddate = DateTime.Now,
                        Modifieddate = DateTime.Now
                    };
                    _context.Blockrequests.Add(blc);
                    _context.SaveChanges();
                    return true;
                } 
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

        #region TransferProvider
        public async Task<bool> TransferProvider(int RequestId, int ProviderId, string notes)
        {
            var request = await _context.Requests.FirstOrDefaultAsync(req => req.Requestid == RequestId);
            Requeststatuslog rsl = new Requeststatuslog();
            rsl.Requestid = RequestId;
            rsl.Physicianid = request.Physicianid;
            rsl.Notes = notes;
            rsl.Createddate = DateTime.Now;
            rsl.Transtophysicianid = ProviderId;
            rsl.Status = 2;
            _context.Requeststatuslogs.Update(rsl);
            _context.SaveChanges();

            request.Physicianid = ProviderId;
            request.Status = 2;
            _context.Requests.Update(request);
            _context.SaveChanges();
            return true;
        }
        #endregion

        #region Clearcase
        public bool ClearCase(int RequestID)
        {
            try
            {
                var requestData = _context.Requests.FirstOrDefault(e => e.Requestid == RequestID);
                if (requestData != null)
                {
                    requestData.Status = 10;
                    _context.Requests.Update(requestData);
                    _context.SaveChanges();
                    Requeststatuslog rsl = new Requeststatuslog
                    {
                        Requestid = RequestID,
                        Status = 10,
                        Createddate = DateTime.Now
                    };
                    _context.Requeststatuslogs.Add(rsl);
                    _context.SaveChanges();
                    return true;
                }
                else { return false; }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

        #region getNotesByID
        public ViewNotesData getNotesByID(int id)
        {
            var req = _context.Requests.FirstOrDefault(W => W.Requestid == id);
            var symptoms = _context.Requestclients.FirstOrDefault(W => W.Requestid == id);
            var transferlog = (from rs in _context.Requeststatuslogs
                               join py in _context.Physicians on rs.Physicianid equals py.Physicianid into pyGroup
                               from py in pyGroup.DefaultIfEmpty()
                               join p in _context.Physicians on rs.Transtophysicianid equals p.Physicianid into pGroup
                               from p in pGroup.DefaultIfEmpty()
                               join a in _context.Admins on rs.Adminid equals a.Adminid into aGroup
                               from a in aGroup.DefaultIfEmpty()
                               where rs.Requestid == id && rs.Status == 2
                               select new TransferNotesData
                               {
                                   TransPhysician = p.Firstname,
                                   Admin = a.Firstname,
                                   Physician = py.Firstname,
                                   Requestid = rs.Requestid,
                                   Notes = rs.Notes,
                                   Status = rs.Status,
                                   Physicianid = rs.Physicianid,
                                   Createddate = rs.Createddate,
                                   Requeststatuslogid = rs.Requeststatuslogid,
                                   Transtoadmin = rs.Transtoadmin,
                                   Transtophysicianid = rs.Transtophysicianid
                               }).ToList(); 
            var cancelbyprovider = _context.Requeststatuslogs.Where(E => E.Requestid == id &&  (E.Transtoadmin != null));
            var cancel = _context.Requeststatuslogs.Where(E => E.Requestid == id && (E.Status == 7 || E.Status == 3));
            var model = _context.Requestnotes.FirstOrDefault(E => E.Requestid == id);
            ViewNotesData allData = new ViewNotesData();
            allData.Requestid = id;
            allData.Patientnotes = symptoms.Notes;
            if (model == null)
            {
                allData.Physiciannotes = "-";
                allData.Adminnotes = "-";
            }
            else
            {
                allData.Status = req.Status;
                allData.Requestnotesid = model.Requestnotesid;
                allData.Physiciannotes = model.Physiciannotes ?? "-";
                allData.Adminnotes = model.Adminnotes ?? "-";
            }
            
            List<TransferNotesData> transfer = new List<TransferNotesData>();
            foreach (var item in transferlog)
            {
                transfer.Add(new TransferNotesData
                {
                    TransPhysician = item.TransPhysician,
                    Admin = item.Admin,
                    Physician = item.Physician,
                    Requestid = item.Requestid,
                    Notes = item.Notes ?? "-",
                    Status = item.Status,
                    Physicianid = item.Physicianid,
                    Createddate = item.Createddate,
                    Requeststatuslogid = item.Requeststatuslogid,
                    Transtoadmin = item.Transtoadmin,
                    Transtophysicianid = item.Transtophysicianid
                });
            }
            allData.transfernotes = transfer;
            List<TransferNotesData> cancelbyphysician = new List<TransferNotesData>();
            foreach (var item in cancelbyprovider)
            {
                cancelbyphysician.Add(new TransferNotesData
                {
                    Requestid = item.Requestid,
                    Notes = item.Notes ?? "-",
                    Status = item.Status,
                    Physicianid = item.Physicianid,
                    Createddate = item.Createddate,
                    Requeststatuslogid = item.Requeststatuslogid,
                    Transtoadmin = item.Transtoadmin,
                    Transtophysicianid = item.Transtophysicianid
                });
            }
            allData.cancelbyphysician = cancelbyphysician;

            List<TransferNotesData> cancelrq = new List<TransferNotesData>();
            foreach (var item in cancel)
            {
                cancelrq.Add(new TransferNotesData
                {
                    Requestid = item.Requestid,
                    Notes = item.Notes ?? "-",
                    Status = item.Status,
                    Physicianid = item.Physicianid,
                    Createddate = item.Createddate,
                    Requeststatuslogid = item.Requeststatuslogid,
                    Transtoadmin = item.Transtoadmin,
                    Transtophysicianid = item.Transtophysicianid
                });
            }
            allData.cancel = cancelrq;

            return allData;
        }
        #endregion

        #region EditViewNotes
        public bool EditViewNotes(string? adminnotes, string? physiciannotes, int RequestID)
        {
            try
            {
                Requestnote notes = _context.Requestnotes.FirstOrDefault(E => E.Requestid == RequestID);
                if (notes != null)
                {
                    if (physiciannotes != null)
                    {
                        if (notes != null)
                        {
                            notes.Physiciannotes = physiciannotes;
                            notes.Modifieddate = DateTime.Now;
                            _context.Requestnotes.Update(notes);
                            _context.SaveChangesAsync();
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else if (adminnotes != null)
                    {
                        if (notes != null)
                        {
                            notes.Adminnotes = adminnotes;
                            notes.Modifieddate = DateTime.Now;
                            _context.Requestnotes.Update(notes);
                            _context.SaveChangesAsync();
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    Requestnote rn = new Requestnote
                    {
                        Requestid = RequestID,
                        Adminnotes = adminnotes,
                        Physiciannotes = physiciannotes,
                        Createddate = DateTime.Now,
                        Createdby = "001e35a5 - cd12 - 4ec8 - a077 - 95db9d54da0f"
                    };
                    _context.Requestnotes.Add(rn);
                    _context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

        #region GetDocumentByRequest
        public async Task<ViewDocuments> GetDocumentByRequest(int? id, ViewDocuments viewDocument)
        {
            var req = _context.Requests.FirstOrDefault(r => r.Requestid == id);
            var result = (from requestWiseFile in _context.Requestwisefiles
                         join request in _context.Requests on requestWiseFile.Requestid equals request.Requestid
                         join physician in _context.Physicians on request.Physicianid equals physician.Physicianid into physicianGroup
                         from phys in physicianGroup.DefaultIfEmpty()
                         join admin in _context.Admins on requestWiseFile.Adminid equals admin.Adminid into adminGroup
                         from adm in adminGroup.DefaultIfEmpty()
                         where request.Requestid == id && requestWiseFile.Isdeleted == new BitArray(1)
                         select new Documents
                         {
                             Uploader = requestWiseFile.Physicianid != null ? phys.Firstname : (requestWiseFile.Adminid != null ? adm.Firstname : request.Firstname),
                             isDeleted = requestWiseFile.Isdeleted.ToString(),
                             RequestwisefilesId = requestWiseFile.Requestwisefileid,
                             Status = requestWiseFile.Doctype,
                             Createddate = requestWiseFile.Createddate,
                             Filename = requestWiseFile.Filename
                         }).ToList();
            if (viewDocument.IsAscending == true)
            {
                result = viewDocument.SortedColumn switch
                {
                    "Uploader" => result.OrderBy(x => x.Uploader).ToList(),
                    "Filename" => result.OrderBy(x => x.Filename).ToList(),
                    "Createddate" => result.OrderBy(x => x.Createddate).ToList(),
                    _ => result.OrderBy(x => x.Createddate).ToList()
                };
            }
            else
            {
                result = viewDocument.SortedColumn switch
                {
                    "Uploader" => result.OrderByDescending(x => x.Uploader).ToList(),
                    "Filename" => result.OrderByDescending(x => x.Filename).ToList(),
                    "Createddate" => result.OrderByDescending(x => x.Createddate).ToList(),
                    _ => result.OrderByDescending(x => x.Createddate).ToList()
                };
            }
            int totalItemCount = result.Count();
            int totalPages = (int)Math.Ceiling(totalItemCount / (double)viewDocument.PageSize);
            List<Documents> list1 = result.Skip((viewDocument.CurrentPage - 1) * viewDocument.PageSize).Take(viewDocument.PageSize).ToList();
            ViewDocuments vd = new()
            {
                documentslist = list1,
                CurrentPage = viewDocument.CurrentPage,
                TotalPages = totalPages,
                PageSize = viewDocument.PageSize,
                SortedColumn = viewDocument.SortedColumn,
                IsAscending = viewDocument.IsAscending,
                Firstname = req.Firstname,
                Lastname = req.Lastname,
                ConfirmationNumber = req.Confirmationnumber,
                RequestID = req.Requestid
            };
            return vd;
        }
        #endregion

        #region Save_Document
        public bool SaveDoc(int Requestid, IFormFile file)
        {
            string UploadDoc = FileSave.UploadDoc(file, Requestid);
            var requestwisefile = new Requestwisefile
            {
                Requestid = Requestid,
                Filename = UploadDoc,
                Createddate = DateTime.Now,
                Isdeleted = new BitArray(1),
                Adminid = 1
            };
            _context.Requestwisefiles.Add(requestwisefile);
            _context.SaveChanges();
            return true;
        }
        #endregion

        #region DeleteDocumentByRequest
        public async Task<bool> DeleteDocumentByRequest(string ids)
        {
            List<int> deletelist = ids.Split(',').Select(int.Parse).ToList();
            foreach (int item in deletelist)
            {
                if (item > 0)
                {
                    var data = await _context.Requestwisefiles.Where(e => e.Requestwisefileid == item).FirstOrDefaultAsync();
                    if (data != null)
                    {
                        data.Isdeleted[0] = true;
                        _context.Requestwisefiles.Update(data);
                        _context.SaveChanges();
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        #endregion

        #region SendFileEmail
        public async Task<bool> SendFileEmail(string ids, int Requestid, string email)
        {
            var v = await GetRequestDetails(Requestid);
            List<int> priceList = ids.Split(',').Select(int.Parse).ToList();
            List<string> files = new();
            foreach (int price in priceList)
            {
                if (price > 0)
                {
                    var data = await _context.Requestwisefiles.Where(e => e.Requestwisefileid == price).FirstOrDefaultAsync();
                    files.Add(Directory.GetCurrentDirectory() + "\\wwwroot\\Upload" + data.Filename.Replace("Upload/", "").Replace("/", "\\"));
                }
            }

            if (await _emailConfig.SendMailAsync(email, "All Document Of Your Request " + v.PatientName, "Heeyy " + v.PatientName + " Kindly Check your Attachments", files))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        #endregion

        #region sendorderd
        public Healthprofessional SelectProfessionlByID(int VendorID)
        {
            return _context.Healthprofessionals.FirstOrDefault(e => e.Vendorid == VendorID);
        }
        public bool SendOrder(ViewSendOrderData data)
        {
            try
            {
                Orderdetail od = new Orderdetail
                {
                    Requestid = data.RequestID,
                    Vendorid = data.VendorID,
                    Faxnumber = data.FaxNumber,
                    Email = data.Email,
                    Businesscontact = data.BusinessContact,
                    Prescription = data.Prescription,
                    Noofrefill = data.NoOFRefill,
                    Createddate = DateTime.Now,
                    Createdby = "001e35a5 - cd12 - 4ec8 - a077 - 95db9d54da0f"
                };
                _context.Orderdetails.Add(od);
                _context.SaveChanges(true);
                var req = _context.Requests.FirstOrDefault(e => e.Requestid == data.RequestID);
                _emailConfig.SendMail(data.Email, "New Order arrived", "Prescription : " + data.Prescription + " Request name : " + req.Firstname);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

        #region SendAgreement
        public bool SendAgreement(int requestid)
        {
            var res = _context.Requestclients.FirstOrDefault(e => e.Requestid == requestid);
            var agreementUrl = "https://localhost:44306/SendAgreement/Index?RequestID=" + requestid;
            _emailConfig.SendMail(res.Email, "Agreement for your request", $"<a href='{agreementUrl}'>Agree/Disagree</a>");
            return true;
        }
        #endregion

        #region SendAgreement_accept
        public bool SendAgreement_accept(int RequestID)
        {
            var request = _context.Requests.Find(RequestID);
            if (request != null)
            {
                request.Status = 4;
                _context.Requests.Update(request);
                _context.SaveChanges();

                Requeststatuslog rsl = new Requeststatuslog();
                rsl.Requestid = RequestID;

                rsl.Status = 4;

                rsl.Createddate = DateTime.Now;

                _context.Requeststatuslogs.Add(rsl);
                _context.SaveChanges();

            }
            return true;
        }
        #endregion

        #region SendAgreement_Reject
        public bool SendAgreement_Reject(int RequestID, string Notes)
        {
            var request = _context.Requests.Find(RequestID);
            if (request != null)
            {
                request.Status = 7;
                _context.Requests.Update(request);
                _context.SaveChanges();

                Requeststatuslog rsl = new Requeststatuslog();
                rsl.Requestid = RequestID;

                rsl.Status = 7;
                rsl.Notes = Notes;

                rsl.Createddate = DateTime.Now;

                _context.Requeststatuslogs.Add(rsl);
                _context.SaveChanges();

            }
            return true;
        }
        #endregion

        #region Data_For_CloseCase
        public ViewCloseCaseModel CloseCaseData(int RequestID)
        {
            ViewCloseCaseModel alldata = new ViewCloseCaseModel();

            var result = from requestWiseFile in _context.Requestwisefiles
                         join request in _context.Requests on requestWiseFile.Requestid equals request.Requestid
                         join physician in _context.Physicians on request.Physicianid equals physician.Physicianid into physicianGroup
                         from phys in physicianGroup.DefaultIfEmpty()
                         join admin in _context.Admins on requestWiseFile.Adminid equals admin.Adminid into adminGroup
                         from adm in adminGroup.DefaultIfEmpty()
                         where request.Requestid == RequestID
                         select new
                         {

                             Uploader = requestWiseFile.Physicianid != null ? phys.Firstname :
                             (requestWiseFile.Adminid != null ? adm.Firstname : request.Firstname),
                             requestWiseFile.Filename,
                             requestWiseFile.Createddate,
                             requestWiseFile.Requestwisefileid

                         };
            List<Documents> doc = new List<Documents>();
            foreach (var item in result)
            {
                doc.Add(new Documents
                {
                    Createddate = item.Createddate,
                    Filename = item.Filename,
                    Uploader = item.Uploader,
                    RequestwisefilesId = item.Requestwisefileid

                });

            }
            alldata.documentslist = doc;
            Request req = _context.Requests.FirstOrDefault(r => r.Requestid == RequestID);

            alldata.Firstname = req.Firstname;
            alldata.RequestID = req.Requestid;
            alldata.ConfirmationNumber = req.Confirmationnumber;
            alldata.Lastname = req.Lastname;

            var reqcl = _context.Requestclients.FirstOrDefault(e => e.Requestid == RequestID);

            alldata.RC_Email = reqcl.Email;
            alldata.RC_Dob = new DateTime((int)reqcl.Intyear, DateTime.ParseExact(reqcl.Strmonth, "MMMM", new CultureInfo("en-US")).Month, (int)reqcl.Intdate);
            alldata.RC_FirstName = reqcl.Firstname;
            alldata.RC_LastName = reqcl.Lastname;
            alldata.RC_PhoneNumber = reqcl.Phonenumber;
            return alldata;
        }
        #endregion

        #region Edit_CloseCase
        public bool EditForCloseCase(ViewCloseCaseModel model)
        {
            try
            {
                Requestclient client = _context.Requestclients.FirstOrDefault(E => E.Requestid == model.RequestID);
                if (client != null)
                {
                    client.Phonenumber = model.RC_PhoneNumber;
                    client.Email = model.RC_Email;
                    _context.Requestclients.Update(client);
                    _context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

        #region CloseCase
        public bool CloseCase(int RequestID)
        {
            try
            {
                var requestData = _context.Requests.FirstOrDefault(e => e.Requestid == RequestID);
                if (requestData != null)
                {

                    requestData.Status = 9;
                    requestData.Modifieddate = DateTime.Now;

                    _context.Requests.Update(requestData);
                    _context.SaveChanges();

                    Requeststatuslog rsl = new Requeststatuslog
                    {
                        Requestid = RequestID,


                        Status = 9,
                        Createddate = DateTime.Now

                    };
                    _context.Requeststatuslogs.Add(rsl);
                    _context.SaveChanges();
                    return true;
                }
                else { return false; }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

        #region GetEncounterDetails
        public ViewEncounterData GetEncounterDetails(int RequestID)
        {
            var datareq = _context.Requestclients.FirstOrDefault(e => e.Requestid == RequestID);
            var Data = _context.Encounterforms.FirstOrDefault(e => e.Requestid == RequestID);
            DateTime? fd = new DateTime((int)datareq.Intyear, DateTime.ParseExact(datareq.Strmonth, "MMMM", new CultureInfo("en-US")).Month, (int)datareq.Intdate);
            if (Data != null)
            {
                ViewEncounterData enc = new ViewEncounterData
                {
                    ABD = Data.Abd,
                    EncounterID = Data.Encounterformid,
                    Allergies = Data.Allergies,
                    BloodPressureD = Data.Bloodpressurediastolic,
                    BloodPressureS = Data.Bloodpressurediastolic,
                    Chest = Data.Chest,
                    CV = Data.Cv,
                    DOB = new DateTime((int)datareq.Intyear, DateTime.ParseExact(datareq.Strmonth, "MMMM", new CultureInfo("en-US")).Month, (int)datareq.Intdate),
                    Date = DateTime.Now,
                    Diagnosis = Data.Diagnosis,
                    Hr = Data.Hr,
                    HistoryOfMedical = Data.Medicalhistory,
                    Heent = Data.Heent,
                    Extr = Data.Extremities,
                    PhoneNumber = datareq.Phonenumber,
                    Email = datareq.Email,
                    HistoryOfP = Data.Historyofpresentillnessorinjury,
                    FirstName = datareq.Firstname,
                    LastName = datareq.Lastname,
                    Followup = Data.Followup,
                    Location = datareq.Location,
                    Medications = Data.Medications,
                    MedicationsDispensed = Data.Medicaldispensed,
                    Neuro = Data.Neuro,
                    O2 = Data.O2,
                    Other = Data.Other,
                    Pain = Data.Pain,
                    Procedures = Data.Procedures,
                    Isfinalize = Data.Isfinalize,
                    Requesid = RequestID,
                    Rr = Data.Rr,
                    Skin = Data.Skin,
                    Temp = Data.Temp,
                    Treatment = Data.TreatmentPlan
                };
                return enc;
            }
            else
            {
                if (datareq != null)
                {
                    ViewEncounterData enc = new ViewEncounterData
                    {
                        FirstName = datareq.Firstname,
                        PhoneNumber = datareq.Phonenumber,
                        LastName = datareq.Lastname,
                        Location = datareq.Location,
                        DOB = new DateTime((int)datareq.Intyear, DateTime.ParseExact(datareq.Strmonth, "MMMM", new CultureInfo("en-US")).Month, (int)datareq.Intdate),
                        Date = DateTime.Now,
                        Requesid = RequestID,
                        Email = datareq.Email,
                    };
                    return enc;
                }
                else
                {
                    return new ViewEncounterData();
                }
            }
        }
        #endregion

        #region EditEncounterDetails
        public bool EditEncounterDetails(ViewEncounterData Data, string id)
        {
            try
            {
                var admindata = _context.Admins.FirstOrDefault(e => e.Aspnetuserid == id);
                if (Data.EncounterID == 0)
                {
                    Encounterform enc = new()
                    {
                        Abd = Data.ABD,
                        Encounterformid = (int)Data.EncounterID,
                        Allergies = Data.Allergies,
                        Bloodpressurediastolic = Data.BloodPressureD,
                        Bloodpressuresystolic = Data.BloodPressureS,
                        Chest = Data.Chest,
                        Cv = Data.CV,
                        Diagnosis = Data.Diagnosis,
                        Hr = Data.Hr,
                        Medicalhistory = Data.HistoryOfMedical,
                        Heent = Data.Heent,
                        Extremities = Data.Extr,
                        Historyofpresentillnessorinjury = Data.HistoryOfP,
                        Followup = Data.Followup,
                        Medications = Data.Medications,
                        Medicaldispensed = Data.MedicationsDispensed,
                        Neuro = Data.Neuro,
                        O2 = Data.O2,
                        Other = Data.Other,
                        Pain = Data.Pain,
                        Procedures = Data.Procedures,
                        Requestid = Data.Requesid,
                        Rr = Data.Rr,
                        Skin = Data.Skin,
                        Temp = Data.Temp,
                        TreatmentPlan = Data.Treatment,
                        Adminid = admindata.Adminid,
                        Createddate = DateTime.Now,
                        Modifieddate = DateTime.Now,
                    };
                    _context.Encounterforms.Add(enc);
                    _context.SaveChanges();
                    return true;
                }
                else
                {
                    var encdetails = _context.Encounterforms.FirstOrDefault(e => e.Encounterformid == Data.EncounterID);
                    if (encdetails != null)
                    {
                        encdetails.Abd = Data.ABD;
                        encdetails.Encounterformid = (int)Data.EncounterID;
                        encdetails.Allergies = Data.Allergies;
                        encdetails.Bloodpressurediastolic = Data.BloodPressureD;
                        encdetails.Bloodpressuresystolic = Data.BloodPressureS;
                        encdetails.Chest = Data.Chest;
                        encdetails.Cv = Data.CV;
                        encdetails.Diagnosis = Data.Diagnosis;
                        encdetails.Hr = Data.Hr;
                        encdetails.Medicalhistory = Data.HistoryOfMedical;
                        encdetails.Heent = Data.Heent;
                        encdetails.Extremities = Data.Extr;
                        encdetails.Historyofpresentillnessorinjury = Data.HistoryOfP;
                        encdetails.Followup = Data.Followup;
                        encdetails.Medications = Data.Medications;
                        encdetails.Medicaldispensed = Data.MedicationsDispensed;
                        encdetails.Neuro = Data.Neuro;
                        encdetails.O2 = Data.O2;
                        encdetails.Other = Data.Other;
                        encdetails.Pain = Data.Pain;
                        encdetails.Procedures = Data.Procedures;
                        encdetails.Requestid = Data.Requesid;
                        encdetails.Rr = Data.Rr;
                        encdetails.Skin = Data.Skin;
                        encdetails.Temp = Data.Temp;
                        encdetails.TreatmentPlan = Data.Treatment;
                        encdetails.Adminid = admindata.Adminid;
                        encdetails.Modifieddate = DateTime.Now;
                        _context.Encounterforms.Update(encdetails);
                        _context.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        #endregion

        #region GetRequestDetails
        public async Task<ViewActions> GetRequestDetails(int? id)
        {

            return await (from req in _context.Requests
                          join reqClient in _context.Requestclients
                          on req.Requestid equals reqClient.Requestid into reqClientGroup
                          from rc in reqClientGroup.DefaultIfEmpty()
                          join phys in _context.Physicians
                        on req.Physicianid equals phys.Physicianid into physGroup
                          from p in physGroup.DefaultIfEmpty()
                          where req.Requestid == id
                          select new ViewActions
                          {
                              PhoneNumber = rc.Phonenumber,
                              ProviderId = p.Physicianid,
                              PatientName = rc.Firstname + rc.Lastname,
                              RequestID = req.Requestid,
                              Email = rc.Email

                          }).FirstAsync();
        }
        #endregion

        #region Accept_Physician
        public async Task<bool> AcceptPhysician(int requestid,string note, int ProviderId)
        {

            var request = await _context.Requests.FirstOrDefaultAsync(req => req.Requestid == requestid);
            request.Status = 2;
            request.Accepteddate = DateTime.Now;
            _context.Requests.Update(request);
            _context.SaveChanges();

            Requeststatuslog rsl = new Requeststatuslog();
            rsl.Requestid = requestid;
            rsl.Createddate = DateTime.Now;
            rsl.Status = 2;
            rsl.Notes = note;
            rsl.Transtophysicianid = ProviderId;
            _context.Requeststatuslogs.Update(rsl);
            _context.SaveChanges();
            return true;
        }
        #endregion
        #region TransfertoAdmin
        public async Task<bool> TransfertoAdmin(int RequestID, string Note, int ProviderId)
        {
            var request = await _context.Requests.FirstOrDefaultAsync(req => req.Requestid == RequestID);
            request.Status = 1;
            request.Physicianid = null;
            _context.Requests.Update(request);
            _context.SaveChanges();

            Requeststatuslog rsl = new Requeststatuslog();
            rsl.Requestid = RequestID;
            rsl.Notes = Note;
            rsl.Createddate = DateTime.Now;
            rsl.Status = 1;

            rsl.Physicianid = ProviderId;
            rsl.Transtoadmin = new BitArray(1);
            rsl.Transtoadmin[0] = true;
            _context.Requeststatuslogs.Update(rsl);
            _context.SaveChanges();

            return true;
        }
        #endregion
    }
}
