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
            request.Status = 2;
            _context.Requests.Update(request);
            _context.SaveChanges();
            Requeststatuslog rsl = new Requeststatuslog();
            rsl.Requestid = RequestId;
            rsl.Physicianid = ProviderId;
            rsl.Notes = notes;
            rsl.Createddate = DateTime.Now;
            rsl.Status = 2;
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
        public async Task<ViewDocuments> GetDocumentByRequest(int? id)
        {
            var req = _context.Requests.FirstOrDefault(r => r.Requestid == id);
            ViewDocuments doc = new ViewDocuments();
            doc.ConfirmationNumber = req.Confirmationnumber;
            doc.Firstname = req.Firstname;
            doc.Lastname = req.Lastname;
            doc.RequestID = req.Requestid;

            var result = from requestWiseFile in _context.Requestwisefiles
                         join request in _context.Requests on requestWiseFile.Requestid equals request.Requestid
                         join physician in _context.Physicians on request.Physicianid equals physician.Physicianid into physicianGroup
                         from phys in physicianGroup.DefaultIfEmpty()
                         join admin in _context.Admins on requestWiseFile.Adminid equals admin.Adminid into adminGroup
                         from adm in adminGroup.DefaultIfEmpty()
                         where request.Requestid == id && requestWiseFile.Isdeleted == new BitArray(1)
                         select new
                         {
                             Uploader = requestWiseFile.Physicianid != null ? phys.Firstname : (requestWiseFile.Adminid != null ? adm.Firstname : request.Firstname),
                             isDeleted = requestWiseFile.Isdeleted.ToString(),
                             RequestwisefilesId = requestWiseFile.Requestwisefileid,
                             Status = requestWiseFile.Doctype,
                             Createddate = requestWiseFile.Createddate,
                             Filename = requestWiseFile.Filename
                         };
            List<Documents> doclist = new List<Documents>();
            foreach (var item in result)
            {
                doclist.Add(new Documents
                {
                    Uploader = item.Uploader,
                    isDeleted = item.isDeleted,
                    RequestwisefilesId = item.RequestwisefilesId,
                    Status = item.Status,
                    Createddate = item.Createddate,
                    Filename = item.Filename
                });
            }
            doc.documentslist = doclist;
            return doc;
        }
        #endregion

        #region Save_Document
        public Boolean SaveDoc(int Requestid, IFormFile file)
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
                //_email.SendMail(data.Email, "New Order arrived", data.Prescription + "Request name" + req.Firstname);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

        #region SendAgreement
        public Boolean SendAgreement(int requestid)
        {
            var res = _context.Requestclients.FirstOrDefault(e => e.Requestid == requestid);
            var agreementUrl = "https://localhost:44306/SendAgreement?RequestID=" + requestid;
            _emailConfig.SendMail(res.Email, "Agreement for your request", $"<a href='{agreementUrl}'>Agree/Disagree</a>");
            return true;
        }
        #endregion

        #region SendAgreement_accept
        public Boolean SendAgreement_accept(int RequestID)
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
        public Boolean SendAgreement_Reject(int RequestID, string Notes)
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
    }
}
