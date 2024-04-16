using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalloDoc.Entity.DataContext;
using HalloDoc.Entity.DataModels;
using HalloDoc.Entity.Models;
using HallodocMVC.Repository.Admin.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace HallodocMVC.Repository.Admin.Repository
{
    public class Records : IRecords
    {
        #region Configuration
        private readonly HalloDocContext _context;
        public Records(HalloDocContext context)
        {
            _context = context;
        }
        #endregion Configuration

        #region GetFilteredSearchRecords
        public RecordsModel GetFilteredSearchRecords(RecordsModel rm)
        {
            List<SearchRecords> allData = (from req in _context.Requests
                                           join reqClient in _context.Requestclients
                                           on req.Requestid equals reqClient.Requestid into reqClientGroup
                                           from rc in reqClientGroup.DefaultIfEmpty()
                                           join phys in _context.Physicians
                                           on req.Physicianid equals phys.Physicianid into physGroup
                                           from p in physGroup.DefaultIfEmpty()
                                           join reg in _context.Regions
                                           on rc.Regionid equals reg.Regionid into RegGroup
                                           from rg in RegGroup.DefaultIfEmpty()
                                           join nts in _context.Requestnotes
                                           on req.Requestid equals nts.Requestid into ntsgrp
                                           from nt in ntsgrp.DefaultIfEmpty()
                                           where (req.Isdeleted == new BitArray(1) && rm.Status == null || req.Status == rm.Status) &&
                                                 (rm.RequestType == null || req.Requesttypeid == rm.RequestType) &&
                                                 (!rm.StartDate.HasValue || req.Createddate.Date >= rm.StartDate.Value.Date) &&
                                                 (!rm.EndDate.HasValue || req.Createddate.Date <= rm.EndDate.Value.Date) &&
                                                 (rm.PatientName.IsNullOrEmpty() || (rc.Firstname + " " + rc.Lastname).ToLower().Contains(rm.PatientName.ToLower())) &&
                                                 (rm.PhysicianName.IsNullOrEmpty() || (p.Firstname + " " + p.Lastname).ToLower().Contains(rm.PhysicianName.ToLower())) &&
                                                 (rm.Email.IsNullOrEmpty() || rc.Email.ToLower().Contains(rm.Email.ToLower())) &&
                                                 (rm.PhoneNumber.IsNullOrEmpty() || rc.Phonenumber.ToLower().Contains(rm.PhoneNumber.ToLower()))
                                           orderby req.Createddate
                                           select new SearchRecords
                                           {
                                               ModifiedDate = req.Modifieddate,
                                               PatientName = req.Firstname + " " + req.Lastname,
                                               RequestId = req.Requestid,
                                               DateOfService = req.Createddate,
                                               PhoneNumber = rc.Phonenumber ?? "",
                                               Email = rc.Email ?? "",
                                               Address = rc.Address ?? "",
                                               Zip = rc.Zipcode ?? "",
                                               RequestTypeId = req.Requesttypeid,
                                               Status = req.Status,
                                               PhysicianName = p.Firstname + " " + p.Lastname ?? "",
                                               AdminNote = nt != null ? nt.Adminnotes ?? "" : "",
                                               PhysicianNote = nt != null ? nt.Physiciannotes ?? "" : "",
                                               PatientNote = rc.Notes ?? ""
                                           }).ToList();

            int totalItemCount = allData.Count;
            int totalPages = (int)Math.Ceiling(totalItemCount / (double)rm.PageSize);
            List<SearchRecords> list = allData.Skip((rm.CurrentPage - 1) * rm.PageSize).Take(rm.PageSize).ToList();

            RecordsModel records = new()
            {
                SearchRecords = list,
                CurrentPage = rm.CurrentPage,
                TotalPages = totalPages,
                PageSize = rm.PageSize,
            };

            for (int i = 0; i < records.SearchRecords.Count; i++)
            {
                if (records.SearchRecords[i].Status == 9)
                {
                    records.SearchRecords[i].CloseCaseDate = records.SearchRecords[i].ModifiedDate;
                }
                else
                {
                    records.SearchRecords[i].CloseCaseDate = null;
                }
                if (records.SearchRecords[i].Status == 3 && records.SearchRecords[i].PhysicianName != null)
                {
                    var data = _context.Requeststatuslogs.FirstOrDefault(x => (x.Status == 3) && (x.Requestid == records.SearchRecords[i].RequestId));
                    records.SearchRecords[i].CancelByProviderNote = data.Notes;
                }

            }
            return records;
        }
        #endregion GetFilteredSearchRecords

        #region DeleteRequest
        public bool DeleteRequest(int? RequestId)
        {
            try
            {
                var data = _context.Requests.FirstOrDefault(v => v.Requestid == RequestId);
                data.Isdeleted = new BitArray(1);
                data.Isdeleted[0] = true;
                data.Modifieddate = DateTime.Now;
                _context.Requests.Update(data);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion DeleteRequest

        #region GetFilteredPatientHistory
        public RecordsModel GetFilteredPatientHistory(RecordsModel rm)
        {
            List<User> allData = (from user in _context.Users
                                  where (string.IsNullOrEmpty(rm.Email) || user.Email.ToLower().Contains(rm.Email.ToLower())) &&
                                  (string.IsNullOrEmpty(rm.PhoneNumber) || user.Mobile.ToLower().Contains(rm.PhoneNumber.ToLower())) &&
                                  (string.IsNullOrEmpty(rm.FirstName) || user.Firstname.ToLower().Contains(rm.FirstName.ToLower())) &&
                                  (string.IsNullOrEmpty(rm.LastName) || user.Lastname.ToLower().Contains(rm.LastName.ToLower()))
                                  select new User
                                  {
                                      Userid = user.Userid,
                                      Firstname = user.Firstname,
                                      Lastname = user.Lastname,
                                      Email = user.Email,
                                      Mobile = user.Mobile,
                                      Street = user.Street,
                                      City = user.City,
                                      State = user.State,
                                      Zipcode = user.Zipcode
                                  }).ToList();

            int totalItemCount = allData.Count;
            int totalPages = (int)Math.Ceiling(totalItemCount / (double)rm.PageSize);
            List<User> list = allData.Skip((rm.CurrentPage - 1) * rm.PageSize).Take(rm.PageSize).ToList();

            RecordsModel records = new()
            {
                Users = list,
                CurrentPage = rm.CurrentPage,
                TotalPages = totalPages,
                PageSize = rm.PageSize
            };
            return records;
        }
        #endregion GetFilteredPatientHistory

        #region PatientRecord
        public async Task<PaginatedViewModel> PatientRecord(int UserId, PaginatedViewModel data)
        {
            List<AdminDashboardList> allData = await (from req in _context.Requests
                                                      join reqClient in _context.Requestclients
                                                      on req.Requestid equals reqClient.Requestid into reqClientGroup
                                                      from rc in reqClientGroup.DefaultIfEmpty()
                                                      join phys in _context.Physicians
                                                      on req.Physicianid equals phys.Physicianid into physGroup
                                                      from p in physGroup.DefaultIfEmpty()
                                                      join reg in _context.Regions
                                                      on rc.Regionid equals reg.Regionid into RegGroup
                                                      from rg in RegGroup.DefaultIfEmpty()
                                                      where req.Userid == (UserId == null ? data.UserId : UserId)
                                                      select new AdminDashboardList
                                                      {
                                                          ProviderName = p.Firstname + " " + p.Lastname,
                                                          RequestClientId = rc.Requestclientid,
                                                          Status = req.Status,
                                                          RequestID = req.Requestid,
                                                          RequestTypeID = req.Requesttypeid,
                                                          Requestor = req.Firstname + " " + req.Lastname,
                                                          PatientName = rc.Firstname + " " + rc.Lastname,
                                                          RequestedDate = req.Createddate,
                                                          //Dob = new DateOnly((int)rc.Intyear, DateTime.ParseExact(rc.Strmonth, "MMMM", new CultureInfo("en-US")).Month, (int)rc.Intdate),
                                                          PhoneNumber = rc.Phonenumber,
                                                          Address = rc.Address + "," + rc.Street + "," + rc.City + "," + rc.State + "," + rc.Zipcode,
                                                          Notes = rc.Notes,
                                                          ProviderID = req.Physicianid,
                                                          RegionId = (int)rc.Regionid,
                                                          RequestorPhoneNumber = req.Phonenumber,
                                                          ConcludedDate = req.Createddate,
                                                          ConfirmationNumber = req.Confirmationnumber
                                                      }).ToListAsync();

            int totalItemCount = allData.Count;
            int totalPages = (int)Math.Ceiling(totalItemCount / (double)data.PageSize);
            List<AdminDashboardList> result = allData.Skip((data.CurrentPage - 1) * data.PageSize).Take(data.PageSize).ToList();

            PaginatedViewModel model = new()
            {
                UserId = UserId,
                adl = result,
                CurrentPage = data.CurrentPage,
                TotalPages = totalPages,
                PageSize = data.PageSize,
                SearchInput = data.SearchInput
            };
            return model;
        }
        #endregion PatientRecord

        #region BlockHistory
        public RecordsModel BlockHistory(RecordsModel rm)
        {
            List<BlockRequests> data = (from req in _context.Blockrequests
                                        where (!rm.StartDate.HasValue || req.Createddate.Value.Date == rm.StartDate.Value.Date) &&
                                              (rm.PatientName.IsNullOrEmpty() || _context.Requests.FirstOrDefault(e => e.Requestid == Convert.ToInt32(req.Requestid)).Firstname.ToLower().Contains(rm.PatientName.ToLower())) &&
                                              (rm.Email.IsNullOrEmpty() || req.Email.ToLower().Contains(rm.Email.ToLower())) &&
                                              (rm.PhoneNumber.IsNullOrEmpty() || req.Phonenumber.ToLower().Contains(rm.PhoneNumber.ToLower()))
                                        select new BlockRequests
                                        {
                                            PatientName = _context.Requests.FirstOrDefault(e => e.Requestid == Convert.ToInt32(req.Requestid)).Firstname,
                                            Email = req.Email,
                                            CreatedDate = (DateTime)req.Createddate,
                                            IsActive = req.Isactive,
                                            RequestId = Convert.ToInt32(req.Requestid),
                                            PhoneNumber = req.Phonenumber,
                                            Reason = req.Reason
                                        }).ToList();

            int totalItemCount = data.Count;
            int totalPages = (int)Math.Ceiling(totalItemCount / (double)rm.PageSize);
            List<BlockRequests> list = data.Skip((rm.CurrentPage - 1) * rm.PageSize).Take(rm.PageSize).ToList();

            RecordsModel model = new()
            {
                BlockRequests = list,
                CurrentPage = rm.CurrentPage,
                TotalPages = totalPages,
                PageSize = rm.PageSize,
            };

            return model;
        }
        #endregion BlockHistory

        #region Unblock
        public bool Unblock(int RequestId, string id)
        {
            try
            {
                Blockrequest block = _context.Blockrequests.FirstOrDefault(e => e.Requestid == RequestId.ToString());
                block.Isactive = new BitArray(1);
                block.Isactive[0] = false;
                _context.Blockrequests.Update(block);
                _context.SaveChanges();

                Request request = _context.Requests.FirstOrDefault(e => e.Requestid == RequestId);
                request.Status = 1;
                request.Modifieddate = DateTime.Now;
                _context.Requests.Update(request);
                _context.SaveChanges();

                var admindata = _context.Admins.FirstOrDefault(e => e.Aspnetuserid == id);
                Requeststatuslog rs = new()
                {
                    Status = 1,
                    Requestid = RequestId,
                    Adminid = admindata.Adminid,
                    Createddate = DateTime.Now
                };
                _context.Requeststatuslogs.Add(rs);
                _context.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion Unblock

        #region block
        public bool block(int RequestId, string id)
        {
            try
            {
                Blockrequest block = _context.Blockrequests.FirstOrDefault(e => e.Requestid == RequestId.ToString());
                block.Isactive = new BitArray(1);
                block.Isactive[0] = true;
                _context.Blockrequests.Update(block);
                _context.SaveChanges();

                Request request = _context.Requests.FirstOrDefault(e => e.Requestid == RequestId);
                request.Status = 11;
                request.Modifieddate = DateTime.Now;
                _context.Requests.Update(request);
                _context.SaveChanges();

                var admindata = _context.Admins.FirstOrDefault(e => e.Aspnetuserid == id);
                Requeststatuslog rs = new()
                {
                    Status = 11,
                    Requestid = RequestId,
                    Adminid = admindata.Adminid,
                    Createddate = DateTime.Now
                };
                _context.Requeststatuslogs.Add(rs);
                _context.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion block

        #region GetFilteredEmailLogs
        public RecordsModel GetFilteredEmailLogs(RecordsModel rm)
        {
            List<EmailLogs> allData = (from req in _context.Emaillogs
                                       where (rm.AccountType == null || rm.AccountType == 0 || req.Roleid == rm.AccountType) &&
                                             (!rm.StartDate.HasValue || req.Createdate.Date == rm.StartDate.Value.Date) &&
                                             (!rm.EndDate.HasValue || req.Sentdate.Value.Date == rm.EndDate.Value.Date) &&
                                             (rm.ReceiverName.IsNullOrEmpty() || _context.Aspnetusers.FirstOrDefault(e => e.Email == req.Emailid).Username.ToLower().Contains(rm.ReceiverName.ToLower())) &&
                                             (rm.Email.IsNullOrEmpty() || req.Emailid.ToLower().Contains(rm.Email.ToLower()))
                                       select new EmailLogs
                                       {
                                           Recipient = _context.Aspnetusers.FirstOrDefault(e => e.Email == req.Emailid).Username ?? null,
                                           ConfirmationNumber = req.Confirmationnumber,
                                           CreateDate = req.Createdate,
                                           EmailTemplate = req.Emailtemplate,
                                           FilePath = req.Filepath,
                                           SentDate = (DateTime)req.Sentdate,
                                           RoleId = req.Roleid,
                                           EmailId = req.Emailid,
                                           SentTries = req.Senttries,
                                           SubjectName = req.Subjectname,
                                           Action = (int)req.Action
                                       }).ToList();

            int totalItemCount = allData.Count;
            int totalPages = (int)Math.Ceiling(totalItemCount / (double)rm.PageSize);
            List<EmailLogs> list = allData.Skip((rm.CurrentPage - 1) * rm.PageSize).Take(rm.PageSize).ToList();

            RecordsModel records = new()
            {
                EmailLogs = list,
                CurrentPage = rm.CurrentPage,
                TotalPages = totalPages,
                PageSize = rm.PageSize
            };

            return records;
        }
        #endregion GetFilteredEmailLogs

        #region GetFilteredSMSLogs
        public RecordsModel GetFilteredSMSLogs(RecordsModel rm)
        {
            List<SMSLogs> allData = (from req in _context.Smslogs
                                     where (rm.AccountType == null || rm.AccountType == 0 || req.Roleid == rm.AccountType) &&
                                           (!rm.StartDate.HasValue || req.Createdate.Date == rm.StartDate.Value.Date) &&
                                           (!rm.EndDate.HasValue || req.Sentdate.Value.Date == rm.EndDate.Value.Date) &&
                                           (rm.ReceiverName.IsNullOrEmpty() || _context.Aspnetusers.FirstOrDefault(e => e.Phonenumber == req.Mobilenumber).Username.ToLower().Contains(rm.ReceiverName.ToLower())) &&
                                           (rm.PhoneNumber.IsNullOrEmpty() || req.Mobilenumber.ToLower().Contains(rm.PhoneNumber.ToLower()))
                                     select new SMSLogs
                                     {
                                         Recipient = _context.Aspnetusers.FirstOrDefault(e => e.Phonenumber == req.Mobilenumber).Username,
                                         ConfirmatioNumber = req.Confirmationnumber,
                                         CreateDate = req.Createdate,
                                         SmsTemplate = req.Smstemplate,
                                         SentDate = (DateTime)req.Sentdate,
                                         RoleId = req.Roleid,
                                         MobileNumber = req.Mobilenumber,
                                         SentTries = req.Senttries,
                                         Action = req.Action
                                     }).ToList();

            int totalItemCount = allData.Count;
            int totalPages = (int)Math.Ceiling(totalItemCount / (double)rm.PageSize);
            List<SMSLogs> list = allData.Skip((rm.CurrentPage - 1) * rm.PageSize).Take(rm.PageSize).ToList();

            RecordsModel records = new()
            {
                SMSLogs = list,
                CurrentPage = rm.CurrentPage,
                TotalPages = totalPages,
                PageSize = rm.PageSize
            };
            return records;
        }
        #endregion GetFilteredSMSLogs
    }
}
