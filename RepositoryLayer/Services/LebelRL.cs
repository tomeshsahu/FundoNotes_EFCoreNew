using DatabaseLayer.LebelModel;
using Microsoft.Data.SqlClient.DataClassification;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using RepositoryLayer.Services.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Label = RepositoryLayer.Services.Entity.Label;

namespace RepositoryLayer.Services
{
    public class LebelRL : ILebelRL
    {
        FundooContext fundooContext;
        IConfiguration iconfiguration;

        public LebelRL(FundooContext fundooContext,IConfiguration iconfiguration)
        {
            this.fundooContext=fundooContext;
            this.iconfiguration=iconfiguration;
        }

        public async Task AddLebel(int UserId, int NoteId, string LebelName)
        {
            try
            {
                Label lebel=new Label();
                lebel.UserId = UserId;
                lebel.NoteId= NoteId;
                lebel.LabelName= LebelName;

                this.fundooContext.Labels.Add(lebel);
                await this.fundooContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteLebel(int UserId, int NoteId)
        {
            try
            {
                var result = this.fundooContext.Labels.Where(x => x.NoteId == NoteId && x.UserId == UserId).FirstOrDefault();
                if(result==null)
                {
                    return false;
                }
                this.fundooContext.Labels.Remove(result);
                this.fundooContext.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<LebelResponseModel>> GetAllLabels(int userId)
        {
            try
            {
                var label=this.fundooContext.Labels.FirstOrDefault(x=>x.UserId== userId);
                var result = await (from users in fundooContext.Users
                                    join notes in fundooContext.Notes on users.UserId equals userId
                                    join labels in fundooContext.Labels on notes.NoteId equals labels.NoteId
                                    where labels.UserId == userId
                                    select new LebelResponseModel
                                    {
                                        LebelId = labels.LabelId,
                                        LebelName=labels.LabelName,
                                        UserId=userId,
                                        NodeId=notes.NoteId,
                                        Title=notes.Title,
                                        Description=notes.Description,
                                        FirstName=users.Firstname,
                                        LastName=users.Lastname,
                                        Email=users.Email,
                                    }).ToListAsync();
                return result;

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public Task<List<LebelResponseModel>> GetLebelByNoteId(int UserId, int NoteId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateLebel(int UserId, int NoteId, string LebelName)
        {
            throw new NotImplementedException();
        }
    }
}
