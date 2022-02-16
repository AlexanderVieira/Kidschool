﻿using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Universal.EBI.Core.DomainObjects;
using Universal.EBI.Core.DomainObjects.Models;
using Universal.EBI.Core.DomainObjects.Models.Enums;
using Universal.EBI.Educators.API.Application.Queries.Interfaces;
using Universal.EBI.Educators.API.Models;
using Universal.EBI.Educators.API.Models.Interfaces;

namespace Universal.EBI.Educators.API.Application.Queries
{
    public class EducatorQueries : IEducatorQueries
    {
        private readonly IEducatorRepository _educatorRepository;

        public EducatorQueries(IEducatorRepository educatorRepository)
        {
            _educatorRepository = educatorRepository;
        }

        public async Task<Educator> GetEducatorByCpf(string cpf)
        {
            const string sql = @"SELECT 
                               E.ID AS EDUCATORID, E.FIRSTNAME, E.LASTNAME, E.FULLNAME, E.EMAIL, E.CPF, E.BIRTHDATE, E.GENDERTYPE, E.EXCLUDED, 
                               E.PHOTOURL, E.FUNCTIONTYPE, E.CREATEDDATE, E.CREATEDBY, E.LASTMODIFIEDDATE, E.LASTMODIFIEDBY, E.CLASSROOMID,
                               P.ID AS PHONEID, P.NUMBER AS PHONENUMBER, P.PHONETYPE, P.EDUCATORID AS EDUCATOR_ID, 
                               A.ID AS ADDRESSID, A.PUBLICPLACE, A.NUMBER, A.COMPLEMENT, A.DISTRICT, A.CITY, A.STATE, A.COUNTRY, A.ZIPCODE
                               FROM EDUCATORS E
                               INNER JOIN PHONES P
                               ON E.ID = P.EDUCATORID
                               INNER JOIN ADDRESSES A
                               ON E.ID = A.EDUCATORID
                               WHERE E.CPF = @cpf";

            using (var connection = _educatorRepository.GetConnection())
            {
                var result = await connection.QueryAsync<dynamic>(sql, new { Cpf = cpf });
                var educatorEntry = QueryDapper(result);
                return educatorEntry;
            }
        }
        
        public async Task<Educator> GetEducatorById(Guid id)
        {
            const string sql = @"SELECT 
                               E.ID AS EDUCATORID, E.FIRSTNAME, E.LASTNAME, E.FULLNAME, E.EMAIL, E.CPF, E.BIRTHDATE, E.GENDERTYPE, E.EXCLUDED, 
                               E.PHOTOURL, E.FUNCTIONTYPE, E.CREATEDDATE, E.CREATEDBY, E.LASTMODIFIEDDATE, E.LASTMODIFIEDBY, E.CLASSROOMID,
                               P.ID AS PHONEID, P.NUMBER AS PHONENUMBER, P.PHONETYPE, P.EDUCATORID AS EDUCATOR_ID, 
                               A.ID AS ADDRESSID, A.PUBLICPLACE, A.NUMBER, A.COMPLEMENT, A.DISTRICT, A.CITY, A.STATE, A.COUNTRY, A.ZIPCODE
                               FROM EDUCATORS E
                               INNER JOIN PHONES P
                               ON E.ID = P.EDUCATORID
                               INNER JOIN ADDRESSES A
                               ON E.ID = A.EDUCATORID
                               WHERE E.ID = @id";

            using (var connection = _educatorRepository.GetConnection())
            {
                var result = await connection.QueryAsync<dynamic>(sql, new { Id = id });
                var educatorEntry = QueryDapper(result);
                return educatorEntry;
            }
            
        }

        public Task<IEnumerable<Educator>> GetEducatorByName(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<PagedResult<Educator>> GetEducators(int pageSize, int pageIndex, string query = null)
        { //E.CREATEDDATE, E.CREATEDBY, E.LASTMODIFIEDDATE, E.LASTMODIFIEDBY, E.CLASSROOMID,           
            string sql = @$"SELECT
                         E.ID AS EDUCATORID, E.FIRSTNAME, E.LASTNAME, E.FULLNAME, E.EMAIL, E.CPF, E.BIRTHDATE, E.GENDERTYPE, E.EXCLUDED, 
                         E.PHOTOURL, E.FUNCTIONTYPE, E.CREATEDDATE, E.CREATEDBY, E.LASTMODIFIEDDATE, E.LASTMODIFIEDBY, E.CLASSROOMID,
                         P.ID AS PHONEID, P.NUMBER AS PHONENUMBER, P.PHONETYPE, P.EDUCATORID AS EDUCATOR_ID, 
                         A.ID AS ADDRESSID, A.PUBLICPLACE, A.NUMBER, A.COMPLEMENT, A.DISTRICT, A.CITY, A.STATE, A.COUNTRY, A.ZIPCODE
                         FROM EDUCATORS E
                         INNER JOIN PHONES P
                         ON E.ID = P.EDUCATORID
                         INNER JOIN ADDRESSES A
                         ON E.ID = A.EDUCATORID
                         WHERE (@FirstName IS NULL OR FirstName LIKE '%' + @FirstName + '%')
                         ORDER BY [FirstName] ASC
                         OFFSET {pageSize * (pageIndex - 1)} ROWS
                         FETCH NEXT {pageSize} ROWS ONLY
                         SELECT COUNT(Id) AS QUANTITY FROM EDUCATORS
                         WHERE (@FirstName IS NULL OR FirstName LIKE '%' + @FirstName + '%')";

            using (var connection = _educatorRepository.GetConnection())
            {
                var result = await connection.QueryAsync<dynamic>(sql, new { FirstName = query });
                //var result = await _educatorRepository.GetConnection().QueryAsync<dynamic>(sql, new { FirstName = query });

                var educatorDictionary = new Dictionary<string, Educator>();
                IDictionary<string, object> dict;
                var educators = new List<Educator>();
                Educator educatorEntry = null;

                if (result != null)
                {
                    for (int i = 0; i < result.Count(); i++)
                    {
                        var aux = result.ToList();
                        dict = aux[i] as IDictionary<string, object>;                        

                        if (!educatorDictionary.TryGetValue(dict["EDUCATORID"].ToString(), out educatorEntry))
                        {
                            var newEducator = new Educator
                            {
                                Id = Guid.Parse(dict["EDUCATORID"].ToString()),
                                FirstName = dict["FIRSTNAME"].ToString(),
                                LastName = dict["LASTNAME"].ToString(),
                                FullName = dict["FULLNAME"].ToString(),
                                Email = new Email(dict["EMAIL"].ToString()),
                                Cpf = new Cpf(dict["CPF"].ToString()),
                                Phones = new List<Phone>(),
                                Address = new Address(),
                                BirthDate = DateTime.Parse(dict["BIRTHDATE"].ToString()),
                                GenderType = (GenderType)Enum.Parse(typeof(GenderType), dict["GENDERTYPE"].ToString(), true),
                                FunctionType = (FunctionType)Enum.Parse(typeof(FunctionType), dict["FUNCTIONTYPE"].ToString(), true),
                                PhotoUrl = dict["PHOTOURL"] == null ? null : dict["PHOTOURL"].ToString(),
                                Excluded = bool.Parse(dict["EXCLUDED"].ToString()),
                                CreatedDate = DateTime.Parse(dict["CREATEDDATE"].ToString()),
                                CreatedBy = dict["CREATEDBY"].ToString(),
                                LastModifiedDate = dict["LASTMODIFIEDDATE"] == null ? null : DateTime.Parse(dict["LASTMODIFIEDDATE"].ToString()),
                                LastModifiedBy = dict["LASTMODIFIEDBY"] == null ? null : dict["LASTMODIFIEDBY"].ToString(),
                                ClassroomId = dict["CLASSROOMID"] == null ? null : Guid.Parse(dict["CLASSROOMID"].ToString())
                            };

                            newEducator.Address = new Address
                            {
                                Id = Guid.Parse(dict["ADDRESSID"].ToString()),
                                PublicPlace = dict["PUBLICPLACE"].ToString(),
                                Number = dict["NUMBER"].ToString(),
                                Complement = dict["COMPLEMENT"].ToString(),
                                District = dict["DISTRICT"].ToString(),
                                City = dict["CITY"].ToString(),
                                State = dict["STATE"].ToString(),
                                ZipCode = dict["ZIPCODE"].ToString(),
                                Country = dict["COUNTRY"].ToString(),
                                EducatorId = Guid.Parse(dict["EDUCATOR_ID"].ToString())

                            };

                            educatorEntry = newEducator;

                            educatorDictionary.Add(educatorEntry.Id.ToString(), educatorEntry);
                            educators.Add(educatorEntry);

                        }

                        educatorEntry.Phones.Add(new Phone
                        {
                            Id = Guid.Parse(dict["PHONEID"].ToString()),
                            Number = dict["PHONENUMBER"].ToString(),
                            PhoneType = (PhoneType)Enum.Parse(typeof(PhoneType), dict["PHONETYPE"].ToString(), true),
                            EducatorId = Guid.Parse(dict["EDUCATOR_ID"].ToString())
                        });

                    }

                    var total = educators.Count;
                    var pageResult = new PagedResult<Educator>
                    {
                        List = educators.OrderBy(e => e.FirstName).ToList(),
                        TotalResults = total,
                        PageIndex = pageIndex,
                        PageSize = pageSize,
                        Query = query
                    };

                    return pageResult;

                }
                else
                {
                    return null;
                }
            }            

        }

        private Educator QueryDapper(IEnumerable<dynamic> result)
        {
            var educatorDictionary = new Dictionary<string, Educator>();
            IDictionary<string, object> dict;
            Educator educatorEntry = null;

            if (result != null)
            {
                for (int i = 0; i < result.Count(); i++)
                {
                    var aux = result.ToList();
                    dict = aux[i] as IDictionary<string, object>;

                    if (!educatorDictionary.TryGetValue(dict["EDUCATORID"].ToString(), out educatorEntry))
                    {
                        var newEducator = new Educator
                        {
                            Id = Guid.Parse(dict["EDUCATORID"].ToString()),
                            FirstName = dict["FIRSTNAME"].ToString(),
                            LastName = dict["LASTNAME"].ToString(),
                            FullName = dict["FULLNAME"].ToString(),
                            Email = new Email(dict["EMAIL"].ToString()),
                            Cpf = new Cpf(dict["CPF"].ToString()),
                            Phones = new List<Phone>(),
                            Address = new Address(),                            
                            BirthDate = DateTime.Parse(dict["BIRTHDATE"].ToString()),
                            GenderType = (GenderType)Enum.Parse(typeof(GenderType), dict["GENDERTYPE"].ToString(), true),
                            FunctionType = (FunctionType)Enum.Parse(typeof(FunctionType), dict["FUNCTIONTYPE"].ToString(), true),
                            PhotoUrl = dict["PHOTOURL"] == null ? null : dict["PHOTOURL"].ToString(),
                            Excluded = bool.Parse(dict["EXCLUDED"].ToString()),
                            CreatedDate = DateTime.Parse(dict["CREATEDDATE"].ToString()),
                            CreatedBy = dict["CREATEDBY"].ToString(),
                            LastModifiedDate = dict["LASTMODIFIEDDATE"] == null ? null : DateTime.Parse(dict["LASTMODIFIEDDATE"].ToString()),
                            LastModifiedBy = dict["LASTMODIFIEDBY"] == null ? null : dict["LASTMODIFIEDBY"].ToString(),
                            ClassroomId = dict["CLASSROOMID"] == null ? null : Guid.Parse(dict["CLASSROOMID"].ToString())
                        };

                        newEducator.Address = new Address
                        {
                            Id = Guid.Parse(dict["ADDRESSID"].ToString()),
                            PublicPlace = dict["PUBLICPLACE"].ToString(),
                            Number = dict["NUMBER"].ToString(),
                            Complement = dict["COMPLEMENT"].ToString(),
                            District = dict["DISTRICT"].ToString(),
                            City = dict["CITY"].ToString(),
                            State = dict["STATE"].ToString(),
                            ZipCode = dict["ZIPCODE"].ToString(),
                            Country = dict["COUNTRY"].ToString(),
                            EducatorId = Guid.Parse(dict["EDUCATOR_ID"].ToString())

                        };

                        educatorEntry = newEducator;

                        educatorDictionary.Add(educatorEntry.Id.ToString(), educatorEntry);

                    }

                    educatorEntry.Phones.Add(new Phone
                    {
                        Id = Guid.Parse(dict["PHONEID"].ToString()),
                        Number = dict["PHONENUMBER"].ToString(),
                        PhoneType = (PhoneType)Enum.Parse(typeof(PhoneType), dict["PHONETYPE"].ToString(), true),
                        EducatorId = Guid.Parse(dict["EDUCATOR_ID"].ToString())
                    });

                }
                return educatorEntry;
            }
            else
            {
                return null;
            }
        }
    }
}